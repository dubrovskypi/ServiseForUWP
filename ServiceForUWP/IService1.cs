using System;
using System.Collections.Generic;
using System.IO;
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

        [WebInvoke(UriTemplate = "/AddHistoryRow",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            Method = "POST")]        
        string AddHistoryRow(Stream newRow);

        //todo cделать атрибуты webinvoke
        [WebInvoke(UriTemplate = "/AddHistory",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            Method = "POST")]
        void AddHistory(List<HistoryRow> newHistory);

        [WebInvoke(UriTemplate = "/DeleteHistoryRow",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            Method = "POST")]
        void DeleteHistoryRow(HistoryRow row);

        [WebInvoke(UriTemplate = "/ClearHistory",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            Method = "POST")]
        void ClearHistory();


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
}
