using NotesService.Models;
using Microsoft.EntityFrameworkCore;

namespace NotesService
{
    public class NotesRepository:DbContext
    {
        public NotesRepository(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Note> Notes { get; set; }
    }
}
