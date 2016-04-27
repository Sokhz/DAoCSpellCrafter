using DaocSpellCraftCalculator.Models;
using DaocSpellCraftCalculator.Models.BDO;
using DaocSpellCraftCalculator.Tools;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Serialization;
using Telerik.Windows.Controls;

namespace DaocSpellCraftCalculator.ViewModels
{
    public class ChooseItemViewModel : GalaSoft.MvvmLight.ViewModelBase, IRequestCloseViewModel
    {

        #region Constructor

        #endregion


        #region Properties

        private string _Slot;
        public string Slot
        {
            get { return _Slot; }
            set { if (_Slot != value) { _Slot = value; RaisePropertyChanged("Slot"); OnSlotChanged(); } }
        }
        public bool IsArmorSlot
        {
            get { return DataProvider.Current.Armors.Select(o => o.Code).Contains(this.Slot); }
        }


        private RealmModel _Realm;
        public RealmModel Realm
        {
            get { return _Realm; }
            set { if (_Realm != value) { _Realm = value; RaisePropertyChanged("Realm"); } }
        }


        private ClassModel _Class;
        public ClassModel Class
        {
            get { return _Class; }
            set { if (_Class != value) { _Class = value; RaisePropertyChanged("Class"); } }
        }
        public string EpicName
        {
            get { return this.Class.Epic; }
        }


        private string _Filter;
        public string Filter
        {
            get { return _Filter; }
            set { if (_Filter != value) { _Filter = value; RaisePropertyChanged("Filter"); OnFilterChanged(); } }
        }


        private List<FileInfo> LstAllItems { get; set; }
        private ObservableCollection<FileInfo> _LstItems;
        public ObservableCollection<FileInfo> LstItems
        {
            get { return _LstItems; }
            set { if (_LstItems != value) { _LstItems = value; RaisePropertyChanged("LstItems"); } }
        }


        private FileInfo _SelectedItem;
        public FileInfo SelectedItem
        {
            get { return _SelectedItem; }
            set { if (_SelectedItem != value) { _SelectedItem = value; RaisePropertyChanged("SelectedItem"); OnSelectedItemChanged(); } }
        }


        private ItemViewModel _DetailItem;
        public ItemViewModel DetailItem
        {
            get { return _DetailItem; }
            set { if (_DetailItem != value) { _DetailItem = value; RaisePropertyChanged("DetailItem"); } }
        }


        private static List<PartialItemViewModel> _loadedItems;
        public static List<PartialItemViewModel> loadedItems
        {
            get { return _loadedItems ?? (_loadedItems = new List<PartialItemViewModel>()); }
        }


        #endregion


        #region Commands



        private RelayCommand _LoadItemCommand;
        public RelayCommand LoadItemCommand
        {
            get
            {
                return _LoadItemCommand ?? (_LoadItemCommand = new RelayCommand(LoadItem));
            }
        }


        private RelayCommand _DeleteItemCommand;
        public RelayCommand DeleteItemCommand
        {
            get
            {
                return _DeleteItemCommand ?? (_DeleteItemCommand = new RelayCommand(DeleteItem));
            }
        }




        #endregion


        #region Methods

        private void InitItems()
        {
            this.LstAllItems = new List<FileInfo>();

            SlotModel slot = DataProvider.Current.Jeweleries.FirstOrDefault(o => o.Code == this.Slot);
            if (slot == null)
                slot = DataProvider.Current.Armors.FirstOrDefault(o => o.Code == this.Slot);
            if (slot == null)
                slot = DataProvider.Current.Weapons.FirstOrDefault(o => o.Code == this.Slot);

            if (slot != null)
            {
                var repAll = Settings.Current.RepItems + "\\All\\" + slot.RepName;
                var repRealm = Settings.Current.RepItems + "\\" + this.Realm.Full + "\\" + slot.RepName;

                var itemsAll = new List<FileInfo>();
                var itemsRealm = new List<FileInfo>();

                if (Directory.Exists(repAll))
                {
                    var dirAll = new DirectoryInfo(repAll);
                    itemsAll = dirAll.GetFiles("*.xml").ToList();
                }
                if (Directory.Exists(repRealm))
                {
                    var dirRealm = new DirectoryInfo(repRealm);
                    itemsRealm = dirRealm.GetFiles("*.xml").ToList();
                }

                this.LstAllItems = itemsAll.Union(itemsRealm).Distinct().OrderBy(o => o.Name).ToList();
                this.FilterItems();

            }
        }

