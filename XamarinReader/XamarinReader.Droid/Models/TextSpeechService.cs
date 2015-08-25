#region License
//-----------------------------------------------------------------------
// <copyright>
//     Copyright matatabi-ux 2015.
// </copyright>
//-----------------------------------------------------------------------
#endregion

using System.Threading.Tasks;
using Android.OS;
using Android.Runtime;
using Android.Speech.Tts;
using Java.Util;
using Xamarin.Forms;
using XamarinReader.Models;

namespace XamarinReader.Droid.Models
{
    /// <summary>
    /// Text speech service class
    /// </summary>
    public class TextSpeechService : Java.Lang.Object, ITextSpeechService, TextToSpeech.IOnInitListener
    {
        /// <summary>
        /// Text speech synthesizer
        /// </summary>
        private TextToSpeech synthesizer;

        /// <summary>
        /// Flag wether service is initialized or not
        /// </summary>
        private bool isInitialized;

        /// <summary>
        /// Speak language
        /// </summary>
        private Locale language = Locale.Us;

        /// <summary>
        /// Constructor
        /// </summary>
        public TextSpeechService()
        {
            this.synthesizer = new TextToSpeech(Forms.Context, this);
        }

        /// <summary>
        /// Text speech service initialized event handler
        /// </summary>
        /// <param name="status">Initialized status</param>
        public void OnInit([GeneratedEnum] OperationResult status)
        {
            if (!status.Equals(OperationResult.Success))
            {
                return;
            }
            this.isInitialized = true;
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
                    this.language = Locale.Japanese;
                    break;

                default:
                    this.language = Locale.Us;
                    break;
            }
        }

        /// <summary>
        /// Speak text sentence
        /// </summary>
        /// <param name="text">target text</param>
        public void Speak(string text)
        {
            if (!this.isInitialized)
            {
                // Ignore request
                return;
            }
            this.Stop();
            this.synthesizer.SetSpeechRate(0.8f);
            this.synthesizer.SetLanguage(this.language);

            if (((int)Build.VERSION.SdkInt) >= 21)
            {
                this.synthesizer.Speak(text, QueueMode.Flush, Bundle.Empty, UUID.RandomUUID().ToString());
            }
            else
            {
                #pragma warning disable 618

                // This method was deprecated in API level 21.
                this.synthesizer.Speak(text, QueueMode.Flush, null);
                
                #pragma warning restore 618
            }
        }

        /// <summary>
        /// Stop speaking
        /// </summary>
        public void Stop()
        {
            if (this.synthesizer.IsSpeaking)
            {
                this.synthesizer.Stop();
            }
        }

        /// <summary>
        /// Dispose service
        /// </summary>
        public void Uninitialize()
        {
            this.synthesizer.Shutdown();
        }
    }
}