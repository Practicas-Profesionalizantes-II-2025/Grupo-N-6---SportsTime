using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MVCSPortTime1.Models;
using MVCSPortTime1.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MVCSPortTime1.Controllers
{
    public class TurnosController : Controller
    {
        private readonly HttpClient _httpClient;

        public TurnosController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient();

            var baseUrl = configuration["ApiSettings:BaseUrl"];
            if (string.IsNullOrEmpty(baseUrl))
                throw new Exception("No se encontró la configuración de la URL base de la API (ApiSettings:BaseUrl).");

            _httpClient.BaseAddress = new Uri(baseUrl);
        }

        // GET: /Turnos?search=...
        public async Task<IActionResult> Index(string search = null)
        {
            var vm = new TurnoIndexViewModel();

            // 1) Obtener todos los turnos desde la API
            try
            {
                var resp = await _httpClient.GetAsync("/api/turnos");
                if (resp.IsSuccessStatusCode)
                {
                    var body = await resp.Content.ReadAsStringAsync();
                    vm.Turnos = JsonConvert.DeserializeObject<List<TurnoDTO>>(body) ?? new List<TurnoDTO>();
                }
                else
                {
                    vm.Turnos = new List<TurnoDTO>();
                    TempData["Error"] = $"Error cargando turnos: {resp.StatusCode}";
                }
            }
            catch (Exception ex)
            {
                vm.Turnos = new List<TurnoDTO>();
                TempData["Error"] = $"Exception cargando turnos: {ex.Message}";
            }

            // 2) Poblar lookups (deportes, canchas, usuarios)
            await PopulateLookupsAsync(vm);

            // 3) Mapear nombres: CanchaNombre y UsuarioNombre
            foreach (var t in vm.Turnos)
            {
                t.CanchaNombre = vm.Canchas.FirstOrDefault(c => c.Id == t.Cancha_ID)?.Text ?? $"Cancha {t.Cancha_ID}";
                t.UsuarioNombre = vm.Usuarios.FirstOrDefault(u => u.Id == t.Usuario_ID)?.Text ?? $"Usuario {t.Usuario_ID}";
            }

            // 4) Aplicar búsqueda (por id turno, id cancha, nombre cancha, o nombre usuario)
            if (!string.IsNullOrWhiteSpace(search))
            {
                var s = search.Trim();
                bool isNumber = int.TryParse(s, out int n);
                vm.Turnos = vm.Turnos.Where(t =>
                    (isNumber && (t.Turno_ID == n || t.Cancha_ID == n || t.Usuario_ID == n)) ||
                    (!isNumber && (
                        (!string.IsNullOrEmpty(t.CanchaNombre) && t.CanchaNombre.IndexOf(s, StringComparison.OrdinalIgnoreCase) >= 0) ||
                        (!string.IsNullOrEmpty(t.UsuarioNombre) && t.UsuarioNombre.IndexOf(s, StringComparison.OrdinalIgnoreCase) >= 0) ||
                        (!string.IsNullOrEmpty(t.Estado) && t.Estado.IndexOf(s, StringComparison.OrdinalIgnoreCase) >= 0)
                    ))
                ).ToList();

                if (vm.Turnos.Count == 0) TempData["Info"] = "No se encontraron turnos para la búsqueda indicada.";
            }

            // inicializar nuevo turno para el formulario
            vm.NuevoTurno = new TurnoDTO
            {
                HoraInicio = DateTime.Now,
                HoraFin = DateTime.Now.AddHours(1)
            };

            vm.Search = search ?? string.Empty;

            return View(vm);
        }

        // GET: /Turnos/Create
        public async Task<IActionResult> Create()
        {
            var vm = new TurnoIndexViewModel();
            await PopulateLookupsAsync(vm);
            vm.NuevoTurno = new TurnoDTO { HoraInicio = DateTime.Now, HoraFin = DateTime.Now.AddHours(1) };
            return View(vm);
        }

        // POST: /Turnos/Create (formulario)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TurnoIndexViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateLookupsAsync(model);
                return View(model);
            }

            // mapear y enviar a la API
            var dto = model.NuevoTurno;
            var json = JsonConvert.SerializeObject(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var resp = await _httpClient.PostAsync("/api/turnos", content);
            if (resp.IsSuccessStatusCode) return RedirectToAction(nameof(Index));

            var body = await resp.Content.ReadAsStringAsync();
            ModelState.AddModelError("", $"Error creando turno: {resp.StatusCode} {body}");
            await PopulateLookupsAsync(model);
            return View(model);
        }

        // GET: /Turnos/Details/5
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var resp = await _httpClient.GetAsync($"/api/turnos/{id}");
                if (!resp.IsSuccessStatusCode) return NotFound();

                var j = await resp.Content.ReadAsStringAsync();
                var turno = JsonConvert.DeserializeObject<TurnoDTO>(j);
                if (turno == null) return NotFound();

                var vm = new TurnoIndexViewModel();
                await PopulateLookupsAsync(vm);
                turno.CanchaNombre = vm.Canchas.FirstOrDefault(c => c.Id == turno.Cancha_ID)?.Text ?? $"Cancha {turno.Cancha_ID}";
                turno.UsuarioNombre = vm.Usuarios.FirstOrDefault(u => u.Id == turno.Usuario_ID)?.Text ?? $"Usuario {turno.Usuario_ID}";

                return View(turno);
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Exception obteniendo detalle de turno: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: /Turnos/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var resp = await _httpClient.GetAsync($"/api/turnos/{id}");
                if (!resp.IsSuccessStatusCode) return NotFound();

                var j = await resp.Content.ReadAsStringAsync();
                var turno = JsonConvert.DeserializeObject<TurnoDTO>(j);
                if (turno == null) return NotFound();

                var vm = new TurnoIndexViewModel { NuevoTurno = turno, Turnos = new List<TurnoDTO>() };
                await PopulateLookupsAsync(vm);

                // pasar lookups a la vista (sin usar ViewBag en lo posible; la vista Edit puede aceptar TurnoIndexViewModel o TurnoDTO + ViewBag)
                ViewBag.Canchas = vm.Canchas;
                ViewBag.Usuarios = vm.Usuarios;
                ViewBag.Estados = vm.Estados;

                turno.CanchaNombre = vm.Canchas.FirstOrDefault(c => c.Id == turno.Cancha_ID)?.Text ?? $"Cancha {turno.Cancha_ID}";
                turno.UsuarioNombre = vm.Usuarios.FirstOrDefault(u => u.Id == turno.Usuario_ID)?.Text ?? $"Usuario {turno.Usuario_ID}";

                return View(turno);
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Exception cargando turno para editar: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: /Turnos/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TurnoDTO turno)
        {
            if (!ModelState.IsValid)
            {
                // recargar lookups y volver a la vista
                var vm = new TurnoIndexViewModel { NuevoTurno = turno };
                await PopulateLookupsAsync(vm);
                ViewBag.Canchas = vm.Canchas;
                ViewBag.Usuarios = vm.Usuarios;
                ViewBag.Estados = vm.Estados;
                return View(turno);
            }

            try
            {
                var json = JsonConvert.SerializeObject(turno);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var resp = await _httpClient.PutAsync($"/api/turnos/{turno.Turno_ID}", content);
                if (resp.IsSuccessStatusCode) return RedirectToAction(nameof(Index));

                var body = await resp.Content.ReadAsStringAsync();
                ModelState.AddModelError("", $"Error actualizando turno: {resp.StatusCode} {body}");

                var vm = new TurnoIndexViewModel { NuevoTurno = turno };
                await PopulateLookupsAsync(vm);
                ViewBag.Canchas = vm.Canchas;
                ViewBag.Usuarios = vm.Usuarios;
                ViewBag.Estados = vm.Estados;
                return View(turno);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Exception actualizando turno: {ex.Message}");
                var vm = new TurnoIndexViewModel { NuevoTurno = turno };
                await PopulateLookupsAsync(vm);
                ViewBag.Canchas = vm.Canchas;
                ViewBag.Usuarios = vm.Usuarios;
                ViewBag.Estados = vm.Estados;
                return View(turno);
            }
        }

        // GET: /Turnos/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var resp = await _httpClient.GetAsync($"/api/turnos/{id}");
                if (!resp.IsSuccessStatusCode) return NotFound();

                var j = await resp.Content.ReadAsStringAsync();
                var turno = JsonConvert.DeserializeObject<TurnoDTO>(j);
                if (turno == null) return NotFound();

                var vm = new TurnoIndexViewModel();
                await PopulateLookupsAsync(vm);
                turno.CanchaNombre = vm.Canchas.FirstOrDefault(c => c.Id == turno.Cancha_ID)?.Text ?? $"Cancha {turno.Cancha_ID}";
                turno.UsuarioNombre = vm.Usuarios.FirstOrDefault(u => u.Id == turno.Usuario_ID)?.Text ?? $"Usuario {turno.Usuario_ID}";

                return View(turno);
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Exception cargando turno para eliminar: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: /Turnos/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(TurnoDTO turno)
        {
            try
            {
                var resp = await _httpClient.DeleteAsync($"/api/turnos/{turno.Turno_ID}");
                if (resp.IsSuccessStatusCode) return RedirectToAction(nameof(Index));

                var body = await resp.Content.ReadAsStringAsync();
                ModelState.AddModelError("", $"Error eliminando turno: {resp.StatusCode} {body}");
                return View(turno);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Exception eliminando turno: {ex.Message}");
                return View(turno);
            }
        }

        // Helper para poblar deportes, canchas y usuarios (robusto frente a casing de JSON)
        private async Task PopulateLookupsAsync(TurnoIndexViewModel vm)
        {
            // Inicializo colecciones
            vm.Deportes = vm.Deportes ?? new List<TurnoLookupItem>();
            vm.Canchas = vm.Canchas ?? new List<TurnoLookupItem>();
            vm.Usuarios = vm.Usuarios ?? new List<TurnoLookupItem>();
            vm.Estados = vm.Estados ?? new List<TurnoLookupItem>();

            // 1) Deportes
            try
            {
                var resp = await _httpClient.GetAsync("/api/deportes");
                if (resp.IsSuccessStatusCode)
                {
                    var j = await resp.Content.ReadAsStringAsync();
                    var arr = JArray.Parse(j);
                    vm.Deportes.Clear();
                    foreach (var item in arr)
                    {
                        var obj = item as JObject;
                        if (obj == null) continue;
                        int? id = obj.Value<int?>("Deporte_ID") ?? obj.Value<int?>("deporte_ID") ?? obj.Value<int?>("Id") ?? obj.Value<int?>("id");
                        string name = obj.Value<string>("Nombre") ?? obj.Value<string>("nombre") ?? obj.Value<string>("Name") ?? obj.Value<string>("name");
                        if (id != null) vm.Deportes.Add(new TurnoLookupItem { Id = id.Value, Text = string.IsNullOrWhiteSpace(name) ? $"Deporte {id.Value}" : name });
                    }
                }
                else
                {
                    vm.Deportes.Add(new TurnoLookupItem { Id = 0, Text = "Sin deportes disponibles" });
                }
            }
            catch
            {
                vm.Deportes.Add(new TurnoLookupItem { Id = 0, Text = "Sin deportes disponibles" });
            }

            // 2) Canchas (necesitamos primero deportes para mostrar nombre asociado)
            try
            {
                var resp = await _httpClient.GetAsync("/api/canchas");
                if (resp.IsSuccessStatusCode)
                {
                    var j = await resp.Content.ReadAsStringAsync();
                    var arr = JArray.Parse(j);
                    vm.Canchas.Clear();
                    foreach (var item in arr)
                    {
                        var obj = item as JObject;
                        if (obj == null) continue;
                        int? id = obj.Value<int?>("Cancha_ID") ?? obj.Value<int?>("cancha_ID") ?? obj.Value<int?>("Id") ?? obj.Value<int?>("id");
                        int? deporteId = obj.Value<int?>("Deporte_ID") ?? obj.Value<int?>("deporte_ID") ?? obj.Value<int?>("DeporteId") ?? obj.Value<int?>("deporteId");
                        string text = id != null ? $"Cancha {id}" : "Cancha";
                        if (deporteId != null)
                        {
                            var deporteName = vm.Deportes.FirstOrDefault(d => d.Id == deporteId.Value)?.Text;
                            if (!string.IsNullOrWhiteSpace(deporteName)) text = $"{deporteName} (Cancha {id})";
                        }
                        if (id != null)
                        {
                            vm.Canchas.Add(new TurnoLookupItem
                            {
                                Id = id.Value,
                                Text = text,
                                ParentId = deporteId // <-- acá asignás la relación cancha -> deporte
                            });
                        }
                    }
                }
                else
                {
                    vm.Canchas.Add(new TurnoLookupItem { Id = 0, Text = "Sin canchas" });
                }
            }
            catch
            {
                vm.Canchas.Add(new TurnoLookupItem { Id = 0, Text = "Sin canchas" });
            }

            // 3) Usuarios
            try
            {
                var resp = await _httpClient.GetAsync("/api/usuarios");
                if (resp.IsSuccessStatusCode)
                {
                    var j = await resp.Content.ReadAsStringAsync();
                    var arr = JArray.Parse(j);
                    vm.Usuarios.Clear();
                    foreach (var item in arr)
                    {
                        var obj = item as JObject;
                        if (obj == null) continue;
                        int? id = obj.Value<int?>("Usuario_ID") ?? obj.Value<int?>("usuario_ID") ?? obj.Value<int?>("Id") ?? obj.Value<int?>("id");
                        string name = obj.Value<string>("Nombre") ?? obj.Value<string>("nombre") ?? obj.Value<string>("Email") ?? obj.Value<string>("email") ?? obj.Value<string>("UserName") ?? obj.Value<string>("userName");
                        if (id != null) vm.Usuarios.Add(new TurnoLookupItem { Id = id.Value, Text = string.IsNullOrWhiteSpace(name) ? $"Usuario {id.Value}" : name });
                    }
                }
                else
                {
                    vm.Usuarios.Add(new TurnoLookupItem { Id = 0, Text = "Sin usuarios disponibles" });
                }
            }
            catch
            {
                vm.Usuarios.Add(new TurnoLookupItem { Id = 0, Text = "Sin usuarios disponibles" });
            }

            // 4) Estados (puedes ajustarlos según tu dominio)
            vm.Estados = new List<TurnoLookupItem>
            {
                new TurnoLookupItem { Id = 0, Text = "Pendiente" },
                new TurnoLookupItem { Id = 1, Text = "Confirmado" },
                new TurnoLookupItem { Id = 2, Text = "Cancelado" }
            };

            // Validación final: asegurarse que hay al menos un placeholder
            if (vm.Deportes.Count == 0) vm.Deportes.Add(new TurnoLookupItem { Id = 0, Text = "Sin deportes" });
            if (vm.Canchas.Count == 0) vm.Canchas.Add(new TurnoLookupItem { Id = 0, Text = "Sin canchas" });
            if (vm.Usuarios.Count == 0) vm.Usuarios.Add(new TurnoLookupItem { Id = 0, Text = "Sin usuarios" });
        }
    }
}