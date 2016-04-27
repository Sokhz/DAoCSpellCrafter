using DaocSpellCraftCalculator.Models;
using DaocSpellCraftCalculator.Tools;
using GalaSoft.MvvmLight;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Serialization;

namespace DaocSpellCraftCalculator.ViewModels
{
    [XmlRoot("Bonus")]
    public class BonusItemViewModel : ViewModelBase
    {

        #region Constructor

        public BonusItemViewModel()
        {
            this.LstCraftBonuses = new ObservableCollection<CraftBonusModel>();
            this.TypeBonus = "";
            this.Value = null;
            this.Bonus = "";
            this.LstMaterials = new List<MaterialModel>();
            this.InitTypeBonuses();
        }

        #endregion


        #region Properties


        private bool _IsLoot;
        [XmlElement("Loot")]
        public bool IsLoot
        {
            get { return _IsLoot; }
            set { if (_IsLoot != value) { _IsLoot = value; RaisePropertyChanged("IsLoot"); RaisePropertyChanged("IsNotLoot"); } InitTypeBonuses(); }
        }
        [XmlIgnore]
        public bool IsNotLoot
        {
            get { return !_IsLoot; }
        }
        private bool _IsFifthBonus;
        public bool IsFifthBonus
        {
            get { return _IsFifthBonus; }
            set { if (_IsFifthBonus != value) { _IsFifthBonus = value; RaisePropertyChanged("IsFifthBonus"); } }
        }



        private ObservableCollection<TypeBonusModel> _LstTypeBonuses;
        [XmlIgnore]
        public ObservableCollection<TypeBonusModel> LstTypeBonuses
        {
            get { return _LstTypeBonuses; }
            set { if (_LstTypeBonuses != value) { _LstTypeBonuses = value; RaisePropertyChanged("LstTypeBonuses"); } }
        }

        private string _TypeBonus;
        public string TypeBonus
        {
            get { return _TypeBonus; }
            set { if (_TypeBonus != value) { _TypeBonus = value; RaisePropertyChanged("TypeBonus"); OnTypeBonusChanged(); RaisePropertyChanged("IsTypeSelected"); RaisePropertyChanged("IsValueEnabled"); RaisePropertyChanged("IsBonusEnabled"); } }
        }
        public bool ShouldSerializeTypeBonus()
        {
            return !string.IsNullOrEmpty(this.TypeBonus);
        }

        [XmlIgnore]
        public bool IsTypeSelected
        {
            get { return !string.IsNullOrEmpty(_TypeBonus); }
        }


        private ObservableCollection<CraftBonusModel> _LstCraftBonuses;
        [XmlIgnore]
        public ObservableCollection<CraftBonusModel> LstCraftBonuses
        {
            get { return _LstCraftBonuses; }
            set { if (_LstCraftBonuses != value) { _LstCraftBonuses = value; RaisePropertyChanged("LstCraftBonuses"); } }
        }

        private int? _Value;
        public int? Value
        {
            get { return _Value; }
            set { if (_Value != value) { _Value = value; RaisePropertyChanged("Value"); } }
        }
        public bool ShouldSerializeValue()
        {
            return Value.HasValue && Value != 0;
        }


        private ObservableCollection<ElementModel> _LstBonuses;
        [XmlIgnore]
        public ObservableCollection<ElementModel> LstBonuses
        {
            get { return _LstBonuses; }
            set { if (_LstBonuses != value) { _LstBonuses = value; RaisePropertyChanged("LstBonuses"); } }
        }

        private string _Bonus;
        public string Bonus
        {
            get { return _Bonus; }
            set { if (_Bonus != value) { _Bonus = value; RaisePropertyChanged("Bonus"); OnBonusChanged(); RaisePropertyChanged("IsValueEnabled"); } }
        }
        public bool ShouldSerializeBonus()
        {
            return !string.IsNullOrEmpty(this.Bonus);
        }
        [XmlIgnore]
        public bool IsValueEnabled
        {
            get { return this.IsLoot || !this.IsAllSkillCraft; }
        }
        [XmlIgnore]
        public bool IsAllSkillCraft
        {
            get { return !this.IsLoot && (this.Bonus == "MELEE" || this.Bonus == "MAGIC" || this.Bonus == "RANGE" || this.Bonus == "DUAL"); }
        }
        [XmlIgnore]
        public bool IsBonusEnabled
        {
            get { return this.TypeBonus != "HP" && this.TypeBonus != "MAN" && this.TypeBonus != "FAT"; }
        }

