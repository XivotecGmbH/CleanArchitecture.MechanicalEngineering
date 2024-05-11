namespace Xivotec.CleanArchitecture.Infrastructure.Exceptions;

public sealed class RecipeImportException : Exception
{
    public RecipeImportException()
    {
    }

    public RecipeImportException(string message)
        : base(message)
    {
    }

    public RecipeImportException(string message, Exception exception)
        : base(message, exception)
    {
    }
}
