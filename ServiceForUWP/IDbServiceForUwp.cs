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
    [ServiceContract]
    public interface IDbServiceForUwp
    {
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
    }
}
