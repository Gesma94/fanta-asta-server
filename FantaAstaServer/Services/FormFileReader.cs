using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using FantaAstaServer.Interfaces.Services;
using FantaAstaServer.Models;
using Microsoft.AspNetCore.Http;

namespace FantaAstaServer.Services;

public class FormFileReader : IFormFileReader
{
    public IEnumerable<JsonFootballer> GetJsonFootballers(IFormFile jsonFile)
    {
        using var reader = new StreamReader(jsonFile.OpenReadStream());
        return JsonSerializer.Deserialize<IEnumerable<JsonFootballer>>(reader.ReadToEnd());
    }
}