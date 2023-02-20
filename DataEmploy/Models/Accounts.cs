using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataEmploy.Models
{
    public class Accounts
    {
        [Key, ForeignKey("Employees")]
        public string? NIK { get; set; }
        [JsonIgnore]
        public virtual Employees Employees { get; set; }
        public string? Password { get; set; }
        [JsonIgnore]
        public virtual ICollection<AccountRoles> AccountRoles { get; set; }
    }
}
