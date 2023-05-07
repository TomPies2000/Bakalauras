using System;
using System.Collections.Generic;
using System.IO;
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
        private readonly HttpClient _client;
        public WebService() 
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri("https://co2bakalaurasapi.azurewebsites.net"),
                MaxResponseContentBufferSize = 256000
            };
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

        public async Task<Draugauja> GetNewFriendByID(int ID, int ID2)
        {
            try
            {
                var response = await _client.GetAsync($"/api/Draugauja/GetNewFriendByID/{ID}/{ID2}");
                Draugauja draugauja = null;
                if (response.IsSuccessStatusCode)
                {
                    string userContents = await response.Content.ReadAsStringAsync();
                    draugauja = JsonConvert.DeserializeObject<Draugauja>(userContents);
                }
                return draugauja;
            }
            catch
            {
                throw new Exception();
            }
        }
        //
        public async Task<List<Draugauja>> GetFriendRequestByID(int ID)
        {
            try
            {
                var response = await _client.GetAsync($"/api/Draugauja/GetFriendRequestByID/{ID}");
                List<Draugauja> draugauja = null;
                if (response.IsSuccessStatusCode)
                {
                    string userContents = await response.Content.ReadAsStringAsync();
                    draugauja = JsonConvert.DeserializeObject<List<Draugauja>>(userContents);
                }
                return draugauja;
            }
            catch
            {
                throw new Exception();
            }
        }

        public async Task<Vartotojas> GetUserByName(string login)
        {
            try
            {
                var response = await _client.GetAsync($"/api/Vartotojas/GetUserByName/{login}");
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

        public async Task<Vartotojas> GetUserByID(int Id)
        {
            try
            {
                var response = await _client.GetAsync($"/api/Vartotojas/GetUserByID/{Id}");
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
        
        public async Task<Uzduotis> GetTaskByID(int Id)
        {
            try
            {
                var response = await _client.GetAsync($"/api/Uzduotis/GetTaskByID/{Id}");
                Uzduotis uzduotis = null;
                if (response.IsSuccessStatusCode)
                {
                    string userContents = await response.Content.ReadAsStringAsync();
                    uzduotis = JsonConvert.DeserializeObject<Uzduotis>(userContents);
                }
                return uzduotis;
            }
            catch
            {
                throw new Exception();
            }
        }

        public async Task<Administratorius> GetAdminByUserID(int Id)
        {
            try
            {
                var response = await _client.GetAsync($"/api/Administratorius/GetAdminByUserID/{Id}");
                Administratorius admin = null;
                if (response.IsSuccessStatusCode)
                {
                    string userContents = await response.Content.ReadAsStringAsync();
                    admin = JsonConvert.DeserializeObject<Administratorius>(userContents);
                }
                return admin;
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

        public async Task<List<Statistika>> GetStatisticByUserId(int Id)
        {
            try
            {
                var response = await _client.GetAsync($"/api/Statistika/GetStatisticByUserId/{Id}");
                List<Statistika> statistika = null;
                if (response.IsSuccessStatusCode)
                {
                    string userContents = await response.Content.ReadAsStringAsync();
                    statistika = JsonConvert.DeserializeObject<List<Statistika>>(userContents);
                }
                return statistika;
            }
            catch
            {
                throw new Exception();
            }
        }
        
        public async Task<List<Draugauja>> GetFriendByID(int Id)
        {
            try
            {
                var response = await _client.GetAsync($"/api/Draugauja/GetFriendByID/{Id}");
                List<Draugauja> draugas = null;
                if (response.IsSuccessStatusCode)
                {
                    string userContents = await response.Content.ReadAsStringAsync();
                    draugas = JsonConvert.DeserializeObject<List<Draugauja>>(userContents);
                }
                return draugas;
            }
            catch
            {
                throw new Exception();
            }
        }

        public async Task<List<Vartotojas>> GetUsers()
        {
            try
            {
                var response = await _client.GetAsync($"/api/Vartotojas/GetUsers/");
                List<Vartotojas> vartotojas = null;
                if (response.IsSuccessStatusCode)
                {
                    string userContents = await response.Content.ReadAsStringAsync();
                    vartotojas = JsonConvert.DeserializeObject<List<Vartotojas>>(userContents);
                }
                return vartotojas;
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
            await _client.PostAsync("/api/Vartotojas/CreateUser", stringContent);
        }
        public async Task CreateStatistic(Statistika statistika)
        {
            string json = JsonConvert.SerializeObject(statistika);
            StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            await _client.PostAsync("/api/Statistika/CreateStatistic", stringContent);
        }
        public async Task CreateTask(Uzduotis uzduotis)
        {
            string json = JsonConvert.SerializeObject(uzduotis);
            StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            await _client.PostAsync("/api/Uzduotis/CreateTask", stringContent);
        }

         
        public async Task ExecuteTask(Atlieka atlieka)
        {
            string json = JsonConvert.SerializeObject(atlieka);
            StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            await _client.PostAsync("/api/Atlieka/ExecuteTask", stringContent);
        }

        public async Task AddFriend(Draugauja draugauja)
        {
            string json = JsonConvert.SerializeObject(draugauja);
            StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            await _client.PostAsync("/api/Draugauja/AddFriend", stringContent);
        }


        public async Task<List<Atlieka>> GetExecutedTasksByUserID(int userID)
        {
            var response = await _client.GetAsync($"/api/Atlieka/GetExecutedTasksByUserID/{userID}");
            List<Atlieka> atlieka = null;
            if (response.IsSuccessStatusCode)
            {
                string userContents = await response.Content.ReadAsStringAsync();
                atlieka = JsonConvert.DeserializeObject<List<Atlieka>>(userContents);
            }
            return atlieka;
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

         
        public async Task<List<Uzduotis>> GetTask()
        {
            var response = await _client.GetAsync($"/api/Uzduotis/GetTask");
            List<Uzduotis> uzduotis = null;
            if (response.IsSuccessStatusCode)
            {
                string userContents = await response.Content.ReadAsStringAsync();
                uzduotis = JsonConvert.DeserializeObject<List<Uzduotis>>(userContents);
            }
            return uzduotis;
        }
        public async Task CreateUsage(Sanaudos sanaudos)
        {
            string json = JsonConvert.SerializeObject(sanaudos);
            StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            await _client.PostAsync("/api/Sanaudos/CreateUsage", stringContent);
        }
        public async Task CreateCar(Automobilis auto)
        {
            string json = JsonConvert.SerializeObject(auto);
            StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            await _client.PostAsync("/api/Automobilis/CreateCar", stringContent);
        }
        
        public async Task CreateHouse(Butas butas)
        {
            string json = JsonConvert.SerializeObject(butas);
            StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/Butas/CreateHouse", stringContent);
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
            try
            {
                string uri = "/api/Sanaudos/UpdateUsage";
                await _client.PutAsync(uri, content);
            }
            catch
            {
                throw new HttpRequestException();
            }
        }
        public async Task UpdateTask(Uzduotis uzduotis)
        {
            string json = JsonConvert.SerializeObject(uzduotis);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            try
            {
                string uri = "/api/Uzduotis/UpdateTask";
                await _client.PutAsync(uri, content);
            }
            catch
            {
                throw new HttpRequestException();
            }
        }
        public async Task UpdateUserProfile(Vartotojas vartotojas)
        {
            string json = JsonConvert.SerializeObject(vartotojas);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            try
            {
                string uri = "/api/Vartotojas/UpdateUserProfile";
                await _client.PutAsync(uri, content);
            }
            catch
            {
                throw new HttpRequestException();
            }
        }

        public async Task UpdateStatistic(Statistika statistika)
        {
            string json = JsonConvert.SerializeObject(statistika);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            try
            {
                string uri = "/api/Statistika/UpdateStatistic";
                var response = await _client.PutAsync(uri, content);
            }
            catch
            {
                throw new HttpRequestException();
            }
        }
        
        public async Task UpdateDraugauja(Draugauja draugauja)
        {
            string json = JsonConvert.SerializeObject(draugauja);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            try
            {
                string uri = "/api/Draugauja/UpdateDraugauja";
                var response = await _client.PutAsync(uri, content);
            }
            catch
            {
                throw new HttpRequestException();
            }
        }

        public async Task DeleteFriend(Draugauja draugauja)
        {
            try
            {
                string uri = $"/api/Draugauja/DeleteFriend/{draugauja.PIRMO_DRAUGO_ID}/{draugauja.ANTRO_DRAUGO_ID}";
                HttpResponseMessage response = await _client.DeleteAsync(uri);
            }
            catch 
            { 
                throw new HttpRequestException(); 
            }
        }

        public async Task DeclineRequest(Draugauja draugauja)
        {
            try
            {
                string uri = $"/api/Draugauja/DeclineRequest/{draugauja.PIRMO_DRAUGO_ID}/{draugauja.ANTRO_DRAUGO_ID}";
                HttpResponseMessage response = await _client.DeleteAsync(uri);
            }
            catch
            {
                throw new HttpRequestException();
            }
        }

        public async Task DeleteTaskByID(int Id)
        {
            try
            {
                string uri = $"/api/Uzduotis/DeleteTaskByID/{Id}";
                HttpResponseMessage response = await _client.DeleteAsync(uri);
            }
            catch
            {
                throw new HttpRequestException();
            }
        }

        public async Task DeleteHouseByUsageID(int Id)
        {
            try
            {
                string uri = $"/api/Butas/DeleteHouseByUsageID/{Id}";
                HttpResponseMessage response = await _client.DeleteAsync(uri);
            }
            catch
            {
                throw new HttpRequestException();
            }
        }
        public async Task DeleteCarByUsageID(int Id)
        {
            try
            {
                string uri = $"/api/Automobilis/DeleteCarByUsageID/{Id}";
                HttpResponseMessage response = await _client.DeleteAsync(uri);
            }
            catch
            {
                throw new HttpRequestException();
            }
        }
        public async Task DeleteExecutedTasksByUserID(int Id)
        {
            try
            {
                string uri = $"/api/Atlieka/DeleteExecutedTasksByUserID/{Id}";
                HttpResponseMessage response = await _client.DeleteAsync(uri);
            }
            catch
            {
                throw new HttpRequestException();
            }
        }

        public async Task DeleteExecutedTasksByTaskID(int Id)
        {
            try
            {
                string uri = $"/api/Atlieka/DeleteExecutedTasksByTaskID/{Id}";
                HttpResponseMessage response = await _client.DeleteAsync(uri);
            }
            catch
            {
                throw new HttpRequestException();
            }
        }
        public async Task DeleteAllUserFriendships(int Id)
        {
            try
            {
                string uri = $"/api/Draugauja/DeleteAllUserFriendships/{Id}";
                HttpResponseMessage response = await _client.DeleteAsync(uri);
            }
            catch
            {
                throw new HttpRequestException();
            }
        }
        public async Task DeleteUsageByUserID(int Id)
        {
            try
            {
                string uri = $"/api/Sanaudos/DeleteUsageByUserID/{Id}";
                HttpResponseMessage response = await _client.DeleteAsync(uri);
            }
            catch
            {
                throw new HttpRequestException();
            }
        }

        public async Task DeleteStatisticByUserID(int Id)
        {
            try
            {
                string uri = $"/api/Statistika/DeleteStatisticByUserID/{Id}";
                HttpResponseMessage response = await _client.DeleteAsync(uri);
            }
            catch
            {
                throw new HttpRequestException();
            }
        }

        public async Task DeleteUser(int Id)
        {
            try
            {
                string uri = $"/api/Vartotojas/DeleteUser/{Id}";
                HttpResponseMessage response = await _client.DeleteAsync(uri);
            }
            catch
            {
                throw new HttpRequestException();
            }
        }
    }
}
