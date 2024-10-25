using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Enemy_Editor.Classes
{
    class AIEnemyGenerator
    {
        private static readonly HttpClient client = new();
        private readonly string? apiUrl = ConfigurationManager.AppSettings["endpoint"];

        public async Task<string> GetOllamaResponseAsync(string prompt, string modelName)
        {
            var requestData = new
            {
                model = modelName,
                prompt = ConfigurationManager.AppSettings["systemPrompt"],
                stream = false
            };

            var jsonContent = JsonConvert.SerializeObject(requestData);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var jsonResponse = JsonConvert.DeserializeObject<dynamic>(responseBody);
                    return jsonResponse.response;
                }
                else
                {
                    return $"Ошибка: {response.StatusCode}, {response.ReasonPhrase}";
                }
            }
            catch (Exception ex)
            {
                return $"Исключение: {ex.Message}";
            }
        }
    }
}
