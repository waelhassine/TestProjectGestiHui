using AutoMapper;
using AutoMapper.Configuration;
using GestionHuil.Controllers.Ressources;
using GestionHuil.Data;
using GestionHuil.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GestionHuil.Controllers
{
    [Authorize]
    [Route("api/employee")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly DataContext _context;
        private IEmployeeService _employeeService;
        private IMapper _mapper;
        public IConfiguration Configuration { get; set; }

        public EmployeesController(IEmployeeService employeeService,
            IMapper mapper, DataContext context)
        {
            _employeeService = employeeService;
            _mapper = mapper;
            _context = context;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]EmployeeRessource employeeDto)
        {
            var employee = _employeeService.Authenticate(employeeDto.Email, employeeDto.Password);

            if (employee == null)
                return BadRequest(new { message = "L'identifiant ou le mot de passe est incorrect" });

            if (employee.Status == "Quitter")
                return BadRequest(new { message = "Vous arrêtez de travailler dans notre entreprise" });

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("aszo&123azertyuiopqsd");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, employee.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // return basic user info (without password) and token to store client side
            return Ok(new
            {
                Id = employee.Id,
                Email = employee.Email,
                NomPrenom = employee.NomPrenom,
                Photo = employee.Photo,
                Tel = employee.Tel,
                Fonction = employee.Fonction,
                Status = employee.Status,
                Token = tokenString
            });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody]EmployeeRessource employeeDto)
        {
            // map dto to entity
            var user = _mapper.Map<Employee>(employeeDto);

            try
            {
                // save 
                _employeeService.Create(user, employeeDto.Password);
                return Ok();
            }
            catch (Exception ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var employees = _employeeService.GetAll();
            var employeesDtos = _mapper.Map<IList<EmployeeRessource>>(employees);
            return Ok(employeesDtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var employee = _employeeService.GetById(id);
            var employeeDto = _mapper.Map<EmployeeRessource>(employee);
            return Ok(employeeDto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]EmployeeRessource employeeDto)
        {
            // map dto to entity and set id
            var employee = _mapper.Map<Employee>(employeeDto);
            employee.Id = id;

            try
            {
                // save 
                _employeeService.Update(employee, employeeDto.Password);
                return Ok();
            }
            catch (Exception ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _employeeService.Delete(id);
            return Ok();
        }
       

    }
}
