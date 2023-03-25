using System;
using FarmManager.Server.Repository;
using FarmManager.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace FarmManager.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FieldsController : ControllerBase
{
	private IFarmRepository _repo;

	public FieldsController(IFarmRepository repo)
	{
		_repo = repo;
	}

    [HttpGet()]
    [ProducesResponseType(typeof(List<Field>), 200)]
    [ProducesResponseType(typeof(List<Field>), 404)]
    public async Task<ActionResult> Customers()
    {
        var fields = await _repo.GetFieldsAsync();
        if (fields == null)
        {
            return NotFound();
        }
        return Ok(fields);
    }

    [HttpGet("{id}", Name = "GetFieldRoute")]
    [ProducesResponseType(typeof(Field), 200)]
    [ProducesResponseType(typeof(Field), 404)]
    public async Task<ActionResult> Customers(Guid id)
    {
        var field = await _repo.GetFieldAsync(id);
        if (field == null)
        {
            return NotFound();
        }
        return Ok(field);
    }

    // POST api/customers
    [HttpPost()]
    [ProducesResponseType(typeof(Field), 201)]
    [ProducesResponseType(typeof(string), 400)]
    public async Task<ActionResult> PostCustomer([FromBody] Field field)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(this.ModelState);
        }

        var newField = await _repo.InsertFieldAsync(field);
        if (newField == null)
        {
            return BadRequest("Unable to insert customer");
        }
        return CreatedAtRoute("GetFieldRoute", new { id = newField.Id }, newField);
    }

    // PUT api/dataservice/customers/5
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(bool), 200)]
    [ProducesResponseType(typeof(bool), 400)]
    public async Task<ActionResult> PutCustomer(Guid id, [FromBody] Field field)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(this.ModelState);
        }

        var status = await _repo.UpdateFieldAsync(field);
        if (!status)
        {
            return BadRequest("Unable to update customer");
        }
        return Ok(status);
    }

    // DELETE api/dataservice/customers/5
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(bool), 200)]
    [ProducesResponseType(typeof(bool), 404)]
    public async Task<ActionResult> DeleteCustomer(Guid id)
    {
        var status = await _repo.DeleteFieldAsync(id);
        if (!status)
        {
            return NotFound();
        }
        return Ok(status);
    }
}

