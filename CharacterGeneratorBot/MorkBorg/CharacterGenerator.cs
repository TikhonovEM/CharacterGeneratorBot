using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace CharacterGeneratorBot.MorkBorg
{
    public static class CharacterGenerator
    {

        private readonly static JsonSerializerOptions _serializerOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public static async Task<GenerationResult> Generate(CancellationToken cancellationToken)
        {
            var result = new GenerationResult();
            using var httpClient = new HttpClient();
            try
            {
                var url = Configuration.ConfigurationProvider.Configuration.GetSection("MorkBorgUrl").Value;
                var request = new HttpRequestMessage(HttpMethod.Get, url)
                {
                    Content = JsonContent.Create(new CharacterGenerationParams(), options: _serializerOptions)
                };
                var response = await httpClient.SendAsync(request, cancellationToken);
                if (response?.IsSuccessStatusCode == true)
                {
                    var content = await response.Content.ReadAsStringAsync(cancellationToken);
                    try
                    {
                        var character = JsonSerializer.Deserialize<Character>(content, _serializerOptions);
                        result.Character = character;
                        result.IsSuccess = true;
                    }
                    catch (Exception ex)
                    {
                        result.ErrorMessage = $"Сасай кудасай, не смогли распарсить ответ, мб текст ошибки поможет - {ex.Message}";
                        result.IsSuccess = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
                result.IsSuccess = false;
            }

            return result;
        }

        public static async Task<string> GenerateAsMarkdownText(CancellationToken cancellationToken)
        {
            var result = await Generate(cancellationToken);
            if (result.IsSuccess)
                return CharacterToMarkdownConverter.Convert(result.Character!);
            else
            {
                if (!string.IsNullOrEmpty(result.ErrorMessage))
                    return result.ErrorMessage;

                return "Упало, но даже текста ошибки никакого нет. Капец";
            }
        }
    }
}
