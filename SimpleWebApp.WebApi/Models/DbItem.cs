using System.ComponentModel.DataAnnotations;

namespace SimpleWebApp.WebApi.Models;

public abstract class DbItem : IDbItem
{
    [Key]
    public long Id { get; set; }
    
    [Required]
    public DateTime Created { get; set; }
    
    [Required] 
    public DateTime Modified { get; set; }
}
