using DaocSpellCraftCalculator.Tools;
using DaocSpellCraftCalculator.ViewModels;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
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

namespace DaocSpellCraftCalculator.Views
{
    /// <summary>
    /// Logique d'interaction pour I_ChooseItem.xaml
    /// </summary>
    public partial class I_ChooseItem : RadWindow
    {
        public I_ChooseItem()
        {
            InitializeComponent();
            var vm = new ChooseItemViewModel();
            var mainVM = ServiceLocator.Current.GetInstance<MainViewModel>();
            vm.Realm = DataProvider.Current.Realms.FirstOrDefault(o => o.Code == mainVM.SelectedTemplate.Realm);
            vm.Class = DataProvider.Current.Classes.FirstOrDefault(o => o.Code == mainVM.SelectedTemplate.Class);
            vm.Slot = mainVM.SelectedTemplate.SelectedSlot;
            this.DataContext = vm;
            this.Owner = App.Current.MainWindow;

            vm.RequestClose += (s, e) => this.Close();
            Chp_ListItems.MouseDoubleClick += vm.OnItemDoubleClick;

        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            Keyboard.Focus(Chp_filter);
        }


    }
}
