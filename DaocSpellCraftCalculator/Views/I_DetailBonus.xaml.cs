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
    /// Logique d'interaction pour I_DetailBonus.xaml
    /// </summary>
    public partial class I_DetailBonus : RadWindow
    {
        public I_DetailBonus(TemplateViewModel template, string typeBonus, string bonus)
        {
            InitializeComponent();
            this.Owner = App.Current.MainWindow;
            var vm = new DetailBonusViewModel(template,typeBonus, bonus);
            this.DataContext = vm;

        }
    }
}
