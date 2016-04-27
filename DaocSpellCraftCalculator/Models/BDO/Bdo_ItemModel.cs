using DaocSpellCraftCalculator.Tools;
using DaocSpellCraftCalculator.ViewModels;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DaocSpellCraftCalculator.Models.BDO
{
    [XmlRoot("SCItem")]
    public class Bdo_PartialItemModel
    {
        public string SOURCE { get; set; }

        public PartialItemViewModel ConvertToInternalItem()
        {
            var retour = new PartialItemViewModel();
            return retour;
        }
    }


    [XmlRoot("SCItem")]
    public class Bdo_ItemModel
    {

        public string Realm { get; set; }
        public int Level { get; set; }
        public int Bonus { get; set; }
        public string Location { get; set; }
        public int ItemQuality { get; set; }
        public string ItemName { get; set; }
        public string TYPE { get; set; }
        public int BDOBLEVEL { get; set; }
        [XmlArray("CLASSRESTRICTIONS")]
        [XmlArrayItem("CLASS", typeof(string))]
        public List<string> CLASSRESTRICTIONS { get; set; }
        public string SOURCE { get; set; }
        public Bdo_ItemBonusModelList DROPITEM { get; set; }



        public ItemViewModel ConvertToInternalItem(string slot)
        {
            var retour = new ItemViewModel();
            retour.IsEquiped = true;
            retour.IsLoot = true;
            retour.Level = this.Level;
            retour.BonusLevel = this.BDOBLEVEL;
            retour.Name = this.ItemName;
            if (!(this.CLASSRESTRICTIONS.Count == 1 && this.CLASSRESTRICTIONS.First().ToLower() == "all"))
                retour.ClassRestriction = this.CLASSRESTRICTIONS.Select(o => DataProvider.Current.Classes.FirstOrDefault(c => c.Full.ToLower() == o.ToLower()).Code).ToList();
            retour.Slot = slot;

            if (this.DROPITEM.SLOT0 != null)
                retour.Bonus1 = this.DROPITEM.SLOT0.ConvertToInternalBonus();
            if (this.DROPITEM.SLOT1 != null)
                retour.Bonus2 = this.DROPITEM.SLOT1.ConvertToInternalBonus();
            if (this.DROPITEM.SLOT2 != null)
                retour.Bonus3 = this.DROPITEM.SLOT2.ConvertToInternalBonus();
            if (this.DROPITEM.SLOT3 != null)
                retour.Bonus4 = this.DROPITEM.SLOT3.ConvertToInternalBonus();
            if (this.DROPITEM.SLOT4 != null)
                retour.Bonus5 = this.DROPITEM.SLOT4.ConvertToInternalBonus();
            if (this.DROPITEM.SLOT5 != null)
                retour.Bonus6 = this.DROPITEM.SLOT5.ConvertToInternalBonus();
            if (this.DROPITEM.SLOT6 != null)
                retour.Bonus7 = this.DROPITEM.SLOT6.ConvertToInternalBonus();
            if (this.DROPITEM.SLOT7 != null)
                retour.Bonus8 = this.DROPITEM.SLOT7.ConvertToInternalBonus();
            if (this.DROPITEM.SLOT8 != null)
                retour.Bonus9 = this.DROPITEM.SLOT8.ConvertToInternalBonus();
            if (this.DROPITEM.SLOT9 != null)
                retour.Bonus10 = this.DROPITEM.SLOT9.ConvertToInternalBonus();
            if (this.DROPITEM.SLOT10 != null)
                retour.Bonus11 = this.DROPITEM.SLOT10.ConvertToInternalBonus();
            if (this.DROPITEM.SLOT11 != null)
                retour.Bonus12 = this.DROPITEM.SLOT11.ConvertToInternalBonus();
            if (this.DROPITEM.SLOT12 != null)
                retour.Bonus13 = this.DROPITEM.SLOT12.ConvertToInternalBonus();
            if (this.DROPITEM.SLOT13 != null)
                retour.Bonus14 = this.DROPITEM.SLOT13.ConvertToInternalBonus();
            if (this.DROPITEM.SLOT14 != null)
                retour.Bonus15 = this.DROPITEM.SLOT14.ConvertToInternalBonus();

            return retour;
        }
    }


}

