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

        // check if user has internet
        public static async Task<bool> hasAnInternetConnection()
        {
            var current = Connectivity.NetworkAccess;

            bool checkInternet = current == NetworkAccess.Internet;
            if (!checkInternet)
            {
                await Application.Current.MainPage.DisplayAlert("Notice", "No Internet Connection. Please try again.", "OK");
            }

            return checkInternet;
        }

        // insertion of data, dynamic
        public static async Task<bool> insertData<T>(T obj, string tableName) where T : class
        {
            var json = JsonSerializer.Serialize(obj);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{baseURL}/{tableName}", content);
            return response.IsSuccessStatusCode;
        }

        // Fetch all data on specific table
        public static async Task<List<T>> fetchAllData<T>(string tableName) where T : class
        {
            var raw = await _httpClient.GetAsync($"{baseURL}/{tableName}");

            if (!raw.IsSuccessStatusCode) throw new Exception($"Table name didn't exist. Ensure that {tableName} exists from the API.");

            var json = await raw.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<List<T>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true});

            return data;
        }
    }
}
