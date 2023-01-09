using System.ComponentModel.DataAnnotations;

namespace TestTask
{
    public class Incidents
    {
        [Key]
        public string? IncidentName { get; set; }
        public string? Description { get; set; }
    }
}
