using BPOR.Rms.Abstractions.Entities;
using BPOR.Rms.Abstractions.Enums;

namespace BPOR.Rms.VolunteerInformation.Data;

public static class Extensions
{
    public static async Task CreateSampleVolunteerInformation(this IVipRepository repository, int studyId,
        CancellationToken cancellationToken)
    {
        await repository.CreatePage(studyId,
            new VsiPage
            {
                Description = """
                              The NuvanaDx Skin Cancer Study is testing a new smartphone tool that uses artificial 
                              intelligence (AI) to help detect skin cancer early, including melanoma.

                              The research team wants to find out if this tool can support doctors in making faster and 
                              more accurate diagnoses.
                              """,
                StudyType = VsiStudyType.Hybrid,
                Sites =
                [
                    new VsiSite
                    {
                        Id = 1,
                        AddressLine1 = "University of Birmingham",
                        AddressLine4 = "Birmingham",
                        Postcode = "B15 2TT"
                    },
                    new VsiSite
                    {
                        Id = 2,
                        AddressLine1 = "University of Essex",
                        AddressLine4 = "Colchester",
                        Postcode = "CO4 3SQ"
                    }
                ],
                Groups =
                [
                    new VsiGroup
                    {
                        Id = 1,
                        Name = "Group One",
                        Criteria =
                        [
                            new()
                            {
                                Id = 2, Description = "aged between 18 and 70 years.",
                                Type = VsiGroupCriteronType.Include
                            },
                            new()
                            {
                                Id = 3,
                                Description =
                                    "experience of two or more episodes of neck pain during the previous 12 months.",
                                Type = VsiGroupCriteronType.Include
                            },
                            new() { Id = 4, Description = "now neck-pain free.", Type = VsiGroupCriteronType.Include },
                            new()
                            {
                                Id = 5, Description = "you have a previous shoulder or spinal surgery.",
                                Type = VsiGroupCriteronType.Exclude
                            },
                            new()
                            {
                                Id = 6, Description = "you have a history of neck trauma e.g. whiplash injury.",
                                Type = VsiGroupCriteronType.Exclude
                            },
                            new()
                            {
                                Id = 7, Description = "you have any neurological or cardiovascular disorders.",
                                Type = VsiGroupCriteronType.Exclude
                            },
                            new()
                            {
                                Id = 8, Description = "you have rheumatic joint disease.",
                                Type = VsiGroupCriteronType.Exclude
                            },
                            new()
                            {
                                Id = 9, Description = "you are currently pregnant", Type = VsiGroupCriteronType.Exclude
                            },
                            new()
                            {
                                Id = 10,
                                Description = "you are currently receiving treatment of neck pain from the NHS",
                                Type = VsiGroupCriteronType.Exclude
                            },
                            new()
                            {
                                Id = 11,
                                Description =
                                    "you have been advised by a doctor not to perform vigorous or exhaustive activity for medical reasons.",
                                Type = VsiGroupCriteronType.Exclude
                            }
                        ]
                    },
                    new VsiGroup
                    {
                        Id = 2,
                        Name = "Group two",
                        Criteria =
                        [
                            new()
                            {
                                Id = 2, Description = "aged between 50 and 80 years.",
                                Type = VsiGroupCriteronType.Include
                            },
                            new()
                            {
                                Id = 3,
                                Description = "experience of two or more episodes of migraine the previous 12 months.",
                                Type = VsiGroupCriteronType.Include
                            },
                            new()
                            {
                                Id = 5, Description = "you have a previous shoulder or spinal surgery.",
                                Type = VsiGroupCriteronType.Exclude
                            },
                            new()
                            {
                                Id = 6, Description = "you have a history of head injury.",
                                Type = VsiGroupCriteronType.Exclude
                            },
                            new()
                            {
                                Id = 10, Description = "you are currently taking anti-inflammatory medication",
                                Type = VsiGroupCriteronType.Exclude
                            },
                            new()
                            {
                                Id = 11,
                                Description =
                                    "you have been advised by a doctor not to perform vigorous or exhaustive activity for medical reasons.",
                                Type = VsiGroupCriteronType.Exclude
                            }
                        ]
                    }
                ],
                WhatYouWillDo = """
                                If you decide to take part in this study, you will be asked to attend one research 
                                session lasting approximately 60–90 minutes. During the session, you will complete a 
                                series of questionnaires about your experiences, thoughts, and feelings in challenging 
                                or stressful situations.
                                """,
                CostReimbursement = true,
                HasIncentive = true,
                IncentiveDetails = "you will be given a £50 gift voucher upon completion of this study",
                NumberOfVisits = "2 visits to the research facility per year for ever",
                StudyDuration = "2 years",
                OtherDetails = """
                               Taking part in this study is entirely voluntary. Choosing not to participate, or deciding 
                               to withdraw from the study at any point, will not result in any penalty or disadvantage. 
                               You do not need to provide a reason for withdrawing.
                               """,
                Contacts =
                [
                    new VsiContact()
                    {
                        Id = 1,
                        Email = "test001@nihr.ac.uk",
                        Name = "Nihr Test User",
                        Organisation = "Nihr",
                        PhoneNumber = "1234567890",
                        Role = "Mock test user"
                    }
                ],
                StudyFormat =
                    """
                    The study will be a series of laboratory experiments, involving torture,
                    ritual incantations, garlic and 1000mg of paracetamol administered twice daily.

                    Following the final session there will be a patient satisfaction survey followed by
                    coffee and petit-fours in the waiting area.
                    """
            },
            cancellationToken);
    }
}