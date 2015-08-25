#region License
//-----------------------------------------------------------------------
// <copyright>
//     Copyright matatabi-ux 2015.
// </copyright>
//-----------------------------------------------------------------------
#endregion

using AVFoundation;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinReader.Models;

namespace XamarinReader.iOS.Models
{
    /// <summary>
    /// Text speech service class
    /// </summary>
    public class TextSpeechService : ITextSpeechService
    {
        /// <summary>
        /// Text speech synthesizer
        /// </summary>
        private AVSpeechSynthesizer synthesizer;

        /// <summary>
        /// Speak language
        /// </summary>
        private AVSpeechSynthesisVoice language = AVSpeechSynthesisVoice.FromLanguage("en-US");

        /// <summary>
        ///     Constructor
        /// </summary>
        public TextSpeechService()
        {
            this.synthesizer = new AVSpeechSynthesizer();
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
                    this.language = AVSpeechSynthesisVoice.FromLanguage("ja-JP");
                    break;

                default:
                    this.language = AVSpeechSynthesisVoice.FromLanguage("en-US");
                    break;
            }
        }

        /// <summary>
        /// Speak text sentence
        /// </summary>
        /// <param name="text">target text</param>
        /// <returns>Task</returns>
        public void Speak(string text)
        {
            this.Stop();
            this.synthesizer.SpeakUtterance(
                new AVSpeechUtterance(text)
                {
                    Rate = AVSpeechUtterance.MaximumSpeechRate * 0.1f,
                    Voice = this.language,
                    Volume = 0.5f,
                    PitchMultiplier = 1.0f
                });
        }

        /// <summary>
        /// Stop speaking
        /// </summary>
        public void Stop()
        {
            if (this.synthesizer.Speaking)
            {
                this.synthesizer.StopSpeaking(AVSpeechBoundary.Immediate);
            }
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