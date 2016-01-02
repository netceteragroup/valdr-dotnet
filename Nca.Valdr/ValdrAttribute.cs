using System;

namespace Nca.Valdr
{
    /// <summary>
    /// Valdr attribute
    /// </summary>
    public class ValdrAttribute
    {
        public string Name { get; set; }

        public string Message { get; set; }

        public Type ResourceType { get; set; }

        public string ResourceName { get; set; }

        public int Minimum { get; set; }

        public int Maximum { get; set; }

        public string Pattern { get; set; }
    }
}
