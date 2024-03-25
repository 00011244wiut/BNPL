using Application.Contracts.Repositories;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories;

public class UserDocumentsRepository : GenericRepository<UserDocumentsEntity>, IUserDocumentsRepository
{
    private readonly ProjectDbContext _context;
    
    public UserDocumentsRepository(ProjectDbContext context) : base(context)
    {
        _context = context;
    }
}

// @UserDocuments