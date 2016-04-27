using DaocSpellCraftCalculator.Models;
using DaocSpellCraftCalculator.Tools;
using GalaSoft.MvvmLight;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;

namespace DaocSpellCraftCalculator.ViewModels
{
    [XmlRoot("Item")]
    public class PartialItemViewModel : ViewModelBase
    {
        private string _PathName;
        [XmlIgnore]
        public string PathName
        {
            get { return _PathName; }
            set { if (_PathName != value) { _PathName = value; RaisePropertyChanged("PathName"); } }
        }


    }


    [XmlRoot("Item")]
    public class ItemViewModel : PartialItemViewModel
    {

        #region Constructor

        public ItemViewModel()
            : this("", false, false)
        {
        }


        public ItemViewModel(string slot, bool isLoot, bool isEquiped)
        {
            this.Name = "";
            this.Slot = slot;
            this.Level = 51;
            this.BonusLevel = 0;
            this.Quality = 99;
            this.Bonus1 = new BonusItemViewModel();
            this.Bonus2 = new BonusItemViewModel();
            this.Bonus3 = new BonusItemViewModel();
            this.Bonus4 = new BonusItemViewModel();
            this.Bonus5 = new BonusItemViewModel() { IsFifthBonus = true };
            this.Bonus6 = new BonusItemViewModel();
            this.Bonus7 = new BonusItemViewModel();
            this.Bonus8 = new BonusItemViewModel();
            this.Bonus9 = new BonusItemViewModel();
            this.Bonus10 = new BonusItemViewModel();
            this.Bonus11 = new BonusItemViewModel();
            this.Bonus12 = new BonusItemViewModel();
            this.Bonus13 = new BonusItemViewModel();
            this.Bonus14 = new BonusItemViewModel();
            this.Bonus15 = new BonusItemViewModel();
            this.IsLoot = isLoot;
            this.IsEquiped = isEquiped;
            this.ClassRestriction = new List<string>();
        }

        #endregion


        #region Properties

        private string _Name;
        public string Name
        {
            get { return _Name; }
            set { if (_Name != value) { _Name = value; RaisePropertyChanged("Name"); } }
        }

        private string _Slot;
        public string Slot
        {
            get { return _Slot; }
            set { if (_Slot != value) { _Slot = value; RaisePropertyChanged("Slot"); } }
        }
        [XmlIgnore]
        public bool IsArmorSlot
        {
            get { return DataProvider.Current.Armors.Select(o => o.Code).Contains(this.Slot); }
        }
        [XmlIgnore]
        public bool IsWeaponSlot
        {
            get { return DataProvider.Current.Weapons.Select(o => o.Code).Contains(this.Slot); }
        }
        [XmlIgnore]
        public bool IsNotWeaponSlot
        {
            get { return !this.IsWeaponSlot; }
        }
        [XmlIgnore]
        public bool IsJewelerySlot
        {
            get { return DataProvider.Current.Jeweleries.Select(o => o.Code).Contains(this.Slot); }
        }
        [XmlIgnore]
        public bool NotIsJewelerySlot
        {
            get { return !this.IsJewelerySlot; }
        }
        [XmlIgnore]
        public string SlotFull
        {
            get
            {
                SlotModel slot = DataProvider.Current.Jeweleries.FirstOrDefault(o => o.Code == this.Slot);
                if (slot == null)
                    slot = DataProvider.Current.Armors.FirstOrDefault(o => o.Code == this.Slot);
                if (slot == null)
                    slot = DataProvider.Current.Weapons.FirstOrDefault(o => o.Code == this.Slot);
                return slot.Full;
            }
        }

        private bool _IsLoot;
        [XmlElement("Loot")]
        public bool IsLoot
        {
            get { return _IsLoot; }
            set { if (_IsLoot != value) { _IsLoot = value; UpdateBonuses(true); } }
        }
        [XmlIgnore]
        public bool IsNotLoot
        {
            get { return !_IsLoot; }
        }
        [XmlIgnore]
        public bool ShowFithBonus
        {
            get { return this.IsNotLoot && !this.IsLegendaryWeapon; }
        }

        private bool _IsLegendaryWeapon;
        [XmlElement("Legendary")]
        public bool IsLegendaryWeapon
        {
            get { return _IsLegendaryWeapon; }
            set { if (_IsLegendaryWeapon != value) { _IsLegendaryWeapon = value; UpdateBonuses(false); } }
        }
        [XmlIgnore]
        public bool NotIsLegendaryWeapon
        {
            get { return !this.IsLegendaryWeapon; }
        }
        [XmlIgnore]
        public bool ShowExtraBonus
        {
            get { return this.IsLoot || this.IsLegendaryWeapon; }
        }


        private List<string> _ClassRestriction;
        [XmlArray(ElementName = "ClassRestriction")]
        [XmlArrayItem(ElementName = "Class")]
        public List<string> ClassRestriction
        {
            get { return _ClassRestriction; }
            set { if (_ClassRestriction != value) { _ClassRestriction = value; RaisePropertyChanged("ClassRestriction"); } }
        }


