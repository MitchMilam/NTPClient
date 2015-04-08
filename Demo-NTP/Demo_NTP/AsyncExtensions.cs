using System;
using System.Threading;
using System.Threading.Tasks;

namespace Demo_NTP
{
    public static class AsyncExtensions
    {
        public static async Task<T> TimeoutAfter<T>(this Task<T> task, TimeSpan timeout, CancellationTokenSource cancellationTokenSource = null)
        {
            if (task == await Task.WhenAny(task, Task.Delay(timeout)))
            {
                return await task;
            }

            if (cancellationTokenSource != null)
            {
                cancellationTokenSource.Cancel();
            }

            throw new TimeoutException();
        }
    }
}
