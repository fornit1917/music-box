namespace MusicBox.Domain.Auth
{
    public interface IAuthProcessFactory
    {
        IAuthProcess CreateAuthProcess(Service service);
    }
}
