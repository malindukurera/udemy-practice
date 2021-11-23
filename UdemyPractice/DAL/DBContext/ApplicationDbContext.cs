using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DAL.Model;
using DAL.Model.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.DBContext
{
    public class ApplicationDbContext : IdentityDbContext
        <AppUser, AppRole, int, IdentityUserClaim<int>, AppUserRole, IdentityUserLogin<int>, 
            IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        private static MethodInfo _propertyMethod = typeof(EF).GetMethod(
            nameof(EF.Property), BindingFlags.Static | BindingFlags.Public)?
            .MakeGenericMethod(typeof(bool));

        private const string IsDeletedProperty = "IsDeleted";

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseStudent> CourseStudents { get; set; }

        private static LambdaExpression GetIsDeleteRestriction(Type type)
        {
            var param = Expression.Parameter(type, "it");
            var prop = Expression.Call(_propertyMethod, param, Expression.Constant(IsDeletedProperty));
            var condition = Expression.MakeBinary(ExpressionType.Equal, prop, Expression.Constant(false));
            var lambda = Expression.Lambda(condition, param);
            return lambda;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(ISoftDeletable).IsAssignableFrom(entity.ClrType))
                {
                    entity.AddProperty(IsDeletedProperty, typeof(bool));
                    modelBuilder.Entity(entity.ClrType).HasQueryFilter(GetIsDeleteRestriction(entity.ClrType));
                }
            }

            modelBuilder.Entity<CourseStudent>().HasKey(cs => new {cs.CourseId, cs.StudentId});
            modelBuilder.Entity<CourseStudent>().HasOne(cs => cs.Course)
                .WithMany(cs => cs.CourseStudents).HasForeignKey(cs => cs.CourseId);
            modelBuilder.Entity<CourseStudent>().HasOne(cs => cs.Student)
                .WithMany(cs => cs.CourseStudents).HasForeignKey(cs => cs.StudentId);

            modelBuilder.Entity<AppUser>(b =>
            {
                b.HasMany(e => e.AppUserRoles).WithOne(e => e.AppUser)
                    .HasForeignKey(ur => ur.UserId).IsRequired();
            });

            modelBuilder.Entity<AppRole>(b =>
            {
                b.HasMany(e => e.AppUserRoles).WithOne(e => e.AppRole)
                    .HasForeignKey(ur => ur.RoleId).IsRequired();
            });

            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
        {
            OnBeforeSaveData();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaveData();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        private void OnBeforeSaveData()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.State != EntityState.Detached && e.State != EntityState.Unchanged);

            foreach (var entry in entries)
            {
                if (entry.Entity is ITrackable trackable)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            trackable.CreatedAt = DateTimeOffset.Now;
                            trackable.LastUpdatedAt = DateTimeOffset.Now;
                            break;

                        case EntityState.Modified:
                            trackable.LastUpdatedAt = DateTimeOffset.Now;
                            break;

                        case EntityState.Deleted:
                            entry.Property(IsDeletedProperty).CurrentValue = true;
                            entry.State = EntityState.Modified;
                            break;
                    }
                }
            }
        }
    }
}
