namespace Xivotec.CleanArchitecture.Presentation.Server.Services.Signal.Interfaces;

public interface ISignalHub<T>
{
    public Task SendMessageAsync(T message);
}