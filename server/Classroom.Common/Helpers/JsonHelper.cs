using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Classroom.Common.Helpers
{
    public static class JsonHelper
    {
        public static StringContent SerializeToJson<T>(T model)
        {
            var content = JsonSerializer.Serialize(model, new JsonSerializerOptions 
            {
                PropertyNameCaseInsensitive = true
            });

            return new StringContent(content, Encoding.UTF8, MediaTypeNames.Application.Json);
        }

        public static async Task<T> DeserializeResponseToJson<T>(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();
            var model = JsonSerializer.Deserialize<T>(content, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            return model;
        }
    }
}