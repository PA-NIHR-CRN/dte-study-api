UPDATE `dte`.`EmailCampaignParticipants`
SET `CampaignTypeId` = 1
WHERE `CampaignTypeId` = 0;

UPDATE `dte`.`EmailCampaigns`
SET `TypeId` = 1
WHERE `TypeId` = 0;
