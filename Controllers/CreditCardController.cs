using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
//using static System.Exception;
using account_ms.Dtos;
using account_ms.Models;
using account_ms.Repositories;

namespace account_ms.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditCardController : ControllerBase
    {
        private readonly ICreditCardRepository _creditCardRepository;

        public CreditCardController(ICreditCardRepository creditCardRepository)
        {
            _creditCardRepository = creditCardRepository;   
        }

        // GET: api/<CreditCardController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CreditCard>>> GetCreditCards()
        {
            var cards = await _creditCardRepository.GetAll();   
            return Ok(cards);
        }

        // GET api/<CreditCardController>
        [HttpGet("{id}")]
        public async Task<ActionResult<CreditCard>> GetCreditCard(int id)
        {
            var card = await _creditCardRepository.Get(id);
            if(card == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(card);
            }
        }

        // POST api/<CreditCardController>
        [HttpPost]
        public async Task<ActionResult<AutenticateClientResponse>> CreateCreditCard(CreateCreditCardDtos createCreditCardDtos)
        {   
            AutenticateClientResponse acr = new AutenticateClientResponse();
            var card = new CreditCard
            {
                cardNumber = createCreditCardDtos.cardNumber,
                idClient = createCreditCardDtos.idClient,
                cvv = createCreditCardDtos.cvv,
                dueDate = createCreditCardDtos.dueDate
            };
            try{
                await _creditCardRepository.Add(card);
                acr.response = card.idCard.ToString();
                return Ok(acr);
            }catch{
                acr.response = "Card already register";
                return Ok(acr);
            }
        }

        // PUT api/<CreditCardController>
        [HttpPut("{id}")]
        public async Task<ActionResult>  UpdateCreditCard(int id, UpdateCreditCardDtos updateCreditCardDtos)
        {
            var cardEx = await _creditCardRepository.Get(id);
            if(cardEx== null)
            {
                return NotFound();
            }
            var card = new CreditCard
            {
                idCard = id,
                cardNumber = updateCreditCardDtos.cardNumber,
                idClient = updateCreditCardDtos.idClient,
                cvv = updateCreditCardDtos.cvv,
                dueDate = updateCreditCardDtos.dueDate
            };
            await _creditCardRepository.Update(card);
            return Ok();
        }

        // DELETE api/<CreditCardController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCreditCard(int id)
        {
            var card = await _creditCardRepository.Get(id);
            if(card == null)
            {
                return NotFound();
            }
            await _creditCardRepository.Delete(id);
            return Ok();
        }
    }
}
