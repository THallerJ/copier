namespace Copier.Interfaces
{
    public interface IDialog
    {
        public event EventHandler? OnCancel;

        public event EventHandler? OnOk;
    }
}
