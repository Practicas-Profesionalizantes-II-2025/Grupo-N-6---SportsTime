using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using MVCSPortTime1.ViewModels;
using MVCSPortTime1.Models.Dtos;
using System;
using System.Text;
using System.Linq;

public class CanchasController : Controller
{
    private readonly HttpClient _httpClient;

    public CanchasController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClient = httpClientFactory.CreateClient();

        var baseUrl = configuration["ApiSettings:BaseUrl"];
        if (string.IsNullOrEmpty(baseUrl))
            throw new Exception("No se encontró la configuración de la URL base de la API (ApiSettings:BaseUrl).");

        _httpClient.BaseAddress = new Uri(baseUrl);
    }

    // GET: /Canchas?search=...
    public async Task<IActionResult> Index(string search = null)
    {
        var vm = new CanchaIndexViewModel();

        var resp = await _httpClient.GetAsync("/api/canchas");
        if (resp.IsSuccessStatusCode)
        {
            var j = await resp.Content.ReadAsStringAsync();
            vm.Canchas = JsonConvert.DeserializeObject<List<CanchaDTO>>(j) ?? new List<CanchaDTO>();
        }
        else
        {
            vm.Canchas = new List<CanchaDTO>();
            TempData["Error"] = "No se pudieron cargar las canchas desde la API.";
        }

        // Lookups (deportes)
        await PopulateLookupsAsync(vm);

        // Asignar nombre de deporte a cada cancha para mostrar en la tabla
        foreach (var c in vm.Canchas)
        {
            c.DeporteNombre = vm.Deportes.FirstOrDefault(d => d.Id == c.Deporte_ID)?.Text ?? "Desconocido";
        }

        // Si hay término de búsqueda, filtrar (ID de cancha, ID de deporte, o nombre de deporte)
        if (!string.IsNullOrWhiteSpace(search))
        {
            var s = search.Trim();
            bool isNumber = int.TryParse(s, out int n);
            vm.Canchas = vm.Canchas.Where(c =>
                (isNumber && (c.Cancha_ID == n || c.Deporte_ID == n)) ||
                (!isNumber && (!string.IsNullOrEmpty(c.DeporteNombre) && c.DeporteNombre.IndexOf(s, StringComparison.OrdinalIgnoreCase) >= 0))
            ).ToList();

            // Si no se encontró nada, opcional: mostrar mensaje
            if (vm.Canchas.Count == 0)
            {
                TempData["Info"] = "No se encontraron canchas para la búsqueda indicada.";
            }
        }

        // Inicializar nuevo formulario
        vm.NuevoCancha = new CanchaDTO { Activa = true };

        return View(vm);
    }

    // POST: /Canchas/Create (el formulario de Create o Index publican aquí)
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CanchaIndexViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var resp = await _httpClient.GetAsync("/api/canchas");
            if (resp.IsSuccessStatusCode)
            {
                var j = await resp.Content.ReadAsStringAsync();
                model.Canchas = JsonConvert.DeserializeObject<List<CanchaDTO>>(j) ?? new List<CanchaDTO>();
            }
            else
            {
                model.Canchas = new List<CanchaDTO>();
            }
            await PopulateLookupsAsync(model);
            // asignar nombres para la tabla
            foreach (var c in model.Canchas)
            {
                c.DeporteNombre = model.Deportes.FirstOrDefault(d => d.Id == c.Deporte_ID)?.Text ?? "Desconocido";
            }
            return View("Index", model);
        }

        var json = JsonConvert.SerializeObject(model.NuevoCancha);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var respApi = await _httpClient.PostAsync("/api/canchas", content);
        if (respApi.IsSuccessStatusCode)
            return RedirectToAction(nameof(Index));

        var error = await respApi.Content.ReadAsStringAsync();
        ModelState.AddModelError("", $"Error al crear la cancha: {respApi.StatusCode} {error}");

        var resp2 = await _httpClient.GetAsync("/api/canchas");
        if (resp2.IsSuccessStatusCode)
        {
            var j = await resp2.Content.ReadAsStringAsync();
            model.Canchas = JsonConvert.DeserializeObject<List<CanchaDTO>>(j) ?? new List<CanchaDTO>();
        }
        else model.Canchas = new List<CanchaDTO>();

        await PopulateLookupsAsync(model);

        foreach (var c in model.Canchas)
        {
            c.DeporteNombre = model.Deportes.FirstOrDefault(d => d.Id == c.Deporte_ID)?.Text ?? "Desconocido";
        }

        return View("Index", model);
    }

    // GET: /Canchas/Create
    public async Task<IActionResult> Create()
    {
        var vm = new CanchaIndexViewModel();
        await PopulateLookupsAsync(vm);
        vm.NuevoCancha = new CanchaDTO { Activa = true };
        return View(vm);
    }

    // GET: /Canchas/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var resp = await _httpClient.GetAsync($"/api/canchas/{id}");
        if (!resp.IsSuccessStatusCode) return NotFound();

        var j = await resp.Content.ReadAsStringAsync();
        var cancha = JsonConvert.DeserializeObject<CanchaDTO>(j);
        if (cancha == null) return NotFound();

        // llenar lookup para obtener nombre
        var vm = new CanchaIndexViewModel();
        await PopulateLookupsAsync(vm);
        cancha.DeporteNombre = vm.Deportes.FirstOrDefault(d => d.Id == cancha.Deporte_ID)?.Text ?? "Desconocido";

        return View(cancha);
    }

    // GET: /Canchas/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var resp = await _httpClient.GetAsync($"/api/canchas/{id}");
        if (!resp.IsSuccessStatusCode) return NotFound();

        var j = await resp.Content.ReadAsStringAsync();
        var cancha = JsonConvert.DeserializeObject<CanchaDTO>(j);
        if (cancha == null) return NotFound();

        var vm = new CanchaIndexViewModel { NuevoCancha = cancha, Canchas = new List<CanchaDTO>() };
        await PopulateLookupsAsync(vm);

        // asignar nombre
        cancha.DeporteNombre = vm.Deportes.FirstOrDefault(d => d.Id == cancha.Deporte_ID)?.Text ?? "Desconocido";

        // pasar lista de deportes a la vista (para el select)
        ViewBag.Deportes = vm.Deportes;

        return View(vm.NuevoCancha);
    }

    // POST: /Canchas/Edit
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(CanchaDTO cancha)
    {
        if (ModelState.IsValid)
        {
            var json = JsonConvert.SerializeObject(cancha);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var resp = await _httpClient.PutAsync($"/api/canchas/{cancha.Cancha_ID}", content);
            if (resp.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            var body = await resp.Content.ReadAsStringAsync();
            ModelState.AddModelError("", $"Error al editar: {resp.StatusCode} {body}");
        }

        // reload lookups
        var vm = new CanchaIndexViewModel { NuevoCancha = cancha };
        await PopulateLookupsAsync(vm);
        ViewBag.Deportes = vm.Deportes;

        // set deporte nombre for display if needed
        cancha.DeporteNombre = vm.Deportes.FirstOrDefault(d => d.Id == cancha.Deporte_ID)?.Text ?? "Desconocido";

        return View(cancha);
    }

    // GET: /Canchas/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
        var resp = await _httpClient.GetAsync($"/api/canchas/{id}");
        if (!resp.IsSuccessStatusCode) return NotFound();

        var j = await resp.Content.ReadAsStringAsync();
        var cancha = JsonConvert.DeserializeObject<CanchaDTO>(j);
        if (cancha == null) return NotFound();

        var vm = new CanchaIndexViewModel();
        await PopulateLookupsAsync(vm);
        cancha.DeporteNombre = vm.Deportes.FirstOrDefault(d => d.Id == cancha.Deporte_ID)?.Text ?? "Desconocido";

        return View(cancha);
    }

    // POST: /Canchas/Delete
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(CanchaDTO cancha)
    {
        var resp = await _httpClient.DeleteAsync($"/api/canchas/{cancha.Cancha_ID}");
        if (resp.IsSuccessStatusCode) return RedirectToAction(nameof(Index));

        var body = await resp.Content.ReadAsStringAsync();
        ModelState.AddModelError("", $"Error al eliminar: {resp.StatusCode} {body}");
        return View(cancha);
    }

    // Helper para lookups
    private async Task PopulateLookupsAsync(CanchaIndexViewModel vm)
    {
        try
        {
            var resp = await _httpClient.GetAsync("/api/deportes");
            if (!resp.IsSuccessStatusCode)
            {
                var body = await resp.Content.ReadAsStringAsync();
                TempData["Error"] = $"Error cargando deportes: {resp.StatusCode} - {body}";
                vm.Deportes = new List<CanchaLookupItem> { new CanchaLookupItem { Id = 0, Text = "Sin deportes disponibles" } };
                return;
            }

            var j = await resp.Content.ReadAsStringAsync();

            // Parsear de forma robusta con Newtonsoft.Json.Linq (case-insensitive)
            var arr = Newtonsoft.Json.Linq.JArray.Parse(j);
            vm.Deportes.Clear();

            foreach (var item in arr)
            {
                var obj = item as Newtonsoft.Json.Linq.JObject;
                if (obj == null) continue;

                // intentar varias claves posibles
                int? id = obj.Value<int?>("Deporte_ID")
                           ?? obj.Value<int?>("deporte_ID")
                           ?? obj.Value<int?>("Id")
                           ?? obj.Value<int?>("id");

                string name = obj.Value<string>("Nombre")
                              ?? obj.Value<string>("nombre")
                              ?? obj.Value<string>("Name")
                              ?? obj.Value<string>("name");

                if (id == null)
                {
                    // si no hay id, ignorar o asignar 0
                    continue;
                }

                vm.Deportes.Add(new CanchaLookupItem { Id = id.Value, Text = string.IsNullOrWhiteSpace(name) ? $"Deporte {id.Value}" : name });
            }
        }
        catch (Exception ex)
        {
            TempData["Error"] = $"Exception al cargar deportes: {ex.Message}";
            vm.Deportes = new List<CanchaLookupItem> { new CanchaLookupItem { Id = 0, Text = "Sin deportes disponibles" } };
        }

        if (vm.Deportes == null || vm.Deportes.Count == 0)
            vm.Deportes = new List<CanchaLookupItem> { new CanchaLookupItem { Id = 0, Text = "Sin deportes disponibles" } };
    }

    // Método temporal de depuración: /Canchas/TestDeportes
    public async Task<IActionResult> TestDeportes()
    {
        try
        {
            var resp = await _httpClient.GetAsync("/api/deportes");
            var body = await resp.Content.ReadAsStringAsync();
            return Content($"Status: {(int)resp.StatusCode} {resp.ReasonPhrase}\n\nBody:\n{body}", "text/plain");
        }
        catch (Exception ex)
        {
            return Content($"Exception: {ex.Message}\n{ex.StackTrace}", "text/plain");
        }
    }
}