using Application.Users;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class UsersController : BaseApiController
    {
        [HttpPut("{id}")]
        public async Task<IActionResult> EditUser(Guid id, UserDto user)
        {
            user.Id = id;
            return HandleResult(await Mediator.Send(
                new Application.Users.Commands.Edit.Command
                {
                    User = user,
                }));
        }
    }
}