using Editor.Components.AbstractClasses;
using Editor.Components.Components;

namespace Editor.Components.Windows
{
    public class WindowChoose : Window
    {
        private List<Button> buttons = new List<Button>();
        private int currentButton;
        private string message;

        public event Action? Confirm;
        public event Action? Cancel;

        public WindowChoose(string text)
        {
            Button btnOk = new Button("ok");
            btnOk.Clicked += BtnOk_Clicked;
            this.buttons.Add(btnOk);

            Button btnCancel = new Button("cancel");
            btnCancel.Clicked += BtnCancel_Clicked;
            this.buttons.Add(btnCancel);

            this.message = text;
        }

        public override void Draw()
        {
            Console.WriteLine(this.message);
            for (int i = 0; i < this.buttons.Count; i++)
            {
                if (i == this.currentButton)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }

                this.buttons[i].Draw();

                Console.ResetColor();
            }
        }

        public override void HandleKey(ConsoleKeyInfo keyInfo)
        {
            if (keyInfo.Key == ConsoleKey.Tab)
            {
                this.currentButton ^= 1;   //stack overflow ♡
            }
            else
            {
                this.buttons[currentButton].HandleKey(keyInfo);
            }
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
