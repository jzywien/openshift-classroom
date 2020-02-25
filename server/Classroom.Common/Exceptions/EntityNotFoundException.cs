using System;
using System.Runtime.Serialization;

namespace Classroom.Common.Exceptions
{
    [Serializable]
    public class EntityNotFoundException : Exception
    {
        private readonly Type type;
        private readonly int id;
        
        protected EntityNotFoundException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
        }

        public EntityNotFoundException(Type type, int id)
        {
            this.type = type;
            this.id = id;
        }

        public override string Message => $"Could not find Entity of type {type.Name} with Id {id}";
    }
}
