#region License
//-----------------------------------------------------------------------
// <copyright>
//     Copyright matatabi-ux 2015.
// </copyright>
//-----------------------------------------------------------------------
#endregion

using System;
using System.Net;
using System.Threading.Tasks;
using Windows.Phone.Speech.Synthesis;
using Xamarin.Forms;
using XamarinReader.Models;

namespace XamarinReader.WinPhone.Models
{
    /// <summary>
    /// Text speech service class
    /// </summary>
    public class TextSpeechService : ITextSpeechService
    {
        /// <summary>
        /// Text speech synthesizer
        /// </summary>
        private SpeechSynthesizer synthesizer;

        /// <summary>
        /// Speak language
        /// </summary>
        private string language = "en-US";

        /// <summary>
        ///     Constructor
        /// </summary>
        public TextSpeechService()
        {
            this.synthesizer = new SpeechSynthesizer();
        }

        /// <summary>
        /// Set speaking a language
        /// </summary>
        /// <param name="language">speak language name</param>
        public void SetLanguage(string language)
        {
            switch (language)
            {
                case "Japanese":
                    this.language = "ja-JP";
                    break;

                default:
                    this.language = "en-US";
                    break;
            }
        }

        /// <summary>
        /// Speak text sentence
        /// </summary>
        /// <param name="text">target text</param>
        public async void Speak(string text)
        {
            this.Stop();
            try
            {
                await this.synthesizer.SpeakSsmlAsync(
                    string.Format(@"<speak version='1.0' 
                                       xmlns='http://www.w3.org/2001/10/synthesis' 
                                       xml:lang='{0}'>
                                    <prosody rate='-0.2' volume='250'>{1}</prosody>
                                </speak>", this.language, WebUtility.HtmlEncode(text)));
            }
            catch (Exception)
            {
                // Ignore task cancelation or interrupted exception
            }
        }

        /// <summary>
        /// Stop speaking
        /// </summary>
        public void Stop()
        {
            this.synthesizer.CancelAll();
        }

        /// <summary>
        /// Dispose service
        /// </summary>
        public void Uninitialize()
        {
            this.synthesizer.Dispose();
        }
    }
}