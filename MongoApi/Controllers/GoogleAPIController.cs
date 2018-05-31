using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Newtonsoft.Json;
using MongoApi.Models;
using Microsoft.Extensions.Options;

namespace MongoApi.Controllers
{
    [Produces("application/json")]
    [Route("api/GoogleAPIBooks")]
    public class GoogleAPIController : Controller 
    {
        private readonly GoogleApiKey _googleApiKey;

        public GoogleAPIController(IOptions<GoogleApiKey> options)
        {
            _googleApiKey = options.Value;
        }
        [HttpGet("getGoogleBook")]
        public async Task<IActionResult> GetGoogleBook()
        {
            string url = "https://www.googleapis.com/books/v1/volumes?q=a&fields=items%2FvolumeInfo%2Ftitle&key="+_googleApiKey.GoogleKey1+"";
            var client = new HttpClient();
            var response = await client.GetAsync(url);
         
            var product = await response.Content.ReadAsStringAsync();


            client.Dispose();

            return new OkObjectResult(product);
        }
    }
}