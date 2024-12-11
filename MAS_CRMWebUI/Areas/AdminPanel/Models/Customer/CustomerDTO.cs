namespace MAS_CRMWebUI.Areas.AdminPanel.Models.Customer
{
    public class CustomerDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Guid Guid { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
    }
}
