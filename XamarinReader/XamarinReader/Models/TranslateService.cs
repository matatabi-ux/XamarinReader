#region License
//-----------------------------------------------------------------------
// <copyright>
//     Copyright matatabi-ux 2015.
// </copyright>
//-----------------------------------------------------------------------
#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace XamarinReader.Models
{
    /// <summary>
    /// Translate service class
    /// </summary>
    public class TranslateService : ITranslateService
    {
        /// <summary>
        /// Client ID
        /// </summary>
        private static readonly string ClientId = @""; //TODO: Specify your client ID of registered developer application in Microsoft Azure Marketplace.

        /// <summary>
        /// Client Secret
        /// </summary>
        private static readonly string ClientSecret = @""; //TODO: Specify your client secret of registered developer application in Microsoft Azure Marketplace.

        /// <summary>
        /// OAuth authentication endpoint uri
        /// </summary>
        private static readonly string OAuthUri = @"https://datamarket.accesscontrol.windows.net/v2/OAuth2-13";

        /// <summary>
        /// Translate service api endpoint uri
        /// </summary>
        private static readonly string TranslateUri = @"http://api.microsofttranslator.com/v2/Http.svc/Translate?text={0}&from=en&to=ja";

        /// <summary>
        /// Expiration datetime translate api access token
        /// </summary>
        private DateTime accessTokenExpires = DateTime.MinValue;

        /// <summary>
        /// Translate api access token
        /// </summary>
        private string accessToken = string.Empty;

        /// <summary>
        /// Initialize translator service
        /// </summary>
        public async Task InitializeAsync()
        {
            var client = new HttpClient();
            var content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"client_id", ClientId},
                {"client_secret", ClientSecret},
                {"grant_type", @"client_credentials" },
                {"scope", @"http://api.microsofttranslator.com" },
            });
            var result = await client.PostAsync(OAuthUri, content);

            var json = await result.Content.ReadAsStringAsync();

            var response = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JContainer>(json);
            var now = DateTime.Now;

            Debug.WriteLine(response);

            this.accessToken = response.Value<string>("access_token");
            var expiresIn = response.Value<long>("expires_in");
            this.accessTokenExpires = now.AddSeconds(expiresIn);
        }

        /// <summary>
        /// Translate to english from japanese
        /// </summary>
        /// <param name="english">english sentence</param>
        /// <returns>japanese sentence</returns>
        public async Task<string> Translate(string english)
        {
            if (string.IsNullOrEmpty(this.accessToken)
                || accessTokenExpires.CompareTo(DateTime.Now) < 0)
            {
                await this.InitializeAsync();
            }

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}", this.accessToken));
            var xml = await client.GetStringAsync(string.Format(TranslateUri, WebUtility.UrlEncode(english)));

            string japanese = null;
            using (var stream = new MemoryStream(Encoding.Unicode.GetBytes(xml)))
            {
                var desirializer = new DataContractSerializer(typeof(string));
                japanese = desirializer.ReadObject(stream) as string;
            }

            return japanese;
        }
    }
}