        [XmlIgnore]
        public string FullDescription
        {
            get
            {
                return this.GetDescription();
            }
        }

        [XmlIgnore]
        public bool IsDefined
        {
            get
            {
                return !string.IsNullOrEmpty(this.TypeBonus) && this.Value.HasValue && Value != 0 && (this.TypeBonus == "HP" || this.TypeBonus == "MAN" || this.TypeBonus == "FAT" || !string.IsNullOrEmpty(this.Bonus));
            }
        }
        [XmlIgnore]
        public bool IsPartialDefined
        {
            get
            {
                return !string.IsNullOrEmpty(this.TypeBonus) && this.Value.HasValue && Value != 0;
            }
        }
        public bool MustUpdate
        {
            get { return this.IsPartialDefined || string.IsNullOrEmpty(this.TypeBonus); }
        }

        [XmlIgnore]
        public string GemNameReport
        {
            get { return this.GetGemName(); }
        }

        [XmlIgnore]
        public string GemCode
        {
            get { return this.GetGemCode(); }
        }

        [XmlIgnore]
        public string GemMaterials
        {
            get { return this.GetMaterials(); }
        }

        private List<MaterialModel> _LstMaterials;
        public List<MaterialModel> LstMaterials
        {
            get { return _LstMaterials; }
            set { if (_LstMaterials != value) { _LstMaterials = value; RaisePropertyChanged("LstMaterials"); } }
        }


        #endregion


        #region Methods

        public string GetDescription()
        {
            var retour = "";
            var type = DataProvider.Current.TypeBonuses.FirstOrDefault(o => o.Code == this.TypeBonus);
            if (this.Value.HasValue && type != null)
            {
                retour = this.Value.Value.ToString();
                switch (this.TypeBonus)
                {
                    case "STA":
                    case "HP":
                    case "MAN":
                    case "CAPS":
                    case "CAPMS":
                    case "CAPSC":
                        if (!string.IsNullOrEmpty(this.Bonus))
                        {
                            var carac = DataProvider.Current.Caracs.FirstOrDefault(o => o.Code == this.Bonus);
                            if (carac != null)
                            {
                                retour += " " + carac.Full;
                                retour += " " + type.Full;
                            }
                        }
                        else if (this.TypeBonus == "HP" || this.TypeBonus == "MAN")
                        {
                            retour += " " + type.Full;
                        }
                        else
                            return "";
                        break;
                    case "SKI":
                        var skill = DataProvider.Current.Skills.FirstOrDefault(o => o.Code == this.Bonus);
                        if (skill != null)
                        {
                            retour += " " + skill.Full;
                            retour += " " + type.Full;
                        }
                        else
                            return "";
                        break;
                    case "RES":
                    case "CAPR":
                    case "CAPRC":
                        var resist = DataProvider.Current.Resists.FirstOrDefault(o => o.Code == this.Bonus);
                        if (resist != null)
                        {
                            retour += " " + resist.Full;
                            retour += " " + type.Full;
                        }
                        else
                            return "";
                        break;
                    case "OTH":
                        var bonus = DataProvider.Current.ToaBonuses.FirstOrDefault(o => o.Code == this.Bonus);
                        if (bonus != null)
                            retour += " " + bonus.Full;
                        else
                            return "";
                        break;
                    case "MYTH":
                        var myth = DataProvider.Current.MythicalBonuses.FirstOrDefault(o => o.Code == this.Bonus);
                        if (myth != null)
                            retour += " " + myth.Full;
                        else
                            return "";
                        break;
                }
            }

            return retour;
        }

        public bool IsEquivalent(BonusItemViewModel bonus)
        {
            return !string.IsNullOrEmpty(this.TypeBonus) && bonus.TypeBonus == this.TypeBonus && !string.IsNullOrEmpty(this.Bonus) && bonus.Bonus == this.Bonus;
        }

