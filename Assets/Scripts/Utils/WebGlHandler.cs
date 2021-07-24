using System.Runtime.InteropServices;
public class WebGlHandler
{
    [DllImport("__Internal")]
    public static extern bool IsMobileBrowser();
}