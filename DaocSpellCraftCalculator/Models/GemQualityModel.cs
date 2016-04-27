using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DaocSpellCraftCalculator.Models
{
    public class GemQualityModel : ElementModel
    {

        private string _Macro;
        public string Macro
        {
            get { return _Macro; }
            set { if (_Macro != value) { _Macro = value; RaisePropertyChanged("Macro"); } }
        }

        private string _Gem;
        public string Gem
        {
            get { return _Gem; }
            set { if (_Gem != value) { _Gem = value; RaisePropertyChanged("Gem"); } }
        }
		
    }


    public class GemQualityModelList
    {

        [XmlElement("GemQuality")]
        public List<GemQualityModel> GemQualities { get; set; }
    }
}
