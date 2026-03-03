using Editor.Components.AbstractClasses;
using Editor.Components.Components;

namespace Editor.Components.Windows
{
    public class WindowChoose : Window
    {
        private string _message;

        public event Action? Confirm;
        public event Action? Cancel;

        public WindowChoose(string text)
        {
            this.Keys[ConsoleKey.Tab] = Tab;

            Button btnOk = new Button("ok");
            btnOk.Clicked += BtnOk_Clicked;
            this.Components.Add(btnOk);

            Button btnCancel = new Button("cancel");
            btnCancel.Clicked += BtnCancel_Clicked;
            this.Components.Add(btnCancel);

            this._message = text;
        }

        public override void Draw()
        {
            Console.WriteLine(this._message);
            
            base.Draw();
        }

        private void Tab()
        {
            this.SelectedComponent ^= 1;   //stack overflow ♡
        }

        private void BtnOk_Clicked()
        {
            this.Confirm?.Invoke();
        }

        private void BtnCancel_Clicked()
        {
            this.Cancel?.Invoke();
        }
    }
}
