using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WpfAppTest.Helpers
{
    class ColorHelper
    {
        public static SolidColorBrush BrushFromHEX(string hex)
        {
            return (SolidColorBrush)(new BrushConverter().ConvertFrom(hex));
        }
    }
}
