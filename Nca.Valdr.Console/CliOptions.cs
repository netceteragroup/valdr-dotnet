namespace Nca.Valdr.Console
{
    using System;

    /// <summary>
    /// Command line parameters
    /// </summary>
    public class CliOptions
    {
        public CliOptions(string[] args)
        {
            if (args == null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            foreach (var arg in args)
            {
                switch (arg.Substring(0, 2))
                {
                    case "-i":
                        AssemblyFileName = arg.Substring(3);
                        break;
                    case "-n":
                        TargetNamespace = arg.Substring(3);
                        break;
                    case "-o":
                        OutputFileName = arg.Substring(3);
                        break;
                    case "-a":
                        Application = arg.Substring(3);
                        break;
                    case "-c":
                        Culture = arg.Substring(3);
                        break;
                    default:
                        throw new ArgumentException("Invalid parameter.");
                }
            }

            if (string.IsNullOrEmpty(AssemblyFileName) ||
                string.IsNullOrEmpty(OutputFileName))
            {
                throw new ArgumentException("Required parameter missing.");
            }
        }

        /// <summary>
        /// Input assembly path
        /// </summary>
        public string AssemblyFileName { get; }

        /// <summary>
        /// Target namespace filter (default: all)
        /// </summary>
        public string TargetNamespace { get; } = string.Empty;

        /// <summary>
        /// Output file path
        /// </summary>
        public string OutputFileName { get; }

        /// <summary>
        /// AngularJS application name (default: app)
        /// </summary>
        public string Application { get; } = "app";

        /// <summary>
        /// Culture (default: en-US)
        /// </summary>
        public string Culture { get; }
    }
}
