namespace Xivotec.CleanArchitecture.Application.Common.Device.Interface;

/// <summary>
/// Device unit of work. Provides device services.
/// </summary>
public interface IDeviceUnitOfWork
{
    /// <summary>
    /// Returns Device Service specified by <see cref="TDevice"/>.
    /// </summary>
    /// <typeparam name="TDevice">Type of service the repository contains.</typeparam>
    public IDeviceService<TDevice> GetDeviceService<TDevice>();
}