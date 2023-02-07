using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Security.Principal;

namespace DataEmploy.Models
{
    public class AccountRoles
    {
        [ForeignKey("Roles")]
        public int Role_Id { get; set; }
        public Roles Roles { get; set; }

        [ForeignKey("Accounts")]
        public string NIK { get; set; }
        public Accounts Accounts { get; set; }
    }
}
