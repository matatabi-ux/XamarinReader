#region License
//-----------------------------------------------------------------------
// <copyright>
//     Copyright matatabi-ux 2015.
// </copyright>
//-----------------------------------------------------------------------
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using Xamarin.Forms;
using XamarinReader.Models;
using XamarinReader.Themes;
using XamarinReader.Views;

namespace XamarinReader
{
    /// <summary>
    /// Application class
    /// </summary>
    public class App : Application
    {
        /// <summary>
        /// Dependency injection container
        /// </summary>
        public IUnityContainer Container = new UnityContainer();

        /// <summary>
        /// Costructor
        /// </summary>
        public App()
        {
            this.Container.RegisterType<INewsFeedService, NewsFeedService>(new ContainerControlledLifetimeManager());
            this.Container.RegisterType<ITranslateService, TranslateService>(new ContainerControlledLifetimeManager());

            switch (Device.OS)
            {
                case TargetPlatform.iOS:
                    App.Current.Resources = new LightThemeResources().Resources;
                    break;

                default:
                    App.Current.Resources = new DarkThemeResources().Resources;
                    break;
            }

            this.MainPage = new TopPage();
        }
    }
}
