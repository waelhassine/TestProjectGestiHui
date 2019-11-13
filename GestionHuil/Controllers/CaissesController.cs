using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestionHuil.Data;
using GestionHuil.Models;
using GestionHuil.Service;

namespace GestionHuil.Controllers
{
    [Route("api/caisses")]
    [ApiController]
    public class CaissesController : Controller
    {
        private ICaisseService _caissesRepository;

        public CaissesController(ICaisseService caissesRepository)
        {
            _caissesRepository = caissesRepository;
        }



        // GET: api/Caisses
        [HttpGet]
        public async Task<IActionResult> GetCaisses()
        {
            try
            {
                var caisses = await _caissesRepository.GetCaissesAsync();
                return Ok(caisses);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        // GET: api/Caisses/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCaisse([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var caisses = await _caissesRepository.GetCaissesByIdAsync(id);
                if (caisses == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(caisses);
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

        }
        [HttpPut("search")]
        public async Task<IActionResult> GetSerchCaisse([FromBody] Search search)
        {
            try
            {
                var caisse = await _caissesRepository.GetSearchAchatAsync(search);
                return Ok(caisse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        // PUT: api/Caisses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCaisse([FromRoute] int id, [FromBody] Caisse caisse)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                if (caisse == null)
                {
                    return BadRequest("Client object is null");
                }
                if (!_caissesRepository.CaissesExists(id))
                {
                    return NotFound();
                }
                await _caissesRepository.UpdateCaissesAsync(caisse);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        // POST: api/Caisses
        [HttpPost]
        public async Task<IActionResult> PostCaisse([FromBody] Caisse caisse)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                if (caisse == null)
                {
                    return BadRequest("Client object is null");
                }

                await _caissesRepository.InsertCaissesAsync(caisse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }


            return CreatedAtAction("GetCaisse", new { id = caisse.Id }, caisse);
        }

        // DELETE: api/Caisses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCaisse([FromRoute] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                var caisses = await _caissesRepository.GetCaissesByIdAsync(id);
                if (caisses == null)
                {
                    return BadRequest("Client object is null");
                }
                await _caissesRepository.DeleteCaissesAsync(caisses);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        protected override void Dispose(bool disposing)
        {
            _caissesRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}