namespace Nca.Valdr
{
    using Newtonsoft.Json.Linq;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Resources;

    /// <summary>
    /// Valdr metadata generator
    /// </summary>
    public interface IParser
    {
        /// <summary>
        /// Parses classes from assemblies provided into valdr constraint metadata.
        /// </summary>
        /// <param name="culture">Culture to use when resolving validation messages from resources.</param>
        /// <param name="targetNamespace">String to filter namespaces checked - if provided, only namespaces starting with the filter string will be considered.</param>
        /// <param name="attribute">Description of attribute used to identify ValdrType classes.  NOTE: attributes used here MUST use named arguments to be picked up correctly by parser</param>
        /// <param name="dataMemberAttributeName">Name of attribute used to identify DataMembers (optional).  NOTE: attributes MUST use named arguments to be picked up correctly by parser</param>
        /// <param name="assemblies">Assemblies to parse for models needing valdr constraints.</param>
        /// <returns>JSON metadata object</returns>
        JObject Parse(CultureInfo culture, string targetNamespace, ValdrTypeAttributeDescriptor attribute, string dataMemberAttributeName, params Assembly[] assemblies);
    }

    /// <summary>
    /// Valdr metadata generator
    /// </summary>
    public class Parser : IParser
    {
        private const string RequiredMessage = "{0} is required.";
        private const string LengthMessage = "{0} must be between {1} and {2} characters.";
        private const string RangeMessage = "{0} must be between {1} and {2}.";
        private const string EmailMessage = "{0} must be a valid E-Mail address.";
        private const string UrlMessage = "{0} must be a valid URL.";
        private const string RegexMessage = "{0} must have a valid pattern.";

        /// <summary>
        /// Parses classes from assemblies provided into valdr constraint metadata.
        /// </summary>
        /// <param name="culture">Culture to use when resolving validation messages from resources.</param>
        /// <param name="targetNamespace">String to filter namespaces checked - if provided, only namespaces starting with the filter string will be considered.</param>
        /// <param name="attribute">Description of attribute used to identify ValdrType classes.  NOTE: attributes used here MUST use named arguments to be picked up correctly by parser</param>
        /// <param name="dataMemberAttributeName">Name of attribute used to identify DataMembers (optional).  NOTE: attributes MUST use named arguments to be picked up correctly by parser</param>
        /// <param name="assemblies">Assemblies to parse for models needing valdr constraints.</param>
        /// <exception cref="ArgumentNullException">Assembies is null.</exception>
        /// <returns>JSON metadata object</returns>
        public JObject Parse(CultureInfo culture, string targetNamespace, ValdrTypeAttributeDescriptor attribute, string dataMemberAttributeName, params Assembly[] assemblies)
        {
            if (assemblies == null || assemblies.Length == 0)
            {
                throw new ArgumentNullException(nameof(assemblies));
            }

            var jsonResult = new JObject();
            foreach (var assembly in assemblies)
            {
                var typeQuery = assembly.GetTypes()
                    .Where(t => t.IsClass && t.Namespace != null && t.Namespace.StartsWith(targetNamespace ?? string.Empty, StringComparison.OrdinalIgnoreCase) &&
                                t.GetCustomAttributesData()
                                    .Any(a => a.AttributeType.Name == attribute.TypeName));

                foreach (var type in typeQuery.ToList())
                {
                    var contract = type.GetCustomAttributesData()
                        .FirstOrDefault(a => a.AttributeType.Name == attribute.TypeName);
                    if (contract?.NamedArguments != null)
                    {
                        var contractName = contract.NamedArguments
                            .FirstOrDefault(n => n.MemberName == attribute.ValdrTypePropertyName);
                        var typeName = contractName.TypedValue.Value != null
                            ? (string)contractName.TypedValue.Value
                            : type.Name;
                        jsonResult[typeName] = new JObject();

                        foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                        {
                            GetProperty(typeName, property, culture, dataMemberAttributeName, jsonResult);
                        }
                    }
                }
            }

            return jsonResult;
        }

