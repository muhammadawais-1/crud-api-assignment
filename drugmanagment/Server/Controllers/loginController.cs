using drugmanagment.Server.Database;
using drugmanagment.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace drugmanagment.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class loginController : ControllerBase
    {
        readonly private Drug_DBContext _db;

        public loginController(Drug_DBContext db)
        {
            _db = db;
        }

        [HttpPost("SignUp")]

        public IActionResult SignUp([FromBody] UserDto userDto)
        {
            User user = new User();
            try
            {
                var exist = _db.Users.Where(x => x.Email == userDto.Email).FirstOrDefault();
                if (exist == null)
                {
                    user.UserName = userDto.UserName;
                    user.Password = userDto.Password;
                    user.Email = userDto.Email;
                    user.CreatedOn = DateTime.Now;
                    user.ModifyOn = DateTime.Now;
                    user.IsDeleted = false;
                    _db.Users.Add(user);
                    _db.SaveChanges();
                    return Ok(user);
                }
                else
                {
                    return BadRequest("User Already Exist");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpPost("SignIn")]

        public IActionResult SignIn([FromBody] UserDto userDto)
        {
            User user = new User();
            try
            {
                var exist = _db.Users.Where(x => x.Email == userDto.Email && x.Password == userDto.Password).FirstOrDefault();
                if (exist != null)
                {
                    return Ok(user);
                }
                else
                {
                    return NotFound("User Not Found");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
