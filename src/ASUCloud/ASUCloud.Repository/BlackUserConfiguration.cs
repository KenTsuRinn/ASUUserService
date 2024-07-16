using ASUCloud.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASUCloud.Repository
{
    /// <summary>
    /// BlackUserConfiguration
    /// この設定はsqlite用であり、
    /// 他のデータベースへの変更はデータ型を変更する必要がある。
    /// </summary>
    public class BlackUserConfiguration : IEntityTypeConfiguration<BlackUser>
    {
        public void Configure(EntityTypeBuilder<BlackUser> builder)
        {
            builder.ToTable("user_blacklist");

            builder.Property(x => x.ID)
                .HasColumnName("id")
                .HasColumnType("char(38)")
                .HasMaxLength(38)
                .IsRequired();

            builder.Property(x => x.UserID)
                .HasColumnName("user_id")
                .HasColumnType("char(38)")
                .HasMaxLength(38)
                .IsRequired();

            builder.Property(x => x.Reason)
                .HasColumnName("reason")
                .HasColumnType("char(128)")
                .HasMaxLength(128)
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

            builder.HasOne<User>()
                .WithOne()
                .HasForeignKey<BlackUser>(s => s.UserID)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();
        }
    }
}
