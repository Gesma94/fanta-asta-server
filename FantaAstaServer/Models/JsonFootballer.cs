using FantaAstaServer.Enums;

namespace FantaAstaServer.Models;

public class JsonFootballer
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Price { get; set; }
    public FootballerRole Role { get; set; }
}