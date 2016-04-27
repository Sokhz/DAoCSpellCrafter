using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace DaocSpellCraftCalculator.Models
{
    [XmlInclude(typeof(ArmorModel))]
    [XmlInclude(typeof(JeweleryModel))]
    [XmlInclude(typeof(WeaponModel))]
    public class SlotModel : ViewModelBase
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

        private string _RepName;
        public string RepName
        {
            get { return _RepName; }
            set { if (_RepName != value) { _RepName = value; RaisePropertyChanged("RepName"); } }
        }
		


    }
}
