using HR_Management_System.DTO;
using HR_Management_System.Models;
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

        public AccountController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<CustomResultDTO>> Register(RegisterUserDTO userDTO)
        {
            CustomResultDTO result = new CustomResultDTO();


            if (ModelState.IsValid)
            {
                // create
                User user = new User();

                Random random = new Random();

                int randomNumber = random.Next(1, 1000);

                user.UserName = string.Concat(userDTO.FirstName, userDTO.LastName, randomNumber.ToString());
                user.Email = userDTO.Email;
                user.PhoneNumber = userDTO.ContactNumber;
                //user.Role= userDTO.Role;
                //if(userDTO.UserPhoto != null)
                //{
                //    user.UserPhoto = userDTO.UserPhoto;
                //}
                try
                {
                    User dublicatedUser = await _userManager.FindByEmailAsync(userDTO.Email);
                }
                catch (Exception)
                {
                    result.IsPass = false;
                    result.Message = "user with the same email already exist.";
                    return result;
                }

                IdentityResult createResult = await _userManager.CreateAsync(user, userDTO.Password);
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
