using KhoaNoiVuCNTT.Data;
using KhoaNoiVuCNTT.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace KhoaNoiVuCNTT.Controllers
{
    public class CanBoController : Controller
    {
        private readonly ThongTinNoiBoContext _context;

        public CanBoController(ThongTinNoiBoContext context)
        {
            _context = context;
        }

        // =============================
        // DANH SÁCH + TÌM KIẾM
        // =============================
        public async Task<IActionResult> Index(string searchString)
        {
            var query = _context.CanBo
                .Include(c => c.MaBoMonNavigation)
                .Include(c => c.MaVaiTroNavigation)
                .AsNoTracking();

            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();

                query = query.Where(c =>
                    c.HoTen.ToLower().Contains(searchString) ||
                    c.Email.ToLower().Contains(searchString));
            }

            ViewData["CurrentFilter"] = searchString;

            PopulateDropDownLists();

            return View(await query.ToListAsync());
        }

        // =============================
        // CREATE
        // =============================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CanBo canBo)
        {
            try
            {
                if (string.IsNullOrEmpty(canBo.MatKhau))
                    canBo.MatKhau = "123456";

                if (ModelState.IsValid)
                {
                    _context.Add(canBo);
                    await _context.SaveChangesAsync();

                    TempData["Success"] = "Thêm cán bộ thành công!";
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }

        // =============================
        // EDIT
        // =============================
        public async Task<IActionResult> Edit(int id)
        {
            var canBo = await _context.CanBo.FindAsync(id);

            if (canBo == null)
                return NotFound();

            PopulateDropDownLists(canBo.MaBoMon, canBo.MaVaiTro);

            return View(canBo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CanBo canBo)
        {
            if (id != canBo.MaCanBo)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(canBo);
                    await _context.SaveChangesAsync();

                    TempData["Success"] = "Cập nhật thành công!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CanBoExists(canBo.MaCanBo))
                        return NotFound();
                    else
                        throw;
                }
            }

            return RedirectToAction(nameof(Index));
        }

        // =============================
        // DETAILS
        // =============================
        public async Task<IActionResult> Details(int id)
        {
            var canBo = await _context.CanBo
                .Include(c => c.MaBoMonNavigation)
                .Include(c => c.MaVaiTroNavigation)
                .FirstOrDefaultAsync(m => m.MaCanBo == id);

            if (canBo == null)
                return NotFound();

            return View(canBo);
        }

        // =============================
        // DELETE
        // =============================
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var canBo = await _context.CanBo.FindAsync(id);

            if (canBo != null)
            {
                _context.CanBo.Remove(canBo);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Xóa cán bộ thành công!";
            }

            return RedirectToAction(nameof(Index));
        }

        // DROPDOWN

        private void PopulateDropDownLists(object selectedBoMon = null, object selectedVaiTro = null)
        {
            ViewData["MaBoMon"] = new SelectList(_context.BoMon, "MaBoMon", "TenBoMon", selectedBoMon);

            ViewData["MaVaiTro"] = new SelectList(_context.VaiTro, "MaVaiTro", "TenVaiTro", selectedVaiTro);
        }

        private bool CanBoExists(int id)
        {
            return _context.CanBo.Any(e => e.MaCanBo == id);
        }
    }
}