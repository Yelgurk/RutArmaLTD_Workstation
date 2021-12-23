using ReactiveUI;
using RutArmaLTD_Workstation.Views.MainTool;
using RutArmaLTD_Workstation.Views.MainTool.Account;
using RutArmaLTD_Workstation.Views.MainTool.Catalog;
using RutArmaLTD_Workstation.Views.MainTool.RootMgmt;
using RutArmaLTD_Workstation.Views.MainTool.SignInUp;
using RutArmaLTD_Workstation.Views.MainTool.Stock;
using RutArmaLTD_Workstation.Views.MainTool.Store;
using System.Collections.ObjectModel;

namespace RutArmaLTD_Workstation.ViewModels
{
    public class NavigationVM : ReactiveObject
    {
        private static SignInUp_nav SIUN { get; } = new SignInUp_nav();
        private static StockState_nav SSN { get; } = new StockState_nav();
        private static CatEdit_nav CEN { get; } = new CatEdit_nav();
        private static RootMgmt_nav AMN { get; } = new RootMgmt_nav();
        private static Account_nav ACCN { get; } = new Account_nav();
        private static StoreTill_nav STN { get; } = new StoreTill_nav();

        public NavigationVM()
        {
            AddControl(SIUN);
            AddControl(SSN);
            AddControl(CEN);
            AddControl(AMN);
            AddControl(ACCN);
            AddControl(STN);
        }

        private IToolNav tool = null;
        public IToolNav SelectedTool
        {
            get => tool;
            set => this.RaiseAndSetIfChanged(ref this.tool, value);
        }

        public static ObservableCollection<IToolNav> ToolsNav { get; } = new ObservableCollection<IToolNav>();

        private void AddControl(IToolNav Control)
        {
            if (!ToolsNav.Contains(Control))
                ToolsNav.Add(Control);
        }
    }
}
