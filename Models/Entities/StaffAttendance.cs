namespace SchoolApi.Models.Entities
{
    public class StaffAttendance : BaseEntity
    {
        public int StaffId { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan? CheckInTime { get; set; }

        public TimeSpan? CheckOutTime { get; set; }

        public string Status { get; set; }

        public Staff Staff { get; set; }
    }
}
