using Application.Core;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Interfaces;

namespace Application.SectorOptions.Queries
{
    public class List
    {
        public class Query : IRequest<Result<List<SectorOption>>>
        {
        }

        public class Handler : IRequestHandler<Query, Result<List<SectorOption>>>
        {
            private readonly IDataContext _context;
            private readonly IMapper _mapper;

            public Handler(IDataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<List<SectorOption>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var query = _context.SectorOptions
                    .Include(x => x.Children)
                    .ThenInclude(x => x.Children)
                    .ThenInclude(x => x.Children)
                    .Where(x => x.Level == 1);

                var list = await query.ToListAsync(cancellationToken);

                var ordered = OrderByLabel(list).ToList();

                return Result<List<SectorOption>>.Success(ordered);
            }

            private IEnumerable<SectorOption> OrderByLabel(IEnumerable<SectorOption> sectorOptions)
            {
                if (sectorOptions == null || sectorOptions.Count() == 0)
                {
                    return Enumerable.Empty<SectorOption>();
                }

                var ordered = sectorOptions.OrderBy(x => x.Label);

                foreach (var option in sectorOptions)
                {
                    option.Children = OrderByLabel(option.Children);
                }

                return ordered;
            }
        }
    }
}
