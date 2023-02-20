using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace DataEmploy.Models
{
    public class Roles
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        // Many to many
        [JsonIgnore]
        public virtual ICollection<AccountRoles>? AccountRoles { get; set; }
    }
}
