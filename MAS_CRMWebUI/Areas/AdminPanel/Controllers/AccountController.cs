using MAS_CRM.Helper.Result;
using MAS_CRMWebUI.Areas.AdminPanel.Models.Login.DTO;
using MAS_CRMWebUI.SessionHelper;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Net;
using System.Text.Json;

namespace MAS_CRMWebUI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Route("[action]")]
    public class AccountController : Controller
    {
        [HttpGet("/AdminAccount/Login")]
        public IActionResult LoginPage()
        {
            return View();
        }

        [HttpPost("/Account/AdminLogin")]
        public async Task<IActionResult> AdminLogin(LoginRequestDTO loginRequestDTO)
        {
            var url = "http://localhost:5096/Login";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            var body = JsonSerializer.Serialize(loginRequestDTO);
            request.AddBody(body, "application/json");
            RestResponse response = await client.ExecuteAsync(request);

            var responseObject = JsonSerializer.Deserialize<ApiResult<LoginResponseDTO>>(response.Content);

            if (response.StatusCode==HttpStatusCode.NotFound)
            {
                ViewData["LoginError"] = "Kullanıcı Adı veya Şire Yanlış";
                return View("LoginPage");
            }
            else
            {
                SessionManager.loginResponseDTO = responseObject.Data;
               return RedirectToAction("Index", "Home");
            }
            
        }

        public async Task<IActionResult> Logout()
        {
            SessionManager.loginResponseDTO = null;
            return RedirectToAction("LoginPage", "Account");
        }
    }
}
