using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
namespace MVPSA_V2022.clases
{
    public class Utilidades
    {
        public static Dictionary<string, List<string>> ExtraerErrorDelWebApi(string json)
        {
            var respuesta = new Dictionary<string, List<string>>();
            var jsonElement = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(json);
            var errorJsonElement = jsonElement.GetProperty("errors");
            try { 
            foreach (var campoConErrores in errorJsonElement.EnumerateObject())
            {
                var campo = campoConErrores.Name;
                var errores = new List<string>();
                foreach (var errorKind in campoConErrores.Value.EnumerateArray())
                {
                    var error = errorKind.GetString();
                     errores.Add(error);
                }
                respuesta.Add(campo, errores);
            }

            return respuesta;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return respuesta = null;
            }
        }
 
    }
}
