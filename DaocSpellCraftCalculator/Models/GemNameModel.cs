using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DaocSpellCraftCalculator.Models
{
    public class GemNameModel : ViewModelBase
    {
        private string _Name;
        [XmlText]
        public string Name
        {
            get { return _Name; }
            set { if (_Name != value) { _Name = value; RaisePropertyChanged("Name"); } }
        }

        private string _Realm;
        [XmlAttribute]
        public string Realm
        {
            get { return _Realm; }
            set { if (_Realm != value) { _Realm = value; RaisePropertyChanged("Realm"); } }
        }

        private string _Code;
        public string Code
        {
            get { return _Code; }
            set { if (_Code != value) { _Code = value; RaisePropertyChanged("Code"); } }
        }

        private string _Agent;
        [XmlAttribute]
        public string Agent
        {
            get { return _Agent; }
            set { if (_Agent != value) { _Agent = value; RaisePropertyChanged("Agent"); } }
        }

        private string _Dust;
        [XmlAttribute]
        public string Dust
        {
            get { return _Dust; }
            set { if (_Dust != value) { _Dust = value; RaisePropertyChanged("Dust"); } }
        }
		
		

    }
}
