using Application.Context;
using Application.Entities;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {

        private readonly ICategoriesService _service;

        public CategoriasController(ICategoriesService service)
        {
            _service = service;
        }

        /*
        [HttpGet("modelo")]
        public ActionResult modeloContrutor()
        {
            try
            {
                return Ok(new Categorias());
            }
            catch (Exception ex)
            {

                return BadRequest($"Erro: {ex}");
            }
        }
        */

        // GET api/<CategoriasController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                // Recupera lista de categorias
                var categorias = await _service.GetAll();
                return Ok(categorias);
            }
            catch (Exception ex)
            {
                //Retorna erro com detalhes caso tenha alguma falha
                return BadRequest($"Erro: {ex}");
            }
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Categorias model)
        {
            try
            {
                // Verificação de nome preenchido é feito pelo 'Required' no model
                // Verificação de nome único foi feito ao criar o BD ao estipular a coluna como tipo Unique
                _service.Add(model);
                if (await _service.SaveChangeAsync())
                {
                    return Ok();
                }
                return BadRequest("Não foi possível salvar");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }
    }
}
