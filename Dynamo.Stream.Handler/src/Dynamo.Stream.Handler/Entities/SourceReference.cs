﻿using Domain.Entities.Participants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynamo.Stream.Handler.Entities
{
    public class SourceReference
    {
        public int Id { get; set; }
        [MaxLength(255)]
        public string Pk { get; set; } = null!;
        public int ParticipantId { get; set; }

        public Participant Participant { get; set; } = null!;
    }
}
