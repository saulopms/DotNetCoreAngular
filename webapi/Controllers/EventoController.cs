using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using repository;
using webapi;
using webapi.Dtos;

namespace webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventoController : ControllerBase
    {
        private readonly IRepository _repo;
        private readonly IMapper _mapper;

        public EventoController(IRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
            
        }
      

        [HttpGet]
        public async Task<IActionResult> Get()
        {
           try{
               var eventos = await 
                  _repo.GetAllEventoAsync(true); 
               var results = _mapper.Map<IEnumerable<EventoDto>>(eventos);
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
               var evento = await 
                  _repo.GetEventoAsyncById(EventoId,true);
               var results = _mapper.Map<EventoDto>(evento); 
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
               var eventos = await 
                  _repo.GetAllEventoAsyncByTema(tema,
                  true); 
               var results = _mapper.Map<IEnumerable<EventoDto>>(eventos);
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
        public async Task<IActionResult> Post(EventoDto model)
        {
           try{
                var evento = _mapper.Map<Evento>(model);
                _repo.Add(evento); 

                if(await _repo.SaveChangesAsync())                
                  return Created($"/evento/{model.Id}",
                  _mapper.Map<EventoDto>(evento));
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


        [HttpPut("{EventoId:int}")]
        public async Task<IActionResult> Put(int EventoId,
        Evento model)
        {
           try{

               var evento = await 
                  _repo.GetEventoAsyncById(EventoId, false);

                if(evento == null) return NotFound();

                _mapper.Map(model, evento);

                _repo.Update(model); 

                if(await _repo.SaveChangesAsync())                
                  return Created($"/evento/{model.Id}",
                  _mapper.Map<EventoDto>(evento));

           }
           catch(System.Exception)
           {
                return this.StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "Banco de dados falhou");
           }

           return BadRequest();
        }  


        [HttpDelete("{EventoId:int}")]
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
