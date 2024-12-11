namespace MAS_CRMWebUI.Areas.AdminPanel.Models.Room
{
    public class AddRoomRequestDTO
    {
        
        public string RoomNumber { get; set; }
        public string Type { get; set; }
        public int PricePerNight { get; set; }
        public bool Availability { get; set; }
        public Guid HotelGUID { get; set; }

        
    }
}
