using Data_Access_Layer.Model;
using Entities.Employee;
using Infrastructure.Common.ViewModel.Employee;
using Sieve.Services;

namespace EmployeeManagementSystem.Extension
{
    public class SieveConfigurationForEmployee : ISieveConfiguration
    {
        public void Configure(SievePropertyMapper mapper)
        {
            mapper.Property<Employee>(a => a.EmployeeCode)
                 .CanFilter()
                 .CanSort();
            mapper.Property<Employee>(a => a.StartDate)
                 .CanFilter()
                 .CanSort();
            mapper.Property<Employee>(a => a.EndDate)
                 .CanFilter()
                 .CanSort();
            mapper.Property<Employee>(a => a.Salary)
                 .CanFilter()
                 .CanSort();
            mapper.Property<Employee>(a => a.People.FirstName)
                 .CanFilter()
                  .HasName(nameof(GetAllEmployeesResponseModel.FirstName))
                  .CanSort();

            mapper.Property<Employee>(a => a.People.LastName)
                .CanFilter()
                .HasName(nameof(GetAllEmployeesResponseModel.LastName))
                .CanSort();
        }

    }
}
