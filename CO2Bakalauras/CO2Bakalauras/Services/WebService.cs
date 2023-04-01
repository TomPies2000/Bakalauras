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
        public async Task<Vartotojas> GetUserById(int Id)
        {
            try
            {
                var response = await _client.GetAsync($"/api/Vartotojas/GetUserByLoginId/{Id}");
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
        public async Task<Automobilis> GetCarByUsageId(int Id)
        {
            try
            {
                var response = await _client.GetAsync($"/api/Automobilis/GetCarByUsageId/{Id}");
                Automobilis auto = null;
                if (response.IsSuccessStatusCode)
                {
                    string userContents = await response.Content.ReadAsStringAsync();
                    auto = JsonConvert.DeserializeObject<Automobilis>(userContents);
                }
                return auto;
            }
            catch
            {
                throw new Exception();
            }
        }
        public async Task<Butas> GetHouseByUsageId(int Id)
        {
            try
            {
                var response = await _client.GetAsync($"/api/Butas/GetHouseByUsageId/{Id}");
                Butas butas = null;
                if (response.IsSuccessStatusCode)
                {
                    string userContents = await response.Content.ReadAsStringAsync();
                    butas = JsonConvert.DeserializeObject<Butas>(userContents);
                }
                return butas;
            }
            catch
            {
                throw new Exception();
            }
        }
        public async Task CreateUser(Vartotojas vartotojas)
        {
            string json = JsonConvert.SerializeObject(vartotojas);
            StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage message;
            message = await _client.PostAsync("/api/Vartotojas/CreateUser", stringContent);
        }
        public async Task<List<Sanaudos>> GetUserUsage(int userID)
        {
            var response = await _client.GetAsync($"/api/Sanaudos/GetUsageByUserID/{userID}");
            List<Sanaudos> sanaudos = null;
            if (response.IsSuccessStatusCode)
            {
                string userContents = await response.Content.ReadAsStringAsync();
                sanaudos = JsonConvert.DeserializeObject <List<Sanaudos>>(userContents);
            }
            return sanaudos;
        }
        public async Task CreateUsage(Sanaudos sanaudos)
        {
            string json = JsonConvert.SerializeObject(sanaudos);
            StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage message;
            message = await _client.PostAsync("/api/Sanaudos/CreateUsage", stringContent);
        }
        public async Task CreateCar(Automobilis auto)
        {
            string json = JsonConvert.SerializeObject(auto);
            StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage message;
            message = await _client.PostAsync("/api/Automobilis/CreateCar", stringContent);
        }
        
        public async Task CreateHouse(Butas butas)
        {
            string json = JsonConvert.SerializeObject(butas);
            StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage message;
            message = await _client.PostAsync("/api/Butas/CreateHouse", stringContent);
        }

        public async Task<List<CO2>> GetCo2()
        {
            var response = await _client.GetAsync($"/api/Co2/GetCo2");
            List<CO2> co2 = null;
            if (response.IsSuccessStatusCode)
            {
                string userContents = await response.Content.ReadAsStringAsync();
                co2 = JsonConvert.DeserializeObject<List<CO2>>(userContents);
            }
            return co2;
        }

        public async Task UpdateUsage (Sanaudos sanaudos)
        {
            string json = JsonConvert.SerializeObject (sanaudos);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage message;
            try
            {
                string uri = "/api/Sanaudos/UpdateUsage";
                message = await _client.PutAsync(uri, content);
            }
            catch
            {
                throw new HttpRequestException();
            }
        }
    }
}
