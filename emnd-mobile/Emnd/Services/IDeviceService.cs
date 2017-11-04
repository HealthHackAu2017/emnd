namespace Emnd
{
    public interface IDeviceService
    {
        string GetFullPath(string folderPath, string filename);
        bool AppendBytes(string folderPath, string fileName, byte[] bytes);
        bool DeleteFile(string folderPath, string filename);
        string OpenLocalFile(string filePath);
        
        double CalculateWidth(string text);
				
        string DeviceIdentifier();
    }
}