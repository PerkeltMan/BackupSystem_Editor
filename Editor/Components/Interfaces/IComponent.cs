using Newtonsoft.Json.Linq;

namespace Editor.Components.Interfaces
{
    public interface IComponent
    {
        public bool HandleKey(ConsoleKeyInfo info);
        public void Draw();
    }
}
