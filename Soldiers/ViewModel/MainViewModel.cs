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
using Soldiers.Enums;

namespace Soldiers.ViewModel
{
    public class MainViewModel : ViewModel
    {
        #region Property
        public string NameWindow { get; } = "Soldiers - 2020";
        //private readonly SoldierRepository soldierRepo = new SoldierRepository();
        //private readonly DictionaryRepository dictionaryRepo = new DictionaryRepository();

        private Soldier soldierSelect;
        private IEnumerable<Soldier> soldiers;
        private string profileNameSelect;
        private List<string> profileNames;
        private string militaryRankSelect;
        private List<string> militaryRanks;
        private string typeAccountingSelect;
        private List<string> typeAccountings;
        private ComboBoxName dictionaryComboBoxSelect;
        private List<ComboBoxName> dictionarysComboBox;
        private Dictionary dictionarySelect;
        private IEnumerable<Dictionary> dictionaries;
        private string itemName;
        private Visibility isVisibleSoldier;
        private Visibility isVisibleEditSoldier;
        private Visibility isVisibleEditDictionary;
        private bool isEnabledButtonSoldier;

        public Soldier SoldierSelect
        {
            get { return soldierSelect; }
            set
            {
                soldierSelect = value;
                OnPropertyChanged(nameof(SoldierSelect));
                SetEnabledButtonSoldier(SoldierSelect);

                ProfileNameSelect = SoldierSelect?.ProfileName;
                MilitaryRankSelect = SoldierSelect?.MilitaryRank;
                TypeAccountingSelect = SoldierSelect?.TypeAccounting;

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
        public string ProfileNameSelect
        {
            get { return profileNameSelect; }
            set
            {
                profileNameSelect = value;
                OnPropertyChanged(nameof(ProfileNameSelect));
            }
        }
        public List<string> ProfileNames
        {
            get { return profileNames; }
            set
            {
                profileNames = value;
                OnPropertyChanged(nameof(ProfileNames));
            }
        }
        public string MilitaryRankSelect
        {
            get { return militaryRankSelect; }
            set
            {
                militaryRankSelect = value;
                OnPropertyChanged(nameof(MilitaryRankSelect));
            }
        }
        public List<string> MilitaryRanks
        {
            get { return militaryRanks; }
            set
            {
                militaryRanks = value;
                OnPropertyChanged(nameof(MilitaryRanks));
            }
        }
        public string TypeAccountingSelect
        {
            get { return typeAccountingSelect; }
            set
            {
                typeAccountingSelect = value;
                OnPropertyChanged(nameof(TypeAccountingSelect));
            }
        }
        public List<string> TypeAccountings
        {
            get { return typeAccountings; }
            set
            {
                typeAccountings = value;
                OnPropertyChanged(nameof(TypeAccountings));
            }
        }
        public ComboBoxName DictionaryComboBoxSelect
        {
            get { return dictionaryComboBoxSelect; }
            set
            {
                dictionaryComboBoxSelect = value;
                OnPropertyChanged(nameof(DictionaryComboBoxSelect));
                using (DictionaryRepository dictionaryRepo = new DictionaryRepository())
                {
                    Dictionaries = dictionaryRepo.GetList().Where(d => d.GroupeName == DictionaryComboBoxSelect);
                }
                ItemName = "";
            }
        }
        public List<ComboBoxName> DictionarysComboBox
        {
            get { return dictionarysComboBox; }
            set
            {
                dictionarysComboBox = value;
                OnPropertyChanged(nameof(DictionarysComboBox));
            }
        }
        public Dictionary DictionarySelect
        {
            get { return dictionarySelect; }
            set
            {
                dictionarySelect = value;
                OnPropertyChanged(nameof(DictionarySelect));
                if (DictionarySelect != null)
                {
                    ItemName = DictionarySelect.ItemName;
                }
            }
        }
        public IEnumerable<Dictionary> Dictionaries
        {
            get { return dictionaries; }
            set
            {
                dictionaries = value;
                OnPropertyChanged(nameof(Dictionaries));
            }
        }
        public string ItemName
        {
            get { return itemName; }
            set
            {
                itemName = value;
                OnPropertyChanged(nameof(ItemName));
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
        public Visibility IsVisibleEditDictionary
        {
            get { return isVisibleEditDictionary; }
            set
            {
                isVisibleEditDictionary = value;
                OnPropertyChanged(nameof(IsVisibleEditDictionary));
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
        private Command _saveEditSoldier;
        private Command _cancelEditSoldier;

        private Command _editDictionary;
        private Command _addDictionary;
        private Command _insDictionary;
        private Command _delDictionary;
        private Command _exitDictionary;

        public Command SearchCommand => _searchCommand ?? (_searchCommand = new Command(obj =>
        {
            string search = obj.ToString();
            if (search == "")
            {
                using (SoldierRepository soldierRepo = new SoldierRepository())
                {
                    Soldiers = soldierRepo.GetList();
                }
            }
            else
            {
                using (SoldierRepository soldierRepo = new SoldierRepository())
                {
                    Soldiers = soldierRepo.GetList().Where(n => n.Search.ToUpper().Contains(search.ToUpper()));
                }
            }
        }));
        public Command AddSoldier => _addSoldier ?? (_addSoldier = new Command(obj =>
        {
            SoldierSelect = new Soldier();
            SetVisibleSoldier();
        }));
        public Command InsSoldier => _insSoldier ?? (_insSoldier = new Command(obj =>
        {
            SetVisibleSoldier();
        }));
        public Command DelSoldier => _delSoldier ?? (_delSoldier = new Command(obj =>
        {
            using (SoldierRepository soldierRepo = new SoldierRepository())
            {
                soldierRepo.Delete(SoldierSelect.Id);
                soldierRepo.Save();
                Soldiers = soldierRepo.GetList();
            }
        }));
        public Command SaveEditSoldier => _saveEditSoldier ?? (_saveEditSoldier = new Command(obj =>
        {
            SoldierSelect.ProfileName = ProfileNameSelect;
            SoldierSelect.MilitaryRank = MilitaryRankSelect;
            SoldierSelect.TypeAccounting = TypeAccountingSelect;
            SoldierSelect.Category = (GetEndPeriodToBool(SoldierSelect.BirthDate, 45)) ? "2" : "1";
            SoldierSelect.SubjectToConscription = !GetEndPeriodToBool(SoldierSelect.BirthDate, 43);
            SoldierSelect.Color = (SoldierSelect.RemoveDate == null || SoldierSelect.RemoveDate == DateTime.MinValue) ? "Black" : "Silver";

            if (SoldierSelect.Id > 0)
            {
                using (SoldierRepository soldierRepo = new SoldierRepository())
                {
                    soldierRepo.Update(SoldierSelect);
                    soldierRepo.Save();
                    Soldiers = soldierRepo.GetList();                    
                }

            }
            else
            {
                using (SoldierRepository soldierRepo = new SoldierRepository())
                {
                    soldierRepo.Create(SoldierSelect);
                    soldierRepo.Save();
                    Soldiers = soldierRepo.GetList();                    
                }

            }
            SetVisibleSoldier();
        }));
        public Command CancelEditSoldier => _cancelEditSoldier ?? (_cancelEditSoldier = new Command(obj =>
        {
            SoldierSelect = null;
            SetVisibleSoldier();
        }));

        public Command EditDictionary => _editDictionary ?? (_editDictionary = new Command(obj =>
        {
            IsVisibleEditDictionary = Visibility.Visible;
            IsVisibleSoldier = Visibility.Collapsed;
            IsVisibleEditSoldier = Visibility.Collapsed;
        }));
        public Command AddDictionary => _addDictionary ?? (_addDictionary = new Command(obj =>
        {
            Dictionary dic = new Dictionary()
            {
                GroupeName = DictionaryComboBoxSelect,
                ItemName = ItemName
            };
            using (DictionaryRepository dictionaryRepo = new DictionaryRepository())
            {
                dictionaryRepo.Create(dic);
                dictionaryRepo.Save();

                Dictionaries = null;
                Dictionaries = dictionaryRepo.GetList().Where(d => d.GroupeName == DictionaryComboBoxSelect);
            }
            ItemName = "";
            LoadComboBox();
        }));
        public Command InsDictionary => _insDictionary ?? (_insDictionary = new Command(obj =>
        {
            DictionarySelect.ItemName = ItemName;
            using (DictionaryRepository dictionaryRepo = new DictionaryRepository())
            {
                dictionaryRepo.Update(DictionarySelect);
                dictionaryRepo.Save();

                Dictionaries = null;
                Dictionaries = dictionaryRepo.GetList().Where(d => d.GroupeName == DictionaryComboBoxSelect);
            }
            ItemName = "";
            LoadComboBox();
        }));
        public Command DelDictionary => _delDictionary ?? (_delDictionary = new Command(obj =>
        {
            using (DictionaryRepository dictionaryRepo = new DictionaryRepository())
            {
                dictionaryRepo.Delete(DictionarySelect.Id);
                dictionaryRepo.Save();

                Dictionaries = null;
                Dictionaries = dictionaryRepo.GetList().Where(d => d.GroupeName == DictionaryComboBoxSelect);
            }
            ItemName = "";
            LoadComboBox();
        }));
        public Command ExitDictionary => _exitDictionary ?? (_exitDictionary = new Command(obj =>
        {
            IsVisibleEditDictionary = Visibility.Collapsed;
            IsVisibleSoldier = Visibility.Visible;
            IsVisibleEditSoldier = Visibility.Collapsed;
        }));




        #endregion

        public MainViewModel()
        {
            Recalculation();

            using (SoldierRepository soldierRepo = new SoldierRepository())
            {
                Soldiers = soldierRepo.GetList();
            }

            DictionarysComboBox = new List<ComboBoxName>();
            DictionarysComboBox.Add(ComboBoxName.MilitaryRank);
            DictionarysComboBox.Add(ComboBoxName.ProfileName);
            DictionarysComboBox.Add(ComboBoxName.TypeAccounting);

            LoadComboBox();
            GetVisibleSoldier();
            SetEnabledButtonSoldier(SoldierSelect);
        }

        #region Functions
        /// <summary>
        /// Початкові параметри відображення панелей
        /// </summary>
        private void GetVisibleSoldier()
        {
            IsVisibleSoldier = Visibility.Visible;
            IsVisibleEditSoldier = Visibility.Collapsed;
            IsVisibleEditDictionary = Visibility.Collapsed;
        }
        /// <summary>
        /// Зміна відображення на протилежну
        /// </summary>
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
        /// <summary>
        /// Активація, присвоєння (true) якщо select != null
        /// </summary>
        /// <param name="select"></param>
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
        /// <summary>
        /// Завантаження данних з довідника
        /// </summary>
        private void LoadComboBox()
        {
            ProfileNames = new List<string>();
            MilitaryRanks = new List<string>();
            TypeAccountings = new List<string>();

            using (DictionaryRepository dictionaryRepo = new DictionaryRepository())
            {
                var dic = dictionaryRepo.GetList();

                ProfileNames = dic.Where(d => d.GroupeName == Enums.ComboBoxName.ProfileName).Select(d => d.ItemName).ToList();
                MilitaryRanks = dic.Where(d => d.GroupeName == Enums.ComboBoxName.MilitaryRank).Select(d => d.ItemName).ToList();
                TypeAccountings = dic.Where(d => d.GroupeName == Enums.ComboBoxName.TypeAccounting).Select(d => d.ItemName).ToList();
            }

            
        }
        /// <summary>
        /// Вираховує кількість років з дати народження, якщо менше 45 то категорія № 1, інакше № 2 
        /// </summary>
        /// <param name="startDate"></param>
        /// <returns></returns>
        private bool GetEndPeriodToBool(DateTime? startDate, int count)
        {
            DateTime endDate = startDate?.AddYears(count) ?? DateTime.MinValue;
            return (endDate <= DateTime.Today);
        }
        /// <summary>
        /// Перераховує кількість років від народження і до сьогодні, та змінює категорію
        /// </summary>
        private void Recalculation()
        {
            using (SoldierRepository soldierRepo = new SoldierRepository())
            {
                var soldier = soldierRepo.GetList();
                foreach (var item in soldier)
                {
                    item.Category = (GetEndPeriodToBool(item.BirthDate, 45)) ? "2" : "1";
                    item.SubjectToConscription = !GetEndPeriodToBool(item.BirthDate, 43);                    
                    soldierRepo.Update(item);
                    soldierRepo.Save();
                }
            }
        }
        #endregion
    }
}
