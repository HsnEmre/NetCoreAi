using Microsoft.AspNetCore.Mvc;
using NetCoreAı.ApiConsumeUi.Dtos;
using Newtonsoft.Json;
using System.Text;

namespace NetCoreAı.ApiConsumeUi.Controllers
{
    public class CustomerController : Controller
    {

        private readonly IHttpClientFactory _httpClientFactory;
        public CustomerController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> CustomerList()
        {
            var ct = _httpClientFactory.CreateClient();
            var responseMessage = await ct.GetAsync("https://localhost:7182/api/Customers");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultCustomerDto>>(jsonData);
                return View(values);
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CreateCustomer()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CreateCustomerDto dto)
        {
            var ct = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(dto);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application.json");

            var responseMessage = await ct.PostAsync("https://localhost:7182/api/Customers", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("CustomerList");
            }
            return View();

            //var responseMessage=await ct.GetAsync("")
        }



        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var ct = _httpClientFactory.CreateClient();
            var responseMessage = await ct.DeleteAsync("https://localhost:7182/api/Customers?id=" + id);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("CustomerList");
            }
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> UpdateCustomer(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7182/api/Customers/GetCustomer?id=" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<GetByIdCustomerDto>(jsonData);
                return View(values);
            }
            return View();


        }

        [HttpPost]
        public async Task<IActionResult> UpdateCustomerDto(UpdateCustomerDto updateCustomerDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateCustomerDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application.json");
            var responseMessage = await client.PutAsync("https://localhost:7182/api/Customers", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("CustomerList");
            }

            return View();
        }

    }
}
