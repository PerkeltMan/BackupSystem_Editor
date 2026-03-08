using Editor.Components.AbstractClasses;
using IComponent = Editor.Components.Interfaces.IComponent;

namespace Editor.Components.Non_WindowComponents
{
    public class DirectoryBrowser : IComponent
    {
        private int selectedDirectoryIndex = 0;
        private string currentPath = string.Empty;
        private List<string> directories { get; set; } = new();
        private Dictionary<ConsoleKey, Action> keys { get; set; } = new();

        public event Action<string>? DirectorySelected;

        public DirectoryBrowser(Action<string> action)
        {
            this.DirectorySelected = action;

            this.currentPath = Directory.GetLogicalDrives()[0];
            this.directories = Directory.GetLogicalDrives().ToList();

            this.keys[ConsoleKey.Spacebar] = this.SelectDirectory;
            this.keys[ConsoleKey.UpArrow] = this.KeyUp;
            this.keys[ConsoleKey.DownArrow] = this.KeyDown;
            this.keys[ConsoleKey.LeftArrow] = this.Surface;
            this.keys[ConsoleKey.RightArrow] = this.GoDeeper;
        }

        public void Draw()
        {
            for (int i = 0; i < this.directories.Count; i++)
            {
                if (i == this.selectedDirectoryIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }

                Console.WriteLine(this.directories[i]);
                Console.ResetColor();
            }
        }

        public void HandleKey(ConsoleKeyInfo info)
        {
            if (this.keys.ContainsKey(info.Key))
            {
                this.keys[info.Key].Invoke();
            }
        }

        private void SelectDirectory()
        {
            this.DirectorySelected?.Invoke(this.directories[this.selectedDirectoryIndex]);
        }

        private void KeyUp()
        {
            this.selectedDirectoryIndex = Math.Max(--this.selectedDirectoryIndex, 0);
        }

        private void KeyDown()
        {
            this.selectedDirectoryIndex = Math.Min(++this.selectedDirectoryIndex, this.directories.Count - 1);
        }

        private void Surface()
        {
            DirectoryInfo parent = Directory.GetParent(this.currentPath)!;

            if (parent != null)
            {
                this.currentPath = parent.FullName;
                this.Load(this.currentPath);
            }
            else
            {
                this.directories = Directory.GetLogicalDrives().ToList();
            }
        }

        private void GoDeeper()
        {
            if (this.directories.Count == 0)
                return;
            string selectedDirectory = this.directories[this.selectedDirectoryIndex];
            this.selectedDirectoryIndex = 0;
            this.currentPath = selectedDirectory;
            this.Load(selectedDirectory);
        }

        private void Load(string dir)
        {
            try 
            {
                if (Directory.GetDirectories(dir).Length == 0)
                {
                    return;
                }

                this.directories = Directory.GetDirectories(dir).ToList();

                this.selectedDirectoryIndex = 0;
            }
            catch
            {
                return;
            }
        }
    }
}
