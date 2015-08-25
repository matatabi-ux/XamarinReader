using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using XamarinReader.Models;
using XamarinReader.WinPhone.Models;
using Microsoft.Practices.Unity;

namespace XamarinReader.WinPhone
{
    public partial class MainPage : global::Xamarin.Forms.Platform.WinPhone.FormsApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();
            SupportedOrientations = SupportedPageOrientation.PortraitOrLandscape;

            global::Xamarin.Forms.Forms.Init();

            var application = new XamarinReader.App();
            application.Container.RegisterInstance<ITextSpeechService>(new TextSpeechService(), new ContainerControlledLifetimeManager());

            this.LoadApplication(application);
        }
    }
}
