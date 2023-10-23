using Newtonsoft.Json;
using System.Text;

namespace HomeWork_22_2_WebClient.Data
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
            string url = @"https://localhost:5001/PhoneBook";

            string json = httpClient.GetStringAsync(url).Result;

            phoneBooks = JsonConvert.DeserializeObject<IEnumerable<PhoneBook>>(json);
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
