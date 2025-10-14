using ClienteAPI.Abstractions;
using Clientes.Data.Models;
using Clientes.Domain.DTO;
using Clientes.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClienteAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientesController : ControllerBase
{
    private readonly IClientesService _clientesService;

    public ClientesController(IClientesService clientesService)
    {
        _clientesService = clientesService;
    }

    // GET: api/Clientes
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
    {
        var clientes = await _clientesService.Listar(c => true);
        return Ok(clientes);
    }

    // GET: api/Clientes/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Cliente>> GetCliente(int id)
    {
        var cliente = await _clientesService.Buscar(id);
        if (cliente == null) return NotFound();
        return Ok(cliente);
    }

    // POST: api/Clientes
    [HttpPost]
    public async Task<ActionResult<Cliente>> PostCliente([FromBody] ClienteCreateDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var cliente = new Cliente
        {
            Nombres = dto.Nombres,
            Direccion = dto.Direccion,
            RNC = dto.RNC,
            LimiteCredito = dto.LimiteCredito,
            FechaIngreso = dto.FechaIngreso
        };

        var guardado = await _clientesService.Guardar(cliente);
        if (!guardado) return BadRequest("No se pudo guardar el cliente.");

        return CreatedAtAction(nameof(GetCliente), new { id = cliente.ClienteId }, cliente);
    }

    // PUT: api/Clientes/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCliente(int id, [FromBody] ClienteCreateDto dto)
    {
        var clienteExistente = await _clientesService.Buscar(id);
        if (clienteExistente == null) return NotFound();

        // Actualizar solo los campos permitidos
        clienteExistente.Nombres = dto.Nombres;
        clienteExistente.Direccion = dto.Direccion;
        clienteExistente.RNC = dto.RNC;
        clienteExistente.LimiteCredito = dto.LimiteCredito;
        clienteExistente.FechaIngreso = dto.FechaIngreso;

        var modificado = await _clientesService.Guardar(clienteExistente);
        if (!modificado) return BadRequest("No se pudo modificar el cliente.");

        return NoContent();
    }

    // DELETE: api/Clientes/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCliente(int id)
    {
        var eliminado = await _clientesService.Eliminar(id);
        if (!eliminado) return NotFound();
        return NoContent();
    }
}
