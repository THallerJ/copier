namespace Copier.Interfaces
{
    public interface IDialogFactory
    {
        public bool? ShowDialog<T>() where T : IDialog;
    }
}