        private string GetGemName()
        {
            var retour = "";
            if (this.IsNotLoot && this.IsDefined && !this.IsFifthBonus)
            {
                CraftBonusModel craftbonus = null;
                GemQualityModel gemQuality = null;
                CaracModel carac = null;
                ResistModel resist = null;
                SkillModel skill = null;
                var main = ServiceLocator.Current.GetInstance<MainViewModel>();
                var realm = DataProvider.Current.Realms.FirstOrDefault(o => o.Code == main.SelectedTemplate.Realm);

                craftbonus = DataProvider.Current.CraftBonuses.FirstOrDefault(o => o.Stat == this.TypeBonus && o.Value == this.Value);
                if (craftbonus != null)
                {
                    gemQuality = DataProvider.Current.LstGemQualities.FirstOrDefault(o => o.Code == craftbonus.GemQuality);
                    if (gemQuality != null)
                    {
                        if (!string.IsNullOrEmpty(this.Bonus) && this.TypeBonus == "STA")
                        {
                            carac = DataProvider.Current.Caracs.FirstOrDefault(o => o.Code == this.Bonus);
                            if (carac != null)
                                retour = carac.GemName.ToLower();
                        }
                        else if (!string.IsNullOrEmpty(this.Bonus) && this.TypeBonus == "RES")
                        {
                            resist = DataProvider.Current.Resists.FirstOrDefault(o => o.Code == this.Bonus);
                            if (resist != null)
                                retour = resist.GemName.ToLower();

                        }
                        else if (!string.IsNullOrEmpty(this.Bonus) && this.TypeBonus == "SKI")
                        {
                            skill = DataProvider.Current.Skills.FirstOrDefault(o => o.Code == this.Bonus);
                            if (skill != null)
                                retour = skill.GemNames.FirstOrDefault(o => o.Realm == "ALL" || o.Realm == realm.Code).Name.ToLower();

                        }
                        else if (this.TypeBonus == "HP" || this.TypeBonus == "MAN")
                        {
                            carac = DataProvider.Current.Caracs.FirstOrDefault(o => o.Code == this.TypeBonus);
                            if (carac != null)
                                retour = carac.GemName.ToLower();
                        }

                        retour = " - " + gemQuality.Full + " " + retour;
                    }
                }
            }
            return retour;
        }

        private string GetGemCode()
        {
            var retour = "";
            if (this.IsNotLoot && this.IsDefined && !this.IsFifthBonus)
            {
                CraftBonusModel craftbonus = null;
                GemQualityModel gemQuality = null;
                CaracModel carac = null;
                ResistModel resist = null;
                SkillModel skill = null;
                var main = ServiceLocator.Current.GetInstance<MainViewModel>();
                var realm = DataProvider.Current.Realms.FirstOrDefault(o => o.Code == main.SelectedTemplate.Realm);

                craftbonus = DataProvider.Current.CraftBonuses.FirstOrDefault(o => o.Stat == this.TypeBonus && o.Value == this.Value);
                if (craftbonus != null)
                {
                    gemQuality = DataProvider.Current.LstGemQualities.FirstOrDefault(o => o.Code == craftbonus.GemQuality);
                    if (gemQuality != null)
                    {
                        if (!string.IsNullOrEmpty(this.Bonus) && this.TypeBonus == "STA")
                        {
                            carac = DataProvider.Current.Caracs.FirstOrDefault(o => o.Code == this.Bonus);
                            if (carac != null)
                                retour = carac.GemCode.ToLower();
                        }
                        else if (!string.IsNullOrEmpty(this.Bonus) && this.TypeBonus == "RES")
                        {
                            resist = DataProvider.Current.Resists.FirstOrDefault(o => o.Code == this.Bonus);
                            if (resist != null)
                                retour = resist.GemCode.ToLower();
                        }
                        else if (!string.IsNullOrEmpty(this.Bonus) && this.TypeBonus == "SKI")
                        {
                            skill = DataProvider.Current.Skills.FirstOrDefault(o => o.Code == this.Bonus);
                            if (skill != null)
                                retour = skill.GemNames.FirstOrDefault(o => o.Realm == "ALL" || o.Realm == realm.Code).Code.ToLower();
                        }
                        else if (this.TypeBonus == "HP" || this.TypeBonus == "MAN")
                        {
                            carac = DataProvider.Current.Caracs.FirstOrDefault(o => o.Code == this.TypeBonus);
                            if (carac != null)
                            {
                                var code = carac.GemCode.Split(',');
                                var realmCode = code.FirstOrDefault(o => o.Contains(realm.Code));
                                if (!string.IsNullOrEmpty(realmCode))
                                    retour = realmCode.Split(':')[1].ToLower();
                            }
                        }

                        retour = retour + gemQuality.Macro;
                    }
                }
            }
            return retour;
        }

