using AutoBid.WebApi.Data;
using Microsoft.EntityFrameworkCore;
using WebApi.Interfaces.Models;

public class UserSet {
    private readonly AutoBidDbContext _context;
    
    public UserSet(AutoBidDbContext context)
    {
        _context = context;
    }

    public Guid Create(UserModel user)
    {
        throw new NotImplementedException();
    }

    public void Update(Guid id, UserModel user)
    {
        throw new NotImplementedException();
    }

    public void Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<UserModel>> GetAll()
    {
        return await _context.Users
            .Select(e => e.ToModel())
            .ToListAsync();
    }
}