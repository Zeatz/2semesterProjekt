using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Printing;
using Fanatic_GUI.Model;

namespace Fanatic_GUI.Persistency
{
    public class PersistencyClass
    {
        private const string ServerUri = "http://fanaticprojekt.azurewebsites.net/";
        /// <summary>
        /// metoden sender en token til webserveren, og returnere en usertable hvis det lykkedes
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static async Task<UserTable> AuthUser(string userName, string password)
        {
            using (var client = new HttpClient(GetHandler()))
            {
                try
                {
                    SetClientSettingsToJson(client);
                    // username og password bliver converteret til en base64 string med ':' imellem
                    var token = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{userName}:{password}"));
                    // laver en users request til webserveren
                    var response = await client.GetAsync("api/Users/" + token);
                    if (response.IsSuccessStatusCode)
                    {
                        // hvis det lykkedes kan vi læse indholdet som et UserTable
                        var user = await response.Content.ReadAsAsync<UserTable>();
                        return user;
                    }
                }
                catch (Exception)
                {
                    return null;
                }
            }
            return null;
        }

        /// <summary>
        /// Sender et StatisticsDTO til webserveren
        /// </summary>
        /// <param name="statsDto"></param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> PostUserReviews(StatisticsDTO statsDto)
        {
            using (var client = new HttpClient(GetHandler()))
            {
                try
                {
                    SetClientSettingsToJson(client);
                    // sender et statisticsDTO til webserveren, som den håndtere
                    var response = await client.PostAsJsonAsync("api/UserStatistics", statsDto);
                    return response;
                }
                catch (Exception e)
                {
                    return new HttpResponseMessage(HttpStatusCode.NotFound) {ReasonPhrase = e.Message};
                }
            }
        }

        /// <summary>
        /// Metoden sætter nogle indstillinger for HttpClient
        /// </summary>
        /// <param name="client"></param>
        private static void SetClientSettingsToJson(HttpClient client)
        {
            client.BaseAddress = new Uri(ServerUri);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }


        /// <summary>
        /// returnere en HttpClientHandler med default credentials
        /// </summary>
        /// <returns></returns>
        private static HttpClientHandler GetHandler()
        {
            return new HttpClientHandler() { UseDefaultCredentials = true };
        }
    }
}
