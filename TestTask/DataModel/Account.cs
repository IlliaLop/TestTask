using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestTask
{
    public class Account
    {
        public string? Name { get; set; }
        public ICollection<Incident> Incident { get; set; }
        public Contact Contact { get; set; }
    }
}
