
using System.Collections.Generic;
using GestionHuil.Data;
using GestionHuil.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestionHuil.Controllers
{
    [Route("api/stockhuile")]
    [ApiController]
    public class ProduitHuileController : ControllerBase
    {
        // GET: api/ProduitHuile
        private readonly DataContext _context;

        public ProduitHuileController(DataContext context)
        {
            _context = context;
        }
        // GET: api/ProduitHuile
        [HttpGet]
        public IEnumerable<ProduitHuille> GetVenteHuiles()
        {
            return _context.ProduitHuilles.Include(c => c.Variete);
        }
    }
}
