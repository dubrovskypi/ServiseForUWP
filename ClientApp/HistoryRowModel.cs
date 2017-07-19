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
        //[DataMember]
        //public DateTime Time { get; set; }

        //todo решить косяк с преобразованием в json (или обратно)
        //[DataMember]
        //public HistoryType Type { get; set; }

        public double Cps { get; set; }
        public double Der { get; set; }
        public double De { get; set; }
    }
}
