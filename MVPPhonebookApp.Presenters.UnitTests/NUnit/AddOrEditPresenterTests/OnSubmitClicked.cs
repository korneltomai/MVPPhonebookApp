using MVPPhonebookApp.Core.Models;
using MVPPhonebookApp.Core.Services;
using MVPPhonebookApp.Presenters.Presenters;
using MVPPhonebookApp.Presenters.UnitTests.Fakes;

namespace MVPPhonebookApp.Presenters.UnitTests.NUnit.AddOrEditPresenterTests
{
    public class OnSubmitClicked
    {
        [Test]
        public void WhenEntryInViewIsNull_CallsAddEntryFromService()
        {
            // Arrange
            var stubView = new FakeAddOrEditView();
            stubView.Entry = null;
            stubView.EntryName = "John Doe";
            stubView.EntryPhoneNumber = "123456789";
            var mockService = new FakePhonebookEntryService();
            var presenter = new AddOrEditPresenter(stubView, mockService);

            var expectedEntry = new PhonebookEntry(stubView.EntryName, stubView.EntryPhoneNumber);

            // Act
            stubView.TriggerSubmitClicked();

            // Assert
            Assert.That(mockService.AddEntryCalled, Is.True);
            Assert.That(mockService.AddEntryParameter!.Name, Is.EqualTo(stubView.EntryName));
            Assert.That(mockService.AddEntryParameter.PhoneNumber, Is.EqualTo(stubView.EntryPhoneNumber));
        }

        [Test]
        public void WhenEntryInViewIsNullAndServiceThrowsValidationException_SendsErrorToViewAndLeavesItOpen()
        {
            // Arrange
            var mockView = new FakeAddOrEditView();
            mockView.Entry = null;
            mockView.EntryName = "John Doe";
            mockView.EntryPhoneNumber = "123456789";
            var stubService = new FakePhonebookEntryService
            {
                AddEntryWillThrow = new ValidationException("Fake exception")
            };
            var presenter = new AddOrEditPresenter(mockView, stubService);

            // Act
            mockView.TriggerSubmitClicked();

            // Assert
            StringAssert.Contains("Fake exception", mockView.ErrorMessage);
            Assert.That(mockView.IsClosed, Is.False, "View should not be closed when an error occurs.");
        }

        [Test]
        public void WhenEntryInViewIsNullAndServiceThrowsException_SendsErrorToViewAndClosesIt()
        {
            // Arrange
            var mockView = new FakeAddOrEditView();
            mockView.Entry = null;
            mockView.EntryName = "John Doe";
            mockView.EntryPhoneNumber = "123456789";
            var stubService = new FakePhonebookEntryService
            {
                AddEntryWillThrow = new Exception("Fake exception")
            };
            var presenter = new AddOrEditPresenter(mockView, stubService);

            // Act
            mockView.TriggerSubmitClicked();

            // Assert
            StringAssert.Contains("Fake exception", mockView.ErrorMessage);
            Assert.That(mockView.IsClosed, Is.True, "View should close on unexpected errors.");
        }

        [Test]
        public void WhenEntryInViewIsNotNull_CallsUpdateEntryOnService()
        {
            // Arrange
            var stubView = new FakeAddOrEditView();
            var existingEntry = new PhonebookEntry("John Doe", "123456789");
            stubView.Entry = existingEntry;
            stubView.EntryName = "Jane Smith";
            stubView.EntryPhoneNumber = "987654321";
            var mockService = new FakePhonebookEntryService();
            var presenter = new AddOrEditPresenter(stubView, mockService);

            // Act
            stubView.TriggerSubmitClicked();

            // Assert
            Assert.That(mockService.UpdateEntryCalled, Is.True);
            Assert.That(mockService.UpdateEntryParameters.oldEntry, Is.EqualTo(existingEntry));
            Assert.That(mockService.UpdateEntryParameters.newEntry!.Name, Is.EqualTo(stubView.EntryName));
            Assert.That(mockService.UpdateEntryParameters.newEntry!.PhoneNumber, Is.EqualTo(stubView.EntryPhoneNumber));
        }

        [Test]
        public void WhenEntryInViewIsNotNullAndServiceThrowValidationException_SendsErrorToViewAndLeavesItOpen()
        {
            // Arrange
            var mockView = new FakeAddOrEditView();
            var existingEntry = new PhonebookEntry("John Doe", "123456789");
            mockView.Entry = existingEntry;
            mockView.EntryName = "Jane Smith";
            mockView.EntryPhoneNumber = "987654321";
            var stubService = new FakePhonebookEntryService
            {
                UpdateEntryWillThrow = new ValidationException("Fake exception")
            };
            var presenter = new AddOrEditPresenter(mockView, stubService);

            // Act
            mockView.TriggerSubmitClicked();

            // Assert
            StringAssert.Contains("Fake exception", mockView.ErrorMessage);
            Assert.That(mockView.IsClosed, Is.False, "View should not be closed when an error occurs.");
        }

        [Test]
        public void WhenEntryInViewIsNotNullAndServiceThrowException_SendsErrorToViewAndClosesIt()
        {
            // Arrange
            var mockView = new FakeAddOrEditView();
            var existingEntry = new PhonebookEntry("John Doe", "123456789");
            mockView.Entry = existingEntry;
            mockView.EntryName = "Jane Smith";
            mockView.EntryPhoneNumber = "987654321";
            var stubService = new FakePhonebookEntryService
            {
                UpdateEntryWillThrow = new Exception("Fake exception")
            };
            var presenter = new AddOrEditPresenter(mockView, stubService);

            // Act
            mockView.TriggerSubmitClicked();

            // Assert
            StringAssert.Contains("Fake exception", mockView.ErrorMessage);
            Assert.That(mockView.IsClosed, Is.True, "View should close on unexpected errors.");
        }

        [Test]
        public void WhenSubmitClicked_ClosesView()
        {
            // Arrange
            var stubView = new FakeAddOrEditView();
            var mockService = new FakePhonebookEntryService();
            var presenter = new AddOrEditPresenter(stubView, mockService);

            // Act
            stubView.TriggerSubmitClicked();

            // Assert
            Assert.That(stubView.IsClosed, Is.True);
        }
    }
}
