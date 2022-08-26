namespace ROASApp.Data.IO
{
    public class FileOperation
    {
        private const string filePath = "data.txt";
        public static void Write(string data)
        {
            File.WriteAllText(filePath, data);
        }
        public static string Read()
        {
            if(File.Exists(filePath))
                return File.ReadAllText(filePath);
            else
                return null;
        }
    }
}