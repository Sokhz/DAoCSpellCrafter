using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace DaocSpellCraftCalculator.Models
{
    public class ElementValueModel : ViewModelBase
    {
        private string _Type;
        public string Type
        {
            get { return _Type; }
            set { if (_Type != value) { _Type = value; RaisePropertyChanged("Type"); } }
        }
        
        private string _Code;
        public string Code
        {
            get { return _Code; }
            set { if (_Code != value) { _Code = value; RaisePropertyChanged("Code"); } }
        }

        private int _Value;
        public int Value
        {
            get { return _Value; }
            set { if (_Value != value) { _Value = value; RaisePropertyChanged("Value"); } }
        }
    }
}
