using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Data;
using System.Data.SqlClient;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http.Headers;
using Newtonsoft.Json;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace ClientApp
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>

    public sealed partial class MainPage : Page
    {

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GetData();
        }

        public async void GetData()
        {

            //URL нормальный нужно юзать
            //var uri = new Uri("http://localhost:64870/service1.svc/GetScheduleJson");
            //var client = new Windows.Web.Http.HttpClient();
            //var json = await client.GetStringAsync(uri);

            //var uri = new Uri("http://localhost:55195/service1.svc/GetHistoryRowsJson");
            //System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
            //System.Net.Http.HttpResponseMessage responseGet = await client.GetAsync(uri);
            //string json = await responseGet.Content.ReadAsStringAsync();
            //List<HistoryRowModel> appsdata = JsonConvert.DeserializeObject<List<HistoryRowModel>>(json);
            //answertb.Text = json;
            //HistoryList.ItemsSource = appsdata;
            try
            {
                var uri = new Uri("http://localhost:55195/DbServiceForUwp.svc/GetHistoryRowsJson");
                var client = new Windows.Web.Http.HttpClient();
                client.DefaultRequestHeaders.IfModifiedSince = DateTime.Now;
                var json = await client.GetStringAsync(uri);
                List<HistoryRowModel> appsdata = JsonConvert.DeserializeObject<List<HistoryRowModel>>(json);
                answertb.Text = json;
                if (HistoryList.ItemsSource != null) HistoryList.ItemsSource = null;
                HistoryList.ItemsSource = appsdata;
                client.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        private async void AddRow_Button_Click(object sender, RoutedEventArgs e)
        {
            var uri = new Uri("http://localhost:55195/DbServiceForUwp.svc/AddHistoryRow");

            try
            {
                System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
                var newrowmodel = new HistoryRowModel()
                {
                    Cps = Convert.ToDouble(Cpstb.Text),
                    De = Convert.ToDouble(Detb.Text),
                    Der = Convert.ToDouble(Dertb.Text),
                    HistoryRowId = Guid.NewGuid(),
                    Time = DateTime.Now,
                    Type = HistoryType.ChangedNCoefficent
                };

                JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                };
                // Serialize our concrete class into a JSON String
                var stringPayload = JsonConvert.SerializeObject(newrowmodel, microsoftDateFormatSettings);

                // Wrap our JSON inside a StringContent which then can be used by the HttpClient class
                var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                // Do the actual request and await the response
                var httpResponse = await client.PostAsync(uri, httpContent);

                // If the response contains content we want to read it!
                if (httpResponse.Content != null)
                {
                    var responseContent = await httpResponse.Content.ReadAsStringAsync();
                    answertb.Text = responseContent;
                    // From here on you could deserialize the ResponseContent back again to a concrete C# type using Json.Net
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            //try
            //{
            //    var newrowmodel2 = new HistoryRowModel()
            //    {
            //        Cps = Convert.ToDouble(Cpstb.Text),
            //        De = Convert.ToDouble(Detb.Text),
            //        Der = Convert.ToDouble(Dertb.Text),
            //        HistoryRowId = Guid.NewGuid(),
            //        Time = DateTime.Now,
            //        Type = HistoryType.Alaram
            //    };
            //    string postBody = JsonSerializer(newrowmodel2);
            //    var client2 = new HttpClient();
            //    client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //    HttpResponseMessage wcfResponse = await client2.PostAsync(uri, new StringContent(postBody, Encoding.UTF8, "application/json"));
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex);
            //}
        }

        public string JsonSerializer(object objectToSerialize)
        {
            if (objectToSerialize == null)
            {
                throw new ArgumentException("objectToSerialize must not be null");
            }
            MemoryStream ms = null;

            DataContractJsonSerializer serializer = new DataContractJsonSerializer(objectToSerialize.GetType());
            ms = new MemoryStream();
            serializer.WriteObject(ms, objectToSerialize);
            ms.Seek(0, SeekOrigin.Begin);
            StreamReader sr = new StreamReader(ms);
            return sr.ReadToEnd();
        }
    }
}
