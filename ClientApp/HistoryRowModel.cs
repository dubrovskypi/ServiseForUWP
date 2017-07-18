using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp
{
    public class HistoryRowModel
    {
        public Guid HistoryRowId { get; set; }
        public DateTime Time { get; set; }
        
        public string Type { get; set; }

        public string Cps { get; set; }
        public string Der { get; set; }
        public string De { get; set; }
    }
}
