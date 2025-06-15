using NSubstitute;
using MVPPhonebookApp.Core.Models;
using MVPPhonebookApp.Core.Services;
using MVPPhonebookApp.Presenters.Presenters;
using MVPPhonebookApp.Presenters.Views;
using MVPPhonebookApp.Core.Repository;

namespace MVPPhonebookApp.Presenters.UnitTests.NUnitWithNSubstitude.AddOrEditPresenterTests
{
    public class OnSubmitClicked
    {
        [Test]
        public void WhenEntryInViewIsNull_CallsAddEntryFromService()
        {
            // Arrange
            var stubView = Substitute.For<IAddOrEditView>();
            stubView.Entry = null;
            stubView.EntryName = "John Doe";
            stubView.EntryPhoneNumber = "123456789";
            var fakeRepository = Substitute.For<IPhonebookRepository>();
            var mockService = Substitute.For<PhonebookEntryService>(fakeRepository);
            var presenter = new AddOrEditPresenter(stubView, mockService);

            var expectedEntry = new PhonebookEntry(stubView.EntryName, stubView.EntryPhoneNumber);

            // Act
            stubView.SubmitClicked += Raise.Event();

            // Assert
            mockService.Received().AddEntry(Arg.Is<PhonebookEntry>(e => e.Name == expectedEntry.Name && e.PhoneNumber == expectedEntry.PhoneNumber));
        }

        [Test]
        public void WhenEntryInViewIsNullAndServiceThrowsValidationException_SendsErrorToViewAndLeavesItOpen()
        {
            // Arrange
            var mockView = Substitute.For<IAddOrEditView>();
            mockView.Entry = null;
            mockView.EntryName = "John Doe";
            mockView.EntryPhoneNumber = "123456789";
            var fakeRepository = Substitute.For<IPhonebookRepository>();
            var stubService = Substitute.For<PhonebookEntryService>(fakeRepository);
            stubService.When(x => x.AddEntry(Arg.Any<PhonebookEntry>()))
                .Do(x => { throw new ValidationException("Fake exception"); });
            var presenter = new AddOrEditPresenter(mockView, stubService);

            // Act
            mockView.SubmitClicked += Raise.Event();

            // Assert
            mockView.Received().ShowError(Arg.Is<string>("Fake exception"));
            mockView.DidNotReceive().Close();
        }

        [Test]
        public void WhenEntryInViewIsNullAndServiceThrowsException_SendsErrorToViewAndClosesIt()
        {
            // Arrange
            var mockView = Substitute.For<IAddOrEditView>();
            mockView.Entry = null;
            mockView.EntryName = "John Doe";
            mockView.EntryPhoneNumber = "123456789";
            var fakeRepository = Substitute.For<IPhonebookRepository>();
            var stubService = Substitute.For<PhonebookEntryService>(fakeRepository);
            stubService.When(x => x.AddEntry(Arg.Any<PhonebookEntry>()))
                .Do(x => { throw new Exception("Fake exception"); });
            var presenter = new AddOrEditPresenter(mockView, stubService);

            // Act
            mockView.SubmitClicked += Raise.Event();

            // Assert
            mockView.Received().ShowError(Arg.Is<string>(s => s.Contains("Fake exception")));
            mockView.Received().Close();
        }

        [Test]
        public void WhenEntryInViewIsNotNull_CallsUpdateEntryFromService()
        {
            // Arrange
            var stubView = Substitute.For<IAddOrEditView>();
            var existingEntry = new PhonebookEntry("John Doe", "123456789");
            stubView.Entry = existingEntry;
            stubView.EntryName = "Jane Smith";
            stubView.EntryPhoneNumber = "987654321";
            var fakeRepository = Substitute.For<IPhonebookRepository>();
            var mockService = Substitute.For<PhonebookEntryService>(fakeRepository);
            var presenter = new AddOrEditPresenter(stubView, mockService);

            // Act
            stubView.SubmitClicked += Raise.Event();

            // Assert
            mockService.Received().UpdateEntry(Arg.Is<PhonebookEntry>(e => e.Equals(existingEntry)),
                Arg.Is<PhonebookEntry>(e => e.Name == stubView.EntryName && e.PhoneNumber == stubView.EntryPhoneNumber));
        }

        [Test]
        public void WhenEntryInViewIsNotNullAndServiceThrowValidationException_SendsErrorToViewAndLeavesItOpen()
        {
            // Arrange
            var mockView = Substitute.For<IAddOrEditView>();
            var existingEntry = new PhonebookEntry("John Doe", "123456789");
            mockView.Entry = existingEntry;
            mockView.EntryName = "Jane Smith";
            mockView.EntryPhoneNumber = "987654321";
            var fakeRepository = Substitute.For<IPhonebookRepository>();
            var stubService = Substitute.For<PhonebookEntryService>(fakeRepository);
            stubService.When(x => x.UpdateEntry(Arg.Any<PhonebookEntry>(), Arg.Any<PhonebookEntry>()))
                .Do(x => { throw new ValidationException("Fake exception"); });
            var presenter = new AddOrEditPresenter(mockView, stubService);

            // Act
            mockView.SubmitClicked += Raise.Event();

            // Assert
            mockView.Received().ShowError(Arg.Is<string>("Fake exception"));
            mockView.DidNotReceive().Close();
        }

        [Test]
        public void WhenEntryInViewIsNotNullAndServiceThrowException_SendsErrorToViewAndClosesIt()
        {
            // Arrange
            var mockView = Substitute.For<IAddOrEditView>();
            var existingEntry = new PhonebookEntry("John Doe", "123456789");
            mockView.Entry = existingEntry;
            mockView.EntryName = "Jane Smith";
            mockView.EntryPhoneNumber = "987654321";
            var fakeRepository = Substitute.For<IPhonebookRepository>();
            var stubService = Substitute.For<PhonebookEntryService>(fakeRepository);
            stubService.When(x => x.UpdateEntry(Arg.Any<PhonebookEntry>(), Arg.Any<PhonebookEntry>()))
                .Do(x => { throw new Exception("Fake exception"); });

            var presenter = new AddOrEditPresenter(mockView, stubService);

            // Act
            mockView.SubmitClicked += Raise.Event();

            // Assert
            mockView.Received().ShowError(Arg.Is<string>(s => s.Contains("Fake exception")));
            mockView.Received().Close();

        }

        [Test]
        public void WhenSubmitClicked_ClosesView()
        {
            // Arrange
            var stubView = Substitute.For<IAddOrEditView>();
            var fakeRepository = Substitute.For<IPhonebookRepository>();
            var mockService = Substitute.For<PhonebookEntryService>(fakeRepository);
            var presenter = new AddOrEditPresenter(stubView, mockService);

            // Act
            stubView.SubmitClicked += Raise.Event();

            // Assert
            stubView.Received().Close();
        }
    }
}
