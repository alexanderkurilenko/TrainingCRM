using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Importer.Infrastructure
{
    public class RecordLockDictionary
    {
        protected readonly ConcurrentDictionary<string, LockWithRefCounter> dictionary
            = new ConcurrentDictionary<string, LockWithRefCounter>();

        private const int refCounterStartValue = 1;
        private const int refCounterRemoveValue = 0;

        public virtual object GetLock(string key)
        {
            dictionary.AddOrUpdate(key, new LockWithRefCounter { LockObject = new object(), RefCounter = refCounterStartValue },
                (@string, @lock) => new LockWithRefCounter { LockObject = @lock.LockObject, RefCounter = @lock.RefCounter + 1 });

            return GetLockObject(key);
        }

        public virtual void ReleaseLock(string key)
        {
            var lockObject = GetLockObject(key);

            dictionary.AddOrUpdate(key, new LockWithRefCounter { LockObject = new object(), RefCounter = refCounterStartValue },
                (@string, @lock) => new LockWithRefCounter { LockObject = @lock.LockObject, RefCounter = @lock.RefCounter - 1 });

            ((ICollection<KeyValuePair<string, LockWithRefCounter>>)dictionary).Remove(
                new KeyValuePair<string, LockWithRefCounter>(key,
                    new LockWithRefCounter { LockObject = lockObject, RefCounter = refCounterRemoveValue }));
        }

        private object GetLockObject(string key)
        {
            LockWithRefCounter lockWithRefCounter;

            if (!dictionary.TryGetValue(key, out lockWithRefCounter))
            {
                throw new Exception(string.Format("Lock object with \"{0}\" key wasn't found.", key));
            }

            return lockWithRefCounter.LockObject;
        }
    }
}
