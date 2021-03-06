﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeFirst.Interfaces;
using System.Runtime.Serialization;

namespace CodeFirst.Entities
{
    [DataContract]
    public class HistoryRow:IHistoryRow
    {
        #region Constructors

        #endregion

        #region IHistoryRow Members
        //[DataMember]
        //public Guid Id { get; set; }
        //[DataMember]
        //public DateTime Time { get; set; }
        [DataMember]
        public DateTime EventTime { get; set; }
        [DataMember]
        public HistoryType Type { get; set; }
        [DataMember]
        public double Cps { get; set; }
        [DataMember]
        public double Der { get; set; }
        [DataMember]
        public double De { get; set; }
        #endregion
        [DataMember]
        public bool IsSynchronized { get; set; }
        [DataMember]
        public string DeviceSerialNumber { get; set; }
        [DataMember]
        public string ReaderSerialNumber { get; set; }

    }
    [DataContract]
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
