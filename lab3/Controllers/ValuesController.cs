using lab3.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace lab3.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ValuesController : ControllerBase
{
    private readonly UserManager<Student> _userManager;
    // private readonly AdminManager<Student> _adminManager;

    public ValuesController(UserManager<Student> userManager/*, AdminManager<Student> adminManager*/)
    {
        _userManager = userManager;
        // _adminManager = adminManager;
    }

    [HttpGet]
    [Authorize(Policy = "AllowAll")]
    public async Task<ActionResult> GetUserInfo()
    {
        Student? student = await _userManager.GetUserAsync(User);
        return Ok(new string[] { student!.UserName!, student.Email!, student!.Degree.ToString() });
    }

    [HttpGet]
    [Route("admin_only")]
    [Authorize(Policy = "AllowAdminsOnly")]
    public async Task<ActionResult> GetDegreeForManagers()
    {
        Student? student = await _userManager.GetUserAsync(User);
        return Ok(new string[] { student!.Degree.ToString() });
    }

    [HttpGet]
    [Route("user_only")]
    [Authorize(Policy = "AllowUsersOnly")]
    public async Task<ActionResult> GetDegreeForUsers()
    {
        Student? student = await _userManager.GetUserAsync(User);
        return Ok(new string[] { student!.Degree.ToString() });
    }

}
