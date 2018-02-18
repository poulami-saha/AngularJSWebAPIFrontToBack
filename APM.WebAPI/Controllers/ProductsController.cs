using APM.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using System.Web.Http.OData;

namespace APM.WebAPI.Controllers
{
    [EnableCorsAttribute("http://localhost:50368","*","*")]
    public class ProductsController : ApiController
    {
        ProductRepository repo = new ProductRepository();
        // GET: api/Products
        [EnableQuery()]
        [ResponseType(typeof(Product))]
       // public IQueryable<Product> Get()
        public IHttpActionResult Get()
        {
            try
            {
                return Ok(repo.Retrieve().AsQueryable());
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/Products/5
        [ResponseType(typeof(Product))]
        public IHttpActionResult Get(string search)
        {
            try
            {
                var products = repo.Retrieve();
                return  Ok(products.Where(p => p.ProductCode.Contains(search)));
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [ResponseType(typeof(Product))]
        public IHttpActionResult Get(int id)
        {
            try
            {
                Product product;
                if (id > 0)
                {
                    var products = repo.Retrieve();
                    product = products.FirstOrDefault(p => p.ProductId == id);
                    if (product == null)
                        return NotFound();
                }
                else
                {
                    product = repo.Create();
                }
                return Ok(product);
            }
            catch(Exception ex)
            {
                return  InternalServerError(ex);
            }
        }

        // POST: api/Products
        [ResponseType(typeof(Product))]
        public IHttpActionResult Post([FromBody]Product product)
        {
            try
            {
                if (product == null)
                {
                    return BadRequest("Product cannot be null");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var newProduct = repo.Save(product);
                if (newProduct == null)
                {
                    return Conflict();
                }
                return Created<Product>(Request.RequestUri + newProduct.ProductId.ToString(), newProduct);
            }
            catch(Exception ex)
            {
                return InternalServerError();
            }
        }

        // PUT: api/Products/5
        public IHttpActionResult Put(int id, [FromBody]Product product)
        {
            try
            {
                if (product == null)
                    return BadRequest("Product cannot be null");

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var updatedProduct = repo.Save(id, product);
                if (updatedProduct == null)
                {
                    return NotFound();
                }
                return Ok();
            }
            catch(Exception ex)
            {
                return InternalServerError();
            }
        }

        // DELETE: api/Products/5
        public void Delete(int id)
        {
        }
    }
}
