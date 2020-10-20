using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Net;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using System.Data;

namespace NotionNetWPF
{
    /// <summary>
    /// Interaction logic for MyListxaml.xaml
    /// </summary>
    public partial class MyListxaml : Window
    {
        public MyListxaml()
        {
            InitializeComponent();

              lbluname.Content=App.Current.Properties["UserName"];            
            var arrlist= singleAPIDataToDB();
            lvDataBinding.ItemsSource = (System.Collections.IEnumerable)arrlist;
        }
        public static List<MyListItem> singleAPIDataToDB()
        {
            MyListItem lst = null;
            List<MyListItem> list = new List<MyListItem>();
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://jsonplaceholder.typicode.com/todos");

                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "GET";

                
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var response = Newtonsoft.Json.JsonConvert.DeserializeObject<List<MyListItem>>(result);
                    // DBCode.SIMDeviceData(response, DBName);
                    //Response.Write(result);
                    for (int i = 0; i < response.Count; i++)
                    {
                        list.Add(response[i]);
                     
                    }
                    
                   
                    return list;
                }
              
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception At : " + DateTime.Now.ToString() + "\n" + ex.StackTrace);
                return list;
            }
        }

        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListViewItem;
           
            if (item != null )
            {              
                var dataContext= item.DataContext;

                MyListItem m =(MyListItem)dataContext;

                MessageBox.Show("Title :"+m.title+"\nUser Id :"+m.userId+"\nId :"+m.id+"\nCompleted: "+(m.completed?"Yes":"No"),m.title);
                               
            }
        }
    }
}
