using Microsoft.AspNetCore.Mvc;
using ExamenPoo1KennethGaldamez.Services.Interfaces;
using ExamenPoo1KennethGaldamez.Dtos.Tarea;


namespace ExamenPoo1KennethGaldamez.Controllers
{      
    [ApiController]
    [Route("api/tareas")]
    public class TareasController : ControllerBase
    {


            private readonly ITareasService _tareasService;

            public TareasController(ITareasService tareasService)
            {

                this._tareasService = tareasService;

            }
    
            [HttpGet("{prioridad}")]
            public async Task<ActionResult> GetPrioridad(string prioridad)
            {
              var tarea = await _tareasService.GetTareasByTimeAsync(prioridad);
              if (tarea == null)
              {
                   return NotFound("Ninguna requiere ese tiempo...");
              }

            else
            {
                return Ok(tarea);
            }

        }


        //[HttpGet("{tiempo}")]

        //    public async Task<ActionResult> GetTime(string tiempo)
        //    {
        //        var tarea = await _tareasService.GetTareasByTimeAsync(tiempo);
        //        if (tarea == null)
        //        {
        //            return NotFound("Ninguna requiere ese tiempo...");
        //        }

        //        else
        //        {
        //            return Ok(tarea);
        //        }

        //    }
                
            [HttpPost]
            public async Task<ActionResult> Create(TareaCreateDto dto)
            {

                await _tareasService.CreateAsync(dto);
                return StatusCode(201);


            }

            [HttpPut]
            public async Task<ActionResult> Edit(TareaEditDto dto, Guid id)
            {

                var result = await _tareasService.EditAsync(dto, id);

                if (!result)
                {

                    return NotFound();

                }
                return Ok();




            }

            [HttpDelete("{id}")]
            public async Task<ActionResult> Delete(Guid id)
            {

                var category = await _tareasService.GetTareasByIdAsync(id);
                if (category is null)
                {
                    return NotFound();
                }

                await _tareasService.DeleteAsync(id);
                return Ok();
            }
        }
}
