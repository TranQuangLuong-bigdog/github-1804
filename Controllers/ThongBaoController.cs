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
    public class ThongBaoController : Controller
    {
        private readonly ThongTinNoiBoContext _context;

        public ThongBaoController(ThongTinNoiBoContext context)
        {
            _context = context;
        }

        // GET: ThongBaos
        public async Task<IActionResult> Index()
        {
            var thongTinNoiBoContext = _context.ThongBao.Include(t => t.MaNguoiDangNavigation);
            return View(await thongTinNoiBoContext.ToListAsync());
        }

        // GET: ThongBaos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thongBao = await _context.ThongBao
                .Include(t => t.MaNguoiDangNavigation)
                .FirstOrDefaultAsync(m => m.MaThongBao == id);
            if (thongBao == null)
            {
                return NotFound();
            }

            return View(thongBao);
        }

        // GET: ThongBaos/Create
        public IActionResult Create()
        {
            ViewData["MaNguoiDang"] = new SelectList(_context.CanBo, "MaCanBo", "MaCanBo");
            return View();
        }

        // POST: ThongBaos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaThongBao,TieuDe,NoiDung,NgayDang,MaNguoiDang,FileDinhKem")] ThongBao thongBao)
        {
            if (ModelState.IsValid)
            {
                _context.Add(thongBao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaNguoiDang"] = new SelectList(_context.CanBo, "MaCanBo", "MaCanBo", thongBao.MaNguoiDang);
            return View(thongBao);
        }

        // GET: ThongBaos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thongBao = await _context.ThongBao.FindAsync(id);
            if (thongBao == null)
            {
                return NotFound();
            }
            ViewData["MaNguoiDang"] = new SelectList(_context.CanBo, "MaCanBo", "MaCanBo", thongBao.MaNguoiDang);
            return View(thongBao);
        }

        // POST: ThongBaos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaThongBao,TieuDe,NoiDung,NgayDang,MaNguoiDang,FileDinhKem")] ThongBao thongBao)
        {
            if (id != thongBao.MaThongBao)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(thongBao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ThongBaoExists(thongBao.MaThongBao))
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
            ViewData["MaNguoiDang"] = new SelectList(_context.CanBo, "MaCanBo", "MaCanBo", thongBao.MaNguoiDang);
            return View(thongBao);
        }

        // GET: ThongBaos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thongBao = await _context.ThongBao
                .Include(t => t.MaNguoiDangNavigation)
                .FirstOrDefaultAsync(m => m.MaThongBao == id);
            if (thongBao == null)
            {
                return NotFound();
            }

            return View(thongBao);
        }

        // POST: ThongBaos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var thongBao = await _context.ThongBao.FindAsync(id);
            if (thongBao != null)
            {
                _context.ThongBao.Remove(thongBao);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ThongBaoExists(int id)
        {
            return _context.ThongBao.Any(e => e.MaThongBao == id);
        }
    }
}
