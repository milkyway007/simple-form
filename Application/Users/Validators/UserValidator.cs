using FluentValidation;

namespace Application.Users.Validators
{
    public class UserValidator : AbstractValidator<UserDto>
    {
        public UserValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
            RuleFor(x => x.SectorOptionIds).NotEmpty();
        }
    }
}
