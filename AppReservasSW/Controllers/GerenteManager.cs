using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Text;
using AppReservasSW.Models;

namespace AppReservasSW.Controllers
{
    public class GerenteManager
    {
        const string URL = "http://localhost:49220/api/gerente/";
        const string URLIngresar = "http://localhost:49220/api/gerente/ingresar/";

        HttpClient GetClient(string token)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Authorization", token);
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            return client;
        }

        public async Task<IEnumerable<Gerente>> ObtenerGerentes(string token)
        {
            HttpClient client = GetClient(token);
            string resultado = await client.GetStringAsync(URL);

            return JsonConvert.DeserializeObject<IEnumerable<Gerente>>(resultado);
        }

        public async Task<IEnumerable<Gerente>> ObtenerGerente(string token, string codigo)
        {
            HttpClient client = GetClient(token);
            string resultado = await client.GetStringAsync(URL + codigo);
            return JsonConvert.DeserializeObject<IEnumerable<Gerente>>(resultado);
        }
        public async Task<Gerente> Ingresar(Gerente gerente, string token)
        {
            HttpClient client = GetClient(token);
            var response = await client.PostAsync(URLIngresar,
                new StringContent(JsonConvert.SerializeObject(gerente), Encoding.UTF8,
                "application/json"));
            return JsonConvert.DeserializeObject<Gerente>(await response.Content.ReadAsStringAsync());
        }
        public async Task<Gerente> Actualizar(Gerente gerente, string token)
        {
            HttpClient client = GetClient(token);
            var response = await client.PutAsync(URL,
                new StringContent(JsonConvert.SerializeObject(gerente), Encoding.UTF8,
                "application/json"));
            return JsonConvert.DeserializeObject<Gerente>(await response.Content.ReadAsStringAsync());
        }
        public async Task<string> Eliminar(string codigo, string token)
        {
            HttpClient client = GetClient(token);
            var response = await client.DeleteAsync(URL + codigo);

            return JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());
        }
    }
}