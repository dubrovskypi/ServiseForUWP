using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeFirst.Interfaces;
using System.Runtime.Serialization;

namespace CodeFirst.Entities
{
    [DataContract]
    public class HistoryRow//:IHistoryRow
    {
        #region Constructors

        #endregion

        #region IHistoryRow Members
        [DataMember]
        public Guid HistoryRowId { get; set; }
        //[DataMember]
        //public DateTime Time { get; set; }

        //todo решить косяк с преобразованием в json (или обратно)
        //[DataMember]
        //public HistoryType Type { get; set; }

        [DataMember]
        public double Cps { get; set; }
        [DataMember]
        public double Der { get; set; }
        [DataMember]
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
