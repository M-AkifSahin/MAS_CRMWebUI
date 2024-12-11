namespace MAS_CRMWebUI.Areas.AdminPanel.Models.Hotel
{
    public class HotelDTO
    {
        public string Name { get; set; }
        public Guid? Guid { get; set; }

        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Description { get; set; }

    }
}
