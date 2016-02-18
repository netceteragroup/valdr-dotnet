namespace Nca.Valdr
{
    using System;

    /// <summary>
    /// Valdr attribute.
    /// </summary>
    internal class ValdrAttribute
    {
        /// <summary>
        /// Attribute name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Error message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Resource type.
        /// </summary>
        public Type ResourceType { get; set; }

        /// <summary>
        /// Resource name.
        /// </summary>
        public string ResourceName { get; set; }

        /// <summary>
        /// Minimum value.
        /// </summary>
        public int Minimum { get; set; }

        /// <summary>
        /// Maximum value.
        /// </summary>
        public int Maximum { get; set; }

        /// <summary>
        /// Regex pattern.
        /// </summary>
        public string Pattern { get; set; }
    }
}
