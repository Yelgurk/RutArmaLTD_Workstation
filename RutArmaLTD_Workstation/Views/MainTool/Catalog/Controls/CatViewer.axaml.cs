using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using RutArmaLTD_Workstation.ViewModels;

namespace RutArmaLTD_Workstation.Views.MainTool.Catalog.Controls
{
    public partial class CatViewer : UserControl
    {
        public CatViewer()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public void StartEditing(object sender, RoutedEventArgs e) => NavigationVM.CategorySlider.State = !NavigationVM.CategorySlider.State;
    }
}