        [XmlIgnore]
        public bool ClassWarning
        {
            get
            {
                var classe = DataProvider.Current.Classes.FirstOrDefault(o => o.Code == ServiceLocator.Current.GetInstance<MainViewModel>().SelectedTemplate.Class);
                return !(this.ClassRestriction.Count == 0) && !this.ClassRestriction.Contains(classe.Code);
            }
        }


        private int _Quality;
        public int Quality
        {
            get { return _Quality; }
            set { if (_Quality != value) { _Quality = value; RaisePropertyChanged("Quality"); } }
        }

        private bool _IsEquiped;
        public bool IsEquiped
        {
            get { return _IsEquiped; }
            set { if (_IsEquiped != value) { _IsEquiped = value; } RaisePropertyChanged("IsEquiped"); RaisePropertyChanged("IsEquipedAndDefined"); }
        }

        private int _Level;
        public int Level
        {
            get { return _Level; }
            set { if (_Level != value) { _Level = value; RaisePropertyChanged("Level"); OnLevelChanged(); } }
        }

        private int _BonusLevel;
        public int BonusLevel
        {
            get { return _BonusLevel; }
            set { if (_BonusLevel != value) { _BonusLevel = value; RaisePropertyChanged("BonusLevel"); } }
        }


        private BonusItemViewModel _Bonus1;
        public BonusItemViewModel Bonus1
        {
            get { return _Bonus1; }
            set { if (_Bonus1 != value) { _Bonus1 = value; RaisePropertyChanged("Bonus1"); _Bonus1.PropertyChanged += BonusItemChanged; } }
        }
        public bool ShouldSerializeBonus1()
        {
            return this.Bonus1.ShouldSerializeBonus() || this.Bonus1.ShouldSerializeTypeBonus() || this.Bonus1.ShouldSerializeValue();
        }

        private BonusItemViewModel _Bonus2;
        public BonusItemViewModel Bonus2
        {
            get { return _Bonus2; }
            set { if (_Bonus2 != value) { _Bonus2 = value; RaisePropertyChanged("Bonus2"); _Bonus2.PropertyChanged += BonusItemChanged; } }
        }
        public bool ShouldSerializeBonus2()
        {
            return this.Bonus2.ShouldSerializeBonus() || this.Bonus2.ShouldSerializeTypeBonus() || this.Bonus2.ShouldSerializeValue();
        }

        private BonusItemViewModel _Bonus3;
        public BonusItemViewModel Bonus3
        {
            get { return _Bonus3; }
            set { if (_Bonus3 != value) { _Bonus3 = value; RaisePropertyChanged("Bonus3"); _Bonus3.PropertyChanged += BonusItemChanged; } }
        }
        public bool ShouldSerializeBonus3()
        {
            return this.Bonus3.ShouldSerializeBonus() || this.Bonus3.ShouldSerializeTypeBonus() || this.Bonus3.ShouldSerializeValue();
        }

        private BonusItemViewModel _Bonus4;
        public BonusItemViewModel Bonus4
        {
            get { return _Bonus4; }
            set { if (_Bonus4 != value) { _Bonus4 = value; RaisePropertyChanged("Bonus4"); _Bonus4.PropertyChanged += BonusItemChanged; } }
        }
        public bool ShouldSerializeBonus4()
        {
            return this.Bonus4.ShouldSerializeBonus() || this.Bonus4.ShouldSerializeTypeBonus() || this.Bonus4.ShouldSerializeValue();
        }

        private BonusItemViewModel _Bonus5;
        public BonusItemViewModel Bonus5
        {
            get { return _Bonus5; }
            set
            {
                if (_Bonus5 != value)
                {
                    if (_Bonus5 != null)
                        _Bonus5.PropertyChanged -= BonusItemChanged;
                    _Bonus5 = value;
                    _Bonus5.PropertyChanged += BonusItemChanged;
                    RaisePropertyChanged("Bonus5");
                }
            }
        }
        public bool ShouldSerializeBonus5()
        {
            return this.Bonus5.ShouldSerializeBonus() || this.Bonus5.ShouldSerializeTypeBonus() || this.Bonus5.ShouldSerializeValue();
        }

        private BonusItemViewModel _Bonus6;
        public BonusItemViewModel Bonus6
        {
            get { return _Bonus6; }
            set { if (_Bonus6 != value) { _Bonus6 = value; RaisePropertyChanged("Bonus6"); _Bonus6.PropertyChanged += BonusItemChanged; } }
        }
        public bool ShouldSerializeBonus6()
        {
            return this.Bonus6.ShouldSerializeBonus() || this.Bonus6.ShouldSerializeTypeBonus() || this.Bonus6.ShouldSerializeValue();
        }

