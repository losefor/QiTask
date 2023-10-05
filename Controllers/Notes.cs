using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QiTask.Data.Data;
using QiTask.Dtos;
using QiTask.Models;

namespace QiTask.Controllers;

public class Notes : AuthorizedBaseController
{
    private IMapper _mapper;
    private DataContext _context;
    private IConfiguration _config;

    public Notes(IMapper mapper, DataContext context, IConfiguration config)
    {
        _mapper = mapper;
        _context = context;
        _config = config;
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<Note>>> GetAllMyNotes()
    {
        var notes = await _context.Notes.Where(n => n.UserId == UserId).ToListAsync();
        var count = await _context.Notes.Where(n => n.UserId == UserId).CountAsync();

        return Ok(new
        {
            count,
            notes
        });
    }


    [HttpGet("{id}")]
    public async Task<ActionResult> GetSingleNote(string id)
    {
        var note = await _context.Notes.FirstOrDefaultAsync(x => x.Id == int.Parse(id) && x.UserId == UserId);

        if (note is null)
            return NotFound();

        return Ok(new
        {
            note
        });
    }


    [HttpPost]
    public async Task<ActionResult> CreateNote(NoteDto noteDto)
    {
        var Note = new Note
        {
            Title = noteDto.Title, UserId = UserId
        };

        await _context.Notes.AddAsync(Note);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetSingleNote", new { id = Note.Id }, Note);
    }


    [HttpPatch("{id}")]
    public async Task<ActionResult<Note>> PatchNote(int id, [FromBody] NoteDto noteDto)
    {
        var note = await _context.Notes.FindAsync(id);

        if (note == null)
        {
            return NotFound();
        }

        note.Title = noteDto.Title;
        note.LastUpdated = DateTime.UtcNow.AddHours(3);

        var isSaved = await _context.SaveChangesAsync() > 0;

        if (!isSaved)
            return UnprocessableEntity();

        return note;
    }
}