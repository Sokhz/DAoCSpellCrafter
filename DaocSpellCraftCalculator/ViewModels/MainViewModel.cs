using DaocSpellCraftCalculator.Models;
using DaocSpellCraftCalculator.Reports;
using DaocSpellCraftCalculator.Tools;
using DaocSpellCraftCalculator.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Xml.Serialization;
using Telerik.Reporting;
using Telerik.Windows.Controls;

namespace DaocSpellCraftCalculator.ViewModels
{

    [DataObject]
    public class MainViewModel : GalaSoft.MvvmLight.ViewModelBase
    {

        #region Constructor

        public MainViewModel()
        {
            this.SelectedTemplate = null;
            this.LstTemplates = new ObservableCollection<TemplateViewModel>();
        }

        #endregion


        #region Properties


        private TemplateViewModel _SelectedTemplate;
        public TemplateViewModel SelectedTemplate
        {
            get { return _SelectedTemplate; }
            set { if (_SelectedTemplate != value) { _SelectedTemplate = value; RaisePropertyChanged("SelectedTemplate"); } }
        }

        private ObservableCollection<TemplateViewModel> _LstTemplates;
        public ObservableCollection<TemplateViewModel> LstTemplates
        {
            get { return _LstTemplates; }
            set { if (_LstTemplates != value) { _LstTemplates = value; RaisePropertyChanged("LstTemplates"); } }
        }


        #endregion


        #region Commandes


        private RelayCommand _OpenCommand;
        public RelayCommand OpenCommand
        {
            get
            {
                return _OpenCommand ?? (_OpenCommand = new RelayCommand(Open));
            }
        }


