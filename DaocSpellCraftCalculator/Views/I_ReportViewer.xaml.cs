namespace DaocSpellCraftCalculator.Views
{
    using DaocSpellCraftCalculator.ViewModels;
    using Telerik.Reporting;
    using Telerik.Windows.Controls;

    public partial class I_ReportViewer : RadWindow
    {
        public I_ReportViewer()
            : this("", new InstanceReportSource(), true, false, false)
        {
        }

        public I_ReportViewer(string title, ReportSource reportSource, bool full, bool materials, bool gems)
        {
            InitializeComponent();
            this.Owner = App.Current.MainWindow;
            var vm = new ReportViewModel(title, reportSource, full, gems, materials);
            this.DataContext = vm;
            vm.SourceReport = reportSource;
        }
    }
}