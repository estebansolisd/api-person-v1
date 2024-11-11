using System;
using person_api_1.Models;

public class Person
{
    public Guid Id { get; set; }
    public string GivenName { get; set; } = String.Empty;
    public string Surname { get; set; } = String.Empty;
    public Gender Gender { get; set; } 
    public DateTime? BirthDate { get; set; }
    public string? BirthLocation { get; set; } = String.Empty;
    public DateTime? DeathDate { get; set; }  
    public string? DeathLocation { get; set; } = String.Empty;

}