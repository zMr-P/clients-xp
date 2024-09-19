using ClientXP.Application.Models;
using ClientXP.Application.Services;
using Microsoft.AspNetCore.Mvc;


namespace ClientXP.Presentation.Controllers
{
    [Controller]
    [Route("[Controller]")]
    public class ClientController : ControllerBase
    {
        private readonly ClientService _service;

        public ClientController(ClientService service)
        {
            _service = service;
        }

        [HttpGet("get-all")]
        public async Task<ActionResult> GetAll()
        {
            var dataClients = await _service.GetAllClientsAsync();

            if (dataClients != null && dataClients.Any())
            {
                return Ok(dataClients);
            }
            return NotFound("Não foi encontrado nehum cliente");
        }
        [HttpGet("get-by-id")]
        public async Task<ActionResult> GetById([FromQuery] int id)
        {
            var dataClient = await _service.GetByIdAsync(id);

            if (dataClient != null)
            {
                return Ok(dataClient);
            }
            return NotFound("Não foi encontrado um cliente com esse id");
        }
        [HttpPost("create")]
        public async Task<ActionResult> Create([FromBody] ClientModel client)
        {
            try
            {
                await _service.CreateAsync(client);
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
