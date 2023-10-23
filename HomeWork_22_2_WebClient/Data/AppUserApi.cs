using Newtonsoft.Json;
using System.Text;

namespace HomeWork_22_2_WebClient.Data
{
    public class AppUserApi : IAppUser
    {
        /// <summary>
        /// Список зарегистрированных в БД пользователей
        /// </summary>
        public IEnumerable<AppUser> appUsers { get;  set;}
        /// <summary>
        /// Получаемый из БД user при EditUser
        /// </summary>
        public AppUser appUser { get; set; }
        private HttpClient httpClient { get; set; }

        /// <summary>
        /// Токен JWT-аутентификации
        /// </summary>
        string TokenJWT { get; set; }

        /// <summary>
        /// Ответ при JWT-аутентификации
        /// </summary>
        HttpResponseMessage httpResponseMessage { get; set; }
        /// <summary>
        /// Выполнен вход или нет
        /// </summary>
        public bool logIn { get; set; }

        public string UserName { get; set; }

        public AppUserApi()
        {
            httpClient = new HttpClient();
        }

        public async Task LoadUsers()
        {
            HttpRequestMessage request = new()
            {
                RequestUri = new Uri("https://localhost:5001/Admin"),
                Method = HttpMethod.Post
            };
            request.Headers.Add("Authorization", "Bearer " + TokenJWT);
            var l_httpResponseMessage = await httpClient.SendAsync(request);
            if (l_httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string str2 = l_httpResponseMessage.Content.ReadAsStringAsync().Result;
                appUsers = JsonConvert.DeserializeObject<IEnumerable<AppUser>>(str2);
            }
        }
        public IEnumerable<AppUser> GetUsers()
        {
            return appUsers;
        }


        public async Task<bool> CreateUser(CreateModel model)
        {
            HttpRequestMessage request = new()
            {
                RequestUri = new Uri("https://localhost:5001/Admin/CreateUser"),
                Method = HttpMethod.Post,
                Content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, mediaType: "application/json")
            };
            request.Headers.Add("Authorization", "Bearer " + TokenJWT);
            var l_httpResponseMessage = await httpClient.SendAsync(request);
            if (l_httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            return false;
        }

        public async Task<AppUser> GetEditUser(string id)
        {
            HttpRequestMessage request = new()
            {
                RequestUri = new Uri("https://localhost:5001/Admin/GetEditUser"),
                Method = HttpMethod.Post,
                Content = new StringContent(JsonConvert.SerializeObject(id), Encoding.UTF8, mediaType: "application/json")
            };
            request.Headers.Add("Authorization", "Bearer " + TokenJWT);
            var l_httpResponseMessage = await httpClient.SendAsync(request);
            if (l_httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string str2 = l_httpResponseMessage.Content.ReadAsStringAsync().Result;
                if (string.IsNullOrEmpty(str2))
                {                    
                    throw new Exception("Нет пользователей");
                }
                appUser = JsonConvert.DeserializeObject<AppUser>(str2);
                return appUser;
            }
            throw new Exception("Нет пользователей");
        }

        public async Task EditUser(string Id, string Email, string UserName)
        {
            AppUser appUser = new()
            {
                Id = Id,
                Email = Email,
                UserName = UserName
            };
            HttpRequestMessage request = new()
            {
                RequestUri = new Uri("https://localhost:5001/Admin/EditUser"),
                Method = HttpMethod.Post,
                Content = new StringContent(JsonConvert.SerializeObject(appUser), Encoding.UTF8, mediaType: "application/json")
            };
            request.Headers.Add("Authorization", "Bearer " + TokenJWT);
            var l_httpResponseMessage = await httpClient.SendAsync(request);
            if (l_httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {                
                return;
            }
            throw new Exception("Ошибка");
        }

        public async Task DeleteUser(string id)
        {
            DeleteModel deleteModel = new(id);
            HttpRequestMessage request = new()
            {
                RequestUri = new Uri("https://localhost:5001/Admin/DeleteUser"),
                Method = HttpMethod.Post,
                Content = new StringContent(JsonConvert.SerializeObject(deleteModel), Encoding.UTF8, mediaType: "application/json")
            };
            request.Headers.Add("Authorization", "Bearer " + TokenJWT);
            var l_httpResponseMessage = await httpClient.SendAsync(request);
            if (l_httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {                
                return;
            }
            throw new Exception("Ошибка");
        }

        public bool Login(LoginModel model)
        {
            string url = @"https://localhost:5001/token";
            httpResponseMessage = httpClient.PostAsync(
                requestUri: url,
                content: new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8,
                mediaType: "application/json")
                ).Result;
            if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string str2 = httpResponseMessage.Content.ReadAsStringAsync().Result;
                ReturnToken returnToken = JsonConvert.DeserializeObject<ReturnToken>(str2);
                TokenJWT = returnToken.Token;
                logIn = true;
                UserName = model.Name;
                return true;
            }
            return false;            
        }

        public void Logout()
        {
            string url = @"https://localhost:5001/LogOut";
            _ =httpClient.PostAsync(
                requestUri: url,
                content: new StringContent("", Encoding.UTF8, mediaType: "application/text")
                ).Result;
            logIn = false;
            UserName = null;
            TokenJWT = null;
        }
        
        public string GetToken()
        {
            return TokenJWT;
        }
    }
}
