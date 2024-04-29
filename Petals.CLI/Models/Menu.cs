namespace Petals.CLI.Models
{
    public abstract class Menu
    {
        public abstract string Label { get; }
        public abstract void OnSelect();
    }
}