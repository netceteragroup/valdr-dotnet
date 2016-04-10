namespace Nca.Valdr
{
    using System;

    /// <summary>
    /// Attribute provided for marking properties as valdr members
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class ValdrMemberAttribute : Attribute
    {
        /// <summary>
        /// Name used for member in constraints
        /// </summary>
        public string Name { get; set; }
    }
}
