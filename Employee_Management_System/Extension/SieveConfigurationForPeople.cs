using Sieve.Services;

namespace EmployeeManagementSystem.Extension
{
    public class SieveConfigurationForPeople :ISieveConfiguration
    {
        public void Configure(SievePropertyMapper mapper)
        {
            mapper.Property<Entities.Employee.People>(a => a.FirstName)
                 .CanFilter()
                 .CanSort();
            mapper.Property<Entities.Employee.People>(a => a.LastName)
                 .CanFilter()
                 .CanSort();
            mapper.Property<Entities.Employee.People>(a => a.Email)
                 .CanFilter()
                 .CanSort();
            mapper.Property<Entities.Employee.People>(a => a.Address)
                 .CanFilter()
                 .CanSort();
        }
    }
}
