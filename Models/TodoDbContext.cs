using Microsoft.EntityFrameworkCore;


namespace TodoApps.Models
{
    public partial class TodoDbContext : DbContext
    {
        public TodoDbContext()
        {

        }
        public TodoDbContext(DbContextOptions<TodoDbContext> options)
            : base(options)
        {

        }

        public virtual DbSet<Todo> Todos { get; set; }
        public virtual DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<Todo>()
                .Property(t => t.CreatedAt)
                .HasColumnType("datetime");

            modelBuilder.Entity<User>()
                .Property(t => t.UserName)
                .IsRequired();

            modelBuilder.Entity<User>()
               .Property(t => t.Password)
               .IsRequired();
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);


    }
}



