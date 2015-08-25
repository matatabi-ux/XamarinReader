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
using System.Threading.Tasks;

namespace XamarinReader.Models
{
    /// <summary>
    /// News feed management service class interface
    /// </summary>
    public interface INewsFeedService
    {
        /// <summary>
        /// Feed items
        /// </summary>
        Rss Feed { get; set; }

        /// <summary>
        /// Update feeds
        /// </summary>
        /// <returns>Task</returns>
        Task Update();
    }
}
