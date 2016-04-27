using System.Collections.Generic;
using System.Xml.Serialization;

namespace DaocSpellCraftCalculator.Models
{

    [XmlRoot("TypeBonus")]
    public class TypeBonusModel : ElementModel
    {

        private bool _Craft;
        public bool Craft
        {
            get { return _Craft; }
            set { if (_Craft != value) { _Craft = value; RaisePropertyChanged("Craft"); } }
        }

        private string _Dust;
        public string Dust
        {
            get { return _Dust; }
            set { if (_Dust != value) { _Dust = value; RaisePropertyChanged("Dust"); } }
        }

        private string _Agent;
        public string Agent
        {
            get { return _Agent; }
            set { if (_Agent != value) { _Agent = value; RaisePropertyChanged("Agent"); } }
        }
		
		
    }

    public class TypeBonusModelList
    {
        [XmlElement("TypeBonus")]
        public List<TypeBonusModel> TypeBonuses { get; set; }
    }
}
