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
using Prism.Mvvm;
using System.Windows.Input;

namespace XamarinReader.ViewModels
{
    /// <summary>
    /// TopPage ViewModel
    /// </summary>
    public class TopPageViewModel : BindableBase
    {
        /// <summary>
        /// News feed items collection
        /// </summary>
        public ObservableCollection<NewsItemViewModel> Items { get; set; }

        /// <summary>
        /// Flag wheter refresing data or not 
        /// </summary>
        private bool isRefresing = false;

        /// <summary>
        /// Flag wheter refresing data or not 
        /// </summary>
        public bool IsRefresing
        {
            get { return this.isRefresing; }
            set { this.SetProperty<bool>(ref this.isRefresing, value); }
        }

        /// <summary>
        /// Refresh data command
        /// </summary>
        public ICommand RefreshCommand { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public TopPageViewModel()
        {
            this.Items = new ObservableCollection<NewsItemViewModel>();
        }
    }
}
