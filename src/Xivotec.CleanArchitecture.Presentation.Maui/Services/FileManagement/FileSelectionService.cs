using Xivotec.CleanArchitecture.Presentation.Core.Services.FileManagement;

namespace Xivotec.CleanArchitecture.Presentation.Maui.Services.FileManagement;

public class FileSelectionService : IFileSelectionService
{
    public async Task<string> SelectFilePath()
    {
        var result = await FilePicker.PickAsync(new PickOptions());
        return result is null ? "" : result.FullPath;
    }
}