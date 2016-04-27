using DaocSpellCraftCalculator.Models;
using DaocSpellCraftCalculator.Tools;
using DaocSpellCraftCalculator.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Xml.Serialization;
using Telerik.Windows.Controls;

namespace DaocSpellCraftCalculator.ViewModels
{
    [XmlRoot("Template")]
    public class TemplateViewModel : GalaSoft.MvvmLight.ViewModelBase, IRequestCloseViewModel
    {

        #region Constructor

        public TemplateViewModel()
        {
            this.Name = "New template";
            this.Realm = null;
            this.Class = null;
            this.Race = null;
            this._Level = 50;
            this.RealmRank = 1;
            this.LstBonuses = new ObservableCollection<StatModel>();
            this.LstCaracs = new ObservableCollection<StatModel>();
            this.LstClasses = new ObservableCollection<ClassModel>();
            this.LstRaces = new ObservableCollection<RaceModel>();
            this.LstRealms = DataProvider.Current.Realms;
            this.LstResists = new ObservableCollection<StatModel>();
            this.LstSkills = new ObservableCollection<StatModel>();


            this.Torso = new ItemViewModel("TOR", false, true);
            this.Head = new ItemViewModel("HEA", false, true);
            this.Legs = new ItemViewModel("LEG", false, true);
            this.Arms = new ItemViewModel("ARM", false, true);
            this.Hands = new ItemViewModel("HAN", false, true);
            this.Feet = new ItemViewModel("FEE", false, true);

            this.Necklace = new ItemViewModel("NEC", true, true);
            this.Myth = new ItemViewModel("MYT", true, true);
            this.Cloak = new ItemViewModel("CLO", true, true);
            this.Jewel = new ItemViewModel("JEW", true, true);
            this.Belt = new ItemViewModel("BEL", true, true);
            this.LWrist = new ItemViewModel("LWR", true, true);
            this.RWrist = new ItemViewModel("RWR", true, true);
            this.LRing = new ItemViewModel("RRI", true, true);
            this.RRing = new ItemViewModel("LRI", true, true);

            this.MainHand = new ItemViewModel("MH", true, true);
            this.OffHand = new ItemViewModel("OH", true, true);
            this.TwoHands = new ItemViewModel("TWO", true, false);
            this.Range = new ItemViewModel("RAN", true, false);

            this.IncludeRaciales = Settings.Current.IncludeRaciales;

            this.UpdateTemplate();
        }

        #endregion


        #region Commands



        private RelayCommand _SetTorsoSelectedCommand;
        public RelayCommand SetTorsoSelectedCommand
        {
            get
            {
                return _SetTorsoSelectedCommand ?? (_SetTorsoSelectedCommand = new RelayCommand(SetTorsoSelected));
            }
        }

        private RelayCommand _SetHeadSelectedCommand;
        public RelayCommand SetHeadSelectedCommand
        {
            get
            {
                return _SetHeadSelectedCommand ?? (_SetHeadSelectedCommand = new RelayCommand(SetHeadSelected));
            }
        }

        private RelayCommand _SetArmsSelectedCommand;
        public RelayCommand SetArmsSelectedCommand
        {
            get
            {
                return _SetArmsSelectedCommand ?? (_SetArmsSelectedCommand = new RelayCommand(SetArmsSelected));
            }
        }

        private RelayCommand _SetHandsSelectedCommand;
        public RelayCommand SetHandsSelectedCommand
        {
            get
            {
                return _SetHandsSelectedCommand ?? (_SetHandsSelectedCommand = new RelayCommand(SetHandsSelected));
            }
        }

        private RelayCommand _SetLegsSelectedCommand;
        public RelayCommand SetLegsSelectedCommand
        {
            get
            {
                return _SetLegsSelectedCommand ?? (_SetLegsSelectedCommand = new RelayCommand(SetLegsSelected));
            }
        }

        private RelayCommand _SetFeetSelectedCommand;
        public RelayCommand SetFeetSelectedCommand
        {
            get
            {
                return _SetFeetSelectedCommand ?? (_SetFeetSelectedCommand = new RelayCommand(SetFeetSelected));
            }
        }


        private RelayCommand _SetNeckSelectedCommand;
        public RelayCommand SetNeckSelectedCommand
        {
            get
            {
                return _SetNeckSelectedCommand ?? (_SetNeckSelectedCommand = new RelayCommand(SetNeckSelected));
            }
        }

        private RelayCommand _SetMythSelectedCommand;
        public RelayCommand SetMythSelectedCommand
        {
            get
            {
                return _SetMythSelectedCommand ?? (_SetMythSelectedCommand = new RelayCommand(SetMythSelected));
            }
        }

        private RelayCommand _SetCloakSelectedCommand;
        public RelayCommand SetCloakSelectedCommand
        {
            get
            {
                return _SetCloakSelectedCommand ?? (_SetCloakSelectedCommand = new RelayCommand(SetCloakSelected));
            }
        }

        private RelayCommand _SetJewelSelectedCommand;
        public RelayCommand SetJewelSelectedCommand
        {
            get
            {
                return _SetJewelSelectedCommand ?? (_SetJewelSelectedCommand = new RelayCommand(SetJewelSelected));
            }
        }

        private RelayCommand _SetBeltSelectedCommand;
        public RelayCommand SetBeltSelectedCommand
        {
            get
            {
                return _SetBeltSelectedCommand ?? (_SetBeltSelectedCommand = new RelayCommand(SetBeltSelected));
            }
        }

        private RelayCommand _SetLWristSelectedCommand;
        public RelayCommand SetLWristSelectedCommand
        {
            get
            {
                return _SetLWristSelectedCommand ?? (_SetLWristSelectedCommand = new RelayCommand(SetLWristSelected));
            }
        }

        private RelayCommand _SetRWristSelectedCommand;
        public RelayCommand SetRWristSelectedCommand
        {
            get
            {
                return _SetRWristSelectedCommand ?? (_SetRWristSelectedCommand = new RelayCommand(SetRWristSelected));
            }
        }

        private RelayCommand _SetLRingSelectedCommand;
        public RelayCommand SetLRingSelectedCommand
        {
            get
            {
                return _SetLRingSelectedCommand ?? (_SetLRingSelectedCommand = new RelayCommand(SetLRingSelected));
            }
        }

        private RelayCommand _SetRRingSelectedCommand;
        public RelayCommand SetRRingSelectedCommand
        {
            get
            {
                return _SetRRingSelectedCommand ?? (_SetRRingSelectedCommand = new RelayCommand(SetRRingSelected));
            }
        }


        private RelayCommand _SetMainHandSelectedCommand;
        public RelayCommand SetMainHandSelectedCommand
        {
            get
            {
                return _SetMainHandSelectedCommand ?? (_SetMainHandSelectedCommand = new RelayCommand(SetMainHandSelected));
            }
        }

        private RelayCommand _SetOffHandSelectedCommand;
        public RelayCommand SetOffHandSelectedCommand
        {
            get
            {
                return _SetOffHandSelectedCommand ?? (_SetOffHandSelectedCommand = new RelayCommand(SetOffHandSelected));
            }
        }

        private RelayCommand _SetTwoHandsSelectedCommand;
        public RelayCommand SetTwoHandsSelectedCommand
        {
            get
            {
                return _SetTwoHandsSelectedCommand ?? (_SetTwoHandsSelectedCommand = new RelayCommand(SetTwoHandsSelected));
            }
        }

        private RelayCommand _SetRangeSelectedCommand;
        public RelayCommand SetRangeSelectedCommand
        {
            get
            {
                return _SetRangeSelectedCommand ?? (_SetRangeSelectedCommand = new RelayCommand(SetRangeSelected));
            }
        }





        private RelayCommand _LoadItemCommand;
        public RelayCommand LoadItemCommand
        {
            get
            {
                return _LoadItemCommand ?? (_LoadItemCommand = new RelayCommand(LoadItem));
            }
        }

        private RelayCommand _SaveRealmItemCommand;
        public RelayCommand SaveRealmItemCommand
        {
            get
            {
                return _SaveRealmItemCommand ?? (_SaveRealmItemCommand = new RelayCommand(SaveRealmItem));
            }
        }

        private RelayCommand _SaveAllItemCommand;
        public RelayCommand SaveAllItemCommand
        {
            get
            {
                return _SaveAllItemCommand ?? (_SaveAllItemCommand = new RelayCommand(SaveAllItem));
            }
        }

