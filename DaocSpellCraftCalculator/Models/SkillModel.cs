using System.Collections.Generic;
using System.Xml.Serialization;
using System.Linq;
using DaocSpellCraftCalculator.Tools;

namespace DaocSpellCraftCalculator.Models
{
    public class SkillModel : ElementModel
    {
        private string _Type;
        public string Type
        {
            get { return _Type; }
            set { if (_Type != value) { _Type = value; RaisePropertyChanged("Type"); } }
        }

        private List<string> _LstClasses;
        [XmlArray(ElementName = "Classes")]
        [XmlArrayItem(ElementName = "Class")]
        public List<string> LstClasses
        {
            get { return _LstClasses; }
            set { if (_LstClasses != value) { _LstClasses = value; RaisePropertyChanged("LstClasses"); } }
        }

        public List<ClassModel> LstClassesFull
        {
            get { return this.LstClasses.Select(o => DataProvider.Current.Classes.FirstOrDefault(c => c.Code == o)).ToList(); }
        }

        private List<GemNameModel> _GemNames;
        [XmlElement("GemName", typeof(GemNameModel))]
        public List<GemNameModel> GemNames
        {
            get { return _GemNames; }
            set { if (_GemNames != value) { _GemNames = value; RaisePropertyChanged("GemNames"); } }
        }

    }



    public class SkillModelList
    {

        [XmlElement("Skill")]
        public List<SkillModel> Skills { get; set; }
    }
}
