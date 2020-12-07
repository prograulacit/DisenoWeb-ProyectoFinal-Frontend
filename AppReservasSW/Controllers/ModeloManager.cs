using System.Collections.Generic;
using AppReservasSW.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Text;

namespace AppReservasSW.Controllers
{
    public class ModeloManager
    {
        const string URL = "http://localhost:49220/api/modelo/";
        const string URLIngresar = "http://localhost:49220/api/modelo/ingresar/";

        HttpClient GetClient(string token)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Authorization", token);
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            return client;
        }

        public async Task<IEnumerable<Modelo>> ObtenerModelos(string token)
        {
            HttpClient client = GetClient(token);
            string resultado = await client.GetStringAsync(URL);

            return JsonConvert.DeserializeObject<IEnumerable<Modelo>>(resultado);
        }

        public async Task<IEnumerable<Modelo>> ObtenerModelo(string token, string codigo)
        {
            HttpClient client = GetClient(token);
            string resultado = await client.GetStringAsync(URL + codigo);
            return JsonConvert.DeserializeObject<IEnumerable<Modelo>>(resultado);
        }
        public async Task<Modelo> Ingresar(Modelo modelo, string token)
        {
            HttpClient client = GetClient(token);
            var response = await client.PostAsync(URLIngresar,
                new StringContent(JsonConvert.SerializeObject(modelo), Encoding.UTF8,
                "application/json"));
            return JsonConvert.DeserializeObject<Modelo>(await response.Content.ReadAsStringAsync());
        }
        public async Task<Modelo> Actualizar(Modelo modelos, string token)
        {
            HttpClient client = GetClient(token);
            var response = await client.PutAsync(URL,
                new StringContent(JsonConvert.SerializeObject(modelos), Encoding.UTF8,
                "application/json"));
            return JsonConvert.DeserializeObject<Modelo>(await response.Content.ReadAsStringAsync());
        }
        public async Task<string> Eliminar(string codigo, string token)
        {
            HttpClient client = GetClient(token);
            var response = await client.DeleteAsync(URL + codigo);

            return JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());
        }
    }
}