        private RelayCommand _ClearItemCommand;
        public RelayCommand ClearItemCommand
        {
            get
            {
                return _ClearItemCommand ?? (_ClearItemCommand = new RelayCommand(ClearItem));
            }
        }


        private RelayCommand _ClearCraftCommand;
        public RelayCommand ClearCraftCommand
        {
            get
            {
                return _ClearCraftCommand ?? (_ClearCraftCommand = new RelayCommand(ClearCraft));
            }
        }



        private RelayCommand _SetMacrosCommand;
        public RelayCommand SetMacrosCommand
        {
            get
            {
                return _SetMacrosCommand ?? (_SetMacrosCommand = new RelayCommand(SetMacros));
            }
        }



        private RelayCommand _ConfigureTemplateCommand;
        public RelayCommand ConfigureTemplateCommand
        {
            get
            {
                return _ConfigureTemplateCommand ?? (_ConfigureTemplateCommand = new RelayCommand(ConfigureTemplate));
            }
        }


        private RelayCommand _EditLstStatsCommand;
        public RelayCommand EditLstStatsCommand
        {
            get
            {
                return _EditLstStatsCommand ?? (_EditLstStatsCommand = new RelayCommand(EditLstStats));
            }
        }

        private RelayCommand _EditLstSkillsCommand;
        public RelayCommand EditLstSkillsCommand
        {
            get
            {
                return _EditLstSkillsCommand ?? (_EditLstSkillsCommand = new RelayCommand(EditLstSkills));
            }
        }


        private RelayCommand _CloseEditLstCommand;
        public RelayCommand CloseEditLstCommand
        {
            get
            {
                return _CloseEditLstCommand ?? (_CloseEditLstCommand = new RelayCommand(CloseEditLst));
            }
        }

        #endregion


        #region Properties


        private string _Name;
        [Required(ErrorMessage = "Name template is required")]
        [MaxLength(ErrorMessage = "Name must not exceed 100 letters")]
        public string Name
        {
            get { return _Name; }
            set { if (_Name != value) { _Name = value; RaisePropertyChanged("Name"); } }
        }


        private string _Realm;
        [Required(ErrorMessage = "Realm is required")]
        public string Realm
        {
            get { return _Realm; }
            set { if (_Realm != value) { _Realm = value; RaisePropertyChanged("Realm"); RaisePropertyChanged("IsFifthBonusEnable"); OnRealmChanged(); } }
        }
        [XmlIgnore]
        public string FullRealm
        {
            get
            {
                var realm = DataProvider.Current.Realms.FirstOrDefault(o => o.Code == this.Realm);
                if (realm != null)
                    return realm.Full;
                return "";
            }
        }
        private List<RealmModel> _LstRealms;
        [XmlIgnore]
        public List<RealmModel> LstRealms
        {
            get { return _LstRealms; }
            set { if (_LstRealms != value) { _LstRealms = value; RaisePropertyChanged("LstRealms"); } }
        }


        private string _Class;
        [Required(ErrorMessage = "Class is required")]
        public string Class
        {
            get { return _Class; }
            set { if (_Class != value) { _Class = value; RaisePropertyChanged("Class"); RaisePropertyChanged("IsFifthBonusEnable"); OnClassChanged(); } }
        }
        [XmlIgnore]
        public bool IsClassSelected
        {
            get
            {
                return !string.IsNullOrEmpty(this.Class);
            }
        }
        [XmlIgnore]
        public string FullClass
        {
            get
            {
                var classe = DataProvider.Current.Classes.FirstOrDefault(o => o.Code == this.Class);
                if (classe != null)
                    return classe.Full;
                return "";
            }
        }

        private ObservableCollection<ClassModel> _LstClasses;
        [XmlIgnore]
        public ObservableCollection<ClassModel> LstClasses
        {
            get { return _LstClasses; }
            set { if (_LstClasses != value) { _LstClasses = value; RaisePropertyChanged("LstClasses"); } }
        }

        [XmlIgnore]
        public bool IsFifthBonusEnable
        {
            get
            {
                return this.Realm != null && this.Class != null && this.SelectedItem != null;
            }
        }

        private string _Race;
        [Required(ErrorMessage = "Race is required")]
        public string Race
        {
            get { return _Race; }
            set { if (_Race != value) { _Race = value; RaisePropertyChanged("Race"); OnRaceChanged(); } }
        }
        [XmlIgnore]
        public string FullRace
        {
            get
            {
                var race = DataProvider.Current.Races.FirstOrDefault(o => o.Code == this.Race);
                if (race != null)
                    return race.Full;
                return "";
            }
        }


        private ObservableCollection<RaceModel> _LstRaces;
        [XmlIgnore]
        public ObservableCollection<RaceModel> LstRaces
        {
            get { return _LstRaces; }
            set { if (_LstRaces != value) { _LstRaces = value; RaisePropertyChanged("LstRaces"); } }
        }


        private int _Level;
        [Required(ErrorMessage = "Level is required")]
        [Range(1, 50, ErrorMessage = "Level should be between 1 to 50")]
        public int Level
        {
            get { return _Level; }
            set { if (_Level != value) { _Level = value; RaisePropertyChanged("Level"); OnLevelChanged(); } }
        }


        [Required(ErrorMessage = "Realm rank is required")]
        [Range(1, 50, ErrorMessage = "Realm rank should be between 1 to 14")]
        private int _RealmRank;
        public int RealmRank
        {
            get { return _RealmRank; }
            set { if (_RealmRank != value) { _RealmRank = value; RaisePropertyChanged("RealmRank"); OnRankChanged(); } }
        }


        private ItemViewModel _SelectedItem;
        [XmlIgnore]
        public ItemViewModel SelectedItem
        {
            get { return _SelectedItem; }
            set
            {
                if (_SelectedItem != value)
                {
                    if (value == null)
                        _SelectedItem.PropertyChanged -= SelectedItemChanged;
                    _SelectedItem = value;
                    RaisePropertyChanged("SelectedItem");
                    RaisePropertyChanged("HasSelectedItem");
                    RaisePropertyChanged("IsFifthBonusEnable");
                    if (_SelectedItem != null)
                    {
                        _SelectedItem.PropertyChanged += SelectedItemChanged;
                        _SelectedItem.UpdateSkillBonus();
                    }
                }
            }
        }
        [XmlIgnore]
        public bool HasSelectedItem
        {
            get { return _SelectedItem != null; }
        }
        private string _SelectedSlot;
        [XmlIgnore]
        public string SelectedSlot
        {
            get { return _SelectedSlot; }
            set { if (_SelectedSlot != value) { _SelectedSlot = value; RaisePropertyChanged("SelectedSlot"); } OnSelectedSlotChanged(); }
        }



        private ObservableCollection<StatModel> _LstCaracs;
        [XmlIgnore]
        public ObservableCollection<StatModel> LstCaracs
        {
            get { return _LstCaracs; }
            set { if (_LstCaracs != value) { _LstCaracs = value; RaisePropertyChanged("LstCaracs"); } }
        }

        private ObservableCollection<StatModel> _LstResists;
        [XmlIgnore]
        public ObservableCollection<StatModel> LstResists
        {
            get { return _LstResists; }
            set { if (_LstResists != value) { _LstResists = value; RaisePropertyChanged("LstResists"); } }
        }

        private ObservableCollection<StatModel> _LstSkills;
        [XmlIgnore]
        public ObservableCollection<StatModel> LstSkills
        {
            get { return _LstSkills; }
            set { if (_LstSkills != value) { _LstSkills = value; RaisePropertyChanged("LstSkills"); } }
        }

        private ObservableCollection<StatModel> _LstBonuses;
        [XmlIgnore]
        public ObservableCollection<StatModel> LstBonuses
        {
            get { return _LstBonuses; }
            set { if (_LstBonuses != value) { _LstBonuses = value; RaisePropertyChanged("LstBonuses"); } }
        }

        private ObservableCollection<StatModel> _LstEditStats;
        [XmlIgnore]
        public ObservableCollection<StatModel> LstEditStats
        {
            get { return _LstEditStats; }
            set { if (_LstEditStats != value) { _LstEditStats = value; RaisePropertyChanged("LstEditStats"); } }
        }

        private bool _IncludeRaciales;
        public bool IncludeRaciales
        {
            get { return _IncludeRaciales; }
            set { if (_IncludeRaciales != value) { _IncludeRaciales = value; RaisePropertyChanged("IncludeRaciales"); } OnIncludeRacialesChanged(); }
        }


