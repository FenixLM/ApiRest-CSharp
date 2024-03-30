
using BackendCurso.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendCurso.Repository
{
    public class BeerRepository : IRepository<Beer>
    {
        private StoreContext _context;
        public BeerRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Beer>> Get() => await _context.Beers.ToListAsync();


        public async Task<Beer> GetById(int id) => await _context.Beers.FindAsync(id);


        public async Task Add(Beer beer) => await _context.Beers.AddAsync(beer);

        public void Update(Beer beer)
        {
            _context.Beers.Attach(beer);
            _context.Beers.Entry(beer).State = EntityState.Modified;
        }

        public void Delete(Beer beer) => _context.Beers.Remove(beer);

        // aqui es donde se guarda en la base de datos
        public async  Task Save()=> await _context.SaveChangesAsync();


        // Vas a mandar una condiciones con una funcion de primera clase  a una funcion de orden superior llamada Where
        // Func recibe como parametro una entidad de tipo Beer y regresa un booleano
        public IEnumerable<Beer> Search(Func<Beer, bool> filter)
        {
            return _context.Beers.Where(filter).ToList();
        }


    }
}
