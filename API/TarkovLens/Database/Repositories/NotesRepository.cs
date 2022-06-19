using Raven.Client.Documents.Session;
using System.Collections.Generic;
using System.Linq;
using TarkovLens.Database.Documents.Miscellaneous;
using TarkovLens.Helpers.ExtensionMethods;

namespace TarkovLens.Database.Repositories
{
    public interface INotesRepository : IRavenRepository
    {
        public Note GetNoteById(string id);
        public List<Note> GetNotes();
        public void StoreNote(Note note, bool saveChanges = false);
    }
    public class NotesRepository : INotesRepository
    {
        private readonly IDocumentSession session;

        public NotesRepository(IDocumentSession documentSession)
        {
            session = documentSession;
        }

        public Note GetNoteById(string id)
        {
            var note = session.Load<Note>(id);
            if (note.IsNotNull())
            {
                session.Advanced.IgnoreChangesFor(note);
            }

            return note;
        }

        public List<Note> GetNotes() => session.Query<Note>().ToList();

        public void StoreNote(Note note, bool saveChanges = false)
        {
            session.Store(note);
            if (saveChanges)
            {
                session.SaveChanges();
            }
        }

        public void IncreaseMaxNumberOfRequestsPerSession(int increase) =>
            session.Advanced.MaxNumberOfRequestsPerSession += increase;

        public void SaveChanges() => session.SaveChanges();
    }
}
