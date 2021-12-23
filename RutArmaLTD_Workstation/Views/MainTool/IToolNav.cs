using Avalonia.Controls;

namespace RutArmaLTD_Workstation.Views.MainTool
{
    public interface IToolNav
    {
        public UserControl MenuNavItem { get; }
        public UserControl ToolControl { get; }
    }
}
