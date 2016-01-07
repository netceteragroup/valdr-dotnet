namespace Nca.Valdr
{
    using Newtonsoft.Json.Linq;
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Resources;

    public static class Parser
    {
        private const string RequiredMessage = "{0} is requiered.";
        private const string LengthMessage = "{0} must be between {1} and {2} characters.";
        private const string RangeMessage = "{0} must be between {1} and {2}.";
        private const string EmailMessage = "{0} must be a valid E-Mail address.";
        private const string UrlMessage = "{0} must be a valid URL.";
        private const string RegexMessage = "{0} must be a valid pattern.";
        private static string _assemblyFile;

        /// <summary>
        /// Parses classes with a "DataContractAttribute" and generates the valdr constraint metadata.
        /// </summary>
        /// <param name="assemblyFile">Input assembly path.</param>
        /// <param name="targetNamespace">Target namespace for parsing.</param>
        /// <returns>JSON metadata object.</returns>
        public static JObject Parse(string assemblyFile, string targetNamespace)
        {
            dynamic jsonResult = new JObject();
            _assemblyFile = assemblyFile;
            AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += (s, e) => CustomReflectionOnlyResolver(e);
            var assembly = Assembly.ReflectionOnlyLoadFrom(assemblyFile);
            var typeQuery = assembly.GetTypes()
                .Where(t => t.IsClass && t.Namespace != null && t.Namespace.StartsWith(targetNamespace) &&
                            t.GetCustomAttributesData().Any(a => a.AttributeType.Name == "DataContractAttribute"));

            foreach (var type in typeQuery.ToList())
            {
                var contract = type.GetCustomAttributesData()
                    .FirstOrDefault(a => a.AttributeType.Name == "DataContractAttribute");
                if (contract?.NamedArguments != null)
                {
                    var contractName = contract.NamedArguments
                        .FirstOrDefault(n => n.MemberName == "Name");
                    var typeName = contractName.TypedValue.Value != null ?
                        (string)contractName.TypedValue.Value : type.Name;
                    jsonResult[typeName] = new JObject();

                    foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                    {
                        GetProperty(typeName, property, jsonResult);
                    }
                }
            }

            return jsonResult;
        }

        private static Assembly CustomReflectionOnlyResolver(ResolveEventArgs args)
        {
            var name = new AssemblyName(args.Name);
            var assemblyPath = Path.Combine(
                Path.GetDirectoryName(_assemblyFile) ?? string.Empty,
                name.Name + ".dll");

            if (File.Exists(assemblyPath))
            {
                return Assembly.ReflectionOnlyLoadFrom(assemblyPath);
            }

            return Assembly.ReflectionOnlyLoad(args.Name);
        }

        private static void GetProperty(string typeName, PropertyInfo property, dynamic jsonResult)
        {
            var required = GetPropertyAttribute(property, "RequiredAttribute");
            var length = GetPropertyAttribute(property, "StringLengthAttribute");
            var range = GetPropertyAttribute(property, "RangeAttribute");
            var email = GetPropertyAttribute(property, "EmailAddressAttribute");
            var url = GetPropertyAttribute(property, "UrlAttribute");
            var regex = GetPropertyAttribute(property, "RegularExpressionAttribute");

            if (required == null && length == null && range == null && email == null && url == null && regex == null)
            {
                return;
            }

            var member = GetPropertyAttribute(property, "DataMemberAttribute");
            var display = GetPropertyAttribute(property, "DisplayAttribute");
            var propertyName = member?.Name ?? property.Name;
            var displayName = display?.Name != null ? GetText(display.Name, display.ResourceType, display.Name) : property.Name;
            jsonResult[typeName][propertyName] = new JObject();

            if (required != null)
            {
                var text = GetText(required.Message ?? RequiredMessage,
                    required.ResourceType, required.ResourceName,
                    displayName);
                jsonResult[typeName][propertyName]["required"] = JObject.FromObject(new
                {
                    message = text
                });
            }

            if (length != null)
            {
                var text = GetText(length.Message ?? LengthMessage,
                    length.ResourceType, length.ResourceName,
                    displayName, length.Minimum, length.Maximum);
                jsonResult[typeName][propertyName]["size"] = JObject.FromObject(new
                {
                    min = length.Minimum,
                    max = length.Maximum,
                    message = text
                });
            }

            if (range != null)
            {
                var text = GetText(range.Message ?? RangeMessage,
                    range.ResourceType, range.ResourceName,
                    displayName, range.Minimum, range.Maximum);
                jsonResult[typeName][propertyName]["min"] = JObject.FromObject(new
                {
                    value = range.Minimum,
                    message = text
                });
                jsonResult[typeName][propertyName]["max"] = JObject.FromObject(new
                {
                    value = range.Maximum,
                    message = text
                });
            }

            if (email != null)
            {
                var text = GetText(email.Message ?? EmailMessage,
                    email.ResourceType, email.ResourceName,
                    displayName);
                jsonResult[typeName][propertyName]["email"] = JObject.FromObject(new
                {
                    message = text
                });
            }

            if (url != null)
            {
                var text = GetText(url.Message ?? UrlMessage,
                    url.ResourceType, url.ResourceName,
                    displayName);
                jsonResult[typeName][propertyName]["url"] = JObject.FromObject(new
                {
                    message = text
                });
            }

            if (regex != null)
            {
                var text = GetText(regex.Message ?? RegexMessage,
                    regex.ResourceType, regex.ResourceName,
                    displayName);
                jsonResult[typeName][propertyName]["pattern"] = JObject.FromObject(new
                {
                    value = regex.Pattern,
                    message = text
                });
            }
        }

        private static ValdrAttribute GetPropertyAttribute(PropertyInfo prop, string attributeName)
        {
            foreach (var data in prop.GetCustomAttributesData())
            {
                if (data.AttributeType.Name == attributeName)
                {
                    var attr = new ValdrAttribute();

                    if (attributeName == "StringLengthAttribute" && data.ConstructorArguments.Count > 0)
                    {
                        attr.Maximum = (int)data.ConstructorArguments[0].Value;
                    }
                    else if (attributeName == "RangeAttribute" && data.ConstructorArguments.Count > 1)
                    {
                        attr.Minimum = (int)data.ConstructorArguments[0].Value;
                        attr.Maximum = (int)data.ConstructorArguments[1].Value;
                    }
                    else if (attributeName == "RegularExpressionAttribute" && data.ConstructorArguments.Count > 0)
                    {
                        attr.Pattern = (string)data.ConstructorArguments[0].Value;
                    }

                    if (data.NamedArguments != null)
                    {
                        foreach (var item in data.NamedArguments)
                        {
                            GetAttribute(attr, item.MemberName, item.TypedValue.Value);
                        }
                    }

                    return attr;
                }
            }

            return null;
        }

        private static void GetAttribute(ValdrAttribute attr, string name, object arg)
        {
            switch (name)
            {
                case "Name":
                    attr.Name = (string)arg;
                    break;
                case "ErrorMessage":
                    attr.Message = (string)arg;
                    break;
                case "ResourceType":
                case "ErrorMessageResourceType":
                    attr.ResourceType = (Type)arg;
                    break;
                case "ErrorMessageResourceName":
                    attr.ResourceName = (string)arg;
                    break;
                case "Minimum":
                case "MinimumLength":
                    attr.Minimum = (int)arg;
                    break;
                case "Maximum":
                case "MaximumLength":
                    attr.Maximum = (int)arg;
                    break;
                case "Pattern":
                    attr.Pattern = (string)arg;
                    break;
            }
        }

        private static string GetText(string text, Type resourceType, string resourceName, params object[] args)
        {
            var result = text;
            if (resourceType != null && !string.IsNullOrEmpty(resourceName))
            {
                var resourceManager = new ResourceManager(resourceType);
                var manifests = resourceType.Assembly.GetManifestResourceNames();
                if (manifests.Length == 1)
                {
                    var manifest = manifests[0].Replace(".resources", string.Empty);
                    resourceManager = new ResourceManager(manifest, resourceType.Assembly);
                }

                result = resourceManager.GetString(resourceName);
            }

            if (args.Length > 0 && result != null)
            {
                result = string.Format(result, args);
            }

            return result;
        }
    }
}
