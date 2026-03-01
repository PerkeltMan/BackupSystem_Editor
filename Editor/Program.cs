namespace Editor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Application app = new Application();
            app.Run();
        }
    }
}

/* TO DO
 * REFACTOR PREVIEW DRAW (OR ALL OF THE DRAW METHODS)
 * CREATE A CHECK TO SEE IF CHANGED VALUES ARE IN CORRECT FORMAT
 * GET RID OF CONSOLE.CLEAR() AND REPLACE IT WITH A BETTER SOLUTION
 */
