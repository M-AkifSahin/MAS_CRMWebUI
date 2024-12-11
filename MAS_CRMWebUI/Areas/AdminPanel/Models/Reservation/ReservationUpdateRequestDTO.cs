namespace MAS_CRMWebUI.Areas.AdminPanel.Models.Reservation
{
    public class ReservationUpdateRequestDTO
    {
        public int RoomId { get; set; }
        public int CustomerId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public Guid CustomerGUID { get; set; }
        public Guid RoomGuid { get; set; }     
        public Guid GUID { get; set; }
    }
}
