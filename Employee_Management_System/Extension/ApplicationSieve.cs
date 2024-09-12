using Microsoft.Extensions.Options;
using Sieve.Models;
using Sieve.Services;

namespace EmployeeManagementSystem.Extension
{
    public class ApplicationSieve : SieveProcessor
    {
        public ApplicationSieve(IOptions<SieveOptions> options) : base(options)
        {
            
        }

        protected override SievePropertyMapper MapProperties(SievePropertyMapper mapper)
        {
            return mapper.ApplyConfigurationsFromAssembly(typeof(ApplicationSieve).Assembly);
        }
    }
}
