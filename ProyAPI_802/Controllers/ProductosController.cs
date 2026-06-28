using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyAPI_802.DTOs;
using ProyAPI_802.Models;

namespace ProyAPI_802.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        public readonly LatiendaContext dbContext;
        private readonly IMapper _mapper;

        public ProductoController(LatiendaContext _dbContext, IMapper mapper)
        {
            dbContext = _dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("Lista")]
        public IActionResult Lista()
        {
            List<Producto> lista = new List<Producto>();
            try
            {
                lista = dbContext.Productos.Include(c => c.objCategoria).ToList();
                var listaDto = _mapper.Map<List<ProductoDto>>(lista);
                return StatusCode(StatusCodes.Status200OK, new { msj = "ok", response = listaDto });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { msj = ex.Message, response = lista });
            }
        }

        [HttpGet]
        [Route("Obtener/{idProducto:int}")]
        public IActionResult Obtener(int idProducto)
        {
            try
            {
                var oProducto = dbContext.Productos
                    .Include(c => c.objCategoria)
                    .Where(p => p.IdProducto == idProducto)
                    .FirstOrDefault();

                if (oProducto == null)
                    return NotFound(new { msj = "Producto no encontrado" });

                var dto = _mapper.Map<ProductoDto>(oProducto);
                return StatusCode(StatusCodes.Status200OK, new { msj = "ok", response = dto });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { msj = ex.Message });
            }
        }

        [HttpPost]
        [Route("Guardar")]
        public IActionResult Guardar([FromBody] ProductoCreateDto objetoDto)
        {
            try
            {
                var objeto = _mapper.Map<Producto>(objetoDto);
                dbContext.Productos.Add(objeto);
                dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status201Created, new { msj = "ok", response = objeto });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { msj = ex.Message });
            }
        }

        [HttpPut]
        [Route("Editar/{idProducto:int}")]
        public IActionResult Editar(int idProducto, [FromBody] ProductoCreateDto objetoDto)
        {
            Producto oProducto = dbContext.Productos.Find(idProducto);
            if (oProducto == null)
                return NotFound(new { msj = "Producto no encontrado" });

            try
            {
                _mapper.Map(objetoDto, oProducto);
                dbContext.Productos.Update(oProducto);
                dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { msj = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { msj = ex.Message });
            }
        }

        [HttpDelete]
        [Route("Eliminar/{idProducto:int}")]
        public IActionResult Eliminar(int idProducto)
        {
            Producto oProducto = dbContext.Productos.Find(idProducto);
            if (oProducto == null)
                return NotFound(new { msj = "Producto no encontrado" });

            try
            {
                dbContext.Productos.Remove(oProducto);
                dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { msj = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { msj = ex.Message });
            }
        }
    }
}