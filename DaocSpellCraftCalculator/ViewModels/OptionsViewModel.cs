using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using DaocSpellCraftCalculator.Tools;
using System.Windows;

namespace DaocSpellCraftCalculator.ViewModels
{
    public class OptionsViewModel : ViewModelBase, IRequestCloseViewModel
    {


        #region Constructor

        public OptionsViewModel()
        {
            this.RepTemplates = Settings.Current.RepTemplates;
            this.RepItems = Settings.Current.RepItems;
            this.CustomizeTemplates = Settings.Current.CustomizeTemplates;
            this.IncludeRaciales = Settings.Current.IncludeRaciales;
            this.RespectBonusLevel = Settings.Current.RespectBonusLevel;
            this.SelectedTheme = this.LstThemes.FirstOrDefault(o => o.Code == Settings.Current.Theme);
        }

        #endregion


        #region Properties

        private string _RepTemplates;
        public string RepTemplates
        {
            get { return _RepTemplates; }
            set { if (_RepTemplates != value) { _RepTemplates = value; RaisePropertyChanged("RepTemplates"); OnDirectoryChanged(); } }
        }

        private string _RepItems;
        public string RepItems
        {
            get { return _RepItems; }
            set { if (_RepItems != value) { _RepItems = value; RaisePropertyChanged("RepItems"); OnDirectoryChanged(); } }
        }

        private bool _CustomizeTemplates;
        public bool CustomizeTemplates
        {
            get { return _CustomizeTemplates; }
            set { if (_CustomizeTemplates != value) { _CustomizeTemplates = value; RaisePropertyChanged("CustomizeTemplates"); } }
        }

        private bool _IncludeRaciales;
        public bool IncludeRaciales
        {
            get { return _IncludeRaciales; }
            set { if (_IncludeRaciales != value) { _IncludeRaciales = value; RaisePropertyChanged("IncludeRaciales"); } }
        }

        private bool _RespectBonusLevel;
        public bool RespectBonusLevel
        {
            get { return _RespectBonusLevel; }
            set { if (_RespectBonusLevel != value) { _RespectBonusLevel = value; RaisePropertyChanged("RespectBonusLevel"); } }
        }


        private List<ThemeModel> _LstThemes;
        public List<ThemeModel> LstThemes
        {
            get { return _LstThemes ?? (_LstThemes = ThemeModel.GetThemes()); }
        }

        private ThemeModel _SelectedTheme;
        public ThemeModel SelectedTheme
        {
            get { return _SelectedTheme; }
            set { if (_SelectedTheme != value) { _SelectedTheme = value; RaisePropertyChanged("SelectedTheme"); } }
        }
		



        #endregion


        #region Commands


        private RelayCommand _SaveOptionsCommand;
        public RelayCommand SaveOptionsCommand
        {
            get
            {
                return _SaveOptionsCommand ?? (_SaveOptionsCommand = new RelayCommand(SaveOptions, CanSave));
            }
        }

        public bool CanSaveOptions
        {
            get { return this.CanSave(); }
        }

        
        private RelayCommand _ApplyThemeCommand;
        public RelayCommand ApplyThemeCommand
        {
            get
            {
                return _ApplyThemeCommand ?? (_ApplyThemeCommand = new RelayCommand(ApplyTheme));
            }
        }
		


        #endregion


        #region Methods


        private void SaveOptions()
        {
            if (Directory.Exists(this.RepItems) && Directory.Exists(this.RepTemplates))
            {
                Settings.Current.SaveSettings(Settings.REP_ITEMS, this.RepItems);
                Settings.Current.SaveSettings(Settings.REP_TEMPLATES, this.RepTemplates);
                Settings.Current.SaveSettings(Settings.THEME, this.SelectedTheme.Code);
                Settings.Current.SaveSettings(Settings.CUSTOMIZE_TEMPLATES, this.CustomizeTemplates.ToString());
                Settings.Current.SaveSettings(Settings.INCLUDE_RACIALES, this.IncludeRaciales.ToString());
                Settings.Current.SaveSettings(Settings.RESPECT_BONUS_LEVEL, this.RespectBonusLevel.ToString());
                RequestClose(this, new EventArgs());
            }
        }
        private bool CanSave()
        {
            return !string.IsNullOrEmpty(this.RepTemplates) && Directory.Exists(this.RepTemplates) && !string.IsNullOrEmpty(this.RepItems) && Directory.Exists(this.RepItems);
        }


        private void ApplyTheme()
        {
            if (this.SelectedTheme != null)
            {
                Application.Current.Resources.MergedDictionaries.Clear();
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("/Telerik.Windows.Themes." + this.SelectedTheme.Code + ";component/Themes/System.Windows.xaml", UriKind.RelativeOrAbsolute)
                });
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("/Telerik.Windows.Themes." + this.SelectedTheme.Code + ";component/Themes/Telerik.Windows.Controls.xaml", UriKind.RelativeOrAbsolute)
                });
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("/Telerik.Windows.Themes." + this.SelectedTheme.Code + ";component/Themes/Telerik.Windows.Controls.Input.xaml", UriKind.RelativeOrAbsolute)
                });
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("/Telerik.Windows.Themes." + this.SelectedTheme.Code + ";component/Themes/Telerik.Windows.Controls.Navigation.xaml", UriKind.RelativeOrAbsolute)
                });
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("../Themes/" + this.SelectedTheme.Code + "/Telerik.ReportViewer.Wpf.xaml", UriKind.RelativeOrAbsolute)
                });
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("../Themes/WindowStyle.xaml", UriKind.RelativeOrAbsolute)
                });
            }
        }


        #endregion


        #region Events


        private void OnDirectoryChanged()
        {
            this.SaveOptionsCommand.RaiseCanExecuteChanged();
        }



        public event EventHandler RequestClose;

        #endregion


    }


    public class ThemeModel
    {
        public string Libelle { get; set; }
        public string Code { get; set; }


        public static List<ThemeModel> GetThemes()
        {
            var retour = new List<ThemeModel>();

            retour.Add(new ThemeModel() { Libelle = "Expression Dark", Code = "Expression_Dark" });
            retour.Add(new ThemeModel() { Libelle = "Office Black", Code = "Office_Black" });
            retour.Add(new ThemeModel() { Libelle = "Office Blue", Code = "Office_Blue" });
            retour.Add(new ThemeModel() { Libelle = "Office Silver", Code = "Office_Silver" });
            retour.Add(new ThemeModel() { Libelle = "Summer", Code = "Summer" });
            retour.Add(new ThemeModel() { Libelle = "Vista", Code = "Vista" });
            retour.Add(new ThemeModel() { Libelle = "Visual Studio 2013", Code = "VisualStudio2013" });
            retour.Add(new ThemeModel() { Libelle = "Windows 7", Code = "Windows7" });

            return retour;
        }
    }
}


