using System.Collections.Generic;
using System.Xml.Serialization;

namespace DaocSpellCraftCalculator.Models
{
    [XmlRoot("ToaBonus")]
    public class ToaBonusModel : ElementModel
    {

        private int _LevelDividedBy;
        public int LevelDividedBy
        {
            get { return _LevelDividedBy; }
            set { if (_LevelDividedBy != value) { _LevelDividedBy = value; RaisePropertyChanged("LevelDividedBy"); } }
        }
		
    }


    public class ToaBonusModelList
    {

        [XmlElement("ToaBonus")]
        public List<ToaBonusModel> ToABonuses { get; set; }
    }
}