        private string GetMaterials()
        {
            var retour = "";
            this.LstMaterials.Clear();
            if (this.IsNotLoot && this.IsDefined && !this.IsFifthBonus)
            {
                CraftBonusModel craftbonus = null;
                GemQualityModel gemQuality = null;
                CaracModel carac = null;
                ResistModel resist = null;
                SkillModel skill = null;
                var main = ServiceLocator.Current.GetInstance<MainViewModel>();
                var realm = DataProvider.Current.Realms.FirstOrDefault(o => o.Code == main.SelectedTemplate.Realm);
                var type = DataProvider.Current.TypeBonuses.FirstOrDefault(o => o.Code == this.TypeBonus);
                craftbonus = DataProvider.Current.CraftBonuses.FirstOrDefault(o => o.Stat == this.TypeBonus && o.Value == this.Value);

                if (craftbonus != null && type != null)
                {
                    gemQuality = DataProvider.Current.LstGemQualities.FirstOrDefault(o => o.Code == craftbonus.GemQuality);
                    if (gemQuality != null)
                    {
                        if (!string.IsNullOrEmpty(this.Bonus) && this.TypeBonus == "STA")
                        {
                            carac = DataProvider.Current.Caracs.FirstOrDefault(o => o.Code == this.Bonus);
                            if (carac != null)
                            {
                                retour = "1 " + DataProvider.Current.LstGems.FirstOrDefault(o => o.Code == gemQuality.Gem).Full + Environment.NewLine;
                                this.LstMaterials.Add(new MaterialModel() { Quantity = 1, Material = DataProvider.Current.LstGems.FirstOrDefault(o => o.Code == gemQuality.Gem), Type = 1 });
                                retour += "\t" + craftbonus.NbDusts + " " + DataProvider.Current.LstDusts.FirstOrDefault(o => o.Code == type.Dust).Full + Environment.NewLine;
                                this.LstMaterials.Add(new MaterialModel() { Quantity = craftbonus.NbDusts, Material = DataProvider.Current.LstDusts.FirstOrDefault(o => o.Code == type.Dust), Type = 2 });
                                retour += "\t" + craftbonus.NbAgents + " " + DataProvider.Current.LstAgents.FirstOrDefault(o => o.Code == carac.Agent).Full + Environment.NewLine;
                                this.LstMaterials.Add(new MaterialModel() { Quantity = craftbonus.NbAgents, Material = DataProvider.Current.LstAgents.FirstOrDefault(o => o.Code == carac.Agent), Type = 3 });
                            }
                        }
                        else if (!string.IsNullOrEmpty(this.Bonus) && this.TypeBonus == "RES")
                        {
                            resist = DataProvider.Current.Resists.FirstOrDefault(o => o.Code == this.Bonus);
                            if (resist != null)
                            {
                                retour = "1 " + DataProvider.Current.LstGems.FirstOrDefault(o => o.Code == gemQuality.Gem).Full + Environment.NewLine;
                                this.LstMaterials.Add(new MaterialModel() { Quantity = 1, Material = DataProvider.Current.LstGems.FirstOrDefault(o => o.Code == gemQuality.Gem), Type = 1 });
                                retour += "\t" + craftbonus.NbDusts + " " + DataProvider.Current.LstDusts.FirstOrDefault(o => o.Code == type.Dust).Full + Environment.NewLine;
                                this.LstMaterials.Add(new MaterialModel() { Quantity = craftbonus.NbDusts, Material = DataProvider.Current.LstDusts.FirstOrDefault(o => o.Code == type.Dust), Type = 2 });
                                retour += "\t" + craftbonus.NbAgents + " " + DataProvider.Current.LstAgents.FirstOrDefault(o => o.Code == resist.Agent).Full + Environment.NewLine;
                                this.LstMaterials.Add(new MaterialModel() { Quantity = craftbonus.NbAgents, Material = DataProvider.Current.LstAgents.FirstOrDefault(o => o.Code == resist.Agent), Type = 3 });
                            }
                        }
                        else if (!string.IsNullOrEmpty(this.Bonus) && this.TypeBonus == "SKI")
                        {
                            skill = DataProvider.Current.Skills.FirstOrDefault(o => o.Code == this.Bonus);
                            if (skill != null && skill.GemNames.Count == 1)
                            {
                                retour = "1 " + DataProvider.Current.LstGems.FirstOrDefault(o => o.Code == gemQuality.Gem).Full + Environment.NewLine;
                                this.LstMaterials.Add(new MaterialModel() { Quantity = 1, Material = DataProvider.Current.LstGems.FirstOrDefault(o => o.Code == gemQuality.Gem), Type = 1 });
                                retour += "\t" + craftbonus.NbDusts + " " + DataProvider.Current.LstDusts.FirstOrDefault(o => o.Code == skill.GemNames.FirstOrDefault(s => s.Realm == "ALL").Dust).Full + Environment.NewLine;
                                this.LstMaterials.Add(new MaterialModel() { Quantity = craftbonus.NbDusts, Material = DataProvider.Current.LstDusts.FirstOrDefault(o => o.Code == skill.GemNames.FirstOrDefault(s => s.Realm == "ALL").Dust), Type = 2 });
                                retour += "\t" + craftbonus.NbAgents + " " + DataProvider.Current.LstAgents.FirstOrDefault(o => o.Code == skill.GemNames.FirstOrDefault(s => s.Realm == "ALL").Agent).Full + Environment.NewLine;
                                this.LstMaterials.Add(new MaterialModel() { Quantity = craftbonus.NbAgents, Material = DataProvider.Current.LstAgents.FirstOrDefault(o => o.Code == skill.GemNames.FirstOrDefault(s => s.Realm == "ALL").Agent), Type = 3 });
                            }
                            else if (skill != null)
                            {
                                retour = "1 " + DataProvider.Current.LstGems.FirstOrDefault(o => o.Code == gemQuality.Gem).Full + Environment.NewLine;
                                this.LstMaterials.Add(new MaterialModel() { Quantity = 1, Material = DataProvider.Current.LstGems.FirstOrDefault(o => o.Code == gemQuality.Gem), Type = 1 });
                                retour += "\t" + "1 " + DataProvider.Current.LstDusts.FirstOrDefault(o => o.Code == skill.GemNames.FirstOrDefault(s => s.Realm == realm.Code).Dust).Full + Environment.NewLine;
                                this.LstMaterials.Add(new MaterialModel() { Quantity = 1, Material = DataProvider.Current.LstDusts.FirstOrDefault(o => o.Code == skill.GemNames.FirstOrDefault(s => s.Realm == realm.Code).Dust), Type = 2 });
                                foreach (var codeAgent in skill.GemNames.FirstOrDefault(o => o.Realm == realm.Code).Agent.Split(';'))
                                {
                                    retour += "\t" + "1 " + DataProvider.Current.LstAgents.FirstOrDefault(o => o.Code == codeAgent).Full + Environment.NewLine;
                                    this.LstMaterials.Add(new MaterialModel() { Quantity = 1, Material = DataProvider.Current.LstAgents.FirstOrDefault(o => o.Code == codeAgent), Type = 3 });
                                }
                            }

                        }
                        else if (this.TypeBonus == "HP" || this.TypeBonus == "MAN")
                        {
                            carac = DataProvider.Current.Caracs.FirstOrDefault(o => o.Code == this.TypeBonus);
                            if (carac != null)
                            {
                                retour = "1 " + DataProvider.Current.LstGems.FirstOrDefault(o => o.Code == gemQuality.Gem).Full + Environment.NewLine;
                                this.LstMaterials.Add(new MaterialModel() { Quantity = 1, Material = DataProvider.Current.LstGems.FirstOrDefault(o => o.Code == gemQuality.Gem), Type = 1 });
                                retour += "\t" + craftbonus.NbDusts + " " + DataProvider.Current.LstDusts.FirstOrDefault(o => o.Code == type.Dust).Full + Environment.NewLine;
                                this.LstMaterials.Add(new MaterialModel() { Quantity = craftbonus.NbDusts, Material = DataProvider.Current.LstDusts.FirstOrDefault(o => o.Code == type.Dust), Type = 2 });
                                retour += "\t" + craftbonus.NbAgents + " " + DataProvider.Current.LstAgents.FirstOrDefault(o => o.Code == type.Agent).Full + Environment.NewLine;
                                this.LstMaterials.Add(new MaterialModel() { Quantity = craftbonus.NbAgents, Material = DataProvider.Current.LstAgents.FirstOrDefault(o => o.Code == type.Agent), Type = 3 });
                            }
                        }
                    }
                    retour = this.GemNameReport + " : \n\t" + retour;
                }
            }
            return retour;
        }

