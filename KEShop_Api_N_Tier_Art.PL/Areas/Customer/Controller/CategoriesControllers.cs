using KEShop_Api_N_Tier_Art.BLL.Services.Interfaces;
using KEShop_Api_N_Tier_Art.DAL.DTO.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KEShop_Api_N_Tier_Art.PL.Areas.Customer.Controllers
{
    // تم تغيير المسار ليتضمن اسم المنطقة (Area)
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Customer")]
    [Authorize(Roles = "Customer")]
    public class CategoriesControllers : ControllerBase
    {
        private readonly ICategoryService categoryService;

        public CategoriesControllers(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var categories = categoryService.GetAll();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var category = categoryService.GetById(id);
            if (category is null) return NotFound();
            return Ok(category);
        }
    }
}