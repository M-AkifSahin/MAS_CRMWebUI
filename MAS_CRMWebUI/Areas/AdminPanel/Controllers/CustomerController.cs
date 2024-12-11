using MAS_CRM.Helper.Const;
using MAS_CRM.Helper.Result;
using MAS_CRMWebUI.Areas.AdminPanel.Models.Customer;
using MAS_CRMWebUI.Areas.AdminPanel.Models.Login.DTO;
using MAS_CRMWebUI.SessionHelper;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Net;
using System.Text.Json;

namespace MAS_CRMWebUI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class CustomerController : Controller
    {
        [HttpGet("/Admin/Musteriler")]
        public async Task<IActionResult> Customers()
        {
            var url = ApiEndpoint.ApiEndpointURL+"/Customers";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");          
            request.AddHeader("Authorization", "Bearer " + SessionManager.loginResponseDTO.Token);
            RestResponse response = await client.ExecuteAsync(request);

            var responseObject = JsonSerializer.Deserialize<ApiResult<List<CustomerDTO>>>(response.Content);
            var customerList = responseObject.Data;

            return View(customerList);
        }

        [HttpGet("/Admin/Musteri/{customerGuid}")]
        public async Task<IActionResult> GetCustomer(Guid customerGuid)
        {

            var url = ApiEndpoint.ApiEndpointURL + "/Customer/"+ customerGuid;
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.loginResponseDTO.Token);
            RestResponse response = await client.ExecuteAsync(request);

            var responseObject = JsonSerializer.Deserialize<ApiResult<CustomerDTO>>(response.Content);
            var customer = responseObject.Data;

            return Json(customer);
        }

        [ValidateAntiForgeryToken]
        [HttpPost("/Admin/UpdateCustomer")]
        public async Task<IActionResult> UpdateCustomer(CustomerUpdateRequestDTO customerUpdateRequestDTO)
        {

            var url = ApiEndpoint.ApiEndpointURL + "/Customer/";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Put);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.loginResponseDTO.Token);

            request.AddBody(JsonSerializer.Serialize(customerUpdateRequestDTO));
            RestResponse response = await client.ExecuteAsync(request);

            var responseObject = JsonSerializer.Deserialize<ApiResult<bool>>(response.Content);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        [ValidateAntiForgeryToken]
        [HttpPost("/Admin/AddCustomer")]
        public async Task<IActionResult> AddCustomer(CustomerAddRequestDTO customerAddRequestDTO)
        {

            var url = ApiEndpoint.ApiEndpointURL + "/Customer/";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.loginResponseDTO.Token);

            request.AddBody(JsonSerializer.Serialize(customerAddRequestDTO));
            RestResponse response = await client.ExecuteAsync(request);

            

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var responseObject = JsonSerializer.Deserialize<ApiResult<bool>>(response.Content);
                return Json(new { success = true });
            }
            var responseErrorObject = JsonSerializer.Deserialize<ApiResult<bool>>(response.Content);
            return Json(new { success = false, hataAciklama= responseErrorObject.HataBilgisi.HataAciklama[0] });
        }
    }
}
