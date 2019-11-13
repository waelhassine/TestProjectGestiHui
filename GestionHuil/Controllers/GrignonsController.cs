using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GestionHuil.Models;
using GestionHuil.Service;

namespace GestionHuil.Controllers
{
    [Route("api/grignons")]
    [ApiController]
    public class GrignonsController : Controller
    {
        private IGrignonsService _grignonsRepository;

        public GrignonsController(IGrignonsService grignonsRepository)
        {
            _grignonsRepository = grignonsRepository;
        }

        // GET: api/Grignons
        [HttpGet]
        public async Task<IActionResult> GetGrignons()
        {
            try
            {
                var grignons = await _grignonsRepository.GetGrignonsAsync();
                return Ok(grignons);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

        }
        [HttpGet("check/{id}")]
        public IActionResult GetCheckNew([FromRoute] int id)
        {
            try
            {
                if (_grignonsRepository.GrigonsFacture(id))
                {
                    return Ok(new { message = "Ne peut pas terminer cette action,car Vente Grignos en facture" });
                }
                else
                {
                    return Ok(new { message = "Ok" });
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        // GET: api/Grignons/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGrignon([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var grignons = await _grignonsRepository.GetGrignonsByIdAsync(id);
                if (grignons == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(grignons);
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        [HttpPut("search")]
        public async Task<IActionResult> GetSerchGrignons([FromBody] Search search)
        {
            try
            {
                var grignosSearch = await _grignonsRepository.GetSerchVenteHuileAsync(search);
                return Ok(grignosSearch);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        // PUT: api/Grignons/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGrignon([FromRoute] int id, [FromBody] Grignon grignon)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                if (grignon == null)
                {
                    return BadRequest("Client object is null");
                }
                if (!_grignonsRepository.GrignonsExists(id))
                {
                    return NotFound();
                }
                if (_grignonsRepository.GrigonsFacture(id))
                {
                    return BadRequest(new { error = "Ne peut pas terminer cette action , car Grigon existe en facture" });

                    // return BadRequest("Ne peut pas terminer cette action , car Vente Huile existe en facture");
                }
                await _grignonsRepository.UpdateGrignonsAsync(grignon);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        // POST: api/Grignons
        [HttpPost]
        public async Task<IActionResult> PostGrignon([FromBody] Grignon grignon)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                if (grignon == null)
                {
                    return BadRequest("Client object is null");
                }

                await _grignonsRepository.InsertGrignonsAsync(grignon);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }


            return CreatedAtAction("GetGrignon", new { id = grignon.Id }, grignon);
        }

        // DELETE: api/Grignons/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGrignon([FromRoute] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                var grignon = await _grignonsRepository.GetGrignonsByIdAsync(id);
                if (grignon == null)
                {
                    return BadRequest("Client object is null");
                }
                if (_grignonsRepository.GrigonsFacture(id))
                {
                    return BadRequest(new { error = "Ne peut pas terminer cette action , car Grigon existe en facture" });

                    // return BadRequest("Ne peut pas terminer cette action , car Vente Huile existe en facture");
                }
                await _grignonsRepository.DeleteGrignonsAsync(grignon);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        protected override void Dispose(bool disposing)
        {
            _grignonsRepository.Dispose();
            base.Dispose(disposing);
        }

    }
}