using Microsoft.AspNetCore.Mvc;

using WebAPI.Models;

namespace WebAPI.Contracts
{
    public interface IStreetService
    {
        public IActionResult GetAll();
        public IActionResult GetById(int id);
        public IActionResult Create(Street street);
        public IActionResult Update(int id, Street street);
        public IActionResult Delete(int id);
    }
}
