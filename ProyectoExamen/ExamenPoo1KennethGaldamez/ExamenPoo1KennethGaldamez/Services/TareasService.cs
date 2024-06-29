using ExamenPoo1KennethGaldamez.Services.Interfaces;
using ExamenPoo1KennethGaldamez.DataBase.Entities;
using ExamenPoo1KennethGaldamez.Dtos.Tarea;
using Newtonsoft.Json;

namespace ExamenPoo1KennethGaldamez.Services
{
    public class TareasService : ITareasService
    {

        public readonly string _JSON_FILE;

        public TareasService()
        {

            _JSON_FILE = "SeedData/Tareas.json";

        }

        public async Task<bool> CreateAsync(TareaCreateDto dto)
        {
            var TareasDto = await ReadTareasFromFileAsync();



            var tareaDto = new TareaDto
            {
                Id = Guid.NewGuid(),
                Description = dto.Description,
                Estado = dto.Estado,
                Tiempo = dto.Tiempo,
                Prioridad = dto.Prioridad,

            };

            TareasDto.Add(tareaDto);

            var tareas = TareasDto.Select(x => new Tarea
            {
                Id = x.Id,
                Estado = x.Estado,
                Tiempo = x.Tiempo,
                Prioridad = x.Prioridad,
                Description = x.Description,

            }).ToList();

            await WriteTareasToFileAsync(tareas);

            return true;
        }

        public async Task<bool> DeleteAsync(Guid Id)
        {
            var tareasDto = await ReadTareasFromFileAsync();
            var tareaToDelete = tareasDto.FirstOrDefault(x => x.Id == Id);
            if (tareaToDelete is null)
            {

                return false;

            }
            tareasDto.Remove(tareaToDelete);

            var tareas = tareasDto.Select(x => new Tarea
            {
                Id = x.Id,
                Estado = x.Estado,
                Tiempo = x.Tiempo,
                Prioridad = x.Prioridad,
                Description = x.Description,

            }).ToList();

            await WriteTareasToFileAsync(tareas);
            return true;
        }

        public async Task<bool> EditAsync(TareaEditDto dto, Guid Id)
        {
            var tareasDto = await ReadTareasFromFileAsync();
            var existingTarea = tareasDto.FirstOrDefault(c => c.Id == Id);
            if (existingTarea is null)
            {

                return false;

            }
            for (int i = 0; i < tareasDto.Count; i++)
            {

                if (tareasDto[i].Id == Id)
                {
                    tareasDto[i].Estado = dto.Estado;
                    tareasDto[i].Tiempo = dto.Tiempo;
                    tareasDto[i].Prioridad = dto.Prioridad;
                    tareasDto[i].Description = dto.Description;
                }

            }

            var categories = tareasDto.Select(x => new Tarea
            {
                Id = x.Id,
                Estado = x.Estado,
                Tiempo = x.Tiempo,
                Prioridad = x.Prioridad,
                Description = x.Description,


            }).ToList();
            await WriteTareasToFileAsync(categories);
            return true;
        }


        public async Task<TareaDto> GetTareasByStateAsync(string estado)
        {
            var tareas = await ReadTareasFromFileAsync();
            TareaDto tarea = tareas.FirstOrDefault(c => c.Estado == estado);
            return tarea;
        }
        public async Task<TareaDto> GetTareasByIdAsync(Guid id)
        {
            var tareas = await ReadTareasFromFileAsync();
            TareaDto tarea = tareas.FirstOrDefault(c => c.Id == id);
            return tarea;
        }
        public async Task<TareaDto> GetTareasByPriorityAsync(string prioridad)
        {
            var tareas = await ReadTareasFromFileAsync();
            TareaDto tarea = tareas.FirstOrDefault(c => c.Prioridad == prioridad);
            return tarea;
        }

        private async Task<List<TareaDto>> ReadTareasFromFileAsync()
        {
            if (!File.Exists(_JSON_FILE))
            {

                return new List<TareaDto>();

            }

            var json = await File.ReadAllTextAsync(_JSON_FILE);
            var tareas = JsonConvert.DeserializeObject<List<Tarea>>(json);
            var dtos = tareas.Select(x => new TareaDto
            {
                Id = x.Id,
                Estado = x.Estado,
                Tiempo = x.Tiempo,
                Prioridad = x.Prioridad,
                Description = x.Description
            }).ToList();


            return dtos;
        }

        private async Task WriteTareasToFileAsync(List<Tarea> tareas)
        {

            var json = JsonConvert.SerializeObject(tareas, Formatting.Indented);

            if (File.Exists(_JSON_FILE))
            {

                await File.WriteAllTextAsync(_JSON_FILE, json);

            }

        }
    }
}
