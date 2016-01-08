namespace Nca.Valdr.Console
{
    using System;

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
            }
            catch (Exception ex)
            {
                OutWriter.WriteLine(ex.Message);
                return;
            }

            new CliRunner(_options, OutWriter).Execute();
        }
    }
}
