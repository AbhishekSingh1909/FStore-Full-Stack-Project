using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fStore.Business;
using fStore.Core;
using Microsoft.AspNetCore.Mvc;

namespace fStore.Controller.src.Controller
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] LoginParams loginParams)
        {
            return Ok(await _authService.LoginAsync(loginParams));
        }
    }
}