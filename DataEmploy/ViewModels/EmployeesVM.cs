using DataEmploy.Models;

namespace DataEmploy.ViewModels
{
    public class EmployeesVM
    {
        public string? NIK { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? Salary { get; set; }
        public Gender Gender { get; set; }
        public string? RoleName  { get; set; }
        public string? DepartmentName { get; set; }
        public string? manager_Id { get; set; }
        public string? Manager_Name { get; set; }
    }
}
