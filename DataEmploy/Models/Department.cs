using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataEmploy.Models
{    
    public class Departments
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        [JsonIgnore]
        public virtual Employees? Employees { get; set; }
        [ForeignKey("Employees")]
        public string? Manager_Id { get; set; }
    }
}
