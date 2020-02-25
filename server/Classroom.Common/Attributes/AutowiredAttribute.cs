using System;

namespace Classroom.Common
{
    public enum AutowiredScope
    {
        Singleton,
        Scoped,
        Transient
    };

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class AutowiredAttribute : Attribute
    {
        public AutowiredAttribute(AutowiredScope scope = AutowiredScope.Scoped)
        {
            Scope = scope;
        }

        public AutowiredScope Scope { get; }
    }
}
