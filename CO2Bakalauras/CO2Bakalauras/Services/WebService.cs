using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using CO2Bakalauras.Models;
using Newtonsoft.Json;

namespace CO2Bakalauras.Services
{
    public class WebService
    {
        private HttpClient _client;
        public WebService() 
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://co2bakalaurasapi.azurewebsites.net");
            _client.MaxResponseContentBufferSize = 256000;
        }
        public async Task<Vartotojas> GetUserByLogin(string login, string psw) 
        {
            try
            {
                var response = await _client.GetAsync($"/api/Vartotojas/GetUserByLogin/{login}/{psw}");
                Vartotojas vartotojas = null;
                if (response.IsSuccessStatusCode)
                {
                    string userContents = await response.Content.ReadAsStringAsync();
                    vartotojas = JsonConvert.DeserializeObject<Vartotojas>(userContents);
                }
                return vartotojas;
            }
            catch
            {
                throw new Exception();
            }
        }
    }
}
