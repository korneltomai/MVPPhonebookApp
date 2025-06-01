namespace WinformsMVPPhonebookApp.Models
{
    public class PhonebookEntry
    {
        public string Name { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;

        public PhonebookEntry(string name, string phoneNumber)
        {
            Name = name;
            PhoneNumber = phoneNumber;
        }
    }
}
