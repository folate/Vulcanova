﻿using System.Collections.Generic;
using System.Threading.Tasks;
using LiteDB.Async;

namespace Vulcanova.Features.Notes;

public class NotesRepository : INotesRepository
{
    private readonly LiteDatabaseAsync _db;

    public NotesRepository(LiteDatabaseAsync db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Note>> GetNotesByPupilAsync(int accountId, int pupilId)
    {
        var notesCollection = _db.GetCollection<Note>();
        var pupilNotes = await notesCollection.Query()
            .Where(n => n.AccountId == accountId && n.PupilId == pupilId)
            .ToListAsync();

        return pupilNotes;
    }

    public async Task UpdateNoteEntriesAsync(IEnumerable<Note> entries, int accountId, int pupilId)
    {
        await _db.GetCollection<Note>()
            .DeleteManyAsync(h => h.AccountId == accountId && h.PupilId == pupilId);

        await _db.GetCollection<Note>().UpsertAsync(entries);
    }
}