namespace Xivotec.CleanArchitecture.Presentation.Core.Services.FileManagement;

public interface IFileSelectionService
{
    public Task<string> SelectFilePath();
}