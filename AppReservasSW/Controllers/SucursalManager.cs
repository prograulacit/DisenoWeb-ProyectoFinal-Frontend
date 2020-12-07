using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Text;
using AppReservasSW.Models;

namespace AppReservasSW.Controllers
{
    public class SucursalManager
    {
        const string URL = "http://localhost:49220/api/sucursal/";
        const string URLIngresar = "http://localhost:49220/api/sucursal/ingresar/";

        HttpClient GetClient(string token)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Authorization", token);
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            return client;
        }

        public async Task<IEnumerable<Sucursal>> ObtenerSucursales(string token)
        {
            HttpClient client = GetClient(token);
            string resultado = await client.GetStringAsync(URL);

            return JsonConvert.DeserializeObject<IEnumerable<Sucursal>>(resultado);
        }

        public async Task<IEnumerable<Sucursal>> ObtenerSucursal(string token, string codigo)
        {
            HttpClient client = GetClient(token);
            string resultado = await client.GetStringAsync(URL + codigo);
            return JsonConvert.DeserializeObject<IEnumerable<Sucursal>>(resultado);
        }
        public async Task<Sucursal> Ingresar(Sucursal sucursal, string token)
        {
            HttpClient client = GetClient(token);
            var response = await client.PostAsync(URLIngresar,
                new StringContent(JsonConvert.SerializeObject(sucursal), Encoding.UTF8,
                "application/json"));
            return JsonConvert.DeserializeObject<Sucursal>(await response.Content.ReadAsStringAsync());
        }
        public async Task<Sucursal> Actualizar(Sucursal sucursal, string token)
        {
            HttpClient client = GetClient(token);
            var response = await client.PutAsync(URL,
                new StringContent(JsonConvert.SerializeObject(sucursal), Encoding.UTF8,
                "application/json"));
            return JsonConvert.DeserializeObject<Sucursal>(await response.Content.ReadAsStringAsync());
        }
        public async Task<string> Eliminar(string codigo, string token)
        {
            HttpClient client = GetClient(token);
            var response = await client.DeleteAsync(URL + codigo);

            return JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());
        }
    }
}