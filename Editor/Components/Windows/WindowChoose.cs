using System.Text;
using Editor.Components.AbstractClasses;
using Editor.Components.Components;

namespace Editor.Components.Windows
{
    public class WindowChoose : Window
    {
        //┘└┌┐│─

        private string message;

        public WindowChoose(string text, params (string label, Action action)[] options)
        {
            this.Keys[ConsoleKey.Tab] = Tab;
            this.message = text;

            foreach (var option in options)
            {
                Button button = new Button(option.label);
                button.Clicked += option.action;
                this.Components.Add(button);
            }
        }

        public override void Draw()
        {
            Console.WriteLine(this.message);
            base.Draw();
        }

        private void Tab()
        {
            this.SelectedComponent = (this.SelectedComponent + 1) % this.Components.Count;
        }
    }
}
