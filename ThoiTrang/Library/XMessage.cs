using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThoiTrang
{
    public class XMessage
    {
        public string type { get; set; }
        public string msg { get; set; }
        public XMessage() { }
        public XMessage(string type, string msg)
        {
            this.type = type;
            this.msg = msg;
        }
    }
}