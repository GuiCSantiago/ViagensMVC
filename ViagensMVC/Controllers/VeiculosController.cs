using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViagensMVC.Models;
using ViagensMVC.Models.ViewModels;
using ViagensMVC.Services;
using ViagensMVC.Services.Exceptions;

namespace ViagensMVC.Controllers
{
    public class VeiculosController : Controller
    {
        private readonly VeiculoService _veiculoService;
        private readonly ItensService _itensService;

        public VeiculosController(VeiculoService veiculoService, ItensService itensService)
        {
            _veiculoService = veiculoService;
            _itensService = itensService;
        }

        public async Task<ActionResult> Index()
        {
            var list = await _veiculoService.FindAllAsync();

            return View("Listagem", list);
        }

        public async Task<ActionResult> DeleteItem(int? id)
        {
            if (id == null)
                return View("Error", new ErrorViewModel("Id nulo"));

            var obj = await _veiculoService.FindByIdAsync(id.Value);
            if (obj == null)
                return View("Error", new ErrorViewModel("Id não encontrado"));

            var viewModel = new ItensVeiculoViewModel { Veiculo = obj };

            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteItem(Item item)
        {
            try
            {
                await _itensService.RemoveAsync(item.Id);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException e)
            {
                return View("Error", new ErrorViewModel(e.Message));
            }
        }
    }
}
