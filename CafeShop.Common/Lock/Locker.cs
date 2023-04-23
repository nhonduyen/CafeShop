using System.Collections.Concurrent;

namespace CafeShop.Common.Lock {
    public class Locker : IDisposable {
        private static readonly ConcurrentDictionary<string, Timer> lockers = new();

        private readonly string lockKey;
        private readonly TimeSpan ttl;

        public Locker(string key, int ttlInSec = 60) {
            this.lockKey = $"Lock:{key}";
            this.ttl = TimeSpan.FromSeconds(ttlInSec);
        }

        public async Task<Locker> Lock() {
            bool locked;
            do {
                locked = lockers.ContainsKey(lockKey);
                if (locked) await Task.Delay(50);
                else lockers.TryAdd(lockKey, new Timer(async _ => await Unlock(), null, ttl, Timeout.InfiniteTimeSpan));
            } while (locked);
            return this;
        }

        public async Task Unlock() {
            if (lockers.TryRemove(lockKey, out Timer? timer) && timer != null) {
                await timer.DisposeAsync();
            }
        }

        private bool disposedValue;

        protected virtual void Dispose(bool disposing) {
            if (!disposedValue) {
                if (disposing) {
                    Unlock().GetAwaiter().GetResult();
                }
                disposedValue = true;
            }
        }

        public void Dispose() {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}