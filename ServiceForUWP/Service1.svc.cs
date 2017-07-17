using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using CodeFirst;
using CodeFirst.Entities;

namespace ServiceForUWP
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "Service1" в коде, SVC-файле и файле конфигурации.
    // ПРИМЕЧАНИЕ. Чтобы запустить клиент проверки WCF для тестирования службы, выберите элементы Service1.svc или Service1.svc.cs в обозревателе решений и начните отладку.
    public class Service1 : IService1
    {
        private string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=testdbforuwp;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public List<HistoryRow> GetHistoryRowsJson()
        {

            DB.ConnectionString = ConnectionString;
            if (!DB.DatabaseExists(ConnectionString))
                DB.CreateDatabase();

            return GetSchedule();
        }

        //private List<Timetable> GetSchedule()
        //{
        //    List<Timetable> Schedule = new List<Timetable>
        //  {
        //    new Timetable
        //    {
        //        id=1, arrivaltime=DateTime.Parse("12:05:00"), busnumber=5, busstation ="Березка"
        //    },
        //    new Timetable
        //    {
        //        id=2, arrivaltime =DateTime.Parse("12:10:00"), busnumber=5, busstation ="Детский мир"
        //    }
        //  };
        //    return Schedule;
        //}

        private List<HistoryRow> GetSchedule()
        {
            using (DataSet ds = new DataSet())
            {
                using (SqlConnection sqlCon = new SqlConnection(ConnectionString))
                {
                    try
                    {
                        sqlCon.Open();
                        string sqlStr = "select * from HistoryRows";
                        using (SqlDataAdapter sqlDa = new SqlDataAdapter(sqlStr, sqlCon))
                        {
                            sqlDa.Fill(ds);
                        }
                    }
                    catch
                    {
                        return null;
                    }
                    finally
                    {
                        sqlCon.Close();
                    }
                }

                List<HistoryRow> historyRowsList = new List<HistoryRow>();

                using (DataTable dt = ds.Tables[0])
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        historyRowsList.Add(new HistoryRow()
                        {
                            //id = Convert.ToInt16((dr["Id"])),
                            //arrivaltime = DateTime.Parse(dr["arrivaltime"].ToString()),
                            //busnumber = Convert.ToInt16((dr["busnumber"] ?? 0)),
                            //busstation = dr["busstation"].ToString()
                            HistoryRowId = Guid.Parse(dr["HistoryRowId"].ToString()),
                            Cps = Convert.ToDouble(dr["Cps"]),
                            De = Convert.ToDouble(dr["De"]),
                            Der = Convert.ToDouble(dr["Der"]),
                            Time = DateTime.Parse(dr["Time"].ToString()),
                            Type = HistoryType.Alaram
                        });
                    }
                }
                return historyRowsList;
            }
        }
    }
}
