using Learn.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Learn.Pages
{
    /// <summary>
    /// Логика взаимодействия для PRecordClient.xaml
    /// </summary>
    public partial class PRecordClient : Page
    {
        Service contextService;
        ClientService contextClientService;
        public PRecordClient(Service service)
        {
            InitializeComponent();
            contextService = service;
            TBTitle.Text = contextService.Title;
            TBDuration.Text = $", длительность: {contextService.DurationInMinutes} минут";
            var recordClient = new ClientService();
            contextClientService = recordClient;
            DataContext = contextClientService;
            CBClient.ItemsSource = App.DB.Client.ToList();
            App.MainWindowInstance.BBack.Visibility = Visibility.Visible;
        }

        private void BSave_Click(object sender, RoutedEventArgs e)
        {
            string error = "";
            if (CBClient.SelectedItem == null)
                error += "Выберите клиента\n";
            if (DPDate.SelectedDate == null)
                error += "Выберите дату\n";
            if (String.IsNullOrWhiteSpace(TBTime.Text))
                error += "Введите время оказания услуги\n";
            if (contextService.MainImagePath == null)
                error += "Загрузите главное фото\n";
            if (String.IsNullOrWhiteSpace(error) == false)
            {
                MessageBox.Show(error);
                return;
            }
            var selectedClient = CBClient.SelectedItem as ClientService;
            contextClientService.StartTime = DPDate.SelectedDate.Value + DateTime.Parse(TBTime.Text).TimeOfDay;
            contextClientService.ServiceID = contextService.ID;
            if (String.IsNullOrWhiteSpace(TBComment.Text) == false)
            {
                contextClientService.Comment = TBComment.Text;
            }
            App.DB.ClientService.Add(contextClientService);
            App.DB.SaveChanges();
            NavigationService.GoBack();
        }
        private void RegexValidation(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^(2[0-3]|[01]d)([:][0-5]d)$");
            e.Handled = regex.IsMatch(e.Text);
            if (e.Text == ":" && (sender as TextBox).Text.Contains("."))
            {
                e.Handled = true;
            }
        }

    }
}
