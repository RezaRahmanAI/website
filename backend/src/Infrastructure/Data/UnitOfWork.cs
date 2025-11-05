using TechNova.Core.Entities;
using TechNova.Core.Interfaces;
using TechNova.Infrastructure.Repositories;

namespace TechNova.Infrastructure.Data;

public class UnitOfWork : IUnitOfWork, IAsyncDisposable
{
    private readonly ApplicationDbContext _dbContext;

    public UnitOfWork(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        Users = new RepositoryBase<ApplicationUser>(_dbContext);
        Courses = new RepositoryBase<Course>(_dbContext);
        CourseCategories = new RepositoryBase<CourseCategory>(_dbContext);
        Instructors = new RepositoryBase<InstructorProfile>(_dbContext);
        Enrollments = new RepositoryBase<Enrollment>(_dbContext);
        PortfolioItems = new RepositoryBase<PortfolioItem>(_dbContext);
        Services = new RepositoryBase<ServiceOffering>(_dbContext);
        ServiceInquiries = new RepositoryBase<ServiceInquiry>(_dbContext);
        BlogPosts = new RepositoryBase<BlogPost>(_dbContext);
        Payments = new RepositoryBase<PaymentTransaction>(_dbContext);
        Notifications = new RepositoryBase<Notification>(_dbContext);
        Certificates = new RepositoryBase<Certificate>(_dbContext);
    }

    public IRepository<ApplicationUser> Users { get; }
    public IRepository<Course> Courses { get; }
    public IRepository<CourseCategory> CourseCategories { get; }
    public IRepository<InstructorProfile> Instructors { get; }
    public IRepository<Enrollment> Enrollments { get; }
    public IRepository<PortfolioItem> PortfolioItems { get; }
    public IRepository<ServiceOffering> Services { get; }
    public IRepository<ServiceInquiry> ServiceInquiries { get; }
    public IRepository<BlogPost> BlogPosts { get; }
    public IRepository<PaymentTransaction> Payments { get; }
    public IRepository<Notification> Notifications { get; }
    public IRepository<Certificate> Certificates { get; }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }

    public ValueTask DisposeAsync()
    {
        return _dbContext.DisposeAsync();
    }
}
