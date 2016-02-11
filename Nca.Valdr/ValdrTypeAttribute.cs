namespace Nca.Valdr
{
    using System;

    /// <summary>
    /// Attribute provided for marking classes needing valdr constraints
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ValdrType : Attribute
    {
        /// <summary>
        /// Name used for valdr constraint
        /// </summary>
        public string Name { get; set; }
    }
}
