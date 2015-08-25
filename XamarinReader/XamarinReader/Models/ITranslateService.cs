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
    /// Translate service class interface
    /// </summary>
    public interface ITranslateService
    {
        /// <summary>
        /// Initialize translator service
        /// </summary>
        Task InitializeAsync();

        /// <summary>
        /// Translate to english from japanese
        /// </summary>
        /// <param name="english">english sentence</param>
        /// <returns>japanese sentence</returns>
        Task<string> Translate(string english);
    }
}
