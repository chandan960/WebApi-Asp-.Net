using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class ProductController : ApiController
    {
        //static List<string> products = new List<string>()
        //{
        //    "Laptop","phone","PC"
        //};

        //[HttpGet]
        //public List<string> GetAll()
        //{
        //    return products;
        //}

        //[HttpGet]
        //public string GetById(int id)
        //{
        //    return products[id];
        //}

        //[HttpGet]
        //public void Save(string productName)
        //{
        //    products.Add(productName);
        //}

        //[HttpGet]
        //public void Update(int id, string productName)
        //{
        //    products[id] = productName;
        //}

        //[HttpGet]
        //public void Delete(int id)
        //{
        //    products.RemoveAt(id);
        //}

        //[HttpGet]
        //public string GetProductName()
        //{
        //    return "Laptop";
        //}

        ApplicationDbContext _db = new ApplicationDbContext();

        /// <summary>
        /// Save new product into database
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Add([FromBody]Product product)
        {
            _db.Products.Add(product);
            int rowCount = _db.SaveChanges();
            if (rowCount > 0)
            {
                return Ok("Product has been saved.");
            }
            else
            {
                return BadRequest("Product save failed.");
            }
        }

        /// <summary>
        /// Get all product into database
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var products =_db.Products.ToList();
            if (products.Count==0)
            {
                return NotFound();
            }
            else
            {
                return Ok(products);
            }
        }


        /// <summary>
        /// Get single product into database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetById(int id)
        {
            var product = _db.Products.FirstOrDefault(c=>c.Id==id);
            if (product == null )
            {
                return NotFound();
            }
            else
            {
                return Ok(product);
            }
        }


        /// <summary>
        /// Product update into database
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPut]
        public IHttpActionResult Update([FromBody]Product product)
        {
            if (product.Id <= 0)
            {
                return NotFound();
            }
            else
            {
                _db.Entry(product).State = System.Data.Entity.EntityState.Modified;
                int rowCount = _db.SaveChanges();

                if (rowCount > 0)
                {
                    return Ok(product);
                }
                else
                {
                    return BadRequest("Update failed");
                }
            }
        }


        /// <summary>
        /// Product deleteinto database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public IHttpActionResult Delete (int id)
        {
            var product = _db.Products.FirstOrDefault(c => c.Id == id);
            if(product== null)
            {
                return NotFound();
            }
            else
            {
                _db.Products.Remove(product);
                int rowCount = _db.SaveChanges();
                if (rowCount > 0)
                {
                    return Ok("Product has been deleted");
                }
                else
                {
                    return BadRequest("Delete failed");
                }
            }
        }


    }
}
