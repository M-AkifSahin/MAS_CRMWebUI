using MAS_CRM.Helper.Const;
using MAS_CRM.Helper.Result;
using MAS_CRMWebUI.Areas.AdminPanel.Models.Hotel;
using MAS_CRMWebUI.Areas.AdminPanel.Models.Room;
using MAS_CRMWebUI.SessionHelper;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Net;
using System.Text.Json;

namespace MAS_CRMWebUI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class RoomController : Controller
    {
        [HttpGet("/Admin/Odalar")]
        public async Task<IActionResult> Index()
        {
            var url = ApiEndpoint.ApiEndpointURL + "/Rooms";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.loginResponseDTO.Token);

            RestResponse response = await client.ExecuteAsync(request);

            var responseObject = JsonSerializer.Deserialize<ApiResult<List<RoomDTO>>>(response.Content);

            var roomList = responseObject.Data;

            return View(roomList);
            
        }

        [ValidateAntiForgeryToken]
        [HttpPost("/Admin/OdaEkle")]
        public async Task<IActionResult> AddHotel(AddRoomRequestDTO addRoomRequestDTO)
        {
            var url = ApiEndpoint.ApiEndpointURL + "/Room";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.loginResponseDTO.Token);
            request.AddBody(JsonSerializer.Serialize(addRoomRequestDTO));
            RestResponse response = await client.ExecuteAsync(request);


            if (response.StatusCode == HttpStatusCode.OK)
            {
                var responseObject = JsonSerializer.Deserialize<ApiResult<bool>>(response.Content);
                return Json(new { success = true });
            }
            var responseErrorObject = JsonSerializer.Deserialize<ApiResult<bool>>(response.Content);
            return Json(new { success = false, hataAciklama = responseErrorObject.HataBilgisi.HataAciklama[0] });
        }

        [HttpGet("/Admin/Oda/{roomGuid}")]
        public async Task<IActionResult> GetRoom(Guid roomGuid)
        {

            var url = ApiEndpoint.ApiEndpointURL + "/Room/" + roomGuid;
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.loginResponseDTO.Token);
            RestResponse response = await client.ExecuteAsync(request);

            var responseObject = JsonSerializer.Deserialize<ApiResult<RoomDTO>>(response.Content);
            var room = responseObject.Data;

            return Json(room);
        }

        [HttpPost("/Admin/UpdateRoom")]
        public async Task<IActionResult> UpdateRoom(UpdateRoomRequestDTO roomUpdateRequestDTO)
        {

            var url = ApiEndpoint.ApiEndpointURL + "/Room";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Put);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.loginResponseDTO.Token);

            request.AddBody(JsonSerializer.Serialize(roomUpdateRequestDTO));
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
