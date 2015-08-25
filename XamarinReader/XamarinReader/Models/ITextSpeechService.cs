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
    /// Text speech service class interface
    /// </summary>
    public interface ITextSpeechService
    {
        /// <summary>
        /// Set speaking a language
        /// </summary>
        /// <param name="language">speak language name</param>
        void SetLanguage(string language);

        /// <summary>
        /// Speak text sentence
        /// </summary>
        /// <param name="text">target text</param>
        void Speak(string text);

        /// <summary>
        /// Stop speaking
        /// </summary>
        void Stop();

        /// <summary>
        /// Dispose service
        /// </summary>
        void Uninitialize();
    }
}
