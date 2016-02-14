namespace Nca.Valdr
{
    using System;

    /// <summary>
    /// Attribute provided for marking properties as valdr members
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ValdrMember : Attribute
    {
        /// <summary>
        /// Name used for member in constraints
        /// </summary>
        public string Name { get; set; }
    }
}
