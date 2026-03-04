using Editor.Components.Interfaces;
using Editor.Model;

namespace Editor.Components.AbstractClasses
{
    public abstract class Window
    {
        public Application Application { get; set; } = null!;
        public int SelectedComponent { get; set; } = 0;
        public Dictionary<ConsoleKey, Action> Keys { get; set; } = new();
        public List<IComponent> Components { get; set; } = new();

        public void HandleKey(ConsoleKeyInfo keyInfo)
        {
            if (this.Keys.ContainsKey(keyInfo.Key))
            {
                this.Keys[keyInfo.Key].Invoke();
            }
            else
            {
                this.Components[this.SelectedComponent].HandleKey(keyInfo);
            }
        }

        public virtual void Draw()
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
    }
}
