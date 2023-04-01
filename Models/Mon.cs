namespace Sukiya.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Mon")]
    public partial class Mon
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Mon()
        {
            CT_DonHang = new HashSet<CT_DonHang>();
        }

        [Key]
        public int MaMon { get; set; }

        [StringLength(50)]
        public string TenMon { get; set; }

        public int? MaLoaiMon { get; set; }

        public decimal? Gia { get; set; }

        [StringLength(50)]
        public string MoTa { get; set; }

        [StringLength(100)]
        public string HinhAnh { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CT_DonHang> CT_DonHang { get; set; }

        public virtual LoaiMon LoaiMon { get; set; }
    }
}