        private BonusItemViewModel _Bonus7;
        public BonusItemViewModel Bonus7
        {
            get { return _Bonus7; }
            set { if (_Bonus7 != value) { _Bonus7 = value; RaisePropertyChanged("Bonus7"); _Bonus7.PropertyChanged += BonusItemChanged; } }
        }
        public bool ShouldSerializeBonus7()
        {
            return this.Bonus7.ShouldSerializeBonus() || this.Bonus7.ShouldSerializeTypeBonus() || this.Bonus7.ShouldSerializeValue();
        }

        private BonusItemViewModel _Bonus8;
        public BonusItemViewModel Bonus8
        {
            get { return _Bonus8; }
            set { if (_Bonus8 != value) { _Bonus8 = value; RaisePropertyChanged("Bonus8"); _Bonus8.PropertyChanged += BonusItemChanged; } }
        }
        public bool ShouldSerializeBonus8()
        {
            return this.Bonus8.ShouldSerializeBonus() || this.Bonus8.ShouldSerializeTypeBonus() || this.Bonus8.ShouldSerializeValue();
        }

        private BonusItemViewModel _Bonus9;
        public BonusItemViewModel Bonus9
        {
            get { return _Bonus9; }
            set { if (_Bonus9 != value) { _Bonus9 = value; RaisePropertyChanged("Bonus9"); _Bonus9.PropertyChanged += BonusItemChanged; } }
        }
        public bool ShouldSerializeBonus9()
        {
            return this.Bonus9.ShouldSerializeBonus() || this.Bonus9.ShouldSerializeTypeBonus() || this.Bonus9.ShouldSerializeValue();
        }

        private BonusItemViewModel _Bonus10;
        public BonusItemViewModel Bonus10
        {
            get { return _Bonus10; }
            set { if (_Bonus10 != value) { _Bonus10 = value; RaisePropertyChanged("Bonus10"); _Bonus10.PropertyChanged += BonusItemChanged; } }
        }
        public bool ShouldSerializeBonus10()
        {
            return this.Bonus10.ShouldSerializeBonus() || this.Bonus10.ShouldSerializeTypeBonus() || this.Bonus10.ShouldSerializeValue();
        }

        private BonusItemViewModel _Bonus11;
        [XmlIgnore]
        public BonusItemViewModel Bonus11
        {
            get { return _Bonus11; }
            set { if (_Bonus11 != value) { _Bonus11 = value; RaisePropertyChanged("Bonus11"); _Bonus11.PropertyChanged += BonusItemChanged; } }
        }
        public bool ShouldSerializeBonus11()
        {
            return this.Bonus11.ShouldSerializeBonus() || this.Bonus11.ShouldSerializeTypeBonus() || this.Bonus11.ShouldSerializeValue();
        }

        private BonusItemViewModel _Bonus12;
        [XmlIgnore]
        public BonusItemViewModel Bonus12
        {
            get { return _Bonus12; }
            set { if (_Bonus12 != value) { _Bonus12 = value; RaisePropertyChanged("Bonus12"); _Bonus12.PropertyChanged += BonusItemChanged; } }
        }
        public bool ShouldSerializeBonus12()
        {
            return this.Bonus12.ShouldSerializeBonus() || this.Bonus12.ShouldSerializeTypeBonus() || this.Bonus12.ShouldSerializeValue();
        }

        private BonusItemViewModel _Bonus13;
        [XmlIgnore]
        public BonusItemViewModel Bonus13
        {
            get { return _Bonus13; }
            set { if (_Bonus13 != value) { _Bonus13 = value; RaisePropertyChanged("Bonus13"); _Bonus13.PropertyChanged += BonusItemChanged; } }
        }
        public bool ShouldSerializeBonus13()
        {
            return this.Bonus13.ShouldSerializeBonus() || this.Bonus13.ShouldSerializeTypeBonus() || this.Bonus13.ShouldSerializeValue();
        }

        private BonusItemViewModel _Bonus14;
        [XmlIgnore]
        public BonusItemViewModel Bonus14
        {
            get { return _Bonus14; }
            set { if (_Bonus14 != value) { _Bonus14 = value; RaisePropertyChanged("Bonus14"); _Bonus14.PropertyChanged += BonusItemChanged; } }
        }
        public bool ShouldSerializeBonus14()
        {
            return this.Bonus14.ShouldSerializeBonus() || this.Bonus14.ShouldSerializeTypeBonus() || this.Bonus14.ShouldSerializeValue();
        }

        private BonusItemViewModel _Bonus15;
        [XmlIgnore]
        public BonusItemViewModel Bonus15
        {
            get { return _Bonus15; }
            set { if (_Bonus15 != value) { _Bonus15 = value; RaisePropertyChanged("Bonus15"); _Bonus15.PropertyChanged += BonusItemChanged; } }
        }
        public bool ShouldSerializeBonus15()
        {
            return this.Bonus15.ShouldSerializeBonus() || this.Bonus15.ShouldSerializeTypeBonus() || this.Bonus15.ShouldSerializeValue();
        }


