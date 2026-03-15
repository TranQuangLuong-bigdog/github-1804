using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace KhoaNoiVuCNTT.Models
{
    [Table("BoMon")]
    public partial class BoMon
    {
        [Key]
        public int MaBoMon { get; set; }

        [StringLength(100)]
        public string TenBoMon { get; set; } = null!;

        [InverseProperty("MaBoMonNavigation")]
        public virtual ICollection<CanBo> CanBos { get; set; } = new List<CanBo>();
    }
}
