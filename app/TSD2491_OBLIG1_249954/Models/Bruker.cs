using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TSD2491_OBLIG1_249954.Models;

public class Bruker 
{
    public int ID { get; set; }

    [StringLength(60, MinimumLength = 3)]
    [Required]
    public string? Navn { get; set; }
	
	public int AntallSpill { get; set; }

    public string? KontaktInfo { get; set; }
}