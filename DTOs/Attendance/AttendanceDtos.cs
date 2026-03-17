namespace SchoolApi.DTOs.Attendance
{
    public class CreateStudentAttendanceDto
    {
        public int StudentId { get; set; }
        public int SectionId { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; } = string.Empty; // Present, Absent, Leave
        public int MarkedByStaffId { get; set; }
    }

    public class BulkStudentAttendanceDto
    {
        public int SectionId { get; set; }
        public DateTime Date { get; set; }
        public int MarkedByStaffId { get; set; }
        public List<StudentAttendanceEntryDto> Entries { get; set; } = [];
    }

    public class StudentAttendanceEntryDto
    {
        public int StudentId { get; set; }
        public string Status { get; set; } = string.Empty;
    }

    public class UpdateStudentAttendanceDto
    {
        public string Status { get; set; } = string.Empty;
    }

    public class CreateStaffAttendanceDto
    {
        public int StaffId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan? CheckInTime { get; set; }
        public TimeSpan? CheckOutTime { get; set; }
        public string Status { get; set; } = string.Empty; // Present, Absent, Leave, HalfDay
    }

    public class UpdateStaffAttendanceDto
    {
        public TimeSpan? CheckInTime { get; set; }
        public TimeSpan? CheckOutTime { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
