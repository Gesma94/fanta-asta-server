using System.Collections.Generic;
using FantaAstaServer.Models;
using Microsoft.AspNetCore.Http;

namespace FantaAstaServer.Interfaces.Services;

public interface IFormFileReader
{
    public IEnumerable<JsonFootballer> GetJsonFootballers(IFormFile jsonFile);
}