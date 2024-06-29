using ExamenPoo1KennethGaldamez.Dtos.Tarea;
namespace ExamenPoo1KennethGaldamez.Services.Interfaces
{
    public interface ITareasService
    {

            //Asincronos
            Task<TareaDto> GetTareasByPriorityAsync(string prioridad);
            Task<TareaDto> GetTareasByTimeAsync(string tiempo);
            Task<TareaDto> GetTareasByIdAsync(Guid id);

             Task<bool> CreateAsync(TareaCreateDto dto);
            Task<bool> EditAsync(TareaEditDto dto, Guid Id);
            Task<bool> DeleteAsync(Guid Id);
  
        
    }
}
