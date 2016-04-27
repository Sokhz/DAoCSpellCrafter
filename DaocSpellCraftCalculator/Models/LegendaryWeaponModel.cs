using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DaocSpellCraftCalculator.Models
{
    public class LegendaryWeaponModel : ViewModelBase
    {

        private string _Code;
        public string Code
        {
            get { return _Code; }
            set { if (_Code != value) { _Code = value; RaisePropertyChanged("Code"); } }
        }

        private List<ElementValueModel> _Bonuses;
        [XmlArray(ElementName = "Bonuses")]
        [XmlArrayItem(ElementName = "Bonus")]
        public List<ElementValueModel> Bonuses
        {
            get { return _Bonuses; }
            set { if (_Bonuses != value) { _Bonuses = value; RaisePropertyChanged("Bonuses"); } }
        }
		

    }

    public class LegendaryWeaponModelList
    {

        [XmlElement("LegendaryWep")]
        public List<LegendaryWeaponModel> LegendaryWeapons { get; set; }
    }
}
