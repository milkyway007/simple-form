using Application.Core;
using Application.Users.Validators;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Interfaces;

namespace Application.Users.Commands
{
    public class Edit
    {
        public class Command : IRequest<Result<Unit>>
        {
            public UserDto User { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.User).SetValidator(new UserValidator());
            }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly IDataContext _context;
            private readonly IMapper _mapper;

            public Handler(IDataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _context.Users
                    .Include(x => x.SectorOptions)
                    .SingleOrDefaultAsync(x => x.Id == request.User.Id, cancellationToken);

                var shouldAdd = user == null;
                if (shouldAdd)
                {
                    user = new User();
                }

                _mapper.Map(request.User, user);

                var requestUserOptionIds = request.User.SectorOptionIds;
                var dbUserOptionIds = user.SectorOptions.Select(x => x.SectorOptionId) ?? new List<Guid>();
                var optionIdsToAdd = requestUserOptionIds.Except(dbUserOptionIds);
                var optionIdsToRemove = dbUserOptionIds.Except(requestUserOptionIds);

                var optionsToAdd = _context.SectorOptions
                    .Where(x => optionIdsToAdd.Contains(x.Id));
                foreach (var option in optionsToAdd)
                {
                    var userSectorOption = new UserSectorOption
                    {
                        UserId = user.Id,
                        SectorOptionId = option.Id,
                        SectorOption = option,
                    };

                    user.SectorOptions.Add(userSectorOption);
                }

                var optionsToRemove = user.SectorOptions.Where(x => optionIdsToRemove.Contains(x.SectorOptionId));
                foreach (var option in optionsToRemove.ToArray())
                {
                    user.SectorOptions.Remove(option);
                }

                if (shouldAdd)
                {
                    await _context.Users.AddAsync(user, cancellationToken);
                }

                var result = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (!result)
                {
                    return Result<Unit>.Failure("Failed to edit user.");
                }

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}