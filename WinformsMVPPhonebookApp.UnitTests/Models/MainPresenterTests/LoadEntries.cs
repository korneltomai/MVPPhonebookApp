using WinformsMVPPhonebookApp.Models;
using WinformsMVPPhonebookApp.Presenters;
using WinformsMVPPhonebookApp.UnitTests.Models.Fakes;

namespace WinformsMVPPhonebookApp.UnitTests.Models.MainPresenterTests
{
    [TestFixture]
    public class LoadEntries
    {
        [Test]
        public void WhenCalled_CallsGetAllEntriesFromRepository()
        {
            // Arrange
            var stubMainView = new FakeMainView();
            var mockPhonebookRepository = new FakePhonebookRepository();
            var mainPresenter = new MainPresenter(stubMainView, mockPhonebookRepository);

            // Act  
            mainPresenter.LoadEntries();

            // Assert
            Assert.That(mockPhonebookRepository.GetAllEntriesCalled, Is.True);
        }

        [Test]
        public void WhenGotValidEntriesFromRepository_SetsBindingList()
        {
            // Arrange
            var stubMainView = new FakeMainView();
            var stubPhonebookRepository = new FakePhonebookRepository
            {
                Entries = new List<PhonebookEntry>
                {
                    new PhonebookEntry("John Doe", "123456789"),
                    new PhonebookEntry ("Jane Smith", "987654321")
                }
            };
            var mainPresenter = new MainPresenter(stubMainView, stubPhonebookRepository);

            // Act
            mainPresenter.LoadEntries();

            // Assert
            Assert.That(stubPhonebookRepository.Entries.Count, Is.EqualTo(mainPresenter.BindingList.Count));
            Assert.That(mainPresenter.BindingList, Has.All.Matches<PhonebookEntry>(entry =>
                stubPhonebookRepository.Entries.Any(e => e.Name == entry.Name && e.PhoneNumber == entry.PhoneNumber)
            ));
        }

        [Test]
        public void WhenGotValidEntriesFromRepository_BindsEntriesInView()
        {
            // Arrange
            var stubMainView = new FakeMainView();
            var stubPhonebookRepository = new FakePhonebookRepository
            {
                Entries = new List<PhonebookEntry>
                {
                    new PhonebookEntry("John Doe", "123456789"),
                    new PhonebookEntry ("Jane Smith", "987654321")
                }
            };

            var mainPresenter = new MainPresenter(stubMainView, stubPhonebookRepository);

            // Act
            mainPresenter.LoadEntries();

            // Assert
            Assert.That(stubMainView.Entries, Is.EqualTo(mainPresenter.BindingList));
        }
    }
}