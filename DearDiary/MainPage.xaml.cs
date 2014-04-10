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

namespace DearDiary
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }
        
        private void btnAddNew_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/AddStory.xaml", UriKind.Relative));
        }

        StoryContext db;
        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            db = new StoryContext("isostore:/Story.sdf");
            if (!db.DatabaseExists())
            {
                db.CreateDatabase();
            }

            MainListBox.ItemsSource = db.diary;
        }

        private void MainListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                MainListBox.SelectedItem = null;
            }
            catch
            {

            }
        }

        private void menuEdit_Click(object sender, RoutedEventArgs e)
        {
            int id = ((sender as MenuItem).DataContext as Story).idStory;
            // lempar id, title, created, textStory
            NavigationService.Navigate(new Uri("/EditPage.xaml?id=" + id.ToString(), UriKind.Relative));
        }

        private void menuDelete_Click(object sender, RoutedEventArgs e)
        {
            int id = ((sender as MenuItem).DataContext as Story).idStory;
            db = new StoryContext("isostore:/Story.sdf");
            Story _story = (from item in db.diary where item.idStory == id select item).FirstOrDefault();
            db.diary.DeleteOnSubmit(_story);
            db.SubmitChanges();
            MainListBox.ItemsSource = db.diary;
        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}