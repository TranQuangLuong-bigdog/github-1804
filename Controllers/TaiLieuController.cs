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
    public class TaiLieuController : Controller
    {
        private readonly ThongTinNoiBoContext _context;

        public TaiLieuController(ThongTinNoiBoContext context)
        {
            _context = context;
        }

        // GET: TaiLieux
        public async Task<IActionResult> Index()
        {
            var thongTinNoiBoContext = _context.TaiLieu.Include(t => t.MaNguoiTaiLenNavigation);
            return View(await thongTinNoiBoContext.ToListAsync());
        }

        // GET: TaiLieux/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taiLieu = await _context.TaiLieu
                .Include(t => t.MaNguoiTaiLenNavigation)
                .FirstOrDefaultAsync(m => m.MaTaiLieu == id);
            if (taiLieu == null)
            {
                return NotFound();
            }

            return View(taiLieu);
        }

        // GET: TaiLieux/Create
        public IActionResult Create()
        {
            ViewData["MaNguoiTaiLen"] = new SelectList(_context.CanBo, "MaCanBo", "MaCanBo");
            return View();
        }

        // POST: TaiLieux/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaTaiLieu,TenTaiLieu,DuongDanFile,MaNguoiTaiLen,NgayTaiLen,PhanLoai")] TaiLieu taiLieu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(taiLieu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaNguoiTaiLen"] = new SelectList(_context.CanBo, "MaCanBo", "MaCanBo", taiLieu.MaNguoiTaiLen);
            return View(taiLieu);
        }

        // GET: TaiLieux/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taiLieu = await _context.TaiLieu.FindAsync(id);
            if (taiLieu == null)
            {
                return NotFound();
            }
            ViewData["MaNguoiTaiLen"] = new SelectList(_context.CanBo, "MaCanBo", "MaCanBo", taiLieu.MaNguoiTaiLen);
            return View(taiLieu);
        }

        // POST: TaiLieux/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaTaiLieu,TenTaiLieu,DuongDanFile,MaNguoiTaiLen,NgayTaiLen,PhanLoai")] TaiLieu taiLieu)
        {
            if (id != taiLieu.MaTaiLieu)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(taiLieu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaiLieuExists(taiLieu.MaTaiLieu))
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
            ViewData["MaNguoiTaiLen"] = new SelectList(_context.CanBo, "MaCanBo", "MaCanBo", taiLieu.MaNguoiTaiLen);
            return View(taiLieu);
        }

        // GET: TaiLieux/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taiLieu = await _context.TaiLieu
                .Include(t => t.MaNguoiTaiLenNavigation)
                .FirstOrDefaultAsync(m => m.MaTaiLieu == id);
            if (taiLieu == null)
            {
                return NotFound();
            }

            return View(taiLieu);
        }

        // POST: TaiLieux/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var taiLieu = await _context.TaiLieu.FindAsync(id);
            if (taiLieu != null)
            {
                _context.TaiLieu.Remove(taiLieu);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaiLieuExists(int id)
        {
            return _context.TaiLieu.Any(e => e.MaTaiLieu == id);
        }
    }
}
