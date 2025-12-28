using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Transaction;
using api.Helpers;
using api.Interfaces;
using api.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionRepository _TransactionRepository;
        private readonly IMapper _mapper;

        public TransactionController(ITransactionRepository TransactionRepository,IMapper mapper)
        {
            _TransactionRepository = TransactionRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery]TransactionQueryObject TransactionQueryObject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var transactions = await _TransactionRepository.GetAllAsync(TransactionQueryObject);
            var transactionDtos = _mapper.Map<List<TransactionDto>>(transactions);
            return Ok(transactionDtos);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var transaction = await  _TransactionRepository.GetByIdAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }

            var transactionDto = _mapper.Map<TransactionDto>(transaction);
            return Ok(transactionDto);
        }
        
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateTransactionDto CreateTransactionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var transaction = _mapper.Map<Transaction>(CreateTransactionDto);
            await _TransactionRepository.CreateAsync(transaction);
            return CreatedAtAction(nameof(GetById),new{id = transaction.Id},_mapper.Map<TransactionDto>(transaction));
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateTransactionDto UpdateTransactionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var update = await _TransactionRepository.UpdateAsync(id,UpdateTransactionDto);

            if (update==null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<TransactionDto>(update));
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var delete = await _TransactionRepository.DeleteAsync(id);

            if (delete==null)
            {
                return NotFound();
            }

            return NoContent();
        }



















    }
}