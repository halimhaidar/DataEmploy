using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Security.Principal;

namespace DataEmploy.Models
{
    public class Employees
    {
        [Key]
        public string? NIK { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [Required]
        public string? Phone { get; set; }

        [Column(TypeName = "Date")]
        public DateTime BirthDate { get; set; }
        public int Salary { get; set; }
        public string? Email { get; set; }
        public Gender Gender { get; set; }

        // Self Reference of Table Employee - One To Many
        public Employees? Manager { get; set; }
        [ForeignKey("Manager")]
        public string? Manager_Id { get; set; }

        // Many employees have one departement - Many To One
        public Departments? Departements { get; set; }
        [ForeignKey("Departements")]
        public int? Departement_Id { get; set; }                              
        // One employee have one account  - One To One
        public Accounts? Account { get; set; }

    }
    public enum Gender 
    { 
        Male, 
        Female 
    }

}