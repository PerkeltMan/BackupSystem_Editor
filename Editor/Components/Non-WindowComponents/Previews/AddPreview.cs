using Editor.Components.AbstractClasses;

namespace Editor.Components.Non_WindowComponents
{
    public class AddPreview : Preview
    {
        public event Action? NewJob;
        private string displayName = "add";

        public override void Draw()
        {
            Console.Write($"""
                ┌{"".PadRight(Console.WindowWidth - 2, '─')}┐
                {"│".PadRight(2, ' ')}{this.displayName}{"│".PadLeft(Console.WindowWidth - 2 - this.displayName.Length)}
                └{"".PadRight(Console.WindowWidth - 2, '─')}┘
                """
                );
        }

        public override void HandleKey(ConsoleKeyInfo info)
        {
            if (info.Key == ConsoleKey.Spacebar)
            {
                this.NewJob?.Invoke();
            }
        }        
    }
}
