using MAS_CRM.Helper.Const;
using MAS_CRM.Helper.Result;
using MAS_CRMWebUI.Areas.AdminPanel.Models.Customer;
using MAS_CRMWebUI.Areas.AdminPanel.Models.Reservation;
using MAS_CRMWebUI.SessionHelper;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Net;
using System.Text.Json;

namespace MAS_CRMWebUI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class ReservationController : Controller
    {
        [HttpGet("/Admin/Rezervasyonlar")]
        public async Task <IActionResult> Index()
        {
            var url = ApiEndpoint.ApiEndpointURL + "/Reservations";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.loginResponseDTO.Token);
            RestResponse response = await client.ExecuteAsync(request);

            var responseObject = JsonSerializer.Deserialize<ApiResult<List<ReservationDTO>>>(response.Content);
            var reservationList = responseObject.Data;

            return View(reservationList);
        }

        //[ValidateAntiForgeryToken]
        [HttpPost("/Admin/AddReservation")]
        public async Task<IActionResult> AddReservation(ReservationAddRequestDTO reservationAddRequestDTO)
        {

            var url = ApiEndpoint.ApiEndpointURL + "/Reservation/";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.loginResponseDTO.Token);

            request.AddBody(JsonSerializer.Serialize(reservationAddRequestDTO));
            RestResponse response = await client.ExecuteAsync(request);



            if (response.StatusCode == HttpStatusCode.OK)
            {
                var responseObject = JsonSerializer.Deserialize<ApiResult<bool>>(response.Content);
                return Json(new { success = true });
            }
            var responseErrorObject = JsonSerializer.Deserialize<ApiResult<bool>>(response.Content);
            return Json(new { success = false, hataAciklama = responseErrorObject.HataBilgisi.HataAciklama[0] });
        }

        [HttpGet("/Admin/Rezervasyon/{reservationGuid}")]
        public async Task<IActionResult> GetCustomer(Guid reservationGuid)
        {

            var url = ApiEndpoint.ApiEndpointURL + "/Reservation/" + reservationGuid;
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.loginResponseDTO.Token);
            RestResponse response = await client.ExecuteAsync(request);

            var responseObject = JsonSerializer.Deserialize<ApiResult<ReservationDTO>>(response.Content);
            var reservation = responseObject.Data;

            return Json(reservation);
        }

        [HttpPost("/Admin/UpdateReservation")]
        public async Task<IActionResult> UpdateReservation(ReservationUpdateRequestDTO reservationUpdateRequestDTO)
        {

            var url = ApiEndpoint.ApiEndpointURL + "/Reservation/";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Put);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.loginResponseDTO.Token);

            request.AddBody(JsonSerializer.Serialize(reservationUpdateRequestDTO));
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
