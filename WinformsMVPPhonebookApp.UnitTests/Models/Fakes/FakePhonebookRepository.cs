using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinformsMVPPhonebookApp.Models;

namespace WinformsMVPPhonebookApp.UnitTests.Models.Fakes
{

    public class FakePhonebookRepository : IPhonebookRepository
    {
        public List<PhonebookEntry> Entries { get; set; }= [];
        public Exception? AddEntryWillThrow { get; set; } = null;
        public bool AddEntryCalled { get; private set; } = false;
        public Exception? DeleteEntryWillThrow { get; set; } = null;
        public bool DeleteEntryCalled { get; private set; } = false;
        public bool GetAllEntriesCalled { get; private set; } = false;

        public void AddEntry(PhonebookEntry entry)
        {
            AddEntryCalled = true;

            if (AddEntryWillThrow != null)
                throw AddEntryWillThrow;
        }

        public void DeleteEntry(PhonebookEntry entry)
        {
            DeleteEntryCalled = true;

            if (DeleteEntryWillThrow != null)
                throw DeleteEntryWillThrow;
        }

        public IEnumerable<PhonebookEntry> GetAllEntries()
        {
            GetAllEntriesCalled = true;

            return Entries.AsEnumerable();
        }
    }
}
