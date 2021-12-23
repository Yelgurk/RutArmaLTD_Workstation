using ReactiveUI;

namespace RutArmaLTD_Workstation.Models
{
    public class BoolProperty : ReactiveObject
    {
        private bool state = true;
        public bool State
        {
            get => state;
            set => this.RaiseAndSetIfChanged(ref this.state, value);
        }
    }
}
