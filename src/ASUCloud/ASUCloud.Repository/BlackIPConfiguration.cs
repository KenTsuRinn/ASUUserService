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
    /// BlackIPConfiguration
    /// この設定はsqlite用であり、
    /// 他のデータベースへの変更はデータ型を変更する必要がある。
    /// </summary>
    public class BlackIPConfiguration : IEntityTypeConfiguration<BlackIP>
    {
        public void Configure(EntityTypeBuilder<BlackIP> builder)
        {
            builder.ToTable("ip_blacklist");

            builder.Property(x => x.ID)
                .HasColumnName("id")
                .HasColumnType("char(38)")
                .HasMaxLength(38)
                .IsRequired();

            builder.Property(x => x.IP)
                .HasColumnName("ip")
                .HasColumnType("char(45)")
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
        }
    }
}
