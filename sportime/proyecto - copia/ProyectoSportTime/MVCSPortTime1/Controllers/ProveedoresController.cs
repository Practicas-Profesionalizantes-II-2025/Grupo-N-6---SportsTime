using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using MVCSPortTime1.ViewModels;

public class ProveedoresController : Controller
{
    private readonly HttpClient _httpClient;

    public ProveedoresController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClient = httpClientFactory.CreateClient();

        // Leer la URL base desde appsettings.json
        var baseUrl = configuration["ApiSettings:BaseUrl"];
        if (string.IsNullOrEmpty(baseUrl))
            throw new Exception("No se encontró la configuración de la URL base de la API.");

        _httpClient.BaseAddress = new Uri(baseUrl);
    }

    // GET: Proveedores
    public async Task<IActionResult> Index()
    {
        var viewModel = new ProveedorIndexViewModel();

        var response = await _httpClient.GetAsync("/api/proveedores");
        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            viewModel.Proveedores = JsonConvert.DeserializeObject<List<ProveedorDTO>>(json);
        }

        viewModel.NuevoProveedor = new ProveedorDTO();

        return View(viewModel);
    }

    // POST: Proveedores/Create (el formulario de alta está en Index)
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProveedorIndexViewModel model)
    {
        if (!ModelState.IsValid)
        {
            // Recargar la lista para que la tabla no desaparezca
            var response = await _httpClient.GetAsync("/api/proveedores");
            if (response.IsSuccessStatusCode)
            {
                var jsonStr = await response.Content.ReadAsStringAsync();
                model.Proveedores = JsonConvert.DeserializeObject<List<ProveedorDTO>>(jsonStr);
            }
            else
            {
                model.Proveedores = new List<ProveedorDTO>();
            }
            return View("Index", model);
        }

        var json = JsonConvert.SerializeObject(model.NuevoProveedor);
        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

        var responseApi = await _httpClient.PostAsync("/api/proveedores", content);
        if (responseApi.IsSuccessStatusCode)
            return RedirectToAction(nameof(Index));

        ModelState.AddModelError("", "Error al crear el proveedor.");

        // Recargar la lista si hay error
        var response2 = await _httpClient.GetAsync("/api/proveedores");
        if (response2.IsSuccessStatusCode)
        {
            var jsonStr = await response2.Content.ReadAsStringAsync();
            model.Proveedores = JsonConvert.DeserializeObject<List<ProveedorDTO>>(jsonStr);
        }
        else
        {
            model.Proveedores = new List<ProveedorDTO>();
        }
        return View("Index", model);
    }

    // GET: Proveedores/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var response = await _httpClient.GetAsync($"/api/proveedores/{id}");
        if (!response.IsSuccessStatusCode)
            return NotFound();

        var json = await response.Content.ReadAsStringAsync();
        var proveedor = JsonConvert.DeserializeObject<ProveedorDTO>(json);

        return View(proveedor);
    }

    // GET: Proveedores/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var response = await _httpClient.GetAsync($"/api/proveedores/{id}");
        if (!response.IsSuccessStatusCode)
            return NotFound();

        var json = await response.Content.ReadAsStringAsync();
        var proveedor = JsonConvert.DeserializeObject<ProveedorDTO>(json);

        return View(proveedor);
    }

    // POST: Proveedores/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(ProveedorDTO proveedor)
    {
        if (ModelState.IsValid)
        {
            var json = JsonConvert.SerializeObject(proveedor);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"/api/proveedores/{proveedor.Proveedor_ID}", content);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Error al editar el proveedor.");
        }
        return View(proveedor);
    }

    // GET: Proveedores/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _httpClient.GetAsync($"/api/proveedores/{id}");
        if (!response.IsSuccessStatusCode)
            return NotFound();

        var json = await response.Content.ReadAsStringAsync();
        var proveedor = JsonConvert.DeserializeObject<ProveedorDTO>(json);

        return View(proveedor);
    }

    // POST: Proveedores/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(ProveedorDTO proveedor)
    {
        var response = await _httpClient.DeleteAsync($"/api/proveedores/{proveedor.Proveedor_ID}");
        if (response.IsSuccessStatusCode)
            return RedirectToAction(nameof(Index));

        ModelState.AddModelError("", "Error al eliminar el proveedor.");
        return View(proveedor);
    }
}