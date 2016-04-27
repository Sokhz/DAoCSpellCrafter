using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace DaocSpellCraftCalculator.ViewModels
{
    class ViewModelLocator
    {
        public MainViewModel MainVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public ChooseItemViewModel ChooseItemVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ChooseItemViewModel>();
            }
        }

        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<ChooseItemViewModel>();
        }

    }
}
