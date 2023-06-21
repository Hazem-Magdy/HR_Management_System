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
                Employee newEmployee = new Employee()
                {
                    FirstName = employeeDTO.EmployeeFirstName,
                    LastName = employeeDTO.EmployeeLastName,
                    Phone = employeeDTO.EmployeePhone,
                    Email = employeeDTO.EmployeeEmail,
                    Password = employeeDTO.EmployeePassword,
                    Position = employeeDTO.EmployeePosition,
                    HiringDate = employeeDTO.EmployeeHiringDate,
                    Status = employeeDTO.EmployeeStatus,
                    OvertimeRate= employeeDTO.OvertimeRate,
                    RegularHoursPerDay= employeeDTO.RegularHoursPerDay,
                    SalaryPerHour = employeeDTO.SalaryPerHour,
                    WorkingDaysPerWeek = employeeDTO.WorkingDaysPerWeek,
                };

                if(employeeDTO.EmployeeProfileUrl != null )
                {
                    newEmployee.ProfileUrl = employeeDTO.EmployeeProfileUrl;
                }

                // add employee to database
                await _employeeService.AddAsync(newEmployee);


                // create user
                User user = new User();

                Random random = new Random();

                int randomNumber = random.Next(1, 100000);

                
                user.UserName = string.Concat(employeeDTO.EmployeeFirstName, employeeDTO.EmployeeLastName, randomNumber.ToString());
              
                user.Email = employeeDTO.EmployeeEmail;
                user.PhoneNumber = employeeDTO.EmployeePhone;
                try
                {
                    User dublicatedUser = await _userManager.FindByEmailAsync(employeeDTO.EmployeeEmail);
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

                User userExist = await _userManager.FindByEmailAsync(userDTO.Email);
                Employee employee = await _employeeService.GetByEmailAsync(userDTO.Email);


                if (userExist != null && await _userManager.CheckPasswordAsync(userExist, userDTO.Password))
                {
                    // claims
                    List<Claim> userClaims = new List<Claim>();
            
                    userClaims.Add(new Claim("Id", employee.Id.ToString()));
                    userClaims.Add(new Claim("Position", employee.Position));
                    userClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

                    
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
