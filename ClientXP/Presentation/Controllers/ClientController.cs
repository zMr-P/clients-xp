using ClientXP.Application.Models;
using ClientXP.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClientXP.Presentation.Controllers
{
    [Controller]
    [Route("[Controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _service;

        public ClientController(IClientService service)
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
            return NotFound("Não foi encontrado nehum cliente.");
        }

        [HttpGet("get-by-id")]
        public async Task<ActionResult> GetById([FromQuery] int id)
        {
            if (id > 0)
            {

                var dataClient = await _service.GetByIdAsync(id);

                if (dataClient != null)
                {
                    return Ok(dataClient);
                }
                return NotFound($"Não foi encontrado nenhum cliente com o id: {id}");
            }
            return BadRequest("O id passado é inválido!");
        }

        [HttpPost("create")]
        public async Task<ActionResult> Create([FromBody] ClientModel clModel)
        {
            if (clModel != null)
            {
                try
                {
                    await _service.CreateAsync(clModel);
                    return Ok("Sucesso ao criar cliente!");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest("Verifique os dados do cliente.");
        }

        [HttpPut("update")]
        public async Task<ActionResult> Update([FromQuery] int id, [FromBody] ClientModel clModel)
        {
            if (id > 0)
            {
                if (clModel != null)
                {
                    var dataClient = await _service.GetByIdAsync(id);
                    if (dataClient != null)
                    {
                        try
                        {
                            await _service.UpdateAsync(dataClient, clModel);
                            return Ok("Sucesso ao atualizar cliente!");
                        }
                        catch (Exception ex)
                        {
                            return BadRequest(ex.Message);
                        }
                    }
                    return NotFound($"Não foi encontrado nenhum cliente com o id: {id}");
                }
                return BadRequest("Verifique os dados do cliente.");
            }
            return BadRequest("O id passado é inválido!");
        }

        [HttpPatch("update-email")]
        public async Task<ActionResult> UpdateEmail([FromBody] ClientEmailModel emailModel)
        {
            if (!String.IsNullOrEmpty(emailModel.Email))
            {
                if (emailModel.Id > 0)
                {
                    var dataClient = await _service.GetByIdAsync(emailModel.Id);
                    if (dataClient != null)
                    {
                        try
                        {
                            await _service.UpdateEmailAsync(dataClient, emailModel.Email);
                            return Ok($"Sucesso ao alterar email do cliente: {dataClient.Name}");
                        }
                        catch (Exception ex)
                        {
                            return BadRequest(ex.Message);
                        }
                    }
                    return NotFound($"Não foi encontrado nenhum cliente com o id: {emailModel.Id}");
                }
                return BadRequest("O id passado é inválido");
            }
            return BadRequest("Email nulo ou vazio.");
        }

        [HttpDelete("delete")]
        public async Task<ActionResult> Delete([FromQuery] int id)
        {
            if (id > 0)
            {
                var dataClient = await _service.GetByIdAsync(id);
                if (dataClient != null)
                {
                    await _service.DeleteAsync(dataClient);
                    return Ok("Cliente deletado com sucesso");
                }
                return NotFound($"Não foi encontrado nenhum cliente com o id: {id}");
            }
            return BadRequest("O id passado é invalido");
        }
    }
}
