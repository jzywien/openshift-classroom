using System;
using System.Linq.Expressions;
using AutoMapper;

namespace Classroom.Common.Extensions
{
    public static class AutoMapperExtensions
    {
        public static IMappingExpression<S, D> MapMember<S, D, TTo, TMember>(this IMappingExpression<S, D> map,
            Expression<Func<D, TTo>> to,
            Expression<Func<S, TMember>> from,
            Action<IMemberConfigurationExpression<S, D, TTo>> opts = null)
        {
            map.ForMember(to, o =>
            {
                o.MapFrom(from);
                opts?.Invoke(o);
            });

            return map;
        }

        public static IMappingExpression<S, D> MapPath<S, D, TTo, TMember>(this IMappingExpression<S, D> map,
            Expression<Func<D, TTo>> to,
            Expression<Func<S, TMember>> from,
            Action<IPathConfigurationExpression<S, D, TTo>> opts = null)
        {
            map.ForPath(to, o =>
            {
                o.MapFrom(from);
                opts?.Invoke(o);
            });

            return map;
        }
    }
}
