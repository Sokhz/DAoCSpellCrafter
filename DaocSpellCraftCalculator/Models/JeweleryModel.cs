using System.Collections.Generic;
using System.Xml.Serialization;

namespace DaocSpellCraftCalculator.Models
{
    [XmlRoot("Jewelery")]
    public class JeweleryModel : SlotModel
    {

		
    }

    public class JeweleryModelList
    {

        [XmlElement("Jewelery")]
        public List<JeweleryModel> Jeweleries { get; set; }
    }
}
