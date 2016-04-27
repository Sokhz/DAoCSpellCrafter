using System.Collections.Generic;
using System.Xml.Serialization;

namespace DaocSpellCraftCalculator.Models
{
    [XmlRoot("Armor")]
    public class ArmorModel : SlotModel
    {

    }


    public class ArmorModelList
    {

        [XmlElement("Armor")]
        public List<ArmorModel> Armors { get; set; }
    }
}
