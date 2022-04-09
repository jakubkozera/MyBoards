using Microsoft.EntityFrameworkCore;
using MyBoards.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBoards.Entities
{
    public class MyBoardsContext : DbContext
    {
        public MyBoardsContext(DbContextOptions<MyBoardsContext> options) : base(options)
        {

        }
        public DbSet<WorkItem> WorkItems { get; set; }
        public DbSet<Issue> Issues { get; set; }
        public DbSet<Epic> Epics { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<WorkItemState> WorkItemStates { get; set; }
        public DbSet<WorkItemTag> WorkItemTag { get; set; }
        public DbSet<TopAuthor> ViewTopAuthors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WorkItemState>()
                 .Property(s => s.Value)
                 .IsRequired()
                 .HasMaxLength(60);

           modelBuilder.Entity<Epic>()
                .Property(wi => wi.EndDate)
                .HasPrecision(3);

            modelBuilder.Entity<Task>()
                .Property(wi => wi.Activity)
                .HasMaxLength(200);

            modelBuilder.Entity<Task>()
                .Property(wi => wi.RemaningWork)
                .HasPrecision(14, 2);

            modelBuilder.Entity<Issue>()
                .Property(wi => wi.Efford)
                .HasColumnType("decimal(5,2)");

            modelBuilder.Entity<WorkItem>(eb =>
            {
                eb.HasOne(w => w.State)
                .WithMany()
                .HasForeignKey(w => w.StateId);

                eb.Property(x => x.Area).HasColumnType("varchar(200)");
                eb.Property(wi => wi.IterationPath).HasColumnName("Iteration_Path");
    
                eb.Property(wi => wi.Priority).HasDefaultValue(1);
                eb.HasMany(w => w.Comments)
                .WithOne(c => c.WorkItem)
                .HasForeignKey(c => c.WorkItemId);

                eb.HasOne(w => w.Author)
                .WithMany(u => u.WorkItems)
                .HasForeignKey(w => w.AuthorId);

                eb.HasMany(w => w.Tags)
                .WithMany(t => t.WorkItems)
                .UsingEntity<WorkItemTag>(
                    w => w.HasOne(wit => wit.Tag)
                    .WithMany()
                    .HasForeignKey(wit => wit.TagId),

                    w => w.HasOne(wit => wit.WorkItem)
                    .WithMany()
                    .HasForeignKey(wit => wit.WorkItemId),

                    wit =>
                    {
                        wit.HasKey(x => new { x.TagId, x.WorkItemId });
                        wit.Property(x => x.PublicationDate).HasDefaultValueSql("getutcdate()");
                    });
            });

            modelBuilder.Entity<Comment>(eb =>
            {
                eb.Property(x => x.CreatedDate).HasDefaultValueSql("getutcdate()");
                eb.Property(x => x.UpdatedDate).ValueGeneratedOnUpdate();
                eb.HasOne(c => c.Author)
                    .WithMany(a => a.Comments)
                    .HasForeignKey(c => c.AuthorId)
                    .OnDelete(DeleteBehavior.ClientCascade);
            });

            modelBuilder.Entity<User>()
                .HasOne(u => u.Address)
                .WithOne(a => a.User)
                .HasForeignKey<Address>(a => a.UserId);

            modelBuilder.Entity<WorkItemState>()
                .HasData(new WorkItemState() { Id = 1, Value = "To Do" },
                new WorkItemState() { Id = 2, Value = "Doing" },
                new WorkItemState() { Id = 3, Value = "Done" });

            modelBuilder.Entity<Tag>()
                .HasData(new Tag() { Id = 1, Value = "Web" },
                new Tag() { Id = 2, Value = "UI" },
                new Tag() { Id = 3, Value = "Desktop" },
                new Tag() { Id = 4, Value = "API" },
                new Tag() { Id = 5, Value = "Service" });


            modelBuilder.Entity<TopAuthor>(eb =>
            {
                eb.ToView("View_TopAuthors");
                eb.HasNoKey();
            });


            modelBuilder.Entity<Address>()
                .OwnsOne(a => a.Coordinate, cmb =>
                {
                    cmb.Property(c => c.Latitude).HasPrecision(18, 7);
                    cmb.Property(c => c.Longitude).HasPrecision(18, 7);
                });
        }
    }
}
