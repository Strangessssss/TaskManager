using System.IO;
using System.Text.Json;

namespace TaskManagaerBeta.Services;

public static class ButtonImageService
{
    private static readonly string PathToImages = "ButtonAppearance/Images/";
    private static readonly string JsonPath = "ButtonAppearance/ButtonImageFileNames.json";
    private static readonly string DefaultButtonPath = "grey_question.png";
    public static Dictionary<string, string>? ImagePaths = JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText(JsonPath));

    public static string GetButonImage(string buttonName)
    {
        try
        {
            return '/' + PathToImages + ImagePaths?[buttonName];
        }
        catch (Exception)
        {
            return '/' + DefaultButtonPath;
        }
        
    }
}