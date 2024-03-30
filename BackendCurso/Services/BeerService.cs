using AutoMapper;
using BackendCurso.DTOs;
using BackendCurso.Models;
using BackendCurso.Repository;

namespace BackendCurso.Services
{
    public class BeerService : ICommonService<BeerDto, BeerInsertDto, BeerUpdateDto>
    {

        private IRepository<Beer> _beerRepository;
        private IMapper _mapper;
        public List<string> Errors { get; }

        public BeerService(
            IRepository<Beer> beerRepository, IMapper mapper

            )
        {
            _beerRepository = beerRepository;
            _mapper = mapper;
            Errors = new List<string>();
        }
        

        public async Task<IEnumerable<BeerDto>> Get()
        {
            // Sin usar el repositorio

            //return await _context.Beers.Select(b => new BeerDto
            //{
            //    Id = b.BeerId,
            //    Name = b.Name,
            //    BrandId = b.BrandId,
            //    Alcohol = b.Alcohol
            //}).ToListAsync();

            var beers = await _beerRepository.Get();



            // Usando Mapper
            //return beers.Select(beer => new BeerDto
            //{
            //    Id = beer.BeerId,
            //    Name = beer.Name,
            //    BrandId = beer.BrandId,
            //    Alcohol = beer.Alcohol
            //});

            // Usando Mapper

            return beers.Select(b => _mapper.Map<BeerDto>(b));
        }
          

        public async Task<BeerDto> GetById(int id)
        {
            // Sin usar el repositorio
            //var beer = await _context.Beers.FindAsync(id);

            var beer = await _beerRepository.GetById(id);
            if (beer == null) return null;

            // sin usar Mapper
            //var beerDto = new BeerDto
            //{
            //    Id = beer.BeerId,
            //    Name = beer.Name,
            //    BrandId = beer.BrandId,
            //    Alcohol = beer.Alcohol
            //};

            // Usando Mapper
            var beerDto = _mapper.Map<BeerDto>(beer);

            return beerDto;
        }

        public async Task<BeerDto> Add(BeerInsertDto beerInsertDto)
        {
            // Sin usar Mapper
            //var beer = new Beer()
            //{
            //    Name = beerInsertDto.Name,
            //    BrandId = beerInsertDto.BrandId,
            //    Alcohol = beerInsertDto.Alcohol
            //};

            // Usando Mapper
            var beer = _mapper.Map<Beer>(beerInsertDto);

            await _beerRepository.Add(beer);
            await _beerRepository.Save();

            // Sin usar el repositorio
            //await _context.Beers.AddAsync(beer);
            //await _context.SaveChangesAsync();

            // Sin usar Mapper
            //var beerDto = new BeerDto
            //{
            //    Id = beer.BeerId,
            //    Name = beer.Name,
            //    BrandId = beer.BrandId,
            //    Alcohol = beer.Alcohol
            //};

            // Usando Mapper
            var beerDto = _mapper.Map<BeerDto>(beer);

            return beerDto;

        }
        public async Task<BeerDto> Update(int id, BeerUpdateDto beerUpdateDto)
        {
            // Sin usar el repositorio
            //var beer = await _context.Beers.FindAsync(id);

            var beer = await _beerRepository.GetById(id);

            if (beer != null)
            {
                // sin usar mapper
                //beer.Name = beerUpdateDto.Name;
                //beer.BrandId = beerUpdateDto.BrandId;
                //beer.Alcohol = beerUpdateDto.Alcohol;

                // Usando Mapper
                // se pasa el objeto beerUpdateDto al objeto beer
                // se manda el origen de la informacion y el existente 
                // con eso se ignora los campos no esten en automaper como el BeerId
                beer = _mapper.Map<BeerUpdateDto, Beer>(beerUpdateDto, beer);

                // Sin usar el repositorio
                //await _context.SaveChangesAsync();

                _beerRepository.Update(beer);
                await _beerRepository.Save();

                var beerDto = new BeerDto
                {
                    Id = beer.BeerId,
                    Name = beer.Name,
                    BrandId = beer.BrandId,
                    Alcohol = beer.Alcohol
                };

                return beerDto;

            }

            return null;

            }

            public async Task<BeerDto> Delete(int id)
        {
            // Sin usar el repositorio
            //var beer = await _context.Beers.FindAsync(id);

            var beer = await _beerRepository.GetById(id);

            if (beer != null)
            {
                var beerDto = new BeerDto
                {
                    Id = beer.BeerId,
                    Name = beer.Name,
                    BrandId = beer.BrandId,
                    Alcohol = beer.Alcohol
                };

                // Sin usar el repositorio

                //_context.Remove(beer);

                _beerRepository.Delete(beer);

                // Sin usar el repositorio
                //await _context.SaveChangesAsync();

                await _beerRepository.Save();

                return beerDto;
            }

            return null;
        }

        public bool Validate(BeerInsertDto beerInsertDto)
        {

            // Search regresa una lista de cervezas que cumplan con la condicion
            // si la lista es mayor a 0 significa que ya existe una cerveza con ese nombre
          if(_beerRepository.Search(b => b.Name == beerInsertDto.Name).Count() > 0)
            {
                Errors.Add("Ya existe una cerveza con ese nombre");
                return false;
            }

            return true;
        }

        public bool Validate(BeerUpdateDto beerUpdateDto)
        {
            return true;
        }

    }
}
