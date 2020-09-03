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
    public class PalestranteController : ControllerBase
    {
        private readonly IRepository _repo;

        public PalestranteController(IRepository repo)
        {
            this._repo = repo;
        }
      

        [HttpGet("{name,includeEventos}")]
        public async Task<IActionResult> Get(string name,
        bool includeEventos)
        {
           try{
               var results = await 
                  _repo.GetAllPalestrantesByNameAsync(
                      name,includeEventos); 
               return Ok(results);

           }
           catch(System.Exception)
           {
                return this.StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "Banco de dados falhou");

           }
        }
        
        [HttpGet("{palestranteId}")]
        public async Task<IActionResult> Get(int palestranteId)
        {
           try{
               var results = await 
                  _repo.GetPalestrantesAsync(palestranteId,true); 
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
        public async Task<IActionResult> Post(Palestrante model)
        {
           try{
                _repo.Add(model); 

                if(await _repo.SaveChangesAsync())                
                  return Created($"/palestrante/{model.Id}",model);
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
        public async Task<IActionResult> Put(int palestranteId,
        Palestrante model)
        {
           try{

               var palestrante = await 
                  _repo.GetPalestrantesAsync(palestranteId, false);

                if(palestrante == null)
                  return NotFound();


                _repo.Update(model); 

                if(await _repo.SaveChangesAsync())                
                  return Created($"/palestrante/{model.Id}",model);
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
        public async Task<IActionResult> Delete(int palestranteId)
        {
           try{

               var palestrante = await 
                  _repo.GetPalestrantesAsync(palestranteId, false);

                if(palestrante == null)
                  return NotFound();

                _repo.Delete(palestrante); 

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
