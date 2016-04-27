using DaocSpellCraftCalculator.Models;
using DaocSpellCraftCalculator.Tools;
using DaocSpellCraftCalculator.Tools.Helper;
using DaocSpellCraftCalculator.ViewModels;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;

namespace DaocSpellCraftCalculator
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            StyleManager.ApplicationTheme = new Expression_DarkTheme();
            InitializeComponent();
            EventManager.RegisterClassHandler(typeof(RadTabItem), RoutedEventHelper.CloseTabEvent, new RoutedEventHandler(OnCloseClicked));
        }

        private void I_MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ShowOptionsAlert();
        }

        public void OnCloseClicked(object sender, RoutedEventArgs e)
        {
            var tabItem = sender as RadTabItem;
            var main = ServiceLocator.Current.GetInstance<MainViewModel>();
            var template = tabItem.DataContext as TemplateViewModel;
            main.EventCloseTemplate(template);
        }


        private void OnSlotDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var main = ServiceLocator.Current.GetInstance<MainViewModel>();
            var template = main.SelectedTemplate;
            template.LoadItemCommand.Execute(null);
        }

        private void ShowOptionsAlert()
        {
            var repItem = Properties.Settings.Default.RepItems;
            var reptemplate = Properties.Settings.Default.RepTemplates;
            if (string.IsNullOrEmpty(repItem) || !Directory.Exists(repItem) || string.IsNullOrEmpty(reptemplate) || !Directory.Exists(reptemplate))
            {
                var param = Settings.Current.GetDialogParameters();
                param.Content = "You must first configure the items and templates directories." + Environment.NewLine + "Please, check the options.";
                RadWindow.Alert(param);
            }
        }

        private void ShowDetailBonus(string typeBonus, RadListBox listBox)
        {
            var main = ServiceLocator.Current.GetInstance<MainViewModel>();
            var template = main.SelectedTemplate;
            if (listBox != null)
            {
                var item = (StatModel)listBox.SelectedItem;
                template.ShowDetailBonus(typeBonus, item.Stat.Code);
            }
        }

        private void OnStatDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.ShowDetailBonus("STA", (RadListBox)sender);
        }
        private void OnResistDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.ShowDetailBonus("RES", (RadListBox)sender);
        }
        private void OnBonusesDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.ShowDetailBonus("OTH", (RadListBox)sender);
        }
        private void OnSkillDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.ShowDetailBonus("SKI", (RadListBox)sender);
        }

    }
}
