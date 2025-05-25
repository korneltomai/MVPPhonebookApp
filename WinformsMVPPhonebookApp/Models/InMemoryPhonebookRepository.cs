
using System.DirectoryServices;

namespace WinformsMVPPhonebookApp.Models
{
    public class InMemoryPhonebookRepository : IPhonebookRepository
    {
        public List<PhonebookEntry> Entries { get; set; }

        public InMemoryPhonebookRepository()
        {
            Entries = new List<PhonebookEntry>
            {
                new PhonebookEntry { Name = "Mom", PhoneNumber = "+361234567" },
                new PhonebookEntry { Name = "Dad", PhoneNumber = "+361234568" },
                new PhonebookEntry { Name = "Sister", PhoneNumber = "+361234569" },
                new PhonebookEntry { Name = "John from school", PhoneNumber = "+367654321" },
                new PhonebookEntry { Name = "Lara from school", PhoneNumber = "+368654321" },
                new PhonebookEntry { Name = "Brook from school", PhoneNumber = "+369654321" },
                new PhonebookEntry { Name = "David Repairman", PhoneNumber = "+365671234" },
                new PhonebookEntry { Name = "Luke Tenant", PhoneNumber = "+365671235" },
                new PhonebookEntry { Name = "Dr. Xavier", PhoneNumber = "+365671236" },
                new PhonebookEntry { Name = "Gerold", PhoneNumber = "+365671237" },
                new PhonebookEntry { Name = "Alice", PhoneNumber = "+365671238" },
                new PhonebookEntry { Name = "Bob", PhoneNumber = "+365671239" },
                new PhonebookEntry { Name = "Charlie", PhoneNumber = "+365671240" },
                new PhonebookEntry { Name = "Diana", PhoneNumber = "+365671241" },
                new PhonebookEntry { Name = "Ethan", PhoneNumber = "+365671242" },
                new PhonebookEntry { Name = "Fiona", PhoneNumber = "+365671243" },
                new PhonebookEntry { Name = "George", PhoneNumber = "+365671244" },
                new PhonebookEntry { Name = "Hannah", PhoneNumber = "+365671245" },
                new PhonebookEntry { Name = "Ivy", PhoneNumber = "+365671246" }
            };
        }

        public IEnumerable<PhonebookEntry> GetAllEntries()
        {
            return Entries;
        }
    }
}