        private RelayCommand _SaveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return _SaveCommand ?? (_SaveCommand = new RelayCommand(Save));
            }
        }


        private RelayCommand _SaveAsCommand;
        public RelayCommand SaveAsCommand
        {
            get
            {
                return _SaveAsCommand ?? (_SaveAsCommand = new RelayCommand(SaveAs));
            }
        }


        private RelayCommand _QuitCommand;
        public RelayCommand QuitCommand
        {
            get
            {
                return _QuitCommand ?? (_QuitCommand = new RelayCommand(Quit));
            }
        }


        private RelayCommand _CreateTemplateCommand;
        public RelayCommand CreateTemplateCommand
        {
            get
            {
                return _CreateTemplateCommand ?? (_CreateTemplateCommand = new RelayCommand(CreateNewTemplate));
            }
        }


        private RelayCommand _ShowOptionsCommand;
        public RelayCommand ShowOptionsCommand
        {
            get
            {
                return _ShowOptionsCommand ?? (_ShowOptionsCommand = new RelayCommand(ShowOptions));
            }
        }


        private RelayCommand _ShowAboutCommand;
        public RelayCommand ShowAboutCommand
        {
            get
            {
                return _ShowAboutCommand ?? (_ShowAboutCommand = new RelayCommand(ShowAbout));
            }
        }


        private RelayCommand _FullReportCommand;
        public RelayCommand FullReportCommand
        {
            get
            {
                return _FullReportCommand ?? (_FullReportCommand = new RelayCommand(FullReport));
            }
        }


        private RelayCommand _SimplifiedReportCommand;
        public RelayCommand SimplifiedReportCommand
        {
            get
            {
                return _SimplifiedReportCommand ?? (_SimplifiedReportCommand = new RelayCommand(SimplifiedReport));
            }
        }


        private RelayCommand _GemListReportCommand;
        public RelayCommand GemListReportCommand
        {
            get
            {
                return _GemListReportCommand ?? (_GemListReportCommand = new RelayCommand(GemListReport));
            }
        }

        private RelayCommand _GemMaterialsReportCommand;
        public RelayCommand GemMaterialsReportCommand
        {
            get
            {
                return _GemMaterialsReportCommand ?? (_GemMaterialsReportCommand = new RelayCommand(GemMaterialsReport));
            }
        }

        #endregion


        #region Methods

        private void Quit()
        {
            //Unsaved templates.


            System.Windows.Application.Current.Shutdown();
        }

        private void Open()
        {

            var file = "";
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.CheckFileExists = true;
            openFileDialog.CheckPathExists = true;
            openFileDialog.InitialDirectory = Settings.Current.RepTemplates;
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "XML files (*.xml)|*.xml";
            openFileDialog.Title = "Select a template ...";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
                file = openFileDialog.FileName;

            if (!string.IsNullOrEmpty(file) && File.Exists(file))
            {
                try
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(TemplateViewModel));
                    using (FileStream reader = new FileStream(file, FileMode.Open))
                    {
                        var template = (TemplateViewModel)serializer.Deserialize(reader);
                        this.LstTemplates.Add(template);
                        this.SelectedTemplate = template;
                    }
                }
                catch (Exception)
                {
                    var param = Settings.Current.GetDialogParameters();
                    param.Content = "Unable to load the selected template." + Environment.NewLine + "Please, check if the file is correctly formated.";
                    RadWindow.Alert(param);
                }
            }
        }

        private void Save()
        {
            if (this.SelectedTemplate != null)
            {
                var template = this.SelectedTemplate;
                var param = Settings.Current.GetDialogParameters();

                if (string.IsNullOrEmpty(template.Name))
                {
                    param.Content = "You must specify a name for your template.";
                    RadWindow.Alert(param);
                }
                else
                {
                    var path = Properties.Settings.Default.RepTemplates + "\\" + template.Name + ".xml";
                    if (File.Exists(path))
                    {
                        RadWindow.Confirm(template.Name + " already exists." + Environment.NewLine + "Do you want to overwrite it ?", OnSaveConfirm);
                    }
                    else
                    {
                        this.SaveTemplateAsFile(template, path);
                    }

                }
            }
        }

        private void SaveAs()
        {
            if (this.SelectedTemplate != null)
            {
                var template = this.SelectedTemplate;

                if (string.IsNullOrEmpty(template.Name))
                {
                    var param = Settings.Current.GetDialogParameters();
                    param.Content = "You must specify a name for your template.";
                    RadWindow.Alert(param);
                }
                else
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.InitialDirectory = Settings.Current.RepTemplates;
                    saveFileDialog.FileName = template.Name;
                    saveFileDialog.AddExtension = true;
                    saveFileDialog.Filter = "XML files (*.xml)|*.xml";
                    saveFileDialog.Title = "Save As ...";
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        this.SaveTemplateAsFile(template, saveFileDialog.FileName);
                    }

                }
            }
        }

        private void OnSaveConfirm(object sender, WindowClosedEventArgs e)
        {
            if (e.DialogResult.Value)
            {
                var template = this.SelectedTemplate;
                var path = Properties.Settings.Default.RepTemplates + "\\" + template.Name + ".xml";
                File.Delete(path);
                this.SaveTemplateAsFile(template, path);
            }
        }
        private void SaveTemplateAsFile(TemplateViewModel template, string path)
        {

            try
            {
                System.Xml.Serialization.XmlSerializer writer =
                     new System.Xml.Serialization.XmlSerializer(typeof(TemplateViewModel));

                using (FileStream reader = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                    writer.Serialize(reader, template);
                }

                var param = Settings.Current.GetDialogParameters();
                param.Content = template.Name + " successfully saved !";
                RadWindow.Alert(param);


            }
            catch (Exception)
            {
                var param = Settings.Current.GetDialogParameters();
                param.Content = "An error occured while saving the template ! Please, try again later.";
                RadWindow.Alert(param);
            }
        }

        private void CreateNewTemplate()
        {
            if (Settings.Current.CustomizeTemplates)
            {
                var param = Settings.Current.GetDialogParameters();
                param.CancelButtonContent = "No Thanks";
                param.OkButtonContent = "Yes";
                param.Content = "Do you want to configure your new template ?";
                param.Closed = OnCreateConfirm;
                RadWindow.Confirm(param);
            }
            else
            {
                var template = new TemplateViewModel();
                this.LstTemplates.Add(template);
                this.SelectedTemplate = template;
            }
        }
        private void OnCreateConfirm(object sender, WindowClosedEventArgs e)
        {
            var template = new TemplateViewModel();
            if (e.DialogResult != null && e.DialogResult.Value)
            {
                var createWindow = new I_CreateTemplate();
                createWindow.DataContext = template;
                createWindow.ShowDialog();
            }
            else
            {
                this.LstTemplates.Add(template);
                this.SelectedTemplate = template;
            }
        }
        public void ConfigureTemplate(TemplateViewModel template)
        {
            if (template != null)
            {
                this.LstTemplates.Add(template);
                this.SelectedTemplate = template;
            }
        }

        private void ShowOptions()
        {
            var optionsWindows = new I_Options();
            optionsWindows.ShowDialog();
        }

        private void ShowAbout()
        {
            var aboutWindows = new I_About();
            aboutWindows.ShowDialog();
        }


        public void EventCloseTemplate(TemplateViewModel template)
        {
            if (template != null)
            {
                this.SelectedTemplate = template;
                RadWindow.Confirm("Do you want to save " + template.Name + " before closing it ? ", OnCloseConfirm);

            }
        }
        private void OnCloseConfirm(object sender, WindowClosedEventArgs e)
        {
            if (e.DialogResult.Value)
            {
                this.Save();
            }
            this.CloseTemplate(this.SelectedTemplate);
        }
        private void CloseTemplate(TemplateViewModel template)
        {
            if (template != null)
            {
                this.LstTemplates.Remove(template);
            }
        }


        private void FullReport()
        {
            if (this.SelectedTemplate != null)
            {
                Report report = new FullReport();
                var source = new ObjectDataSource();
                source.DataSource = typeof(MainViewModel);
                source.DataMember = "GetDataSourceReport";
                report.DataSource = source;
                var reportSource = new InstanceReportSource() { ReportDocument = report };

                var reportWindow = new I_ReportViewer(this.SelectedTemplate.Name + " Full Report", reportSource, true, false,false);
                reportWindow.ShowDialog();
            }
        }

        private void SimplifiedReport()
        {
            if (this.SelectedTemplate != null)
            {
                Report report = new SimpleReport();
                var source = new ObjectDataSource();
                source.DataSource = typeof(MainViewModel);
                source.DataMember = "GetDataSourceReport";
                report.DataSource = source;
                var reportSource = new InstanceReportSource() { ReportDocument = report };

                var reportWindow = new I_ReportViewer(this.SelectedTemplate.Name + " Simplified Report", reportSource, false, false, false);
                reportWindow.ShowDialog();
            }
        }

        private void GemListReport()
        {
            if (this.SelectedTemplate != null)
            {
                Report report = new GemReport();
                var source = new ObjectDataSource();
                source.DataSource = typeof(MainViewModel);
                source.DataMember = "GetDataSourceReport";
                report.DataSource = source;
                var reportSource = new InstanceReportSource() { ReportDocument = report };

                var reportWindow = new I_ReportViewer(this.SelectedTemplate.Name + " Gems Report", reportSource, false, true, false);
                reportWindow.ShowDialog();
            }
        }

        private void GemMaterialsReport()
        {
            if (this.SelectedTemplate != null)
            {
                Report report = new MaterialsReport();
                var source = new ObjectDataSource();
                source.DataSource = typeof(MainViewModel);
                source.DataMember = "GetDataSourceReport";
                report.DataSource = source;
                var reportSource = new InstanceReportSource() { ReportDocument = report };

                var reportWindow = new I_ReportViewer(this.SelectedTemplate.Name + " Materials Report", reportSource, false, false, true);
                reportWindow.ShowDialog();
            }
        }




        [DataObjectMethod(DataObjectMethodType.Select)]
        public TemplateViewModel GetDataSourceReport()
        {
            var mainVM = ServiceLocator.Current.GetInstance<MainViewModel>();
            return mainVM.SelectedTemplate;
        }

        #endregion

    }
}
