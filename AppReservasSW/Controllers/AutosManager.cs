using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Text;
using AppReservasSW.Models;

namespace AppReservasSW.Controllers
{
    public class AutosManager
    {
        const string URL = "http://localhost:49220/api/auto/";
        const string URLIngresar = "http://localhost:49220/api/auto/ingresar/";

        HttpClient GetClient(string token)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Authorization", token);
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            return client;
        }

        public async Task<IEnumerable<Autos>> ObtenerAutos(string token)
        {
            HttpClient client = GetClient(token);
            string resultado = await client.GetStringAsync(URL);

            return JsonConvert.DeserializeObject<IEnumerable<Autos>>(resultado);
        }

        public async Task<IEnumerable<Autos>> ObtenerAuto(string token, string codigo)
        {
            HttpClient client = GetClient(token);
            string resultado = await client.GetStringAsync(URL + codigo);
            return JsonConvert.DeserializeObject<IEnumerable<Autos>>(resultado);
        }
        public async Task<Autos> Ingresar(Autos auto, string token)
        {
            HttpClient client = GetClient(token);
            var response = await client.PostAsync(URLIngresar,
                new StringContent(JsonConvert.SerializeObject(auto), Encoding.UTF8,
                "application/json"));
            return JsonConvert.DeserializeObject<Autos>(await response.Content.ReadAsStringAsync());
        }
        public async Task<Autos> Actualizar(Autos auto, string token)
        {
            HttpClient client = GetClient(token);
            var response = await client.PutAsync(URL,
                new StringContent(JsonConvert.SerializeObject(auto), Encoding.UTF8,
                "application/json"));
            return JsonConvert.DeserializeObject<Autos>(await response.Content.ReadAsStringAsync());
        }
        public async Task<string> Eliminar(string codigo, string token)
        {
            HttpClient client = GetClient(token);
            var response = await client.DeleteAsync(URL + codigo);

            return JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());
        }
    }
}