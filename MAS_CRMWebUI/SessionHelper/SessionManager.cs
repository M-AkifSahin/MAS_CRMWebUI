using MAS_CRMWebUI.Areas.AdminPanel.Models.Login.DTO;

namespace MAS_CRMWebUI.SessionHelper
{
    public class SessionManager
    {
        public static LoginResponseDTO? loginResponseDTO
        {
            get => AppHttpContext.Current.Session.GetObject<LoginResponseDTO>("LoginResponseDTO");

            set => AppHttpContext.Current.Session.SetObject("LoginResponseDTO", value);
        }
   
    }
}
