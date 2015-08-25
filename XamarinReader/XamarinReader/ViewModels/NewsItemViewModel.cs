#region License
//-----------------------------------------------------------------------
// <copyright>
//     Copyright matatabi-ux 2015.
// </copyright>
//-----------------------------------------------------------------------
#endregion

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Mvvm;
using Xamarin.Forms;

namespace XamarinReader.ViewModels
{
    /// <summary>
    /// News feed item ViewModel
    /// </summary>
    public class NewsItemViewModel : BindableBase
    {
        /// <summary>
        /// Unique item ID
        /// </summary>
        private string uniqueId = string.Empty;

        /// <summary>
        /// Unique item ID
        /// </summary>
        public string UniqueId
        {
            get { return this.uniqueId; }
            set { this.SetProperty<string>(ref this.uniqueId, value); }
        }

        /// <summary>
        /// Title
        /// </summary>
        private string title = string.Empty;

        /// <summary>
        /// Title
        /// </summary>
        public string Title
        {
            get { return this.title; }
            set { this.SetProperty<string>(ref this.title, value); }
        }

        /// <summary>
        /// Category collections
        /// </summary>
        private ObservableCollection<string> categories = new ObservableCollection<string>();

        /// <summary>
        /// Category collections
        /// </summary>
        public ObservableCollection<string> Categories
        {
            get { return this.categories; }
            set { this.SetProperty<ObservableCollection<string>>(ref this.categories, value); }
        }

        /// <summary>
        /// Detail article link uri
        /// </summary>
        private string link = string.Empty;

        /// <summary>
        /// Detail article link uri
        /// </summary>
        public string Link
        {
            get { return this.link; }
            set { this.SetProperty<string>(ref this.link, value); }
        }

        /// <summary>
        /// Thumbnail uri
        /// </summary>
        private ImageSource thumbnail = null;

        /// <summary>
        /// Thumbnail uri
        /// </summary>
        public ImageSource Thumbnail
        {
            get { return this.thumbnail; }
            set { this.SetProperty<ImageSource>(ref this.thumbnail, value); }
        }

        /// <summary>
        /// Description text
        /// </summary>
        private string description = string.Empty;

        /// <summary>
        /// Description text
        /// </summary>
        public string Description
        {
            get { return this.description; }
            set { this.SetProperty<string>(ref this.description, value); }
        }

        /// <summary>
        /// Last updated datetime
        /// </summary>
        private DateTime lastUpdated = DateTime.Now;

        /// <summary>
        /// Last updated datetime
        /// </summary>
        public DateTime LastUpdated
        {
            get { return this.lastUpdated; }
            set { this.SetProperty<DateTime>(ref this.lastUpdated, value); }
        }

        /// <summary>
        /// Translate and speak a selected item command
        /// </summary>
        public ICommand TranslateCommand { get; set; }

        /// <summary>
        /// Launch a selected item link uri command
        /// </summary>
        public ICommand LaunchLinkUriCommand { get; set; }
    }
}
