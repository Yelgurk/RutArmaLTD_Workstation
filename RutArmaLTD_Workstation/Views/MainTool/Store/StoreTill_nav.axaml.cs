using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace RutArmaLTD_Workstation.Views.MainTool.Store
{
    public partial class StoreTill_nav : UserControl, IToolNav
    {
        public UserControl MenuNavItem { get => this; }
        public UserControl ToolControl { get; } = new StoreTill_tool();

        public StoreTill_nav()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
