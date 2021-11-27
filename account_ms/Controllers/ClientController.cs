using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using account_ms.Dtos;
using account_ms.Models;
using account_ms.Repositories;

namespace account_ms.Controllers
{   
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;

        public ClientController(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetClient(int id)
        {
            var client = await _clientRepository.Get(id);
            if (client == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(client);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetClients()
        {
            var clients = await _clientRepository.GetAll();
            return Ok(clients);
        }


        [HttpPost]
        public async Task<ActionResult> CreateClient(CreateClientDto createClientDto)
        {
            var client = new Client
            {
                fName = createClientDto.fName,
                sName = createClientDto.sName,
                sureName = createClientDto.sureName,
                telNumber = createClientDto.telNumber,
                active = createClientDto.active,
                email = createClientDto.email,
                password = createClientDto.password
            };
            await _clientRepository.Add(client);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteClient(int id)
        {
            var client = await _clientRepository.Get(id);
            if(client == null)
            {
                //var message = string.Format("Client with id = {0} was not found", id);
                //HttpError err = new HttpError(message);
                return NotFound();
            }
            await _clientRepository.Delete(id);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateClient(int id, UpdateClientDto updateClientDto)
        {
            var clientEx = await _clientRepository.Get(id);
            
            if (clientEx == null) { 
                return NotFound();
            }

            Client client = new Client
            {
                idClient = id,
                fName = updateClientDto.fName,
                sName = updateClientDto.sName,
                sureName = updateClientDto.sureName,
                telNumber = updateClientDto.telNumber,
                active = updateClientDto.active,
                email = updateClientDto.email,
                password = updateClientDto.password
            };

            await _clientRepository.Update(client);
            return Ok();
        }

        
        [HttpGet("cards/{id}")]
        public async  Task<ActionResult> GetCards(int id)
        {
            var client = await _clientRepository.Get(id);
            if (client == null)
            {
                return NotFound();
            }
            var cards = await _clientRepository.GetCards(id);
            return Ok(cards);
        } 
    }
}