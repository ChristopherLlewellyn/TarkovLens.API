using System;

namespace TarkovLens.Database.Documents.Miscellaneous
{
    public class Note
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
