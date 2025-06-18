using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BPOR.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BPOR.Domain.Extensions
{
    public static class ParticipantExtensions
    {
        public static IQueryable<Participant> GetRandomSampleOfParticipants(
            this ParticipantDbContext dbContext, int seed, double pageMinInclusive, double pageMaxExclusive)
        {
            // The no_merge hint is to force the execution engine to evaulate both the random
            // ordering and page before evaulating any further filters. If this doesn't
            // happen then a full table scan occurs which can timeout.
            return dbContext.Database
            .SqlQuery<ParticipantOrdering>(
                @$"(SELECT /*+ no_merge(t) */ Id as ParticipantId, RandomOrderingIndex 
                FROM (SELECT Id, RAND({seed}) as RandomOrderingIndex FROM Participants) t 
                WHERE RandomOrderingIndex >= {pageMinInclusive} AND RandomOrderingIndex < {pageMaxExclusive})")
            .OrderBy(i => i.RandomOrderingIndex)
            .Join(dbContext.Participants, ordering => ordering.ParticipantId, participant => participant.Id, (ordering, participant) => participant);
        }

        private class ParticipantOrdering
        {
            public int ParticipantId { get; set; }
            public int RandomOrderingIndex { get; set; }
        }
    }
}