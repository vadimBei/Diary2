using Microsoft.EntityFrameworkCore;
using Records.Core.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Records.Core.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Record> Records { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
