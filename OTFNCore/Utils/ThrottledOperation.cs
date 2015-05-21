using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTFN.Core.Utils
{
    public class ThrottledOperation
    {
        private DateTime nextUpdateTime;
        private int timeout;
        private Func<Task> operation;
        private bool isRunning;

        public ThrottledOperation(int timeoutMs, Func<Task> operation)
        {
            timeout = timeoutMs;
            this.operation = operation;
        }

        public async Task Run()
        {
            lock (this)
            {
                if (isRunning ||
                    nextUpdateTime != null && nextUpdateTime > DateTime.Now)
                {
                    return;
                }
                nextUpdateTime = DateTime.Now.AddMilliseconds(timeout);
                isRunning = true;
            }

            await operation.Invoke();
            isRunning = false;
        }
    }
}