        public void UpdateSkills()
        {
            if (this.TypeBonus == "SKI")
            {
                if (!this.IsLoot && ServiceLocator.Current.GetInstance<MainViewModel>().SelectedTemplate != null)
                {
                    var codeClasse = ServiceLocator.Current.GetInstance<MainViewModel>().SelectedTemplate.Class;
                    this.LstBonuses = new ObservableCollection<ElementModel>(DataProvider.Current.Skills.Where(o => o.LstClasses.Contains(codeClasse) || (o.LstClasses.Count == 0 && o.GemNames.Count > 0)));
                }
                else if (ServiceLocator.Current.GetInstance<MainViewModel>().SelectedTemplate != null)
                {
                    var realm = ServiceLocator.Current.GetInstance<MainViewModel>().SelectedTemplate.Realm;
                    this.LstBonuses = new ObservableCollection<ElementModel>(DataProvider.Current.Skills.Where(o => o.LstClasses.Count == 0 || (o.LstClassesFull.Count > 0 && o.LstClassesFull.SelectMany(c => c.LstRealms).Contains(realm))));
                }
                else
                    this.LstBonuses = new ObservableCollection<ElementModel>(DataProvider.Current.Skills);
            }
        }

        #endregion


        #region Events

        private void InitTypeBonuses()
        {
            var types = DataProvider.Current.TypeBonuses;
            if (!this.IsLoot)
                types = types.Where(o => o.Craft).ToList();
            this.LstTypeBonuses = new ObservableCollection<TypeBonusModel>(types);
        }

