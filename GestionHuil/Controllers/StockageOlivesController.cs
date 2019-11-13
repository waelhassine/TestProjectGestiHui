using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GestionHuil.Models;
using GestionHuil.Service;

namespace GestionHuil.Controllers
{
    [Route("api/stockageolive")]
    [ApiController]
    public class StockageOlivesController : Controller
    {
        private IStockageOlivesService _stockageOlivesRepository;

        public StockageOlivesController(IStockageOlivesService stockageOlivesRepository)
        {
            _stockageOlivesRepository = stockageOlivesRepository;
        }

        // GET: api/StockageOlives
        [HttpGet]
        public async Task<IActionResult> GetAllStockageOlives()
        {
            try
            {
                var stockageOlives = await _stockageOlivesRepository.GetStockageOliveAsync();
                return Ok(stockageOlives);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        [HttpGet("variete")]
        public async Task<IActionResult> GetVarietes()
        {
            try
            {
                var varietes = await _stockageOlivesRepository.GetAllVarietesAsync();
                return Ok(varietes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        // GET: api/StockageOlives/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStockageOlive([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var stockageOlive = await _stockageOlivesRepository.GetStockOlivesByIdAsync(id);
                if (stockageOlive == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(stockageOlive);
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        // PUT: api/StockageOlives/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStockageOlive([FromRoute] int id, [FromBody] StockageOlive stockageOlive)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                if (stockageOlive == null)
                {
                    return BadRequest("Stockage Olive object is null");
                }
                if (!_stockageOlivesRepository.StockageOliveExists(id))
                {
                    return NotFound();
                }
                if (!_stockageOlivesRepository.StockageOliveExistsTrituration(id))
                {
                    return BadRequest("Ne peut pas terminer cette action , car Trituration existe en facture");
                }
                await _stockageOlivesRepository.UpdateStockageOliveAsync(stockageOlive);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
       

        // POST: api/StockageOlives
        [HttpPost]
        public async Task<IActionResult> PostStockageOlives([FromBody] StockageOlive stockageOlives)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                if (stockageOlives == null)
                {
                    return BadRequest("Stockage Olive object is null");
                }

                await _stockageOlivesRepository.InsertStockOliveAsync(stockageOlives);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
            return CreatedAtAction("GetStockageOlive", new { id = stockageOlives.Id }, stockageOlives);
        }
        //  Get: api/stockOlives/pdfcreator/2
        // DELETE: api/Achats/5
        [HttpGet("check/{id}")]
        public IActionResult GetCheckNew([FromRoute] int id)
        {
            try
            {
                if (!_stockageOlivesRepository.StockageOliveExistsTrituration(id))
                {
                    return Ok(new { message = "Ne peut pas terminer cette action,car Stockage Olive existe en Trituration" });
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
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStockageOlives([FromRoute] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                var stockageOlive = await _stockageOlivesRepository.GetStockOlivesByIdAsync(id);
                if (stockageOlive == null)
                {
                    return BadRequest("Client object is null");
                }
                if (!_stockageOlivesRepository.StockageOliveExistsTrituration(id))
                {
                    return BadRequest("Ne peut pas terminer cette action , car Trituration existe en facture");
                }
                await _stockageOlivesRepository.DeleteStockOliveAsync(stockageOlive);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        // GET Search: api/stockageolives/search
        [HttpPut("search")]
        public async Task<IActionResult> GetSerchStockageOlive([FromBody] Search search)
        {
            try
            {
                var stockageOlives = await _stockageOlivesRepository.GetSerchStockageOliveAsync(search);
                return Ok(stockageOlives);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        [HttpGet("pdfcreator/{id}")]
        public async Task<IActionResult> Createpdf([FromRoute] int id)
        {
            var pdfCreator = await _stockageOlivesRepository.Createpdf(id);
            if (pdfCreator == null)
            {
                return NotFound();
           }

           return File(pdfCreator.FileBytes, "application/pdf");
        }
        protected override void Dispose(bool disposing)
        {
            _stockageOlivesRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}