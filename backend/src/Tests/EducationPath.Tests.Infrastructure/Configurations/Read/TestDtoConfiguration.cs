using EducationPath.Tests.Contracts.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationPath.Tests.Infrastructure.Configurations.Read;

public class TestDtoConfiguration : IEntityTypeConfiguration<TestDto>
{
    public void Configure(EntityTypeBuilder<TestDto> builder)
    {
        builder.ToTable("tests");

        builder.HasKey(t => t.Id);

        builder.HasMany(t => t.Questions)
            .WithOne(q => q.Test)
            .HasForeignKey(q => q.TestId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}