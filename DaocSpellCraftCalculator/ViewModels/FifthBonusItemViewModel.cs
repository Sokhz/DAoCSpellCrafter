using DaocSpellCraftCalculator.Models;
using DaocSpellCraftCalculator.Tools;
using DaocSpellCraftCalculator.Views;
using GalaSoft.MvvmLight;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaocSpellCraftCalculator.ViewModels
{
    public class FifthBonusItemViewModel : ViewModelBase
    {

        #region Constructor

        public FifthBonusItemViewModel()
        {
            var mainVM = ServiceLocator.Current.GetInstance<MainViewModel>();
            this.Realm = DataProvider.Current.Realms.FirstOrDefault(o => o.Code == mainVM.SelectedTemplate.Realm);
            this.Class = DataProvider.Current.Classes.FirstOrDefault(o => o.Code == mainVM.SelectedTemplate.Class);
            this.Slot = mainVM.SelectedTemplate.SelectedSlot;
        }

        #endregion


        #region Properties

        private string _Slot;
        public string Slot
        {
            get { return _Slot; }
            set { if (_Slot != value) { _Slot = value; RaisePropertyChanged("Slot"); OnSlotChanged(); } }
        }

        private bool IsWeaponSlot
        {
            get
            {
                return this.Slot == "MH" || this.Slot == "OH";
            }
        }

        private RealmModel _Realm;
        public RealmModel Realm
        {
            get { return _Realm; }
            set { if (_Realm != value) { _Realm = value; RaisePropertyChanged("Realm"); } }
        }

        private ClassModel _Class;
        public ClassModel Class
        {
            get { return _Class; }
            set { if (_Class != value) { _Class = value; RaisePropertyChanged("Class"); } }
        }

        private ObservableCollection<ArmorBonusModel> _LstArmorBonuses;
        public ObservableCollection<ArmorBonusModel> LstArmorBonuses
        {
            get { return _LstArmorBonuses; }
            set { if (_LstArmorBonuses != value) { _LstArmorBonuses = value; RaisePropertyChanged("LstArmorBonuses"); } }
        }

        #endregion


        #region Commands

        #endregion


        #region Methods

        private void InitArmorBonuses()
        {
            var bonuses = new List<ArmorBonusModel>();
            if (this.Realm != null && this.Class != null && !string.IsNullOrEmpty(this.Slot) && !this.IsWeaponSlot)
                bonuses = DataProvider.Current.ArmorBonuses.Where(o => o.Realm == this.Realm.Code && this.Class.LstArmors.Contains(o.ArmorType) && o.Armor == this.Slot).OrderBy(o => o.ArmorTypeModel.Order).ToList();
            else if (this.Realm != null && this.Class != null && !string.IsNullOrEmpty(this.Slot) && this.IsWeaponSlot)
            {
                bonuses = DataProvider.Current.ArmorBonuses.Where(o => o.Realm == this.Realm.Code && o.Armor == this.Slot).OrderBy(o => o.Representation).ToList();
                if (this.Class.Shield == "MED")
                    bonuses = bonuses.Except(bonuses.Where(o => o.ArmorType == "LAR")).ToList();
                if (string.IsNullOrEmpty(this.Class.Shield))
                    bonuses = bonuses.Except(bonuses.Where(o => o.ArmorType == "LAR" || o.ArmorType == "MED")).ToList();
            }
            this.LstArmorBonuses = new ObservableCollection<ArmorBonusModel>(bonuses);
        }

        #endregion


        #region Events

        private void OnSlotChanged()
        {
            this.InitArmorBonuses();
        }

        #endregion
    }
}
