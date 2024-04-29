using System.Text;

namespace Application.Helpers;

public static class OtpHelper
{
    public static string GenerateRandomString(int length)
    {
        var random = new Random();
        var sb = new StringBuilder();
        for (var i = 0; i < length; i++)
        {
            sb.Append(random.Next(10));
        }
        return sb.ToString();
    }
}