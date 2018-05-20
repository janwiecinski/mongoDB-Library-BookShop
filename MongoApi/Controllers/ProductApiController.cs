using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MongoApi.Models;
using MongoDB.Bson;
using DataAcces.DAL;
using DataAcces.DAL.Models;
using DataAcces.DAL.Repository;
using AutoMapper;

namespace MongoApi.Controllers
{
    [Produces("application/json")]
    [Route("api/product")]
    public class ProductApiController : Controller
    {
        IRepository<Products> _dsObject;
        IMapper _imapper;
        public ProductApiController(IRepository<Products> dsObject, IMapper mapper)
        {
            _dsObject = dsObject;
            _imapper = mapper;
        }

        [HttpGet]
        public IEnumerable<Products> Get()
        {
            var result = _dsObject.GetItems();
            return result;
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var product = _dsObject.GetItem(new ObjectId(id));
            if (product == null)
            {
                return NotFound();
            }

            return new ObjectResult(product);
        }

        [HttpPost]
        public IActionResult Post([FromBody]Products p)
        {
            _dsObject.Create(p);
            return new OkObjectResult(p);
        }

        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody]Products p)
        {
            var prodId = new ObjectId(id);
            var product = _dsObject.GetItem(prodId);

            if (product == null)
            {
                return NotFound();
            }
            _dsObject.Update(p);

            return new OkResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var product = _dsObject.GetItem(new ObjectId(id));

            if (product == null)
            {
                return NotFound();
            }

            _dsObject.Remove(product.Id);

            return new OkResult();
        }
    }
}