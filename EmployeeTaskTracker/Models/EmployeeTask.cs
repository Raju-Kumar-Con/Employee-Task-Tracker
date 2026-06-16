using System.ComponentModel.DataAnnotations;

namespace EmployeeTaskTracker.Models
{
    public class EmployeeTask
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

public string Description { get; set; } = string.Empty;

        public TaskState Status { get; set; }

        [DataType(DataType.Date)]
        public DateTime Deadline { get; set; }

        public int EmployeeId { get; set; }
    }
}