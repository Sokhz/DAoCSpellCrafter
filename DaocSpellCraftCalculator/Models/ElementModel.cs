using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace DaocSpellCraftCalculator.Models
{
    [XmlInclude(typeof(ArmorBonusModel))]
    [XmlInclude(typeof(ArmorModel))]
    [XmlInclude(typeof(ArmorTypeModel))]
    [XmlInclude(typeof(CaracModel))]
    [XmlInclude(typeof(ClassModel))]
    [XmlInclude(typeof(CraftBonusModel))]
    [XmlInclude(typeof(JeweleryModel))]
    [XmlInclude(typeof(RaceModel))]
    [XmlInclude(typeof(RealmModel))]
    [XmlInclude(typeof(ResistModel))]
    [XmlInclude(typeof(SkillModel))]
    [XmlInclude(typeof(ToaBonusModel))]
    [XmlInclude(typeof(TypeBonusModel))]
    [XmlInclude(typeof(WeaponModel))]
    public class ElementModel : ViewModelBase
    {
        private string _Code;
        public string Code
        {
            get { return _Code; }
            set { if (_Code != value) { _Code = value; RaisePropertyChanged("Code"); } }
        }

        private string _Full;
        public string Full
        {
            get { return _Full; }
            set { if (_Full != value) { _Full = value; RaisePropertyChanged("Full"); } }
        }

        private int _Order;
        public int Order
        {
            get { return _Order; }
            set { if (_Order != value) { _Order = value; RaisePropertyChanged("Order"); } }
        }


    }
}
