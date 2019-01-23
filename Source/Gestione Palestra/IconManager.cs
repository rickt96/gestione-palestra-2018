using System; using GestionePalestra.MVC;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionePalestra
{
    public static class IconManager
    {
        static string _iconPath = "/Gestione Palestra;component/Resources/Icons/";

        public static string GetIcon(string iconName)
        {
            return _iconPath + iconName;
        }
    }
}
