using HR_Management_System.DTO.Account;
using HR_Management_System.DTO.CustomResult;
using HR_Management_System.Models;
using HR_Management_System.Services.InterfacesServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HR_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IEmployeeService _employeeService;
        private readonly IConfiguration _configuration;
        public AccountController(UserManager<User> userManager,IEmployeeService employeeService, IConfiguration configuration)
        {
            _userManager = userManager;
            _employeeService = employeeService;
            _configuration = configuration;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<CustomResultDTO>> Login(LoginUserDTO userDTO)
        {
            CustomResultDTO result = new CustomResultDTO();


            if (ModelState.IsValid)
            {
                // check if user exist in the system

                User userExist = await _userManager.FindByEmailAsync(userDTO.Email);
                Employee employee = await _employeeService.GetByEmailAsync(userDTO.Email);


                if (userExist != null && await _userManager.CheckPasswordAsync(userExist, userDTO.Password))
                {
                    // claims
                    List<Claim> userClaims = new List<Claim>();
            
                    userClaims.Add(new Claim("Id", employee.Id.ToString()));
                    userClaims.Add(new Claim("Position", employee.Position.ToString()));
                    userClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

                    //get roles 
                    var roles = await _userManager.GetRolesAsync(userExist);
                    foreach (var userRole in roles)
                    {
                        userClaims.Add(new Claim(ClaimTypes.Role, userRole));
                    }

                    // generate secret key
                    SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:secretKey"]));
                    SigningCredentials userCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                    // represent token
                    JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                          issuer: _configuration["JWT:validIssuer"],
                          audience: _configuration["JWT:validAudience"],
                          expires: DateTime.Now.AddHours(8),
                          claims: userClaims,
                          signingCredentials: userCredentials
                          );
                    result.IsPass = true;
                    result.Data = new
                    {
                        // generate token
                        token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                        expiration = jwtSecurityToken.ValidTo
                    };
                    result.Message = "token created successfully.";
                }
                else
                {
                    result.IsPass = false;
                    result.Message = "you are not authorized";
                }
            }
            else
            {
                result.IsPass = false;
                result.Message = "Invalid login account.";
                result.Data = ModelState;
            }
            return result;
        }
    }
}
