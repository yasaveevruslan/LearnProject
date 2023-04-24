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
using System.Windows.Shapes;

namespace Learn.AppWindows
{
    /// <summary>
    /// Логика взаимодействия для ListRecordingClientWindow.xaml
    /// </summary>
    public partial class ListRecordingClientWindow : Window
    {
        public ListRecordingClientWindow()
        {
            InitializeComponent();
            Refresh();
        }
        private void Refresh()
        {
            var filtred = App.DB.ClientService.ToList();
            filtred = filtred.OrderBy(f => f.StartTime).ToList();
            filtred = filtred.Where((f => f.StartTime.Date == DateTime.Now.Date || f.StartTime.Date == (DateTime.Now.Date.AddDays(1)))).ToList();
            LVRecordingClient.ItemsSource = filtred;
        }
    }
}
