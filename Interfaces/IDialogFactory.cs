namespace Copier.Interfaces
{
    public interface IDialogFactory
    {
        public bool? ShowDialog<T>(T vm) where T : IDialog;
    }
}
