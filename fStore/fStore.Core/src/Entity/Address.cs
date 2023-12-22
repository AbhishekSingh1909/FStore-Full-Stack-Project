using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace fStore.Core;

public class Address : BaseEntity
{
    public required string HouseNumber { get; set; }
    public required string Street { get; set; }
    public required string PostCode { get; set; }
    public required string City { get; set; }
    public required string Country { get; set; }
    public required Guid UserId { get; set; }
}
