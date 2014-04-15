using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using DearDiary.Resources;
using System.ComponentModel;

namespace DearDiary
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }


        StoryContext db;
        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            db = new StoryContext("isostore:/Story.sdf");
            if (!db.DatabaseExists())
            {
                db.CreateDatabase();
            }

            MainListBox.ItemsSource = db.diary.OrderBy(item => item.idStory);
            MainListBox.SelectedItem = null;

            while (this.NavigationService.BackStack.Any())
            {
                this.NavigationService.RemoveBackEntry();
            }
        }

        private void MainListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                int id = (MainListBox.SelectedItem as Story).idStory;
                NavigationService.Navigate(new Uri("/ReadStory.xaml?id=" + id.ToString(), UriKind.Relative));
            }
            catch
            {
                
            }
        }

        private void menuEdit_Click(object sender, RoutedEventArgs e)
        {
            int id = ((sender as MenuItem).DataContext as Story).idStory;
            NavigationService.Navigate(new Uri("/EditPage.xaml?id=" + id.ToString(), UriKind.Relative));
        }

        private void menuDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Would you like to delete this story?", "Delete Story", MessageBoxButton.OKCancel);

            if (result == MessageBoxResult.OK)
            {
                // delete data
                int id = ((sender as MenuItem).DataContext as Story).idStory;
                db = new StoryContext("isostore:/Story.sdf");
                Story _story = (from item in db.diary where item.idStory == id select item).FirstOrDefault();
                db.diary.DeleteOnSubmit(_story);
                db.SubmitChanges();
                MainListBox.ItemsSource = db.diary;
            }
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Tap add button to write your story, or tap your story to read it.");
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/AboutUs.xaml", UriKind.Relative));
        }

        private void Image_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Uri("/AddStory.xaml", UriKind.Relative));
        }

        private void btnChg_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/chgPIN.xaml", UriKind.Relative));
        }

    }
}