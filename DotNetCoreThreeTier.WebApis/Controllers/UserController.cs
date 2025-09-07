using DotNetCoreThreeTier.Core.Contracts;
using DotNetCoreThreeTier.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCoreThreeTier.WebApis.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userHandler) 
        {
            _userService = userHandler;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _userService.GetAllAsync();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _userService.GetByIdAsync(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] User user)
        {
            var response = await _userService.CreateAsync(user);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] User user)
        {
            var response = await _userService.UpdateAsync(user);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _userService.DeleteAsync(id);
            return Ok(response);
        }
    }
}
