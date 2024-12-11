using MAS_CRM.Helper.Const;
using MAS_CRM.Helper.Result;
using MAS_CRMWebUI.Areas.AdminPanel.Models.Hotel;
using MAS_CRMWebUI.SessionHelper;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Text.Json;

namespace MAS_CRMWebUI.Areas.AdminPanel.Component
{
    public class HotelDropDownViewComponent:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(Guid? roomHotelGuid, string ddlID)
        {
            var url = ApiEndpoint.ApiEndpointURL + "/Hotels";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.loginResponseDTO.Token);

            RestResponse response = await client.ExecuteAsync(request);

            var responseObject = JsonSerializer.Deserialize<ApiResult<List<HotelResponseDTO>>>(response.Content);

            var hotelList = responseObject.Data;

            HotelDropDownViewModel viewModel = new HotelDropDownViewModel();
            viewModel.HotelList = hotelList;
            viewModel.HotelGUID = roomHotelGuid == Guid.Empty? null : roomHotelGuid;
            viewModel.ddlHotelID = ddlID;
            
            

            return View("~/Areas/AdminPanel/Views/Shared/Hotel/HotelDropDown.cshtml",viewModel);
        }
    }
}
