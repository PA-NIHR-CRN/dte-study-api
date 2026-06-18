using BPOR.Rms.Abstractions.Entities;
using BPOR.Rms.Abstractions.Enums;

namespace BPOR.Rms.VolunteerInformation.Data;

public static class Extensions
{
    public static async Task CreateSampleVolunteerInformation(this IVipRepository repository, int studyId, CancellationToken cancellationToken)
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
                Sites = [
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
                Groups = [
                    new VsiGroup
                    {
                        Id = 1,
                        Name = "Group One",
                        Criteria = [
                            new() {Id = 2, Description = "aged between 18 and 70 years.", Type = VsiGroupCriteronType.Include},
                            new() {Id = 3, Description = "experience of two or more episodes of neck pain during the previous 12 months.", Type = VsiGroupCriteronType.Include},
                            new() {Id = 4, Description = "now neck-pain free.", Type = VsiGroupCriteronType.Include},
                            new() {Id = 5, Description = "you have a previous shoulder or spinal surgery.", Type = VsiGroupCriteronType.Exclude},
                            new() {Id = 6, Description = "you have a history of neck trauma e.g. whiplash injury.", Type = VsiGroupCriteronType.Exclude},
                            new() {Id = 7, Description = "you have any neurological or cardiovascular disorders.", Type = VsiGroupCriteronType.Exclude},
                            new() {Id = 8, Description = "you have rheumatic joint disease.", Type = VsiGroupCriteronType.Exclude},
                            new() {Id = 9, Description = "you are currently pregnant", Type = VsiGroupCriteronType.Exclude},
                            new() {Id = 10, Description = "you are currently receiving treatment of neck pain from the NHS", Type = VsiGroupCriteronType.Exclude},
                            new() {Id = 11, Description = "you have been advised by a doctor not to perform vigorous or exhaustive activity for medical reasons.", Type = VsiGroupCriteronType.Exclude}
                        ]
                    },
                    new VsiGroup
                    {
                        Id = 2,
                        Name = "Group two",
                        Criteria = [
                            new() {Id = 2, Description = "aged between 50 and 80 years.", Type = VsiGroupCriteronType.Include},
                            new() {Id = 3, Description = "experience of two or more episodes of migraine the previous 12 months.", Type = VsiGroupCriteronType.Include},
                            new() {Id = 5, Description = "you have a previous shoulder or spinal surgery.", Type = VsiGroupCriteronType.Exclude},
                            new() {Id = 6, Description = "you have a history of head injury.", Type = VsiGroupCriteronType.Exclude},
                            new() {Id = 10, Description = "you are currently taking anti-inflammatory medication", Type = VsiGroupCriteronType.Exclude},
                            new() {Id = 11, Description = "you have been advised by a doctor not to perform vigorous or exhaustive activity for medical reasons.", Type = VsiGroupCriteronType.Exclude}
                        ]
                    }
                ],
                WhatYouWillDo = """
                                If you decide to take part in this study, you will be asked to attend one research 
                                session lasting approximately 60–90 minutes. During the session, you will complete a 
                                series of questionnaires about your experiences, thoughts, and feelings in challenging 
                                or stressful situations.

                                You will also take part in a number of computer-based tasks designed to simulate 
                                decision-making and problem-solving under pressure. These activities are intended to 
                                create mild, temporary psychological stress similar to that experienced in everyday
                                situations such as examinations, public speaking, or working to a deadline. You will 
                                not be exposed to any real danger or survival situation.

                                Throughout the session, researchers will measure physical responses associated with 
                                stress. These measurements may include heart rate, blood pressure, skin conductance 
                                (changes in sweating), and saliva samples used to assess stress-related hormones. All 
                                measurements are non-invasive and will be explained before the session begins.

                                You may be asked to take short breaks between tasks, and you can request additional 
                                breaks at any time. Participation is entirely voluntary, and you are free to stop any 
                                activity or withdraw from the study without giving a reason.
                                
                                At the end of the session, you will be given an explanation of the study and an 
                                opportunity to ask questions about the research.
                                """,
                CostReimbursement = true,
                HasIncentive = true,
                IncentiveDetails = "you will be given a £50 gift voucher upon completion of this study",
                NumberOfVisits = "2 visits to the research facility per year for ever",
                OtherDetails = """
                               Taking part in this study is entirely voluntary. Choosing not to participate, or deciding 
                               to withdraw from the study at any point, will not result in any penalty or disadvantage. 
                               You do not need to provide a reason for withdrawing.

                               The study investigates how the human body responds to acute stress in situations that
                                require rapid decision-making and adaptation. Although the study examines responses 
                                relevant to survival scenarios, all activities take place in a controlled research 
                                environment and do not involve exposure to real physical danger.

                               Some participants may find certain tasks mildly uncomfortable or stressful. If you 
                               experience discomfort, you may pause the session, request a break, or stop participating
                               altogether. The research team will monitor participants throughout the study and will
                               provide further information during the consent process.

                               Information collected during the study will be handled confidentially and stored securely
                               in accordance with applicable data protection regulations and institutional policies. 
                               Personal information will be separated from research data wherever possible, and reports 
                               arising from the study will not identify individual participants.

                               With your permission, anonymised data collected during this study may be used in future 
                               research projects or shared with other researchers for scientific purposes. Any such 
                               sharing will take place in accordance with relevant ethical approvals and data protection
                               requirements.

                               If you have any questions about the study, your participation, or how your data will be 
                               used, you will be provided with contact details for the research team and the appropriate 
                               ethics review body before deciding whether to take part.
                               """,
                Contacts = [
                new VsiContact()
                {
                    Id = 1,
                    Email = "test001@nihr.ac.uk",
                    Name = "Nihr Test User",
                    Organisation = "Nihr",
                    PhoneNumber = "1234567890",
                    Role = "Mock test user"
                }]
                
            },
            cancellationToken);
    }
}