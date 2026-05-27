namespace DB_ex_proj.Models
{
    public class Auditorium
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; }

        public Group Group { get; set; }
    }
}
