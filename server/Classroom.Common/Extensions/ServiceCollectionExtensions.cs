using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Classroom.Common.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddAutoWired(this IServiceCollection services, Assembly assembly)
        {
            var autowiredTypes = assembly
                .GetTypes().Where(t => Attribute.GetCustomAttribute(t, typeof(AutowiredAttribute)) != null)
                .ToList();

            autowiredTypes.ForEach(t => services.AutoWire(t));
        }

        private static void AutoWire(this IServiceCollection services, Type type)
        {
            if (!(Attribute.GetCustomAttribute(type, typeof(AutowiredAttribute)) is AutowiredAttribute attribute)) return;

            var scope = attribute.Scope;
            var interfaceType = type.GetInterfaces().FirstOrDefault();

            AutoWireActionMap[scope](services, interfaceType, type);
        }

        private static readonly Dictionary<AutowiredScope, Action<IServiceCollection, Type, Type>> AutoWireActionMap
            = new Dictionary<AutowiredScope, Action<IServiceCollection, Type, Type>>()
        {
            {  AutowiredScope.Scoped, (IServiceCollection services, Type intf, Type impl) => services.AddScoped(intf, impl) },
            {  AutowiredScope.Singleton, (IServiceCollection services, Type intf, Type impl) => services.AddSingleton(intf, impl) },
            {  AutowiredScope.Transient, (IServiceCollection services, Type intf, Type impl) => services.AddTransient(intf, impl) }
        };
    }
}
