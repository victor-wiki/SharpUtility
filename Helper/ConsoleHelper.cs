namespace SharpUtility.Helper
{
    public class ConsoleHelper
    {
        public static void Info(string message)
        {
            WriteMessage(message, ConsoleColor.White);
        }

        public static void Warn(string message)
        {
            WriteMessage(message, ConsoleColor.Yellow);
        }

        public static void Error(string message)
        {
            WriteMessage(message, ConsoleColor.Red);
        }

        public static void WriteMessage(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