        private List<BonusItemViewModel> _LstBonusDetails;
        [XmlIgnore]
        public List<BonusItemViewModel> LstBonusDetails
        {
            get { return _LstBonusDetails; }
            set { if (_LstBonusDetails != value) { _LstBonusDetails = value; RaisePropertyChanged("LstBonusDetails"); } }
        }



        private FifthBonusItemViewModel _FifthBonus;
        [XmlIgnore]
        public FifthBonusItemViewModel FifthBonus
        {
            get { return _FifthBonus; }
            set { if (_FifthBonus != value) { _FifthBonus = value; RaisePropertyChanged("FifthBonus"); RaisePropertyChanged("SelectedFifthBonus"); } }
        }

        private string _SelectedFifthBonus;
        public string SelectedFifthBonus
        {
            get { return _SelectedFifthBonus; }
            set { if (_SelectedFifthBonus != value) { _SelectedFifthBonus = value; OnFifthBonusChanged(); RaisePropertyChanged("SelectedFifthBonus"); RaisePropertyChanged("IsSkinsEnable"); RaisePropertyChanged("LstSkins"); } }
        }

        [XmlIgnore]
        public bool ShowSkinsAvalaible
        {
            get
            {
                return this.Slot == "HEA";
            }
        }

        [XmlIgnore]
        public bool IsSkinsEnable
        {
            get
            {
                return !string.IsNullOrEmpty(this.SelectedFifthBonus) && this.LstSkins.Count > 0;
            }
        }

        [XmlIgnore]
        public ObservableCollection<string> LstSkins
        {
            get
            {
                var retour = new ObservableCollection<string>();
                if (!string.IsNullOrEmpty(this.SelectedFifthBonus))
                {
                    var bonus = DataProvider.Current.ArmorBonuses.FirstOrDefault(o => o.Code == this.SelectedFifthBonus);
                    retour = new ObservableCollection<string>(bonus.Skins);
                }
                return retour;
            }
        }

        private string _SelectedSkinHead;
        public string SelectedSkinHead
        {
            get { return _SelectedSkinHead; }
            set { if (_SelectedSkinHead != value) { _SelectedSkinHead = value; RaisePropertyChanged("SelectedSkinHead"); OnSkinHeadChanged(); } }
        }



        private string _Effect1;
        public string Effect1
        {
            get { return _Effect1; }
            set { if (_Effect1 != value) { _Effect1 = value; RaisePropertyChanged("Effect1"); OnEffect1Changed(); } }
        }
        public bool ShouldSerializeEffect1()
        {
            return !string.IsNullOrEmpty(this.Effect1);
        }
        private UseItemModel _Effect1Model;
        [XmlIgnore]
        public UseItemModel Effect1Model
        {
            get { return _Effect1Model; }
            set { if (_Effect1Model != value) { _Effect1Model = value; RaisePropertyChanged("Effect1Model"); } }
        }


        private string _Effect2;
        public string Effect2
        {
            get { return _Effect2; }
            set { if (_Effect2 != value) { _Effect2 = value; RaisePropertyChanged("Effect2"); OnEffect2Changed(); } }
        }
        public bool ShouldSerializeEffect2()
        {
            return !string.IsNullOrEmpty(this.Effect2);
        }
        private UseItemModel _Effect2Model;
        [XmlIgnore]
        public UseItemModel Effect2Model
        {
            get { return _Effect2Model; }
            set { if (_Effect2Model != value) { _Effect2Model = value; RaisePropertyChanged("Effect2Model"); } }
        }

        [XmlIgnore]
        public List<UseItemModel> LstUses
        {
            get
            {
                var retour = new List<UseItemModel>();
                if (this.Effect1Model != null && this.Effect1Model.EffectType == "USE")
                    retour.Add(this.Effect1Model);
                if (this.Effect2Model != null && this.Effect2Model.EffectType == "USE")
                    retour.Add(this.Effect2Model);
                return retour;
            }
        }
        [XmlIgnore]
        public List<UseItemModel> LstPassives
        {
            get
            {
                var retour = new List<UseItemModel>();
                if (this.Effect1Model != null && this.Effect1Model.EffectType == "PASSIVE")
                    retour.Add(this.Effect1Model);
                if (this.Effect2Model != null && this.Effect2Model.EffectType == "PASSIVE")
                    retour.Add(this.Effect2Model);
                return retour;
            }
        }
        [XmlIgnore]
        public List<UseItemModel> LstReverses
        {
            get
            {
                var retour = new List<UseItemModel>();
                if (this.Effect1Model != null && this.Effect1Model.EffectType == "REVERSE")
                    retour.Add(this.Effect1Model);
                if (this.Effect2Model != null && this.Effect2Model.EffectType == "REVERSE")
                    retour.Add(this.Effect2Model);
                return retour;
            }
        }
        [XmlIgnore]
        public List<UseItemModel> LstProcs
        {
            get
            {
                var retour = new List<UseItemModel>();
                if (this.Effect1Model != null && this.Effect1Model.EffectType == "PROC")
                    retour.Add(this.Effect1Model);
                if (this.Effect2Model != null && this.Effect2Model.EffectType == "PROC")
                    retour.Add(this.Effect2Model);
                return retour;
            }
        }

