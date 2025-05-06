using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Luna.Models
{
    // To have a unified API, accessible to all
    class MockAPI
    {
        static readonly string baseURL = "https://681934661ac115563504382f.mockapi.io/PROEL03/";
        static HttpClient _httpClient = new HttpClient();

        // insertion of data, dynamic
        public static async Task<bool> insertData<T>(T obj, string tableName) where T : class
        {
            var json = JsonSerializer.Serialize(obj);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{baseURL}/{tableName}", content);
            return response.IsSuccessStatusCode;
        }
    }
}
