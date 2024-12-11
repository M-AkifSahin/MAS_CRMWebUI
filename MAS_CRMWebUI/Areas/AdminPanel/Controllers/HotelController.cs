using MAS_CRM.Helper.Const;
using MAS_CRM.Helper.Result;
using MAS_CRMWebUI.Areas.AdminPanel.Models.Customer;
using MAS_CRMWebUI.Areas.AdminPanel.Models.Hotel;
using MAS_CRMWebUI.SessionHelper;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Net;
using System.Text.Json;

namespace MAS_CRMWebUI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class HotelController : Controller
    {
        
        [HttpGet("/Admin/Oteller")]
        public async Task<IActionResult> Index()
        {
            var url = ApiEndpoint.ApiEndpointURL + "/Hotels";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.loginResponseDTO.Token);
           
            RestResponse response = await client.ExecuteAsync(request);

            var responseObject = JsonSerializer.Deserialize<ApiResult<List<HotelDTO>>>(response.Content);

            var hotelList = responseObject.Data;

            return View(hotelList);
        }

        [ValidateAntiForgeryToken]
        [HttpPost("/Admin/OtelEkle")]
        public async Task<IActionResult> AddHotel(AddHotelRequestDTO addHotelRequestDTO)
        {
            var url = ApiEndpoint.ApiEndpointURL + "/Hotel";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.loginResponseDTO.Token);
            request.AddBody(JsonSerializer.Serialize(addHotelRequestDTO));
            RestResponse response = await client.ExecuteAsync(request);

            
            if (response.StatusCode==HttpStatusCode.OK)
            {
                var responseObject = JsonSerializer.Deserialize<ApiResult<bool>>(response.Content);
                return Json(new { success = true });
            }
            var responseErrorObject = JsonSerializer.Deserialize<ApiResult<bool>>(response.Content);
            return Json(new { success = false, hataAciklama = responseErrorObject.HataBilgisi.HataAciklama[0] });
        }

        [HttpGet("/Admin/Otel/{otelGuid}")]
        public async Task<IActionResult> GetHotel(Guid otelGuid)
        {

            var url = ApiEndpoint.ApiEndpointURL + "/Hotel/" + otelGuid;
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.loginResponseDTO.Token);
            RestResponse response = await client.ExecuteAsync(request);

            var responseObject = JsonSerializer.Deserialize<ApiResult<HotelDTO>>(response.Content);
            var hotel = responseObject.Data;

            return Json(hotel);
        }

        
        [HttpPost("/Admin/UpdateHotel")]
        public async Task<IActionResult> UpdateHotel(HotelUpdateRequestDTO hotelUpdateRequestDTO)
        {

            var url = ApiEndpoint.ApiEndpointURL + "/Hotel";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Put);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.loginResponseDTO.Token);

            request.AddBody(JsonSerializer.Serialize(hotelUpdateRequestDTO));
            RestResponse response = await client.ExecuteAsync(request);

            var responseObject = JsonSerializer.Deserialize<ApiResult<bool>>(response.Content);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }



        [HttpGet("/Admin/InvokeHotelDropDown")]
        public IActionResult InvokeHotelDropDown(Guid? roomHotelGuid, string ddlID)
        {
            return ViewComponent("HotelDropDown", new { roomHotelGuid , ddlID});
        }
    }
}
