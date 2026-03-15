using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace KhoaNoiVuCNTT.Models
{
    public partial class VaiTro
    {
        [Key]
        public int MaVaiTro { get; set; }

        [StringLength(50)]
        public string TenVaiTro { get; set; } = null!;

        [InverseProperty("MaVaiTroNavigation")]
        public virtual ICollection<CanBo> CanBos { get; set; } = new List<CanBo>();
    }
}
