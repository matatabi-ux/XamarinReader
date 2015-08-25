#region License
//-----------------------------------------------------------------------
// <copyright>
//     Copyright matatabi-ux 2015.
// </copyright>
//-----------------------------------------------------------------------
#endregion

using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinReader.Models;
using XamarinReader.ViewModels;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace XamarinReader.Views
{
    /// <summary>
    /// Top page
    /// </summary>
    public partial class TopPage : ContentPage
    {
        /// <summary>
        /// Remove html tag regex
        /// </summary>
        private static readonly Regex HtmlTagRegex = new Regex(@"<.*?>", RegexOptions.Singleline);

        /// <summary>
        /// Remove last read more paragraph regex
        /// </summary>
        private static readonly Regex ReadMoreRegex = new Regex(@"Read\s*?More[\r\n\s][\r\n\s]*", RegexOptions.Singleline);

        /// <summary>
        /// Get image html tag src attribute regex
        /// </summary>
        private static readonly Regex ImgTagRegex = new Regex(@"<img.*?src\s*=\s*[""'](?<uri>.+?)[""'].*?>", RegexOptions.Singleline);

        /// <summary>
        /// News feed retrieve service
        /// </summary>
        private INewsFeedService newsFeed = null;

        /// <summary>
        /// Text speech service 
        /// </summary>
        private ITextSpeechService speech = null;

        /// <summary>
        /// Translate service
        /// </summary>
        private ITranslateService translate = null;

        /// <summary>
        /// ViewModel
        /// </summary>
        public TopPageViewModel ViewModel { get; private set; }

        /// <summary>
        /// Constructir
        /// </summary>
        public TopPage()
        {
            this.ViewModel = new TopPageViewModel();
            this.ViewModel.RefreshCommand = DelegateCommand.FromAsyncHandler(this.Refresh);
            this.newsFeed = ((App)App.Current).Container.Resolve<INewsFeedService>();
            this.translate = ((App)App.Current).Container.Resolve<ITranslateService>();

            this.InitializeComponent();
        }

        /// <summary>
        /// Appearing event handler
        /// </summary>
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            this.speech = ((App)App.Current).Container.Resolve<ITextSpeechService>();
            await this.translate.InitializeAsync();
            await this.Refresh();
        }

        /// <summary>
        /// Refresh news feed items
        /// </summary>
        /// <returns>Task</returns>
        private async Task Refresh()
        {
            this.ViewModel.IsRefresing = true;

            await this.newsFeed.Update();

            // Add items on UI thread
            foreach (var item in this.newsFeed.Feed.Channel.Items.OrderBy(i => i.PubDate))
            {
                var vm = new NewsItemViewModel
                {
                    UniqueId = item.Guid,
                    Categories = new ObservableCollection<string>(item.Categories),
                    Title = item.Title,
                    Link = item.Link,
                    LastUpdated = item.PubDate,
                    Description = ReadMoreRegex.Replace(
                                    WebUtility.HtmlDecode(
                                    HtmlTagRegex.Replace(item.Description, string.Empty)),
                                    string.Empty),
                    LaunchLinkUriCommand = new DelegateCommand<NewsItemViewModel>(this.LaunchLinkUri),
                    TranslateCommand = DelegateCommand<NewsItemViewModel>.FromAsyncHandler(this.Translate),
                };
                var imgMatch = ImgTagRegex.Match(item.Description);
                if (imgMatch.Success)
                {
                    vm.Thumbnail = ImageSource.FromUri(new Uri(imgMatch.Groups["uri"].Value));
                }

                var oldItem = this.ViewModel.Items.FirstOrDefault(i => i.UniqueId.Equals(vm.UniqueId));
                if (oldItem != null)
                {
                    if (oldItem.LastUpdated.CompareTo(vm.LastUpdated) >= 0)
                    {
                        // no update
                        continue;
                    }

                    // updated
                    oldItem.Categories = vm.Categories;
                    oldItem.Title = vm.Title;
                    oldItem.Link = vm.Link;
                    oldItem.LastUpdated = vm.LastUpdated;
                    oldItem.Description = vm.Description;
                    oldItem.Thumbnail = vm.Thumbnail;
                    continue;
                }

                // new item
                this.ViewModel.Items.Insert(0, vm);
            }

            this.ViewModel.IsRefresing = false;
        }

        /// <summary>
        /// ListView item selected event handler
        /// </summary>
        /// <param name="sender">Event publisher</param>
        /// <param name="e">Event arguments</param>
        private void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var listView = sender as ListView;
            var item = e.SelectedItem as NewsItemViewModel;
            if (listView == null || item == null)
            {
                return;
            }
            listView.SelectedItem = null;

            this.speech.SetLanguage("English");
            this.speech.Speak(string.Format("{0}: {1}", item.Title, item.Description));
        }

        /// <summary>
        /// Launch a selected item link uri
        /// </summary>
        /// <param name="item">Select item</param>
        private void LaunchLinkUri(NewsItemViewModel item)
        {
            Device.OpenUri(new Uri(item.Link));
        }

        /// <summary>
        /// Translate to english from japanese and speak
        /// </summary>
        /// <param name="item">Select item</param>
        /// <returns>Task</returns>
        private async Task Translate(NewsItemViewModel item)
        {
            var japanse = await this.translate.Translate(string.Format("{0}: {1}", item.Title, item.Description));
            this.speech.SetLanguage("Japanese");
            this.speech.Speak(japanse);
        }
    }
}
