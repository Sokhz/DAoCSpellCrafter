using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DaocSpellCraftCalculator.Models
{
    [XmlRoot("Gem")]
    public class GemModel : ElementModel
    {
    }

    public class GemModelList
    {

        [XmlElement("Gem")]
        public List<GemModel> Gems { get; set; }
    }

    [XmlRoot("Agent")]
    public class AgentModel : ElementModel
    {
    }

    public class AgentModelList
    {

        [XmlElement("Agent")]
        public List<AgentModel> Agents { get; set; }
    }

    [XmlRoot("Dust")]
    public class DustModel : ElementModel
    {
    }

    public class DustModelList
    {

        [XmlElement("Dust")]
        public List<DustModel> Dusts { get; set; }
    }



    public class MaterialModel
    {

        public int Quantity { get; set; }
        public ElementModel Material { get; set; }
        public int Type { get; set; }
    }
}
