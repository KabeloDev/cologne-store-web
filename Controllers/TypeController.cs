using CologneStore.Constants;
using CologneStore.DTO;
using CologneStore.Models;
using CologneStore.Repositories;
using Humanizer.Localisation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CologneStore.Controllers
{
    [Authorize(Roles = nameof(Roles.Admin))]
    public class TypeController : Controller
    {
        private readonly ITypeRepository _typeRepository;

        public TypeController(ITypeRepository typeRepository)
        {
            _typeRepository = typeRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _typeRepository.GetAllTypes());
        }

        public IActionResult AddType()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddType(TypeDTO type)
        {
            if (!ModelState.IsValid)
            {
                return View(type);
            }

            try
            {
                var typeToAdd = new CologneType
                {
                    TypeName = type.TypeName,
                    Id = type.Id,
                };
                await _typeRepository.AddType(typeToAdd);
                TempData["successMessage"] = "Type added successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                TempData["errorMessage"] = "Type has not been added";
                return View(type);
            }
        }

        public async Task<IActionResult> UpdateType(int id)
        {
            var type = await _typeRepository.GetTypeById(id);
            if (type == null)
                throw new InvalidOperationException($"Type with id: {id} was not found");

            var typeToUpdate = new TypeDTO
            {
                Id = type.Id,
                TypeName = type.TypeName,
            };

            return View(typeToUpdate);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateType(TypeDTO typeDTO)
        {
            if (!ModelState.IsValid)
            {
                return View(typeDTO);
            }

            try
            {
                var type = new CologneType
                {
                    TypeName = typeDTO.TypeName,
                    Id = typeDTO.Id,
                };

                await _typeRepository.UpdateType(type);
                TempData["successMessage"] = "Type updated successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                TempData["errorMessage"] = "Type has not been updated";
                return View(typeDTO);
            }
        }

        public async Task<IActionResult> DeleteType(int id)
        {
            var type = await _typeRepository.GetTypeById(id);
            if (type == null)
                throw new InvalidOperationException($"Type with id: {id} was not found");
            await _typeRepository.DeleteType(type);
            return RedirectToAction(nameof(Index));
        }
    }
}
