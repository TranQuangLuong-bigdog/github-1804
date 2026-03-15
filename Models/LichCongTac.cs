using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace KhoaNoiVuCNTT.Models
{
    [Table("LichCongTac")]
    public partial class LichCongTac
    {
        [Key]
        public int MaLich { get; set; }

        [StringLength(255)]
        public string TieuDe { get; set; } = null!;

        public string? MoTa { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime ThoiGianBatDau { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime ThoiGianKetThuc { get; set; }

        [StringLength(255)]
        public string DiaDiem { get; set; } = null!;

        public int? MaNguoiToChuc { get; set; }

        [ForeignKey("MaNguoiToChuc")]
        [InverseProperty("LichCongTacs")]
        public virtual CanBo? MaNguoiToChucNavigation { get; set; }
    }
}
