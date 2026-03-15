using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace KhoaNoiVuCNTT.Models
{
    [Table("ThongBao")]
    public partial class ThongBao
    {
        [Key]
        public int MaThongBao { get; set; }

        [StringLength(255)]
        public string TieuDe { get; set; } = null!;

        public string NoiDung { get; set; } = null!;

        [Column(TypeName = "datetime")]
        public DateTime? NgayDang { get; set; }

        public int? MaNguoiDang { get; set; }

        [StringLength(255)]
        [Unicode(false)]
        public string? FileDinhKem { get; set; }

        [ForeignKey("MaNguoiDang")]
        [InverseProperty("ThongBaos")]
        public virtual CanBo? MaNguoiDangNavigation { get; set; }
    }
}
