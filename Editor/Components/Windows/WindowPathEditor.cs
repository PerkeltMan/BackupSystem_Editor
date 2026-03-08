using Editor.Components.AbstractClasses;
using Editor.Components.Components;
using Editor.Components.Non_WindowComponents;

namespace Editor.Components.Windows
{
    public class WindowPathEditor : Window
    {
        private List<string> paths = new();
        public event Action<List<string>>? OnPathChanged;

        public WindowPathEditor(List<string> paths, Action<List<string>> save)
        {
            this.paths = paths;
            this.OnPathChanged = save;

            this.Keys[ConsoleKey.Tab] = this.Tab;
            this.Keys[ConsoleKey.Escape] = this.Exit;

            ListBox listBox = new ListBox(this.paths, (pathToDelete) => {
                this.paths.Remove(pathToDelete);
                });

            DirectoryBrowser directoryBrowser = new DirectoryBrowser((newPath) => {
                this.paths.Add(newPath);
                listBox.AddItem(newPath);
                });

            this.Components.Add(listBox);
            this.Components.Add(directoryBrowser);
        }

        public override void Draw()
        {
            for (int i = 0; i < this.Components.Count; i++)
            {
                if (i == this.SelectedComponent)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                this.Components[i].Draw();
                Console.ResetColor();
            }
        }

        private void Tab()
        {
            this.SelectedComponent ^= 1;
        }

        private void Exit()
        {
            string message = "Do you want to save the changes?";

            WindowChoose choose = new WindowChoose(message,
                ("Yes", () =>
                {   
                    this.OnPathChanged?.Invoke(this.paths);
                    this.RequestShutWindow();
                    this.RequestShutWindow();
                }),
                ("No", () => { this.RequestShutWindow(); this.RequestShutWindow(); } )
                );

            this.RequestCreateWindow(choose);
        }
    }
}

