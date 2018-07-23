using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TodoServices
{
    public class GoogleBooksAPIService
    {
        public static async Task<(List<Book> books, string errorMessage)> Request(string bookName)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.Timeout = TimeSpan.FromSeconds(5);
                var jsonResult = await client.GetStringAsync("https://www.googleapis.com/books/v1/volumes?q=" + bookName);
                BookResult result = JsonConvert.DeserializeObject<BookResult>(jsonResult);
                if(result.items != null && result.items.Count > 0)
                    return (result.items, "");
            }
            catch(Exception exp)
            {
                return (new List<Book>(), exp.Message);
            }
            return (new List<Book>(), "Keine Treffer für diesen Suchbegriff!");
        }
    }
}
