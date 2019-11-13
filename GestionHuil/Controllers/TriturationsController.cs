using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GestionHuil.Models;
using GestionHuil.Controllers.Ressources;
using GestionHuil.Service;

namespace GestionHuil.Controllers
{
    [Route("api/trituration")]
    [ApiController]
    public class TriturationsController : Controller
    {
        private ITriturationsService _triturationsService;
        TriturationAllRessource triturationAllRessource;
        public TriturationsController(ITriturationsService triturationsService)
        {
            _triturationsService = triturationsService;
            triturationAllRessource = new TriturationAllRessource();
        }

        // GET: api/Triturations
        [HttpGet]
        public async Task<IActionResult> GetTriturations()
        {
            try
            {
                var triturations = await _triturationsService.GetStockageTriturationAsync();
                return Ok(triturations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        // GET: api/StockClient
        [HttpGet("check/{id}")]
        public IActionResult GetCheckNew([FromRoute] int id)
        {
            try
            {
                if (_triturationsService.TriturationExitsFacture(id))
                {
                    return Ok(new { message = "Ne peut pas terminer cette action,car Trituration existe en facture" });
                }
                else
                {
                    return Ok(new { message = "Ok"});
                    }


            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        [HttpGet("checkachat/{id}")]
        public IActionResult GetCheckAchatNew([FromRoute] int id)
        {
            try
            {
                if (_triturationsService.TriturationExitsAchat(id) && _triturationsService.TriturationExitsFacture(id))
                {
                    return Ok(new { message = "Ne peut pas terminer cette action,car Trituration existe en Achat" });
                }
                else if (_triturationsService.TriturationExitsAchat(id) && !_triturationsService.TriturationExitsFacture(id))
                {
                    return Ok(new { message = "Ne peut pas terminer cette action,car Trituration existe en Achat" });
                }
                else if (_triturationsService.TriturationExitsFacture(id))
                {
                    return Ok(new { message = "Ne peut pas terminer cette action,car Trituration existe en Facture" });
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
        // GET: api/StockClient
        [HttpGet("stockclient/{id}")]
        public async Task<IActionResult> GetStockageOliveNew([FromRoute] int id)
        {
            try
            {
                var stockageOlives = await _triturationsService.GetStockageOliveNew(id);
                return Ok(stockageOlives);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        // GET: api/Triturations/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTrituration([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var trituration = await _triturationsService.GetTriturationByIdAsync(id);
                if (trituration == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(trituration);
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        // PUT: api/Triturations/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrituration([FromRoute] int id, [FromBody] Trituration trituration)
        {
  
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                if (trituration == null)
                {
                    return BadRequest("Trituration object is null");
                }
                if (!_triturationsService.TriturationExists(id))
                {
                    return NotFound();
                }
                if (_triturationsService.TriturationExitsFacture(id))
                {
                    return BadRequest("Ne peut pas terminer cette action , car Trituration existe en facture");
                }
                await _triturationsService.UpdateTriturationsAsync(trituration);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        // POST: api/Triturations
        [HttpPost]
        public async Task<IActionResult> PostTrituration([FromBody] Trituration triturati)
        {
            if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                if (triturati == null)
                {
                    return BadRequest("Trituration object is null");
                }
            try
            {
                await _triturationsService.InsertTriturationAsync(triturati);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
            return CreatedAtAction("GetTrituration", new { id = triturati.Id }, triturati);

        }
        // GET Search: api/trituration/search
        [HttpPut("search")]
        public async Task<IActionResult> GetSerchFicheReceptions([FromBody] Search search)
        {
            try
            {
                var triturationSearch = await _triturationsService.GetSerchTriturationAsync(search);
                return Ok(triturationSearch);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        // GET: api/Triturations/pdfcreator/2

        //[HttpGet("pdfcreator/{id}")]
        //public async Task<ActionResult> CreatePdf([FromRoute] int id)
        //{
        //    var pdfCreator = await _triturationsService.Createpdf(id);
        //    if (pdfCreator == null)
        //    {
        //        return NotFound();
        //    }

        //    return File(pdfCreator.FileBytes, "application/pdf");
        //}

        //DELETE: api/Triturations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrituration([FromRoute] int id)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                var trituration = await _triturationsService.GetTriturationByIdAsync(id);
                if (trituration == null)
                {
                    return BadRequest("Trituration object is null");
                }
                await _triturationsService.DeletTriturationsAsync(trituration);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

            //var trituration = await _context.Triturations.FindAsync(id);
            //var stockolives = await _context.StockageOlives.Where(c => c.TriturationId == id).ToListAsync();
            //foreach (var stock in stockolives)
            //{
            //    stock.TriturationId = null;
            //    _context.StockageOlives.Update(stock);
            //}
            //if (trituration == null)
            //{
            //    return NotFound();
            //}

            // bool checkexist = triturationenachatexist(id);
            ///* if (checkexist == true)
            // {
            //     return notfound(new { message = "il faut supprimer dans le tableau achat " });
            // }*/
            //_context.Triturations.Remove(trituration);
            //await _context.SaveChangesAsync();

            //return Ok(trituration);
        }
        protected override void Dispose(bool disposing)
        {
            _triturationsService.Dispose();
            base.Dispose(disposing);
        }

    }
}