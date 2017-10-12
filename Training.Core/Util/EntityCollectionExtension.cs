using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;

namespace Training.Core.Util
{
    public static class EntityCollectionExtension
    {
        public static IEnumerable<T> GetUniqueRecordsById<T>(this IEnumerable<T> @this)
            where T : Entity
        {
            var uniqueStore = new HashSet<Guid>();
            var result = new List<T>();
            if (@this == null)
            {
                return result;
            }

            foreach (var entity in @this)
            {
                if (uniqueStore.Contains(entity.Id))
                {
                    continue;
                }

                uniqueStore.Add(entity.Id);
                result.Add(entity);
            }

            return result;
        }
    }
}
