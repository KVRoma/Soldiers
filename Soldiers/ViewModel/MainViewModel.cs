using Soldiers.EF;
using Soldiers.Model;
using Soldiers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Soldiers.Commands;

namespace Soldiers.ViewModel
{
    public class MainViewModel : ViewModel
    {
        public string NameWindow { get; } = "Soldiers - 2020";
        private SoldierRepository repository = new SoldierRepository();
        
        private Soldier soldierSelect;
        private IEnumerable<Soldier> soldiers;

        public Soldier SoldierSelect
        {
            get { return soldierSelect; }
            set
            {
                soldierSelect = value;
                OnPropertyChanged(nameof(SoldierSelect));
            }
        }
        public IEnumerable<Soldier> Soldiers
        {
            get { return soldiers; }
            set
            {
                soldiers = value;
                OnPropertyChanged(nameof(Soldiers));
            }
        }

        private Command _searchCommand;
        public Command SearchCommand => _searchCommand ?? (_searchCommand = new Command(obj=> 
        {
            string search = obj.ToString();
            if (search == "")
            {
                Soldiers = repository.GetList();
            }
            else
            {
                Soldiers = repository.GetList().Where(n => n.AccountNumber.ToUpper().Contains(search.ToUpper()));
            }
        }));


        public  MainViewModel()
        {
            Soldiers = repository.GetList();           
        }
    }
}
