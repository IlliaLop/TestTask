using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestTask
{
    public class Incident
    {
        public string? IncidentName { get; set; }
        public string? Description { get; set; }
        public Account Account { get; set; }
    }
}
