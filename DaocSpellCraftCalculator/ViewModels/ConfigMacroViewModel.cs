using DaocSpellCraftCalculator.Tools;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Windows.Controls;

namespace DaocSpellCraftCalculator.ViewModels
{
    public class ConfigMacroViewModel : GalaSoft.MvvmLight.ViewModelBase
    {

        #region Constructor

        public ConfigMacroViewModel()
        {
            this.IniFilesRepertory = Settings.Current.RepIniFiles;
            this.QuickBar = Settings.Current.QuickBarMacro;
            this.Bank = Settings.Current.BankMacro;
            this.Slot = Settings.Current.SlotMacro;
            this.Separator = Settings.Current.SeparatorMacro;
        }

        #endregion


        #region Properties

        private string _IniFilesRepertory;
        public string IniFilesRepertory
        {
            get { return _IniFilesRepertory; }
            set { if (_IniFilesRepertory != value) { _IniFilesRepertory = value; RaisePropertyChanged("IniFilesRepertory"); OnIniRepertoryChanged(); } }
        }

        private string _Filter;
        public string Filter
        {
            get { return _Filter; }
            set { if (_Filter != value) { _Filter = value; RaisePropertyChanged("Filter"); OnFilterChanged(); } }
        }

        private List<FileInfo> LstAllIniFiles { get; set; }
        private ObservableCollection<FileInfo> _LstIniFiles;
        public ObservableCollection<FileInfo> LstIniFiles
        {
            get { return _LstIniFiles; }
            set { if (_LstIniFiles != value) { _LstIniFiles = value; RaisePropertyChanged("LstIniFiles"); } }
        }

        private FileInfo _SelectedIniFile;
        public FileInfo SelectedIniFile
        {
            get { return _SelectedIniFile; }
            set { if (_SelectedIniFile != value) { _SelectedIniFile = value; RaisePropertyChanged("SelectedIniFile"); } }
        }


        private int _QuickBar;
        public int QuickBar
        {
            get { return _QuickBar; }
            set { if (_QuickBar != value) { _QuickBar = value; RaisePropertyChanged("QuickBar"); OnQuickBarChanged(); } }
        }

        private int _Bank;
        public int Bank
        {
            get { return _Bank; }
            set { if (_Bank != value) { _Bank = value; RaisePropertyChanged("Bank"); OnBankChanged(); } }
        }

        private int _Slot;
        public int Slot
        {
            get { return _Slot; }
            set { if (_Slot != value) { _Slot = value; RaisePropertyChanged("Slot"); OnSlotChanged(); } }
        }

        private bool _Separator;
        public bool Separator
        {
            get { return _Separator; }
            set { if (_Separator != value) { _Separator = value; RaisePropertyChanged("Separator"); } }
        }


        #endregion


        #region Commands

        private RelayCommand _LoadMacroCommand;
        public RelayCommand LoadMacroCommand
        {
            get
            {
                return _LoadMacroCommand ?? (_LoadMacroCommand = new RelayCommand(LoadMacro));
            }
        }

        #endregion


        #region Methods

        private void LoadMacro()
        {
            if (this.IsQuickBarValid() && this.IsBankValid() && this.IsSlotValid() && this.SelectedIniFile != null)
            {
                var mainVM = ServiceLocator.Current.GetInstance<MainViewModel>();
                var template = mainVM.SelectedTemplate;

                if (template != null)
                {
                    try
                    {
                        var macros = new List<MacroModel>();
                        var begin = new List<string>();
                        var end = new List<string>();

                        using (StreamReader reader = new StreamReader(this.SelectedIniFile.FullName))
                        {
                            var line = "";
                            var currentQuickBar = 0;

                            while ((line = reader.ReadLine()) != null)
                            {
                                if (line.StartsWith("[Quickbar"))
                                {
                                    currentQuickBar++;
                                }
                                else if (line.StartsWith("Hotkey_"))
                                {
                                    macros.Add(new MacroModel(currentQuickBar, line));
                                }
                                else
                                {
                                    if (currentQuickBar == 0)
                                        begin.Add(line);
                                    else if (line != "GroupSize=10")
                                        end.Add(line);
                                }

                            }

                        }

                        int quickbar = this.QuickBar;
                        int bank = this.Bank;
                        int slot = this.Slot;
                        var macroRef = macros.FirstOrDefault(o => o.Quickbar == quickbar && o.Bank == bank && o.Slot == slot);

                        slot++;
                        if (slot == 11)
                        {
                            slot = 1;
                            bank++;
                            if (bank == 11)
                                bank = 1;
                        }

                        if (macroRef != null)
                        {

                            MacroModel macro = null;

                            foreach (var item in template.LstItemsCraft)
                            {
                                foreach (var bonus in item.LstBonuses.Where(o => !string.IsNullOrEmpty(o.GemCode) && !o.IsFifthBonus))
                                {
                                    macro = macros.FirstOrDefault(o => o.Quickbar == quickbar && o.Bank == bank && o.Slot == slot);
                                    if (macro == null)
                                    {
                                        macro = new MacroModel();
                                        macro.Quickbar = quickbar;
                                        macro.Bank = bank;
                                        macro.Slot = slot;
                                        macros.Add(macro);
                                    }
                                    macro.Var1 = "45";
                                    macro.Var2 = bonus.GemCode;
                                    macro.Var3 = "";
                                    macro.Var4 = macroRef.Var4;

                                    slot++;
                                    if (slot == 11)
                                    {
                                        slot = 1;
                                        bank++;
                                        if (bank == 11)
                                            bank = 1;
                                    }
                                }

                                if (this.Separator)
                                {
                                    //macro = new MacroModel();
                                    //macro.Quickbar = quickbar;
                                    //macro.Bank = bank;
                                    //macro.Slot = slot;
                                    //macros.Add(macro);
                                    //macro.Var1 = "45";
                                    //macro.Var2 = "";
                                    //macro.Var3 = "";
                                    //macro.Var4 = macroRef.Var4;

                                    slot++;
                                    if (slot == 11)
                                    {
                                        slot = 1;
                                        bank++;
                                        if (bank == 11)
                                            bank = 1;
                                    }
                                }

                            }

                            using (var writer = new StreamWriter(this.SelectedIniFile.FullName, false))
                            {
                                foreach (var oLine in begin)
                                {
                                    writer.WriteLine(oLine);
                                }

                                for (int i = 1; i <= 3; i++)
                                {
                                    if (i == 1)
                                    {
                                        writer.WriteLine("[Quickbar]");
                                    }
                                    else
                                    {
                                        writer.WriteLine("[Quickbar" + i + "]");
                                    }
                                    writer.WriteLine("GroupSize=10");


                                    var currentMacros = (from o in macros
                                                         where o.Quickbar == i
                                                         orderby o.Bank, o.Slot
                                                         select o);

                                    foreach (var cMacro in currentMacros)
                                    {
                                        writer.WriteLine(cMacro.ToString());

                                    }

                                }

                                foreach (var oLine in end)
                                {
                                    writer.WriteLine(oLine);
                                }

                            }

                            var param = Settings.Current.GetDialogParameters();
                            param.Content = "Your macros have been loaded on your SpellCrafter !";
                            RadWindow.Alert(param);


                        }
                        else
                        {
                            var param = Settings.Current.GetDialogParameters();
                            param.Content = "Unable to find the SpellCraft macro on your quickbars !";
                            RadWindow.Alert(param);
                        }


                    }
                    catch (Exception)
                    {
                        var param = Settings.Current.GetDialogParameters();
                        param.Content = "An error occured while loading your macros !";
                        RadWindow.Alert(param);
                    }
                }
            }
            else
            {
                var param = Settings.Current.GetDialogParameters();
                param.Content = "You must select a SpellCrafter and configure the options" + Environment.NewLine + " before loading any macros !";
                RadWindow.Alert(param);
            }
        }


