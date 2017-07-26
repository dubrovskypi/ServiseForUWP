using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp
{
    public class CloudHistoryRowModel
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime Time { get; set; }

        public HistoryType Type { get; set; }

        public double Cps { get; set; }
        public double Der { get; set; }
        public double De { get; set; }
        public string DeviceSerialNumber { get; set; }
        public string ReaderSerialNumber { get; set; }
    }

}
