using DaocSpellCraftCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DaocSpellCraftCalculator.Tools.Binding
{

    public class SlotNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (string.IsNullOrEmpty((string)value))
            {
                string slotCode = (string)parameter;
                SlotModel slot = DataProvider.Current.Jeweleries.FirstOrDefault(o => o.Code == slotCode);
                if (slot == null)
                    slot = DataProvider.Current.Armors.FirstOrDefault(o => o.Code == slotCode);
                if (slot == null)
                    slot = DataProvider.Current.Weapons.FirstOrDefault(o => o.Code == slotCode);
                if (slot != null)
                    return slot.Full;
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
