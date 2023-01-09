using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestTask
{
    public class Accounts
    {
        [Key]
        public string? Name { get; set; }
        public Incidents? Incidents { get; set; }
    }
}
