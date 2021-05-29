using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VerstaTestTask.Db
{
    public interface IDbInitializer
    {
        Task InitializeAsync();
    }

    public class EFDbInitializer : IDbInitializer
    {
        private readonly EFContext _context;

        public EFDbInitializer(EFContext context)
        {
            _context = context;
        }

        public async Task InitializeAsync()
        {
            await _context.Database.EnsureCreatedAsync();
        }
    }
}
