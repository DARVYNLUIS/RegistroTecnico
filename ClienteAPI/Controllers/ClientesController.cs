using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Clientes.Data.DAL;       
using Clientes.Data.Models;   
using Clientes.Abstraction;  
using Clientes.Services;        
using ClienteAPI.Abstractions;
using ClienteAPI.Services;

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

    // GET: api/Clientes/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Cliente>> GetCliente(int id)
    {
        var cliente = await _clientesService.Buscar(id);
        if (cliente == null) return NotFound();
        return Ok(cliente);
    }

    // POST: api/Clientes
    [HttpPost]
    public async Task<ActionResult<Cliente>> PostCliente(Cliente cliente)
    {
        var guardado = await _clientesService.Guardar(cliente);
        if (!guardado) return BadRequest("No se pudo guardar el cliente.");
        return CreatedAtAction(nameof(GetCliente), new { id = cliente.ClienteId }, cliente);
    }

    // PUT: api/Clientes/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCliente(int id, Cliente cliente)
    {
        if (id != cliente.ClienteId) return BadRequest();
        var modificado = await _clientesService.Guardar(cliente);
        if (!modificado) return NotFound();
        return NoContent();
    }

    // DELETE: api/Clientes/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCliente(int id)
    {
        var eliminado = await _clientesService.Eliminar(id);
        if (!eliminado) return NotFound();
        return NoContent();
    }
}

