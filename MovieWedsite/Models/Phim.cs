//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MovieWedsite.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Phim
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Phim()
        {
            this.TapPhims = new HashSet<TapPhim>();
            this.phim_theloaiphim = new HashSet<phim_theloaiphim>();
        }
    
        public string MaPhim { get; set; }
        public string TenPhim { get; set; }
        public string Anhbia { get; set; }
        public string Mota { get; set; }
        public string TacGia { get; set; }
        public Nullable<System.DateTime> NamSanXuat { get; set; }
        public int LuotXem { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TapPhim> TapPhims { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<phim_theloaiphim> phim_theloaiphim { get; set; }
    }
}
