
using Editor.Components.Interfaces;

namespace Editor.Components.AbstractClasses
{
    public class WindowMovable : Window
    {
        public int SelectedX { get; set; } = 0;
        public int SelectedY { get; set; } = 0;
        public int Width { get; set; } = 3;

        public Dictionary<(int x, int y), IComponent> GridComponents = new();

        public WindowMovable()
        {
            this.Keys[ConsoleKey.LeftArrow] = this.KeyLeft;
            this.Keys[ConsoleKey.RightArrow] = this.KeyRight;
        }

        public override void Draw()
        {
            int maxX = this.GridComponents.Keys.Max(k => k.x);
            int maxY = this.GridComponents.Keys.Max(k => k.y);

            for (int y = 0; y <= maxY; y++)
            {
                for (int x = 0; x <= maxX; x++)
                {
                    if (x == this.SelectedX && y == this.SelectedY)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }

                    if (this.GridComponents.TryGetValue((x, y), out IComponent? comp))
                    {
                        comp.Draw();
                    }
                    else
                    {
                        Console.Write("     ");
                    }

                    Console.ResetColor();
                }
                Console.WriteLine();
            }
        }

        public override void HandleKey(ConsoleKeyInfo info)
        {
            if (this.Keys.ContainsKey(info.Key))
            {
                this.Keys[info.Key].Invoke();
            }
            else if (this.GridComponents.ContainsKey((this.SelectedX, this.SelectedY))) 
            {
                this.GridComponents[(this.SelectedX, this.SelectedY)].HandleKey(info);
            }
        }

        public void FillLocations()
        {
            for (int i = 0; i < this.Components.Count; i++)
            {
                int x = i % this.Width;
                int y = i / this.Width;

                this.GridComponents[(x, y)] = this.Components[i];
            }
        }

        public override void KeyUp()
        {
            this.SelectedY = Math.Max(--this.SelectedY, 0);
        }

        public override void KeyDown()
        {
            int maxX = this.GridComponents.Keys.Max(k => k.x);

            this.SelectedY = Math.Min(++this.SelectedY, maxX);
        }

        public void KeyLeft()
        {
            this.SelectedX = Math.Max(--this.SelectedX, 0);
        }

        public void KeyRight()
        {
            int maxY = this.GridComponents.Keys.Max(k => k.y);

            this.SelectedX = Math.Min(++this.SelectedX, maxY);
        }
    }
}
