using HomeWork_22_2_WPFClient.Interfaces;
using HomeWork_22_2_WPFClient.Model;
using HomeWork_22_2_WPFClient.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_22_2_WPFClient.Data
{
    public class PhoneBookApi : IPhoneBook
    {
        private HttpClient httpClient { get; set; }

        public IEnumerable<PhoneBook> phoneBooks { get; set; }
        PhoneBook phoneBooksRecord { get; set; }

        public PhoneBookApi()
        {
            httpClient = new HttpClient();
        }

        public IEnumerable<PhoneBook> GetPhoneBook()
        {
            try
            {
                string url = @"https://localhost:5001/PhoneBook";

                string json = httpClient.GetStringAsync(url).Result;

                phoneBooks = JsonConvert.DeserializeObject<IEnumerable<PhoneBook>>(json);
            }
            catch (InvalidOperationException)
            {
                phoneBooks = new List<PhoneBook>();
                Error.NameExeption = "InvalidOperationException";
            }
            catch (HttpRequestException)
            {
                phoneBooks = new List<PhoneBook>();
                Error.NameExeption = "HttpRequestException";
            }
            catch (TaskCanceledException)
            {
                phoneBooks = new List<PhoneBook>();
                Error.NameExeption = "TaskCanceledException";
            }
            catch (Exception)
            {
                phoneBooks = new List<PhoneBook>();
                Error.NameExeption = "Exception";
            }
            return phoneBooks;
        }

        public PhoneBook GetPhoneBookId(int id)
        {
            string url = @"https://localhost:5001/PhoneBook/" + id.ToString();

            string json = httpClient.GetStringAsync(url).Result;

            phoneBooksRecord = JsonConvert.DeserializeObject<PhoneBook>(json);
            return phoneBooksRecord;
        }

        public async Task AddRecord(PhoneBook phoneBook, IAppUser appUser)
        {
            HttpRequestMessage request = new HttpRequestMessage();
            request.RequestUri = new Uri("https://localhost:5001/PhoneBook/Add");
            request.Method = HttpMethod.Post;
            request.Content = new StringContent(JsonConvert.SerializeObject(phoneBook), Encoding.UTF8, mediaType: "application/json");
            request.Headers.Add("Authorization", "Bearer " + appUser.GetToken());
            await httpClient.SendAsync(request);
        }

        public async Task EditRecord(PhoneBook phoneBook, IAppUser appUser)
        {
            HttpRequestMessage request = new HttpRequestMessage();
            request.RequestUri = new Uri("https://localhost:5001/PhoneBook");
            request.Method = HttpMethod.Post;
            request.Content = new StringContent(JsonConvert.SerializeObject(phoneBook), Encoding.UTF8, mediaType: "application/json");
            request.Headers.Add("Authorization", "Bearer " + appUser.GetToken());
            await httpClient.SendAsync(request);
        }

        public async Task DeleteRecord(int id, IAppUser appUser)
        {
            HttpRequestMessage request = new HttpRequestMessage();
            request.RequestUri = new Uri("https://localhost:5001/PhoneBook/" + id.ToString());
            request.Method = HttpMethod.Post;
            request.Content = new StringContent(id.ToString(), Encoding.UTF8, mediaType: "application/json");
            request.Headers.Add("Authorization", "Bearer " + appUser.GetToken());
            await httpClient.SendAsync(request);
        }
    }
}
