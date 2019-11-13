using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GestionHuil.Models;
using AutoMapper;
using GestionHuil.Service;

namespace GestionHuil.Controllers
{
    [Route("api/achats")]
    [ApiController]
    public class AchatsController : Controller
    {
        private IAchatsService _achatService;
       // private ITemplateAchatGenerator _templateGenerator;

        public AchatsController(IAchatsService achatsService,
            IMapper mapper)
        {
            _achatService = achatsService;
          //  _templateGenerator = templateGenerator;
        }

        // GET: api/Achats

        [HttpGet]
        public async Task<IActionResult> GetAchats()
        {
            try
            {
                var achats = await _achatService.GetStockageAchatsAsync();
                return Ok(achats);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        // GET: api/Achats/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAchat([FromRoute] int id)
        {
            try
            {
                var achat = await _achatService.GetAchatByIdAsync(id);
                return Ok(achat);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

           
        }
       // GUT: api/Achats/trituration
       [HttpGet("trituration")]
        public async Task<IActionResult> GetTrituration()
        {
            try
            {
                var triturations = await _achatService.GetTrituration();
                return Ok(triturations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
       // GET Search: api/achat/search
       [HttpPut("search")]
        public async Task<IActionResult> GetSerchFicheReceptions([FromBody] Search search)
        {
            try
            {
                var achatSearch = await _achatService.GetSearchAchatAsync(search);
                return Ok(achatSearch);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        ////GET: api/Achat/pdfcreator/2
        //[HttpGet("pdfcreator/{id}")]
        //public async Task<ActionResult> CreatePDF([FromRoute] int id)
        //{
        //    var pdfCreator = await _achatService.Createpdf(id);
        //    if (pdfCreator == null)
        //    {
        //        return NotFound();
        //    }

        //    return File(pdfCreator.FileBytes, "application/pdf");
        //}

        // PUT: api/Triturations/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrituration([FromRoute] int id, [FromBody] Achat achat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != achat.Id)
            {
                return BadRequest();
            }

            try
            {
           
                if (!_achatService.AchatExists(id))
                {
                    return NotFound();
                }
                var trituration = await _achatService.GeTriturationByIdAchat(achat.TriturationId);
                await _achatService.UpdateAchatAsync(achat,trituration);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }


            //if (!AchatExists(id))
            //{
            //    return NotFound();
            //}
            //var achatBase = await _context.Achats.AsNoTracking().SingleOrDefaultAsync(i => i.Id == id);
            //var etriturationUpdate =  _context.Triturations.AsNoTracking().SingleOrDefault(c => c.Id == achat.TriturationId);
            //if (etriturationUpdate == null)
            //{
            //    return BadRequest(ModelState);
            //}
            //etriturationUpdate.HuileRestante = etriturationUpdate.HuileRestante + achatBase.QteAchete;

            //if (etriturationUpdate.HuileRestante >= achat.QteAchete)
            //{

            //    etriturationUpdate.HuileRestante = etriturationUpdate.HuileRestante - achat.QteAchete;
            //     _context.Entry(etriturationUpdate).State = EntityState.Modified;
            //    _context.Entry(achat).State = EntityState.Modified;
            //    _context.SaveChanges();
            //    var achats = await _context.Achats.Include(c => c.Trituration).Where(c => c.Trituration.VarieteId == achat.Trituration.VarieteId).ToListAsync();

            //    foreach (var achato in achats)
            //    {
            //        AllHuile = AllHuile + achato.QteAchete;
            //        sommeAllAchat = sommeAllAchat + achato.MontantAchat;
            //    }
            //    AllPrixUnitaire = sommeAllAchat / AllHuile;
            //    var StockProduithuile = _context.ProduitHuilles.SingleOrDefault(c => c.VarieteId == achat.Trituration.VarieteId);
            //    StockProduithuile.Qte_En_Stock = (StockProduithuile.Qte_En_Stock  + achatBase.QteAchete )- achat.QteAchete;
            //    StockProduithuile.Prix_unitaire = AllPrixUnitaire;
            //    _context.ProduitHuilles.Attach(StockProduithuile);
            //    await _context.SaveChangesAsync();



            //    return NoContent();

            //}
            //else
            //{
            //    return NotFound(new { message = "Le quantité en stock insuffisante" });
            //}







        }

        // POST: api/Achats
        [HttpPost]
        public async  Task<IActionResult> PostAchat([FromBody] Achat achat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _achatService.InsertAchatAsync(achat);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
            return CreatedAtAction("GetAchat", new { id = achat.Id }, achat);
            //var triturationUpdate = new Trituration();
            //var stokageHuile = new ProduitHuille();

            //int AllHuile = 0;
            //float AllPrixUnitaire = 0;
            //float sommeAllAchat = 0;


            //_context.Achats.Add(achat);
            //var etriturationUpdate = _context.Triturations.SingleOrDefault(c => c.Id == achat.TriturationId);
            //if (triturationUpdate == null)
            //{
            //    return BadRequest(ModelState);
            //}
            //etriturationUpdate.HuileRestante = etriturationUpdate.HuileRestante - achat.QteAchete;
            //_context.Triturations.Attach(etriturationUpdate);
            //_context.Entry(etriturationUpdate).Property(x => x.HuileRestante).IsModified = true;
            //await _context.SaveChangesAsync();
            //////////////////////////          Stockage Huile          ////////////////////////

            //var achats = await _context.Achats.Where(c => c.Trituration.VarieteId == achat.Trituration.VarieteId).ToListAsync();
            //foreach (var achato in achats)
            //{
            //    AllHuile = AllHuile + achato.QteAchete;
            //    sommeAllAchat = sommeAllAchat + achato.MontantAchat;
            //}
            //AllPrixUnitaire = sommeAllAchat / AllHuile;

            //var StockProduithuile = _context.ProduitHuilles.SingleOrDefault(c => c.VarieteId == achat.Trituration.VarieteId);


            //StockProduithuile.Qte_En_Stock = StockProduithuile.Qte_En_Stock + achat.QteAchete;
            //StockProduithuile.Prix_unitaire = AllPrixUnitaire;
            //_context.ProduitHuilles.Attach(StockProduithuile);
            //_context.Entry(StockProduithuile).Property(x => x.Qte_En_Stock).IsModified = true;
            //_context.Entry(StockProduithuile).Property(x => x.Prix_unitaire).IsModified = true;
            //await _context.SaveChangesAsync();

            //return CreatedAtAction("GetAchat", new { id = achat.Id }, achat);
        }

        // DELETE: api/Achats/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAchat([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var achatDb = await _achatService.GetAchatByIdAsync(id);
                if (achatDb == null)
                {
                    return BadRequest("Achat object is null");
                }
                await _achatService.DeleteAchatAsync(achatDb);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
            //var stokageHuile = new ProduitHuille();
            //int AllHuile = 0;
            //float AllPrixUnitaire = 0;
            //float sommeAllAchat = 0;



            //var achat = await _context.Achats.SingleOrDefaultAsync(i => i.Id == id);
            //if (achat == null)
            //{
            //    return NotFound();
            //}
            ////////////////////////////////////////////////////////////////
            //var etriturationUpdate = _context.Triturations.AsNoTracking().SingleOrDefault(c => c.Id == achat.TriturationId);
            //if (etriturationUpdate == null)
            //{
            //    return BadRequest(ModelState);
            //}
            //etriturationUpdate.HuileRestante = etriturationUpdate.HuileRestante + achat.QteAchete;


            //_context.Triturations.Attach(etriturationUpdate);
            //_context.Entry(etriturationUpdate).Property(x => x.HuileRestante).IsModified = true;
            //_context.Achats.Remove(achat);
            //_context.SaveChanges();
            //////////////////////////////////////////////////////////////
            //var achats = await _context.Achats.Where(c => c.Trituration.VarieteId == achat.Trituration.VarieteId).ToListAsync();
            //var StockProduithuile = _context.ProduitHuilles.SingleOrDefault(c => c.VarieteId == achat.Trituration.VarieteId);
            //if (achats != null || achats.Count != 0)
            //{
            //    foreach (var achato in achats)
            //    {
            //        AllHuile = AllHuile + achato.QteAchete;
            //        sommeAllAchat = sommeAllAchat + achato.MontantAchat;
            //    }
            //    AllPrixUnitaire = sommeAllAchat / AllHuile;
            //    if(sommeAllAchat == 0)
            //    {
            //        StockProduithuile.Qte_En_Stock = 0;
            //        StockProduithuile.Prix_unitaire = 0;
            //        _context.ProduitHuilles.Attach(StockProduithuile);
            //        await _context.SaveChangesAsync();
            //        return Ok(achat);
            //    }
            //     StockProduithuile.Qte_En_Stock = StockProduithuile.Qte_En_Stock - achat.QteAchete;
            //    StockProduithuile.Prix_unitaire = AllPrixUnitaire;
            //    _context.ProduitHuilles.Attach(StockProduithuile);
            //    await _context.SaveChangesAsync();
            //}
            //return Ok(achat);

        }
        [HttpGet("check/{id}")]
        public IActionResult GetCheckNew([FromRoute] int id)
        {
            try
            {
                if (_achatService.AchatHuileExitsFacture(id))
                {
                    return Ok(new { message = "Ne peut pas terminer cette action,car Achat existe en facture" });
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
        protected override void Dispose(bool disposing)
        {
            _achatService.Dispose();
            base.Dispose(disposing);
        }
    }
}