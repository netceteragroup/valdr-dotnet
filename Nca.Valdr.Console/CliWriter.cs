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

        public override void Write(string value)
        {
            _writer.Write(value);
        }

        public override void WriteLine(string value)
        {
            _writer.WriteLine(value);
        }
    }
}
