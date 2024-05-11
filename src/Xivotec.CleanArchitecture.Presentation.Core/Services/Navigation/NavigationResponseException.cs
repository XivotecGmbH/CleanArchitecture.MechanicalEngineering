namespace Xivotec.CleanArchitecture.Presentation.Core.Services.Navigation;

/// <summary>
/// Exception for navigation response failures.
/// </summary>
public class NavigationResponseException : Exception
{
    public NavigationResponseException()
    {
    }

    public NavigationResponseException(string message) : base(message)
    {
    }

    public NavigationResponseException(string message, Exception exception)
        : base(message, exception)
    {
    }
}