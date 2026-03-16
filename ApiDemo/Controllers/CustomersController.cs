using ApiDemo.Context;
using ApiDemo.Entities;
using Azure.Messaging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {

        private readonly ApiContext _context;
        public CustomersController(ApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult CustomerList()
        {
            var val = _context.Customers.ToList();
            return Ok(val);
        }

        [HttpPost]
        public IActionResult CreateCustomers(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return Ok("Müşteri Ekleme İşlemi Başarılı");
        }


        [HttpDelete]
        public IActionResult DeleteCustomers(int id)
        {
            var value = _context.Customers.Find(id);
            _context.Customers.Remove(value);
            return Ok("Müşteri Başarıyla Silindi");
        }


        [HttpGet("GetCustomer")]
        public IActionResult GetCustomer(int id)
        {
            var value = _context.Customers.Find(id);
            return Ok(value);
        }


        [HttpPut]
        public IActionResult UpdateCustomer(Customer Customer)
        {
            _context.Customers.Update(Customer);
            _context.SaveChanges();
            return Ok("Güncelleme İşlemi Tamamlandı");
        }
    }
}
