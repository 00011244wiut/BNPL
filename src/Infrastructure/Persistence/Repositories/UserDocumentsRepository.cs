// Importing necessary namespaces and contracts
using Application.Contracts.Repositories;
using Domain.Entities;

// Namespace for Persistence repository implementation
namespace Infrastructure.Persistence.Repositories;

// Repository implementation for managing user documents
public class UserDocumentsRepository : GenericRepository<UserDocumentsEntity>, IUserDocumentsRepository
{
    private readonly ProjectDbContext _context;
    
    // Constructor to initialize UserDocumentsRepository with database context
    public UserDocumentsRepository(ProjectDbContext context) : base(context)
    {
        _context = context;
    }
}