        private void InitEpicItems()
        {

        }

        private void FilterItems()
        {
            var items = this.LstAllItems;
            if (!string.IsNullOrEmpty(this.Filter))
            {
                items = items.Where(o => o.Name.ToLower().Contains(this.Filter.ToLower())).ToList();
            }
            this.LstItems = new ObservableCollection<FileInfo>(items);
        }

        private void LoadSelectedItem()
        {
            if (this.SelectedItem != null)
            {
                var path = this.SelectedItem.FullName;
                this.DetailItem = this.LoadItem(path);
            }
        }

        private PartialItemViewModel LoadPartialItem(string file)
        {
            var item = ChooseItemViewModel.loadedItems.FirstOrDefault(o => o.PathName == file);
            if (item == null && File.Exists(file))
            {
                item = this.LoadPartialInternalItem(file);
                if (item == null)
                    item = this.LoadPartialBdoItem(file);
                if (item != null)
                    ChooseItemViewModel.loadedItems.Add(item);
            }
            return item;
        }
        private PartialItemViewModel LoadPartialInternalItem(string file)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(PartialItemViewModel));
                using (FileStream reader = new FileStream(file, FileMode.Open))
                {
                    var item = (PartialItemViewModel)serializer.Deserialize(reader);
                    item.PathName = file;
                    return item;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        private PartialItemViewModel LoadPartialBdoItem(string file)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Bdo_PartialItemModel));
                using (FileStream reader = new FileStream(file, FileMode.Open))
                {
                    var item = (Bdo_PartialItemModel)serializer.Deserialize(reader);
                    if (item != null)
                    {
                        var myItem = item.ConvertToInternalItem();
                        myItem.PathName = file;
                        return myItem;
                    }
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }


        private ItemViewModel LoadItem(string file)
        {
            if (File.Exists(file))
            {
                var item = this.LoadInternalItem(file);
                if (item == null)
                    item = this.LoadBdoItem(file);
                return item;
            }
            return null;
        }
        private ItemViewModel LoadInternalItem(string file)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ItemViewModel));
                using (FileStream reader = new FileStream(file, FileMode.Open))
                {
                    var item = (ItemViewModel)serializer.Deserialize(reader);
                    item.PathName = file;
                    item.Slot = this.Slot;
                    return item;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        private ItemViewModel LoadBdoItem(string file)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Bdo_ItemModel));
                using (FileStream reader = new FileStream(file, FileMode.Open))
                {
                    var item = (Bdo_ItemModel)serializer.Deserialize(reader);
                    if (item != null)
                    {
                        var myItem = item.ConvertToInternalItem(this.Slot);
                        myItem.PathName = file;
                        return myItem;
                    }
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }


        private void LoadItem()
        {
            if (this.DetailItem != null)
            {
                var template = ServiceLocator.Current.GetInstance<MainViewModel>().SelectedTemplate;
                if (template != null)
                    this.DetailItem.IsEquiped = true;
                template.SetItem(this.DetailItem);
                RequestClose(this, new EventArgs());
            }

        }

        private void DeleteItem()
        {
            if (this.SelectedItem != null)
                RadWindow.Confirm("Do you really want to delete this item ?", OnDeleteConfirm);
        }
        private void OnDeleteConfirm(object sender, WindowClosedEventArgs e)
        {
            if (e.DialogResult.Value)
            {
                try
                {
                    File.Delete(this.SelectedItem.FullName);
                }
                catch (Exception)
                {
                    var param = Settings.Current.GetDialogParameters();
                    param.Content = "An error occured while deleting the item ! Please, try again later later.";
                    RadWindow.Alert(param);
                }
                this.InitItems();
            }
        }



        #endregion


        #region Events


        public event EventHandler RequestClose;

        private void OnSlotChanged()
        {
            this.InitItems();
        }

        private void OnFilterChanged()
        {
            this.FilterItems();
        }

        private void OnSelectedItemChanged()
        {
            this.LoadSelectedItem();
        }

        public void OnItemDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.LoadItem();
        }

        #endregion
    }
}
