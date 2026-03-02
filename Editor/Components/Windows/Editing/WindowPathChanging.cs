using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Editor.Components.AbstractClasses;

namespace Editor.Components.Windows.Editing
{ // THIS WINDOW IS NOT IN USE ANYMORE, I DECIDED TO MAKE PATH CHANGING A COMPONENT OF THE EDIT WINDOW INSTEAD OF A SEPERATE WINDOW (aspoň chci, ale copilot predictuje dobře)
    public class WindowPathChanging : Window
    {
        private string label = "Sources";
        private int selectedIndex = 0;
        private List<string> paths;
        private List<IComponent> components;
        public WindowPathChanging(List<string> paths)
        {
            this.paths = paths;
        }
        public override void Draw()
        {
            Console.WriteLine(this.label);

            for (int i = 0; i < this.paths.Count; i++)
            {
                if (i == selectedIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }

                Console.WriteLine(this.paths[i]);

                Console.ResetColor();
            }
        }

        public override void HandleKey(ConsoleKeyInfo keyInfo)
        {
            throw new NotImplementedException();
        }
    }
}
