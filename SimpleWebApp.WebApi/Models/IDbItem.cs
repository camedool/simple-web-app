using System.ComponentModel.DataAnnotations;

namespace SimpleWebApp.WebApi.Models;

public interface IDbItem
{
    /// <summary>
    /// Primary key of the table.
    /// For brevity taken as long for all cases.
    /// </summary>
    [Key]
    public long Id { get; set; }
    
    /// <summary>
    /// Skipped for brevity.
    /// </summary>
    //[Timestamp]
    //public byte[] RowVersion { get; set; }
    
    [Required]
    public DateTime Created { get; set; }

    [Required]
    public DateTime Modified { get; set; }
}
