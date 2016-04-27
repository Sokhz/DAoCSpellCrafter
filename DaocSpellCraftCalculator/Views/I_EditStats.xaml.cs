using DaocSpellCraftCalculator.ViewModels;
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
    /// Logique d'interaction pour I_EditStats.xaml
    /// </summary>
    public partial class I_EditStats : RadWindow
    {
        public I_EditStats()
        {
            InitializeComponent();
            this.Owner = App.Current.MainWindow;
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);

            var vm = (TemplateViewModel)this.DataContext;
            vm.RequestClose += (s, ev) => this.Close();
        }
    }
}
