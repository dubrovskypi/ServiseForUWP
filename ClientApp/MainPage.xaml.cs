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
            var uri = new Uri("http://localhost:55195/service1.svc/GetHistoryRowsJson");
            System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
            System.Net.Http.HttpResponseMessage responseGet = await client.GetAsync(uri);
            string json = await responseGet.Content.ReadAsStringAsync();
            answertb.Text = json;
        }

    }
}
