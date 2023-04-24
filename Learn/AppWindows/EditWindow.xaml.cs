﻿using Learn.Model;
using Learn.Services;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;

namespace Learn.AppWindows
{
    /// <summary>
    /// Логика взаимодействия для EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        Service contextService;
        int currentPage = 0;
        int maxPage;
        int takePicture = 3;
        public EditWindow(Service service)
        {
            InitializeComponent();
            contextService = service;
            TBDuration.Text = Convert.ToString(contextService.DurationInSeconds / 60);
            DataContext = contextService;
            if (contextService.ID == 0)
                contextService.Discount = 0;
            if (contextService.Title == null)
                Title = "Добавление услуги";
            maxPage = App.DB.ServicePhoto.Count() / takePicture;
            if (App.DB.ServicePhoto.Count() % takePicture != 0)
                maxPage++;
        }

        private void BAddPhoto_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog() { Filter = ".png, .jpeg, .jpg | *.png; *.jpeg; *.jpg" };
            if (dialog.ShowDialog().GetValueOrDefault())
            {
                var photo = File.ReadAllBytes(dialog.FileName);
                contextService.MainImagePath = photo;
                DataContext = null;
                DataContext = contextService;
            }
        }

        private void BSave_Click(object sender, RoutedEventArgs e)
        {
            string error = "";
            if (String.IsNullOrWhiteSpace(contextService.Title))
                error += "Введите название услуги\n";
            var title = App.DB.Service.FirstOrDefault(c => c.Title == contextService.Title && contextService.ID == 0);
            if (title != null)
                error += "Такая услуга уже существует\n";
            if (String.IsNullOrWhiteSpace(Convert.ToString(contextService.Cost)))
                error += "Введите цену услуги\n";
            if (contextService.MainImagePath == null)
                error += "Загрузите главное фото\n";
            if ((int.Parse(TBDuration.Text) > 240))
                error += "Длительность не может быть больше 4 часов";
            if (String.IsNullOrWhiteSpace(error) == false)
            {
                MessageBox.Show(error);
                return;
            }
            contextService.DurationInSeconds = int.Parse(TBDuration.Text) * 60;
            if (contextService.ID == 0)
                App.DB.Service.Add(contextService);
            App.DB.SaveChanges();
            this.DialogResult = true;
        }

        private void BNext_Click(object sender, RoutedEventArgs e)
        {
            currentPage++;
            if (currentPage == maxPage)
                currentPage = maxPage - 1;
            Refresh();
        }

        private void BPrevious_Click(object sender, RoutedEventArgs e)
        {
            currentPage--;
            if (currentPage < 0)
                currentPage = 0;
            Refresh();
        }
        private void Refresh()
        {
            TBList.Text = Convert.ToString(currentPage + 1);
            GenerateImages(App.DB.ServicePhoto.ToList().Where(f=> f.ServiceID == contextService.ID).Skip(currentPage * takePicture).Take(takePicture).ToList());
        }
        private void GenerateImages(List<ServicePhoto> pictures)
        {
            WPPhotos.Children.Clear();
            foreach (var picture in pictures)
            {
                Image image = new Image();
                image.Source = MyTools.BytesToImage(picture.PhotoPath);
                image.Width = 400;
                image.Height = 700;
                image.MouseRightButtonDown += MouseRightButtonDown;
                image.DataContext = picture;
                image.Margin = new Thickness(5);
                WPPhotos.Children.Add(image);
            }
        }
        private void MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var selectedPicture = (sender as Image).DataContext as ServicePhoto;
            App.DB.ServicePhoto.Remove(selectedPicture);
            WPPhotos.Children.Clear();
            App.DB.SaveChanges();
            Refresh();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }
        private void BLoadPhoto_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog() { Filter = ".png, .jpg, .jpeg | *.png; *.jpg; *.jpeg;", Multiselect = true };
            if (dialog.ShowDialog().GetValueOrDefault())
            {
                foreach (var fileName in dialog.FileNames)
                {
                    var picture = new ServicePhoto();
                    picture.PhotoPath = File.ReadAllBytes(fileName);
                    picture.ServiceID = contextService.ID;
                    App.DB.ServicePhoto.Add(picture);
                    App.DB.SaveChanges();
                    Refresh();
                }

            }
        }

        private void RegexValidation(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
            if ((sender as TextBox).Text.Contains("."))
            {
                e.Handled = true;
            }
        }

    }
}
