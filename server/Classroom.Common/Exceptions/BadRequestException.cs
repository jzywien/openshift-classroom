using System;
using System.Runtime.Serialization;

namespace Classroom.Common.Exceptions
{
    [Serializable]
    public class BadRequestException : Exception
    {
        protected BadRequestException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
        }
    }
}
