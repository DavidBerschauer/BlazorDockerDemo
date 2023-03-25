using System;
using FarmManager.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace FarmManager.Server.Repository;

public class FarmRepository : IFarmRepository
{
    private readonly FarmDbContext _context;
    private readonly ILogger _logger;

    public FarmRepository(FarmDbContext context, ILoggerFactory loggerFactory)
    {
        _context = context;
        _logger = loggerFactory.CreateLogger("FarmRepository");
    }

    public async Task<List<Field>> GetFieldsAsync()
    {
        return await _context.Fields.OrderBy(c => c.Name).ToListAsync();
    }

    public async Task<Field?> GetFieldAsync(Guid id)
    {
        return await _context.Fields.SingleOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Field> InsertFieldAsync(Field field)
    {
        _context.Add(field);
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (System.Exception exp)
        {
            _logger.LogError($"Error in {nameof(InsertFieldAsync)}: " + exp.Message);
        }

        return field;
    }

    public async Task<bool> UpdateFieldAsync(Field field)
    {
        //Will update all properties of the Customer
        _context.Fields.Attach(field);
        _context.Entry(field).State = EntityState.Modified;
        try
        {
            return (await _context.SaveChangesAsync() > 0 ? true : false);
        }
        catch (Exception exp)
        {
            _logger.LogError($"Error in {nameof(UpdateFieldAsync)}: " + exp.Message);
        }
        return false;
    }

    public async Task<bool> DeleteFieldAsync(Guid id)
    {
        //Extra hop to the database but keeps it nice and simple for this demo
        var field = await _context.Fields.SingleOrDefaultAsync(c => c.Id == id);
        if (field == null)
            return false;
        _context.Remove(field);
        try
        {
            return (await _context.SaveChangesAsync() > 0 ? true : false);
        }
        catch (System.Exception exp)
        {
            _logger.LogError($"Error in {nameof(DeleteFieldAsync)}: " + exp.Message);
        }
        return false;
    }
}

