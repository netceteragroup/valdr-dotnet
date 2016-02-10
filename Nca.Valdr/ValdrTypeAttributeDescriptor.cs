namespace Nca.Valdr
{
    using System;

    /// <summary>
    /// Description of attribute used to identify classes needing valdr validation constraints generated
    /// </summary>
    public class ValdrTypeAttributeDescriptor 
    {
        /// <summary>
        /// Name of the attribute type
        /// </summary>
        public string TypeName { get; private set; }

        /// <summary>
        /// Name of property exposed by the attribute type that should drive naming in constraint generation
        /// </summary>
        public string ValdrTypePropertyName { get; private set; }

        /// <summary>
        /// ValdrTypeAttributeDescriptor constructor
        /// </summary>
        /// <param name="type">Type to use to mark objects needing valdr constraints generated</param>
        /// <param name="nameProperty">Name of property exposed by <paramref name="type"/> used to drive naming in constraint generation</param>
        public ValdrTypeAttributeDescriptor(Type type, string nameProperty)
        {
            TypeName = type.Name;
            ValdrTypePropertyName = nameProperty;
        }
    }
}
