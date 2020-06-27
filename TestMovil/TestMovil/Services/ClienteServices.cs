using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TestMovil.Models;

namespace TestMovil.Services
{
    public class ClienteServices : IDataStore<Cliente>
    {
        public async Task<bool> DeleteItemAsync(int id)
        {
            string url = $"api/clientes/{id}";
            using (HttpResponseMessage response = await ApiClient.httpClient.DeleteAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                    return false;
            }
        }

        public async Task<Cliente> GetItemAsync(int id)
        {
            string url = $"api/clientes/{id}";
            using (HttpResponseMessage response = await ApiClient.httpClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<Cliente>();
                }
                else
                    return new Cliente();
            }
        }

        public async Task<IEnumerable<Cliente>> GetItemsAsync(bool forceRefresh = false)
        {
            string url = $"api/clientes";
            using (HttpResponseMessage response = await ApiClient.httpClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<List<Cliente>>();
                }
                else
                    return new List<Cliente>();
            }
        }

        public async Task<bool> UpdateItemAsync(Cliente item)
        {
            string url = $"api/clientes";
            using (HttpResponseMessage response = await ApiClient.httpClient.PutAsJsonAsync(url, item))
            {
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                    return false;
            }
        }

        public async Task<int> AddItemAsync(Cliente item)
        {
            string url = $"api/clientes";
            var json = JsonConvert.SerializeObject(item);
            using (HttpResponseMessage response = await ApiClient.httpClient.PostAsJsonAsync(url, item))
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<int>();
                }
                else
                    return 0;
            }
        }
    }
}
