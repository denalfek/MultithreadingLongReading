using System.Threading;

namespace MultiThreadingLongReading.Services
{
    public static class ReaderService
    {
        private static string _file;
        private static readonly object _locker = new object();
        private static readonly Semaphore _semaphore;

        public static string TryingSemaphore()
        {
            //_semaphore.

            return $"Hello from thread {Thread.CurrentThread.ManagedThreadId}";
        }

        public static string ReadFile()
        {
            Thread.Sleep(20000);
            return $"Hello from thread {Thread.CurrentThread.ManagedThreadId}";
        }

        public static string ReadFileWithLock()
        {
            // Вернуть в оба потока надо, посмотреть про именованный семафор, zookeeper, webdav
            if (Monitor.TryEnter(_locker))
            {
                try
                {
                    _file = $"Hello from thread {Thread.CurrentThread.ManagedThreadId}";
                    Thread.Sleep(20000);
                    return _file;
                }
                finally
                {
                    Monitor.Exit(_locker);
                }
            }
            else
            {
                Monitor.Wait(_locker); // todo: async wait
                return _file;
            }
        }

        public static string MonitorTest()
        {
            object locker = new object();
            bool acquiredLock = false;
            try
            {
                Monitor.Enter(locker, ref acquiredLock);
                Thread.Sleep(20000);
                return $"Hello from thread {Thread.CurrentThread.ManagedThreadId}";
            }
            finally
            {
                if (acquiredLock)
                {
                    Monitor.Exit(locker);
                }
            }
        }
    }
}