        [XmlIgnore]
        public List<UseItemModel> LstUses
        {
            get
            {
                return DataProvider.Current.UseItems;
            }
        }


        [XmlIgnore]
        public List<ItemViewModel> LstItems
        {
            get { return this.GetAllItems(); }
        }
        public List<ItemViewModel> LstEquipedItems
        {
            get { return this.GetItems(); }
        }
        [XmlIgnore]
        public List<ItemViewModel> LstItemsCraft
        {
            get { return this.LstItems.Where(o => o.IsNotLoot).ToList(); }
        }
        [XmlIgnore]
        public List<MaterialModel> LstGemsCraft
        {
            get
            {
                return this.LstItemsCraft.SelectMany(o => o.LstBonusesCraft.SelectMany(b => b.LstMaterials.Where(m => m.Type == 1))).GroupBy(o => o.Material).Select(o => new MaterialModel() { Material = o.Key, Quantity = o.Sum(m => m.Quantity), Type = 1 }).OrderBy(o => o.Material.Full).ToList();
            }
        }
        [XmlIgnore]
        public List<MaterialModel> LstDustsCraft
        {
            get
            {
                return this.LstItemsCraft.SelectMany(o => o.LstBonusesCraft.SelectMany(b => b.LstMaterials.Where(m => m.Type == 2))).GroupBy(o => o.Material).Select(o => new MaterialModel() { Material = o.Key, Quantity = o.Sum(m => m.Quantity), Type = 2 }).OrderBy(o => o.Material.Full).ToList();
            }
        }
        [XmlIgnore]
        public List<MaterialModel> LstAgentsCraft
        {
            get
            {
                return this.LstItemsCraft.SelectMany(o => o.LstBonusesCraft.SelectMany(b => b.LstMaterials.Where(m => m.Type == 3))).GroupBy(o => o.Material).Select(o => new MaterialModel() { Material = o.Key, Quantity = o.Sum(m => m.Quantity), Type = 3 }).OrderBy(o => o.Material.Full).ToList();
            }
        }
        [XmlIgnore]
        public List<ItemViewModel> LstUsesItems
        {
            get { return this.LstItems.Where(o => o.LstUses.Count > 0).OrderBy(o => o.Name).ToList(); }
        }
        [XmlIgnore]
        public List<ItemViewModel> LstreversesItems
        {
            get { return this.LstItems.Where(o => o.LstReverses.Count > 0).OrderBy(o => o.Name).ToList(); }
        }
        [XmlIgnore]
        public List<ItemViewModel> LstProcsItems
        {
            get { return this.LstItems.Where(o => o.LstProcs.Count > 0).OrderBy(o => o.Name).ToList(); }
        }
        [XmlIgnore]
        public List<ItemViewModel> LstPassivesItems
        {
            get { return this.LstItems.Where(o => o.LstPassives.Count > 0).OrderBy(o => o.Name).ToList(); }
        }


        private ItemViewModel _Torso;
        public ItemViewModel Torso
        {
            get { return _Torso; }
            set { if (_Torso != value) { _Torso = value; RaisePropertyChanged("Torso"); OnItemChanged(); } }
        }
        private ItemViewModel _Head;
        public ItemViewModel Head
        {
            get { return _Head; }
            set { if (_Head != value) { _Head = value; RaisePropertyChanged("Head"); OnItemChanged(); } }
        }
        private ItemViewModel _Arms;
        public ItemViewModel Arms
        {
            get { return _Arms; }
            set { if (_Arms != value) { _Arms = value; RaisePropertyChanged("Arms"); OnItemChanged(); } }
        }
        private ItemViewModel _Legs;
        public ItemViewModel Legs
        {
            get { return _Legs; }
            set { if (_Legs != value) { _Legs = value; RaisePropertyChanged("Legs"); OnItemChanged(); } }
        }
        private ItemViewModel _Hands;
        public ItemViewModel Hands
        {
            get { return _Hands; }
            set { if (_Hands != value) { _Hands = value; RaisePropertyChanged("Hands"); OnItemChanged(); } }
        }
        private ItemViewModel _Feet;
        public ItemViewModel Feet
        {
            get { return _Feet; }
            set { if (_Feet != value) { _Feet = value; RaisePropertyChanged("Feet"); OnItemChanged(); } }
        }


        private ItemViewModel _Myth;
        public ItemViewModel Myth
        {
            get { return _Myth; }
            set { if (_Myth != value) { _Myth = value; RaisePropertyChanged("Myth"); OnItemChanged(); } }
        }
        private ItemViewModel _Necklace;
        public ItemViewModel Necklace
        {
            get { return _Necklace; }
            set { if (_Necklace != value) { _Necklace = value; RaisePropertyChanged("Necklace"); OnItemChanged(); } }
        }
        private ItemViewModel _Cloak;
        public ItemViewModel Cloak
        {
            get { return _Cloak; }
            set { if (_Cloak != value) { _Cloak = value; RaisePropertyChanged("Cloak"); OnItemChanged(); } }
        }
        private ItemViewModel _Jewel;
        public ItemViewModel Jewel
        {
            get { return _Jewel; }
            set { if (_Jewel != value) { _Jewel = value; RaisePropertyChanged("Jewel"); OnItemChanged(); } }
        }
        private ItemViewModel _Belt;
        public ItemViewModel Belt
        {
            get { return _Belt; }
            set { if (_Belt != value) { _Belt = value; RaisePropertyChanged("Belt"); OnItemChanged(); } }
        }
        private ItemViewModel _LWrist;
        public ItemViewModel LWrist
        {
            get { return _LWrist; }
            set { if (_LWrist != value) { _LWrist = value; RaisePropertyChanged("LWrist"); OnItemChanged(); } }
        }
        private ItemViewModel _RWrist;
        public ItemViewModel RWrist
        {
            get { return _RWrist; }
            set { if (_RWrist != value) { _RWrist = value; RaisePropertyChanged("RWrist"); OnItemChanged(); } }
        }
        private ItemViewModel _LRing;
        public ItemViewModel LRing
        {
            get { return _LRing; }
            set { if (_LRing != value) { _LRing = value; RaisePropertyChanged("LRing"); OnItemChanged(); } }
        }
        private ItemViewModel _RRing;
        public ItemViewModel RRing
        {
            get { return _RRing; }
            set { if (_RRing != value) { _RRing = value; RaisePropertyChanged("RRing"); OnItemChanged(); } }
        }


        private ItemViewModel _MainHand;
        public ItemViewModel MainHand
        {
            get { return _MainHand; }
            set { if (_MainHand != value) { _MainHand = value; RaisePropertyChanged("MainHand"); OnItemChanged(); } }
        }
        private ItemViewModel _OffHand;
        public ItemViewModel OffHand
        {
            get { return _OffHand; }
            set { if (_OffHand != value) { _OffHand = value; RaisePropertyChanged("OffHand"); OnItemChanged(); } }
        }
        private ItemViewModel _TwoHands;
        public ItemViewModel TwoHands
        {
            get { return _TwoHands; }
            set { if (_TwoHands != value) { _TwoHands = value; RaisePropertyChanged("TwoHands"); OnItemChanged(); } }
        }
        private ItemViewModel _Range;
        public ItemViewModel Range
        {
            get { return _Range; }
            set { if (_Range != value) { _Range = value; RaisePropertyChanged("Range"); OnItemChanged(); } }
        }



        #endregion


        #region Methods

        private void ConfigureTemplate()
        {
            var mainVM = ServiceLocator.Current.GetInstance<MainViewModel>();
            mainVM.ConfigureTemplate(this);
            RequestClose(this, new EventArgs());
        }
        private void EditLstStats()
        {
            this.LstEditStats = this.LstCaracs;
            var editWindow = new I_EditStats();
            editWindow.Header = "Stats List";
            editWindow.DataContext = this;
            editWindow.Show();
        }
        private void EditLstSkills()
        {
            this.LstEditStats = this.LstSkills;
            var editWindow = new I_EditStats();
            editWindow.Header = "Skills List";
            editWindow.DataContext = this;
            editWindow.Show();
        }
        private void CloseEditLst()
        {
            RequestClose(this, new EventArgs());
        }


