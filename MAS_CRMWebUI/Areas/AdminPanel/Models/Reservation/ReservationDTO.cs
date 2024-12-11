namespace MAS_CRMWebUI.Areas.AdminPanel.Models.Reservation
{
    public class ReservationDTO
    {
        public int RoomId { get; set; }
        public int CustomerId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public Guid Guid { get; set; }
    }
}
