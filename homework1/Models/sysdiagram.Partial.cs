namespace homework1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    [MetadataType(typeof(sysdiagramMetaData))]
    public partial class sysdiagram
    {
    }
    
    public partial class sysdiagramMetaData
    {
        
        [StringLength(128, ErrorMessage="欄位長度不得大於 128 個字元")]
        [Required]
        public string name { get; set; }
        [Required]
        public int principal_id { get; set; }
        [Required]
        public int diagram_id { get; set; }
        public Nullable<int> version { get; set; }
        public byte[] definition { get; set; }
    }
}
