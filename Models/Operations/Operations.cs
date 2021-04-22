using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DatingSite
{
    public abstract class Operations<TValue, TKey> where TValue : class, MainEntity<TKey>
    {
        private DataContext db = new DataContext();
        public Operations(DataContext dc)
        {
            this.db = dc;
        }

        public DbSet<TValue> Items => db.Set<TValue>();
        public TValue Get(TKey id)
        {
            return Items.Find(id);
        }

        public List<TValue> GetAll()
        {
            return Items.ToList();
        }


        public void Add(TValue item)
        {
            Items.Add(item);
        }

        public void Remove(TKey id)
        {
            var item = Get(id);
            Items.Remove(item);
        }

        public void Edit(TValue item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Save()
        {
            db.SaveChanges();
        }

    }
}
