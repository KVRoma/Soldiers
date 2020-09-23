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
        private bool isCheckedReportThree;
        private bool isCheckedReportFour;

        private bool isFilterUBD;
        private bool isFilterOR1;
        private bool isFilterOR2;
        private bool isFilterAssignedTeam;
        private bool isFilterTypeAccounting;
        private bool isFilterRemoveDate;
        private bool isFilterAcceptedDate;
        private bool isFilterAccountingOther;
        private bool isFilterUnsuitable;
        private bool isFilterRightToDefer;
        private bool isFilterGender;
        

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
        public bool IsCheckedReportThree
        {
            get { return isCheckedReportThree; }
            set
            {
                isCheckedReportThree = value;
                OnPropertyChanged(nameof(IsCheckedReportThree));
                CountItems = "Натисніть <Перегляд> ...";
            }
        }
        public bool IsCheckedReportFour
        {
            get { return isCheckedReportFour; }
            set
            {
                isCheckedReportFour = value;
                OnPropertyChanged(nameof(IsCheckedReportFour));
                CountItems = "Натисніть <Перегляд> ...";
            }
        }

        public bool IsFilterUBD
        {
            get { return isFilterUBD; }
            set
            {
                isFilterUBD = value;
                OnPropertyChanged(nameof(IsFilterUBD));
            }
        }
        public bool IsFilterOR1
        {
            get { return isFilterOR1; }
            set
            {
                isFilterOR1 = value;
                OnPropertyChanged(nameof(IsFilterOR1));
            }
        }
        public bool IsFilterOR2
        {
            get { return isFilterOR2; }
            set
            {
                isFilterOR2 = value;
                OnPropertyChanged(nameof(IsFilterOR2));
            }
        }
        public bool IsFilterAssignedTeam 
        { 
            get { return isFilterAssignedTeam; } 
            set 
            { 
                isFilterAssignedTeam = value;
                OnPropertyChanged(nameof(IsFilterAssignedTeam));
            } 
        }
        public bool IsFilterTypeAccounting
        {
            get { return isFilterTypeAccounting; }
            set
            {
                isFilterTypeAccounting = value;
                OnPropertyChanged(nameof(IsFilterTypeAccounting));
            }
        }
        public bool IsFilterRemoveDate 
        { 
            get { return isFilterRemoveDate; } 
            set 
            {
                isFilterRemoveDate = value;
                OnPropertyChanged(nameof(IsFilterRemoveDate));
            } 
        }
        public bool IsFilterAcceptedDate
        {
            get { return isFilterAcceptedDate; }
            set
            {
                isFilterAcceptedDate = value;
                OnPropertyChanged(nameof(IsFilterAcceptedDate));
            }
        }
        public bool IsFilterAccountingOther
        {
            get { return isFilterAccountingOther; }
            set
            {
                isFilterAccountingOther = value;
                OnPropertyChanged(nameof(IsFilterAccountingOther));
            }
        }
        public bool IsFilterUnsuitable
        {
            get { return isFilterUnsuitable; }
            set
            {
                isFilterUnsuitable = value;
                OnPropertyChanged(nameof(IsFilterUnsuitable));
            }
        }
        public bool IsFilterRightToDefer
        {
            get { return isFilterRightToDefer; }
            set
            {
                isFilterRightToDefer = value;
                OnPropertyChanged(nameof(IsFilterRightToDefer));
            }
        }
        public bool IsFilterGender
        {
            get { return isFilterGender; }
            set
            {
                isFilterGender = value;
                OnPropertyChanged(nameof(IsFilterGender));
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
                IsCheckedStart = true;
                ViewCommand.Execute("");
            }
            else
            {                
                Soldiers = Soldiers.Where(n => n.Search.ToUpper().Contains(search.ToUpper()));                
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
            SoldierSelect.SubjectToConscription = SoldierSelect.MilitaryService ? false : true;         //спочатку перевіряєм чи служив
            if (GetEndPeriodToBool(SoldierSelect.BirthDate, 43))                                        // потім дивимось чи ще проходить по віку
            {
                SoldierSelect.SubjectToConscription = false;
            }            
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
            if (IsCheckedReportThree)
            {
                GetReportThree();
            }
            if (IsCheckedReportFour)
            {
                GetReportFour(DateReport);
            }
            
            if (IsFilterUBD)
            {
                GetUBD(DateReport);
            }
            if (IsFilterOR1)
            {
                GetOR1(DateReport);
            }
            if (IsFilterOR2)
            {
                GetOR2(DateReport);
            }
            if (IsFilterAssignedTeam)
            {
                GetAssignedTeam(DateReport);
            }
            if (IsFilterTypeAccounting)
            {
                GetTypeAccounting(DateReport);
            }
            if (IsFilterRemoveDate)
            {
                GetRemoveDate(DateReport);
            }
            if (IsFilterAcceptedDate)
            {
                GetAcceptedDate(DateReport);
            }
            if (IsFilterAccountingOther)
            {
                GetAccountingOther(DateReport);
            }
            if (IsFilterUnsuitable)
            {
                GetUnsuitable(DateReport);
            }
            if (IsFilterRightToDefer)
            {
                GetRightToDefer(DateReport);
            }
            if (IsFilterGender)
            {
                GetGender(DateReport);
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
                case 3:
                    {
                        StartProgressBar();
                        await PrintReportThree("\\Blanks\\ReportThree.xltx", DateReport);
                        StopProgressBar();
                    }
                    break;
                case 4:
                    {
                        StartProgressBar();
                        await PrintReportFour("\\Blanks\\ReportFour.xltx", DateReport);
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
        /// Вираховує кількість років з дати народження, якщо (рік народження + count) менше або дорівнює сьогодняшній даті то true, інакше false 
        /// </summary>
        /// <param name="startDate"></param>
        /// <returns></returns>
        private bool GetEndPeriodToBool(DateTime? startDate, int count)
        {
            DateTime endDate = startDate?.AddYears(count) ?? DateTime.MinValue;
            return (endDate <= DateTime.Today);
        }
        /// <summary>
        /// Перераховує кількість років від народження і до сьогодні, та змінює категорію (якщо 45 і більше то "2", менше 45 то "1") + змінює "Підлягає призову" по віку (43)
        /// </summary>
        private void Recalculation()
        {
            using (SoldierRepository soldierRepo = new SoldierRepository())
            {
                var soldier = soldierRepo.GetList();
                foreach (var item in soldier)
                {
                    item.Category = (GetEndPeriodToBool(item.BirthDate, 45)) ? "2" : "1";
                    if (GetEndPeriodToBool(item.BirthDate, 43))
                    {
                        item.SubjectToConscription = false;
                    }                                       

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
            Soldiers = Soldiers.Where(s => s.TypeAccounting == "Загальний" && s.SubjectToConscription == true && s.AcceptedDate <= date)?.Where(r => r.RemoveDate > date || r.RemoveDate == null);
            CountItems = "Загальна кількість відібраних - " + Soldiers.Count() + " шт.";
            reportNumber = 1;
        }
        /// <summary>
        /// Друкує звіт з шаблону Excel "до 43 років"
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
            Soldiers = Soldiers.Where(s => s.TypeAccounting == "Загальний" && 
                                           s.AccountingOther == true && 
                                           s.AcceptedDate <= date)?
                                .Where(r => r.RemoveDate > date || r.RemoveDate == null);            
            CountItems = "Загальна кількість відібраних - " + Soldiers.Count() + " шт.";
            reportNumber = 2;
        }
        /// <summary>
        /// Друкує звіт з шаблону Excel "3.27"
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
        /// <summary>
        ///  Формує звіт "3.28" станом на "date".
        /// </summary>
        /// <param name="date"></param>
        private void GetReportThree()
        {
            IsVisibleReportButton = Visibility.Visible;
            CountItems = "Загальна кількість відібраних - " + Soldiers.Count() + " шт.";
            reportNumber = 3;
        }
        /// <summary>
        /// Друкує звіт "3.28" станом на "date".
        /// </summary>
        /// <param name="path"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        private async Task PrintReportThree(string path, DateTime date)
        {
            await Task.Run(() =>
            {
                Excel.Application ExcelApp = new Excel.Application();
                Excel.Workbook ExcelWorkBook;
                ExcelWorkBook = ExcelApp.Workbooks.Open(Environment.CurrentDirectory + path);   //Вказуємо шлях до шаблону

                DateTime oldDate = new DateTime(date.Year,1,1);
                //Було всього на 01.01.хххх
                ExcelApp.Cells[7, 7] = Soldiers.Where(s=>s.AcceptedDate < oldDate && s.Category == "1")?.Where(r=>r.RemoveDate > oldDate || r.RemoveDate == null).Count();
                ExcelApp.Cells[8, 7] = Soldiers.Where(s => s.AcceptedDate < oldDate && s.Category == "2")?.Where(r => r.RemoveDate > oldDate || r.RemoveDate == null).Count();
                // Взято на облік
                ExcelApp.Cells[10, 7] = Soldiers.Where(s => s.AcceptedDate >= oldDate && s.AcceptedDate <= date && s.Category == "1")?.Count();
                ExcelApp.Cells[11, 7] = Soldiers.Where(s => s.AcceptedDate >= oldDate && s.AcceptedDate <= date && s.Category == "2")?.Count();
                // Знято з обліку
                ExcelApp.Cells[13, 7] = Soldiers.Where(s => s.RemoveDate >= oldDate && s.RemoveDate <= date && s.Category == "1")?.Count();
                ExcelApp.Cells[14, 7] = Soldiers.Where(s => s.RemoveDate >= oldDate && s.RemoveDate <= date && s.Category == "2")?.Count();
                // Виключені з обліку
                //ExcelApp.Cells[16, 7] = "";
                //ExcelApp.Cells[17, 7] = "";               
                var totalAccounting = Soldiers.Where(s =>s.AcceptedDate <= date && s.TypeAccounting == "Загальний").Where(r => r.RemoveDate > date || r.RemoveDate == null);
                var specialAccounting = Soldiers.Where(s => s.AcceptedDate <= date && s.TypeAccounting == "Спеціальний").Where(r => r.RemoveDate > date || r.RemoveDate == null);
                // Перебуває на "Спеціальному"
                ExcelApp.Cells[22, 7] = specialAccounting?.Where(s => s.Category == "1")?.Count();
                ExcelApp.Cells[23, 7] = specialAccounting?.Where(s => s.Category == "2")?.Count();
                // Призначені до складу команд
                ExcelApp.Cells[28, 7] = totalAccounting.Where(s => s.AssignedTeam == true && s.Category == "1")?.Count();
                ExcelApp.Cells[29, 7] = totalAccounting.Where(s => s.AssignedTeam == true && s.Category == "2")?.Count();
                ExcelApp.Cells[30, 7] = totalAccounting.Where(s => s.AssignedTeam == true && s.OR1 == true)?.Count();

                //var test = totalAccounting.Where(s => s.AccountingOther == true)?.Count();
                //var test1 = totalAccounting.Where(s => s.AssignedTeam == false)?.Count();
                //var test2 = totalAccounting.Where(s => s.SubjectToConscription == true)?.Count();
                //Console.Out.WriteLine("Вільні залишки - {0} шт. не призначені - {1} шт. підлягає призову - {2} шт.", test, test1, test2);

                //// Вільні ресурси (підлягають призову)
                //ExcelApp.Cells[41, 7] = totalAccounting.Where(s => s.AccountingOther == true  && s.Category == "1")?.Count();
                //ExcelApp.Cells[42, 7] = totalAccounting.Where(s => s.AccountingOther == true  && s.Category == "2")?.Count();
                
                // Право на відстрочку
                int? rightToDefer1 = totalAccounting.Where(s => s.AccountingOther == true && s.RightToDefer == true && s.Category == "1")?.Count();
                int? rightToDefer2 = totalAccounting.Where(s => s.AccountingOther == true && s.RightToDefer == true && s.Category == "2")?.Count();
                ExcelApp.Cells[44, 7] = rightToDefer1;
                ExcelApp.Cells[45, 7] = rightToDefer2;
                // Жінки
                int? gender1 = totalAccounting.Where(s => s.AccountingOther == true && s.Gender == false && s.Category == "1")?.Count();
                int? gender2 = totalAccounting.Where(s => s.AccountingOther == true && s.Gender == false && s.Category == "2")?.Count();
                ExcelApp.Cells[53, 7] = gender1;
                ExcelApp.Cells[54, 7] = gender2;
                // Непридатні
                int? unsuitable1 = totalAccounting.Where(s => s.AccountingOther == true && s.Unsuitable == true && s.Category == "1")?.Count();
                int? unsuitable2 = totalAccounting.Where(s => s.AccountingOther == true && s.Unsuitable == true && s.Category == "2")?.Count();
                ExcelApp.Cells[56, 7] = unsuitable1;
                ExcelApp.Cells[57, 7] = unsuitable2;
                // Підлягають призову для звіту 3.28
                var temp1 = totalAccounting.Where(s => s.AccountingOther == true &&
                                                        s.RightToDefer == false &&
                                                        s.Gender == true &&
                                                        s.Unsuitable == false && 
                                                        s.Category == "1");

                var temp2 = totalAccounting.Where(s => s.AccountingOther == true &&
                                                        s.RightToDefer == false &&
                                                        s.Gender == true &&
                                                        s.Unsuitable == false &&
                                                        s.Category == "2");

               
                // OP-1
                int? or1 = temp1.Where(s => s.OR1 == true )?.Count();
                int? or2 = temp2.Where(s => s.OR1 == true )?.Count();
                ExcelApp.Cells[62, 7] = or1;
                ExcelApp.Cells[63, 7] = or2;
                // Вільні ресурси
                ExcelApp.Cells[41, 7] = temp1?.Count() - or1;
                ExcelApp.Cells[42, 7] = temp2?.Count() - or2;


                ExcelApp.Cells[1, 18] = oldDate;

                ExcelApp.Visible = true;           // Робим книгу видимою
                ExcelApp.UserControl = true;       // Передаємо керування користувачу                
            });
        }
        /// <summary>
        /// Формує звіт "Загальна відомість" станом на "date". 
        /// </summary>
        private void GetReportFour(DateTime date)
        {
            IsVisibleReportButton = Visibility.Visible;
            Soldiers = Soldiers.Where(s => s.AcceptedDate <= date)?.Where(r => r.RemoveDate > date || r.RemoveDate == null);
            CountItems = "Загальна кількість відібраних - " + Soldiers.Count() + " шт.";
            reportNumber = 4;
        }
        /// <summary>
        /// Друкує звіт "Загальна відомість" станом на "date"
        /// </summary>
        /// <param name="path"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        private async Task PrintReportFour(string path, DateTime date)
        {
            await Task.Run(() =>
            {
                Excel.Application ExcelApp = new Excel.Application();
                Excel.Workbook ExcelWorkBook;
                ExcelWorkBook = ExcelApp.Workbooks.Open(Environment.CurrentDirectory + path);   //Вказуємо шлях до шаблону

                DateTime oldDate = new DateTime(date.Year, 1, 1);

                int i = 10;
                foreach (var item in Vos)
                {
                    var temp = Soldiers.Where(s => s.VOSzvit == item.Name); // відбираємо по ВОС
                    ExcelApp.Cells[i, 3] = temp.Where(s => s.UBD == true)?.Count(); // наявність УБД
                    ExcelApp.Cells[i, 4] = temp.Where(s => s.UBD == true && s.ATO == true)?.Count(); // наявність УБД та участь в АТО
                    ExcelApp.Cells[i, 5] = temp.Where(s => s.MilitaryService == false)?.Count(); // без досвіду проходження служби
                    ExcelApp.Cells[i, 6] = temp.Where(s => s.AccountingTotal == true)?.Count(); // на загальному обліку
                    ExcelApp.Cells[i, 7] = temp.Where(s => s.AccountingTotal == true && s.Category == "1")?.Count(); // на загальному обліку, 1 розряду
                    ExcelApp.Cells[i, 8] = temp.Where(s => s.AccountingTotal == true && s.Category == "2")?.Count(); // на загальному обліку, 2 розряду
                    ExcelApp.Cells[i, 9] = temp.Where(s => s.AccountingTotal == true && s.Gender == false)?.Count(); // на загальному обліку, жінки
                    ExcelApp.Cells[i, 10] = temp.Where(s => s.AccountingTotal == false)?.Count(); // на спеціальному обліку
                    ExcelApp.Cells[i, 11] = temp.Where(s => s.AccountingTotal == false && s.Gender == false)?.Count(); // на спеціальному обліку, жінки
                    ExcelApp.Cells[i, 12] = temp.Where(s => s.AssignedTeam == true)?.Count(); // призначені
                    ExcelApp.Cells[i, 17] = temp.Where(s => s.AssignedTeam == true && s.Gender == false)?.Count(); // призначені, жінки
                    ExcelApp.Cells[i, 18] = temp.Where(s => s.OR1 == true)?.Count(); // призначені в ОР1
                    ExcelApp.Cells[i, 19] = temp.Where(s => s.AccountingOther == true)?.Count(); // Вільні залишки
                    ExcelApp.Cells[i, 20] = temp.Where(s => s.AccountingOther == true && s.Category == "1")?.Count(); // Вільні залишки, 1 розряду
                    ExcelApp.Cells[i, 21] = temp.Where(s => s.AccountingOther == true && s.Category == "2")?.Count(); // Вільні залишки, 2 розряду
                    ExcelApp.Cells[i, 22] = temp.Where(s => s.AccountingOther == true && s.MilitaryService == false)?.Count(); // Вільні залишки, без досвіду служби
                    ExcelApp.Cells[i, 23] = temp.Where(s => s.AccountingOther == true && s.Gender == false)?.Count(); // Вільні залишки, жінки
                    ExcelApp.Cells[i, 24] = temp.Where(s => s.AccountingOther == true && s.SubjectToConscription == true)?.Count(); // Вільні залишки, підлягають призову
                    i++;
                }

                ExcelApp.Visible = true;           // Робим книгу видимою
                ExcelApp.UserControl = true;       // Передаємо керування користувачу                
            });
        }
        /// <summary>
        /// УБД на дату
        /// </summary>
        /// <param name="date"></param>
        private void GetUBD(DateTime date)
        {
            IsVisibleReportButton = Visibility.Collapsed;
            Soldiers = Soldiers.Where(s => s.UBD == true && s.AcceptedDate <= date)?.Where(r => r.RemoveDate > date || r.RemoveDate == null);
            CountItems = "Загальна кількість відібраних (УБД) - " + Soldiers.Count() + " шт."; 
            reportNumber = 5;
        }
        /// <summary>
        /// ОР1 на дату
        /// </summary>
        /// <param name="date"></param>
        private void GetOR1(DateTime date)
        {
            IsVisibleReportButton = Visibility.Collapsed;
            Soldiers = Soldiers.Where(s => s.OR1 == true && s.AcceptedDate <= date)?.Where(r => r.RemoveDate > date || r.RemoveDate == null);
            CountItems = "Загальна кількість відібраних (ОР1) - " + Soldiers.Count() + " шт.";
            reportNumber = 6;
        }
        /// <summary>
        /// ОР2 на дату
        /// </summary>
        /// <param name="date"></param>
        private void GetOR2(DateTime date)
        {
            IsVisibleReportButton = Visibility.Collapsed;
            Soldiers = Soldiers.Where(s => s.OR2 == true && s.AcceptedDate <= date)?.Where(r => r.RemoveDate > date || r.RemoveDate == null);
            CountItems = "Загальна кількість відібраних (ОР2) - " + Soldiers.Count() + " шт.";
            reportNumber = 7;
        }
        /// <summary>
        /// Призначені на дату
        /// </summary>
        /// <param name="date"></param>
        private void GetAssignedTeam(DateTime date)
        {
            IsVisibleReportButton = Visibility.Collapsed;
            Soldiers = Soldiers.Where(s => s.AssignedTeam == true && s.AcceptedDate <= date)?.Where(r => r.RemoveDate > date || r.RemoveDate == null);
            CountItems = "Загальна кількість відібраних (Призначені) - " + Soldiers.Count() + " шт.";
            reportNumber = 8;
        }
        /// <summary>
        /// Спеціальний на дату
        /// </summary>
        /// <param name="date"></param>
        private void GetTypeAccounting(DateTime date)  // Спеціальний
        {
            IsVisibleReportButton = Visibility.Collapsed;
            Soldiers = Soldiers.Where(s => s.TypeAccounting == "Спеціальний" && s.AcceptedDate <= date)?.Where(r => r.RemoveDate > date || r.RemoveDate == null);
            CountItems = "Загальна кількість відібраних (Спеціальний) - " + Soldiers.Count() + " шт.";
            reportNumber = 9;
        }
        /// <summary>
        /// Зняті з обліку на дату
        /// </summary>
        /// <param name="date"></param>
        private void GetRemoveDate(DateTime date)
        {
            IsVisibleReportButton = Visibility.Collapsed;
            Soldiers = Soldiers.Where(s => s.RemoveDate != null)?.Where(s => s.RemoveDate < date);
            CountItems = "Загальна кількість відібраних (Зняті з обліку) - " + Soldiers.Count() + " шт.";
            reportNumber = 10;
        }
        /// <summary>
        /// Взяті на облік на дату
        /// </summary>
        /// <param name="date"></param>      
        private void GetAcceptedDate(DateTime date)
        {
            IsVisibleReportButton = Visibility.Collapsed;
            Soldiers = Soldiers.Where(s => s.AcceptedDate <= date)?.Where(r => r.RemoveDate > date || r.RemoveDate == null);
            CountItems = "Загальна кількість відібраних (Взяті на облік) - " + Soldiers.Count() + " шт.";
            reportNumber = 11;
        }
        /// <summary>
        /// Вільні залишки на дату
        /// </summary>
        /// <param name="date"></param>
        private void GetAccountingOther(DateTime date)
        {
            IsVisibleReportButton = Visibility.Collapsed;
            Soldiers = Soldiers.Where(s => s.AccountingOther == true && s.AcceptedDate <= date)?.Where(r => r.RemoveDate > date || r.RemoveDate == null);
            CountItems = "Загальна кількість відібраних (Вільні залишки) - " + Soldiers.Count() + " шт.";
            reportNumber = 12;
        }
        /// <summary>
        /// Непридатні на дату
        /// </summary>
        /// <param name="date"></param>
        private void GetUnsuitable(DateTime date)
        {
            IsVisibleReportButton = Visibility.Collapsed;
            Soldiers = Soldiers.Where(s => s.Unsuitable == true && s.AcceptedDate <= date)?.Where(r => r.RemoveDate > date || r.RemoveDate == null);
            CountItems = "Загальна кількість відібраних (Непридатні) - " + Soldiers.Count() + " шт.";
            reportNumber = 13;
        }
        /// <summary>
        /// Право на відстрочку на дату
        /// </summary>
        /// <param name="date"></param>
        private void GetRightToDefer(DateTime date)
        {
            IsVisibleReportButton = Visibility.Collapsed;
            Soldiers = Soldiers.Where(s => s.RightToDefer == true && s.AcceptedDate <= date)?.Where(r => r.RemoveDate > date || r.RemoveDate == null);
            CountItems = "Загальна кількість відібраних (Прово на відстрочку) - " + Soldiers.Count() + " шт.";
            reportNumber = 14;
        }
        /// <summary>
        /// Жінки на дату
        /// </summary>
        /// <param name="date"></param>
        private void GetGender(DateTime date)
        {
            IsVisibleReportButton = Visibility.Collapsed;
            Soldiers = Soldiers.Where(s => s.Gender == false && s.AcceptedDate <= date)?.Where(r => r.RemoveDate > date || r.RemoveDate == null);
            CountItems = "Загальна кількість відібраних (Жінки) - " + Soldiers.Count() + " шт.";
            reportNumber = 15;
        }

        #endregion
    }
}
