using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTFN.Core.Market
{
    public class Timeframe
    {
        private int minutes;
        public int Minutes
        {
            get
            {
                return minutes;
            }
        }

        private Timeframe(int minutes)
        {
            this.minutes = minutes;
            timeframeByMinutes.Add(minutes, this);
        }

        private static Dictionary<int, Timeframe> timeframeByMinutes = new Dictionary<int, Timeframe>();

        public static Timeframe GetFromMinutes(int minutes)
        {
            Timeframe tf = null;
            if (!timeframeByMinutes.TryGetValue(minutes, out tf))
            {
                tf = new Timeframe(minutes);
            }

            return tf;
        }

        public static Timeframe M1 = new Timeframe(1);
        public static Timeframe M5 = new Timeframe(5);
        public static Timeframe M15 = new Timeframe(15);
        public static Timeframe M30 = new Timeframe(30);
        public static Timeframe H1 = new Timeframe(60);
        public static Timeframe H4 = new Timeframe(INMUNITES_H4);
        public static Timeframe D1 = new Timeframe(INMUNITES_D1);
        public static Timeframe W1 = new Timeframe(INMUNITES_W1);
        public static Timeframe MN1 = new Timeframe(INMUNITES_MN1);

        public const int INMUNITES_H4 = 240;
        public const int INMUNITES_D1 = 1440;
        public const int INMUNITES_W1 = 10080;
        public const int INMUNITES_MN1 = 43200;
    }
}
