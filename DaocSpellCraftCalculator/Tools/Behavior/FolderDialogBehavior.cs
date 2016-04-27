using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Interactivity;

namespace DaocSpellCraftCalculator.Tools.Behavior
{
    public class FolderDialogBehavior : Behavior<System.Windows.Controls.Button>
    {
        public string SetterName { get; set; }

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Click += OnClick;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.Click -= OnClick;
        }

        private void OnClick(object sender, RoutedEventArgs e)
        {
            var propertyInfo = AssociatedObject.DataContext.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(p => p.CanRead && p.CanWrite)
                .Where(p => p.Name.Equals(SetterName))
                .First();

            if (propertyInfo == null)
                throw new Exception("Invalid property name for dialog chooser");

            var dialog = new FolderBrowserDialog();
            dialog.ShowNewFolderButton = true;
            dialog.SelectedPath = propertyInfo.GetValue(AssociatedObject.DataContext).ToString();
            var result = dialog.ShowDialog();
            if (result == DialogResult.OK && AssociatedObject.DataContext != null)
            {
                propertyInfo.SetValue(AssociatedObject.DataContext, dialog.SelectedPath, null);
            }
        }
    }
}
