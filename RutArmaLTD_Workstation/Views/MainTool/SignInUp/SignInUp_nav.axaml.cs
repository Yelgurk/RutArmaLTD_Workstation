using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace RutArmaLTD_Workstation.Views.MainTool.SignInUp
{
    public partial class SignInUp_nav : UserControl, IToolNav
    {
        public UserControl MenuNavItem { get => this; }
        public UserControl ToolControl { get; } = new SignInUp_tool();

        public SignInUp_nav()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
