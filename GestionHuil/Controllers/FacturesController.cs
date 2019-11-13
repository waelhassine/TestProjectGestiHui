using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GestionHuil.Models;
using GestionHuil.Service;

namespace GestionHuil.Controllers
{
    [Route("api/facture")]
    [ApiController]
    public class FacturesController : Controller
    {
        private IFactureService _factureService;

        public FacturesController(IFactureService factureService)
        {
            _factureService = factureService;

        }

        // GET: api/Factures
        [HttpGet]
        public async Task<IActionResult> GetFactures()
        {
            try
            {
                var factures = await _factureService.GetFactureAsync();
                return Ok(factures);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        // GET: api/Factures/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFacture([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var facture = await _factureService.GetFactureByIdAsync(id);
                if (facture == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(facture);
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        [HttpGet("pdfcreator/{id}")]
        public async Task<ActionResult> CreatePdf([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                   var pdfCreator = await _factureService.Createpdf(id);
                    if (pdfCreator == null)
                    {
                        return NotFound();
                    }

                    return File(pdfCreator.FileBytes, "application/pdf");

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        // GET: api/Factures/Trituration/5
        [HttpGet("factureclient/{id}")]
        public async Task<IActionResult> GetFactureTriturationByClient([FromRoute] int id)
        {
            try
            {
                var factures = await _factureService.GetFactureByIdClientAsync(id);
                if (factures == null)
                {
                    return NotFound();
                }
                return Ok(factures);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }


            //        var facture = await _context.Factures.Include(c => c.Reglements).Include(c => c.TypeFacture).Where(c => c.ClientId == id).ToListAsync();

            //        foreach(var fac in facture)
            //        {
            //            float s = 0;
            //            foreach (var reg in fac.Reglements)
            //            {
            //                s = s + reg.Montant;
            //            }
            //            NewListFacture.Add(new FactureRessource
            //            {
            //                Id = fac.Id,
            //                Montant = fac.Montant,
            //                Date = fac.Date , 
            //                TypeFacture = fac.TypeFacture,
            //                TypeFactureId = fac.TypeFactureId,
            //                RestApayer = fac.Montant - s
            //});
            //        }
            //        if (NewListFacture == null)
            //        {
            //            return NotFound();
            //        }

            //        return Ok(NewListFacture);
        }
        // GET: api/Factures/Trituration/5
        //[HttpGet("achat/{id}")]
        //public async Task<IActionResult> GetFactureAchatByClient([FromRoute] int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var facture = await _context.Factures.Include(c => c.Reglements).Where(c => c.TypeFacture.Nom == "Achat").Where(c => c.Achat.Trituration.ClientId== id).ToListAsync();

        //    if (facture == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(facture);
        //}
        //// GET: api/Factures/Vente/5
        //[HttpGet("vente/{id}")]
        //public async Task<IActionResult> GetFactureVenteByClient([FromRoute] int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var facture = await _context.Factures.Include(c => c.Reglements).Where(c => c.TypeFacture.Nom == "Vente").Where(c => c.VenteHuile.ClientId == id).ToListAsync();

        //    if (facture == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(facture);
        //}

        //// PUT: api/Factures/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutFacture([FromRoute] int id, [FromBody] Facture facture)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != facture.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(facture).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!FactureExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Factures
        [HttpPost]
        public async Task<IActionResult> PostFacture([FromBody] Facture facture)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                if (facture == null)
                {
                    return BadRequest("facture object is null");
                }

                await _factureService.InsertFactureAsync(facture);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }


            return CreatedAtAction("GetFactureTriturationByClient", new { id = facture.Id }, facture);
           

            //_context.Factures.Add(facture);
            //await _context.SaveChangesAsync();

            //return CreatedAtAction("GetFacture", new { id = facture.Id }, facture);
        }

        //// DELETE: api/Factures/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteFacture([FromRoute] int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var facture = await _context.Factures.FindAsync(id);
        //    if (facture == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Factures.Remove(facture);
        //    await _context.SaveChangesAsync();

        //    return Ok(facture);
        //}

        //private bool FactureExists(int id)
        //{
        //    return _context.Factures.Any(e => e.Id == id);
        //}
        protected override void Dispose(bool disposing)
        {
            _factureService.Dispose();
            base.Dispose(disposing);
        }
    }
}