        private decimal _Utility;
        [XmlIgnore]
        public decimal Utility
        {
            get { return _Utility; }
            set { if (_Utility != value) { _Utility = value; RaisePropertyChanged("Utility"); } }
        }


        [XmlIgnore]
        public decimal ChargeCraft
        {
            get
            {
                return this.GetCraftCharge();
            }
        }
        [XmlIgnore]
        public decimal MaxChargeCraft
        {
            get
            {
                return this.GetMaxCraftCharge();
            }
        }
        [XmlIgnore]
        public bool IsOverCharged
        {
            get
            {
                return (this.IsNotLoot ? this.ChargeCraft > (this.MaxChargeCraft + (decimal)5.5) : false);
            }
        }
        [XmlIgnore]
        public bool IsCraftInvalid
        {
            get
            {
                return this.IsOverCharged || this.CheckCraftBonusInvalid();
            }
        }
        [XmlIgnore]
        public bool HasBonusDefined
        {
            get
            {
                return this.GetHasBonusDefined();
            }
        }
        [XmlIgnore]
        public bool IsEquipedAndDefined
        {
            get
            {
                return this.IsEquiped && this.HasBonusDefined;
            }
        }
        [XmlIgnore]
        public string ChargeReport
        {
            get { return this.GetChargeReport(); }
        }

        [XmlIgnore]
        public List<BonusItemViewModel> LstBonuses
        {
            get { return this.GetBonuses(false); }
        }

        [XmlIgnore]
        public List<BonusItemViewModel> LstBonusesCraft
        {
            get { return this.GetBonusesCraft(false); }
        }

        #endregion


        #region Methods

        public List<BonusItemViewModel> GetBonuses(bool noBonus)
        {
            var retour = new List<BonusItemViewModel>();
            if (this.Bonus1 != null && this.Bonus1.TypeBonus != null && (noBonus || this.Bonus1.Bonus != null) && this.Bonus1.Value.HasValue) retour.Add(this.Bonus1);
            if (this.Bonus2 != null && this.Bonus2.TypeBonus != null && (noBonus || this.Bonus2.Bonus != null) && this.Bonus2.Value.HasValue) retour.Add(this.Bonus2);
            if (this.Bonus3 != null && this.Bonus3.TypeBonus != null && (noBonus || this.Bonus3.Bonus != null) && this.Bonus3.Value.HasValue) retour.Add(this.Bonus3);
            if (this.Bonus4 != null && this.Bonus4.TypeBonus != null && (noBonus || this.Bonus4.Bonus != null) && this.Bonus4.Value.HasValue) retour.Add(this.Bonus4);
            if (this.Bonus5 != null && this.Bonus5.TypeBonus != null && (noBonus || this.Bonus5.Bonus != null) && this.Bonus5.Value.HasValue) retour.Add(this.Bonus5);
            if (this.Bonus6 != null && this.Bonus6.TypeBonus != null && (noBonus || this.Bonus6.Bonus != null) && this.Bonus6.Value.HasValue) retour.Add(this.Bonus6);
            if (this.Bonus7 != null && this.Bonus7.TypeBonus != null && (noBonus || this.Bonus7.Bonus != null) && this.Bonus7.Value.HasValue) retour.Add(this.Bonus7);
            if (this.Bonus8 != null && this.Bonus8.TypeBonus != null && (noBonus || this.Bonus8.Bonus != null) && this.Bonus8.Value.HasValue) retour.Add(this.Bonus8);
            if (this.Bonus9 != null && this.Bonus9.TypeBonus != null && (noBonus || this.Bonus9.Bonus != null) && this.Bonus9.Value.HasValue) retour.Add(this.Bonus9);
            if (this.Bonus10 != null && this.Bonus10.TypeBonus != null && (noBonus || this.Bonus10.Bonus != null) && this.Bonus10.Value.HasValue) retour.Add(this.Bonus10);
            if (this.Bonus11 != null && this.Bonus11.TypeBonus != null && (noBonus || this.Bonus11.Bonus != null) && this.Bonus11.Value.HasValue) retour.Add(this.Bonus11);
            if (this.Bonus12 != null && this.Bonus12.TypeBonus != null && (noBonus || this.Bonus12.Bonus != null) && this.Bonus12.Value.HasValue) retour.Add(this.Bonus12);
            if (this.Bonus13 != null && this.Bonus13.TypeBonus != null && (noBonus || this.Bonus13.Bonus != null) && this.Bonus13.Value.HasValue) retour.Add(this.Bonus13);
            if (this.Bonus14 != null && this.Bonus14.TypeBonus != null && (noBonus || this.Bonus14.Bonus != null) && this.Bonus14.Value.HasValue) retour.Add(this.Bonus14);
            if (this.Bonus15 != null && this.Bonus15.TypeBonus != null && (noBonus || this.Bonus15.Bonus != null) && this.Bonus15.Value.HasValue) retour.Add(this.Bonus15);
            return retour;
        }

