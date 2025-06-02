namespace WinformsMVPPhonebookApp
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            var mainView = new Views.MainForm();
            var fileSystem = new Models.RealFileSystem();
            var repository = new Models.CsvPhonebookRepository(fileSystem, "entries.csv");
            
            try 
            {
                var mainPresenter = new Presenters.MainPresenter(mainView, repository);
            }
            catch (InvalidOperationException e)
            {
                DialogResult result = MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (result == DialogResult.OK)
                    Application.Exit();
                else
                    Application.Run(mainView);
            }
            
        }
    }
}