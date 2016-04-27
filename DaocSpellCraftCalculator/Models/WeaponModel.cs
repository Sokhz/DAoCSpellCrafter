using System.Collections.Generic;
using System.Xml.Serialization;

namespace DaocSpellCraftCalculator.Models
{
    [XmlRoot("Weapon")]
    public class WeaponModel : SlotModel
    {


    }


    public class WeaponModelList
    {

        [XmlElement("Weapon")]
        public List<WeaponModel> Weapons { get; set; }
    }
}
