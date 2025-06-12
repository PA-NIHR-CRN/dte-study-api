UPDATE dte.ParticipantAddress SET canonicaltown = town
where (IFNULL(BINARY addressline1 = BINARY UPPER(addressline1), true)
and IFNULL(BINARY addressline2 = BINARY UPPER(addressline2), true)
and IFNULL(BINARY addressline3 = BINARY UPPER(addressline3), true)
and IFNULL(BINARY addressline4 = BINARY UPPER(addressline4), true)
and IFNULL(BINARY town = BINARY UPPER(town), true)
and IFNULL(BINARY postcode = BINARY UPPER(postcode), true)
and town is not null
and postcode is not null
and canonicaltown is null
and town <> '-'
)