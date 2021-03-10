using Application.Context;
using Application.Entities;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IProductsService _service;

        public ProdutosController(IProductsService service)
        {
            _service = service;
        }

        // GET: api/<ProdutosController>
        [HttpGet("{term}/{page}/{pageSize}")]
        public async Task<IActionResult> Get(string term, int page, int pageSize)
        {
            try
            {
                var produtos = await _service.GetAll(term, page, pageSize);
                if (produtos != null)
                {
                    return Ok(produtos);
                }
                else
                {
                    return BadRequest("Página não encontrada.");
                }
                
            }
            catch (Exception ex)
            {

                return BadRequest($"Erro: {ex}");
            }
        }

        // POST api/<ProdutosController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Produtos model)
        {
            // Validação de nome preencido feito no model do Produto
            // Validação de nome único feito ai criar o BD, no Context
            string produtoValido = VerificaProdutoValido(model);

            if (produtoValido != "OK")
            {
                // Retorna erro caso não seja válido
                return BadRequest(produtoValido);
            }

            try
            {
                // Salva no BD
                _service.Add(model);
                if (await _service.SaveChangeAsync())
                {
                    return Ok();
                }
                return BadRequest("Não foi possível salvar");
            }
            catch (Exception ex)
            {
                // Retorna erro com detalhes caso dê algum erro
                return BadRequest($"Erro: {ex}");
            }
        }


        // PUT api/<ProdutosController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, [FromBody] Produtos model)
        {
            // mesmas verificações para o método Add
            string produtoValido = VerificaProdutoValido(model);
            if (produtoValido != "OK")
            {
                return BadRequest(produtoValido);
            }

            try
            {
                var produto = await _service.GetProdutoById(id);
                if (produto != null)
                {
                    _service.Update(model);
                    await _service.SaveChangeAsync();
                    return Ok();
                }
                else
                {
                    // Retorna erro caso não encontre o registro
                    return BadRequest("Não foi possível encontrar o produto");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }

        }

        // DELETE api/<ProdutosController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                var produto = await _service.GetProdutoById(id);
                if (produto != null)
                {
                    _service.Delete(produto);
                    await _service.SaveChangeAsync();
                    return Ok();
                }
                // Retorna erro caso não encontre o registro
                return BadRequest("Não foi possível encontrar o produto");
            }
            catch (Exception ex)
            {

                return BadRequest($"Erro: {ex}");
            }

        }

        // Método para verificar os dados
        [ApiExplorerSettings(IgnoreApi = true)]
        public string VerificaProdutoValido(Produtos model)
        {

            if (model.Nome == "" | model.Nome == null)
            {
                return "Informe um nome válido.";
            }
            else if (model.PrecoUnitario <= 0)
            {
                return "Informe um preço válido.";
            }
            else if (model.QuantidadeEstoque < 1)
            {
                return "Informe uma quantidade válida.";
            }
            return "OK";
        }
    }
}
