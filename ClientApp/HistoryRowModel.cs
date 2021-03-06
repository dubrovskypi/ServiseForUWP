﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp
{
    public class HistoryRowModel
    {
        public Guid Id { get; set; }
        public DateTime EventTime { get; set; }
        public HistoryType Type { get; set; }
        public double Cps { get; set; }
        public double Der { get; set; }
        public double De { get; set; }
        public bool IsSynchronized { get; set; }
        public string DeviceSerialNumber { get; set; }
        public string ReaderSerialNumber { get; set; }
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
