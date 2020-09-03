using System.Threading.Tasks;
using domain;

namespace repository
{
    public interface IRepository
    {
         void Add<T>(T entity) where T: class;
         void Update<T>(T entity) where T: class;
         void Delete<T>(T entity) where T: class;

         Task<bool> SaveChangesAsync();

         //eventos
         Task<Evento[]> GetAllEventoAsyncByTema(string tema,
         bool includePalestrante);
         Task<Evento[]> GetAllEventoAsync(
         bool includePalestrante);
         Task<Evento> GetEventoAsyncById(int Id,
         bool includePalestrante);

         //palestrante
         Task<Palestrante[]> GetAllPalestrantesByNameAsync(string name,
         bool includeEvento);

         Task<Palestrante> GetPalestrantesAsync(
         int palestranteId,
         bool includeEvento);

         



    }
}