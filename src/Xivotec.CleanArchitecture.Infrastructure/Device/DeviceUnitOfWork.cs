using Xivotec.CleanArchitecture.Application.Common.Device.Interface;
using Xivotec.CleanArchitecture.Infrastructure.Exceptions;

namespace Xivotec.CleanArchitecture.Infrastructure.Device;

///<inheritdoc cref="IDeviceUnitOfWork"/>
public class DeviceUnitOfWork : IDeviceUnitOfWork
{
    private readonly Dictionary<Type, object> _repositories = new();

    public DeviceUnitOfWork(IEnumerable<IDeviceServiceBase> deviceServices)
    {
        foreach (var deviceService in deviceServices)
        {
            AddDeviceServiceToDictionary(deviceService);
        }
    }

    public IDeviceService<TDevice> GetDeviceService<TDevice>()
    {
        var type = typeof(TDevice);
        if (!_repositories.TryGetValue(type, out var repository))
        {
            throw new DeviceServiceNotFoundException();
        }

        return (IDeviceService<TDevice>)repository;
    }

    private void AddDeviceServiceToDictionary(object repo)
    {
        var genArg = GetDeviceServiceGenericType(repo);

        if (genArg is not null)
        {
            _repositories.Add(genArg, repo);
        }
    }

    private Type? GetDeviceServiceGenericType(object repo)
    {
        var typ = repo.GetType();

        foreach (var intType in typ.GetInterfaces())
        {
            if (!intType.IsGenericType)
            {
                continue;
            }

            var genType = intType.GetGenericTypeDefinition();

            if (genType == typeof(IDeviceService<>))
            {
                return intType.GetGenericArguments()[0];
            }
        }
        return null;
    }
}