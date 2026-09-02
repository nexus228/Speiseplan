using Speiseplan.Model;
using Speiseplan.Services.Enum;

namespace Speiseplan.Services.CustomEventArgs
{
   
    public class MenusChangedEventArgs : EventArgs
    {
        public MenuChangeType ChangeType { get; }
        public Menu? AffectedMenu { get; }

        public MenusChangedEventArgs(MenuChangeType changeType, Menu? affectedMenu = null)
        {
            ChangeType = changeType;
            AffectedMenu = affectedMenu;
        }
    }
}
