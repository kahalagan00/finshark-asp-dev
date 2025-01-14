using api.Data;
using api.Dtos.Stock;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IStockRepository _stockRepo;
        public StockController(ApplicationDBContext context, IStockRepository stockRepo)
        {
            _stockRepo = stockRepo;
            _context = context; 
        }

        [HttpGet]   // OriginalRoute
        public async Task<IActionResult> GetAll()
        {
            // Deferred execution
            var stocks = await _stockRepo.GetAllSync();
            var stockDto = stocks.Select(s => s.ToStockDto());
            
            return Ok(stockDto);
        }

        [HttpGet("{id}")] // OriginalRoute/id
        public async Task<IActionResult> GetById([FromRoute] int id) 
        {
            var stock = await _stockRepo.GetByIdAsync(id);

            if (stock == null)
            {
                return NotFound();
            }

            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
        {
            var stock = stockDto.ToStockFromCreateDTO();
            await _stockRepo.CreateAsync(stock);
            return CreatedAtAction(nameof(GetById), new { id = stock.Id }, stock.ToStockDto());
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto)
        {
            var stock = await _stockRepo.UpdateAsync(id, updateDto);

            if (stock == null)
            {
                return NotFound();
            }

            return Ok(stock.ToStockDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var stock = await _stockRepo.DeleteAsync(id);

            if (stock == null)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}
