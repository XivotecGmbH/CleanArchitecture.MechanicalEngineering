namespace Xivotec.CleanArchitecture.Presentation.Core.Services.Navigation;

public interface INavigationService
{
    /// <summary>
    /// Navigates to page given by viewmodel name as route.
    /// </summary>
    /// <param name="route">Viewmodel route to be navigated to.</param>
    public Task NavigateToAsync(string route);

    /// <summary>
    /// Navigates to page given by viewmodel name as route and sends data to the target viewmodel.
    /// </summary>
    /// <param name="route">Viewmodel route to be navigated to.</param>
    /// <param name="data">Data for the target viewmodel.</param>
    public Task NavigateToAsync(string route, object data);

    /// <summary>
    /// Navigates to page given by viewmodel name as route.
    /// </summary>
    /// <param name="route">Viewmodel route to be navigated to.</param>
    public Task NavigateToBaseAsync(string route);

    /// <summary>
    /// Navigates back to last visited page.
    /// </summary>
    public Task NavigateBackAsync();
}
