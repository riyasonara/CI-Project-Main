using System;
using System.Collections.Generic;

namespace CI_PLatform_Entities.Models;

public partial class ContactU
{
    public int ContactUsId { get; set; }

    public string? Subject { get; set; }

    public string? Message { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Email { get; set; }

    public string? Username { get; set; }
}
