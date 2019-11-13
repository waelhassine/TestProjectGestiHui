using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GestionHuil.Models;
using GestionHuil.Service;

namespace GestionHuil.Controllers
{
    [Route("api/reglement")]
    [ApiController]
    public class ReglementsController : Controller
    {
        private IReglementService _reglementService;

        public ReglementsController(IReglementService reglementService)
        {
            _reglementService = reglementService;
        }

        // GET: api/Reglements
        [HttpGet]
        public async Task<IActionResult> GetReglements()
        {

            try
            {
                var reglements = await _reglementService.GetReglementsAsync();
                return Ok(reglements);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        // GET: api/Reglements/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReglement([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var reglement = await _reglementService.GetRegelementByIdAsync(id);
                if (reglement == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(reglement);
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        [HttpGet("factureTrituration/{id}")]
        public async Task<IActionResult> GetFactureReglement([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var reglements = await _reglementService.GetReglementByClient(id);
                if (reglements == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(reglements);
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        // PUT: api/Reglements/5


        // POST: api/Reglements
        [HttpPost]
        public async Task<IActionResult> PostReglement([FromBody] Reglement reglement)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                if (reglement == null)
                {
                    return BadRequest("Client object is null");
                }

                await _reglementService.InsertReglementAsync(reglement);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }


            return CreatedAtAction("GetReglement", new { id = reglement.Id }, reglement);
        }
        protected override void Dispose(bool disposing)
        {
            _reglementService.Dispose();
            base.Dispose(disposing);
        }
    }
}