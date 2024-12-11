using MAS_CRM.Helper.Const;
using MAS_CRM.Helper.Result;
using MAS_CRMWebUI.Areas.AdminPanel.Models.Customer;
using MAS_CRMWebUI.Areas.AdminPanel.Models.Hotel;
using MAS_CRMWebUI.Areas.AdminPanel.Models.Payment;
using MAS_CRMWebUI.Areas.AdminPanel.Models.Room;
using MAS_CRMWebUI.SessionHelper;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Net;
using System.Text.Json;

namespace MAS_CRMWebUI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class PaymentController : Controller
    {
        [HttpGet("/Admin/Odemeler")]
        public async Task<IActionResult> Index()
        {
            var url = ApiEndpoint.ApiEndpointURL + "/Payments";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.loginResponseDTO.Token);

            RestResponse response = await client.ExecuteAsync(request);

            var responseObject = JsonSerializer.Deserialize<ApiResult<List<PaymentDTO>>>(response.Content);

            var paymentList = responseObject.Data;

            return View(paymentList);
        }

        [ValidateAntiForgeryToken]
        [HttpPost("/Admin/OdemeEkle")]
        public async Task<IActionResult> AddPayment(AddPaymentRequestDTO addPaymentRequestDTO)
        {
            var url = ApiEndpoint.ApiEndpointURL + "/Payment";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.loginResponseDTO.Token);
            request.AddBody(JsonSerializer.Serialize(addPaymentRequestDTO));
            RestResponse response = await client.ExecuteAsync(request);


            if (response.StatusCode == HttpStatusCode.OK)
            {
                var responseObject = JsonSerializer.Deserialize<ApiResult<bool>>(response.Content);
                return Json(new { success = true });
            }
            var responseErrorObject = JsonSerializer.Deserialize<ApiResult<bool>>(response.Content);
            return Json(new { success = false, hataAciklama = responseErrorObject.HataBilgisi.HataAciklama[0] });
        }

        [HttpGet("/Admin/Odeme/{paymentGuid}")]
        public async Task<IActionResult> GetCustomer(Guid paymentGuid)
        {

            var url = ApiEndpoint.ApiEndpointURL + "/Payment/" + paymentGuid;
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.loginResponseDTO.Token);
            RestResponse response = await client.ExecuteAsync(request);

            var responseObject = JsonSerializer.Deserialize<ApiResult<PaymentDTO>>(response.Content);
            var payment = responseObject.Data;

            return Json(payment);
        }

        [HttpPost("/Admin/UpdatePayment")]
        public async Task<IActionResult> UpdatePayment(PaymentUpdateRequestDTO paymentUpdateRequestDTO)
        {

            var url = ApiEndpoint.ApiEndpointURL + "/Payment/";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Put);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.loginResponseDTO.Token);

            request.AddBody(JsonSerializer.Serialize(paymentUpdateRequestDTO));
            RestResponse response = await client.ExecuteAsync(request);

            var responseObject = JsonSerializer.Deserialize<ApiResult<bool>>(response.Content);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}
