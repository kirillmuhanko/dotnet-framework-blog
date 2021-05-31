using System;
using BlogApplication.Models.Enums;

namespace BlogApplication.Models.Attributes.DependencyInjection
{
    [AttributeUsage(AttributeTargets.Class)]
    public class InstanceScopeAttribute : Attribute
    {
        public InstanceScopeAttribute(InstanceLifetime lifetime)
        {
            Lifetime = lifetime;
        }

        public InstanceLifetime Lifetime { get; }
    }
}