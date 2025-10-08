using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KEShop_Api_N_Tier_Art.DAL.Models;
//using KEShop_Api_N_Tier_Art.DAL.Models; // تأكد من أن هذا السطر يشير إلى ApplictionUser


//namespace KEShop_Api_N_Tier_Art.PL.Areas.Identity.Controller
//{

//}

namespace KEShop_Api_N_Tier_Art.PL.Areas.Identity.Controllers
{
    [Area("Identity")]
    [Route("api/Identity/[controller]/[action]")]



    //[Route("api/Identity/")]
    [ApiController]



    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        // 1. قراءة كل المستخدمين
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _userManager.Users.ToList();
            return Ok(users);
        }

        // 2. قراءة مستخدم واحد بالمعرف (ID)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound($"User with ID {id} not found.");
            }
            return Ok(user);
        }

        // 3. إضافة مستخدم جديد (Register)
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] ApplicationUser newUser)
        {
            var result = await _userManager.CreateAsync(newUser);
            if (result.Succeeded)
            {
                return Ok("User created successfully.");
            }
            return BadRequest(result.Errors);
        }

        // 4. تعديل مستخدم موجود
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] ApplicationUser updatedUser)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound($"User with ID {id} not found.");
            }
            // تحديث الخصائص المراد تغييرها
            user.UserName = updatedUser.UserName;
            user.Email = updatedUser.Email;
            // يمكنك إضافة خصائص أخرى للتحديث

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return Ok("User updated successfully.");
            }
            return BadRequest(result.Errors);
        }

        // 5. حذف مستخدم
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound($"User with ID {id} not found.");
            }

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return Ok("User deleted successfully.");
            }
            return BadRequest(result.Errors);
        }
    }
}