        private void LoadItem()
        {
            if (this.SelectedItem != null && !string.IsNullOrEmpty(this.Realm) && !string.IsNullOrEmpty(this.Class))
            {
                var chooseWindow = new I_ChooseItem();
                chooseWindow.ShowDialog();
            }
            else
            {
                var param = Settings.Current.GetDialogParameters();
                param.Content = "You must select a realm, a class and a slot before loading an item.";
                RadWindow.Alert(param);
            }
        }
        private void ClearItem()
        {
            if (this.SelectedItem != null)
            {
                switch (this.SelectedSlot)
                {
                    case "TOR":
                        this.Torso = new ItemViewModel("TOR", false, true);
                        this.SetTorsoSelected();
                        break;
                    case "HEA":
                        this.Head = new ItemViewModel("HEA", false, true);
                        this.SetHeadSelected();
                        break;
                    case "HAN":
                        this.Hands = new ItemViewModel("HAN", false, true);
                        this.SetHandsSelected();
                        break;
                    case "LEG":
                        this.Legs = new ItemViewModel("LEG", false, true);
                        this.SetLegsSelected();
                        break;
                    case "ARM":
                        this.Arms = new ItemViewModel("ARM", false, true);
                        this.SetArmsSelected();
                        break;
                    case "FEE":
                        this.Feet = new ItemViewModel("FEE", false, true);
                        this.SetFeetSelected();
                        break;
                    case "NEC":
                        this.Necklace = new ItemViewModel("NEC", true, true);
                        this.SetNeckSelected();
                        break;
                    case "MYT":
                        this.Myth = new ItemViewModel("MYT", true, true);
                        this.SetMythSelected();
                        break;
                    case "CLO":
                        this.Cloak = new ItemViewModel("CLO", true, true);
                        this.SetCloakSelected();
                        break;
                    case "JEW":
                        this.Jewel = new ItemViewModel("JEW", true, true);
                        this.SetJewelSelected();
                        break;
                    case "BEL":
                        this.Belt = new ItemViewModel("BEL", true, true);
                        this.SetBeltSelected();
                        break;
                    case "LWR":
                        this.LWrist = new ItemViewModel("LWR", true, true);
                        this.SetLWristSelected();
                        break;
                    case "RWR":
                        this.RWrist = new ItemViewModel("RWR", true, true);
                        this.SetRWristSelected();
                        break;
                    case "RRI":
                        this.RRing = new ItemViewModel("RRI", true, true);
                        this.SetRRingSelected();
                        break;
                    case "LRI":
                        this.LRing = new ItemViewModel("LRI", true, true);
                        this.SetLRingSelected();
                        break;
                    case "MH":
                        this.MainHand = new ItemViewModel("MH", true, true);
                        this.SetMainHandSelected();
                        break;
                    case "OH":
                        this.OffHand = new ItemViewModel("OH", true, true);
                        this.SetOffHandSelected();
                        break;
                    case "TWO":
                        this.TwoHands = new ItemViewModel("TWO", true, false);
                        this.SetTwoHandsSelected();
                        break;
                    case "RAN":
                        this.Range = new ItemViewModel("RAN", true, false);
                        this.SetRangeSelected();
                        break;

                }
            }
        }
        private void SaveRealmItem()
        {
            if (this.SelectedItem != null)
            {
                var param = Settings.Current.GetDialogParameters();
                var item = this.SelectedItem;

                SlotModel slot = DataProvider.Current.Jeweleries.FirstOrDefault(o => o.Code == this.SelectedSlot);
                if (slot == null)
                    slot = DataProvider.Current.Armors.FirstOrDefault(o => o.Code == this.SelectedSlot);
                if (slot == null)
                    slot = DataProvider.Current.Weapons.FirstOrDefault(o => o.Code == this.SelectedSlot);

                var realm = DataProvider.Current.Realms.FirstOrDefault(o => o.Code == this.Realm);

                if (realm == null)
                {
                    param.Content = "You must select a realm before saving an item.";
                    RadWindow.Alert(param);
                }
                else if (string.IsNullOrEmpty(item.Name))
                {
                    param.Content = "You must specify a name for your item.";
                    RadWindow.Alert(param);
                }
                else
                {
                    if (File.Exists(item.PathName))
                        File.Delete(item.PathName);

                    var path = Settings.Current.RepItems + "\\" + realm.Full + "\\" + slot.RepName + "\\" + item.Name + ".xml";

                    if (slot.Code == "MYT")
                    {
                        if (!Directory.Exists(Settings.Current.RepItems + "\\All\\" + slot.RepName))
                            Directory.CreateDirectory(Settings.Current.RepItems + "\\All\\" + slot.RepName);
                        path = Settings.Current.RepItems + "\\All\\" + slot.RepName + "\\" + item.Name + ".xml";
                    }


                    this.SaveItemAsFile(item, path);
                }
            }
        }
        private void SaveAllItem()
        {
            if (this.SelectedItem != null)
            {
                var param = Settings.Current.GetDialogParameters();
                var item = this.SelectedItem;

                SlotModel slot = DataProvider.Current.Jeweleries.FirstOrDefault(o => o.Code == this.SelectedSlot);
                if (slot == null)
                    slot = DataProvider.Current.Armors.FirstOrDefault(o => o.Code == this.SelectedSlot);
                if (slot == null)
                    slot = DataProvider.Current.Weapons.FirstOrDefault(o => o.Code == this.SelectedSlot);

                var realm = DataProvider.Current.Realms.FirstOrDefault(o => o.Code == this.Realm);

                if (realm == null)
                {
                    param.Content = "You must select a realm before saving an item.";
                    RadWindow.Alert(param);
                }
                else if (string.IsNullOrEmpty(item.Name))
                {
                    param.Content = "You must specify a name for your item.";
                    RadWindow.Alert(param);
                }
                else
                {
                    if (File.Exists(item.PathName))
                        File.Delete(item.PathName);

                    if (!Directory.Exists(Settings.Current.RepItems + "\\All\\" + slot.RepName))
                        Directory.CreateDirectory(Settings.Current.RepItems + "\\All\\" + slot.RepName);
                    var path = Settings.Current.RepItems + "\\All\\" + slot.RepName + "\\" + item.Name + ".xml";

                    this.SaveItemAsFile(item, path);
                }
            }
        }
        private void SaveItemAsFile(ItemViewModel item, string path)
        {
            try
            {
                item.PathName = path;

                System.Xml.Serialization.XmlSerializer writer =
                     new System.Xml.Serialization.XmlSerializer(typeof(ItemViewModel));

                using (FileStream reader = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                    writer.Serialize(reader, item);
                }

                var param = Settings.Current.GetDialogParameters();
                param.Content = item.Name + " successfully saved !";
                RadWindow.Alert(param);

            }
            catch (Exception)
            {
                var param = Settings.Current.GetDialogParameters();
                param.Content = "An error occured while saving the item ! Please, try again later.";
                RadWindow.Alert(param);
            }
        }

        private void ClearCraft()
        {
            foreach (var item in this.LstItemsCraft)
            {
                item.ClearCraft();
            }
            this.UpdateTemplate();
        }

        private void SetMacros()
        {
            var macroWindow = new I_ConfigMacro();
            macroWindow.ShowDialog();
        }


