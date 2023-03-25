using System;
using FarmManager.Shared.Models;

namespace FarmManager.Server.Repository;

public interface IFarmRepository
{
    Task<List<Field>> GetFieldsAsync();

    Task<Field?> GetFieldAsync(Guid id);

    Task<Field> InsertFieldAsync(Field field);
    Task<bool> UpdateFieldAsync(Field field);
    Task<bool> DeleteFieldAsync(Guid id);
}

