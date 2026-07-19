using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Crawler.Entities;

[Table("raw_links")]
public partial class RawLink
{
    public int Id { get; set; }

    [Required]
    public string Url { get; set; } = null!;
}
