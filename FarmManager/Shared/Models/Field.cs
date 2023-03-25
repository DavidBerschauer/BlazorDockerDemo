using System;
namespace FarmManager.Shared.Models;

public class Field
{
	public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = "";

}

