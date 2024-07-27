using System;
using System.Collections.Generic;

namespace MVPSA_V2022.Modelos;

public partial class EmailQueue
{
    public int Id { get; set; }

    public string Recipients { get; set; } = null!;

    public string? CcRecipients { get; set; }

    public string EmailSubject { get; set; } = null!;

    public string? EmailBody { get; set; }

    public DateTime QueueTime { get; set; }

    public DateTime? SentTime { get; set; }
}
