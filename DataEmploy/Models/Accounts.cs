using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataEmploy.Models
{
    public class Accounts
    {
        [Key, ForeignKey("Employees")]
        public string? NIK { get; set; }
        public Employees Employees { get; set; }
        public string? Password { get; set; }
        public virtual ICollection<AccountRoles> AccountRoles { get; set; }
    }
}
