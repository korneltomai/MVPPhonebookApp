namespace WinformsMVPPhonebookApp.UnitTests.Models.Helpers
{
    public class WriteBackStream : MemoryStream
    {
        private readonly Action<string> _onDispose;

        public WriteBackStream(Action<string> onDispose) : base()
        {
            _onDispose = onDispose;
        }

        protected override void Dispose(bool disposing)
        {
            Position = 0;
            using var reader = new StreamReader(this, leaveOpen: true);
            var content = reader.ReadToEnd();
            _onDispose(content);
            base.Dispose(disposing);
        }
    }
}
