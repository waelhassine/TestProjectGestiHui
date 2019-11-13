using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GestionHuil.Models;
using GestionHuil.Service;

namespace GestionHuil.Controllers
{
    [Route("api/clients")]
    [ApiController]
    public class ClientsController : Controller
    {
        private IClientService _clientRepository;

        public ClientsController(IClientService clientRepository)
        {
            _clientRepository = clientRepository;
        }

        // GET: api/Clients
        [HttpGet]
        public async Task<IActionResult>  GetClients()
        {
            try
            {
                var clients = await _clientRepository.GetClientsAsync();
                return Ok(clients);
            }
            catch(Exception ex)
            {
               return StatusCode(500, ex);
            }
            
        }

        // GET: api/Clients/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClient([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var client = await _clientRepository.GetClientByIdAsync(id);
                if(client == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(client);
                }

            }
            catch(Exception ex)
            {
                return StatusCode(500, ex);
            }

        }

        // PUT: api/Clients/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClient([FromRoute] int id, [FromBody] Client client)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                if (client == null)
                {
                    return BadRequest("Client object is null");
                }
                if (!_clientRepository.ClientExists(id))
                {
                    return NotFound();
                }
               await _clientRepository.UpdateClientAsync(client);
               return NoContent();
            } catch(Exception ex)
            {
                return StatusCode(500, ex);
            }
       

           
        }

        // POST: api/Clients
        [HttpPost]
        public async Task<IActionResult> PostClient([FromBody] Client client)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                if(client == null)
                {
                    return BadRequest("Client object is null");
                }
                if (_clientRepository.ClientExistByNumber(client.Tel))
                {
                    return BadRequest("Client existe en systéme");
                }

                await _clientRepository.InsertClientAsync(client);
            }
            catch( Exception ex)
            {
                return StatusCode(500, ex);
            }
           

            return CreatedAtAction("GetClient", new { id = client.Id }, client);
        }

        // DELETE: api/Clients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient([FromRoute] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                var client = await _clientRepository.GetClientByIdAsync(id);
                if (client == null)
                {
                    return BadRequest("Client object is null");
                }
                await _clientRepository.DeleteClientAsync(client);
                return NoContent();
            } catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
           

        } 
        protected override void Dispose(bool disposing)
        {
            _clientRepository.Dispose();
            base.Dispose(disposing);
        }

    }
}