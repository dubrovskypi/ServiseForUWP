using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using CodeFirst;
using CodeFirst.Entities;

namespace ServiceForUWP
{
    [ServiceContract]
    public interface IDbServiceForUwp
    {
        [OperationContract]
        [WebGet(UriTemplate = "/GetHistoryRowsJson",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        List<HistoryRow> GetHistoryRowsJson();

        [OperationContract]
        [WebInvoke(UriTemplate = "/AddHistoryRow",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            Method = "POST")]        
        void AddHistoryRow(HistoryRow newRow);

        [OperationContract]
        [WebInvoke(UriTemplate = "/AddHistory",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            Method = "POST")]
        void AddHistory(List<HistoryRow> newHistory);

        [OperationContract]
        [WebInvoke(UriTemplate = "/SetConnection",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            Method = "POST")]
        void SetConnection(ConnectionProperty connection);

        [WebGet(UriTemplate = "/WriteToCloud",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        void WriteToCloud();

        [WebGet(UriTemplate = "/ClearHistory",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        void ClearHistory();

        [WebGet(UriTemplate = "/FillTestRows",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        void FillTestRows();
    }
}
