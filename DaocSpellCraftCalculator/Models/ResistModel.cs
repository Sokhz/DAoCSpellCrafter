using System.Collections.Generic;
using System.Xml.Serialization;

namespace DaocSpellCraftCalculator.Models
{
    [XmlRoot("Resist")]
    public class ResistModel : ElementModel
    {

        private string _GemName;
        public string GemName
        {
            get { return _GemName; }
            set { if (_GemName != value) { _GemName = value; RaisePropertyChanged("GemName"); } }
        }

        private string _GemCode;
        public string GemCode
        {
            get { return _GemCode; }
            set { if (_GemCode != value) { _GemCode = value; RaisePropertyChanged("GemCode"); } }
        }

        private string _Agent;
        public string Agent
        {
            get { return _Agent; }
            set { if (_Agent != value) { _Agent = value; RaisePropertyChanged("Agent"); } }
        }
		
		

    }

    public class ResistModelList
    {

        [XmlElement("Resist")]
        public List<ResistModel> Resists { get; set; }
    }
}
