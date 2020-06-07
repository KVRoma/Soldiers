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
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using System.Threading;

namespace Soldiers.ViewModel
{
    public class MainViewModel : ViewModel
    {
        #region Property
        public string NameWindow { get; } = "Soldiers - 2020";
        //private readonly SoldierRepository soldierRepo = new SoldierRepository();
        //private readonly DictionaryRepository dictionaryRepo = new DictionaryRepository();
        private int reportNumber;
        private IEnumerable<VOS> vos;
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

        private string countItems;
        private bool isCheckedStart;
        private bool isCheckedReportOne;
        private bool isCheckedReportTwo;
        private Visibility isVisibleReportButton;
        private Visibility isVisibleProgressBar;
        private decimal isOpacity;
        private DateTime dateReport;

        public IEnumerable<VOS> Vos
        {
            get { return vos; }
            set
            {
                vos = value;
                OnPropertyChanged(nameof(Vos));
            }
        }

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
        public Visibility IsVisibleProgressBar
        {
            get { return isVisibleProgressBar; }
            set
            {
                isVisibleProgressBar = value;
                OnPropertyChanged(nameof(IsVisibleProgressBar));
            }
        }
        public decimal IsOpacity
        {
            get { return isOpacity; }
            set
            {
                isOpacity = value;
                OnPropertyChanged(nameof(IsOpacity));
            }
        }
        public DateTime DateReport
        {
            get { return dateReport; }
            set
            {
                dateReport = value;
                OnPropertyChanged(nameof(DateReport));
            }
        }

