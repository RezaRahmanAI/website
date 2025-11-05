using TechNova.Core.Entities;

namespace TechNova.Core.Interfaces;

public interface IUnitOfWork
{
    IRepository<ApplicationUser> Users { get; }
    IRepository<Course> Courses { get; }
    IRepository<CourseCategory> CourseCategories { get; }
    IRepository<InstructorProfile> Instructors { get; }
    IRepository<Enrollment> Enrollments { get; }
    IRepository<PortfolioItem> PortfolioItems { get; }
    IRepository<ServiceOffering> Services { get; }
    IRepository<ServiceInquiry> ServiceInquiries { get; }
    IRepository<BlogPost> BlogPosts { get; }
    IRepository<PaymentTransaction> Payments { get; }
    IRepository<Notification> Notifications { get; }
    IRepository<Certificate> Certificates { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
