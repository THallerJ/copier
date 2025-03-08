namespace Copier.Interfaces
{
    public interface ISubmittableDialog
    {
        public event EventHandler? OnCancel;

        public event EventHandler? OnOk;
    }
}
