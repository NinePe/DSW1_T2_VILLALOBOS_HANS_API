using Library.Application.DTOs;
using Library.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoansController : ControllerBase
{
    private readonly ILoanService _loanService;

    public LoansController(ILoanService loanService)
    {
        _loanService = loanService;
    }

    // Crear préstamo (aplica reglas de stock)
    [HttpPost]
    public async Task<ActionResult<LoanDto>> CreateLoan([FromBody] CreateLoanDto dto)
    {
        var loan = await _loanService.CreateLoanAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = loan.Id }, loan);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<LoanDto>> GetById(int id)
    {
        // Si quieres, puedes agregar un método GetByIdAsync en ILoanService
        return NotFound(); // placeholder si aún no lo implementas
    }

    // Devolver préstamo (actualiza stock)
    [HttpPost("{id:int}/return")]
    public async Task<ActionResult<LoanDto>> Return(int id)
    {
        var result = await _loanService.ReturnLoanAsync(id);
        return Ok(result);
    }
}
