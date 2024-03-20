using System.ComponentModel.DataAnnotations.Schema;

namespace BulletinBoard.Core.Models;

public class Advertisement
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public string Message { get; set; } = null!;
}
