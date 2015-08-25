#region License
//-----------------------------------------------------------------------
// <copyright>
//     Copyright matatabi-ux 2015.
// </copyright>
//-----------------------------------------------------------------------
#endregion

using System;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.Practices.Unity;
using XamarinReader.Droid.Models;
using XamarinReader.Models;

namespace XamarinReader.Droid
{
    /// <summary>
    /// Main Activity class
    /// </summary>
    [Activity(Label = "XamarinReader", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        /// <summary>
        /// Activity create event handler
        /// </summary>
        /// <param name="bundle">Application bundle</param>
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            var application = new App();
            application.Container.RegisterInstance<ITextSpeechService>(new TextSpeechService(), new ContainerControlledLifetimeManager());

            this.LoadApplication(application);
        }
    }
}