        public List<BonusItemViewModel> GetBonusesCraft(bool noBonus)
        {
            var retour = new List<BonusItemViewModel>();
            if (this.Bonus1 != null && this.Bonus1.TypeBonus != null && (noBonus || this.Bonus1.Bonus != null) && this.Bonus1.Value.HasValue) retour.Add(this.Bonus1);
            if (this.Bonus2 != null && this.Bonus2.TypeBonus != null && (noBonus || this.Bonus2.Bonus != null) && this.Bonus2.Value.HasValue) retour.Add(this.Bonus2);
            if (this.Bonus3 != null && this.Bonus3.TypeBonus != null && (noBonus || this.Bonus3.Bonus != null) && this.Bonus3.Value.HasValue) retour.Add(this.Bonus3);
            if (this.Bonus4 != null && this.Bonus4.TypeBonus != null && (noBonus || this.Bonus4.Bonus != null) && this.Bonus4.Value.HasValue) retour.Add(this.Bonus4);
            if (this.Bonus5 != null && this.Bonus5.TypeBonus != null && (noBonus || this.Bonus5.Bonus != null) && this.Bonus5.Value.HasValue) retour.Add(this.Bonus5);
            return retour;
        }

        private decimal GetCraftCharge()
        {
            decimal retour = 0;
            CraftBonusModel craftbonus1 = null;
            CraftBonusModel craftbonus2 = null;
            CraftBonusModel craftbonus3 = null;
            CraftBonusModel craftbonus4 = null;
            List<CraftBonusModel> lstCraftBonuses = new List<CraftBonusModel>();
            if (this.IsNotLoot)
            {
                if (this.Bonus1.IsPartialDefined)
                {
                    craftbonus1 = DataProvider.Current.CraftBonuses.FirstOrDefault(o => o.Stat == this.Bonus1.TypeBonus && o.Value == this.Bonus1.Value.Value);
                }
                if (this.Bonus2.IsPartialDefined)
                {
                    craftbonus2 = DataProvider.Current.CraftBonuses.FirstOrDefault(o => o.Stat == this.Bonus2.TypeBonus && o.Value == this.Bonus2.Value.Value);
                }
                if (this.Bonus3.IsPartialDefined)
                {
                    craftbonus3 = DataProvider.Current.CraftBonuses.FirstOrDefault(o => o.Stat == this.Bonus3.TypeBonus && o.Value == this.Bonus3.Value.Value);
                }
                if (this.Bonus4.IsPartialDefined)
                {
                    craftbonus4 = DataProvider.Current.CraftBonuses.FirstOrDefault(o => o.Stat == this.Bonus4.TypeBonus && o.Value == this.Bonus4.Value.Value);
                }

                if (craftbonus1 != null)
                {
                    retour = craftbonus1.Charge;
                    lstCraftBonuses.Add(craftbonus1);
                }
                if (craftbonus2 != null)
                {
                    retour += craftbonus2.Charge;
                    lstCraftBonuses.Add(craftbonus2);
                }
                if (craftbonus3 != null)
                {
                    retour += craftbonus3.Charge;
                    lstCraftBonuses.Add(craftbonus3);
                }
                if (craftbonus4 != null)
                {
                    retour += craftbonus4.Charge;
                    lstCraftBonuses.Add(craftbonus4);
                }

                if (lstCraftBonuses.Count > 0)
                {
                    retour += lstCraftBonuses.Max(o => o.Charge);
                }

            }
            return retour / 2;
        }
        private decimal GetMaxCraftCharge()
        {
            return (decimal)(Math.Ceiling(this.Level * 0.61));
        }
        private bool CheckCraftBonusInvalid()
        {
            if (this.IsNotLoot)
            {
                var lstCraftBonuses = new List<BonusItemViewModel>();
                lstCraftBonuses.Add(this.Bonus1);
                lstCraftBonuses.Add(this.Bonus2);
                lstCraftBonuses.Add(this.Bonus3);
                lstCraftBonuses.Add(this.Bonus4);

                foreach (var bonus in lstCraftBonuses)
                {
                    if (lstCraftBonuses.Count(o => bonus.IsEquivalent(o)) > 1)
                    {
                        return true;
                    }
                }
                return false;
            }
            return false;
        }
        private string GetChargeReport()
        {
            var retour = "";
            if (this.IsNotLoot)
            {
                retour = "Charge : " + this.ChargeCraft + "/" + this.MaxChargeCraft;
            }
            return retour;

        }

