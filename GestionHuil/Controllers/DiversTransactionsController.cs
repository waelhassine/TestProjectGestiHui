using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GestionHuil.Models;
using GestionHuil.Service;

namespace GestionHuil.Controllers
{
    [Route("api/diverstransactions")]
    [ApiController]
    public class DiversTransactionsController : Controller
    {
        public IDiversTransactionsService _diversRepository;

        public DiversTransactionsController(IDiversTransactionsService diversRepository)
        {
            _diversRepository = diversRepository;
        }

        // GET: api/DiversTransactions
        [HttpGet]
        public async Task<IActionResult> GetDiversTransactions()
        {
            try
            {
                var divers = await _diversRepository.GetDiversTransactionAsync();
                return Ok(divers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

        }
 
        [HttpGet("client/{id}")]
        public async Task<IActionResult> GetDiversTransactionsByClient([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var divers = await _diversRepository.GetDiversTransactionsByClient(id);
                if (divers == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(divers);
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

        }
        // GET: api/DiversTransactions/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDiversTransaction([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var divers = await _diversRepository.GetDiversTransactionsByIdAsync(id);
                if (divers == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(divers);
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        // PUT: api/DiversTransactions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDiversTransaction([FromRoute] int id, [FromBody] DiversTransaction diversTransaction)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                if (diversTransaction == null)
                {
                    return BadRequest("Client object is null");
                }
                if (!_diversRepository.DiversTransactionExists(id))
                {
                    return NotFound();
                }
                await _diversRepository.UpdateDiversTransactionAsync(diversTransaction);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        // POST: api/DiversTransactions
        [HttpPost]
        public async Task<IActionResult> PostDiversTransaction([FromBody] DiversTransaction diversTransaction)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                if (diversTransaction == null)
                {
                    return BadRequest("Client object is null");
                }

                await _diversRepository.InsertDiversTransactionAsync(diversTransaction);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }


            return CreatedAtAction("GetDiversTransaction", new { id = diversTransaction.Id }, diversTransaction);
        }

        // DELETE: api/DiversTransactions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiversTransaction([FromRoute] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                var diversTransaction = await _diversRepository.GetDiversTransactionsByIdAsync(id);
                if (diversTransaction == null)
                {
                    return BadRequest("Client object is null");
                }
                await _diversRepository.DeleteDiversTransactionAsync(diversTransaction);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        // GET Search: api/trituration/search
        [HttpPut("search")]
        public async Task<IActionResult> GetSerchFicheReceptions([FromBody] Search search)
        {
            try
            {
                var achatSearch = await _diversRepository.GetSearchDiversTransactionAsync(search);
                return Ok(achatSearch);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        // GET: api/Triturations/pdfcreator/2

        [HttpGet("pdfcreator/{id}")]
        public async Task<ActionResult> CreatePDF([FromRoute] int id)
        {
            var pdfCreator = await _diversRepository.Createpdf(id);
            if (pdfCreator == null)
            {
                return NotFound();
            }

            return File(pdfCreator.FileBytes, "application/pdf");
        }
        protected override void Dispose(bool disposing)
        {
            _diversRepository.Dispose();
            base.Dispose(disposing);
        }


    }
}