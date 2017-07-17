using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeFirst.Interfaces;

namespace CodeFirst.Entities
{
    public class HistoryRow:IHistoryRow
    {
        #region Constructors

        #endregion

        #region IHistoryRow Members
        public Guid HistoryRowId { get; set; }
        public DateTime Time { get; set; }
        public HistoryType Type { get; set; }
        public double Cps { get; set; }
        public double Der { get; set; }
        public double De { get; set; }

        #endregion
    }

    public enum HistoryType
    {
        /// <summary>
        /// Прибор выключен
        /// </summary>
        DeviceOff = '0',
        /// <summary>
        /// Прибор включен
        /// </summary>
        DeviceOn = '1',
        /// <summary>
        /// Фон по времени
        /// </summary>
        Background = 'F',
        Alaram = 'A',
        ChangedNCoefficent = 'S',
        ChangedSinSignaling = 's',
        DerThreshold1Alarm = 'e',
        DerThreshold2Alarm = 'E',
        DeThresholdAlarm = 'D',
        /// <summary>
        /// Калибровка прибора
        /// </summary>
        Calibration = 'C'
    }

}
