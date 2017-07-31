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
using HttpClient = Windows.Web.Http.HttpClient;

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

        #region EventHandlers

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GetHistory();
        }

        private async void AddRow_Button_Click(object sender, RoutedEventArgs e)
        {
            await AddHistoryRow();
        }

        private async void AddHistory_Button_Click(object sender, RoutedEventArgs e)
        {
            await AddTestHistory();
        }

        private async void SetConnection_Button_Click(object sender, RoutedEventArgs e)
        {
            await SetConnection();
        }

        private async void SaveToCloud_Button_Click(object sender, RoutedEventArgs e)
        {
            await SaveToCloud();
        }

        private async void ClearHistory_Button_Click(object sender, RoutedEventArgs e)
        {
            await ClearHistory();
        }
        #endregion

        #region PrivateMetods

        private async void GetHistory()
        {
            try
            {
                var uri = new Uri("http://localhost:55195/DbServiceForUwp.svc/GetHistoryRowsJson"); //текушее решение
                //var uri = new Uri("http://localhost:60136/DbService.svc/GetHistoryRowsJson"); //dbservice
                //var uri = new Uri("http://localhost:14888/DbService/GetHistoryRowsJson");//dbwebservice
                var client = new HttpClient();
                client.DefaultRequestHeaders.IfModifiedSince = DateTime.Now;
                var json = await client.GetStringAsync(uri);
                answertb.Text = json;
                List<HistoryRowModel> appsdata = JsonConvert.DeserializeObject<List<HistoryRowModel>>(json);
                if (HistoryList.ItemsSource != null) HistoryList.ItemsSource = null;
                HistoryList.ItemsSource = appsdata;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private async Task AddHistoryRow()
        {
            try
            {
                var uri = new Uri("http://localhost:55195/DbServiceForUwp.svc/AddHistoryRow");//текущее решение
                //var uri = new Uri("http://localhost:60136/DbService.svc/AddHistoryRow"); dbservice
                //var uri = new Uri("http://localhost:14888/DbService/AddHistoryRow"); вебсервис
                System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
                var newrowmodel = new HistoryRowModel()
                {
                    Cps = Convert.ToDouble(Cpstb.Text),
                    De = Convert.ToDouble(Detb.Text),
                    Der = Convert.ToDouble(Dertb.Text),
                    //Id = Guid.NewGuid(),
                    EventTime = DateTime.Now,
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
        }

        private async Task AddTestHistory()
        {
            try
            {
                System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
                //var uri = new Uri("http://localhost:60136/DbService.svc/AddHistory"); //dbservice
                var uri = new Uri("http://localhost:55195/DbServiceForUwp.svc/AddHistory"); // current solution
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
                        EventTime = DateTime.Now,
                        Type = HistoryType.DeviceOn,
                        DeviceSerialNumber = Guid.NewGuid().ToString(),
                        ReaderSerialNumber = "readerserial"
                    };
                    history.Add(newrowmodel);
                }
                JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                };
                var stringPayload = JsonConvert.SerializeObject(history, microsoftDateFormatSettings);
                var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");
                var httpResponse = await client.PostAsync(uri, httpContent);
                if (httpResponse.Content != null)
                {
                    var responseContent = await httpResponse.Content.ReadAsStringAsync();
                    answertb.Text = responseContent;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private async Task SetConnection()
        {
            try
            {
                var uri = new Uri("http://localhost:55195/DbServiceForUwp.svc/SetConnection"); //current solution
                //var uri = new Uri("http://localhost:60136/DbService.svc/SetConnection"); //dbservice
                System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
                var newConProp = new ConnectionPropertyModel();

                var stringPayload = JsonConvert.SerializeObject(newConProp);
                var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");
                var httpResponse = await client.PostAsync(uri, httpContent);
                if (httpResponse.Content != null)
                {
                    var responseContent = await httpResponse.Content.ReadAsStringAsync();
                    answertb.Text = responseContent;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private async Task SaveToCloud()
        {
            try
            {
                //var uri = new Uri("http://localhost:60136/DbService.svc/WriteDataToCloud"); //dbservice
                var uri = new Uri("http://localhost:55195/DbServiceForUwp.svc/WriteToCloud"); //current solution
                var client = new HttpClient();
                client.DefaultRequestHeaders.IfModifiedSince = DateTime.Now;
                var json = await client.GetStringAsync(uri);
                answertb.Text = json;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private async Task ClearHistory()
        {
            try
            {
                //var uri = new Uri("http://localhost:60136/DbService.svc/ClearHistory"); //dbservice
                var uri = new Uri("http://localhost:55195/DbServiceForUwp.svc/ClearHistory"); //current solution
                var client = new HttpClient();
                client.DefaultRequestHeaders.IfModifiedSince = DateTime.Now;
                var json = await client.GetStringAsync(uri);
                answertb.Text = json;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        #endregion

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