        public void OnTypeBonusChanged()
        {
            this.LstCraftBonuses.Clear();
            if (!string.IsNullOrEmpty(this.TypeBonus))
            {

                if (!this.IsLoot)
                {
                    var values = DataProvider.Current.CraftBonuses.Where(o => o.Stat == this.TypeBonus);
                    this.LstCraftBonuses = new ObservableCollection<CraftBonusModel>(values);
                }

                switch (this.TypeBonus)
                {
                    case "STA":
                        if (this.IsLoot)
                            this.LstBonuses = new ObservableCollection<ElementModel>(DataProvider.Current.Caracs.Where(o => o.OnLoot));
                        else
                            this.LstBonuses = new ObservableCollection<ElementModel>(DataProvider.Current.Caracs.Where(o => o.Craft));
                        break;
                    case "CAPSC":
                    case "CAPMS":
                    case "CAPS":
                        this.LstBonuses = new ObservableCollection<ElementModel>(DataProvider.Current.Caracs);
                        break;
                    case "RES":
                    case "CAPR":
                    case "CAPRC":
                        this.LstBonuses = new ObservableCollection<ElementModel>(DataProvider.Current.Resists);
                        break;
                    case "SKI":
                        if (!this.IsLoot && ServiceLocator.Current.GetInstance<MainViewModel>().SelectedTemplate != null)
                        {
                            var codeClasse = ServiceLocator.Current.GetInstance<MainViewModel>().SelectedTemplate.Class;
                            this.LstBonuses = new ObservableCollection<ElementModel>(DataProvider.Current.Skills.Where(o => o.LstClasses.Contains(codeClasse) || (o.LstClasses.Count == 0 && o.GemNames.Count > 0)));
                        }
                        else if (ServiceLocator.Current.GetInstance<MainViewModel>().SelectedTemplate != null)
                        {
                            var realm = ServiceLocator.Current.GetInstance<MainViewModel>().SelectedTemplate.Realm;
                            this.LstBonuses = new ObservableCollection<ElementModel>(DataProvider.Current.Skills.Where(o => o.LstClasses.Count == 0 || (o.LstClassesFull.Count > 0 && o.LstClassesFull.SelectMany(c => c.LstRealms).Contains(realm))));
                        }
                        else
                            this.LstBonuses = new ObservableCollection<ElementModel>(DataProvider.Current.Skills);
                        break;
                    case "OTH":
                        this.LstBonuses = new ObservableCollection<ElementModel>(DataProvider.Current.ToaBonuses);
                        break;
                    case "MYTH":
                        this.LstBonuses = new ObservableCollection<ElementModel>(DataProvider.Current.MythicalBonuses);
                        break;
                    default:
                        this.LstBonuses = new ObservableCollection<ElementModel>();
                        break;
                }
            }
            else
            {
                this.Value = null;
                this.Bonus = "";
                this.LstCraftBonuses = new ObservableCollection<CraftBonusModel>();
            }
        }

        private void OnBonusChanged()
        {
            if (this.IsAllSkillCraft)
            {
                this.Value = 1;
            }
        }

        #endregion

    }

}
