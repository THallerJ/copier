namespace Copier.Interfaces
{
    public interface IDialogFactory
    {
        bool? ShowDialog<T>() where T : IDialog;
    }
}
