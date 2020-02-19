using AvDe.Contracts.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AvDe.Persistence.Configurations
{
	public class ArticleConfiguration
	{
		public ArticleConfiguration(EntityTypeBuilder<Article> entity)
		{
			// Table
			entity.ToTable("Articles");

			// Keys
			entity.HasKey(e => e.Id);

			// Properties
			entity.Property(e => e.Id)
				.HasMaxLength(32);
			entity.Property(e => e.Name)
				.IsRequired()
				.HasMaxLength(128);
			entity.Property(e => e.Price)
				.IsRequired();
		}
	}
}
