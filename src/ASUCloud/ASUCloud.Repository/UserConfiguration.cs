using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASUCloud.Repository
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("user");

            builder.Property(x => x.ID)
                .HasColumnName("id")
                .HasColumnType("char(38)")
                .HasMaxLength(38)
                .IsRequired();

            builder.Property(x => x.Name)
                .HasColumnName("name")
                .HasColumnType("varchar(128)")
                .HasMaxLength(128)
                .IsRequired();

            builder.Property(x => x.Email)
                .HasColumnName("email")
                .HasColumnType("varchar(320)")
                .HasMaxLength(320)
                .IsRequired();


            builder.Property(x => x.Password)
                .HasColumnName("password")
                .HasColumnType("varchar(64)")
                .HasMaxLength(64)
                .IsRequired();


            builder.Property(x => x.HasVerified)
                .HasColumnName("has_verified")
                .HasColumnType("bool")
                .IsRequired();

            builder.Property(x => x.CreatedTime)
                .HasColumnName("created_time")
                .HasColumnType("integer")
                .HasDefaultValueSql("datetime(current_timestamp)");

            builder.Property(x => x.UpdatedTime)
                .HasColumnName("updated_time")
                .HasColumnType("integer")
                .HasDefaultValueSql("datetime(current_timestamp)")
                .IsRequired();

            builder.HasIndex(u => u.ID, "primary_key");

            builder.HasIndex(u=>new {u.Name, u.Email}, "name_email_avoid_dup");

        }
    }
}
