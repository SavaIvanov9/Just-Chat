using System;
using JustChat.Domain.Interfaces;
using JustChat.Persistence.Commands.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pluralize.NET;

namespace JustChat.Persistence.Commands.Configurations
{
    public abstract class BaseConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
           where TEntity : class, IEntity
    {
        protected static readonly Pluralizer Pluralizer = new Pluralizer();

        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.ToTable(Pluralizer.Pluralize(typeof(TEntity).Name));

            builder
                .Property<bool>(nameof(IDeletable.IsDeleted))
                .IsRequired(true)
                .HasDefaultValue(false);

            builder
                .Property<DateTime?>(nameof(IDeletable.DeletedOn))
                .IsRequired(false);

            builder
                .HasQueryFilter(x => EF.Property<bool>(x, nameof(IDeletable.IsDeleted)) == false);

            builder.HasKey(x => x.Id);
            builder.Ignore(x => x.DomainEvents);
        }
    }
}
