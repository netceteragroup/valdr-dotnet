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
                        AssemblyFilename = arg.Substring(3);
                        break;
                    case "-n":
                        TargetNamespace = arg.Substring(3);
                        break;
                    case "-o":
                        OutputFilename = arg.Substring(3);
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

            if (string.IsNullOrEmpty(AssemblyFilename) ||
                string.IsNullOrEmpty(OutputFilename))
            {
                throw new ArgumentException("Required parameter missing.");
            }
        }

        /// <summary>
        /// Input assembly path
        /// </summary>
        public string AssemblyFilename { get; }

        /// <summary>
        /// Target namespace filter (default: all)
        /// </summary>
        public string TargetNamespace { get; } = string.Empty;

        /// <summary>
        /// Output file path
        /// </summary>
        public string OutputFilename { get; }

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
