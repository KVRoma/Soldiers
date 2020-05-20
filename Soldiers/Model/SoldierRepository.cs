using Soldiers.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Soldiers.Model
{
    public class SoldierRepository : ISoldierRepository
    {
        private SoldierContext db;
        /// <summary>
        /// Класс для роботи з базою в таблиці Soldier (pattern Repository)
        /// </summary>
        public SoldierRepository()
        {
            this.db = new SoldierContext();
        }



        /// <summary>
        /// Створює екземпляр Soldier
        /// </summary>
        /// <param name="item"></param>
        public void Create(Soldier item)
        {
            db.Soldiers.Add(item);
        }
        /// <summary>
        /// Видаляє екземпляр Soldier по вказаному Id, перевіряє на валідність
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            Soldier soldier = db.Soldiers.Find(id);
            if (soldier != null)
            {
                db.Soldiers.Remove(soldier);
            }
        }
        /// <summary>
        /// Видаляє діапазон Soldier
        /// </summary>
        /// <param name="soldiers"></param>
        public void DeleteRange(List<Soldier> soldiers)
        {
            db.Soldiers.RemoveRange(soldiers);
        }
        /// <summary>
        /// Повертає екземпляр Soldier по вказаному Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Soldier Get(int id)
        {
            return db.Soldiers.Find(id);
        }
        /// <summary>
        /// Повертає всю таблицю Soldier, використовує ToList()
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Soldier> GetList()
        {
            return db.Soldiers.ToList();
        }
        /// <summary>
        /// Зберігає всі зміни
        /// </summary>
        public void Save()
        {
            db.SaveChanges();
        }
        /// <summary>
        /// Оновлює дані в БД по заданому екземпляру Soldier
        /// </summary>
        /// <param name="item"></param>
        public void Update(Soldier item)
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
