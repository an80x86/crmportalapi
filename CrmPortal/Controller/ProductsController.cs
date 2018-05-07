using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CrmPortal.Models;

namespace CrmPortal.Controllers
{
    [Route("crmportal/restapi/[controller]")]
    public class ProductsController : Controller
    {
        private readonly CrmPortalContext _context;

        public ProductsController(CrmPortalContext context)
        {
            _context = context;

            if (_context.Products.Count() == 0)
            {
                _context.Products.Add(new Product { Id = 19201, Name = "Lego Nexo Knights King I", UnitPrice = 45 });
                _context.Products.Add(new Product { Id = 23942, Name = "Lego Starwars Minifigure Jedi", UnitPrice = 55 });
                _context.Products.Add(new Product { Id = 30021, Name = "Star Wars Yay takimi", UnitPrice = 35.50 });
                _context.Products.Add(new Product { Id = 30492, Name = "Star Wars kahve takimi", UnitPrice = 24.40 });

                _context.SaveChanges();
            }
        }

        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return _context.Products.ToList();
        }


        [HttpGet("{id}", Name = "GetProduct")]
        public IActionResult Get(int id)
        {
            var product = _context.Products.FirstOrDefault(t => t.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return new ObjectResult(product);
        }

        [HttpPost]
        public IActionResult Post([FromBody]Product newProduct)
        {
            if (newProduct == null)
                return BadRequest();

            _context.Products.Add(newProduct);
            _context.SaveChanges();
            return CreatedAtRoute("GetProduct", new { id = newProduct.Id }, newProduct);
        }

        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody]Product value)
        {
            if (value == null || value.Id != id)
                return BadRequest();

            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound();

            product.Name = value.Name;
            product.UnitPrice = value.UnitPrice;

            _context.Products.Update(product);
            _context.SaveChanges();

            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound();

            _context.Products.Remove(product);
            _context.SaveChanges();

            return new NoContentResult();
        }
    }
}