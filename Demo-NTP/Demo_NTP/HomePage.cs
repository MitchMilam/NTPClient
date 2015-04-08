using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Demo_NTP
{
    public class HomePage : ContentPage
    {
        private DateTime CurrentNetworkTime;
        private Label TimeLabel;

        public HomePage()
        {
            TimeLabel = new Label
            {
                XAlign = TextAlignment.Center,
            };

            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                Children =
                {
                    new Label
                    {
                        XAlign = TextAlignment.Center,
                        Text = "Welcome to Xamarin Forms!"
                    },
                    TimeLabel
                }
            };
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                CurrentNetworkTime = await NtpClient.GetNetworkTimeAsync();

                TimeLabel.Text = string.Format("It is currently {0}", CurrentNetworkTime);
            }
            catch (TimeoutException ex)
            {
                DisplayAlert("Error", ex.Message, "OK");

                Debug.WriteLine(ex.Message);
            }
        }
    }
}
