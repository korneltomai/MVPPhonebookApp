﻿using NSubstitute;
using MVPPhonebookApp.Core.Models;
using MVPPhonebookApp.Presenters.Presenters;
using MVPPhonebookApp.Presenters.Views;
using MVPPhonebookApp.Core.Services;
using MVPPhonebookApp.Presenters.UnitTests.Fakes;
using MVPPhonebookApp.Presenters.Services;

namespace MVPPhonebookApp.Presenters.UnitTests.NUnitWithNSubstitude.MainPresenterTests;

[TestFixture]
public class OnDeleteEntryClicked
{
    [Test]
    public void WhenSelectedEntryIsNotNull_CallsDeleteEntryFromService()
    {
        // Arrange
        var stubMainView = Substitute.For<IMainView>();
        stubMainView.SelectedEntry.Returns(new PhonebookEntry("Fake Entry", "123456789"));
        var mockPhonebookEntryService = Substitute.For<PhonebookEntryService>(new EmptyPhonebookRepository());
        var stubAddOrEditDialogService = Substitute.For<IAddOrEditDialogService>();
        var mainPresenter = new MainPresenter(stubMainView, mockPhonebookEntryService, stubAddOrEditDialogService);

        // Act  
        stubMainView.DeleteEntryClicked += Raise.Event();

        // Assert
        mockPhonebookEntryService.Received().DeleteEntry(stubMainView.SelectedEntry!);
    }

    [Test]
    public void WhenServiceThrowsException_SendsErrorToView()
    {
        // Arrange
        var mockMainView = Substitute.For<IMainView>();
        mockMainView.SelectedEntry.Returns(_ => new PhonebookEntry("Fake Entry", "123456789"));
        var stubPhonebookEntryService = Substitute.For<PhonebookEntryService>(new EmptyPhonebookRepository());
        stubPhonebookEntryService.When(s => s.DeleteEntry(Arg.Any<PhonebookEntry>()))
            .Do(info => { throw new Exception("Fake exception"); });
        var stubAddOrEditDialogService = Substitute.For<IAddOrEditDialogService>();
        var mainPresenter = new MainPresenter(mockMainView, stubPhonebookEntryService, stubAddOrEditDialogService);

        // Act  
        mockMainView.DeleteEntryClicked += Raise.Event();

        // Assert
        mockMainView.Received().ShowError(Arg.Is<string>(s => s.Contains("Fake exception")));
    }

    [Test]
    public void WhenSelectedEntryIsNull_SendsErrorToView()
    {
        // Arrange
        var mockMainView = Substitute.For<IMainView>();
        mockMainView.SelectedEntry.Returns(_ => null);
        var stubPhonebookEntryService = Substitute.For<PhonebookEntryService>(new EmptyPhonebookRepository());
        var stubAddOrEditDialogService = Substitute.For<IAddOrEditDialogService>();
        var mainPresenter = new MainPresenter(mockMainView, stubPhonebookEntryService, stubAddOrEditDialogService);

        // Act  
        mockMainView.DeleteEntryClicked += Raise.Event();

        // Assert
        mockMainView.Received().ShowError(Arg.Is<string>(s => s.Contains("No entry selected for deletion")));
    }

    [Test]
    public void WhenSelectedEntryIsNotNull_RemovesEntryView()
    {
        // Arrange
        var mockMainView = Substitute.For<IMainView>();
        mockMainView.Entries =
            [
                new PhonebookEntry("John Doe", "123456789"),
                new PhonebookEntry ("Jane Smith", "987654321")
            ];
        mockMainView.SelectedEntry.Returns(_ => mockMainView.Entries.First());
        var stubPhonebookEntryService = Substitute.For<PhonebookEntryService>(new EmptyPhonebookRepository());
        var stubAddOrEditDialogService = Substitute.For<IAddOrEditDialogService>();
        var mainPresenter = new MainPresenter(mockMainView, stubPhonebookEntryService, stubAddOrEditDialogService);

        var expectedEntries = new List<PhonebookEntry>
        {
            new PhonebookEntry("Jane Smith", "987654321")
        };

        // Act
        mockMainView.DeleteEntryClicked += Raise.Event();

        // Assert
        Assert.That(mockMainView.Entries.Count, Is.EqualTo(expectedEntries.Count));
        Assert.That(mockMainView.Entries, Has.All.Matches<PhonebookEntry>(entry =>
            expectedEntries.Any(e => e.Name == entry.Name && e.PhoneNumber == entry.PhoneNumber)
        ));
    }
}
