using System.Collections.Generic;
using System.Xml.Serialization;

namespace DaocSpellCraftCalculator.Models
{
    [XmlRoot("Race")]
    public class RaceModel : ElementModel
    {
        private string _Realm;
        public string Realm
        {
            get { return _Realm; }
            set { if (_Realm != value) { _Realm = value; RaisePropertyChanged("Realm"); } }
        }


        private List<ElementValueModel> _LstResists;
        [XmlArray(ElementName = "Resists")]
        [XmlArrayItem(ElementName = "Resist")]
        public List<ElementValueModel> LstResists
        {
            get { return _LstResists; }
            set { if (_LstResists != value) { _LstResists = value; RaisePropertyChanged("LstResists"); } }
        }
    }



    public class RaceModelList
    {
        [XmlElement("Race")]
        public List<RaceModel> Races { get; set; }
    }
}
