using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using repository;
using webapi;

namespace webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        public DataContext _context { get; }
        public WeatherForecastController(DataContext context)
        {
            _context = context;

        }
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        //private readonly ILogger<WeatherForecastController> _logger;

       /* public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }*/

        [HttpGet]
        public async Task<IActionResult> Get()
        {
           try{
               var results = await 
                  _context.Eventos.ToListAsync();
               return Ok(results);

           }
           catch(System.Exception)
           {
                return this.StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "Banco de dados falhou");

           }
        }

        /*[HttpGet]
        public async Task<IActionResult> Get(int id)
        {
           try{
               var results = await 
                  _context.Eventos.FirstOrDefaultAsync(
                      x => x.EventoId == id);
                  
               return Ok(results);

           }
           catch(System.Exception)
           {
                return this.StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "Banco de dados falhou");

           }
        }*/

    }
}
