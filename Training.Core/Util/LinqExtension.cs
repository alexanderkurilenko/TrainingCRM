using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;

namespace Training.Core.Util
{

    public static class LinqExtension
    {
        public static void ForEach<T>(this IEnumerable<T> @this, Action<T> action)
        {
            foreach (var element in @this)
            {
                var localElement = element; // local variable is used to avoid potential problem with loop variable inside closure
                action(localElement);
            }
        }

        public static void IfNotNull<T>(this T @this, Action<T> action)
            where T : class
        {
            if (@this != null)
            {
                action(@this);
            }
        }

        public static U IfNotNull<T, U>(this T @this, Func<T, U> func)
            where T : class
            where U : class
        {
            if (@this != null)
            {
                return func(@this);
            }

            return null;
        }

        public static U IfNull<T, U>(this T @this, Func<U> func)
            where T : class
            where U : class
        {
            if (@this == null)
            {
                return func();
            }

            return null;
        }

        public static U IfNotNull<T, U>(this T @this, Func<T, U> ifFunc, Action elseFunc)
            where T : class
            where U : class
        {
            if (@this != null)
            {
                return ifFunc(@this);
            }

            elseFunc();
            return null;
        }

        public static EntityReference ToEntityReference<T>(this T @this)
            where T : Entity
        {
            EntityReference returnEntityReference = null;

            if (@this.Id != Guid.Empty)
            {
                returnEntityReference = new EntityReference(@this.LogicalName, @this.Id);
            }

            return returnEntityReference;
        }

        public static U IfTrue<U>(this bool @this, Func<U> func)
            where U : class
        {
            U value = null;

            if (@this)
            {
                value = func();
            }

            return value;
        }

        public static U IfTrue<U>(this bool @this, Func<U> func, Func<U> elseFunc)
           where U : class
        {
            return @this ? func() : elseFunc();
        }

        public static T? IfTrue<T>(this bool @this, Func<T?> func)
            where T : struct
        {
            T? value = null;

            if (@this)
            {
                value = func();
            }

            return value;
        }

        public static T GetSingleOrDefault<T>(this IQueryable<T> @this, Dictionary<string, string> parameters) where T : Entity
        {
            var result = @this.ToList();

            if (result.Count == 0 || result.Count == 1)
            {
                return result.SingleOrDefault();
            }

            if (result.Count > 1)
            {
                throw new Exception("DuplicateError");
            }

            return null;
        }
    }
}
