using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Shared.Dtos;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using MVCSPortTime1.ViewModels;

namespace MVCSPortTime1.Controllers
{
    public class CanchasController : Controller
    {
        private readonly HttpClient _httpClient;

        public CanchasController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient();

            var baseUrl = configuration["ApiSettings:BaseUrl"];
            if (string.IsNullOrEmpty(baseUrl))
                throw new Exception("No se encontró la configuración de la URL base de la API.");

            _httpClient.BaseAddress = new Uri(baseUrl);
        }

        // GET: Canchas
        public async Task<IActionResult> Index()
        {
            var vm = new CanchaIndexViewModel
            {
                NuevaCancha = new CanchaDTO()
            };

            // Deportes para dropdown
            var respDep = await _httpClient.GetAsync("/api/deportes");
            var deportes = new List<DeporteDTO>();
            if (respDep.IsSuccessStatusCode)
            {
                var jsonDep = await respDep.Content.ReadAsStringAsync();
                deportes = JsonConvert.DeserializeObject<List<DeporteDTO>>(jsonDep) ?? new();
            }
            vm.Deportes = deportes
                .Select(d => new SelectListItem { Value = d.Deporte_ID.ToString(), Text = d.Nombre })
                .ToList();

            // Canchas + nombre del deporte
            var respCan = await _httpClient.GetAsync("/api/canchas");
            if (respCan.IsSuccessStatusCode)
            {
                var jsonCan = await respCan.Content.ReadAsStringAsync();
                var canchas = JsonConvert.DeserializeObject<List<CanchaDTO>>(jsonCan) ?? new();
                var dictDep = deportes.ToDictionary(d => d.Deporte_ID, d => d.Nombre);

                vm.Canchas = canchas
                    .Select(c => new CanchaViewModel
                    {
                        Cancha_ID = c.Cancha_ID,
                        Deporte_ID = c.Deporte_ID,
                        DeporteNombre = dictDep.TryGetValue(c.Deporte_ID, out var nom) ? nom : $"ID {c.Deporte_ID}",
                        Activa = c.Activa
                    })
                    .ToList();
            }
            else
            {
                vm.Canchas = new List<CanchaViewModel>();
            }

            return View(vm);
        }

        // En el método Create, reemplaza las llamadas a RecargarIndex por return await RecargarIndex(...);
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CanchaIndexViewModel model)
        {
            if (!ModelState.IsValid)
                return await RecargarIndex(model, "Corrige los errores del formulario.");

            var json = JsonConvert.SerializeObject(model.NuevaCancha);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var responseApi = await _httpClient.PostAsync("/api/canchas", content);
            if (responseApi.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            return await RecargarIndex(model, "Error al crear la cancha.");
        }

        // GET: Canchas/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var vm = await GetCanchaViewModel(id);
            if (vm == null) return NotFound();
            return View(vm);
        }

        // GET: Canchas/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var resp = await _httpClient.GetAsync($"/api/canchas/{id}");
            if (!resp.IsSuccessStatusCode) return NotFound();

            var json = await resp.Content.ReadAsStringAsync();
            var cancha = JsonConvert.DeserializeObject<CanchaDTO>(json);
            if (cancha == null) return NotFound();

            await CargarDeportesEnViewBag();
            return View(cancha);
        }

        // POST: Canchas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CanchaDTO cancha)
        {
            if (!ModelState.IsValid)
            {
                await CargarDeportesEnViewBag();
                return View(cancha);
            }

            var json = JsonConvert.SerializeObject(cancha);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"/api/canchas/{cancha.Cancha_ID}", content);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Error al editar la cancha.");
            await CargarDeportesEnViewBag();
            return View(cancha);
        }

        // GET: Canchas/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var vm = await GetCanchaViewModel(id);
            if (vm == null) return NotFound();
            return View(vm);
        }

        // POST: Canchas/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(CanchaViewModel cancha)
        {
            var response = await _httpClient.DeleteAsync($"/api/canchas/{cancha.Cancha_ID}");
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Error al eliminar la cancha.");
            return View(cancha);
        }

        // Reemplaza las líneas donde se llama a RecargarIndex en los métodos Create y Edit por un return View(model)
        // y asegúrate de que RecargarIndex devuelva una View con el modelo recargado.

        private async Task<IActionResult> RecargarIndex(CanchaIndexViewModel model, string error)
        {
            ModelState.AddModelError("", error);

            // Recargar deportes
            var respDep = await _httpClient.GetAsync("/api/deportes");
            var deportes = new List<DeporteDTO>();
            if (respDep.IsSuccessStatusCode)
            {
                var jsonDep = await respDep.Content.ReadAsStringAsync();
                deportes = JsonConvert.DeserializeObject<List<DeporteDTO>>(jsonDep) ?? new();
            }
            model.Deportes = deportes
                .Select(d => new SelectListItem { Value = d.Deporte_ID.ToString(), Text = d.Nombre })
                .ToList();

            // Recargar canchas
            var respCan = await _httpClient.GetAsync("/api/canchas");
            if (respCan.IsSuccessStatusCode)
            {
                var jsonCan = await respCan.Content.ReadAsStringAsync();
                var canchas = JsonConvert.DeserializeObject<List<CanchaDTO>>(jsonCan) ?? new();
                var dictDep = deportes.ToDictionary(d => d.Deporte_ID, d => d.Nombre);

                model.Canchas = canchas
                    .Select(c => new CanchaViewModel
                    {
                        Cancha_ID = c.Cancha_ID,
                        Deporte_ID = c.Deporte_ID,
                        DeporteNombre = dictDep.TryGetValue(c.Deporte_ID, out var nom) ? nom : $"ID {c.Deporte_ID}",
                        Activa = c.Activa
                    })
                    .ToList();
            }
            else
            {
                model.Canchas = new List<CanchaViewModel>();
            }

            return View(model);
        }

        private async Task CargarDeportesEnViewBag()
        {
            var respDep = await _httpClient.GetAsync("/api/deportes");
            var deportes = new List<DeporteDTO>();
            if (respDep.IsSuccessStatusCode)
            {
                var jsonDep = await respDep.Content.ReadAsStringAsync();
                deportes = JsonConvert.DeserializeObject<List<DeporteDTO>>(jsonDep) ?? new();
            }
            ViewBag.Deportes = deportes
                .Select(d => new SelectListItem { Value = d.Deporte_ID.ToString(), Text = d.Nombre })
                .ToList();
        }

        private async Task<CanchaViewModel?> GetCanchaViewModel(int id)
        {
            var resp = await _httpClient.GetAsync($"/api/canchas/{id}");
            if (!resp.IsSuccessStatusCode) return null;

            var json = await resp.Content.ReadAsStringAsync();
            var cancha = JsonConvert.DeserializeObject<CanchaDTO>(json);
            if (cancha == null) return null;

            string deporteNombre = $"ID {cancha.Deporte_ID}";
            var respDep = await _httpClient.GetAsync($"/api/deportes/{cancha.Deporte_ID}");
            if (respDep.IsSuccessStatusCode)
            {
                var jsonDep = await respDep.Content.ReadAsStringAsync();
                var dep = JsonConvert.DeserializeObject<DeporteDTO>(jsonDep);
                if (dep != null) deporteNombre = dep.Nombre;
            }

            return new CanchaViewModel
            {
                Cancha_ID = cancha.Cancha_ID,
                Deporte_ID = cancha.Deporte_ID,
                DeporteNombre = deporteNombre,
                Activa = cancha.Activa
            };
        }
    }
}