        public string CountItems
        {
            get { return countItems; }
            set
            {
                countItems = value;
                OnPropertyChanged(nameof(CountItems));
            }
        }
        public bool IsCheckedStart
        {
            get { return isCheckedStart; }
            set
            {
                isCheckedStart = value;
                OnPropertyChanged(nameof(IsCheckedStart));
                CountItems = "Натисніть <Перегляд> ...";
            }
        }
        public bool IsCheckedReportOne
        {
            get { return isCheckedReportOne; }
            set
            {
                isCheckedReportOne = value;
                OnPropertyChanged(nameof(IsCheckedReportOne));
                CountItems = "Натисніть <Перегляд> ...";
            }
        }
        public bool IsCheckedReportTwo
        {
            get { return isCheckedReportTwo; }
            set
            {
                isCheckedReportTwo = value;
                OnPropertyChanged(nameof(IsCheckedReportTwo));
                CountItems = "Натисніть <Перегляд> ...";
            }
        }
        public Visibility IsVisibleReportButton
        {
            get { return isVisibleReportButton; }
            set
            {
                isVisibleReportButton = value;
                OnPropertyChanged(nameof(IsVisibleReportButton));
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

        private Command _viewCommand;
        private Command _printReport;

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

        public Command ViewCommand => _viewCommand ?? (_viewCommand = new Command(obj =>
        {
            if (IsCheckedStart)
            {
                GetSoldiers();
            }
            if (IsCheckedReportOne)
            {
                GetReportOne(DateReport);
            }
            if (IsCheckedReportTwo)
            {
                GetReportTwo(DateReport);
            }

        }));
        public Command PrintReport => _printReport ?? (_printReport = new Command(async obj =>
        {
            switch (reportNumber)
            {
                case 1:
                    {
                        StartProgressBar();
                        await PrintReportOne("\\Blanks\\ReportOne.xltx");
                        StopProgressBar();
                    }
                    break;
                case 2:
                    {
                        StartProgressBar();
                        await PrintReportTwo("\\Blanks\\ReportTwo.xltx");
                        StopProgressBar();
                    }
                    break;
                default:
                    break;

            }

        }));

        

        #endregion

        public MainViewModel()
        {
            StopProgressBar();
            Recalculation();

            GetSoldiers();

            DateReport = DateTime.Today;

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
        /// Робить видимим ProgressBar
        /// </summary>
        private void StartProgressBar()
        {
            IsVisibleProgressBar = Visibility.Visible;
            IsOpacity = 0.2m;
        }
        /// <summary>
        /// Приховує відображення ProgressBar
        /// </summary>
        private void StopProgressBar()
        {
            IsVisibleProgressBar = Visibility.Collapsed;
            IsOpacity = 1m;
        }
        /// <summary>
        /// Повертає загальний список з бази та виділяє перший пункт в меню
        /// </summary>
        private void GetSoldiers()
        {
            IsCheckedStart = true;
            IsVisibleReportButton = Visibility.Collapsed;
            reportNumber = 0;
            using (SoldierRepository soldierRepo = new SoldierRepository())
            {
                Soldiers = soldierRepo.GetList();
            }
            CountItems = "Загальна кількість відібраних - " + Soldiers.Count() + " шт.";
        }
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
        /// Завантаження данних з довідників
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
            using (VOSRepository vosRepo = new VOSRepository())
            {
                Vos = vosRepo.GetList();
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
        /// <summary>
        /// Формує звіт "до 43 років" станом на "date".
        /// </summary>
        /// <param name="date"></param>
        private void GetReportOne(DateTime date)
        {
            IsVisibleReportButton = Visibility.Visible;
            Soldiers = Soldiers.Where(s => s.TypeAccounting == "Загальний" && s.SubjectToConscription == true && s.AcceptedDate <= date);
            CountItems = "Загальна кількість відібраних - " + Soldiers.Count() + " шт.";
            reportNumber = 1;
        }
        /// <summary>
        /// Формує звіт з шаблону Excel "до 43 років"
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private async Task PrintReportOne(string path)
        {
            await Task.Run(() =>
            {                
                Excel.Application ExcelApp = new Excel.Application();
                Excel.Workbook ExcelWorkBook;
                ExcelWorkBook = ExcelApp.Workbooks.Open(Environment.CurrentDirectory + path);   //Вказуємо шлях до шаблону

                int i = 7;
                foreach (var item in Vos)
                {
                    ExcelApp.Cells[i, 2] = Soldiers.Where(s => s.VOSzvit == item.Name)?.Count();
                    i++;
                }                
                ExcelApp.Cells[1, 18] = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                
                ExcelApp.Visible = true;           // Робим книгу видимою
                ExcelApp.UserControl = true;       // Передаємо керування користувачу                
            });
        }
        /// <summary>
        /// Формує звіт "3.27" станом на "date".
        /// </summary>
        /// <param name="date"></param>
        private void GetReportTwo(DateTime date)
        {
            IsVisibleReportButton = Visibility.Visible;
            Soldiers = Soldiers.Where(s => s.TypeAccounting == "Загальний" && s.AccountingOther == true && s.AcceptedDate <= date);
            CountItems = "Загальна кількість відібраних - " + Soldiers.Count() + " шт.";
            reportNumber = 2;
        }
        /// <summary>
        /// Формує звіт з шаблону Excel "3.27"
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private async Task PrintReportTwo(string path)
        {
            await Task.Run(() =>
            {
                Excel.Application ExcelApp = new Excel.Application();
                Excel.Workbook ExcelWorkBook;
                ExcelWorkBook = ExcelApp.Workbooks.Open(Environment.CurrentDirectory + path);   //Вказуємо шлях до шаблону

                int i = 11;
                foreach (var item in Vos)
                {
                    ExcelApp.Cells[i, 2] = Soldiers.Where(s => s.VOSzvit == item.Name && s.Gender == true && s.Category == "1")?.Count();
                    ExcelApp.Cells[i, 3] = Soldiers.Where(s => s.VOSzvit == item.Name && s.Gender == true && s.Category == "2")?.Count();
                    ExcelApp.Cells[i, 5] = Soldiers.Where(s => s.VOSzvit == item.Name && s.Gender == false && s.Category == "1")?.Count();
                    ExcelApp.Cells[i, 6] = Soldiers.Where(s => s.VOSzvit == item.Name && s.Gender == false && s.Category == "2")?.Count();
                    i++;
                }
                ExcelApp.Cells[1, 18] = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);

                ExcelApp.Visible = true;           // Робим книгу видимою
                ExcelApp.UserControl = true;       // Передаємо керування користувачу                
            });
        }
        #endregion
    }
}
