using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blitzkrieg.DataBase
{
    public class uTorrentObject
    {
        public string TorAddress { get; set; }
        public string TorPort { get; set; }
        public string TorUser { get; set; }
        public string TorPass { get; set; }
        public bool TorChkForceDown { get; set; }
        public bool TorChkStop100 { get; set; }
        public string TorUpSeconds { get; set; }
        public string TorRefresh { get; set; }
        public string uTorFullUrl { get; set; }

    }
}
