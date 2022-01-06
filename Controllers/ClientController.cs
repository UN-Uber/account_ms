using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
//using static System.Exception;
using System.Threading.Tasks;
using account_ms.Dtos;
using account_ms.Models;
using account_ms.Repositories;
using account_ms.Services;
using System.Linq;

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
        public async Task<ActionResult<AutenticateClientResponse>> CreateClient(CreateClientDto createClientDto)
        {
            PassCrip passCrip = new PassCrip();
            AutenticateClientResponse acr = new AutenticateClientResponse();
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
            try{
                await _clientRepository.Add(client);
                acr.response = client.idClient.ToString();
                return Ok(acr);
            }catch{
                acr.response = "Email or phone number already register";
                return Ok(acr);
            }
            
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
            PassCrip passCrip = new PassCrip();
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
                image = updateClientDto.image,
                password = ""
            };

            //Pequeña corrección en la contraseña para que admita cambios sin moficar la contraseña.
            if(updateClientDto.password != ""){
                    client.password = passCrip.hashPass(updateClientDto.password);
            }

            try{
                await _clientRepository.Update(client);
                return Ok();
            }catch{
                return BadRequest("Email or number have already been registered");
            }
            
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

        [HttpPost("enter")]
        public async Task<ActionResult<Object>> Autenticate(AtenticateClientDto acd)
        {   
            var client = new Client();
            AutenticateClientResponse acr = new AutenticateClientResponse();
            if(acd.email == null || acd.email == ""){
                try{
                    long number = long.Parse(acd.telNumber);
                    client = await _clientRepository.getTelNumber(number);
                }catch{
                    acr.response = "Wrong Data";
                    return Ok(acr);
                }
            }else{
                client = await _clientRepository.getEmail(acd.email);
            }

            if(client.fName == null){
                acr.response = "Email or not Number found";
                return Ok(acr);
            }else{
                VerifyPass verPass = new VerifyPass();
                bool response = verPass.verify(acd, client);
                if(response)
                {
                    return Ok(client);
                }else{
                    acr.response = "Incorrect Password";
                    return Ok(acr);    
                }
            }
        }

        [HttpPut("changePass/{id}")]
        public async Task<ActionResult<Object>> ChangePassword(ChangePasswordDto cpdto, int id)
        {
            AutenticateClientResponse acr = new AutenticateClientResponse();
            var clientEx = await _clientRepository.Get(id);
            PassCrip passCrip = new PassCrip();
            if (clientEx == null) { 
                return NotFound();
            }

            AtenticateClientDto acd = new AtenticateClientDto
            {
                email = cpdto.email,
                telNumber = cpdto.telNumber,
                password = cpdto.password
            }; 

            VerifyPass verPass = new VerifyPass();
            bool response = verPass.verify(acd, clientEx);
            if(!response){
                acr.response = "Wrong Old Password";
                return Ok(acr);
            }else{
                Client client = new Client
                {
                    idClient = id,
                    fName = cpdto.fName,
                    sName = cpdto.sName,
                    sureName = cpdto.sureName,
                    telNumber = long.Parse(cpdto.telNumber),
                    active = cpdto.active,
                    email = cpdto.email,
                    image = cpdto.image,
                    password = passCrip.hashPass(cpdto.newPassword)
                };

                try{
                    await _clientRepository.Update(client);
                    acr.response = "Password Changed";
                    return Ok(acr);
                }catch{
                    acr.response = "Email or number have already been registered";
                    return Ok(acr);
                }
            }
        }

    }
}