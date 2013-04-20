using System.Threading;
using System.Threading.Tasks;

namespace Presentation.Console
{
    public static class ConsoleHelper
    {
        public static void WriteUntilComplete(Task task, string msg, int numDots = 3, int delay = 100)
        {
            System.Console.WriteLine();
            System.Console.Write(msg);
            var count = 0;
            while (task.IsCompleted == false)
            {
                count++;
                OverwriteConsoleMessage(msg + GetDots(count));
                Thread.Sleep(100);
                if (count == 3)
                    count = 0;
            }
        }

        public static void OverwriteConsoleMessage(string message)
        {
            System.Console.CursorLeft = 0;
            int maxCharacterWidth = System.Console.WindowWidth - 1;
            if (message.Length > maxCharacterWidth)
                message = message.Substring(0, maxCharacterWidth - 3) + "...";
            message = message + new string(' ', maxCharacterWidth - message.Length);
            System.Console.Write(message);
        }

        private static string GetDots(int count)
        {
            var dots = "";
            for (var i = 0; i < count; i++)
                dots += '.';
            return dots;
        }
    }
}