        private List<ItemViewModel> GetItems()
        {
            var retour = new List<ItemViewModel>();
            if (this.Torso != null && this.Torso.IsEquiped) retour.Add(this.Torso);
            if (this.Arms != null && this.Arms.IsEquiped) retour.Add(this.Arms);
            if (this.Legs != null && this.Legs.IsEquiped) retour.Add(this.Legs);
            if (this.Head != null && this.Head.IsEquiped) retour.Add(this.Head);
            if (this.Hands != null && this.Hands.IsEquiped) retour.Add(this.Hands);
            if (this.Feet != null && this.Feet.IsEquiped) retour.Add(this.Feet);
            if (this.Necklace != null && this.Necklace.IsEquiped) retour.Add(this.Necklace);
            if (this.Cloak != null && this.Cloak.IsEquiped) retour.Add(this.Cloak);
            if (this.Myth != null && this.Myth.IsEquiped) retour.Add(this.Myth);
            if (this.Jewel != null && this.Jewel.IsEquiped) retour.Add(this.Jewel);
            if (this.Belt != null && this.Belt.IsEquiped) retour.Add(this.Belt);
            if (this.LWrist != null && this.LWrist.IsEquiped) retour.Add(this.LWrist);
            if (this.RWrist != null && this.RWrist.IsEquiped) retour.Add(this.RWrist);
            if (this.LRing != null && this.LRing.IsEquiped) retour.Add(this.LRing);
            if (this.RRing != null && this.RRing.IsEquiped) retour.Add(this.RRing);
            if (this.MainHand != null && this.MainHand.IsEquiped) retour.Add(this.MainHand);
            if (this.OffHand != null && this.OffHand.IsEquiped) retour.Add(this.OffHand);
            if (this.TwoHands != null && this.TwoHands.IsEquiped) retour.Add(this.TwoHands);
            if (this.Range != null && this.Range.IsEquiped) retour.Add(this.Range);

            if (Settings.Current.RespectBonusLevel && this.Level < 50)
                retour = retour.Where(o => o.BonusLevel <= this.Level).ToList();

            return retour;
        }
        private List<ItemViewModel> GetAllItems()
        {
            var retour = new List<ItemViewModel>();

            retour.Add(this.Torso);
            retour.Add(this.Arms);
            retour.Add(this.Legs);
            retour.Add(this.Head);
            retour.Add(this.Hands);
            retour.Add(this.Feet);

            retour.Add(this.MainHand);
            retour.Add(this.OffHand);
            retour.Add(this.TwoHands);
            retour.Add(this.Range);

            retour.Add(this.Necklace);
            retour.Add(this.Cloak);
            retour.Add(this.Jewel);
            retour.Add(this.Belt);
            retour.Add(this.LWrist);
            retour.Add(this.RWrist);
            retour.Add(this.LRing);
            retour.Add(this.RRing);

            retour.Add(this.Myth);
            return retour;
        }


        private bool CheckClassSelected()
        {
            var retour = true;
            if (!this.IsClassSelected)
            {
                retour = false;
                var param = Settings.Current.GetDialogParameters();
                param.Content = "You must first select a realm and a class.";
                RadWindow.Alert(param);
            }


            return retour;
        }

        private void SetTorsoSelected()
        {
            if (this.CheckClassSelected())
            {
                this.SelectedItem = this.Torso;
                this.SelectedSlot = "TOR";
            }
        }
        private void SetHeadSelected()
        {
            if (this.CheckClassSelected())
            {
                this.SelectedItem = this.Head;
                this.SelectedSlot = "HEA";
            }
        }
        private void SetArmsSelected()
        {
            if (this.CheckClassSelected())
            {
                this.SelectedItem = this.Arms;
                this.SelectedSlot = "ARM";
            }
        }
        private void SetLegsSelected()
        {
            if (this.CheckClassSelected())
            {
                this.SelectedItem = this.Legs;
                this.SelectedSlot = "LEG";
            }
        }
        private void SetFeetSelected()
        {
            if (this.CheckClassSelected())
            {
                this.SelectedItem = this.Feet;
                this.SelectedSlot = "FEE";
            }
        }
        private void SetHandsSelected()
        {
            if (this.CheckClassSelected())
            {
                this.SelectedItem = this.Hands;
                this.SelectedSlot = "HAN";
            }
        }
        private void SetNeckSelected()
        {
            if (this.CheckClassSelected())
            {
                this.SelectedItem = this.Necklace;
                this.SelectedSlot = "NEC";
            }
        }
        private void SetMythSelected()
        {
            if (this.CheckClassSelected())
            {
                this.SelectedItem = this.Myth;
                this.SelectedSlot = "MYT";
            }
        }
        private void SetCloakSelected()
        {
            if (this.CheckClassSelected())
            {
                this.SelectedItem = this.Cloak;
                this.SelectedSlot = "CLO";
            }
        }
        private void SetJewelSelected()
        {
            if (this.CheckClassSelected())
            {
                this.SelectedItem = this.Jewel;
                this.SelectedSlot = "JEW";
            }
        }
        private void SetBeltSelected()
        {
            if (this.CheckClassSelected())
            {
                this.SelectedItem = this.Belt;
                this.SelectedSlot = "BEL";
            }
        }
        private void SetLWristSelected()
        {
            if (this.CheckClassSelected())
            {
                this.SelectedItem = this.LWrist;
                this.SelectedSlot = "LWR";
            }
        }
        private void SetRWristSelected()
        {
            if (this.CheckClassSelected())
            {
                this.SelectedItem = this.RWrist;
                this.SelectedSlot = "RWR";
            }
        }
        private void SetLRingSelected()
        {
            if (this.CheckClassSelected())
            {
                this.SelectedItem = this.LRing;
                this.SelectedSlot = "LRI";
            }
        }
        private void SetRRingSelected()
        {
            if (this.CheckClassSelected())
            {
                this.SelectedItem = this.RRing;
                this.SelectedSlot = "RRI";
            }
        }
        private void SetMainHandSelected()
        {
            if (this.CheckClassSelected())
            {
                this.SelectedItem = this.MainHand;
                this.SelectedSlot = "MH";
            }
        }
        private void SetOffHandSelected()
        {
            if (this.CheckClassSelected())
            {
                this.SelectedItem = this.OffHand;
                this.SelectedSlot = "OH";
            }
        }
        private void SetTwoHandsSelected()
        {
            if (this.CheckClassSelected())
            {
                this.SelectedItem = this.TwoHands;
                this.SelectedSlot = "TWO";
            }
        }
        private void SetRangeSelected()
        {
            if (this.CheckClassSelected())
            {
                this.SelectedItem = this.Range;
                this.SelectedSlot = "RAN";
            }
        }

        private void UpdateFifthBonus()
        {
            if (this.SelectedItem != null)
            {
                this.SelectedItem.FifthBonus = new FifthBonusItemViewModel();
            }
        }

        private void UpdateTemplate()
        {
            var classe = DataProvider.Current.Classes.FirstOrDefault(o => o.Code == this.Class);
            var items = this.GetItems().ToList();
            var bonuses = items.SelectMany(o => o.GetBonuses(false)).ToList();

            this.UpdateCaracs(items);
            if (classe != null)
            {
                this.UpdateStatsBonus(classe, items);
                this.UpdateAcuityBonus(classe, bonuses);
            }
            this.UpdateResists(bonuses);
            this.UpdateDecapResist(bonuses);
            this.UpdateDecapAndResist(bonuses);
            this.UpdateSkills(bonuses);
            this.UpdateBonuses(bonuses);
            this.UpdateMythicalBonuses(bonuses);
        }

