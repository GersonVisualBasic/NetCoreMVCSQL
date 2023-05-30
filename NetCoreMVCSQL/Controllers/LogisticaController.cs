#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NetCoreMVCSQL.Models;
using Newtonsoft.Json;
using NuGet.Protocol;
using RestSharp;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace NetCoreMVCSQL.Controllers
{
    public class LogisticaController : Controller
    {
        private readonly Contexto _context;

        public LogisticaController(Contexto context)
        {
            _context = context;
        }

        // GET: Logisticas
        public IActionResult Index()
        {
            return View();
        }

        // GET: Logisticas/Consultar

        public IActionResult Consultar()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Consultar([Bind("Semana,Ano,Mes")] DadosAPI obj = null)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var link = "https://fuct-smk186-code-challenge.cblx.com.br/minerais?";
                    var conteudo = "mes=" + obj.Mes + "&ano=" + obj.Ano + "&semana=" + obj.Semana;
                    var endpoint = link + conteudo;

                    HttpResponseMessage response = await client.GetAsync(endpoint);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        obj = JsonConvert.DeserializeObject<DadosAPI>(responseBody);
                    }
                    else
                    {
                        Console.WriteLine($"Erro na requisição: {response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro: {ex.Message}");
            }

            var model = new DadosAPI
            {
                A = obj?.A,
                B = obj?.B,
                C = obj?.C,
                D = obj?.D
            };

            return View(model);
        }

        // GET: Logisticas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Logistica = await _context.Logistica
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Logistica == null)
            {
                return NotFound();
            }

            return View(Logistica);
        }

        // GET: Logisticas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Logisticas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome")] Logistica Logistica)
        {
            if (ModelState.IsValid)
            {
                _context.Add(Logistica);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(Logistica);
        }

        // GET: Logisticas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Logistica = await _context.Logistica.FindAsync(id);
            if (Logistica == null)
            {
                return NotFound();
            }
            return View(Logistica);
        }

        // POST: Logisticas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome")] Logistica Logistica)
        {
            if (id != Logistica.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Logistica);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LogisticaExists(Logistica.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(Logistica);
        }

        // GET: Logisticas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Logistica = await _context.Logistica
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Logistica == null)
            {
                return NotFound();
            }

            return View(Logistica);
        }

        // POST: Logisticas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Logistica = await _context.Logistica.FindAsync(id);
            _context.Logistica.Remove(Logistica);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LogisticaExists(int id)
        {
            return _context.Logistica.Any(e => e.Id == id);
        }
    }
}
