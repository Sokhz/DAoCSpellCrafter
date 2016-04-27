using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaocSpellCraftCalculator.ViewModels
{
    class DetailBonusViewModel : ViewModelBase
    {

        public DetailBonusViewModel(TemplateViewModel template, string typeBonus, string bonus)
        {
            this.Template = template;
            this.Bonus = bonus;
            this.TypeBonus = typeBonus;
            this.GetDetailBonus();
        }


        private TemplateViewModel _Template;
        public TemplateViewModel Template
        {
            get { return _Template; }
            set { if (_Template != value) { _Template = value; RaisePropertyChanged("Template"); } }
        }

        private string _TypeBonus;
        public string TypeBonus
        {
            get { return _TypeBonus; }
            set { if (_TypeBonus != value) { _TypeBonus = value; RaisePropertyChanged("TypeBonus"); } }
        }


        private string _Bonus;
        public string Bonus
        {
            get { return _Bonus; }
            set { if (_Bonus != value) { _Bonus = value; RaisePropertyChanged("Bonus"); } }
        }


        private List<ItemViewModel> _LstItems;
        public List<ItemViewModel> LstItems
        {
            get { return _LstItems; }
            set { if (_LstItems != value) { _LstItems = value; RaisePropertyChanged("LstItems"); } }
        }



        private void GetDetailBonus()
        {
            if (!string.IsNullOrEmpty(this.TypeBonus) && !string.IsNullOrEmpty(this.Bonus) && this.Template != null)
            {
                this.LstItems = this.Template.LstEquipedItems.Where(o => o.GetBonus(this.TypeBonus, this.Bonus).Count > 0).ToList();
            }
        }



    }
}