        private void FilterIniFiles()
        {
            var items = this.LstAllIniFiles;
            if (!string.IsNullOrEmpty(this.Filter))
            {
                items = items.Where(o => o.Name.ToLower().Contains(this.Filter.ToLower())).ToList();
            }
            this.LstIniFiles = new ObservableCollection<FileInfo>(items);
        }

        private bool IsQuickBarValid()
        {
            return this.QuickBar >= 1 && this.QuickBar <= 3;
        }
        private bool IsBankValid()
        {
            return this.Bank >= 1 && this.Bank <= 10;
        }
        private bool IsSlotValid()
        {
            return this.Slot >= 1 && this.Slot <= 10;
        }

        #endregion


        #region Events

        private void OnIniRepertoryChanged()
        {
            if (Directory.Exists(this.IniFilesRepertory))
            {
                Settings.Current.SaveSettings(Settings.REP_INI_FILES, this.IniFilesRepertory);
                var dirIni = new DirectoryInfo(this.IniFilesRepertory);
                this.LstAllIniFiles = dirIni.GetFiles("*.ini").ToList();
                this.FilterIniFiles();
            }
        }

        private void OnFilterChanged()
        {
            this.FilterIniFiles();
        }

        private void OnQuickBarChanged()
        {
            if (this.IsQuickBarValid())
            {
                Settings.Current.SaveSettings(Settings.QUICKBAR_MACRO, this.QuickBar.ToString());
            }
        }
        private void OnBankChanged()
        {
            if (this.IsBankValid())
            {
                Settings.Current.SaveSettings(Settings.BANK_MACRO, this.Bank.ToString());
            }
        }
        private void OnSlotChanged()
        {
            if (this.IsSlotValid())
            {
                Settings.Current.SaveSettings(Settings.SLOT_MACRO, this.Slot.ToString());
            }
        }

        #endregion

    }





    public class MacroModel
    {
        public int Quickbar { get; set; }
        public int Bank { get; set; }
        public int Slot { get; set; }

        public string Var1 { get; set; }
        public string Var2 { get; set; }
        public string Var3 { get; set; }
        public string Var4 { get; set; }

        public MacroModel() { }


        public MacroModel(int quickbar, string line)
        {
            this.Quickbar = quickbar;
            var split = line.Split('=');
            var hotkey = split.FirstOrDefault();
            var content = split.LastOrDefault().Split(',');
            var bankslot = hotkey.Substring(7);
            if (bankslot.Length == 1)
            {
                this.Bank = 1;
                this.Slot = Convert.ToInt32(bankslot) + 1;
            }
            else if (bankslot.Length == 2)
            {
                this.Bank = Convert.ToInt32(bankslot[0].ToString()) + 1;
                this.Slot = Convert.ToInt32(bankslot[1].ToString()) + 1;
            }

            if (content.Length == 4)
            {
                this.Var1 = content[0];
                this.Var2 = content[1];
                this.Var3 = content[2];
                this.Var4 = content[3];
            }


        }


        public override string ToString()
        {
            return "Hotkey_" + (this.Bank == 1 ? "" : Math.Max((this.Bank - 1), 0).ToString()) + Math.Max((this.Slot - 1), 0) + "=" + this.Var1 + "," + this.Var2 + "," + this.Var3 + "," + this.Var4;
        }
    }
}
