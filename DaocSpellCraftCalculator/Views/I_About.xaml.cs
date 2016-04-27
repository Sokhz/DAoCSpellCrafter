using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Logique d'interaction pour I_About.xaml
    /// </summary>
    public partial class I_About : RadWindow
    {
        public I_About()
        {
            InitializeComponent();
            this.Owner = App.Current.MainWindow;
        }

        private void Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start("http://paypal.me/LaurentHignet");
        }
    }
}
