
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Http;
using System.Threading;
using System.Globalization;
using System.Windows.Threading;

namespace AsyncFirstExample
{
    public partial class MainWindow : Window
    {
        #region 
        //private void StartButton_Click(object sender, RoutedEventArgs e)
        //{
        //    int index = (new Random()).Next(10,99);
        //    resultsTextBox.Text += index + " - Start : " + DateTime.Now.ToLongTimeString() + "\r\n";

        //    int contentLength = AccessTheWeb(index);

        //    resultsTextBox.Text += index + " - End : " +
        //        String.Format(" - Length of the downloaded string: {0}. ", contentLength) + " : " + DateTime.Now.ToLongTimeString() +
        //        "\r\n";
        //}

        //int AccessTheWeb(int index)
        //{ 
        //    HttpClient client = new HttpClient();

        //    string getStringTask = client.GetStringAsync("http://www.baidu.com").Result;

        //    DoWork(index);

        //    return getStringTask.Length;
        //}

        //void DoWork(int index)
        //{
        //    resultsTextBox.Text += index + " - Working . . . . . . ." + DateTime.Now.ToLongTimeString() + "\r\n";
        //}
        #endregion

        // Begin Code change

        private async void StartButton_Click(object sender, RoutedEventArgs e)
        {
            int index = (new Random()).Next(10, 99);
            string firstMessage = index + " - Start : " + DateTime.Now.ToLongTimeString() + "\r\n";
            ShowMessage(firstMessage);
            int contentLength = await AccessTheWeb(index);

            string thirdMessage= index + " - End : " +
                String.Format(" - Length of the downloaded string: {0}. ", contentLength) + " : " + DateTime.Now.ToLongTimeString() +
                "\r\n";
            ShowMessage(thirdMessage);
        }

        async Task<int> AccessTheWeb(int index)
        {
            var TaskResult = TimeConsumingMethodReturnObject();
            var returnResult = await TaskResult;
            string getStringTask = returnResult.ReturnMessage;

            DoWork(index);

            return getStringTask.Length;
        }

        private async Task<ReturnObject> TimeConsumingMethodReturnObject()
        {
            var task = Task.Run(() =>
            {
                ReturnObject returnObject = new ReturnObject();
                HttpClient client = new HttpClient();
                returnObject.ReturnMessage = client.GetStringAsync("http://www.baidu.com").Result;
                return returnObject;
            });
            return await task;
        }
        void DoWork(int index)
        {
            string secondMessage = index + " - Working . . . . . . ." + DateTime.Now.ToLongTimeString() + "\r\n";
            ShowMessage(secondMessage);
        }

        private void ShowMessage(string message)
        {
            this.Dispatcher.BeginInvoke(new Action(() => resultsTextBox.Text += message));
        }
        public class ReturnObject
        {
            public string ReturnMessage { get; set; }
        }

    }
}
