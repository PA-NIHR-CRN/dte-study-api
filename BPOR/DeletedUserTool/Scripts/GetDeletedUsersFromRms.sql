WITH one_participant_per_email AS (
    SELECT
        p.*,
        ROW_NUMBER() OVER (
            PARTITION BY p.Email
            ORDER BY
                CASE
                    WHEN EXISTS (
                        SELECT 1
                        FROM dte.ParticipantIdentifiers pi
                        WHERE pi.ParticipantId = p.Id
                          AND pi.IdentifierTypeId = 3
                    )
                        THEN 0
                    ELSE 1
                    END,
                p.Id
            ) AS row_num
    FROM dte.Participants p
    WHERE p.IsDeleted = 0
      AND p.Email IS NOT NULL
      AND EXISTS (
        SELECT 1
        FROM dte.Participants duplicate
        WHERE duplicate.Email = p.Email
          AND duplicate.Id <> p.Id
    )
      AND EXISTS (
        SELECT 1
        FROM dte.Participants related
                 INNER JOIN dte.ParticipantIdentifiers pi
                            ON pi.ParticipantId = related.Id
        WHERE related.Email = p.Email
          AND pi.IdentifierTypeId = 3
    )
)
SELECT p.Email
FROM one_participant_per_email p
WHERE row_num = 1
ORDER BY Email, Id;