using Sieve.Services;

namespace EmployeeManagementSystem.Extension
{
    public class SieveConfigurationForEmployeeJobHistories: ISieveConfiguration
    {
        public void Configure(SievePropertyMapper mapper)
        {
            mapper.Property<Entities.Employee.Employee>(a => a.StartDate)
                 .CanFilter()
                 .CanSort();
            mapper.Property<Entities.Employee.Employee>(a => a.EndDate)
                     .CanFilter();
        }       

    }
}
