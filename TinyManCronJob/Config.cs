using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyManCronJob
{
    public class Config
    {
        public string MyAccountMnemonic { get; set; }
        public int? ToSell { get; set; }
        public int? ToTradeQFrom { get; set; }
        public int? ToTradeQTo { get; set; }
        public ulong? ToTradeQMultiplier { get; set; }
        public string ApiHost { get; set; }
        public string ApiKey { get; set; }
        public int DelayFrom { get; set; }
        public int DelayTo { get; set; }
        public int DelayAfter { get; set; }
        public ulong? BaseAsset { get; set; }
        public List<ulong> OtherAssets { get; set; } = new List<ulong>();
        public int TradeType { get; set; } = 1;// sell = 1, buy = 2
    }
}