        private bool GetHasBonusDefined()
        {
            var retour = false;
            retour = this.Bonus1.IsDefined;
            retour = retour || this.Bonus2.IsDefined;
            retour = retour || this.Bonus3.IsDefined;
            retour = retour || this.Bonus4.IsDefined;
            retour = retour || this.Bonus5.IsDefined;
            retour = retour || this.Bonus6.IsDefined;
            retour = retour || this.Bonus7.IsDefined;
            retour = retour || this.Bonus8.IsDefined;
            retour = retour || this.Bonus9.IsDefined;
            retour = retour || this.Bonus10.IsDefined;
            retour = retour || this.Bonus11.IsDefined;
            retour = retour || this.Bonus12.IsDefined;
            retour = retour || this.Bonus13.IsDefined;
            retour = retour || this.Bonus14.IsDefined;
            retour = retour || this.Bonus15.IsDefined;

            return retour;
        }


        public void ClearCraft()
        {
            if (this.IsNotLoot)
            {
                this.Bonus1 = new BonusItemViewModel();
                this.Bonus2 = new BonusItemViewModel();
                this.Bonus3 = new BonusItemViewModel();
                this.Bonus4 = new BonusItemViewModel();
            }
        }
        
        public List<BonusItemViewModel> GetBonus(string type, string bonus)
        {
            var retour = new List<BonusItemViewModel>();
            if (bonus == "HP" || bonus == "MAN")
            {
                retour = this.GetBonuses(true).Where(o => o.TypeBonus == bonus || (o.TypeBonus == "CAPS" && o.Bonus == bonus)).ToList();
            }
            else if (type == "STA")
            {
                var classe = DataProvider.Current.Classes.FirstOrDefault(o => o.Code == ServiceLocator.Current.GetInstance<MainViewModel>().SelectedTemplate.Class);
                if (classe != null && bonus == classe.AcuityStat)
                {
                    retour = this.GetBonuses(true).Where(o => (o.TypeBonus == "STA" || o.TypeBonus == "CAPS" || o.TypeBonus == "CAPMS" || o.TypeBonus == "CAPSC") && (o.Bonus == bonus || o.Bonus == "ACU")).ToList();
                }
                else
                    retour = this.GetBonuses(true).Where(o => (o.TypeBonus == "STA" || o.TypeBonus == "CAPS" || o.TypeBonus == "CAPMS" || o.TypeBonus == "CAPSC") && o.Bonus == bonus).ToList();
            }
            else if (type == "RES")
            {
                retour = this.GetBonuses(true).Where(o => (o.TypeBonus == "RES" || o.TypeBonus == "CAPR" || o.TypeBonus == "CAPRC") && o.Bonus == bonus).ToList();
            }
            else if (type == "SKI")
            {
                var skill = DataProvider.Current.Skills.FirstOrDefault(o => o.Code == bonus);
                retour = this.GetBonuses(true).Where(o => o.TypeBonus == type && (o.Bonus == bonus || o.Bonus == skill.Type)).ToList();
            }
            else
                retour = this.GetBonuses(true).Where(o => o.TypeBonus == type && o.Bonus == bonus).ToList();

            this.LstBonusDetails = retour;
            return retour;
        }

        #endregion


        #region Events

        public void RaiseChargeEvents()
        {
            RaisePropertyChanged("ChargeCraft");
            RaisePropertyChanged("MaxChargeCraft");
            RaisePropertyChanged("IsOverCharged");
            RaisePropertyChanged("IsCraftInvalid");
        }

        public void RaiseIsEquipedEvent()
        {
            RaisePropertyChanged("IsEquiped");
        }

        private void OnLevelChanged()
        {
            this.RaiseChargeEvents();
        }

        private void BonusItemChanged(object sender, PropertyChangedEventArgs e)
        {
            var bonus = sender as BonusItemViewModel;
            if (bonus.MustUpdate)
            {
                this.RaiseChargeEvents();
                RaisePropertyChanged("HasBonusDefined");
                RaisePropertyChanged("IsEquipedAndDefined");
                RaisePropertyChanged(e.PropertyName);
            }
        }

        private void OnFifthBonusChanged()
        {
            var bonus = DataProvider.Current.ArmorBonuses.FirstOrDefault(o => o.Code == this.SelectedFifthBonus);
            if (bonus != null)
            {
                this.Name = bonus.Full;
                var fifth = bonus.GetBonusItem();
                fifth.IsFifthBonus = true;
                this.Bonus5 = fifth;
            }
            else
            {
                this.Name = "";
                this.Bonus5 = new BonusItemViewModel() { IsFifthBonus = true };
            }
            RaisePropertyChanged("HasBonusDefined");
            RaisePropertyChanged("IsEquipedAndDefined");
        }

        private void OnSkinHeadChanged()
        {
            var bonus = DataProvider.Current.ArmorBonuses.FirstOrDefault(o => o.Code == this.SelectedFifthBonus);
            if (bonus != null && !string.IsNullOrEmpty(this.SelectedSkinHead))
            {
                var name = bonus.Full;
                var last = name.Split(' ').Last();
                name = name.Substring(0, name.Length - last.Length);
                name += this.SelectedSkinHead;
                this.Name = name;
            }
        }

