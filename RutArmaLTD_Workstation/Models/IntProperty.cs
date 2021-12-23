using ReactiveUI;

namespace RutArmaLTD_Workstation.Models
{
    public class IntProperty : ReactiveObject
    {
        private double value = 100;
        public double Value
        {
            get => value;
            set => this.RaiseAndSetIfChanged(ref this.value, value);
        }
    }
}
