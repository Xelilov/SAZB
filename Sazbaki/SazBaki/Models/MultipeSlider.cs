//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SazBaki.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class MultipeSlider
    {
        public int Id { get; set; }
        public string mp_logo_img { get; set; }
        public string mp_img { get; set; }
        public string mp_text { get; set; }
        public int mp_lang_id { get; set; }
    
        public virtual Language Language { get; set; }
    }
}
