using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Telerik.Windows.Controls;

namespace DaocSpellCraftCalculator.Tools
{
    public class Settings
    {

        private static Settings _current;
        public static Settings Current
        {
            get
            {
                return _current ?? (_current = new Settings());
            }
        }

        public string RepTemplates
        {
            get
            {
                return Properties.Settings.Default.RepTemplates;
            }
        }
        public string RepItems
        {
            get
            {
                return Properties.Settings.Default.RepItems;
            }
        }
        public string RepIniFiles
        {
            get
            {
                return Properties.Settings.Default.RepIniFiles;
            }
        }
        public string RepAppli
        {
            get
            {
                var path = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\Data\";
#if DEBUG
                path = @"D:\_PC Laurent\Divers\DaocSpellCraftCalculator\DaocSpellCraftCalculator\DaocSpellCraftCalculator\Data\";
#endif
                return path;
            }
        }
        public bool CustomizeTemplates
        {
            get { return Properties.Settings.Default.CustomizeTemplates; }
        }
        public bool IncludeRaciales
        {
            get { return Properties.Settings.Default.IncludeRaciales; }
        }
        public bool RespectBonusLevel
        {
            get { return Properties.Settings.Default.RespectBonusLevel; }
        }
        public int QuickBarMacro
        {
            get { return Properties.Settings.Default.QuickBarMacro; }
        }
        public int BankMacro
        {
            get { return Properties.Settings.Default.BankMAcro; }
        }
        public int SlotMacro
        {
            get { return Properties.Settings.Default.SlotMacro; }
        }
        public bool SeparatorMacro
        {
            get { return Properties.Settings.Default.SeparatorMacro; }
        }
        public string Theme
        {
            get { return Properties.Settings.Default.Theme; }
        }
		


        public DialogParameters GetDialogParameters()
        {
            var param = new DialogParameters();
            param.CancelButtonContent = "Cancel";
            param.OkButtonContent = "Ok";
            param.DialogStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
            param.Owner = App.Current.MainWindow;
            return param;
        }


        public void SaveSettings(string key, string value)
        {
            if (key == REP_TEMPLATES)
                Properties.Settings.Default.RepTemplates = value;
            else if (key == REP_ITEMS)
                Properties.Settings.Default.RepItems = value;
            else if (key == REP_INI_FILES)
                Properties.Settings.Default.RepIniFiles = value;
            else if (key == THEME)
                Properties.Settings.Default.Theme = value;
            else if (key == CUSTOMIZE_TEMPLATES)
                Properties.Settings.Default.CustomizeTemplates = Convert.ToBoolean(value);
            else if (key == INCLUDE_RACIALES)
                Properties.Settings.Default.IncludeRaciales = Convert.ToBoolean(value);
            else if (key == RESPECT_BONUS_LEVEL)
                Properties.Settings.Default.RespectBonusLevel = Convert.ToBoolean(value);
            else if (key == QUICKBAR_MACRO)
                Properties.Settings.Default.QuickBarMacro = Convert.ToInt32(value);
            else if (key == BANK_MACRO)
                Properties.Settings.Default.BankMAcro = Convert.ToInt32(value);
            else if (key == SLOT_MACRO)
                Properties.Settings.Default.SlotMacro = Convert.ToInt32(value);
            else if (key == SEPARATOR_MACRO)
                Properties.Settings.Default.SeparatorMacro = Convert.ToBoolean(value);

            Properties.Settings.Default.Save();

        }

        public const string REP_TEMPLATES = "RepTemplates";
        public const string REP_ITEMS = "RepItems";
        public const string REP_INI_FILES = "RepIniFiles";
        public const string CUSTOMIZE_TEMPLATES = "CustomizeTemplates";
        public const string INCLUDE_RACIALES = "IncludeRaciales";
        public const string RESPECT_BONUS_LEVEL = "RespectBonusLevel";
        public const string QUICKBAR_MACRO = "QuickBarMacro";
        public const string BANK_MACRO = "BankMacro";
        public const string SLOT_MACRO = "SlotMacro";
        public const string SEPARATOR_MACRO = "SeparatorMacro";
        public const string THEME = "Theme";

    }
}
