using Microsoft.EntityFrameworkCore;
using TechNova.Core.Entities;

namespace TechNova.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<ApplicationUser> Users => Set<ApplicationUser>();
    public DbSet<InstructorProfile> InstructorProfiles => Set<InstructorProfile>();
    public DbSet<Course> Courses => Set<Course>();
    public DbSet<CourseCategory> CourseCategories => Set<CourseCategory>();
    public DbSet<CourseSection> CourseSections => Set<CourseSection>();
    public DbSet<Lesson> Lessons => Set<Lesson>();
    public DbSet<Enrollment> Enrollments => Set<Enrollment>();
    public DbSet<CourseReview> Reviews => Set<CourseReview>();
    public DbSet<PortfolioItem> PortfolioItems => Set<PortfolioItem>();
    public DbSet<PortfolioMedia> PortfolioMedia => Set<PortfolioMedia>();
    public DbSet<PortfolioTag> PortfolioTags => Set<PortfolioTag>();
    public DbSet<ServiceOffering> ServiceOfferings => Set<ServiceOffering>();
    public DbSet<ServicePackage> ServicePackages => Set<ServicePackage>();
    public DbSet<ServiceHighlight> ServiceHighlights => Set<ServiceHighlight>();
    public DbSet<ServiceInquiry> ServiceInquiries => Set<ServiceInquiry>();
    public DbSet<BlogPost> BlogPosts => Set<BlogPost>();
    public DbSet<BlogTag> BlogTags => Set<BlogTag>();
    public DbSet<PaymentTransaction> PaymentTransactions => Set<PaymentTransaction>();
    public DbSet<Notification> Notifications => Set<Notification>();
    public DbSet<Certificate> Certificates => Set<Certificate>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ApplicationUser>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<ApplicationUser>()
            .HasMany(u => u.Courses)
            .WithOne(c => c.Instructor!.User)
            .HasForeignKey(c => c.InstructorId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Course>()
            .HasMany(c => c.Categories)
            .WithMany(category => category.Courses);

        modelBuilder.Entity<CourseSection>()
            .HasMany(section => section.Lessons)
            .WithOne(lesson => lesson.Section)
            .HasForeignKey(lesson => lesson.SectionId);

        modelBuilder.Entity<PortfolioItem>()
            .HasMany(item => item.Media)
            .WithOne(media => media.PortfolioItem)
            .HasForeignKey(media => media.PortfolioItemId);

        modelBuilder.Entity<PortfolioItem>()
            .HasMany(item => item.Tags)
            .WithOne(tag => tag.PortfolioItem)
            .HasForeignKey(tag => tag.PortfolioItemId);

        modelBuilder.Entity<BlogPost>()
            .HasMany(post => post.Tags)
            .WithOne(tag => tag.BlogPost)
            .HasForeignKey(tag => tag.BlogPostId);

        modelBuilder.Entity<InstructorProfile>()
            .HasOne(profile => profile.User)
            .WithOne(user => user.InstructorProfile)
            .HasForeignKey<InstructorProfile>(profile => profile.UserId);
    }
}
