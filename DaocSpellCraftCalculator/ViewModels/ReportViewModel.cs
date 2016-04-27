using DaocSpellCraftCalculator.Tools;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.Reporting;
using Telerik.Windows.Controls;

namespace DaocSpellCraftCalculator.ViewModels
{
    public class ReportViewModel : GalaSoft.MvvmLight.ViewModelBase
    {

        #region Constructor

        public ReportViewModel(string title, ReportSource source, bool full, bool materials, bool gems)
        {
            this.Title = title;
            this.SourceReport = source;
            this.Full = full;
            this.Gems = gems;
            this.Materials = materials;
        }

        #endregion


        #region Properties

        private ReportSource _SourceReport;
        public ReportSource SourceReport
        {
            get { return _SourceReport; }
            set { if (_SourceReport != value) { _SourceReport = value; RaisePropertyChanged("SourceReport"); } }
        }

        private string _Title;
        public string Title
        {
            get { return _Title; }
            set { if (_Title != value) { _Title = value; RaisePropertyChanged("Title"); } }
        }

        private bool _Full;
        public bool Full
        {
            get { return _Full; }
            set { if (_Full != value) { _Full = value; RaisePropertyChanged("Full"); } }
        }

        private bool _Gems;
        public bool Gems
        {
            get { return _Gems; }
            set { if (_Gems != value) { _Gems = value; RaisePropertyChanged("Gems"); } }
        }

        private bool _Materials;
        public bool Materials
        {
            get { return _Materials; }
            set { if (_Materials != value) { _Materials = value; RaisePropertyChanged("Materials"); } }
        }





        #endregion


        #region Commands

        private RelayCommand _SaveForumCommand;
        public RelayCommand SaveForumCommand
        {
            get
            {
                return _SaveForumCommand ?? (_SaveForumCommand = new RelayCommand(SaveForum));
            }
        }

        private RelayCommand _SaveTxtCommand;
        public RelayCommand SaveTxtCommand
        {
            get
            {
                return _SaveTxtCommand ?? (_SaveTxtCommand = new RelayCommand(SaveTxt));
            }
        }


        private RelayCommand _SavePdfCommand;
        public RelayCommand SavePdfCommand
        {
            get
            {
                return _SavePdfCommand ?? (_SavePdfCommand = new RelayCommand(SavePdf));
            }
        }



        #endregion


        #region Methods

        private void SaveForum()
        {
            try
            {
                var mainVM = ServiceLocator.Current.GetInstance<MainViewModel>();
                var template = mainVM.SelectedTemplate;
                if (template != null)
                {
                    var content = template.GetTxtReport(this.Full, this.Gems, this.Materials, true);
                    if (!string.IsNullOrEmpty(content))
                    {
                        Clipboard.SetText(content, TextDataFormat.Text);

                        var param = Settings.Current.GetDialogParameters();
                        param.Content = "Report saved to ClipBoard !";
                        RadWindow.Alert(param);
                    }
                }
            }
            catch (Exception)
            {
                var param = Settings.Current.GetDialogParameters();
                param.Content = "An error occured while saving the template to clipboard ! Please, try again later.";
                RadWindow.Alert(param);
            }
        }

        private void SaveTxt()
        {
            var file = "";
            try
            {

                var mainVM = ServiceLocator.Current.GetInstance<MainViewModel>();
                var template = mainVM.SelectedTemplate;
                if (template != null)
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.AddExtension = true;
                    saveFileDialog.InitialDirectory = Settings.Current.RepTemplates;
                    saveFileDialog.Filter = "Text files (*.txt)|*.txt";
                    saveFileDialog.Title = "Save report to ...";
                    saveFileDialog.FileName = this.Title + ".txt";
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        file = saveFileDialog.FileName;

                    if (!string.IsNullOrEmpty(file))
                    {
                        if (File.Exists(file))
                            File.Delete(file);

                        var content = Encoding.UTF8.GetBytes(template.GetTxtReport(this.Full, this.Gems, this.Materials, false));
                        using (FileStream fs = new System.IO.FileStream(file, System.IO.FileMode.Create))
                        {
                            fs.Write(content, 0, content.Length);
                        }

                        var param = Settings.Current.GetDialogParameters();
                        param.Content = this.Title + " successfully saved !";
                        RadWindow.Alert(param);
                    }
                }

            }
            catch (Exception)
            {
                if (File.Exists(file))
                    File.Delete(file);
                var param = Settings.Current.GetDialogParameters();
                param.Content = "An error occured while saving the template ! Please, try again later.";
                RadWindow.Alert(param);
            }
        }

        private void SavePdf()
        {

            var file = "";
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.AddExtension = true;
                saveFileDialog.InitialDirectory = Settings.Current.RepTemplates;
                saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
                saveFileDialog.Title = "Save report to ...";
                saveFileDialog.FileName = this.Title + ".pdf";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    file = saveFileDialog.FileName;

                if (!string.IsNullOrEmpty(file))
                {
                    if (File.Exists(file))
                        File.Delete(file);

                    Telerik.Reporting.Processing.ReportProcessor reportProcessor = new Telerik.Reporting.Processing.ReportProcessor();
                    Telerik.Reporting.Processing.RenderingResult result = reportProcessor.RenderReport("PDF", this.SourceReport, new System.Collections.Hashtable());

                    using (FileStream fs = new System.IO.FileStream(file, System.IO.FileMode.Create))
                    {
                        fs.Write(result.DocumentBytes, 0, result.DocumentBytes.Length);
                    }

                    var param = Settings.Current.GetDialogParameters();
                    param.Content = this.Title + " successfully saved !";
                    RadWindow.Alert(param);
                }

            }
            catch (Exception)
            {
                if (File.Exists(file))
                    File.Delete(file);
                var param = Settings.Current.GetDialogParameters();
                param.Content = "An error occured while saving the template ! Please, try again later.";
                RadWindow.Alert(param);
            }

        }

        #endregion


        #region Events


        #endregion
    }
}
