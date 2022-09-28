using Microsoft.EntityFrameworkCore;

using WebAPI.Models;

namespace WebAPI.Services
{
    public interface IStreetService
    {
        public Task<List<Street>> GetAll();
        public Task<Street> GetById(int id);
        public Task<Street> Create(Street street);
        public Task<Street> Update(Street street);
        public Task<bool> Delete(int id);
    }

    public class StreetService : IStreetService
    {
        public readonly string Entity = "Streets";
        private readonly APIDbContext _context;

        public StreetService(APIDbContext context)
        {
            _context = context;
        }

        private void AddLog(string action, string message)
        {
            try
            {
                int lastId = _context.Logs.Max(p => p.Id);
                _context.Logs.Add(new Log(lastId + 1, Entity, action, message));
                _context.SaveChanges();
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
        }

        public async Task<List<Street>> GetAll()
        {
            try
            {
                var streets = await _context.Streets.ToListAsync();
                return streets;
            }
            catch (Exception err)
            {
                AddLog("GetAll", err.Message);
                return new List<Street>();
            }
        }
        
        public async Task<Street> GetById(int id)
        {
            try
            {
                var street = await _context.Streets.FirstOrDefaultAsync(s => s.Id == id);
                if(street != null)
                {
                    return street;
                }
                return new Street();
            }
            catch (Exception err)
            {
                AddLog("GetById", err.Message);
                return new Street();
            }
        }

        public async Task<Street> Create(Street street)
        {
            try
            {
                var newStreet = await _context.Streets.AddAsync(street);
                _context.SaveChanges();

                if(newStreet != null)
                {
                    return newStreet.Entity;
                }
                return new Street();
            }
            catch (Exception err)
            {
                AddLog("Create", err.Message);
                return new Street();
            }
        }

        public async Task<Street> Update(Street street)
        {
            try
            {
                var streetToEdit = await _context.Streets.FirstOrDefaultAsync(s => s.Id == street.Id);
                if(streetToEdit != null)
                {
                    streetToEdit.Name = street.Name;
                    streetToEdit.City = street.City;

                    _context.SaveChanges();

                    return streetToEdit;
                }
                return new Street();
            }
            catch (Exception err)
            {
                AddLog("Update", err.Message);
                return new Street();
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var streetToDelete = await _context.Streets.FirstOrDefaultAsync(s => s.Id == id);
                if(streetToDelete != null)
                {
                    _context.Remove(streetToDelete);
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception err)
            {
                AddLog("Delete", err.Message);
                return false;
            }
        }
    }
}

