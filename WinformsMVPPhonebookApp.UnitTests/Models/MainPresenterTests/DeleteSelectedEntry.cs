using WinformsMVPPhonebookApp.Models;
using WinformsMVPPhonebookApp.Presenters;
using WinformsMVPPhonebookApp.UnitTests.Models.Fakes;

namespace WinformsMVPPhonebookApp.UnitTests.Models.MainPresenterTests
{
    [TestFixture]
    public class DeleteSelectedEntry
    {
        [Test]
        public void WhenSelectedEntryIsNotNull_CallsDeleteEntryFromRepository()
        {
            // Arrange
            var stubMainView = new FakeMainView();
            var mockPhonebookRepository = new FakePhonebookRepository();
            var mainPresenter = new MainPresenter(stubMainView, mockPhonebookRepository);

            stubMainView.SelectedEntry = new PhonebookEntry("Fake Entry", "123456789");

            // Act  
            stubMainView.TriggerDeleteEntryClicked();

            // Assert
            Assert.That(mockPhonebookRepository.DeleteEntryCalled, Is.True);
        }

        [Test]
        public void WhenRepositoryThrowsException_SendsErrorToView()
        {
            // Arrange
            var stubMainView = new FakeMainView();
            var mockPhonebookRepository = new FakePhonebookRepository();
            mockPhonebookRepository.DeleteEntryWillThrow = new Exception("Fake exception");
            var mainPresenter = new MainPresenter(stubMainView, mockPhonebookRepository);

            stubMainView.SelectedEntry = new PhonebookEntry("Fake Entry", "123456789");

            // Act  
            stubMainView.TriggerDeleteEntryClicked();

            // Assert
            Assert.That(stubMainView.ErrorMessage, Contains.Substring("Fake exception"));
        }

        [Test]
        public void WhenSelectedEntryIsNull_SendsErrorToView()
        {
            // Arrange
            var stubMainView = new FakeMainView();
            var mockPhonebookRepository = new FakePhonebookRepository();
            var mainPresenter = new MainPresenter(stubMainView, mockPhonebookRepository);

            stubMainView.SelectedEntry = null;

            // Act  
            stubMainView.TriggerDeleteEntryClicked();

            // Assert
            Assert.That(stubMainView.ErrorMessage, Contains.Substring("No entry selected for deletion"));
        }

        [Test]
        public void WhenSelectedEntryIsNotNull_RemovesEntryFromBindingList()
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
            
            mainPresenter.LoadEntries();
            stubMainView.SelectedEntry = mainPresenter.BindingList.First();

            var exceptedEntries = new List<PhonebookEntry> 
            {  
                new PhonebookEntry("Jane Smith", "987654321") 
            };

            // Act
            stubMainView.TriggerDeleteEntryClicked();

            // Assert
            Assert.That(exceptedEntries.Count, Is.EqualTo(mainPresenter.BindingList.Count));
            Assert.That(mainPresenter.BindingList, Has.All.Matches<PhonebookEntry>(entry =>
                exceptedEntries.Any(e => e.Name == entry.Name && e.PhoneNumber == entry.PhoneNumber)
            ));
        }
    }
}
