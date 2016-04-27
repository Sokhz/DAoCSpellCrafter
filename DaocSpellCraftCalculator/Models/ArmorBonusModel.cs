using DaocSpellCraftCalculator.Tools;
using DaocSpellCraftCalculator.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DaocSpellCraftCalculator.Models
{
    public class ArmorBonusModel : ElementModel
    {


        private string _Armor;
        public string Armor
        {
            get { return _Armor; }
            set { if (_Armor != value) { _Armor = value; RaisePropertyChanged("Armor"); } }
        }


        private string _ArmorType;
        public string ArmorType
        {
            get { return _ArmorType; }
            set { if (_ArmorType != value) { _ArmorType = value; RaisePropertyChanged("ArmorType"); } }
        }
        public ArmorTypeModel ArmorTypeModel
        {
            get { return DataProvider.Current.ArmorTypes.FirstOrDefault(o => o.Code == this.ArmorType); }
        }
        public WeaponModel WeaponModel
        {
            get { return DataProvider.Current.Weapons.FirstOrDefault(o => o.Code == this.ArmorType); }
        }

        private string _Realm;
        public string Realm
        {
            get { return _Realm; }
            set { if (_Realm != value) { _Realm = value; RaisePropertyChanged("Realm"); } }
        }


        private ElementValueModel _Bonus;
        public ElementValueModel Bonus
        {
            get { return _Bonus; }
            set { if (_Bonus != value) { _Bonus = value; RaisePropertyChanged("Bonus"); } }
        }


        public string Representation
        {
            get { return this.GetRepresentation(); }
        }


        private List<string> _Skins;
        [XmlArray(ElementName = "Skins")]
        [XmlArrayItem(ElementName = "Skin")]
        public List<string> Skins
        {
            get { return _Skins; }
            set { if (_Skins != value) { _Skins = value; RaisePropertyChanged("Skins"); } }
        }



        private string GetRepresentation()
        {
            var retour = "";

            if (this.ArmorTypeModel != null)
                retour = this.ArmorTypeModel.Full;
            else if (this.WeaponModel != null)
                retour = this.WeaponModel.Full;
            else
                retour = this.ArmorType;
            retour += " : ";

            retour += this.Bonus.Value + " ";
            switch (this.Bonus.Type)
            {
                case "STA":
                    retour += DataProvider.Current.Caracs.FirstOrDefault(o => o.Code == this.Bonus.Code).Full;
                    break;
                case "CAPS":
                    retour += DataProvider.Current.TypeBonuses.FirstOrDefault(o => o.Code == this.Bonus.Type).Full + " ";
                    ElementModel carac = DataProvider.Current.Caracs.FirstOrDefault(o => o.Code == this.Bonus.Code);
                    if (carac == null)
                        carac = DataProvider.Current.ToaBonuses.FirstOrDefault(o => o.Code == this.Bonus.Code);
                    retour += carac.Full;
                    break;
                case "HP":
                    retour += DataProvider.Current.TypeBonuses.FirstOrDefault(o => o.Code == this.Bonus.Type).Full;
                    break;
                case "OTH":
                    retour += DataProvider.Current.ToaBonuses.FirstOrDefault(o => o.Code == this.Bonus.Code).Full;
                    break;
                case "SKI":
                    ElementModel bonus = DataProvider.Current.ToaBonuses.FirstOrDefault(o => o.Code == this.Bonus.Code);
                    if (bonus == null)
                        bonus = DataProvider.Current.Skills.FirstOrDefault(o => o.Code == this.Bonus.Code);
                    retour += bonus.Full;
                    break;
            }
            return retour;
        }

        public BonusItemViewModel GetBonusItem()
        {
            var bonusItem = new BonusItemViewModel();
            if ((this.Bonus.Code == "HP" || this.Bonus.Code == "MAN") && this.Bonus.Type == "STA")
            {
                bonusItem.Bonus = "";
                bonusItem.IsLoot = false;
                bonusItem.TypeBonus = this.Bonus.Code;

            }
            else
            {
                bonusItem.Bonus = this.Bonus.Code;
                bonusItem.IsLoot = false;
                bonusItem.TypeBonus = this.Bonus.Type;
            }
            bonusItem.Value = this.Bonus.Value;
            return bonusItem;
        }

    }


    public class ArmorBonusModelList
    {
        [XmlElement("ArmorBonus")]
        public List<ArmorBonusModel> ArmorBonuses { get; set; }
    }
}
