using Soldiers.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Soldiers.Model
{
    public class DictionaryRepository : IRepository<Dictionary>
    {
        private SoldierContext db;
        /// <summary>
        /// Класс для роботи з базою в таблиці Dictionary (pattern Repository)
        /// </summary>
        public DictionaryRepository()
        {
            this.db = new SoldierContext();
        }

        /// <summary>
        /// Створює екземпляр Dictionary
        /// </summary>
        /// <param name="item"></param>
        public void Create(Dictionary item)
        {
            db.Dictionaries.Add(item);
        }
        /// <summary>
        /// Видаляє екземпляр Dictionary по вказаному Id, перевіряє на валідність
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            Dictionary dic = db.Dictionaries.Find(id);
            if (dic != null)
            {
                db.Dictionaries.Remove(dic);
            }
        }
        /// <summary>
        /// Видаляє діапазон Dictionary
        /// </summary>
        /// <param name="items"></param>
        public void DeleteRange(List<Dictionary> items)
        {
            db.Dictionaries.RemoveRange(items);
        }
        /// <summary>
        /// Повертає екземпляр Dictionary по вказаному Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Dictionary Get(int id)
        {
            return db.Dictionaries.Find(id);
        }
        /// <summary>
        ///  Повертає всю таблицю Dictionary, використовує ToList()
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Dictionary> GetList()
        {
            return db.Dictionaries.ToList();
        }
        /// <summary>
        /// Зберігає всі зміни
        /// </summary>
        public void Save()
        {
            db.SaveChanges();
        }
        /// <summary>
        /// Оновлює дані в БД по заданому екземпляру Dictionary
        /// </summary>
        /// <param name="item"></param>
        public void Update(Dictionary item)
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
