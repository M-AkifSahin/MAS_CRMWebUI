namespace MAS_CRMWebUI.Areas.AdminPanel.Models.Room
{
    public class RoomDTO
    {
        public int HotelId { get; set; }
        public string RoomNumber { get; set; }
        public string Type { get; set; }
        public int PricePerNight { get; set; }
        public bool Availability { get; set; }
        public Guid Guid { get; set; }

        public string HotelName { get; set; }

        public Guid HotelGUID { get; set; }
    }
}
