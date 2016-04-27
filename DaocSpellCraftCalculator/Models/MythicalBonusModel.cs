using System.Collections.Generic;
using System.Xml.Serialization;

namespace DaocSpellCraftCalculator.Models
{
    [XmlRoot("MythicalBonus")]
    public class MythicalBonusModel : ElementModel
    {

        private int _LevelDividedBy;
        public int LevelDividedBy
        {
            get { return _LevelDividedBy; }
            set { if (_LevelDividedBy != value) { _LevelDividedBy = value; RaisePropertyChanged("LevelDividedBy"); } }
        }

    }


    public class MythicalBonusModelList
    {

        [XmlElement("MythicalBonus")]
        public List<MythicalBonusModel> ToABonuses { get; set; }
    }
}
