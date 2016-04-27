using DaocSpellCraftCalculator.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace DaocSpellCraftCalculator.Tools
{
    public class DataProvider
    {

        #region Constructor


        #endregion


        #region Properties

        private static DataProvider _current;
        public static DataProvider Current
        {
            get
            {
                return _current ?? (_current = new DataProvider());
            }
        }

        private List<RealmModel> _Realms;
        public List<RealmModel> Realms
        {
            get { return _Realms ?? (_Realms = this.InitRealms()); }
        }

        private List<ClassModel> _Classes;
        public List<ClassModel> Classes
        {
            get { return _Classes ?? (_Classes = this.InitClasses()); }
        }

        private List<RaceModel> _Races;
        public List<RaceModel> Races
        {
            get { return _Races ?? (_Races = this.InitRaces()); }
        }

        private List<ArmorModel> _Armors;
        public List<ArmorModel> Armors
        {
            get { return _Armors ?? (_Armors = this.InitArmors()); }
        }

        private List<JeweleryModel> _Jeweleries;
        public List<JeweleryModel> Jeweleries
        {
            get { return _Jeweleries ?? (_Jeweleries = this.InitJeweleries()); }
        }

        private List<ResistModel> _Resists;
        public List<ResistModel> Resists
        {
            get { return _Resists ?? (_Resists = this.InitResists()); }
        }

        private List<CaracModel> _Caracs;
        public List<CaracModel> Caracs
        {
            get { return _Caracs ?? (_Caracs = this.InitCaracs()); }
        }

        private List<WeaponModel> _Weapons;
        public List<WeaponModel> Weapons
        {
            get { return _Weapons ?? (_Weapons = this.InitWeapons()); }
        }

        private List<SkillModel> _Skills;
        public List<SkillModel> Skills
        {
            get { return _Skills ?? (_Skills = this.InitSkills()); }
        }

        private List<TypeBonusModel> _TypeBonuses;
        public List<TypeBonusModel> TypeBonuses
        {
            get { return _TypeBonuses ?? (_TypeBonuses = this.InitTypeBonuses()); }
        }

        private List<CraftBonusModel> _CraftBonuses;
        public List<CraftBonusModel> CraftBonuses
        {
            get { return _CraftBonuses ?? (_CraftBonuses = this.InitCraftBonuses()); }
        }

        private List<ToaBonusModel> _ToaBonuses;
        public List<ToaBonusModel> ToaBonuses
        {
            get { return _ToaBonuses ?? (_ToaBonuses = this.InitToaBonuses()); }
        }

        private List<MythicalBonusModel> _MythicalBonuses;
        public List<MythicalBonusModel> MythicalBonuses
        {
            get { return _MythicalBonuses ?? (_MythicalBonuses = this.InitMythicalBonuses()); }
        }

        private List<ArmorBonusModel> _ArmorBonuses;
        public List<ArmorBonusModel> ArmorBonuses
        {
            get { return _ArmorBonuses ?? (_ArmorBonuses = this.InitArmorBonuses()); }
        }

        private List<ArmorTypeModel> _ArmorTypes;
        public List<ArmorTypeModel> ArmorTypes
        {
            get { return _ArmorTypes ?? (_ArmorTypes = this.InitArmorTypes()); }
        }

        private List<UseItemModel> _UseItems;
        public List<UseItemModel> UseItems
        {
            get { return _UseItems ?? (_UseItems = this.InitUseItems()); }
        }

        private List<LegendaryWeaponModel> _LegendaryWeapons;
        public List<LegendaryWeaponModel> LegendaryWeapons
        {
            get { return _LegendaryWeapons ?? (_LegendaryWeapons = this.InitLegendaryWeapons()); }
        }

        private List<GemQualityModel> _LstGemQualities;
        public List<GemQualityModel> LstGemQualities
        {
            get { return _LstGemQualities ?? (_LstGemQualities = this.InitGemQualities()); }
        }

        private List<GemModel> _LstGems;
        public List<GemModel> LstGems
        {
            get { return _LstGems ?? (_LstGems = this.InitGems()); }
        }

        private List<AgentModel> _LstAgents;
        public List<AgentModel> LstAgents
        {
            get { return _LstAgents ?? (_LstAgents = this.InitAgents()); }
        }

        private List<DustModel> _LstDusts;
        public List<DustModel> LstDusts
        {
            get { return _LstDusts ?? (_LstDusts = this.InitDusts()); }
        }
		


        #endregion


        #region Methods

        private List<RealmModel> InitRealms()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(RealmModelList), new XmlRootAttribute("Realms"));
            var file = Settings.Current.RepAppli + Realms_File;
            using (FileStream reader = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                var realms = (RealmModelList)serializer.Deserialize(reader);
                return new List<RealmModel>(realms.Realms.OrderBy(o => o.Full));
            }

        }

        private List<ClassModel> InitClasses()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ClassModelList), new XmlRootAttribute("Classes"));
            var file = Settings.Current.RepAppli + Classes_File;
            using (FileStream reader = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                var classes = (ClassModelList)serializer.Deserialize(reader);
                return new List<ClassModel>(classes.Classes.OrderBy(o => o.Full));
            }

        }

        private List<RaceModel> InitRaces()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(RaceModelList), new XmlRootAttribute("Races"));
            var file = Settings.Current.RepAppli + Races_File;
            using (FileStream reader = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                var races = (RaceModelList)serializer.Deserialize(reader);
                return new List<RaceModel>(races.Races.OrderBy(o => o.Full));
            }

        }

        private List<ArmorModel> InitArmors()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ArmorModelList), new XmlRootAttribute("Armors"));
            var file = Settings.Current.RepAppli + Armors_File;
            using (FileStream reader = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                var armors = (ArmorModelList)serializer.Deserialize(reader);
                return new List<ArmorModel>(armors.Armors.OrderBy(o => o.Full));
            }

        }

        private List<JeweleryModel> InitJeweleries()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(JeweleryModelList), new XmlRootAttribute("Jeweleries"));
            var file = Settings.Current.RepAppli + Jeweleries_File;
            using (FileStream reader = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                var jeweleries = (JeweleryModelList)serializer.Deserialize(reader);
                return new List<JeweleryModel>(jeweleries.Jeweleries.OrderBy(o => o.Full));
            }

        }

        private List<ResistModel> InitResists()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ResistModelList), new XmlRootAttribute("Resists"));
            var file = Settings.Current.RepAppli + Resists_File;
            using (FileStream reader = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                var resists = (ResistModelList)serializer.Deserialize(reader);
                return new List<ResistModel>(resists.Resists.OrderBy(o => o.Order));
            }

        }

        private List<CaracModel> InitCaracs()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(CaracModelList), new XmlRootAttribute("Caracs"));
            var file = Settings.Current.RepAppli + Caracs_File;
            using (FileStream reader = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                var caracs = (CaracModelList)serializer.Deserialize(reader);
                return new List<CaracModel>(caracs.Caracs.OrderBy(o => o.Order));
            }

        }

        private List<WeaponModel> InitWeapons()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(WeaponModelList), new XmlRootAttribute("Weapons"));
            var file = Settings.Current.RepAppli + Weapons_File;
            using (FileStream reader = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                var weapons = (WeaponModelList)serializer.Deserialize(reader);
                return new List<WeaponModel>(weapons.Weapons.OrderBy(o => o.Full));
            }

        }

        private List<SkillModel> InitSkills()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(SkillModelList), new XmlRootAttribute("Skills"));
            var file = Settings.Current.RepAppli + Skills_File;
            using (FileStream reader = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                var skills = (SkillModelList)serializer.Deserialize(reader);
                return new List<SkillModel>(skills.Skills.OrderBy(o => o.Full).OrderBy(o => o.Order));
            }

        }

        private List<TypeBonusModel> InitTypeBonuses()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(TypeBonusModelList), new XmlRootAttribute("TypeBonuses"));
            var file = Settings.Current.RepAppli + TypeBonuses_File;
            using (FileStream reader = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                var typeBonuses = (TypeBonusModelList)serializer.Deserialize(reader);
                return new List<TypeBonusModel>(typeBonuses.TypeBonuses.OrderBy(o => o.Order));
            }

        }

        private List<CraftBonusModel> InitCraftBonuses()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(CraftBonusModelList), new XmlRootAttribute("CraftBonuses"));
            var file = Settings.Current.RepAppli + CraftBonuses_File;
            using (FileStream reader = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                var craftBonuses = (CraftBonusModelList)serializer.Deserialize(reader);
                return new List<CraftBonusModel>(craftBonuses.CraftBonuses.OrderBy(o => o.Value));
            }
        }

        private List<ToaBonusModel> InitToaBonuses()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ToaBonusModelList), new XmlRootAttribute("ToABonuses"));
            var file = Settings.Current.RepAppli + ToABonuses_File;
            using (FileStream reader = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                var toaBonuses = (ToaBonusModelList)serializer.Deserialize(reader);
                return new List<ToaBonusModel>(toaBonuses.ToABonuses.OrderBy(o => o.Full));
            }
        }

        private List<MythicalBonusModel> InitMythicalBonuses()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(MythicalBonusModelList), new XmlRootAttribute("MythicalBonuses"));
            var file = Settings.Current.RepAppli + MythicalBonuses_File;
            using (FileStream reader = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                var mythicalBonuses = (MythicalBonusModelList)serializer.Deserialize(reader);
                return new List<MythicalBonusModel>(mythicalBonuses.ToABonuses.OrderBy(o => o.Full));
            }
        }

        private List<ArmorBonusModel> InitArmorBonuses()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ArmorBonusModelList), new XmlRootAttribute("ArmorBonuses"));
            var file = Settings.Current.RepAppli + ArmorBonuses_File;
            using (FileStream reader = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                var armorBonuses = (ArmorBonusModelList)serializer.Deserialize(reader);
                return new List<ArmorBonusModel>(armorBonuses.ArmorBonuses.OrderBy(o => o.Full));
            }
        }

        private List<ArmorTypeModel> InitArmorTypes()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ArmorTypeModelList), new XmlRootAttribute("ArmorTypes"));
            var file = Settings.Current.RepAppli + ArmorTypes_File;
            using (FileStream reader = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                var types = (ArmorTypeModelList)serializer.Deserialize(reader);
                return new List<ArmorTypeModel>(types.ArmorTypes.OrderBy(o => o.Full));
            }

        }

        private List<UseItemModel> InitUseItems()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(UseItemModelList), new XmlRootAttribute("UsesItem"));
            var file = Settings.Current.RepAppli + UsesItem_File;
            using (FileStream reader = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                var uses = (UseItemModelList)serializer.Deserialize(reader);
                return new List<UseItemModel>(uses.UseItems.OrderBy(o => o.Full));
            }
        }

        private List<LegendaryWeaponModel> InitLegendaryWeapons()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(LegendaryWeaponModelList), new XmlRootAttribute("LegendariesWep"));
            var file = Settings.Current.RepAppli + LegendaryWeapons_File;
            using (FileStream reader = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                var weapons = (LegendaryWeaponModelList)serializer.Deserialize(reader);
                return new List<LegendaryWeaponModel>(weapons.LegendaryWeapons.OrderBy(o => o.Code));
            }
        }

        private List<GemQualityModel> InitGemQualities()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(GemQualityModelList), new XmlRootAttribute("GemQualities"));
            var file = Settings.Current.RepAppli + GemQualities_File;
            using (FileStream reader = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                var qualities = (GemQualityModelList)serializer.Deserialize(reader);
                return new List<GemQualityModel>(qualities.GemQualities.OrderBy(o => o.Code));
            }
        }

        private List<GemModel> InitGems()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(GemModelList), new XmlRootAttribute("CraftMaterials"));
            var file = Settings.Current.RepAppli + CraftMaterials_File;
            using (FileStream reader = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                var gems = (GemModelList)serializer.Deserialize(reader);
                return new List<GemModel>(gems.Gems.OrderBy(o => o.Code));
            }
        }

        private List<AgentModel> InitAgents()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(AgentModelList), new XmlRootAttribute("CraftMaterials"));
            var file = Settings.Current.RepAppli + CraftMaterials_File;
            using (FileStream reader = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                var agents = (AgentModelList)serializer.Deserialize(reader);
                return new List<AgentModel>(agents.Agents.OrderBy(o => o.Code));
            }
        }

        private List<DustModel> InitDusts()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(DustModelList), new XmlRootAttribute("CraftMaterials"));
            var file = Settings.Current.RepAppli + CraftMaterials_File;
            using (FileStream reader = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                var dusts = (DustModelList)serializer.Deserialize(reader);
                return new List<DustModel>(dusts.Dusts.OrderBy(o => o.Code));
            }
        }

        #endregion


        #region Const

        private const string Realms_File = "Realms.xml";
        private const string Classes_File = "Classes.xml";
        private const string Races_File = "Races.xml";
        private const string Armors_File = "Armors.xml";
        private const string Jeweleries_File = "Jeweleries.xml";
        private const string Resists_File = "Resists.xml";
        private const string Caracs_File = "Caracs.xml";
        private const string Weapons_File = "Weapons.xml";
        private const string Skills_File = "Skills.xml";
        private const string TypeBonuses_File = "TypeBonuses.xml";
        private const string CraftBonuses_File = "CraftBonuses.xml";
        private const string ToABonuses_File = "ToABonuses.xml";
        private const string MythicalBonuses_File = "MythicalBonuses.xml";
        private const string ArmorBonuses_File = "ArmorBonuses.xml";
        private const string ArmorTypes_File = "ArmorTypes.xml";
        private const string UsesItem_File = "UsesItem.xml";
        private const string LegendaryWeapons_File = "LegendaryWeapons.xml";
        private const string GemQualities_File = "GemQualities.xml";
        private const string CraftMaterials_File = "CraftMaterials.xml";

        #endregion
    }
}
