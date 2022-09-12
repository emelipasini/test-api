using Microsoft.AspNetCore.Mvc;

using WebAPI.Contracts;
using WebAPI.Models;

namespace WebAPI.Services
{
    public class StreetService: IStreetService
    {
        public IActionResult GetAll() => throw new NotImplementedException();
        public IActionResult GetById(int id) => throw new NotImplementedException();
        public IActionResult Create(Street street) => throw new NotImplementedException();
        public IActionResult Update(int id, Street street) => throw new NotImplementedException();
        public IActionResult Delete(int id) => throw new NotImplementedException();
    }
}
