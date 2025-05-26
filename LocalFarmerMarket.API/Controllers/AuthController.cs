using LocalFarmerMarket.Core.Models;
using LocalFarmerMarket.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LocalFarmerMarket.Core.Models.RequestDTOs;
using LocalFarmerMarket.API.Services;
using Microsoft.AspNetCore.Identity;
using Castle.Core.Resource;

namespace LocalFarmerMarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly CustomersRepository _customersRepo;
        private readonly FarmersRepository _farmersRepo;
        private readonly JwtService _jwtService;
        private readonly IPasswordHasher<User> _passwordHasher;




        public AuthController(CustomersRepository customersRepo,FarmersRepository farmersRepo, JwtService jwtService, IPasswordHasher<User> passwordHasher)
        {
            _customersRepo = customersRepo;
            _farmersRepo = farmersRepo;
            _jwtService = jwtService;
            _passwordHasher = passwordHasher;

        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequest request)
        {
            if (_customersRepo.GetFirstOrDefault(x => x.Username == request.Username) != null ||
                _farmersRepo.GetFirstOrDefault(x => x.Username == request.Username) != null)
            {
                return BadRequest(new { message = "Username is already taken." });
            }

            User user = new User
            {
                Username = request.Username,
                Password = _passwordHasher.HashPassword(null, request.Password),  //add hash password // Hash password for security
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Address = request.Address,
                PhoneNumber = request.PhoneNumber,
                Role = request.Role 
            };
            string token;



            if (request.Role.ToLower() == "customer")
            {
                Customer customer = new Customer
                {
                    Username = user.Username,
                    Password = user.Password,  //add hash password // Hash password for security
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Address = user.Address,
                    PhoneNumber = user.PhoneNumber,
                    Role = "Customer"
                };



                _customersRepo.Save(customer);
                token = _jwtService.GenerateToken(customer);
                return Created("", new { id = customer.Id, token = token, message = "Registration successful." });
            }
            else if (request.Role.ToLower() == "farmer")
            {
                Farmer farmer = new Farmer
                {
                    Username = user.Username,
                    Password = user.Password,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Address = user.Address,
                    PhoneNumber = user.PhoneNumber,
                    Role = "Farmer"
                };

                _farmersRepo.Save(farmer);
                token = _jwtService.GenerateToken(farmer);
                return Created("", new { id = farmer.Id,token=token, message = "Registration successful." });
            }
            else
            {
                return BadRequest(new { message = "Invalid role. Choose 'customer' or 'farmer'." });
            }
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {

            User user = _customersRepo.GetFirstOrDefault(x => x.Username == request.Username) as User
                      ?? _farmersRepo.GetFirstOrDefault(x => x.Username == request.Username) as User;

            if (user == null)
            {
                return BadRequest(new { message = "Invalid username." });
            }

            Console.WriteLine($"Found user: {user.Username}");


            var passwordVerification = _passwordHasher.VerifyHashedPassword(null, user.Password, request.Password);
            if (passwordVerification != PasswordVerificationResult.Success)
            {
                return Unauthorized(new { message = "Incorrect password." });
            }

            var token = _jwtService.GenerateToken(user);

            return Ok(new { Token = token, Role = user.Role, message = "Login successful." });
        }



        [HttpPost("logout")]
        public IActionResult Logout()
        {
            return Ok(new { message = "Успешен изход." });
        }   
    }
}
