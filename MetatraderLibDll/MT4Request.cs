using OTFN.Core.Strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MetatraderLibDll
{
    class MT4Request
    {
        public const int Ping = 0;
        public const int GetOrders = 1;
        public const int GetAccountInfo = 2;
        //public const int  = ;

        private static int nextId = 1;

        public int Id;

        public int Command;
        public double[] Doubles;
        public string[] Strings;

        private TaskCompletionSource<MT4Response> tcs = new TaskCompletionSource<MT4Response>();

        public Task<MT4Response> WaitTask
        {
            get { return tcs.Task; }
        }

        public void SetResponse(MT4Response resp)
        {
            tcs.SetResult(resp);
        }

        public MT4Request(int command, double[] doubles, string[] strings)
        {
            Command = command;
            Doubles = doubles;
            Strings = strings;

            Id = Interlocked.Increment(ref nextId);
        }

        public MT4Request(int command, double[] doubles)
        {
            Command = command;
            Doubles = doubles;
            Strings = null;

            Id = Interlocked.Increment(ref nextId);
        }

        public MT4Request(int command, string[] strings)
        {
            Command = command;
            Doubles = null;
            Strings = strings;

            Id = Interlocked.Increment(ref nextId);
        }

        public MT4Request(int command)
        {
            Command = command;
            Doubles = null;
            Strings = null;

            Id = Interlocked.Increment(ref nextId);
        }


    }
}
