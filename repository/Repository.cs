using System.Linq;
using System.Threading.Tasks;
using domain;
using Microsoft.EntityFrameworkCore;

namespace repository
{
    public class Repository : IRepository
    {
        private readonly DataContext _context;

        public Repository(DataContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior =
            QueryTrackingBehavior.NoTracking;
        }        
        
        //GERAIS
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }
        public void Update<T>(T entity) where T : class
        {
           _context.Update(entity);
        }
        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }
        public async Task<bool> SaveChangesAsync()
        {
           return (await _context.SaveChangesAsync()) > 0;
        }       
       //EVENTO
        public async Task<Evento[]> GetAllEventoAsync(bool
         includePalestrante = false)
        {
            IQueryable<Evento> query = _context.Eventos
            .Include(c => c.Lotes)
            .Include(c => c.RedesSociais);

            if(includePalestrante)
            {
                query = query
                .Include(PE => PE.PalestrantesEventos)
                .ThenInclude(P => P.Palestrante);
            }

            query = query.OrderByDescending(c => c.DataEvento);

            return await query.ToArrayAsync();
        }

        public async Task<Evento[]> GetAllEventoAsyncByTema(
            string tema, bool includePalestrante)
        {
            IQueryable<Evento> query = _context.Eventos
            .Include(c => c.Lotes)
            .Include(c => c.RedesSociais);

            if(includePalestrante)
            {
                query = query
                .Include(PE => PE.PalestrantesEventos)
                .ThenInclude(P => P.Palestrante);
            }

            query = query.OrderByDescending(c => c.DataEvento)
            .Where(c => c.Tema.ToLower().Contains(tema.ToLower()));

            return await query.ToArrayAsync();
        }

        public async Task<Evento> GetEventoAsyncById(int Id, bool includePalestrante)
        {
             IQueryable<Evento> query = _context.Eventos
            .Include(c => c.Lotes)
            .Include(c => c.RedesSociais);

            if(includePalestrante)
            {
                query = query
                .Include(PE => PE.PalestrantesEventos)
                .ThenInclude(P => P.Palestrante);
            }

            query = query.OrderByDescending(c => c.DataEvento)
            .Where(c => c.Id == Id);

            return await query.FirstOrDefaultAsync();
        }

        //PALESTRANTE
        public async Task<Palestrante[]> GetAllPalestrantesByNameAsync(
            string name, bool 
        includeEventos)
        {
           IQueryable<Palestrante> query = _context.Palestrantes
             .Include(c => c.RedesSociais);

            if(includeEventos)
            {
                query = query
                .Include(PE => PE.PalestrantesEventos)
                .ThenInclude(E => E.Evento);
            }

            query = query.OrderBy(c => c.Nome)
            .Where(c => c.Nome.ToLower().Contains(name.ToLower()));

            return await query.ToArrayAsync();
        }

        public async Task<Palestrante> GetPalestrantesAsync(int palestranteId, 
        bool includeEventos = false)
        {
             IQueryable<Palestrante> query = _context.Palestrantes
             .Include(c => c.RedesSociais);

            if(includeEventos)
            {
                query = query
                .Include(PE => PE.PalestrantesEventos)
                .ThenInclude(E => E.Evento);
            }

            query = query.OrderBy(c => c.Nome)
            .Where(c => c.Id == palestranteId);

            return await query.FirstOrDefaultAsync();
        }

      
    }
}