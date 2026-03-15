using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace KhoaNoiVuCNTT.Models
{
    [Table("TaiLieu")]
    public partial class TaiLieu
    {
        [Key]
        public int MaTaiLieu { get; set; }

        [StringLength(255)]
        public string TenTaiLieu { get; set; } = null!;

        [StringLength(255)]
        [Unicode(false)]
        public string DuongDanFile { get; set; } = null!;

        public int? MaNguoiTaiLen { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? NgayTaiLen { get; set; }

        [StringLength(100)]
        public string PhanLoai { get; set; } = null!;

        [ForeignKey("MaNguoiTaiLen")]
        [InverseProperty("TaiLieus")]
        public virtual CanBo? MaNguoiTaiLenNavigation { get; set; }
    }
}
