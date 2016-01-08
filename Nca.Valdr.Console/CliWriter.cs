namespace Nca.Valdr.Console
{
    using System;
    using System.IO;
    using System.Text;

    /// <summary>
    /// Command line inteface writer.
    /// </summary>
    public class CliWriter : TextWriter
    {
        private readonly TextWriter _writer = Console.Out;

        public override Encoding Encoding => new UTF8Encoding();

        public override void Write(string message)
        {
            _writer.Write(message);
        }
        public override void WriteLine(string message)
        {
            _writer.WriteLine(message);
        }
    }
}
