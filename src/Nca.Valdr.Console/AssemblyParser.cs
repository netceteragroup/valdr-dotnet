namespace Nca.Valdr.Console
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Reflection;
    using System.Runtime.Serialization;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Valdr metadata generator for console application - handles assembly loading and delegates to parser
    /// </summary>
    public class AssemblyParser
    {
        private readonly string _assemblyFile;
        private readonly string _targetNamespace;
        private readonly CultureInfo _culture;
        private readonly Parser _parser;
        private Assembly _assembly;

        /// <summary>
        /// Initializes a new instance of the <see cref="AssemblyParser"/> class.
        /// </summary>
        /// <param name="assemblyFile">Input assembly path.</param>
        /// <param name="targetNamespace">Target namespace for parsing.</param>
        /// <param name="culture">The culture.</param>
        public AssemblyParser(string assemblyFile, string targetNamespace, string culture)
        {
            if (string.IsNullOrEmpty(assemblyFile))
            {
                throw new ArgumentException("Parameter \"assemblyFile\" is null or empty.");
            }

            _assemblyFile = assemblyFile.StartsWith("file:///", StringComparison.OrdinalIgnoreCase) ? assemblyFile.Substring(8) : assemblyFile;
            if (!File.Exists(_assemblyFile))
            {
                throw new ArgumentException($"Specified \"assemblyFile\" not found: {_assemblyFile}");
            }

            _targetNamespace = targetNamespace ?? string.Empty;
            _culture = string.IsNullOrEmpty(culture) ? null : CultureInfo.GetCultureInfo(culture);

            _parser = new Parser();
        }

        /// <summary>
        /// Loads an assembly and parses classes with a "DataContractAttribute" to generate the valdr constraint metadata.
        /// </summary>
        /// <returns>JSON metadata object.</returns>
        public JObject Parse()
        {
            var domain = AppDomain.CurrentDomain;

            try
            {
                if (_culture == null)
                {
                    domain.ReflectionOnlyAssemblyResolve += CustomAssemblyResolver;
                    _assembly = Assembly.ReflectionOnlyLoadFrom(_assemblyFile);
                }
                else
                {
                    // Load with satellite assemblies for text resources
                    domain.AssemblyResolve += CustomAssemblyResolver;
                    _assembly = Assembly.LoadFrom(_assemblyFile);
                }

                return _parser.Parse(_culture, _targetNamespace, new ValdrTypeAttributeDescriptor(typeof(DataContractAttribute), nameof(DataContractAttribute.Name)), nameof(DataMemberAttribute), _assembly);
            }
            finally
            {
                if (_culture == null)
                {
                    domain.ReflectionOnlyAssemblyResolve -= CustomAssemblyResolver;
                }
                else
                {
                    domain.AssemblyResolve -= CustomAssemblyResolver;
                }
            }
        }

        private Assembly CustomAssemblyResolver(object sender, ResolveEventArgs args)
        {
            var name = new AssemblyName(args.Name);
            var assemblyPath = Path.Combine(
                Path.GetDirectoryName(_assemblyFile) ?? string.Empty,
                name.Name + ".dll");

            if (File.Exists(assemblyPath))
            {
                return _culture == null
                    ? Assembly.ReflectionOnlyLoadFrom(assemblyPath)
                    : Assembly.LoadFrom(assemblyPath);
            }

            // Load from GAC
            return _culture == null
                ? Assembly.ReflectionOnlyLoad(args.Name)
                : Assembly.Load(args.Name);
        }
    }
}
