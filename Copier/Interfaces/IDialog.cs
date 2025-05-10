namespace Copier.Interfaces
{
    public interface IDialog
    {
        event EventHandler? OnCancel;

        event EventHandler? OnOk;
    }
}
