
namespace DragoSharp.Views;

public static class Component
{
    public static string GetProgressBar(int current, int total, int barLength = 30)
    {
        float progress = (float)current / total;
        int filledLength = (int)(barLength * progress);
        string bar = new string('█', filledLength) + new string('-', barLength - filledLength);
        return $"<{bar}> {current}/{total}";
    }

    public static void DisplayProgressBar(int current, int total, int barLength = 30)
    {
        Console.Write(GetProgressBar(current, total, barLength));
    }
}