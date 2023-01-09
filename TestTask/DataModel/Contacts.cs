using System.ComponentModel.DataAnnotations;

namespace TestTask
{
    public class Contacts
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [Key]
        public string? Email { get; set; }
        public Accounts? Accounts{ get; set; }
    }
}
