using Soldiers.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soldiers.Model
{
    public class VOSRepository : IRepository<VOS>
    {
        private SoldierContext db;
        /// <summary>
        /// Класс для роботи з базою в таблиці VOS (pattern Repository)
        /// </summary>
        public VOSRepository()
        {
            this.db = new SoldierContext();
        }

        /// <summary>
        /// Створює екземпляр VOS
        /// </summary>
        /// <param name="item"></param>
        public void Create(VOS item)
        {
            db.VOS.Add(item);
        }
        /// <summary>
        /// Видаляє екземпляр VOS по вказаному Id, перевіряє на валідність
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            VOS vos = db.VOS.Find(id);
            if (vos != null)
            {
                db.VOS.Remove(vos);
            }
        }
        /// <summary>
        /// Видаляє діапазон VOS
        /// </summary>
        /// <param name="items"></param>
        public void DeleteRange(List<VOS> items)
        {
            db.VOS.RemoveRange(items);
        }
        /// <summary>
        /// Повертає екземпляр VOS по вказаному Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VOS Get(int id)
        {
            return db.VOS.Find(id);
        }
        /// <summary>
        ///  Повертає всю таблицю VOS, використовує ToList()
        /// </summary>
        /// <returns></returns>
        public IEnumerable<VOS> GetList()
        {
            return db.VOS.ToList();
        }
        /// <summary>
        /// Зберігає всі зміни
        /// </summary>
        public void Save()
        {
            db.SaveChanges();
        }
        /// <summary>
        /// Оновлює дані в БД по заданому екземпляру VOS
        /// </summary>
        /// <param name="item"></param>
        public void Update(VOS item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        #region IDisposable Support
        private bool disposed = false; // To detect redundant calls
        /// <summary>
        /// Викликає Dispose
        /// </summary>
        /// <param name="disposing"></param>
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }
        /// <summary>
        /// Викликає Dispose та GC
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
