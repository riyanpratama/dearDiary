using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;

namespace DearDiary
{
    public partial class AboutUs : PhoneApplicationPage
    {
        public AboutUs()
        {
            InitializeComponent();
        }

        private void TextBlock_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            WebBrowserTask _twt = new WebBrowserTask();
            _twt.Uri = new Uri("https://twitter.com/DearDiaryID_", UriKind.Absolute);
            _twt.Show();
        }
    }
}