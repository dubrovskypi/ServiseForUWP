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
            GetHistory();
        }

        private async void AddRow_Button_Click(object sender, RoutedEventArgs e)
        {
            await AddHistoryRow();
        }

        private void AddHistory_Button_Click(object sender, RoutedEventArgs e)
        {
            AddTestHistory();
        }

        public async void GetHistory()
        {
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
                //var uri = new Uri("http://localhost:60136/DbService.svc/GetHistoryRowsJson");
                //var uri = new Uri("http://localhost:14888/DbService/GetHistoryRowsJson");
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

        private async Task AddHistoryRow()
        {
            //var uri = new Uri("http://localhost:55195/DbServiceForUwp.svc/AddHistoryRow");
            //var uri = new Uri("http://localhost:60136/DbService.svc/AddHistoryRow");
            var uri = new Uri("http://localhost:14888/DbService/AddHistoryRow");


            try
            {
                HttpClient client = new HttpClient();
                var newrowmodel = new HistoryRowModel()
                {
                    Cps = Convert.ToDouble(Cpstb.Text),
                    De = Convert.ToDouble(Detb.Text),
                    Der = Convert.ToDouble(Dertb.Text),
                    //HistoryRowId = Guid.NewGuid(),
                    //Id = Guid.NewGuid(),
                    MyTime = DateTime.Now,
                    Type = HistoryType.ChangedNCoefficent,
                    IsSynchronized = false,
                    DeviceSerialNumber = Guid.NewGuid().ToString(),
                    ReaderSerialNumber = "asdasd123123"
                };

                //установка настроек сериализации
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

            //отправка пост-запроса данными, сериализованными стандартным способом microsoft
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

        private async void AddTestHistory()
        {
            try
            {
                HttpClient client = new HttpClient();
                List<HistoryRowModel> history = new List<HistoryRowModel>();
                Random random = new Random();

                for (int i = 0; i < 50; i++)
                {
                    var newrowmodel = new HistoryRowModel()
                    {
                        Cps = random.NextDouble(),
                        De = random.NextDouble(),
                        Der = random.NextDouble(),
                        //HistoryRowId = Guid.NewGuid(),
                        Id = Guid.NewGuid(),
                        MyTime = DateTime.Now,
                        Type = HistoryType.DeviceOn,
                        DeviceSerialNumber = Guid.NewGuid().ToString(),
                        ReaderSerialNumber = "readerserial",

                        Integer321 = random.Next()
                    };
                    history.Add(newrowmodel);
                }

                //установка настроек сериализации
                JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                };
                // Serialize our concrete class into a JSON String
                var stringPayload = JsonConvert.SerializeObject(history, microsoftDateFormatSettings);

                // Wrap our JSON inside a StringContent which then can be used by the HttpClient class
                var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");
                //StreamContent httpContent;
                //using (Stream s = GenerateStreamFromString(stringPayload))
                //{
                //    httpContent = new StreamContent(s);
                //}
                // Do the actual request and await the response
                //var httpResponse = await client.PostAsync(new Uri("http://localhost:55195/DbServiceForUwp.svc/AddHistory"), httpContent);
                var httpResponse = await client.PostAsync(new Uri("http://localhost:60136/DbService.svc/AddHistory"), httpContent);

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
        }

        public static Stream GenerateStreamFromString(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        private async void SetConnection_Button_Click(object sender, RoutedEventArgs e)
        {
            //var uri = new Uri("http://localhost:55195/DbServiceForUwp.svc/SetConnection");
            var uri = new Uri("http://localhost:60136/DbServiceForUwp.svc/SetConnection");

            try
            {
                HttpClient client = new HttpClient();
                var newConProp = new ConPropModel();

                //установка настроек сериализации
                JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                };
                // Serialize our concrete class into a JSON String
                var stringPayload = JsonConvert.SerializeObject(newConProp, microsoftDateFormatSettings);

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
        }

        private async void SaveToCloud_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //var uri = new Uri("http://localhost:60136/DbService.svc/WriteDataToCloud");
                var uri = new Uri("http://localhost:55195/DbServiceForUwp.svc/WriteToCloud");
                var client = new Windows.Web.Http.HttpClient();
                client.DefaultRequestHeaders.IfModifiedSince = DateTime.Now;
                var json = await client.GetStringAsync(uri);
                //var appsdata = JsonConvert.DeserializeObject<bool>(json);
                answertb.Text = json;
                client.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private async void ClearHistory_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var uri = new Uri("http://localhost:60136/DbService.svc/ClearHistory");
                var client = new Windows.Web.Http.HttpClient();
                client.DefaultRequestHeaders.IfModifiedSince = DateTime.Now;
                var json = await client.GetStringAsync(uri);
                //var appsdata = JsonConvert.DeserializeObject<bool>(json);
                answertb.Text = json;
                client.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        //public string JsonSerializer(object objectToSerialize)
        //{
        //    if (objectToSerialize == null)
        //    {
        //        throw new ArgumentException("objectToSerialize must not be null");
        //    }
        //    MemoryStream ms = null;

        //    DataContractJsonSerializer serializer = new DataContractJsonSerializer(objectToSerialize.GetType());
        //    ms = new MemoryStream();
        //    serializer.WriteObject(ms, objectToSerialize);
        //    ms.Seek(0, SeekOrigin.Begin);
        //    StreamReader sr = new StreamReader(ms);
        //    return sr.ReadToEnd();
        //}
    }
}
