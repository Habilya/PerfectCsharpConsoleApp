using Microsoft.AspNetCore.Mvc;
using Users.Api.Services;

namespace Users.Api.Controllers;

[ApiController]
public class TypicodeController : ControllerBase
{
	private readonly ITypicodeService _typicodeService;

	public TypicodeController(ITypicodeService typicodeService)
	{
		_typicodeService = typicodeService;
	}

	[HttpGet("typicodeusers")]
	public async Task<IActionResult> GetAll()
	{
		var users = await _typicodeService.GetAllUsersAsync();
		return Ok(users);
	}
}
