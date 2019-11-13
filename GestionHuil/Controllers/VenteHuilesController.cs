using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GestionHuil.Models;
using GestionHuil.Service;

namespace GestionHuil.Controllers
{
    [Route("api/vente")]
    [ApiController]
    public class VenteHuilesController : Controller
    {
        private IVenteHuileService _venteHuileService;

        public VenteHuilesController(IVenteHuileService venteHuileService)
        {
            _venteHuileService = venteHuileService;
        }

        // GET: api/VenteHuiles
        [HttpGet]
        public async Task<IActionResult> GetVenteHuiles()
        {
            try
            {
                var venteHuile = await _venteHuileService.GetVenteHuileAsync();
                return Ok(venteHuile);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        [HttpGet("clients")]
        public async Task<IActionResult> GetClients()
        {
            try
            {
                var clients = await _venteHuileService.GetSClientsAsync();
                return Ok(clients);
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
                var varietes = await _venteHuileService.GetVarieteAsync();
                return Ok(varietes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        // GET Search: api/trituration/search
        [HttpPut("search")]
        public async Task<IActionResult> GetSerchVenteHuile([FromBody] Search search)
        {
            try
            {
                var venteHuileSearch = await _venteHuileService.GetSerchVenteHuileAsync(search);
                return Ok(venteHuileSearch);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        // GET: api/VenteHuiles/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVenteHuile([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var venteHuile = await _venteHuileService.GetVenteHuileByIdAsync(id);
                if (venteHuile == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(venteHuile);
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        // PUT: api/VenteHuiles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVenteHuile([FromRoute] int id, [FromBody] VenteHuile venteHuile)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                if (venteHuile == null)
                {
                    return BadRequest("Vente Huile object is null");
                }
                if (!_venteHuileService.VenteHuileExists(id))
                {
                    return NotFound();
                }
                if (_venteHuileService.VenteHuileFacture(id))
                {
                    return BadRequest(new { error = "Ne peut pas terminer cette action , car Vente Huile existe en facture" });
                    
                   // return BadRequest("Ne peut pas terminer cette action , car Vente Huile existe en facture");
                }
                if(_venteHuileService.CheckQantityHuileUpdateInf(venteHuile))
                {
                    return BadRequest("Quantité insifisante");
                }
                await _venteHuileService.UpdateVenteHuileAsync(venteHuile);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
            //var stockagehuile = await _context.ProduitHuilles.Where(h => h.VarieteId == venteHuile.VarieteId).SingleOrDefaultAsync();
            //var venthuileDb = await _context.VenteHuiles.AsNoTracking().SingleOrDefaultAsync(c => c.Id == id);
            //stockagehuile.Qte_En_Stock = stockagehuile.Qte_En_Stock + venthuileDb.Qte_Vente;
            //if (stockagehuile.Qte_En_Stock < venteHuile.Qte_Vente)
            //{
            //    Ok("Quantité insifisante");
            //}
            //stockagehuile.Qte_En_Stock = stockagehuile.Qte_En_Stock - venteHuile.Qte_Vente;
            //_context.Entry(stockagehuile).State = EntityState.Modified;
            //_context.Entry(venteHuile).State = EntityState.Modified;

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!VenteHuileExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            //return NoContent();
        }

        // POST: api/VenteHuiles
        [HttpPost]
        public async Task<IActionResult> PostVenteHuile([FromBody] VenteHuile venteHuile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }
            if (venteHuile == null)
            {
                return BadRequest("Vente Huile object is null");
            }
            if (!_venteHuileService.CheckQantityHuileInf(venteHuile))
            {
                return BadRequest(new { error = "Quantité insifisante"});

            }
            try
            {
                await _venteHuileService.InsertVenteHuileAsync(venteHuile);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
            return CreatedAtAction("GetVenteHuile", new { id = venteHuile.Id }, venteHuile);
        //    var stockagehuile =  await _context.ProduitHuilles.Where(h => h.VarieteId == venteHuile.VarieteId).SingleOrDefaultAsync();
        //    if (stockagehuile.Qte_En_Stock < venteHuile.Qte_Vente)
        //    {
        //        Ok("Quantité insifisante");
        //    }
        //    stockagehuile.Qte_En_Stock = stockagehuile.Qte_En_Stock - venteHuile.Qte_Vente;
        //    _context.Entry(stockagehuile).State = EntityState.Modified;
        //    _context.VenteHuiles.Add(venteHuile);

        //    await _context.SaveChangesAsync();
        //    return CreatedAtAction("GetVenteHuile", new { id = venteHuile.Id }, venteHuile);
        }
        [HttpGet("check/{id}")]
        public IActionResult GetCheckNew([FromRoute] int id)
        {
            try
            {
                if (_venteHuileService.VenteHuileFacture(id))
                {
                    return Ok(new { message = "Ne peut pas terminer cette action,car Vente Huile existe en facture" });
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
        // DELETE: api/VenteHuiles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVenteHuile([FromRoute] int id)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                var venteHuilea = await _venteHuileService.GetVenteHuileByIdAsync(id);
                if (venteHuilea == null)
                {
                    return BadRequest("Vente Huile object is null");
                }
                await _venteHuileService.DeletVenteHuileAsync(venteHuilea.Id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            //var venteHuile = await _context.VenteHuiles.FindAsync(id);
            //var stockagehuile = await _context.ProduitHuilles.Where(h => h.VarieteId == venteHuile.VarieteId).SingleOrDefaultAsync();
            //if (venteHuile == null)
            //{
            //    return NotFound();
            //}
            //stockagehuile.Qte_En_Stock = stockagehuile.Qte_En_Stock + venteHuile.Qte_Vente;
            //_context.Entry(stockagehuile).State = EntityState.Modified;
            //_context.VenteHuiles.Remove(venteHuile);
            //await _context.SaveChangesAsync();

            //return Ok(venteHuile);
        }

    }
}