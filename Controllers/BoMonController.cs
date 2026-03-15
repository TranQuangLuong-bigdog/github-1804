using KhoaNoiVuCNTT.Data;
using KhoaNoiVuCNTT.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KhoaNoiVuCNTT.Controllers
{
    public class BoMonController : Controller
    {
        private readonly ThongTinNoiBoContext _context;

        public BoMonController(ThongTinNoiBoContext context)
        {
            _context = context;
        }

        // HIỂN THỊ
        public async Task<IActionResult> Index()
        {
            return View(await _context.BoMon.ToListAsync());
        }

        // THÊM
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string TenBoMon)
        {
            if (!string.IsNullOrEmpty(TenBoMon))
            {
                BoMon bm = new BoMon();
                bm.TenBoMon = TenBoMon;

                _context.BoMon.Add(bm);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        // SỬA
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int MaBoMon, string TenBoMon)
        {
            var bm = await _context.BoMon.FindAsync(MaBoMon);

            if (bm != null)
            {
                bm.TenBoMon = TenBoMon;
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        // XÓA
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hasStaff = _context.CanBo.Any(c => c.MaBoMon == id);

            if (hasStaff)
            {
                TempData["Error"] = "Không thể xóa bộ môn vì còn cán bộ thuộc bộ môn này.";
                return RedirectToAction(nameof(Index));
            }

            var boMon = await _context.BoMon.FindAsync(id);

            if (boMon != null)
            {
                _context.BoMon.Remove(boMon);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

    }
}