using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace DaocSpellCraftCalculator.Models
{

    [XmlRoot("CraftBonus")]
    public class CraftBonusModel : ViewModelBase
    {

        private string _Stat;
        public string Stat
        {
            get { return _Stat; }
            set { if (_Stat != value) { _Stat = value; RaisePropertyChanged("Stat"); } }
        }

        private int _Value;
        public int Value
        {
            get { return _Value; }
            set { if (_Value != value) { _Value = value; RaisePropertyChanged("Value"); } }
        }

        private decimal _Charge;
        public decimal Charge
        {
            get { return _Charge; }
            set { if (_Charge != value) { _Charge = value; RaisePropertyChanged("Charge"); } }
        }

        private string _GemQuality;
        public string GemQuality
        {
            get { return _GemQuality; }
            set { if (_GemQuality != value) { _GemQuality = value; RaisePropertyChanged("GemQuality"); } }
        }

        private int _NbAgents;
        public int NbAgents
        {
            get { return _NbAgents; }
            set { if (_NbAgents != value) { _NbAgents = value; RaisePropertyChanged("NbAgents"); } }
        }

        private int _NbDusts;
        public int NbDusts
        {
            get { return _NbDusts; }
            set { if (_NbDusts != value) { _NbDusts = value; RaisePropertyChanged("NbDusts"); } }
        }
		
		
		
		
		
    }


    public class CraftBonusModelList
    {
        [XmlElement("CraftBonus")]
        public List<CraftBonusModel> CraftBonuses { get; set; }
    }
}
