using Microsoft.EntityFrameworkCore;
using Notesphere.Entities.DashboardModels;
using Notesphere.Entities.NotesModels;
using Notesphere.Entities.PlannerModels;
using Notesphere.Entities.SharingModels;
using Notesphere.Entities.ProductivityModels;


namespace Notesphere.Services.NotesphereDataAccessLayer
{
    public class NotesphereDbContext : DbContext
    {
        public NotesphereDbContext(DbContextOptions<NotesphereDbContext> options) : base(options)
        {
        }

        //Dashboard.db sets
        public DbSet<Reminder> Reminders { get; set; }
        public DbSet<QuickActions> QuickActions { get; set; }
        public DbSet<Workspace>Workspaces { get; set; }

        //Notes.db sets 
        public DbSet<Note> Notes { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<NoteTag> NoteTags { get; set; }
        public DbSet<NoteTemplate> NoteTemplates { get; set; }
        public DbSet<NoteVersion> NoteVersions { get; set; }
        public DbSet<NoteExport> NoteExports { get; set; }
        public DbSet<NotePage> NotePages { get; set; }
        public DbSet<StudentUser> StudentUser { get; set; } = null!;


        //Sharing.db sets
        public DbSet<GroupSpace> GroupSpaces { get; set; }
        public DbSet<GroupMember> GroupMembers { get; set; }
        public DbSet<SharedNote> SharedNotes { get; set; }
        public DbSet<NoteComment> NoteComments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // composite key for join table
            modelBuilder.Entity<NoteTag>()
                .HasKey(nt => new { nt.NoteId, nt.TagId });
        
            modelBuilder.Entity<NoteTemplate>().HasData(
                new NoteTemplate
                {
                    Id = 1,
                    Name = "Classic lined",
                    Description = "Simple ruled notebook page",
                    CssKey = "lined",
                    DefaultContent = "",
                    IsSystemTemplate = true
                },
                new NoteTemplate
                {
                    Id = 2,
                    Name = "Dot grid",
                    Description = "For bullet journaling and sketches",
                    CssKey = "dotgrid",
                    DefaultContent = "",
                    IsSystemTemplate = true
                },
                new NoteTemplate
                {
                    Id = 3,
                    Name = "Cornell notes",
                    Description = "Cue, notes, and summary layout",
                    CssKey = "cornell",
                    DefaultContent = "Topic:\nDate:\n\n[Main notes]\n\nSummary:",
                    IsSystemTemplate = true
                },
                new NoteTemplate
                {
                    Id = 4,
                    Name = "Minimal blank",
                    Description = "Plain, no guides",
                    CssKey = "blank",
                    DefaultContent = "",
                    IsSystemTemplate = true
                }
            );

        }

        //Planner.db sets
        public DbSet<Event> Events { get; set; }
        public DbSet<RecurringEvent> RecurringEvents { get; set; }
        public DbSet<Conflict> Conflicts { get; set; }

        //Sharing.db sets

        //Productivity.db sets
        public DbSet<ProductivityTask> ProductivityTasks { get; set; } = null!;
        public DbSet<TaskChecklistItem> TaskChecklistItems { get; set; } = null!;

    }
}

