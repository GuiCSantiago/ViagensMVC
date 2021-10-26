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
    public class ViagensController : Controller
    {
        private readonly ViagemService _viagemService;
        private readonly VeiculoService _veiculoService;
        private readonly MotoristaService _motoristaService;

        public ViagensController(ViagemService viagemService, VeiculoService veiculoService, MotoristaService motoristaService)
        {
            _viagemService = viagemService;
            _veiculoService = veiculoService;
            _motoristaService = motoristaService;
        }

        public async Task<ActionResult> Index()
        {
            var list = await _viagemService.FindAllAsync();

            return View("Listagem", list);
        }

        public async Task<ActionResult> ListaViagensDia(DateTime? dia)
        {
            if (!dia.HasValue)
                dia = DateTime.Now;

            ViewData["dia"] = dia.Value.ToString("dd-MM-yyyy");

            var list = await _viagemService.FindAllByDateAsync(dia);

            return View("Listagem", list);
        }


        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
                return View("Error", new ErrorViewModel("Id nulo"));

            var obj = await _viagemService.FindByIdAsync(id.Value);
            if (obj == null)
                return View("Error", new ErrorViewModel("Id não encontrado"));

            return View(obj);
        }

        public async Task<ActionResult> UpdateViagens()
        {
            List<Viagem> viagens = await _viagemService.FindAllAsync();
            viagens.ForEach(viagem => viagem.UpdateStatus());
            return View("Listagem", viagens);
        }

        public async Task<ActionResult> Create()
        {
            try
            {
                var veiculos = await _veiculoService.FindAllAsync();
                var motoristas = await _motoristaService.FindAllAsync();
                var viewModel = new ViagemFormViewModel { Veiculos = veiculos, Motoristas = motoristas };

                return View(viewModel);
            }
            catch (Exception e)
            {
                return View("Error", new ErrorViewModel(e.Message));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Viagem viagem)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var veiculos = await _veiculoService.FindAllAsync();
                    var motoristas = await _motoristaService.FindAllAsync();
                    var viewModel = new ViagemFormViewModel { Veiculos = veiculos, Motoristas = motoristas };
                }

                await _viagemService.InsertAsync(viagem);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return View("Error", new ErrorViewModel(e.Message));
            }
        }


        public async Task<ActionResult> Edit(int? id)
        {
            try
            {
                var veiculos = await _veiculoService.FindAllAsync();
                var motoristas = await _motoristaService.FindAllAsync();
                var viagem = await _viagemService.FindByIdAsync(id);
                var viewModel = new ViagemFormViewModel { Veiculos = veiculos, Motoristas = motoristas, Viagem = viagem };

                return View(viewModel);
            }
            catch (Exception e)
            {
                return View("Error", new ErrorViewModel(e.Message));
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Viagem viagem)
        {
            if (!ModelState.IsValid)
            {
                var veiculos = await _veiculoService.FindAllAsync();
                var motoristas = await _motoristaService.FindAllAsync();
                var viagemAntiga = await _viagemService.FindByIdAsync(viagem.Id);
                var viewModel = new ViagemFormViewModel { Veiculos = veiculos, Motoristas = motoristas, Viagem = viagem };
            }

            try
            {
                await _viagemService.UpdateAsync(viagem);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return View("Error", new ErrorViewModel(e.Message));
            }
        }


        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
                return View("Error", new ErrorViewModel("Id nulo"));

            var obj = await _viagemService.FindByIdAsync(id.Value);
            if (obj == null)
                return View("Error", new ErrorViewModel("Id não encontrado"));

            return View(obj);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                await _viagemService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException e)
            {
                return View("Error", new ErrorViewModel(e.Message));
            }
        }

        public async Task<ActionResult> MotoristasRota()
        {
            return View(new MotoristaRotaViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> MotoristasRota(MotoristaRotaViewModel viewModel)
        {
            var list = await _viagemService.FindAllAsync();

            viewModel.Motoristas = list.Where(v => v.Rota.Equals(viewModel?.Rota)).Select(obj => obj.Motorista).ToList();

            return View(viewModel);
        }

    }
}
