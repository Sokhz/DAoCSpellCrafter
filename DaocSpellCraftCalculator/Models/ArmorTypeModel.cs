using System.Collections.Generic;
using System.Xml.Serialization;

namespace DaocSpellCraftCalculator.Models
{
    [XmlRoot("ArmorType")]
    public class ArmorTypeModel : ElementModel
    {

    }

    public class ArmorTypeModelList
    {

        [XmlElement("ArmorType")]
        public List<ArmorTypeModel> ArmorTypes { get; set; }
    }
}
