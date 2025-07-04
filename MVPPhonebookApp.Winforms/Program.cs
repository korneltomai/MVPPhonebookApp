using MVPPhonebookApp.Core.FileSystem;
using MVPPhonebookApp.Core.Repository;
using MVPPhonebookApp.Core.Services;
using MVPPhonebookApp.Forms;
using MVPPhonebookApp.Presenters.Presenters;
using MVPPhonebookApp.Winforms.Services;

namespace MVPPhonebookApp
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

            bool successfulLaunch = true;

            var mainView = new MainForm();
            var fileSystem = new RealFileSystem();
            var repository = new CsvPhonebookRepository(fileSystem, "entries.csv");
            var phonebookEntryService = new PhonebookEntryService(repository);
            var addOrEditDialogService = new AddOrEditDialogService(phonebookEntryService);
            var mainPresenter = new MainPresenter(mainView, phonebookEntryService, addOrEditDialogService);

            try
            {
                mainPresenter.LoadEntries();
            }
            catch (InvalidOperationException ex)
            {
                DialogResult result = MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (result == DialogResult.OK)
                    Application.Exit();

                successfulLaunch = false;
            }

            if (successfulLaunch)
                Application.Run(mainView);
        }
    }
}