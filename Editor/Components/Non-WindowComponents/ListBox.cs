using Editor.Components.Components;
using Editor.Components.Interfaces;

namespace Editor.Components.Non_WindowComponents
{
    public class ListBox : IComponent
    {
        Dictionary<ConsoleKey, Action> keys { get; set; } = new();

        private List<IComponent> items = new();
        private int selectedIndex = 0;

        public event Action<string>? ItemSelected;

        public ListBox(List<string> itemsToShow, Action<string> itemSelected)
        {
            this.ItemSelected = itemSelected;

            this.keys[ConsoleKey.UpArrow] = this.KeyUp;
            this.keys[ConsoleKey.DownArrow] = this.KeyDown;

            foreach (string item in itemsToShow)
            {
                Button button = new Button(item, () => this.ItemSelected?.Invoke(item));
                button.Clicked += () =>
                {
                    this.RemoveItem(item);
                    this.ItemSelected?.Invoke(item);
                };
                this.items.Add(button);
            }
        }

        public void Draw()
        {
            for (int i = 0; i < this.items.Count; i++)
            {
                if (i == this.selectedIndex)
                    Console.ForegroundColor = ConsoleColor.Green;

                this.items[i].Draw();

                Console.ResetColor();
            }
        }

        public void HandleKey(ConsoleKeyInfo key)
        {
            if (this.keys.ContainsKey(key.Key))
            {
                this.keys[key.Key].Invoke();
            }
            else if (this.items.Count > 0)
            {
                this.items[selectedIndex].HandleKey(key);
            }
            else return;
        }

        private void KeyUp()
        {
            this.selectedIndex = Math.Max(--this.selectedIndex, 0);
        }

        private void KeyDown()
        {
            this.selectedIndex = Math.Min(++this.selectedIndex, this.items.Count - 1);
        }

        public void RemoveItem(string item)
        {
            this.items.RemoveAll(button => button.ToString() == item);
            this.selectedIndex = 0;
        }

        public void AddItem(string item)
        {
            if (this.items.Any(button => button.ToString() == item))
                return;

            Button button = new Button(item, () => this.ItemSelected?.Invoke(item));
            this.items.Add(button);
        }
    }
}
