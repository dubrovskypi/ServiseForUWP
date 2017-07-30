using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Transactions;
using System.Text;
using CodeFirst;
using CodeFirst.Entities;
using CodeFirst.Interfaces;

namespace ServiceForUWP
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "Service1" в коде, SVC-файле и файле конфигурации.
    // ПРИМЕЧАНИЕ. Чтобы запустить клиент проверки WCF для тестирования службы, выберите элементы Service1.svc или Service1.svc.cs в обозревателе решений и начните отладку.
    public class DbServiceForUwp : IDbServiceForUwp, IDisposable
    {
        //private string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=testdbforuwp;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private string ConnectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=testdbforuwp;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private IRepository<HistoryRow> historyRepository;

        public DbServiceForUwp()
        {

            DB.ConnectionString = ConnectionString;
            if (!DB.DatabaseExists(ConnectionString))
                DB.CreateDatabase();

            historyRepository = DB.CreateHistoryRepository();
        }
        #region IDbServiceForUwp_Implementation

        public List<HistoryRow> GetHistoryRowsJson()
        {
            return GetHistory();
        }

        public void AddHistoryRow(HistoryRow newRow)
        {
            WriteHistoryRow(newRow);
        }

        public void AddHistory(List<HistoryRow> newHistory)
        {
            WriteHistory(newHistory);
        }

        public void SetConnection(ConnectionProperty connection)
        {
            //TODO убрать заглушку
            var c = connection;
        }

        public void WriteToCloud()
        {
            var localHistory = historyRepository.GetItems();
            var unsyncLocalHistory = localHistory.Where(r => !r.IsSynchronized);
            try
            {
                var cloudConStr = @"Data Source=.\SQLEXPRESS;Initial Catalog=RemotedDB;Integrated Security=True;TrustServerCertificate=True;";
                using (var cloudRep = DB.CreateHistoryRepository(cloudConStr))
                {
                    using (var scope = new TransactionScope())
                    {
                        foreach (var row in unsyncLocalHistory)
                        {
                            cloudRep.Create(row);
                            row.IsSynchronized = true;
                        }
                        historyRepository.Save();
                        cloudRep.Save();
                    scope.Complete();
                }
            }
            }
            catch (Exception e)
            {
                throw new Exception("ошибка при переносе истори в удаленную бд: " + e.Message + "\n" + e.StackTrace);
            }
        }

        public void ClearHistory()
        {
            ClearLocalHistory();
        }


        public void FillTestRows()
        {
            //Todo пока что сделаю заполнение локальной бд записями для теста
            var testHistory = new List<HistoryRow>();
            var random = new Random();
            for (int i = 0; i < 50; i++)
            {
                testHistory.Add(new HistoryRow()
                {
                    EventTime = DateTime.Now,
                    Cps = random.NextDouble(),
                    De = random.NextDouble(),
                    Der = random.NextDouble(),
                    DeviceSerialNumber = Guid.NewGuid().ToString(),
                    ReaderSerialNumber = "Serial",
                    IsSynchronized = false,
                    Type = HistoryType.Alaram
                });
            }
            WriteHistory(testHistory);
        }
        #endregion

        #region PrivateMetods

        private List<HistoryRow> GetHistory()
        {
            try
            {
                List<HistoryRow> historyRowsList = null;
                if (historyRepository == null) return historyRowsList;
                historyRowsList = historyRepository.GetItems().ToList();
                return historyRowsList;
            }
            catch (Exception e)
            {
                throw new FaultException(e.Message);
            }
            //using (DataSet ds = new DataSet())
            //{
            //    using (SqlConnection sqlCon = new SqlConnection(ConnectionString))
            //    {
            //        try
            //        {
            //            sqlCon.Open();
            //            string sqlStr = "select * from HistoryRows";
            //            using (SqlDataAdapter sqlDa = new SqlDataAdapter(sqlStr, sqlCon))
            //            {
            //                sqlDa.Fill(ds);
            //            }
            //        }
            //        catch
            //        {
            //            return null;
            //        }
            //        finally
            //        {
            //            sqlCon.Close();
            //        }
            //    }

            //    List<HistoryRow> historyRowsList = new List<HistoryRow>();

            //    using (DataTable dt = ds.Tables[0])
            //    {
            //        foreach (DataRow dr in dt.Rows)
            //        {
            //            historyRowsList.Add(new HistoryRow()
            //            {
            //                //id = Convert.ToInt16((dr["Id"])),
            //                //arrivaltime = DateTime.Parse(dr["arrivaltime"].ToString()),
            //                //busnumber = Convert.ToInt16((dr["busnumber"] ?? 0)),
            //                //busstation = dr["busstation"].ToString()
            //                HistoryRowId = Guid.Parse(dr["HistoryRowId"].ToString()),
            //                Cps = Convert.ToDouble(dr["Cps"]),
            //                De = Convert.ToDouble(dr["De"]),
            //                Der = Convert.ToDouble(dr["Der"]),
            //                Time = DateTime.Parse(dr["Time"].ToString()),
            //                Type = HistoryType.Alaram
            //            });
            //        }
            //    }
            //    return historyRowsList;
            //}
        }

        private void WriteHistoryRow(HistoryRow newRow)
        {
            try
            {
                if (historyRepository == null) return ;
                historyRepository.Create(newRow);
                historyRepository.Save();
            }
            catch (Exception e)
            {
                throw new FaultException(e.Message);
            }
        }

        private void WriteHistory(IEnumerable<HistoryRow> newHistory)
        {
            try
            {
                if (historyRepository == null) return;
                foreach (var row in newHistory) historyRepository.Create(row);
                historyRepository.Save();
            }
            catch (Exception e)
            {
                throw new FaultException(e.Message);
            }
        }

        private void ClearLocalHistory()
        {
            try
            {
                if (historyRepository == null) return;
                var localhistory = historyRepository.GetItems();
                var syncLocalHistory = localhistory.Where(r => r.IsSynchronized);
                foreach (var row in syncLocalHistory)
                {
                    historyRepository.Delete(row.EventTime, row.Type, row.DeviceSerialNumber);
                }
                historyRepository.Save();
            }
            catch (Exception e)
            {
                throw new FaultException(e.Message);
            }
        }
        #endregion

        public void Dispose()
        {
            historyRepository?.Dispose();
        }

    }
}
