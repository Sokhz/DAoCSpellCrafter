using DaocSpellCraftCalculator.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DaocSpellCraftCalculator.Models
{
    public class UseItemModel : ElementModel
    {

        private string _Duration;
        public string Duration
        {
            get { return _Duration; }
            set { if (_Duration != value) { _Duration = value; RaisePropertyChanged("Duration"); } }
        }

        private string _Value;
        public string Value
        {
            get { return _Value; }
            set { if (_Value != value) { _Value = value; RaisePropertyChanged("Value"); } }
        }

        private string _Timer;
        public string Timer
        {
            get { return _Timer; }
            set { if (_Timer != value) { _Timer = value; RaisePropertyChanged("Timer"); } }
        }

        private string _Description;
        public string Description
        {
            get { return _Description; }
            set { if (_Description != value) { _Description = value; RaisePropertyChanged("Description"); } }
        }

        private int? _Level;
        public int? Level
        {
            get { return _Level; }
            set { if (_Level != value) { _Level = value; RaisePropertyChanged("Level"); } }
        }

        private string _EffectType;
        public string EffectType
        {
            get { return _EffectType; }
            set { if (_EffectType != value) { _EffectType = value; RaisePropertyChanged("EffectType"); } }
        }

        public string FullDescription
        {
            get { return this.GetFullDescription(); }
        }

        public string ShortDescription
        {
            get { return this.GetShortDescription(); }
        }

        public string ReportDescription
        {
            get { return this.GetReportDescription(); }
        }

        private ItemViewModel _Item;
        public ItemViewModel Item
        {
            get { return _Item; }
            set { if (_Item != value) { _Item = value; RaisePropertyChanged("Item"); } }
        }

        private string GetFullDescription()
        {
            var retour = this.Full;
            if (!string.IsNullOrEmpty(this.Description))
                retour += " : " + this.Description;
            if (!string.IsNullOrEmpty(this.Value))
                retour += " / Value : " + this.Value;
            if (!string.IsNullOrEmpty(this.Duration))
                retour += " / Duration : " + this.Duration;
            if (!string.IsNullOrEmpty(this.Timer))
                retour += " / Timer : " + this.Timer;
            if (this.Level.HasValue)
                retour += " / Level : " + this.Level.Value;

            return retour;
        }
        private string GetShortDescription()
        {
            var retour = this.Full;
            if (!string.IsNullOrEmpty(this.Value))
                retour += " / Value : " + this.Value;
            if (!string.IsNullOrEmpty(this.Duration))
                retour += " / Duration : " + this.Duration;

            return retour;
        }

        private string GetReportDescription()
        {
            var retour = "";
            if (!string.IsNullOrEmpty(this.Description))
                retour +=  " : " + this.Description;
            if (!string.IsNullOrEmpty(this.EffectType))
                retour += " (value : " + this.Value + ")";

            return retour;
        }




        public UseItemModel GetClone()
        {
            var retour = new UseItemModel();
            retour.Code = this.Code;
            retour.Full = this.Full;
            retour.Duration = this.Duration;
            retour.Value = this.Value;
            retour.Timer = this.Timer;
            retour.Level = this.Level;
            retour.Description = this.Description;
            retour.EffectType = this.EffectType;
            retour.Item = null;
            return retour;
        }

    }

    public class UseItemModelList
    {
        [XmlElement("UseItem")]
        public List<UseItemModel> UseItems { get; set; }
    }
}
