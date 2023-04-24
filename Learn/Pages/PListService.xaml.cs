using Learn.AppWindows;
using Learn.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Learn.Pages
{
    /// <summary>
    /// Логика взаимодействия для PListService.xaml
    /// </summary>
    public partial class PListService : Page
    {
        public PListService()
        {
            InitializeComponent();
            Refresh(0);
            CBSale.SelectedIndex = 0;
            

        }
        
        private void BEdit_Click(object sender, RoutedEventArgs e)
        {
            var service = (sender as Button).DataContext as Service;
            new EditWindow(service).ShowDialog();
            Refresh(0);
        }

        private void BDelete_Click(object sender, RoutedEventArgs e)
        {
            var service = (sender as Button).DataContext as Service;
            new DeleteWindow(service).ShowDialog();
            Refresh(0);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh(0);
        }
        private void Refresh(int i)
        {
            App.MainWindowInstance.BBack.Visibility = Visibility.Collapsed;
            if (App.MainWindowInstance.TBCode.Text == "0000")
            {
                BViewRecordingService.Visibility = Visibility.Visible;
                BAddSerice.Visibility = Visibility.Visible;
            }
            
            else
            {
                BViewRecordingService.Visibility = Visibility.Collapsed;
                BAddSerice.Visibility = Visibility.Collapsed;

            }
            
            var filtred = App.DB.Service.ToList();
            var allService = App.DB.Service.ToList();
            var searchText = TBSearch.Text.ToLower();
            if (CBSale != null && CBSale.SelectedIndex == 1)
                filtred = filtred.Where(f => Convert.ToString(f.Discount.Value) == f.Discount0).ToList();
            if (CBSale != null && CBSale.SelectedIndex == 2)
                filtred = filtred.Where(f => Convert.ToString(f.Discount.Value) == f.Discount5).ToList();
            if (CBSale != null && CBSale.SelectedIndex == 3)
                filtred = filtred.Where(f => Convert.ToString(f.Discount.Value) == f.Discount15).ToList();
            if (CBSale != null && CBSale.SelectedIndex == 4)
                filtred = filtred.Where(f => Convert.ToString(f.Discount.Value) == f.Discount30).ToList();
            if (CBSale != null && CBSale.SelectedIndex == 5)
                filtred = filtred.Where(f => Convert.ToString(f.Discount.Value) == f.Discount70).ToList();
            if (string.IsNullOrWhiteSpace(searchText) == false)
                filtred = filtred.Where(f => f.Title.ToLower().Contains(searchText) || (f.Description != null && f.Description.ToLower().Contains(searchText))).ToList();
            if(i == 1)
                filtred = filtred.OrderBy(f => f.NewCost).ToList();
            if(i == 2)
                filtred = filtred.OrderByDescending(f => f.NewCost).ToList();
            LVService.ItemsSource = filtred.ToList();
            TBAllProducts.Text = $"{filtred.Count} из {allService.Count}";
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh(0);
        }

        private void TBSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Refresh(0);
        }

        private void BAddService_Click(object sender, RoutedEventArgs e)
        {
            new EditWindow(new Service()).ShowDialog();
            Refresh(0);
        }

        private void BRecordClient_Click(object sender, RoutedEventArgs e)
        {
            var service = (sender as Button).DataContext as Service;
            NavigationService.Navigate(new PRecordClient(service));
        }

        private void BAscending_Click(object sender, RoutedEventArgs e)
        {
            Refresh(1);
        }

        private void BDecreasing_Click(object sender, RoutedEventArgs e)
        {
            Refresh(2);
        }

        private void BViewRecordingService_Click(object sender, RoutedEventArgs e)
        {
            new ListRecordingClientWindow().ShowDialog();
        }
        
    }
}
