namespace Nca.Valdr
{
    using System;

    /// <summary>
    /// Description of attribute used to identify classes needing valdr validation constraints generated.  NOTE: attributes MUST use named arguments to be picked up correctly by parser
    /// </summary>
    public class ValdrTypeAttributeDescriptor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValdrTypeAttributeDescriptor"/> class.
        /// </summary>
        /// <param name="type">Type to use to mark objects needing valdr constraints generated</param>
        /// <param name="nameProperty">Name of property exposed by <paramref name="type"/> used to drive naming in constraint generation</param>
        /// <exception cref="ArgumentNullException">Type is null.</exception>
        public ValdrTypeAttributeDescriptor(Type type, string nameProperty)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            TypeName = type.Name;
            ValdrTypePropertyName = nameProperty;
        }

        /// <summary>
        /// Name of the attribute type
        /// </summary>
        public string TypeName { get; private set; }

        /// <summary>
        /// Name of property exposed by the attribute type that should drive naming in constraint generation
        /// </summary>
        public string ValdrTypePropertyName { get; private set; }
    }
}
