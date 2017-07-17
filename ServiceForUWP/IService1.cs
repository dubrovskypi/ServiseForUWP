using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using CodeFirst.Entities;

namespace ServiceForUWP
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IService1" в коде и файле конфигурации.
    [ServiceContract]
    public interface IService1
    {
        [WebGet(UriTemplate = "/GetHistoryRowsJson",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare)]
        List<HistoryRow> GetHistoryRowsJson();
        //List<Timetable> GetScheduleJson();
    }

    //[DataContract]
    //public class Timetable
    //{
    //    [DataMember]
    //    public int id { get; set; }

    //    [DataMember]
    //    public DateTime arrivaltime { get; set; }

    //    [DataMember]
    //    public Int16 busnumber { get; set; }

    //    [DataMember]
    //    public string busstation { get; set; }
    //}
    [DataContract]
    public class Customer
    {
        [DataMember]
        public Guid CustomerId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public int Age { get; set; }

        [DataMember]
        public byte[] Photo { get; set; }

        public virtual List<Order> Orders { get; set; }
    }
}
