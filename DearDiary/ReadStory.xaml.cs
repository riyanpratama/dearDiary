using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace DearDiary
{
    public partial class ReadStory : PhoneApplicationPage
    {
        public ReadStory()
        {
            InitializeComponent();
        }

        StoryContext db;
        Story _story;
        int id;

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (NavigationContext.QueryString["id"] != null)
            {
                int.TryParse(NavigationContext.QueryString["id"].ToString(), out id);
                db = new StoryContext("isostore:/Story.sdf");

                _story = (from item in db.diary where item.idStory == id select item).FirstOrDefault();
                 //MessageBox.Show(id.ToString());

                txtReadStory.Text = _story.TextStory;
                txtReadCreated.Text = _story.Created;
            }
        }

        private void btnReadDelete_Click(object sender, EventArgs e)
        {
            if (NavigationContext.QueryString["id"] != null)
            {
                int.TryParse(NavigationContext.QueryString["id"].ToString(), out id);
                db = new StoryContext("isostore:/Story.sdf");

                _story = (from item in db.diary where item.idStory == id select item).FirstOrDefault();

                db.diary.DeleteOnSubmit(_story);
                db.SubmitChanges();

                NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
            }
        }

        private void btnReadEdit_Click(object sender, EventArgs e)
        {
            int.TryParse(NavigationContext.QueryString["id"].ToString(), out id);
            NavigationService.Navigate(new Uri("/EditPage.xaml?id=" + id.ToString(), UriKind.Relative));
        }

    }
}