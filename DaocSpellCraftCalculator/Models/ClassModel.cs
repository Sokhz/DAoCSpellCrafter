using System.Collections.Generic;
using System.Xml.Serialization;

namespace DaocSpellCraftCalculator.Models
{
    [XmlRoot("Class")]
    public class ClassModel : ElementModel
    {
        private List<string> _LstRaces;
        [XmlArray(ElementName = "Races")]
        [XmlArrayItem(ElementName = "Race")]
        public List<string> LstRaces
        {
            get { return _LstRaces; }
            set { if (_LstRaces != value) { _LstRaces = value; RaisePropertyChanged("LstRaces"); } }
        }


        private List<string> _LstRealms;
        [XmlArray(ElementName = "Realms")]
        [XmlArrayItem(ElementName = "Realm")]
        public List<string> LstRealms
        {
            get { return _LstRealms; }
            set { if (_LstRealms != value) { _LstRealms = value; RaisePropertyChanged("LstRealms"); } }
        }


        private List<string> _LstArmors;
        [XmlArray(ElementName = "ArmorTypes")]
        [XmlArrayItem(ElementName = "ArmorType")]
        public List<string> LstArmors
        {
            get { return _LstArmors; }
            set { if (_LstArmors != value) { _LstArmors = value; RaisePropertyChanged("LstArmors"); } }
        }


        private string _Epic;
        public string Epic
        {
            get { return _Epic; }
            set { if (_Epic != value) { _Epic = value; RaisePropertyChanged("Epic"); } }
        }

        private string _AcuityStat;
        public string AcuityStat
        {
            get { return _AcuityStat; }
            set { if (_AcuityStat != value) { _AcuityStat = value; RaisePropertyChanged("AcuityStat"); } }
        }

        private string _LegendWeaponType;
        public string LegendWeaponType
        {
            get { return _LegendWeaponType; }
            set { if (_LegendWeaponType != value) { _LegendWeaponType = value; RaisePropertyChanged("LegendWeaponType"); } }
        }

        private string _Shield;
        public string Shield
        {
            get { return _Shield; }
            set { if (_Shield != value) { _Shield = value; RaisePropertyChanged("Shield"); } }
        }
		
    }


    public class ClassModelList
    {

        [XmlElement("Class")]
        public List<ClassModel> Classes { get; set; }
    }
}
