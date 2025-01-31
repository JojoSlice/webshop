using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace webshop.Helpers
{
    internal class WindowManager
    {
        public string Header { get; set; }
        public int Left { get; set; }
        public int Top { get; set; }
        public List<string> TextRows { get; set; }


        int windowWidth = Console.WindowWidth;

        public WindowManager(string header, int left, int top, List<string> textRows)
        {
            Header = header;
            Left = left;
            Top = top;
            TextRows = textRows;
        }

        public void Draw()
        {
            var width = TextRows.OrderByDescending(t => t.Length).FirstOrDefault().Length;

            if (width < Header.Length + 4)
            {
                width = Header.Length + 4;
            }

            if (Header == "Welcome to")
            {
                width = windowWidth - 4;
            }

            Console.SetCursorPosition(Left, Top);
            if (Header == "Welcome to")
            {
                Console.Write('╔' + new String('═', (width / 2) - Header.Length) + " ");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write(Header);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" " + new String('═', width / 2) + '╗');
            }
            else if (Header != "")
            {
                Console.Write('╔' + " ");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write(Header);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" " + new String('═', width - Header.Length) + '╗');
            }
            else
            {
                Console.Write('╔' + new String('═', width + 2) + '╗');
            }

            for (int i = 0; i < TextRows.Count; i++)
            {
                Console.SetCursorPosition(Left, Top + i + 1);
                Console.WriteLine('║' + " " + TextRows[i] + new String(' ', width - TextRows[i].Length + 1) + '║');
            }

            Console.SetCursorPosition(Left, Top + TextRows.Count + 1);
            Console.Write('╚' + new String('═', width + 2) + '╝');

            if (Lowest.LowestPosition < Top + TextRows.Count + 2)
            {
                Lowest.LowestPosition = Top + TextRows.Count + 2;
            }

            Console.SetCursorPosition(0, Lowest.LowestPosition);
        }
    }

    public static class Lowest
    {
        public static int LowestPosition { get; set; }
    }

}
