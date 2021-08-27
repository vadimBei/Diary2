using Microsoft.EntityFrameworkCore;
using Records.Core.Application.Common.Dtos;
using Records.Core.Application.Common.Interfaces;
using Records.Core.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Records.Core.Application.Common.Services
{
    public class RecordService : IRecordService
    {
        private readonly IApplicationDbContext _dbContext;

        public RecordService(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Record> CreateAsync(RecordToCreateDto recordDto, CancellationToken cancellationToken)
        {
            var record = new Record
            {
                DateOfCreation = DateTime.Now,
                DateOfModification = DateTime.Now,
                IvKey = recordDto.IvKey,
                Name = recordDto.Name,
                Text = recordDto.EncryptedContent,
                UserId = recordDto.UserId
            };

            _dbContext.Records.Add(record);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return record;
        }

        public async Task<Record> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _dbContext.Records.FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
        }

        public async Task<Record> UpdateAsync(RecordToUpdateDto recordDto, CancellationToken cancellationToken)
        {
            var record = await GetByIdAsync(recordDto.Id, cancellationToken);

            record.DateOfModification = DateTime.Now;
            record.Name = recordDto.Name;
            record.Text = recordDto.EncryptedContent;
            record.IvKey = recordDto.IvKey;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return record;
        }
    }
}
