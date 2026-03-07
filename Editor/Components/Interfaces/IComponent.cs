using Newtonsoft.Json.Linq;

namespace Editor.Components.Interfaces
{
    public interface IComponent
    {
        public void HandleKey(ConsoleKeyInfo info);
        public void Draw();
    }
}
