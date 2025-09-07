using DotNetCoreThreeTier.Core.Contracts;
using DotNetCoreThreeTier.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCoreThreeTier.WebApis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserHandler _userHandler;
        public UserController(IUserHandler userHandler) 
        { 
            _userHandler = userHandler;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _userHandler.GetAllAsync();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _userHandler.GetByIdAsync(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] User user)
        {
            var response = await _userHandler.CreateAsync(user);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] User user)
        {
            var response = await _userHandler.UpdateAsync(user);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _userHandler.DeleteAsync(id);
            return Ok(response);
        }
    }
}
