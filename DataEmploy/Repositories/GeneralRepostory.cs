using DataEmploy.Contexts;
using DataEmploy.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace DataEmploy.Repositories
{
    public class GeneralRepository<Context, Entity, Key> : IRepository<Entity, Key>
        where Entity : class
        where Context : AppDbContext
    {
        private readonly AppDbContext appDbContext;
        private readonly DbSet<Entity> entities;
        public GeneralRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
            entities = appDbContext.Set<Entity>();
        }
        public int Create(Entity entity)
        {
            entities.Add(entity);
            return appDbContext.SaveChanges();
        }

        public int Delete(Key key)
        {
            appDbContext.Remove(entities.Find(key));
            return appDbContext.SaveChanges();
        }

        public IEnumerable<Entity> Read()
        {
            return entities.ToList();
        }

        public Entity Read(Key key)
        {
            return entities.Find(key);
        }

        public int Update(Entity entity)
        {
            appDbContext.Entry(entity).State = EntityState.Modified;
            return appDbContext.SaveChanges();
        }
    }
}
