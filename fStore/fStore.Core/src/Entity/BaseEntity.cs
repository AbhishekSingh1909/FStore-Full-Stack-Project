using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fStore.Core;

public class BaseEntity : TimeStamp
{
    [Key, Column(Order = 0)]
    public Guid Id { get; set; }
}
