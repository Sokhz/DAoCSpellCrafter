using System.Collections.Generic;
using System.Xml.Serialization;

namespace DaocSpellCraftCalculator.Models
{
    [XmlRoot("Realm")]
    public class RealmModel : ElementModel
    {
    }

    public class RealmModelList
    {
        [XmlElement("Realm")]
        public List<RealmModel> Realms { get; set; }
    }
}
