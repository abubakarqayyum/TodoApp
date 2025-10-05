using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todo.Utilities;


namespace Todo.Entities.EntityMappings
{
    public class TodoMapping : IEntityTypeConfiguration<Todo.Entities.Entity.Todo>
    {
        public void Configure(EntityTypeBuilder<Todo.Entities.Entity.Todo> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title).HasMaxLength(300).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(500);
            builder.Property(x => x.Status).IsRequired();
            //builder.HasOne(x=>x.User).WithMany(x=>x.Todos).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
