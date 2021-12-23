using ReactiveUI;

namespace RutArmaLTD_Workstation.Models
{
    public class BoolProperty : ReactiveObject
    {
        public BoolProperty(bool state = true) => this.state = state;

        private bool state;
        public bool State
        {
            get => state;
            set => this.RaiseAndSetIfChanged(ref this.state, value);
        }
    }
}
