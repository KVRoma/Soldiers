using Soldiers.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soldiers.Model
{
    interface ISoldierRepository : IDisposable
    {
        IEnumerable<Soldier> GetBookList();
        Soldier GetBook(int id);
        void Create(Soldier item);
        void Update(Soldier item);
        void Delete(int id);
        void DeleteRange(List<Soldier> soldiers);
        void Save();
    }
}
