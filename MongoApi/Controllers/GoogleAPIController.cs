using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Newtonsoft.Json;

namespace MongoApi.Controllers
{
    [Produces("application/json")]
    [Route("api/GoogleAPI")]
    public class GoogleAPIController : Controller
    {
        [HttpGet("getGoogleBook")]
        public async Task<IActionResult> GetGoogleBook()
        {
            string url = "https://www.googleapis.com/books/v1/volumes?q=a&fields=items%2FvolumeInfo%2Ftitle&key=AIzaSyAlSzb3Dv5ExVZsikbJ_EyU64iCc2H_-9c";
            var client = new HttpClient();
            var response = await client.GetAsync(url);
         
            var product = await response.Content.ReadAsStreamAsync();

            string json = JsonConvert.SerializeObject(product);

            client.Dispose();

            return new OkObjectResult(json);
        }
    }
}