using BackendCurso.DTOs;

namespace BackendCurso.Services
{
    public interface ICommonService<T, TI, TU>
    {
        // T es el tipo de dato que se va a retornar
        // TI es el tipo de dato que se va a recibir para insertar
        // TU es el tipo de dato que se va a recibir para actualizar

        public List<string> Errors { get;  }
        Task<IEnumerable<T>> Get();
        Task<T> GetById(int id);
        Task<T> Add(TI beerInsertDto);
        Task<T> Update(int id, TU beerUpdateDto);
        Task<T> Delete(int id);

        bool Validate(TI dto);
        bool Validate(TU dto);
    }
}
