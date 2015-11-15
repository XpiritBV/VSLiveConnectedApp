using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VSLiveConnectedApp.Views;
using Xamarin.Forms;

namespace VSLiveConnectedApp
{
    public class App : Application
    {
        public App()
        {
            var nav = new NavigationPage(new CitiesView());
            // The root page of your application
            MainPage = nav;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
