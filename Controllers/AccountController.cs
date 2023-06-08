using HR_Management_System.DTO;
using HR_Management_System.Models;
using HR_Management_System.Services;
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

        public AccountController(UserManager<User> userManager,IEmployeeService employeeService)
        {
            _userManager = userManager;
            _employeeService = employeeService;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<CustomResultDTO>> Register(RegisterEmployeeDTO employeeDTO)
        {
            CustomResultDTO result = new CustomResultDTO();


            if (ModelState.IsValid)
            {
                // create Employee
                Models.Employee newEmployee = new Models.Employee()
                {
                    FirstName = employeeDTO.FirstName,
                    LastName = employeeDTO.LastName,
                    Salary= employeeDTO.Salary,
                    Phone = employeeDTO.Phone,
                    Email = employeeDTO.Email,
                    Password = employeeDTO.Password,
                    Position = employeeDTO.Position,
                    HiringDate = employeeDTO.HiringDate,
                    Status = employeeDTO.Status
                };

                if(employeeDTO.ProfileUrl != null )
                {
                    newEmployee.ProfileUrl = employeeDTO.ProfileUrl;
                }

                // add employee to database
                await _employeeService.AddAsync(newEmployee);


                // create user
                User user = new User();

                Random random = new Random();

                int randomNumber = random.Next(1, 100000);

                
                user.UserName = string.Concat(employeeDTO.FirstName, employeeDTO.LastName, randomNumber.ToString());
              
                user.Email = employeeDTO.Email;
                user.PhoneNumber = employeeDTO.Phone;
                try
                {
                    User dublicatedUser = await _userManager.FindByEmailAsync(employeeDTO.Email);
                }
                catch(Exception)
                {
                    result.IsPass = false;
                    result.Message = "user with the same email already exist.";
                    return result;
                }

                
                IdentityResult createResult = await _userManager.CreateAsync(user, newEmployee.Password);

                if (createResult.Succeeded)
                {
                    result.IsPass = true;
                    result.Message = "account created successfully.";
                }
            }
            else
            {
                result.IsPass = false;
                result.Message = "account created failed.";
                result.Data = ModelState;

            }
            return result;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<CustomResultDTO>> Login(LoginUserDTO userDTO)
        {
            CustomResultDTO result = new CustomResultDTO();


            if (ModelState.IsValid)
            {
                // check if user exist in the system

                User userExist = await _userManager.FindByNameAsync(userDTO.UserName);
                if (userExist != null && await _userManager.CheckPasswordAsync(userExist, userDTO.Password))
                {
                    // claims
                    List<Claim> userClaims = new List<Claim>();
                    userClaims.Add(new Claim(ClaimTypes.NameIdentifier, userExist.Id));
                    userClaims.Add(new Claim(ClaimTypes.Name, userExist.UserName));
                    userClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

                    // roles 
                    IList<string> roles = await _userManager.GetRolesAsync(userExist);

                    if (roles != null)
                    {
                        // add roles to claims
                        foreach (string role in roles)
                        {
                            userClaims.Add(new Claim(ClaimTypes.Role, role));
                        }
                    }
                    // generate secret key
                    SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("frr656164971316cfrvv6f4v6f7v49fv46ftv6v"));
                    SigningCredentials userCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                    // represent token
                    JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                          issuer: "http://localhost:51006",
                          audience: "http://localhost:4200",
                          expires: DateTime.Now.AddHours(2),
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
                    result.Message = "token created successfully. ";
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
