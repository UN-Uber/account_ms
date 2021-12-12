using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using account_ms.Dtos;
using account_ms.Models;
using account_ms.Repositories;
using account_ms.Services;

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
            PassCrip passCrip = new PassCrip();
            var client = new Client
            {
                fName = createClientDto.fName,
                sName = createClientDto.sName,
                sureName = createClientDto.sureName,
                telNumber = createClientDto.telNumber,
                active = createClientDto.active,
                email = createClientDto.email,
                password = passCrip.hashPass(createClientDto.password),
                image = createClientDto.image
            };
            await _clientRepository.Add(client);
            return Ok(client.idClient);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteClient(int id)
        {
            var client = await _clientRepository.Get(id);
            if(client == null)
            {
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
                password = updateClientDto.password,
                image = updateClientDto.image
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

        [HttpGet("enter")]
        public async Task<ActionResult<AutenticateClientResponse>> Autenticate(AutenticateClientDto acd)
        {   
            var client;
            if(acd.email == ""){
                client = await _clientRepository.getEmail(acd.telNumber);
            }else{
                client = await _clientRepository.getEmail(acd.email);
            }

            AutenticateClientResponse acr = new AutenticateClientResponse();
            if(client == null){
                acr.response = "Email ni Numero encontrado";
                return Ok(acr);
            }else{
                VerifyPass verPass = new VerifyPass();
                acr.response = verPass.verify(acd, client);
                return Ok(acr);
            }

        }
    }
}