using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace RutArmaLTD_Workstation.Views.MainTool.RootMgmt
{
    public partial class RootMgmt_nav : UserControl, IToolNav
    {
        public UserControl MenuNavItem { get => this; }
        public UserControl ToolControl { get; } = new RootMgmt_tool();

        public RootMgmt_nav()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
