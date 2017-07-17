using System;
using CodeFirst.Entities;

namespace CodeFirst.Interfaces
{
    public interface IHistoryRow
    {
        DateTime Time { get; set; }
        HistoryType Type { get; set; }
        double Cps { get; set; }
        double Der { get; set; }
        double De { get; set; }
    }
}