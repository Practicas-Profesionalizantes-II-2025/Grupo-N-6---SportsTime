using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Shared.Dtos;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using MVCSPortTime1.ViewModels;

namespace MVCSPortTime1.Controllers
{
    public class DeportesController : Controller
    {
        private readonly HttpClient _httpClient;

        public DeportesController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient();

            var baseUrl = configuration["ApiSettings:BaseUrl"];
            if (string.IsNullOrEmpty(baseUrl))
                throw new Exception("No se encontró la configuración de la URL base de la API.");

            _httpClient.BaseAddress = new Uri(baseUrl);
        }

        // GET: Deportes
        public async Task<IActionResult> Index()
        {
            var vm = new DeporteIndexViewModel { NuevoDeporte = new DeporteDTO() };

            var response = await _httpClient.GetAsync("/api/deportes");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                vm.Deportes = JsonConvert.DeserializeObject<List<DeporteDTO>>(json) ?? new();
            }
            else
            {
                vm.Deportes = new List<DeporteDTO>();
            }

            return View(vm);
        }

        // Reemplaza las líneas donde se llama a RecargarIndex en los métodos Create y Edit por un return View(model);
        // y en Delete por return View(deporte);
        // Así se devuelve correctamente un IActionResult.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DeporteIndexViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await RecargarIndex(model, "Corrige los errores del formulario.");
                return View("Index", model);
            }

            var json = JsonConvert.SerializeObject(model.NuevoDeporte);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var responseApi = await _httpClient.PostAsync("/api/deportes", content);
            if (responseApi.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            await RecargarIndex(model, "Error al crear el deporte.");
            return View("Index", model);
        }

        // GET: Deportes/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var dep = await GetDeporte(id);
            if (dep == null) return NotFound();
            return View(dep);
        }

        // GET: Deportes/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var dep = await GetDeporte(id);
            if (dep == null) return NotFound();
            return View(dep);
        }

        // POST: Deportes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DeporteDTO deporte)
        {
            if (!ModelState.IsValid)
                return View(deporte);

            var json = JsonConvert.SerializeObject(deporte);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"/api/deportes/{deporte.Deporte_ID}", content);

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Error al editar el deporte.");
            return View(deporte);
        }

        // GET: Deportes/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var dep = await GetDeporte(id);
            if (dep == null) return NotFound();
            return View(dep);
        }

        // POST: Deportes/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(DeporteDTO deporte)
        {
            var response = await _httpClient.DeleteAsync($"/api/deportes/{deporte.Deporte_ID}");
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Error al eliminar el deporte.");
            return View(deporte);
        }

        private async Task<DeporteIndexViewModel> RecargarIndex(DeporteIndexViewModel model, string error)
        {
            ModelState.AddModelError("", error);

            var response = await _httpClient.GetAsync("/api/deportes");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                model.Deportes = JsonConvert.DeserializeObject<List<DeporteDTO>>(json) ?? new();
            }
            else
            {
                model.Deportes = new List<DeporteDTO>();
            }
            return model;
        }

        private async Task<DeporteDTO?> GetDeporte(int id)
        {
            var resp = await _httpClient.GetAsync($"/api/deportes/{id}");
            if (!resp.IsSuccessStatusCode) return null;

            var json = await resp.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<DeporteDTO>(json);
        }
    }
}