        private void UpdateBonuses(bool fromLoot)
        {
            if (fromLoot)
                this._IsLegendaryWeapon = false;
            else
                this._IsLoot = false;

            this.Bonus1 = new BonusItemViewModel() { IsLoot = this.IsLoot };
            this.Bonus2 = new BonusItemViewModel() { IsLoot = this.IsLoot };
            this.Bonus3 = new BonusItemViewModel() { IsLoot = this.IsLoot };
            this.Bonus4 = new BonusItemViewModel() { IsLoot = this.IsLoot };
            this.Bonus5 = new BonusItemViewModel() { IsLoot = this.IsLoot, IsFifthBonus = true };
            this.Bonus6 = new BonusItemViewModel() { IsLoot = this.IsLoot };
            this.Bonus7 = new BonusItemViewModel() { IsLoot = this.IsLoot };
            this.Bonus8 = new BonusItemViewModel() { IsLoot = this.IsLoot };
            this.Bonus9 = new BonusItemViewModel() { IsLoot = this.IsLoot };
            this.Bonus10 = new BonusItemViewModel() { IsLoot = this.IsLoot };
            this.Bonus11 = new BonusItemViewModel() { IsLoot = this.IsLoot };
            this.Bonus12 = new BonusItemViewModel() { IsLoot = this.IsLoot };
            this.Bonus13 = new BonusItemViewModel() { IsLoot = this.IsLoot };
            this.Bonus14 = new BonusItemViewModel() { IsLoot = this.IsLoot };
            this.Bonus15 = new BonusItemViewModel() { IsLoot = this.IsLoot };

            if (this.IsLegendaryWeapon)
            {

                LegendaryWeaponModel legend = null;
                if (this.Slot == "MH" || this.Slot == "OH")
                {
                    legend = DataProvider.Current.LegendaryWeapons.FirstOrDefault(o => o.Code == "MELEE");
                }
                else if (this.Slot == "RANGE")
                {
                    legend = DataProvider.Current.LegendaryWeapons.FirstOrDefault(o => o.Code == "MAGIC");
                }
                else if (this.Slot == "TWO")
                {
                    var classe = DataProvider.Current.Classes.First(o => o.Code == ServiceLocator.Current.GetInstance<MainViewModel>().SelectedTemplate.Class);
                    if (classe != null)
                    {
                        legend = DataProvider.Current.LegendaryWeapons.FirstOrDefault(o => o.Code == classe.LegendWeaponType);
                        if (legend == null)
                            legend = DataProvider.Current.LegendaryWeapons.FirstOrDefault(o => o.Code == "MELEE");
                    }
                }

                if (legend != null)
                {
                    var i = 5;
                    foreach (var bonus in legend.Bonuses)
                    {
                        var bonusItem = new BonusItemViewModel();
                        bonusItem.Bonus = bonus.Code;
                        bonusItem.IsLoot = true;
                        bonusItem.TypeBonus = bonus.Type;
                        bonusItem.Value = bonus.Value;

                        if (i == 5)
                            this.Bonus5 = bonusItem;
                        else if (i == 6)
                            this.Bonus6 = bonusItem;
                        else if (i == 7)
                            this.Bonus7 = bonusItem;
                        i += 1;
                    }

                }
            }

            RaisePropertyChanged("IsLoot");
            RaisePropertyChanged("IsNotLoot");
            RaisePropertyChanged("ShowFithBonus");
            RaisePropertyChanged("ShowExtraBonus");
            RaisePropertyChanged("IsLegendaryWeapon");
            RaisePropertyChanged("NotIsLegendaryWeapon");
        }

        private void OnEffect1Changed()
        {
            var use = DataProvider.Current.UseItems.FirstOrDefault(o => o.Code == this.Effect1);
            UseItemModel useItem = null;
            if (use != null)
                useItem = use.GetClone();
            this.Effect1Model = useItem;
            if (this.Effect1Model != null)
                this.Effect1Model.Item = this;
        }
        private void OnEffect2Changed()
        {
            var use = DataProvider.Current.UseItems.FirstOrDefault(o => o.Code == this.Effect2);
            UseItemModel useItem = null;
            if (use != null)
                useItem = use.GetClone();
            this.Effect2Model = useItem;
            if (this.Effect2Model != null)
                this.Effect2Model.Item = this;
        }

        public void UpdateSkillBonus()
        {
            if (!this.IsLoot)
            {
                if (this.Bonus1.IsPartialDefined && this.Bonus1.TypeBonus == "SKI") this.Bonus1.UpdateSkills();
                if (this.Bonus2.IsPartialDefined && this.Bonus2.TypeBonus == "SKI") this.Bonus2.UpdateSkills();
                if (this.Bonus3.IsPartialDefined && this.Bonus3.TypeBonus == "SKI") this.Bonus3.UpdateSkills();
                if (this.Bonus4.IsPartialDefined && this.Bonus4.TypeBonus == "SKI") this.Bonus4.UpdateSkills();
            }
        }

        #endregion


        #region Enumerations

        public enum Enum_Type_Item
        {
            standard = 0,
            epic = 2,
        }

        #endregion

    }
}
