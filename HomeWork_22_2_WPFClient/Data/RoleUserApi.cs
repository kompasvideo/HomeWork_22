using HomeWork_22_2_WPFClient.Interfaces;
using HomeWork_22_2_WPFClient.Models;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_22_2_WPFClient.Data
{
    public class RoleUserApi : IRoleUser
    {
        private HttpClient httpClient { get; set; }
        IEnumerable<IdentityRole> roleUsers;
        IEnumerable<string> names;
        public RoleUserApi()
        {
            httpClient = new HttpClient();
        }

        public async Task<IEnumerable<IdentityRole>> GetRoles(IAppUser appUser)
        {
            if (appUser.logIn)
            {
                HttpRequestMessage request = new HttpRequestMessage();
                request.RequestUri = new Uri("https://localhost:5001/Role");
                request.Method = HttpMethod.Post;
                request.Content = new StringContent(JsonConvert.SerializeObject("1"), Encoding.UTF8, mediaType: "application/json");
                request.Headers.Add("Authorization", "Bearer " + appUser.GetToken());
                var l_httpResponseMessage = await httpClient.SendAsync(request);
                if (l_httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string str2 = l_httpResponseMessage.Content.ReadAsStringAsync().Result;
                    roleUsers = JsonConvert.DeserializeObject<IEnumerable<IdentityRole>>(str2);
                    return roleUsers;
                }
            }
            throw new Exception("Ошибка");
        }

        public async Task<List<string>> FindByIdAsync(string Role, IAppUser appUser)
        {
            if (appUser.logIn)
            {
                HttpRequestMessage request = new HttpRequestMessage();
                request.RequestUri = new Uri("https://localhost:5001/GetRole");
                request.Method = HttpMethod.Post;
                request.Content = new StringContent(JsonConvert.SerializeObject(Role), Encoding.UTF8, mediaType: "application/json");
                request.Headers.Add("Authorization", "Bearer " + appUser.GetToken());
                var l_httpResponseMessage = await httpClient.SendAsync(request);
                if (l_httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string str2 = l_httpResponseMessage.Content.ReadAsStringAsync().Result;
                    names = JsonConvert.DeserializeObject<List<string>>(str2);
                    return names.ToList();
                }
            }
            throw new Exception("Ошибка");
        }        

        public async Task<RoleEditModel> GetEditRole(string id, IAppUser appUser)
        {
            HttpRequestMessage request = new HttpRequestMessage();
            request.RequestUri = new Uri("https://localhost:5001/GetEditRole");
            request.Method = HttpMethod.Post;
            request.Content = new StringContent(JsonConvert.SerializeObject(id), Encoding.UTF8, mediaType: "application/json");
            request.Headers.Add("Authorization", "Bearer " + appUser.GetToken());
            var l_httpResponseMessage = await httpClient.SendAsync(request);
            if (l_httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string str2 = l_httpResponseMessage.Content.ReadAsStringAsync().Result;
                if (string.IsNullOrEmpty(str2))
                {
                    throw new Exception("Нет пользователей");
                }
                RoleEditModel roleEditModel = JsonConvert.DeserializeObject<RoleEditModel>(str2);
                return roleEditModel;
            }
            throw new Exception("Нет пользователей");
        }

        public async Task<bool> SetEditRole(RoleModificationModel model, IAppUser appUser)
        {
            HttpRequestMessage request = new HttpRequestMessage();
            request.RequestUri = new Uri("https://localhost:5001/SetEditRole");
            request.Method = HttpMethod.Post;
            request.Content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, mediaType: "application/json");
            request.Headers.Add("Authorization", "Bearer " + appUser.GetToken());
            var l_httpResponseMessage = await httpClient.SendAsync(request);
            if (l_httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string str2 = l_httpResponseMessage.Content.ReadAsStringAsync().Result;
                if (string.IsNullOrEmpty(str2))
                {
                    throw new Exception("Ошибка");
                }
                bool result = JsonConvert.DeserializeObject<bool>(str2);
                return result;
            }
            throw new Exception("Ошибка");
        }

        public async Task<bool> DeleteRole(string id, IAppUser appUser)
        {
            HttpRequestMessage request = new HttpRequestMessage();
            request.RequestUri = new Uri("https://localhost:5001/DeleteRole");
            request.Method = HttpMethod.Post;
            request.Content = new StringContent(JsonConvert.SerializeObject(id), Encoding.UTF8, mediaType: "application/json");
            request.Headers.Add("Authorization", "Bearer " + appUser.GetToken());
            var l_httpResponseMessage = await httpClient.SendAsync(request);
            if (l_httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string str2 = l_httpResponseMessage.Content.ReadAsStringAsync().Result;
                if (string.IsNullOrEmpty(str2))
                {
                    throw new Exception("Ошибка");
                }
                bool result = JsonConvert.DeserializeObject<bool>(str2);
                return result;
            }
            throw new Exception("Ошибка");
        }

        public async Task<bool> CreateRole(string name, IAppUser appUser)
        {
            HttpRequestMessage request = new HttpRequestMessage();
            request.RequestUri = new Uri("https://localhost:5001/CreateRole");
            request.Method = HttpMethod.Post;
            request.Content = new StringContent(JsonConvert.SerializeObject(name), Encoding.UTF8, mediaType: "application/json");
            request.Headers.Add("Authorization", "Bearer " + appUser.GetToken());
            var l_httpResponseMessage = await httpClient.SendAsync(request);
            if (l_httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string str2 = l_httpResponseMessage.Content.ReadAsStringAsync().Result;
                if (string.IsNullOrEmpty(str2))
                {
                    throw new Exception("Ошибка");
                }
                bool result = JsonConvert.DeserializeObject<bool>(str2);
                return result;
            }
            throw new Exception("Ошибка");
        }
    }
}
