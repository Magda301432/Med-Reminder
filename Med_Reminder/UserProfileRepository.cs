using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Med_Reminder
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly MyAppDbContext _dbContext;

        

        public UserProfileRepository(MyAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<DaneOsobowe> GetDaneOsoboweAsync(int userId)
        {
            //int userIdInt = int.Parse(userId);
            return await _dbContext.__dane_osobowe.FirstOrDefaultAsync(u => u.Id == userId);
        }
        public async Task SaveChanges()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void UpdateDaneOsobowe(DaneOsobowe daneOsobowe)
        {
            // Oznacz obiekt jako zmieniony, aby został uwzględniony przy zapisie zmian
            _dbContext.Entry(daneOsobowe).State = EntityState.Modified;
        }
    }
}