        private void UpdateCaracs(List<ItemViewModel> items)
        {
            var bonusesTrue = items.SelectMany(o => o.GetBonuses(true));
            var bonusesFalse = items.SelectMany(o => o.GetBonuses(false));


            foreach (var carac in DataProvider.Current.Caracs.Where(o => o.ShowStats))
            {
                var cCarac = this.LstCaracs.FirstOrDefault(o => o.Stat.Code == carac.Code);
                if (cCarac == null)
                {
                    cCarac = new StatModel();
                    cCarac.Stat = carac;
                    this.LstCaracs.Add(cCarac);
                }

                cCarac.Value = 0;
                cCarac.MaxValue = 0;
                cCarac.Diff = 0;
                cCarac.NormalDecap = 0;
                cCarac.MythicalDecap = 0;
                cCarac.DiffNormalDecap = 0;
                cCarac.DiffMythicalDecap = 0;

                //Hit Points
                if (carac.Code == "HP")
                {
                    cCarac.MaxValue = (int)Math.Floor((double)this.Level * 4);
                    cCarac.MaxNormalDecap = (int)Math.Floor((double)this.Level * 4);
                    cCarac.Value = bonusesTrue.Where(o => o.TypeBonus == carac.Code).Sum(o => o.Value).Value;
                }

                    //Mana
                else if (carac.Code == "MAN")
                {
                    cCarac.MaxValue = (int)Math.Floor((double)this.Level / 2);
                    cCarac.MaxNormalDecap = (int)Math.Floor((double)this.Level / 2);
                    cCarac.Value = bonusesTrue.Where(o => o.TypeBonus == carac.Code).Sum(o => o.Value).Value;
                }

                    //Fatigue
                else if (carac.Code == "FAT")
                {
                    cCarac.MaxValue = (int)Math.Floor((double)this.Level / 2);
                    cCarac.MaxNormalDecap = (int)Math.Floor((double)this.Level / 2);
                    cCarac.Value = bonusesFalse.Where(o => !string.IsNullOrEmpty(o.Bonus) && o.TypeBonus == "STA" && o.Bonus == carac.Code).Sum(o => o.Value).Value;
                }

                    //Base Caracs.
                else
                {
                    cCarac.MaxValue = (int)Math.Floor(this.Level * 1.5);
                    cCarac.MaxNormalDecap = (int)Math.Floor((double)(this.Level / 2) + 1);
                    cCarac.MaxMythicalDecap = (int)Math.Floor((double)((this.Level / 2) + 1) * 2);
                    cCarac.Value = bonusesFalse.Where(o => !string.IsNullOrEmpty(o.Bonus) && o.TypeBonus == "STA" && o.Bonus == carac.Code).Sum(o => o.Value).Value;
                }
                cCarac.Diff = cCarac.Value - cCarac.MaxValue;

            }
        }
        private void UpdateStatsBonus(ClassModel classe, List<ItemViewModel> items)
        {

            foreach (var carac in DataProvider.Current.Caracs.Where(o => o.ShowStats))
            {
                var cCarac = this.LstCaracs.FirstOrDefault(o => o.Stat.Code == carac.Code);
                if (cCarac != null && cCarac.Stat.Code != classe.AcuityStat)
                {

                    var valueStat = 0;
                    var noBonus = cCarac.Stat.Code == "HP" || cCarac.Stat.Code == "MAN";
                    if (noBonus)
                        valueStat = items.SelectMany(o => o.GetBonuses(noBonus)).Where(o => o.TypeBonus == cCarac.Stat.Code).Sum(o => o.Value).Value;
                    else
                        valueStat = items.SelectMany(o => o.GetBonuses(noBonus)).Where(o => o.TypeBonus == "STA" && o.Bonus == cCarac.Stat.Code).Sum(o => o.Value).Value;

                    var valueCap = items.SelectMany(o => o.GetBonuses(noBonus)).Where(o => (o.TypeBonus == "CAPS") && o.Bonus == cCarac.Stat.Code).Sum(o => o.Value).Value;
                    var valueMythicalCap = items.SelectMany(o => o.GetBonuses(noBonus)).Where(o => o.TypeBonus == "CAPMS" && o.Bonus == cCarac.Stat.Code).Sum(o => o.Value).Value;
                    var valueMythicalCapStat = items.SelectMany(o => o.GetBonuses(noBonus)).Where(o => o.TypeBonus == "CAPSC" && o.Bonus == cCarac.Stat.Code).Sum(o => o.Value).Value;

                    cCarac.Value = valueStat + valueMythicalCapStat;
                    cCarac.MaxValue += Math.Min(Math.Min(valueCap, cCarac.MaxNormalDecap) + Math.Min(valueMythicalCap + valueMythicalCapStat, cCarac.MaxMythicalDecap), Math.Max(cCarac.MaxMythicalDecap, cCarac.MaxNormalDecap));

                    cCarac.NormalDecap += valueCap;
                    cCarac.MythicalDecap += valueMythicalCap;
                    cCarac.MythicalDecap += valueMythicalCapStat;

                    cCarac.Diff = cCarac.Value - cCarac.MaxValue;
                    cCarac.DiffNormalDecap = cCarac.MaxNormalDecap - cCarac.NormalDecap;
                    cCarac.DiffMythicalDecap = cCarac.MaxMythicalDecap - cCarac.MythicalDecap;


                }
            }
        }
        private void UpdateAcuityBonus(ClassModel classe, List<BonusItemViewModel> bonuses)
        {

            var carac = DataProvider.Current.Caracs.FirstOrDefault(o => o.Code == classe.AcuityStat);
            if (carac != null)
            {
                var cCarac = this.LstCaracs.FirstOrDefault(o => o.Stat.Code == carac.Code);
                if (cCarac != null)
                {

                    var valueStat = bonuses.Where(o => o.TypeBonus == "STA" && (o.Bonus == "ACU" || o.Bonus == classe.AcuityStat)).Sum(o => o.Value).Value;
                    var valueCap = bonuses.Where(o => (o.TypeBonus == "CAPS") && (o.Bonus == "ACU" || o.Bonus == classe.AcuityStat)).Sum(o => o.Value).Value;
                    var valueMythicalCap = bonuses.Where(o => o.TypeBonus == "CAPMS" && (o.Bonus == "ACU" || o.Bonus == classe.AcuityStat)).Sum(o => o.Value).Value;
                    var valueMythicalCapStat = bonuses.Where(o => o.TypeBonus == "CAPSC" && (o.Bonus == "ACU" || o.Bonus == classe.AcuityStat)).Sum(o => o.Value).Value;

                    cCarac.Value = valueStat + valueMythicalCapStat;
                    cCarac.MaxValue += Math.Min(Math.Min(valueCap, cCarac.MaxNormalDecap) + Math.Min(valueMythicalCap + valueMythicalCapStat, cCarac.MaxMythicalDecap), cCarac.MaxMythicalDecap);

                    cCarac.NormalDecap += valueCap;
                    cCarac.MythicalDecap += valueMythicalCap;
                    cCarac.MythicalDecap += valueMythicalCapStat;

                    cCarac.Diff = cCarac.Value - cCarac.MaxValue;
                    cCarac.DiffNormalDecap = cCarac.MaxNormalDecap - cCarac.NormalDecap;
                    cCarac.DiffMythicalDecap = cCarac.MaxMythicalDecap - cCarac.MythicalDecap;
                }
            }


        }

        private void UpdateResists(List<BonusItemViewModel> bonuses)
        {

            //Resists
            foreach (var resist in DataProvider.Current.Resists)
            {
                var cResist = this.LstResists.FirstOrDefault(o => o.Stat.Code == resist.Code);
                if (cResist == null)
                {
                    cResist = new StatModel();
                    cResist.Stat = resist;
                    cResist.Value = 0;
                    cResist.IsImportant = true;
                    this.LstResists.Add(cResist);
                }

                cResist.Value = 0;
                cResist.MaxValue = 0;
                cResist.Diff = 0;
                cResist.NormalDecap = 0;
                cResist.MythicalDecap = 0;
                cResist.DiffNormalDecap = 0;
                cResist.DiffMythicalDecap = 0;

                int raciales = 0;
                var race = DataProvider.Current.Races.FirstOrDefault(o => o.Code == this.Race);
                if (race != null && this.IncludeRaciales)
                {
                    var restRac = race.LstResists.FirstOrDefault(o => o.Code == resist.Code);
                    if (restRac != null)
                        raciales = (int)restRac.Value;
                }
                cResist.Value = bonuses.Where(o => !string.IsNullOrEmpty(o.Bonus) && o.TypeBonus == "RES" && o.Bonus == resist.Code).Sum(o => o.Value).Value + raciales;
                cResist.MaxValue = (int)Math.Floor((double)(this.Level / 2) + 1) + raciales;
                cResist.Diff = cResist.Value - cResist.MaxValue;

            }
        }
        private void UpdateDecapResist(List<BonusItemViewModel> bonuses)
        {

            //Mythical Resists Cap
            foreach (var bonus in bonuses.Where(o => !string.IsNullOrEmpty(o.Bonus) && o.TypeBonus == "CAPR").GroupBy(o => o.Bonus))
            {
                var cResist = this.LstResists.FirstOrDefault(o => o.Stat.Code == bonus.FirstOrDefault().Bonus);
                if (cResist != null)
                {
                    cResist.MaxValue = cResist.MaxValue + bonus.Sum(o => o.Value).Value;
                    cResist.Diff = cResist.Value - cResist.MaxValue;
                }

            }
        }
        private void UpdateDecapAndResist(List<BonusItemViewModel> bonuses)
        {

            //Mythical Resist and Cap
            foreach (var bonus in bonuses.Where(o => !string.IsNullOrEmpty(o.Bonus) && o.TypeBonus == "CAPRC").GroupBy(o => o.Bonus))
            {
                var cResist = this.LstResists.FirstOrDefault(o => o.Stat.Code == bonus.FirstOrDefault().Bonus);
                if (cResist != null)
                {
                    cResist.Value = cResist.Value + bonus.Sum(o => o.Value).Value;
                    cResist.MaxValue = cResist.MaxValue + bonus.Sum(o => o.Value).Value;
                    cResist.Diff = cResist.Value - cResist.MaxValue;
                }
            }
        }

