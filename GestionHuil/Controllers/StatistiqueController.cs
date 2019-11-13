using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestionHuil.Models;
using GestionHuil.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionHuil.Controllers
{
    [Route("api/statistique")]
    [ApiController]
    public class StatistiqueController : ControllerBase
    {
        private IStatistiqueService _statistiqueRepository;

        public StatistiqueController(IStatistiqueService statistiqueRepository)
        {
            _statistiqueRepository = statistiqueRepository;
        }

        // GET: api/Clients
        [HttpGet]
        public async Task<IActionResult> GetClients()
        {
            try
            {
                var clients = await _statistiqueRepository.GetAllStatistiqueAsync();
                return Ok(clients);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

        }
        [HttpPost]
        public async Task<IActionResult> GetClient([FromBody] TimeByWeek time)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var client = await _statistiqueRepository.GetAllStatistiqueByWeekAsync(time);
                if (client == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(client);
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

        }
    }
}
