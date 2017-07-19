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
using Newtonsoft.Json;
using UwpClassLib;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace ClientApp
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    /// 
    /// 
    /// 
    public class Timetable
    {
        
        public int id { get; set; }

        
        //public DateTime arrivaltime { get; set; }

        
        public Int16 busnumber { get; set; }

        
        public string busstation { get; set; }
    }
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

            var uri = new Uri("http://localhost:55195/DbServiceForUwp.svc/GetHistoryRowsJson");
            var client = new Windows.Web.Http.HttpClient();
            var json = await client.GetStringAsync(uri);
            List<HistoryRowModel> appsdata = JsonConvert.DeserializeObject<List<HistoryRowModel>>(json);
            answertb.Text = json;
            HistoryList.ItemsSource = appsdata;
        }


        //TODO хз как передать объект в пост запросе
        private async void AddRow_Button_Click(object sender, RoutedEventArgs e)
        {


            
            //HistoryRowModel order = new HistoryRowModel
            //{
            //    HistoryRowId = Guid.NewGuid(),
            //    Cps = "0,1",
            //    De = "0,2",
            //    Der = "0,3",
            //    Time = DateTime.Now,
            //    Type = "s"
            //};
            var uri = new Uri("http://localhost:55195/DbServiceForUwp.svc/AddHistoryRow");
            System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
            var newrowmodel = new Timetable()
            {
                //arrivaltime = DateTime.Now,
                busnumber = 5,
                busstation = "station",
                id = 9
                //Cps = Convert.ToDouble(Cpstb.Text),
                //De = Convert.ToDouble(Detb.Text),
                //Der = Convert.ToDouble(Dertb.Text),
                //HistoryRowId = Guid.NewGuid(),
                //Time = DateTime.Now,
                //Type = HistoryType.ChangedNCoefficent
            };
            // Serialize our concrete class into a JSON String
            var stringPayload = JsonConvert.SerializeObject(newrowmodel);

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


            string postBody = JsonSerializer(newrowmodel);
            var client2 = new HttpClient();
            client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage wcfResponse = await client2.PostAsync(uri, new StringContent(postBody, Encoding.UTF8, "application/json"));


            //System.Net.Http.HttpResponseMessage response = await client.PostAsync(uri,null);
            //string s = await response.Content.ReadAsStringAsync();
            //answertb.Text = s;
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
