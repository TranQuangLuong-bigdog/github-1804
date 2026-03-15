using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace KhoaNoiVuCNTT.Models
{
    [Table("CanBo")]
    [Index("Email", Name = "UQ__CanBo__A9D1053497EA75FD", IsUnique = true)]
    public partial class CanBo
    {

        [Key]
        public int MaCanBo { get; set; }

        [StringLength(100)]
        public string HoTen { get; set; } = null!;

        [StringLength(100)]
        [Unicode(false)]
        public string Email { get; set; } = null!;

        [StringLength(255)]
        [Unicode(false)]
        public string MatKhau { get; set; } = null!;

        public int? MaBoMon { get; set; }

        public int? MaVaiTro { get; set; }

        [InverseProperty("MaNguoiToChucNavigation")]
        public virtual ICollection<LichCongTac> LichCongTacs { get; set; } = new List<LichCongTac>();

        [ForeignKey("MaBoMon")]
        [InverseProperty("CanBos")]
        public virtual BoMon? MaBoMonNavigation { get; set; }

        [ForeignKey("MaVaiTro")]
        [InverseProperty("CanBos")]
        public virtual VaiTro? MaVaiTroNavigation { get; set; }

        [InverseProperty("MaNguoiTaiLenNavigation")]
        public virtual ICollection<TaiLieu> TaiLieus { get; set; } = new List<TaiLieu>();

        [InverseProperty("MaNguoiDangNavigation")]
        public virtual ICollection<ThongBao> ThongBaos { get; set; } = new List<ThongBao>();
    }
}
