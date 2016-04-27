
using DaocSpellCraftCalculator.Tools;
using DaocSpellCraftCalculator.ViewModels;
using System;
using System.Linq;


namespace DaocSpellCraftCalculator.Models.BDO
{
    public class Bdo_ItemBonusModel
    {
        public string Effect { get; set; }
        public string Amount { get; set; }
        public string Type { get; set; }


        public BonusItemViewModel ConvertToInternalBonus()
        {
            var retour = new BonusItemViewModel();
            retour.IsLoot = true;
            if (this.Type != "Unused" && this.Type != "PvE")
            {
                switch (this.Type)
                {
                    case "Stat":
                        retour.TypeBonus = "STA";
                        var carac = DataProvider.Current.Caracs.FirstOrDefault(o => this.Effect.ToLower().Contains(o.Full.ToLower()));
                        if (carac != null)
                            retour.Bonus = carac.Code;
                        break;
                    case "Resist":
                        retour.TypeBonus = "RES";
                        var resist = DataProvider.Current.Resists.FirstOrDefault(o => this.Effect.ToLower().Contains(o.Full.ToLower()));
                        if (resist != null)
                            retour.Bonus = resist.Code;
                        break;
                    case "Hits":
                        retour.TypeBonus = "HP";
                        break;
                    case "Skill":
                        if (this.Effect.Contains("All Magic"))
                        {
                            retour.TypeBonus = "SKI";
                            retour.Bonus = "MAGIC";
                        }
                        else if (this.Effect.Contains("All Melee"))
                        {
                            retour.TypeBonus = "SKI";
                            retour.Bonus = "MELEE";
                        }
                        else if (this.Effect.Contains("All Archery"))
                        {
                            retour.TypeBonus = "SKI";
                            retour.Bonus = "RANGE";
                        }
                        else if (this.Effect.Contains("All Dual"))
                        {
                            retour.TypeBonus = "SKI";
                            retour.Bonus = "DUAL";
                        }
                        else
                        {
                            retour.TypeBonus = "SKI";
                            var skill = DataProvider.Current.Skills.FirstOrDefault(o => this.Effect.ToLower().Contains(o.Full.ToLower()));
                            if (skill != null)
                                retour.Bonus = skill.Code;
                        }
                        break;
                    case "Cap Increase":
                        retour.TypeBonus = "CAPS";
                        var cap = DataProvider.Current.Caracs.FirstOrDefault(o => this.Effect.ToLower().Contains(o.Full.ToLower()));
                        if (cap != null)
                            retour.Bonus = cap.Code;
                        if (cap == null && this.Effect == "Hits")
                            retour.Bonus = "HP";
                        else if (cap == null && this.Effect == "Power")
                            retour.Bonus = "MAN";
                        else if (cap == null && this.Effect == "Fatigue")
                            retour.Bonus = "FAT";
                        break;
                    case "Other Bonus":
                        retour.TypeBonus = "OTH";
                        switch (this.Effect)
                        {
                            case "Spell Duration Bonus":
                                retour.Bonus = "DUR";
                                break;
                            case "Spell Range Bonus":
                                retour.Bonus = "RAN";
                                break;
                            case "Casting Speed Bonus":
                            case "Archery Speed Bonus":
                                retour.Bonus = "CASTSPEED";
                                break;
                            case "Buff Bonus":
                                retour.Bonus = "BUF";
                                break;
                            case "Debuff Bonus":
                                retour.Bonus = "DEBUF";
                                break;
                            case "Spell Damage Bonus":
                            case "Archery Damage Bonus":
                                retour.Bonus = "MADMG";
                                break;
                            case "Melee Speed Bonus":
                                retour.Bonus = "MELSPEED";
                                break;
                            case "Style Damage Bonus":
                                retour.Bonus = "MES";
                                break;
                            case "Melee Damage Bonus":
                                retour.Bonus = "MEDMG";
                                break;
                            case "Resist Pierce":
                                retour.Bonus = "RES";
                                break;
                            case "Healing Bonus":
                                retour.Bonus = "HEA";
                                break;
                            case "AF Bonus":
                                retour.Bonus = "AF";
                                break;
                            case "Power Percentage Bonus":
                                retour.Bonus = "POW";
                                break;
                            case "Fatigue":
                                retour.TypeBonus = "STA";
                                retour.Bonus = "FAT";
                                break;
                        }
                        break;
                    case "Focus":
                        retour.TypeBonus = "OTH";
                        retour.Bonus = "FOCUS";
                        break;
                }
                retour.Value = Convert.ToInt32(this.Amount);
            }
            return retour;
        }
    }
}