using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using domain;
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
    public class EventoController : ControllerBase
    {
        private readonly IRepository _repo;

        public EventoController(IRepository repo)
        {
            this._repo = repo;
        }
      

        [HttpGet]
        public async Task<IActionResult> Get()
        {
           try{
               var results = await 
                  _repo.GetAllEventoAsync(true); 
               return Ok(results);

           }
           catch(System.Exception)
           {
                return this.StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "Banco de dados falhou");

           }
        }
        
        [HttpGet("{EventoId}")]
        public async Task<IActionResult> Get(int EventoId)
        {
           try{
               var results = await 
                  _repo.GetEventoAsyncById(EventoId,true); 
               return Ok(results);

           }
           catch(System.Exception)
           {
                return this.StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "Banco de dados falhou");

           }
        }

        [HttpGet("getByTema/{tema}")]
        public async Task<IActionResult> Get(string tema)
        {
           try{
               var results = await 
                  _repo.GetAllEventoAsyncByTema(tema,
                  true); 
               return Ok(results);

           }
           catch(System.Exception)
           {
                return this.StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "Banco de dados falhou");

           }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Evento model)
        {
           try{
                _repo.Add(model); 

                if(await _repo.SaveChangesAsync())                
                  return Created($"/evento/{model.Id}",model);
                 // return Ok(results);


           }
           catch(System.Exception)
           {
                return this.StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "Banco de dados falhou");

           }

           return BadRequest();
        } 


        [HttpPut]
        public async Task<IActionResult> Put(int EventoId,
        Evento model)
        {
           try{

               var evento = await 
                  _repo.GetEventoAsyncById(EventoId, false);

                if(evento == null)
                  return NotFound();


                _repo.Update(model); 

                if(await _repo.SaveChangesAsync())                
                  return Created($"/evento/{model.Id}",model);
                 // return Ok(results);


           }
           catch(System.Exception)
           {
                return this.StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "Banco de dados falhou");

           }

           return BadRequest();
        }  


        [HttpDelete]
        public async Task<IActionResult> Delete(int EventoId)
        {
           try{

               var evento = await 
                  _repo.GetEventoAsyncById(EventoId, false);

                if(evento == null)
                  return NotFound();

                _repo.Delete(evento); 

                if(await _repo.SaveChangesAsync())                
                  return Ok();


           }
           catch(System.Exception)
           {
                return this.StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "Banco de dados falhou");

           }

           return BadRequest();
        }      

    }
}