        private void UpdateSkills(List<BonusItemViewModel> bonuses)
        {

            var classe = DataProvider.Current.Classes.FirstOrDefault(o => o.Code == this.Class);
            if (classe != null)
            {
                if (this.LstSkills != null)
                    this.LstSkills.Clear();

                foreach (var skill in DataProvider.Current.Skills.Where(o => o.LstClasses.Contains(this.Class)))
                {
                    var rankBonus = Math.Max(this.RealmRank - 1, 0);

                    var cSkill = this.LstSkills.FirstOrDefault(o => o.Stat.Code == skill.Code);
                    if (cSkill == null)
                    {
                        cSkill = new StatModel();
                        cSkill.Stat = skill;
                        cSkill.Value = rankBonus;
                        this.LstSkills.Add(cSkill);
                    }
                    cSkill.Value = bonuses.Where(o => !string.IsNullOrEmpty(o.Bonus) && o.TypeBonus == "SKI" && o.Bonus == skill.Code).Sum(o => o.Value).Value + rankBonus;
                    cSkill.MaxValue = (int)Math.Floor((double)this.Level / 5) + 1 + rankBonus;
                    cSkill.Diff = cSkill.Value - cSkill.MaxValue;

                }

                //All Melee/Magic/Range/Dual Skills
                foreach (var bonus in bonuses.Where(o => !string.IsNullOrEmpty(o.Bonus) && o.TypeBonus == "SKI" && (o.Bonus == "MELEE" || o.Bonus == "MAGIC" || o.Bonus == "RANGE" || o.Bonus == "DUAL")).GroupBy(o => o.Bonus))
                {
                    foreach (var cSkill in this.LstSkills)
                    {
                        var skill = DataProvider.Current.Skills.FirstOrDefault(o => o.Code == cSkill.Stat.Code);
                        if (skill != null && ((skill.Type == "MAGIC" && bonus.First().Bonus == "MAGIC") || (skill.Type == "MELEE" && bonus.First().Bonus == "MELEE") || (skill.Type == "RANGE" && bonus.First().Bonus == "RANGE") || (skill.Type == "DUAL" && bonus.First().Bonus == "DUAL")))
                        {
                            cSkill.Value += bonus.Sum(o => o.Value).Value;
                            cSkill.Diff = cSkill.Value - cSkill.MaxValue;
                        }
                    }
                }
            }
            else if (this.LstSkills != null)
                this.LstSkills.Clear();

        }

        private void UpdateBonuses(List<BonusItemViewModel> bonuses)
        {

            this.LstBonuses.Clear();
            var lstTemp = new List<StatModel>();
            foreach (var bonus in bonuses.Where(o => !string.IsNullOrEmpty(o.Bonus) && o.TypeBonus == "OTH").GroupBy(o => o.Bonus).OrderBy(o => o.Key))
            {
                var tBonus = DataProvider.Current.ToaBonuses.FirstOrDefault(o => o.Code == bonus.FirstOrDefault().Bonus);
                if (tBonus != null)
                {
                    var toaBonus = new StatModel();
                    toaBonus.Stat = tBonus;
                    toaBonus.Value = bonus.Sum(o => o.Value).Value;
                    toaBonus.MaxValue = (int)Math.Floor((double)this.Level / tBonus.LevelDividedBy);
                    toaBonus.Diff = toaBonus.Value - toaBonus.MaxValue;
                    lstTemp.Add(toaBonus);
                }
            }

            //Power Decap
            var decapPower = bonuses.Where(o => o.TypeBonus == "CAPS" && o.Bonus == "MAN").Sum(o => o.Value).Value;
            if (decapPower > 0)
            {
                var decapBonus = lstTemp.FirstOrDefault(o => o.Stat.Code == "POW");
                if (decapBonus != null)
                {
                    decapBonus.MaxValue += decapPower;
                    decapBonus.Diff = decapBonus.Value - decapBonus.MaxValue;
                }
            }


            this.LstBonuses = new ObservableCollection<StatModel>(lstTemp.OrderBy(o => o.Stat.Full).ToList());
        }
        private void UpdateMythicalBonuses(List<BonusItemViewModel> bonuses)
        {
            var lstTemp = new List<StatModel>();
            foreach (var bonus in bonuses.Where(o => !string.IsNullOrEmpty(o.Bonus) && o.TypeBonus == "MYTH").GroupBy(o => o.Bonus).OrderBy(o => o.Key))
            {

                var tBonus = DataProvider.Current.MythicalBonuses.FirstOrDefault(o => o.Code == bonus.FirstOrDefault().Bonus);
                if (tBonus != null)
                {
                    var mythBonus = new StatModel();
                    mythBonus.Stat = tBonus;
                    mythBonus.Value = bonus.Sum(o => o.Value).Value;
                    mythBonus.MaxValue = (int)Math.Floor((double)this.Level / tBonus.LevelDividedBy);
                    mythBonus.Diff = mythBonus.Value - mythBonus.MaxValue;
                    lstTemp.Add(mythBonus);
                }
            }
            foreach (var bonus in lstTemp.OrderBy(o => o.Stat.Full))
            {
                this.LstBonuses.Add(bonus);
            }
        }



        public void SetItem(ItemViewModel item)
        {
            if (!string.IsNullOrEmpty(SelectedSlot))
            {
                switch (this.SelectedSlot)
                {
                    case "TOR":
                        this.Torso = item;
                        this.SetTorsoSelected();
                        break;
                    case "HEA":
                        this.Head = item;
                        this.SetHeadSelected();
                        break;
                    case "HAN":
                        this.Hands = item;
                        this.SetHandsSelected();
                        break;
                    case "LEG":
                        this.Legs = item;
                        this.SetLegsSelected();
                        break;
                    case "ARM":
                        this.Arms = item;
                        this.SetArmsSelected();
                        break;
                    case "FEE":
                        this.Feet = item;
                        this.SetFeetSelected();
                        break;
                    case "NEC":
                        this.Necklace = item;
                        this.SetNeckSelected();
                        break;
                    case "MYT":
                        this.Myth = item;
                        this.SetMythSelected();
                        break;
                    case "CLO":
                        this.Cloak = item;
                        this.SetCloakSelected();
                        break;
                    case "JEW":
                        this.Jewel = item;
                        this.SetJewelSelected();
                        break;
                    case "BEL":
                        this.Belt = item;
                        this.SetBeltSelected();
                        break;
                    case "LWR":
                        this.LWrist = item;
                        this.SetLWristSelected();
                        break;
                    case "RWR":
                        this.RWrist = item;
                        this.SetRWristSelected();
                        break;
                    case "RRI":
                        this.RRing = item;
                        this.SetRRingSelected();
                        break;
                    case "LRI":
                        this.LRing = item;
                        this.SetLRingSelected();
                        break;
                    case "MH":
                        this.MainHand = item;
                        this.OffHand.IsEquiped = true;
                        this.TwoHands.IsEquiped = false;
                        this.Range.IsEquiped = false;
                        this.SetMainHandSelected();
                        break;
                    case "OH":
                        this.OffHand = item;
                        this.MainHand.IsEquiped = true;
                        this.TwoHands.IsEquiped = false;
                        this.Range.IsEquiped = false;
                        this.SetOffHandSelected();
                        break;
                    case "TWO":
                        this.TwoHands = item;
                        this.MainHand.IsEquiped = false;
                        this.OffHand.IsEquiped = false;
                        this.Range.IsEquiped = false;
                        this.SetTwoHandsSelected();
                        break;
                    case "RAN":
                        this.Range = item;
                        this.MainHand.IsEquiped = false;
                        this.OffHand.IsEquiped = false;
                        this.TwoHands.IsEquiped = false;
                        this.SetRangeSelected();
                        break;
                }
            }
        }

        private void SetWeaponsEquiped(ItemViewModel item)
        {
            if (!string.IsNullOrEmpty(this.SelectedSlot) && item != null && item.IsEquiped)
            {
                switch (this.SelectedSlot)
                {
                    case "MH":
                        this.OffHand.IsEquiped = true;
                        this.TwoHands.IsEquiped = false;
                        this.Range.IsEquiped = false;
                        break;
                    case "OH":
                        this.MainHand.IsEquiped = true;
                        this.TwoHands.IsEquiped = false;
                        this.Range.IsEquiped = false;
                        break;
                    case "TWO":
                        this.MainHand.IsEquiped = false;
                        this.OffHand.IsEquiped = false;
                        this.Range.IsEquiped = false;
                        break;
                    case "RAN":
                        this.MainHand.IsEquiped = false;
                        this.OffHand.IsEquiped = false;
                        this.TwoHands.IsEquiped = false;
                        break;
                }
            }
        }

