using System.Collections.Generic;
using System.Xml.Serialization;

namespace DaocSpellCraftCalculator.Models
{
    [XmlRoot("Carac")]
    public class CaracModel : ElementModel
    {

        private bool _Craft;
        public bool Craft
        {
            get { return _Craft; }
            set { if (_Craft != value) { _Craft = value; RaisePropertyChanged("Craft"); } }
        }

        private bool _ShowStats;
        public bool ShowStats
        {
            get { return _ShowStats; }
            set { if (_ShowStats != value) { _ShowStats = value; RaisePropertyChanged("ShowStats"); } }
        }

        private bool _OnLoot;
        public bool OnLoot
        {
            get { return _OnLoot; }
            set { if (_OnLoot != value) { _OnLoot = value; RaisePropertyChanged("OnLoot"); } }
        }

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

    public class CaracModelList
    {

        [XmlElement("Carac")]
        public List<CaracModel> Caracs { get; set; }
    }
}
