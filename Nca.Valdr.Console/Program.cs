namespace Nca.Valdr.Console
{
    using System;
    using System.Reflection;

    /// <summary>
    /// This class provides the entry point for the console runner.
    /// </summary>
    public static class Program
    {
        private static readonly CliWriter OutWriter = new CliWriter();
        private static CliOptions _options;

        /// <summary>
        /// The main entry point.
        /// </summary>
        /// <param name="args">CLI parameters.</param>
        public static void Main(string[] args)
        {
            try
            {
                _options = new CliOptions(args);

                OutWriter.WriteLine($"Nca.Valdr -> { _options.OutputFilename} {_options.Culture}");

                new CliRunner(_options).Execute();
            }
            catch (ReflectionTypeLoadException ex)
            {
                foreach (var item in ex.LoaderExceptions)
                {
                    OutWriter.WriteLine(item.Message);
                }
            }
            catch (Exception ex)
            {
                OutWriter.WriteLine(ex.Message);
            }
        }
    }
}
