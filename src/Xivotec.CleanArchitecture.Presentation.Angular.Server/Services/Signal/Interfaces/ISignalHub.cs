namespace Xivotec.CleanArchitecture.Presentation.Angular.Server.Services.Signal.Interfaces;

public interface ISignalHub<T>
{
    public Task SendMessageAsync(T message);
}