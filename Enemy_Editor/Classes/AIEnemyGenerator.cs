using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Drawing;
using System.IO;
using System.Windows;

namespace Enemy_Editor.Classes
{
    class AIEnemyGenerator
    {
        private static readonly HttpClient client = new();
        private readonly string? OllamaApiUrl = ConfigurationManager.AppSettings["OllamaEndpoints"];
        private readonly string? StableOllamaUrl = ConfigurationManager.AppSettings["StableEndpoints"];

        public async Task<string> GenerateEnemy()
        {
            var enemyJsonData = await GetEnemyDataAsync(ConfigurationManager.AppSettings["systemPrompt"]);
            MessageBox.Show(enemyJsonData);



            var enemyDescripData = await GetEnemyDataAsync(ConfigurationManager.AppSettings["descriptionPrompt"],enemyJsonData);
            MessageBox.Show(enemyDescripData);

            var enemyIcon = await GetEnemyIconAsync(enemyDescripData);

            return enemyJsonData;
        }

        public async Task<string> GetEnemyIconAsync(string prompt)
        {
            var requestData = new
            {
                prompt = prompt,
                width = 512,
                height = 512,
                steps = 25
            };

            var jsonContent = JsonConvert.SerializeObject(requestData);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage response = await client.PostAsync(StableOllamaUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var jsonResponse = JsonConvert.DeserializeObject<dynamic>(responseBody);

                    // Получение строки Base64 из первого элемента массива images
                    string firstImageBase64 = jsonResponse.images[0];

                    // Преобразование строки Base64 в массив байтов
                    byte[] imageBytes = Convert.FromBase64String(firstImageBase64);

                    // Создание изображения из массива байтов
                    using (MemoryStream ms = new MemoryStream(imageBytes))
                    {
                        Image image = Image.FromStream(ms);

                        // Сохранение изображения в файл (например, PNG)
                        image.Save("output.png", System.Drawing.Imaging.ImageFormat.Png);
                        //Console.WriteLine("Изображение сохранено как output.png");
                    }

                    return jsonResponse.response;
                }
                else
                {
                    return $"Ошибка: {response.StatusCode}, {response.ReasonPhrase}";
                }
            }
            catch (HttpRequestException ex)
            {
                return $"Исключение: {ex.Message}";
            }
        }

        public async Task<string> GetEnemyDataAsync(string? prompt, string additionalPrompt = "", string modelName = "gemma2")
        {
            var requestData = new
            {
                model = modelName,
                prompt = $"{prompt}\n{additionalPrompt}",
                stream = false
            };

            var jsonContent = JsonConvert.SerializeObject(requestData);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage response = await client.PostAsync(OllamaApiUrl, content);

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
            catch (HttpRequestException ex)
            {
                return $"Исключение: {ex.Message}";
            }
        }
    }
}
