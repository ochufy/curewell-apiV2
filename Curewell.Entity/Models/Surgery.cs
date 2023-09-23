namespace curewell.Entity.Models
{
    public class Surgery
    {
        public int? DoctorId { get; set; }
        public decimal EndTime { get; set; }
        public decimal StartTime { get; set; }
        public string? SurgeryCategory { get; set; }

        public DateTime SurgeryDate { get; set; }
        public int SurgeryId { get; set; }



    }
}