        public string GetTxtReport(bool full, bool gems, bool materials, bool forum)
        {
            var builder = new StringBuilder();
            if (forum)
                builder.AppendLine("[CODE]");
            builder.AppendLine();
            builder.AppendLine();
            builder.AppendLine("Template : " + this.Name);
            builder.AppendLine();
            builder.AppendLine();
            builder.AppendLine("Realm : " + this.FullRealm);
            builder.AppendLine("Class : " + this.FullClass);
            builder.AppendLine("Race : " + this.FullRace);
            builder.AppendLine("Level : " + this.Level.ToString());
            builder.AppendLine("Realm Reank : " + this.RealmRank.ToString());

            builder.AppendLine();
            builder.AppendLine();

            if (!gems && !materials)
            {

                builder.AppendLine("Stats : ");
                foreach (var stats in this.LstCaracs)
                {
                    builder.AppendLine("\t" + stats.Stat.Full + " : " + stats.Value + "/" + stats.MaxValue);
                }
                builder.AppendLine();
                builder.AppendLine();

                builder.AppendLine("Resist : ");
                foreach (var stats in this.LstResists)
                {
                    builder.AppendLine("\t" + stats.Stat.Full + " : " + stats.Value + "/" + stats.MaxValue);
                }
                builder.AppendLine();
                builder.AppendLine();

                builder.AppendLine("Bonuses : ");
                foreach (var stats in this.LstBonuses)
                {
                    builder.AppendLine("\t" + stats.Stat.Full + " : " + stats.Value + "/" + stats.MaxValue);
                }
                builder.AppendLine();
                builder.AppendLine();

                builder.AppendLine("Skills : ");
                foreach (var stats in this.LstSkills)
                {
                    builder.AppendLine("\t" + stats.Stat.Full + " : " + stats.Value + "/" + stats.MaxValue);
                }
                builder.AppendLine();
                builder.AppendLine();

            }

            if (full)
            {
                builder.AppendLine("Uses : ");
                foreach (var item in this.LstUsesItems)
                {
                    builder.AppendLine(item.Name + " : ");
                    foreach (var use in item.LstUses)
                    {
                        builder.AppendLine("\t" + use.Full + use.ReportDescription);
                    }
                    builder.AppendLine();
                }
                builder.AppendLine();

                builder.AppendLine("Procs : ");
                foreach (var item in this.LstProcsItems)
                {
                    builder.AppendLine(item.Name + " : ");
                    foreach (var use in item.LstProcs)
                    {
                        builder.AppendLine("\t" + use.Full + use.ReportDescription);
                    }
                    builder.AppendLine();
                }
                builder.AppendLine();

                builder.AppendLine("Passives : ");
                foreach (var item in this.LstPassivesItems)
                {
                    builder.AppendLine(item.Name + " : ");
                    foreach (var use in item.LstPassives)
                    {
                        builder.AppendLine("\t" + use.Full + use.ReportDescription);
                    }
                    builder.AppendLine();
                }
                builder.AppendLine();

                builder.AppendLine("Reverses : ");
                foreach (var item in this.LstreversesItems)
                {
                    builder.AppendLine(item.Name + " : ");
                    foreach (var use in item.LstReverses)
                    {
                        builder.AppendLine("\t" + use.Full + use.ReportDescription);
                    }
                    builder.AppendLine();
                }
                builder.AppendLine();


                builder.AppendLine("Items : ");
                foreach (var item in this.LstItems)
                {
                    builder.AppendLine(item.SlotFull + " : " + item.Name);
                    builder.AppendLine("Level : " + item.Level + " Quality : " + item.Quality + " Bonus Level : " + item.BonusLevel + ' ' + item.ChargeReport);
                    foreach (var bonus in item.LstBonuses)
                    {
                        builder.AppendLine("\t" + bonus.FullDescription + bonus.GemNameReport);
                    }
                    builder.AppendLine();
                }
            }

            if (gems)
            {
                builder.AppendLine("Gems List : ");
                foreach (var item in this.LstItemsCraft)
                {
                    builder.AppendLine(item.SlotFull + " : " + item.Name);
                    builder.AppendLine(item.ChargeReport);
                    foreach (var bonus in item.LstBonuses)
                    {
                        builder.AppendLine("\t" + bonus.FullDescription + bonus.GemNameReport);
                    }
                    builder.AppendLine();
                }
            }

            if (materials)
            {
                builder.AppendLine("Materials List : ");
                foreach (var item in this.LstItemsCraft)
                {
                    builder.AppendLine(item.SlotFull + " : " + item.Name);
                    builder.AppendLine(item.ChargeReport);
                    foreach (var bonus in item.LstBonuses)
                    {
                        builder.AppendLine("\t" + bonus.GemMaterials);
                    }
                    builder.AppendLine();
                }

                builder.AppendLine("Summary : ");
                builder.AppendLine("\t Gems : ");
                foreach (var gem in this.LstGemsCraft)
                {
                    builder.AppendLine("\t\t" + gem.Quantity + " " + gem.Material.Full);
                }
                builder.AppendLine();

                builder.AppendLine("\t Dusts : ");
                foreach (var dust in this.LstDustsCraft)
                {
                    builder.AppendLine("\t\t" + dust.Quantity + " " + dust.Material.Full);
                }
                builder.AppendLine();


                builder.AppendLine("\t Agents : ");
                foreach (var agent in this.LstAgentsCraft)
                {
                    builder.AppendLine("\t\t" + agent.Quantity + " " + agent.Material.Full);
                }
                builder.AppendLine();

            }

            if (forum)
                builder.AppendLine("[/CODE]");

            return builder.ToString();
        }

        public void ShowDetailBonus(string typeBonus, string bonus)
        {
            var detailWindow = new I_DetailBonus(this, typeBonus, bonus);
            detailWindow.ShowDialog();
        }

        #endregion


        #region Events

        public event EventHandler RequestClose;

        private void OnRealmChanged()
        {
            var realm = DataProvider.Current.Realms.FirstOrDefault(o => o.Code == this.Realm);
            if (realm != null)
            {
                var classes = DataProvider.Current.Classes.Where(o => o.LstRealms.Contains(this.Realm));
                this.LstClasses = new ObservableCollection<ClassModel>(classes);
                this.Class = "";
                this.Race = "";
            }
        }

        private void OnClassChanged()
        {
            var classe = DataProvider.Current.Classes.FirstOrDefault(o => o.Code == this.Class);
            var realm = DataProvider.Current.Realms.FirstOrDefault(o => o.Code == this.Realm);
            if (classe != null && realm != null)
            {
                var races = DataProvider.Current.Races.Where(o => classe.LstRaces.Contains(o.Code) && o.Realm == this.Realm);
                this.LstRaces = new ObservableCollection<RaceModel>(races);
            }
            this.UpdateTemplate();
            this.UpdateFifthBonus();
        }

        private void OnRaceChanged()
        {
            var bonuses = this.GetItems().SelectMany(o => o.GetBonuses(false)).ToList();
            this.UpdateResists(bonuses);
        }

        private void OnLevelChanged()
        {
            this.UpdateTemplate();
            if (this.SelectedItem != null)
                this.SelectedItem.RaiseChargeEvents();
        }

        private void OnRankChanged()
        {
            var bonuses = this.GetItems().SelectMany(o => o.GetBonuses(false)).ToList();
            this.UpdateSkills(bonuses);
        }

        private void OnSelectedSlotChanged()
        {
            this.UpdateFifthBonus();
        }

        private void OnItemChanged()
        {
            this.UpdateTemplate();
        }

        private void SelectedItemChanged(object sender, PropertyChangedEventArgs e)
        {
            var item = sender as ItemViewModel;
            if (item == this.SelectedItem && e.PropertyName == "IsEquiped")
            {
                this.SetWeaponsEquiped(item);
            }
            this.UpdateTemplate();
        }

        public void OnSlotDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.LoadItem();
        }

        private void OnIncludeRacialesChanged()
        {
            var bonuses = this.GetItems().SelectMany(o => o.GetBonuses(false)).ToList();
            this.UpdateResists(bonuses);
            this.UpdateDecapResist(bonuses);
            this.UpdateDecapAndResist(bonuses);
        }

        #endregion


    }

}
