using Soldiers.EF;
using Soldiers.Model;
using Soldiers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Soldiers.Commands;
using System.Windows;

namespace Soldiers.ViewModel
{
    public class MainViewModel : ViewModel
    {
        #region Property
        public string NameWindow { get; } = "Soldiers - 2020";
        private readonly SoldierRepository soldierRepo = new SoldierRepository();
        private readonly DictionaryRepository dictionaryRepo = new DictionaryRepository();

        private Soldier soldierSelect;
        private IEnumerable<Soldier> soldiers;
        private Visibility isVisibleSoldier;
        private Visibility isVisibleEditSoldier;
        private bool isEnabledButtonSoldier;

        public Soldier SoldierSelect
        {
            get { return soldierSelect; }
            set
            {
                soldierSelect = value;
                OnPropertyChanged(nameof(SoldierSelect));
                SetEnabledButtonSoldier(SoldierSelect);
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
        public Visibility IsVisibleEditSoldier
        {
            get { return isVisibleEditSoldier; }

            set
            {
                isVisibleEditSoldier = value;
                OnPropertyChanged(nameof(IsVisibleEditSoldier));
            }
        }
        public Visibility IsVisibleSoldier
        {
            get { return isVisibleSoldier; }

            set
            {
                isVisibleSoldier = value;
                OnPropertyChanged(nameof(IsVisibleSoldier));
            }
        }
        public bool IsEnabledButtonSoldier
        {
            get { return isEnabledButtonSoldier; }

            set
            {
                isEnabledButtonSoldier = value;
                OnPropertyChanged(nameof(IsEnabledButtonSoldier));
            }
        }
        #endregion

        #region Command
        private Command _searchCommand;
        private Command _addSoldier;
        private Command _insSoldier;
        private Command _delSoldier;

        public Command SearchCommand => _searchCommand ?? (_searchCommand = new Command(obj =>
        {
            string search = obj.ToString();
            if (search == "")
            {
                Soldiers = soldierRepo.GetList();
            }
            else
            {
                Soldiers = soldierRepo.GetList().Where(n => n.AccountNumber.ToUpper().Contains(search.ToUpper()));
            }
        }));
        public Command AddSoldier => _addSoldier ?? (_addSoldier = new Command(obj =>
        {
            SetVisibleSoldier();
        }));
        public Command InsSoldier => _insSoldier ?? (_insSoldier = new Command(obj=> 
        {
            SetVisibleSoldier();
        }));
        public Command DelSoldier => _delSoldier ?? (_delSoldier = new Command(obj=> 
        {
            soldierRepo.Delete(SoldierSelect.Id);
            soldierRepo.Save();
        }));

        #endregion

        public MainViewModel()
        {
            Soldiers = soldierRepo.GetList();
            GetVisibleSoldier();
            SetEnabledButtonSoldier(SoldierSelect);
        }

        #region Functions
        private void GetVisibleSoldier()
        {
            IsVisibleSoldier = Visibility.Visible;
            IsVisibleEditSoldier = Visibility.Collapsed;
        }
        private void SetVisibleSoldier()
        {
            if (IsVisibleSoldier == Visibility.Visible)
            {
                IsVisibleSoldier = Visibility.Collapsed;
                IsVisibleEditSoldier = Visibility.Visible;
            }
            else
            {
                IsVisibleSoldier = Visibility.Visible;
                IsVisibleEditSoldier = Visibility.Collapsed;
            }
        }
        private void SetEnabledButtonSoldier(Soldier select)
        {
            if (select == null)
            {
                IsEnabledButtonSoldier = false;
            }
            else
            {
                IsEnabledButtonSoldier = true;
            }
        }
        #endregion
    }
}
