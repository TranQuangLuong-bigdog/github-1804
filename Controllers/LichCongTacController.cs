using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KhoaNoiVuCNTT.Data;
using KhoaNoiVuCNTT.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace KhoaNoiVuCNTT.Controllers
{
    public class LichCongTacController : Controller
    {
        private readonly ThongTinNoiBoContext _context;

        public LichCongTacController(ThongTinNoiBoContext context)
        {
            _context = context;
        }

        // GET: LichCongTacs
        public async Task<IActionResult> Index()
        {
            var thongTinNoiBoContext = _context.LichCongTac.Include(l => l.MaNguoiToChucNavigation);
            return View(await thongTinNoiBoContext.ToListAsync());
        }

        // GET: LichCongTacs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lichCongTac = await _context.LichCongTac
                .Include(l => l.MaNguoiToChucNavigation)
                .FirstOrDefaultAsync(m => m.MaLich == id);
            if (lichCongTac == null)
            {
                return NotFound();
            }

            return View(lichCongTac);
        }

        // GET: LichCongTacs/Create
        public IActionResult Create()
        {
            ViewData["MaNguoiToChuc"] = new SelectList(_context.CanBo, "MaCanBo", "MaCanBo");
            return View();
        }

        // POST: LichCongTacs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaLich,TieuDe,MoTa,ThoiGianBatDau,ThoiGianKetThuc,DiaDiem,MaNguoiToChuc")] LichCongTac lichCongTac)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lichCongTac);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaNguoiToChuc"] = new SelectList(_context.CanBo, "MaCanBo", "MaCanBo", lichCongTac.MaNguoiToChuc);
            return View(lichCongTac);
        }

        // GET: LichCongTacs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lichCongTac = await _context.LichCongTac.FindAsync(id);
            if (lichCongTac == null)
            {
                return NotFound();
            }
            ViewData["MaNguoiToChuc"] = new SelectList(_context.CanBo, "MaCanBo", "MaCanBo", lichCongTac.MaNguoiToChuc);
            return View(lichCongTac);
        }

        // POST: LichCongTacs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaLich,TieuDe,MoTa,ThoiGianBatDau,ThoiGianKetThuc,DiaDiem,MaNguoiToChuc")] LichCongTac lichCongTac)
        {
            if (id != lichCongTac.MaLich)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lichCongTac);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LichCongTacExists(lichCongTac.MaLich))
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
            ViewData["MaNguoiToChuc"] = new SelectList(_context.CanBo, "MaCanBo", "MaCanBo", lichCongTac.MaNguoiToChuc);
            return View(lichCongTac);
        }

        // GET: LichCongTacs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lichCongTac = await _context.LichCongTac
                .Include(l => l.MaNguoiToChucNavigation)
                .FirstOrDefaultAsync(m => m.MaLich == id);
            if (lichCongTac == null)
            {
                return NotFound();
            }

            return View(lichCongTac);
        }

        // POST: LichCongTacs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lichCongTac = await _context.LichCongTac.FindAsync(id);
            if (lichCongTac != null)
            {
                _context.LichCongTac.Remove(lichCongTac);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LichCongTacExists(int id)
        {
            return _context.LichCongTac.Any(e => e.MaLich == id);
        }
    }
}
