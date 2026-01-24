using System.Text;

namespace ExInspectorBackend.Middleware
{
    public static class FormatAPI
    {
        public static string FormatString(string stringToFormat)
        {

            byte xorConstant = 0x53;

            byte[] data = Convert.FromBase64String(stringToFormat);
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = (byte)(data[i] ^ xorConstant);
            }
            string formatText = Encoding.UTF8.GetString(data);

            return formatText;

        }
    }
}