        private void GetProperty(string typeName, PropertyInfo property, CultureInfo culture, string dataMemberAttributeName, dynamic jsonResult)
        {
            var required = GetPropertyAttribute(property, nameof(RequiredAttribute));
            var length = GetPropertyAttribute(property, nameof(StringLengthAttribute));
            var range = GetPropertyAttribute(property, nameof(RangeAttribute));
            var email = GetPropertyAttribute(property, nameof(EmailAddressAttribute));
            var url = GetPropertyAttribute(property, nameof(UrlAttribute));
            var regex = GetPropertyAttribute(property, nameof(RegularExpressionAttribute));

            if (required == null && length == null && range == null && email == null && url == null && regex == null)
            {
                return;
            }

            var member = GetPropertyAttribute(property, dataMemberAttributeName);
            var display = GetPropertyAttribute(property, nameof(DisplayAttribute));
            var propertyName = member?.Name ?? property.Name;
            var displayName = display?.Name != null ? GetText(display.Name, display.ResourceType, display.Name, culture) : property.Name;
            jsonResult[typeName][propertyName] = new JObject();

            if (required != null)
            {
                var text = GetText(required.Message ?? RequiredMessage,
                    required.ResourceType, required.ResourceName, culture, displayName);
                jsonResult[typeName][propertyName]["required"] = JObject.FromObject(new
                {
                    message = text
                });
            }

            if (length != null)
            {
                var text = GetText(length.Message ?? LengthMessage,
                    length.ResourceType, length.ResourceName, culture,
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
                    range.ResourceType, range.ResourceName, culture,
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
                    email.ResourceType, email.ResourceName, culture,
                    displayName);
                jsonResult[typeName][propertyName]["email"] = JObject.FromObject(new
                {
                    message = text
                });
            }

            if (url != null)
            {
                var text = GetText(url.Message ?? UrlMessage,
                    url.ResourceType, url.ResourceName, culture,
                    displayName);
                jsonResult[typeName][propertyName]["url"] = JObject.FromObject(new
                {
                    message = text
                });
            }

            if (regex != null)
            {
                var text = GetText(regex.Message ?? RegexMessage,
                    regex.ResourceType, regex.ResourceName, culture,
                    displayName);
                jsonResult[typeName][propertyName]["pattern"] = JObject.FromObject(new
                {
                    value = regex.Pattern,
                    message = text
                });
            }
        }

        private ValdrAttribute GetPropertyAttribute(PropertyInfo prop, string attributeName)
        {
            foreach (var data in prop.GetCustomAttributesData())
            {
                if (data.AttributeType.Name == attributeName)
                {
                    var attr = new ValdrAttribute();

                    if (attributeName == nameof(StringLengthAttribute) && data.ConstructorArguments.Count > 0)
                    {
                        attr.Maximum = (int)data.ConstructorArguments[0].Value;
                    }
                    else if (attributeName == nameof(RangeAttribute) && data.ConstructorArguments.Count > 1)
                    {
                        attr.Minimum = (int)data.ConstructorArguments[0].Value;
                        attr.Maximum = (int)data.ConstructorArguments[1].Value;
                    }
                    else if (attributeName == nameof(RegularExpressionAttribute) && data.ConstructorArguments.Count > 0)
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

        private void GetAttribute(ValdrAttribute attr, string name, object arg)
        {
            switch (name)
            {
                case nameof(DisplayAttribute.Name):
                    attr.Name = (string)arg;
                    break;
                case nameof(RequiredAttribute.ErrorMessage):
                    attr.Message = (string)arg;
                    break;
                case nameof(DisplayAttribute.ResourceType):
                case nameof(RequiredAttribute.ErrorMessageResourceType):
                    attr.ResourceType = (Type)arg;
                    break;
                case nameof(RequiredAttribute.ErrorMessageResourceName):
                    attr.ResourceName = (string)arg;
                    break;
                case nameof(RangeAttribute.Minimum):
                case nameof(StringLengthAttribute.MinimumLength):
                    attr.Minimum = (int)arg;
                    break;
                case nameof(RangeAttribute.Maximum):
                case nameof(StringLengthAttribute.MaximumLength):
                    attr.Maximum = (int)arg;
                    break;
                case nameof(RegularExpressionAttribute.Pattern):
                    attr.Pattern = (string)arg;
                    break;
            }
        }

        private string GetText(string text, Type resourceType, string resourceName, CultureInfo culture, params object[] args)
        {
            var result = text;
            if (resourceType != null && !string.IsNullOrEmpty(resourceName))
            {
                var resourceManager = new ResourceManager(resourceType);
                var manifests = resourceType.Assembly.GetManifestResourceNames();
                var baseName = manifests.FirstOrDefault(m => m.Contains(resourceType.Name));
                if (!string.IsNullOrEmpty(baseName))
                {
                    resourceManager = new ResourceManager(baseName.Replace(".resources", string.Empty), resourceType.Assembly);
                }

                result = resourceManager.GetString(resourceName, culture);
            }

            if (args.Length > 0 && result != null)
            {
                result = string.Format(culture, result, args);
            }

            return result;
        }
    }
}
