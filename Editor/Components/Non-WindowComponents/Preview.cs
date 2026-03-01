using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Editor.Components.Non_WindowComponents
{
    public class Preview : IComponent
    {
        private string name;

        public Preview(string name)
        {
            this.name = name;
        }

        public void Draw()
        {
            Console.Write($"""
                ┌{"".PadRight(Console.WindowWidth - 2, '─')}┐
                {"│".PadRight(2, ' ')}{this.name}{"│".PadLeft(Console.WindowWidth - 2 - this.name.Length)}
                └{"".PadRight(Console.WindowWidth - 2, '─')}┘
                """
                );
        }

        
        public void HandleKey(ConsoleKeyInfo info)
        {
            throw new NotImplementedException();
        }
    }
}
