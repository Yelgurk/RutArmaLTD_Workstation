using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace RutArmaLTD_Workstation.Views.MainTool.Account
{
    public partial class Account_nav : UserControl, IToolNav
    {
        public UserControl MenuNavItem { get => this; }
        public UserControl ToolControl { get; } = new Account_tool();

        public Account_nav()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
