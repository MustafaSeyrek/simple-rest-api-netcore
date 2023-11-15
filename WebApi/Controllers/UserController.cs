using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Database;
using WebApi.Models.Dtos;
using WebApi.Models.Entities;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        AppConnectionDbContext _connectionContext = new AppConnectionDbContext();
        

        [HttpGet]
        public IEnumerable<User> GetAllUsers()
        {
            return _connectionContext.Users.ToArray();
        }

        [HttpGet("{id}")]
        public ActionResult<UserResponseDto> GetUserById(int id)
        {
            var user = _connectionContext.Users.FirstOrDefault(x => x.Id == id);
            if (user == null) return NotFound();

            return Ok(new UserResponseDto { Id = user.Id, Name = user.Name, Surname = user.Surname, Email = user.Email });
        }

        [HttpPost]
        public ActionResult CreateNewUser(UserRequestDto userDto)
        {
            var user = new User { Name = userDto.Name, Surname = userDto.Surname, Email = userDto.Email, Password = userDto.Password };
            _connectionContext.Users.Add(user);
            _connectionContext.SaveChanges();
            return Ok(user);

        }

        [HttpPut("{id}")]
        public ActionResult UpdateUserById(int id, UserRequestDto userDto)
        {
            var user = _connectionContext.Users.FirstOrDefault(x => x.Id == id);
            if (user == null) return NotFound();

            user.Name = userDto.Name;
            user.Surname = userDto.Surname;
            user.Email = userDto.Email;
            user.Password = userDto.Password;
            _connectionContext.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteUserById(int id)
        {
            var user = _connectionContext.Users.FirstOrDefault(x => x.Id == id);
            if (user == null) return NotFound();

            _connectionContext.Remove(user);
            _connectionContext.SaveChanges();

            return NoContent();
        }

    }
}
