using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace RutArmaLTD_Workstation.Views.MainTool.Catalog
{
    public partial class CatEdit_nav : UserControl, IToolNav
    {
        public UserControl MenuNavItem { get => this; }
        public UserControl ToolControl { get; } = new CatEdit_tool();

        public CatEdit_nav()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
