using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DYNAMO.STREAM.HANDLER.Migrations
{
    public partial class InitialSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "SysRefCommunicationLanguage",
                columns: new[] { "Id", "Code", "Description", "IsDeleted" },
                values: new object[,]
                {
                    { 1, "1", "en-GB", false },
                    { 2, "2", "cy-GB", false }
                });

            migrationBuilder.InsertData(
                table: "SysRefDailyLifeImpact",
                columns: new[] { "Id", "Code", "Description", "IsDeleted" },
                values: new object[,]
                {
                    { 1, "1", "Yes, a lot", false },
                    { 2, "2", "Yes, a little", false },
                    { 3, "3", "Not at all", false },
                    { 4, "4", "Prefer not to say", false }
                });

            migrationBuilder.InsertData(
                table: "SysRefGender",
                columns: new[] { "Id", "Code", "Description", "IsDeleted" },
                values: new object[,]
                {
                    { 1, "1", "Male", false },
                    { 2, "2", "Female", false },
                    { 3, "3", "Prefer Not to Say", false }
                });

            migrationBuilder.InsertData(
                table: "SysRefHealthCondition",
                columns: new[] { "Id", "Code", "Description", "IsDeleted" },
                values: new object[,]
                {
                    { 1, "1", "Acanthosis nigricans", false },
                    { 2, "2", "Achalasia", false },
                    { 3, "3", "Acid and chemical burns", false },
                    { 4, "4", "Acoustic neuroma (vestibular schwannoma)", false },
                    { 5, "5", "Vestibular schwannoma", false },
                    { 6, "6", "Acromegaly", false },
                    { 7, "7", "Gigantism", false },
                    { 8, "8", "Urine albumin to creatinine ratio (ACR)", false },
                    { 9, "9", "Actinic keratoses (solar keratoses)", false },
                    { 10, "10", "Solar keratoses", false },
                    { 11, "11", "Acupuncture", false },
                    { 12, "12", "Acute cholecystitis", false },
                    { 13, "13", "Gallbladder pain", false },
                    { 14, "14", "Cholecystitis (acute)", false },
                    { 15, "15", "MND", false },
                    { 16, "16", "Acute kidney injury", false },
                    { 17, "17", "Acute respiratory distress syndrome", false },
                    { 18, "18", "Adenoidectomy", false },
                    { 19, "19", "Air or gas embolism", false },
                    { 20, "20", "Decompression sickness", false },
                    { 21, "21", "Alcohol poisoning", false },
                    { 22, "22", "Alexander technique", false },
                    { 23, "23", "Alkaptonuria", false },
                    { 24, "24", "Amputation", false },
                    { 25, "25", "Amyloidosis", false },
                    { 26, "26", "Anabolic steroid misuse", false },
                    { 27, "27", "Steroid misuse", false },
                    { 28, "28", "Anaesthesia", false },
                    { 29, "29", "Anal cancer", false },
                    { 30, "30", "Anal pain", false },
                    { 31, "31", "Proctalgia", false },
                    { 32, "32", "Angelman syndrome", false },
                    { 33, "33", "Animal and human bites", false },
                    { 34, "34", "Bite (animal or human)", false },
                    { 35, "35", "Anosmia", false },
                    { 36, "36", "Antacids", false },
                    { 37, "37", "Antihistamines", false },
                    { 38, "38", "Antisocial personality disorder", false },
                    { 39, "39", "Anxiety disorders in children", false },
                    { 40, "40", "Arrhythmia", false },
                    { 41, "41", "Heart rhythm problems", false },
                    { 42, "42", "Arterial thrombosis", false },
                    { 43, "43", "Intrauterine insemination (IUI)", false },
                    { 44, "44", "Asbestosis", false },
                    { 45, "45", "Aspirin", false },
                    { 46, "46", "Atherosclerosis (arteriosclerosis)", false },
                    { 47, "47", "Athlete's foot", false },
                    { 48, "48", "Auditory processing disorder (APD)", false },
                    { 49, "49", "Balanitis", false },
                    { 50, "50", "Barium enema", false },
                    { 51, "51", "Bedbugs", false },
                    { 52, "52", "Beta blockers", false },
                    { 53, "53", "Black eye", false },
                    { 54, "54", "Blood clots", false },
                    { 55, "55", "Blood groups", false },
                    { 56, "56", "Blood in semen (haematospermia)", false },
                    { 57, "57", "Blood in urine", false },
                    { 58, "58", "Blood pressure test", false },
                    { 59, "59", "Body dysmorphic disorder (BDD)", false },
                    { 60, "60", "Infected piercings", false },
                    { 61, "61", "Boils", false },
                    { 62, "62", "Botulism", false },
                    { 63, "63", "Bowel polyps", false },
                    { 64, "64", "Bowen's disease", false },
                    { 65, "65", "Brain tumours", false },
                    { 66, "66", "Breast pain", false },
                    { 67, "67", "Breast reduction on the NHS", false },
                    { 68, "68", "Breath-holding in babies and children", false },
                    { 69, "69", "Broken ankle", false },
                    { 70, "70", "Broken arm or wrist", false },
                    { 71, "71", "Broken collarbone", false },
                    { 72, "72", "Broken finger or thumb", false },
                    { 73, "73", "Broken leg", false },
                    { 74, "74", "Broken nose", false },
                    { 75, "75", "Broken or bruised ribs", false },
                    { 76, "76", "Broken toe", false },
                    { 77, "77", "Bronchitis", false },
                    { 78, "78", "Brucellosis", false },
                    { 79, "79", "Brugada syndrome", false },
                    { 80, "80", "Carbon monoxide poisoning", false },
                    { 81, "81", "Neuroendocrine tumours and carcinoid syndrome", false },
                    { 82, "82", "Cardiomyopathy", false },
                    { 83, "83", "Cardiovascular disease", false },
                    { 84, "84", "Age-related cataracts", false },
                    { 85, "85", "Cataracts (age-related)", false },
                    { 86, "86", "Catarrh", false },
                    { 87, "87", "Cavernoma", false },
                    { 88, "88", "Clostridium difficile (C. diff) infection", false },
                    { 89, "89", "Carcinoembryonic antigen (CEA) test", false },
                    { 90, "90", "Cervical rib", false },
                    { 91, "91", "Thoracic outlet syndrome", false },
                    { 92, "92", "Charles Bonnet syndrome", false },
                    { 93, "93", "Chest infection", false },
                    { 94, "94", "Chest pain", false },
                    { 95, "95", "Heart pain", false },
                    { 96, "96", "Chiari malformation", false },
                    { 97, "97", "Chilblains", false },
                    { 98, "98", "Chiropractic", false },
                    { 99, "99", "Cholesteatoma", false },
                    { 100, "100", "Chronic traumatic encephalopathy", false },
                    { 101, "101", "Circumcision in boys", false },
                    { 102, "102", "Circumcision in men", false },
                    { 103, "103", "Claustrophobia", false },
                    { 104, "104", "Cluster headaches", false },
                    { 105, "105", "Colour vision deficiency (colour blindness)", false },
                    { 106, "106", "Coma", false },
                    { 107, "107", "Compartment syndrome", false },
                    { 108, "108", "Concussion", false },
                    { 109, "109", "Sudden confusion (delirium)", false },
                    { 110, "110", "Confusion (sudden)", false },
                    { 111, "111", "Delirium", false },
                    { 112, "112", "Costochondritis", false },
                    { 113, "113", "Cough", false },
                    { 114, "114", "Coughing up blood (blood in phlegm)", false },
                    { 115, "115", "Cradle cap", false },
                    { 116, "116", "CT scan", false },
                    { 117, "117", "Cuts and grazes", false },
                    { 118, "118", "Blue skin or lips (cyanosis)", false },
                    { 119, "119", "Cyanosis", false },
                    { 120, "120", "Cyclical vomiting syndrome", false },
                    { 121, "121", "Cyclospora", false },
                    { 122, "122", "Cyclothymia", false },
                    { 123, "123", "Dandruff", false },
                    { 124, "124", "Decongestants", false },
                    { 125, "125", "Dental abscess", false },
                    { 126, "126", "Dentures (false teeth)", false },
                    { 127, "127", "Dyspraxia (developmental co-ordination disorder) in adults", false },
                    { 128, "128", "Developmental dysplasia of the hip", false },
                    { 129, "129", "Congenital hip dislocation", false },
                    { 130, "130", "Hip dysplasia", false },
                    { 131, "131", "Diabetes", false },
                    { 132, "132", "Diabetic eye screening", false },
                    { 133, "133", "Diabetic ketoacidosis", false },
                    { 134, "134", "DiGeorge syndrome (22q11 deletion)", false },
                    { 135, "135", "Dislocated kneecap", false },
                    { 136, "136", "Dislocated shoulder", false },
                    { 137, "137", "Differences in sex development", false },
                    { 138, "138", "intersex", false },
                    { 139, "139", "Dissociative disorders", false },
                    { 140, "140", "Diverticular disease and diverticulitis", false },
                    { 141, "141", "Dizziness", false },
                    { 142, "142", "Dry mouth", false },
                    { 143, "143", "Dysarthria (difficulty speaking)", false },
                    { 144, "144", "Dysentery", false },
                    { 145, "145", "Earache", false },
                    { 146, "146", "Early or delayed puberty", false },
                    { 147, "147", "Puberty (early or delayed)", false },
                    { 148, "148", "Earwax build-up", false },
                    { 149, "149", "Eating disorders", false },
                    { 150, "150", "Ebola virus disease", false },
                    { 151, "151", "Echocardiogram", false },
                    { 152, "152", "Ectropion", false },
                    { 153, "153", "Edwards' syndrome (trisomy 18)", false },
                    { 154, "154", "Ehlers-Danlos syndromes", false },
                    { 155, "155", "Ejaculation problems", false },
                    { 156, "156", "Premature ejaculation", false },
                    { 157, "157", "Elbow and arm pain", false },
                    { 158, "158", "Electrocardiogram (ECG)", false },
                    { 159, "159", "Electroencephalogram (EEG)", false },
                    { 160, "160", "Electrolyte test", false },
                    { 161, "161", "Embolism", false },
                    { 162, "162", "Emollients", false },
                    { 163, "163", "Empyema", false },
                    { 164, "164", "Endoscopy", false },
                    { 165, "165", "Enhanced recovery", false },
                    { 166, "166", "Epididymitis", false },
                    { 167, "167", "Epiglottitis", false },
                    { 168, "168", "Erythema multiforme", false },
                    { 169, "169", "Erythema nodosum", false },
                    { 170, "170", "Erythromelalgia", false },
                    { 171, "171", "Euthanasia and assisted suicide", false },
                    { 172, "172", "Ewing sarcoma", false },
                    { 173, "173", "Excessive daytime sleepiness (hypersomnia)", false },
                    { 174, "174", "Hypersomnia", false },
                    { 175, "175", "Eye cancer", false },
                    { 176, "176", "Eye injuries", false },
                    { 177, "177", "Eyelid problems", false },
                    { 178, "178", "Eye tests for children", false },
                    { 179, "179", "Prosopagnosia (face blindness)", false },
                    { 180, "180", "Face blindness", false },
                    { 181, "181", "Febrile seizures", false },
                    { 182, "182", "Fits (children with fever)", false },
                    { 183, "183", "Seizures (children with fever)", false },
                    { 184, "184", "Female genital mutilation (FGM)", false },
                    { 185, "185", "High temperature (fever) in children", false },
                    { 186, "186", "Fever in children", false },
                    { 187, "187", "Flat feet", false },
                    { 188, "188", "Fluoride", false },
                    { 189, "189", "Foetal alcohol spectrum disorder", false },
                    { 190, "190", "Food colours and hyperactivity", false },
                    { 191, "191", "Food intolerance", false },
                    { 192, "192", "Foot drop", false },
                    { 193, "193", "Gallbladder cancer", false },
                    { 194, "194", "Ganglion cyst", false },
                    { 195, "195", "Gastritis", false },
                    { 196, "196", "Gastroparesis", false },
                    { 197, "197", "General anaesthesia", false },
                    { 198, "198", "Gilbert's syndrome", false },
                    { 199, "199", "Glutaric aciduria type 1", false },
                    { 200, "200", "Granuloma annulare", false },
                    { 201, "201", "Granulomatosis with polyangiitis", false },
                    { 202, "202", "Growing pains", false },
                    { 203, "203", "Hair dye reactions", false },
                    { 204, "204", "Hairy cell leukaemia", false },
                    { 205, "205", "Leukaemia (hairy cell)", false },
                    { 206, "206", "Hallucinations and hearing voices", false },
                    { 207, "207", "Hearing voices", false },
                    { 208, "208", "Hamstring injury", false },
                    { 209, "209", "Hand foot and mouth disease", false },
                    { 210, "210", "Head and neck cancer", false },
                    { 211, "211", "Health anxiety", false },
                    { 212, "212", "Hypochondria", false },
                    { 213, "213", "Hearing tests for children", false },
                    { 214, "214", "Heart-lung transplant", false },
                    { 215, "215", "Heart palpitations and ectopic beats", false },
                    { 216, "216", "Palpitations", false },
                    { 217, "217", "Ectopic beats", false },
                    { 218, "218", "Heat exhaustion and heatstroke", false },
                    { 219, "219", "Heat rash (prickly heat)", false },
                    { 220, "220", "Prickly heat", false },
                    { 221, "221", "Sweating (excessive)", false },
                    { 222, "222", "sweat rash", false },
                    { 223, "223", "Henoch-Schönlein purpura (HSP)", false },
                    { 224, "224", "Hepatitis", false },
                    { 225, "225", "Herbal medicines", false },
                    { 226, "226", "Herceptin (trastuzumab)", false },
                    { 227, "227", "Hereditary haemorrhagic telangiectasia (HHT)", false },
                    { 228, "228", "Hereditary neuropathy with pressure palsies (HNPP)", false },
                    { 229, "229", "Hereditary spastic paraplegia", false },
                    { 230, "230", "Hernia", false },
                    { 231, "231", "Herpes simplex eye infections", false },
                    { 232, "232", "Eye infection (herpes)", false },
                    { 233, "233", "Herpetic whitlow (whitlow finger)", false },
                    { 234, "234", "Whitlow finger", false },
                    { 235, "235", "Haemophilus influenzae type b (Hib)", false },
                    { 236, "236", "Hidradenitis suppurativa (HS)", false },
                    { 237, "237", "Hyperglycaemia (high blood sugar)", false },
                    { 238, "238", "Hip pain in adults", false },
                    { 239, "239", "Hirschsprung's disease", false },
                    { 240, "240", "Hoarding disorder", false },
                    { 241, "241", "Homeopathy", false },
                    { 242, "242", "Home oxygen therapy", false },
                    { 243, "243", "Oxygen therapy", false },
                    { 244, "244", "Homocystinuria", false },
                    { 245, "245", "Noise sensitivity (hyperacusis)", false },
                    { 246, "246", "Hypnotherapy", false },
                    { 247, "247", "Hypothermia", false },
                    { 248, "248", "Ichthyosis", false },
                    { 249, "249", "Indigestion", false },
                    { 250, "250", "Inflammatory bowel disease", false },
                    { 251, "251", "Ingrown hairs", false },
                    { 252, "252", "Ingrown toenail", false },
                    { 253, "253", "Intensive care", false },
                    { 254, "254", "Interstitial cystitis", false },
                    { 255, "255", "Intracranial hypertension", false },
                    { 256, "256", "Hip pain in children (irritable hip)", false },
                    { 257, "257", "Irritable hip", false },
                    { 258, "258", "Isovaleric acidaemia", false },
                    { 259, "259", "Joint pain", false },
                    { 260, "260", "Kaposi's sarcoma", false },
                    { 261, "261", "Keratosis pilaris", false },
                    { 262, "262", "Klinefelter syndrome", false },
                    { 263, "263", "Knee pain", false },
                    { 264, "264", "Knock knees", false },
                    { 265, "265", "Kwashiorkor", false },
                    { 266, "266", "Labial fusion", false },
                    { 267, "267", "Lambert-Eaton myasthenic syndrome", false },
                    { 268, "268", "Lactate dehydrogenase (LDH) test", false },
                    { 269, "269", "Legionnaires' disease", false },
                    { 270, "270", "Lichen sclerosus", false },
                    { 271, "271", "Limping in children", false },
                    { 272, "272", "Lipoedema", false },
                    { 273, "273", "Lipoma", false },
                    { 274, "274", "Liver disease", false },
                    { 275, "275", "Local anaesthesia", false },
                    { 276, "276", "Long QT syndrome", false },
                    { 277, "277", "Loss of libido (reduced sex drive)", false },
                    { 278, "278", "Low blood sugar (hypoglycaemia)", false },
                    { 279, "279", "Hypoglycaemia (low blood sugar)", false },
                    { 280, "280", "Low sperm count", false },
                    { 281, "281", "Sperm count (low)", false },
                    { 282, "282", "Lumps", false },
                    { 283, "283", "Lyme disease", false },
                    { 284, "284", "Macular hole", false },
                    { 285, "285", "Magnesium test", false },
                    { 286, "286", "The 'male menopause'", false },
                    { 287, "287", "Male menopause", false },
                    { 288, "288", "Mallet finger", false },
                    { 289, "289", "Maple syrup urine disease", false },
                    { 290, "290", "Mastoiditis", false },
                    { 291, "291", "MCADD", false },
                    { 292, "292", "Medically unexplained symptoms", false },
                    { 293, "293", "Functional neurological disorder", false },
                    { 294, "294", "Ménière's disease", false },
                    { 295, "295", "Mesothelioma", false },
                    { 296, "296", "Metabolic syndrome", false },
                    { 297, "297", "Metallic taste", false },
                    { 298, "298", "Mitral valve problems", false },
                    { 299, "299", "Heart valve problems", false },
                    { 300, "300", "Molar pregnancy", false },
                    { 301, "301", "Morton's neuroma", false },
                    { 302, "302", "Motion sickness", false },
                    { 303, "303", "Mouth ulcers", false },
                    { 304, "304", "MRSA", false },
                    { 305, "305", "Multiple system atrophy", false },
                    { 306, "306", "Mycobacterium chimaera infection", false },
                    { 307, "307", "Myelodysplastic syndrome (myelodysplasia)", false },
                    { 308, "308", "Myositis (polymyositis and dermatomyositis)", false },
                    { 309, "309", "Nail patella syndrome", false },
                    { 310, "310", "Nail problems", false },
                    { 311, "311", "Nasal and sinus cancer", false },
                    { 312, "312", "Nose cancer", false },
                    { 313, "313", "Sinus cancer", false },
                    { 314, "314", "Nasopharyngeal cancer", false },
                    { 315, "315", "Neck pain", false },
                    { 316, "316", "Necrotising fasciitis", false },
                    { 317, "317", "Neonatal herpes (herpes in a baby)", false },
                    { 318, "318", "Herpes in babies", false },
                    { 319, "319", "Nephrotic syndrome in children", false },
                    { 320, "320", "Neuroblastoma", false },
                    { 321, "321", "Neuroendocrine tumours", false },
                    { 322, "322", "Neuromyelitis optica", false },
                    { 323, "323", "Night sweats", false },
                    { 324, "324", "Sweating at night", false },
                    { 325, "325", "Night terrors and nightmares", false },
                    { 326, "326", "Nipple discharge", false },
                    { 327, "327", "Non-alcoholic fatty liver disease (NAFLD)", false },
                    { 328, "328", "Norovirus (vomiting bug)", false },
                    { 329, "329", "Vomiting bug", false },
                    { 330, "330", "Winter vomiting bug", false },
                    { 331, "331", "NSAIDs", false },
                    { 332, "332", "Swollen ankles feet and legs (oedema)", false },
                    { 333, "333", "Oesophageal atresia and tracheo-oesophageal fistula", false },
                    { 334, "334", "Orf", false },
                    { 335, "335", "Osteophyte (bone spur)", false },
                    { 336, "336", "Otosclerosis", false },
                    { 337, "337", "Ovulation pain", false },
                    { 338, "338", "Panic disorder", false },
                    { 339, "339", "Patau's syndrome", false },
                    { 340, "340", "Peak flow test", false },
                    { 341, "341", "Pelvic pain", false },
                    { 342, "342", "Penile cancer", false },
                    { 343, "343", "Period pain", false },
                    { 344, "344", "Menstrual pain", false },
                    { 345, "345", "Periods", false },
                    { 346, "346", "Persistent trophoblastic disease and choriocarcinoma", false },
                    { 347, "347", "Personality disorder", false },
                    { 348, "348", "PET scan", false },
                    { 349, "349", "Phaeochromocytoma", false },
                    { 350, "350", "Phenylketonuria", false },
                    { 351, "351", "Tight foreskin (phimosis and paraphimosis)", false },
                    { 352, "352", "Foreskin problems", false },
                    { 353, "353", "Phimosis", false },
                    { 354, "354", "Phlebitis (superficial thrombophlebitis)", false },
                    { 355, "355", "Superficial thrombophlebitis", false },
                    { 356, "356", "Phosphate test", false },
                    { 357, "357", "Photodynamic therapy (PDT)", false },
                    { 358, "358", "Pins and needles", false },
                    { 359, "359", "PIP breast implants", false },
                    { 360, "360", "Pityriasis rosea", false },
                    { 361, "361", "Pityriasis versicolor", false },
                    { 362, "362", "Plagiocephaly and brachycephaly (flat head syndrome)", false },
                    { 363, "363", "Brachycephaly and plagiocephaly", false },
                    { 364, "364", "Flat head syndrome", false },
                    { 365, "365", "Pleurisy", false },
                    { 366, "366", "Polio", false },
                    { 367, "367", "Polyhydramnios (too much amniotic fluid)", false },
                    { 368, "368", "Polymorphic light eruption", false },
                    { 369, "369", "Pompholyx (dyshidrotic eczema)", false },
                    { 370, "370", "Postmenopausal bleeding", false },
                    { 371, "371", "Bleeding after the menopause", false },
                    { 372, "372", "Post-mortem", false },
                    { 373, "373", "Postpartum psychosis", false },
                    { 374, "374", "Postural tachycardia syndrome (PoTS)", false },
                    { 375, "375", "Potassium test", false },
                    { 376, "376", "Predictive genetic tests for cancer risk genes", false },
                    { 377, "377", "Genetic test for cancer gene", false },
                    { 378, "378", "Probiotics", false },
                    { 379, "379", "Problems swallowing pills", false },
                    { 380, "380", "Swallowing pills", false },
                    { 381, "381", "Prostate problems", false },
                    { 382, "382", "Prostatitis", false },
                    { 383, "383", "Psoriatic arthritis", false },
                    { 384, "384", "Psychiatry", false },
                    { 385, "385", "Pubic lice", false },
                    { 386, "386", "Pudendal neuralgia", false },
                    { 387, "387", "Pyoderma gangrenosum", false },
                    { 388, "388", "Q fever", false },
                    { 389, "389", "Rashes in babies and children", false },
                    { 390, "390", "Red blood cell count", false },
                    { 391, "391", "Red eye", false },
                    { 392, "392", "Reflux in babies", false },
                    { 393, "393", "Acid reflux in babies", false },
                    { 394, "394", "Respiratory tract infections (RTIs)", false },
                    { 395, "395", "Retinal migraine", false },
                    { 396, "396", "Retinoblastoma (eye cancer in children)", false },
                    { 397, "397", "Rett syndrome", false },
                    { 398, "398", "Reye's syndrome", false },
                    { 399, "399", "Roseola", false },
                    { 400, "400", "Salivary gland stones", false },
                    { 401, "401", "Sarcoidosis", false },
                    { 402, "402", "SARS (severe acute respiratory syndrome)", false },
                    { 403, "403", "Scarlet fever", false },
                    { 404, "404", "Schistosomiasis (bilharzia)", false },
                    { 405, "405", "Bilharzia", false },
                    { 406, "406", "Scleroderma", false },
                    { 407, "407", "Selective mutism", false },
                    { 408, "408", "Septic arthritis", false },
                    { 409, "409", "Sexually transmitted infections (STIs)", false },
                    { 410, "410", "Shin splints", false },
                    { 411, "411", "Shin pain (shin splints)", false },
                    { 412, "412", "Shortness of breath", false },
                    { 413, "413", "Shoulder impingement", false },
                    { 414, "414", "Sick building syndrome", false },
                    { 415, "415", "Silicosis", false },
                    { 416, "416", "Skin cyst", false },
                    { 417, "417", "Skin tags", false },
                    { 418, "418", "Slapped cheek syndrome", false },
                    { 419, "419", "Sleepwalking", false },
                    { 420, "420", "Smelly urine", false },
                    { 421, "421", "Urine (smelly)", false },
                    { 422, "422", "Snake bites", false },
                    { 423, "423", "Social anxiety (social phobia)", false },
                    { 424, "424", "Soft tissue sarcomas", false },
                    { 425, "425", "Sore or dry lips", false },
                    { 426, "426", "Sore lips", false },
                    { 427, "427", "Dry lips", false },
                    { 428, "428", "Lips (sore or dry)", false },
                    { 429, "429", "Sore or white tongue", false },
                    { 430, "430", "Tongue (sore or white)", false },
                    { 431, "431", "Sore throat", false },
                    { 432, "432", "Throat (sore)", false },
                    { 433, "433", "Spirometry", false },
                    { 434, "434", "Spleen problems and spleen removal", false },
                    { 435, "435", "Spondylolisthesis", false },
                    { 436, "436", "Staph infection", false },
                    { 437, "437", "Steroid inhalers", false },
                    { 438, "438", "Steroid injections", false },
                    { 439, "439", "Steroid nasal sprays", false },
                    { 440, "440", "Steroids", false },
                    { 441, "441", "Corticosteroids", false },
                    { 442, "442", "Steroid tablets", false },
                    { 443, "443", "Stevens-Johnson syndrome", false },
                    { 444, "444", "Stomach ache", false },
                    { 445, "445", "Tummy ache", false },
                    { 446, "446", "Stopped or missed periods", false },
                    { 447, "447", "Periods (stopped or missed)", false },
                    { 448, "448", "Stop smoking treatments", false },
                    { 449, "449", "Smoking (treatments to stop)", false },
                    { 450, "450", "Stretch marks", false },
                    { 451, "451", "Stye", false },
                    { 452, "452", "Sudden infant death syndrome (SIDS)", false },
                    { 453, "453", "Sunburn", false },
                    { 454, "454", "Swine flu (H1N1)", false },
                    { 455, "455", "Swollen glands", false },
                    { 456, "456", "Temporomandibular disorder (TMD)", false },
                    { 457, "457", "Jaw pain", false },
                    { 458, "458", "Tension-type headaches", false },
                    { 459, "459", "Headaches (tension-type)", false },
                    { 460, "460", "Tetanus", false },
                    { 461, "461", "Excessive thirst", false },
                    { 462, "462", "Thirst (excessive)", false },
                    { 463, "463", "Thrombophilia", false },
                    { 464, "464", "Thyroiditis", false },
                    { 465, "465", "Total iron-binding capacity (TIBC) and transferrin test", false },
                    { 466, "466", "Tongue-tie", false },
                    { 467, "467", "Toothache", false },
                    { 468, "468", "Dental pain", false },
                    { 469, "469", "Tooth decay", false },
                    { 470, "470", "Topical corticosteroids", false },
                    { 471, "471", "Steroid cream", false },
                    { 472, "472", "Corticosteroid cream", false },
                    { 473, "473", "Total protein test", false },
                    { 474, "474", "Toxic shock syndrome", false },
                    { 475, "475", "TENS (transcutaneous electrical nerve stimulation)", false },
                    { 476, "476", "Trimethylaminuria ('fish odour syndrome')", false },
                    { 477, "477", "Typhus", false },
                    { 478, "478", "Ultrasound scan", false },
                    { 479, "479", "Unintentional weight loss", false },
                    { 480, "480", "Weight loss (unintentional)", false },
                    { 481, "481", "Weight loss (unexpected)", false },
                    { 482, "482", "Urinary tract infections (UTIs)", false },
                    { 483, "483", "Vaginal discharge", false },
                    { 484, "484", "Vaginal dryness", false },
                    { 485, "485", "Vaginitis", false },
                    { 486, "486", "Vasculitis", false },
                    { 487, "487", "Blindness and vision loss", false },
                    { 488, "488", "Vomiting blood (haematemesis)", false },
                    { 489, "489", "Von Willebrand disease", false },
                    { 490, "490", "Vulvodynia (vulval pain)", false },
                    { 491, "491", "Vaginal pain", false },
                    { 492, "492", "Warts and verrucas", false },
                    { 493, "493", "West Nile virus", false },
                    { 494, "494", "Whooping cough", false },
                    { 495, "495", "Wolff-Parkinson-White syndrome", false },
                    { 496, "496", "X-ray", false },
                    { 497, "497", "Zika virus", false },
                    { 498, "498", "Abdominal aortic aneurysm", false },
                    { 499, "499", "AAA", false },
                    { 500, "500", "Aneurysm (abdominal aortic)", false },
                    { 501, "501", "Abdominal aortic aneurysm screening", false },
                    { 502, "502", "AAA screening", false },
                    { 503, "503", "Abscess", false },
                    { 504, "504", "Acne", false },
                    { 505, "505", "Actinomycosis", false },
                    { 506, "506", "Acute lymphoblastic leukaemia", false },
                    { 507, "507", "Leukaemia (acute lymphoblastic)", false },
                    { 508, "508", "Acute myeloid leukaemia", false },
                    { 509, "509", "Leukaemia (acute myeloid)", false },
                    { 510, "510", "Acute pancreatitis", false },
                    { 511, "511", "Pancreatitis (acute)", false },
                    { 512, "512", "Addison's disease", false },
                    { 513, "513", "Agoraphobia", false },
                    { 514, "514", "Albinism", false },
                    { 515, "515", "Alcohol misuse", false }
                });

            migrationBuilder.InsertData(
                table: "SysRefHealthCondition",
                columns: new[] { "Id", "Code", "Description", "IsDeleted" },
                values: new object[,]
                {
                    { 516, "516", "Alcohol-related liver disease", false },
                    { 517, "517", "Liver disease (alcohol-related)", false },
                    { 518, "518", "Allergic rhinitis", false },
                    { 519, "519", "Rhinitis (allergic)", false },
                    { 520, "520", "Allergies", false },
                    { 521, "521", "Altitude sickness", false },
                    { 522, "522", "Alzheimer's disease", false },
                    { 523, "523", "Amniocentesis", false },
                    { 524, "524", "Anal fissure", false },
                    { 525, "525", "Anal fistula", false },
                    { 526, "526", "Anaphylaxis", false },
                    { 527, "527", "Androgen insensitivity syndrome", false },
                    { 528, "528", "Angina", false },
                    { 529, "529", "Angioedema", false },
                    { 530, "530", "Angiography", false },
                    { 531, "531", "Ankylosing spondylitis", false },
                    { 532, "532", "Anorexia nervosa", false },
                    { 533, "533", "Antibiotics", false },
                    { 534, "534", "Anticoagulant medicines", false },
                    { 535, "535", "Antidepressants", false },
                    { 536, "536", "Antiphospholipid syndrome (APS)", false },
                    { 537, "537", "Hughes syndrome", false },
                    { 538, "538", "Aortic valve replacement", false },
                    { 539, "539", "Heart valve replacement", false },
                    { 540, "540", "Aphasia", false },
                    { 541, "541", "Appendicitis", false },
                    { 542, "542", "Arthritis", false },
                    { 543, "543", "Arthroscopy", false },
                    { 544, "544", "Aspergillosis", false },
                    { 545, "545", "Asthma", false },
                    { 546, "546", "Astigmatism", false },
                    { 547, "547", "Ataxia", false },
                    { 548, "548", "Atopic eczema", false },
                    { 549, "549", "Eczema (atopic)", false },
                    { 550, "550", "Atrial fibrillation", false },
                    { 551, "551", "Attention deficit hyperactivity disorder (ADHD)", false },
                    { 552, "552", "Autosomal dominant polycystic kidney disease", false },
                    { 553, "553", "Polycystic kidney disease (autosomal dominant)", false },
                    { 554, "554", "Autosomal recessive polycystic kidney disease", false },
                    { 555, "555", "Polycystic kidney disease (autosomal recessive)", false },
                    { 556, "556", "Back pain", false },
                    { 557, "557", "Bacterial vaginosis", false },
                    { 558, "558", "Bad breath", false },
                    { 559, "559", "Halitosis", false },
                    { 560, "560", "Baker's cyst", false },
                    { 561, "561", "Popliteal cyst", false },
                    { 562, "562", "Bartholin's cyst", false },
                    { 563, "563", "Bedwetting in children", false },
                    { 564, "564", "Behçet's disease", false },
                    { 565, "565", "Bell's palsy", false },
                    { 566, "566", "Benign brain tumour (non-cancerous)", false },
                    { 567, "567", "Brain tumour (benign)", false },
                    { 568, "568", "Bile duct cancer (cholangiocarcinoma)", false },
                    { 569, "569", "Cholangiocarcinoma", false },
                    { 570, "570", "Binge eating disorder", false },
                    { 571, "571", "Biopsy", false },
                    { 572, "572", "Bipolar disorder", false },
                    { 573, "573", "Birthmarks", false },
                    { 574, "574", "Bladder cancer", false },
                    { 575, "575", "Bladder stones", false },
                    { 576, "576", "Bleeding from the bottom (rectal bleeding)", false },
                    { 577, "577", "Rectal bleeding", false },
                    { 578, "578", "Blepharitis", false },
                    { 579, "579", "Blisters", false },
                    { 580, "580", "Blood tests", false },
                    { 581, "581", "Blushing", false },
                    { 582, "582", "Bone cancer", false },
                    { 583, "583", "Bone cyst", false },
                    { 584, "584", "Borderline personality disorder", false },
                    { 585, "585", "Bowel cancer", false },
                    { 586, "586", "Colon cancer", false },
                    { 587, "587", "Rectal cancer", false },
                    { 588, "588", "Bowel cancer screening", false },
                    { 589, "589", "Bowel incontinence", false },
                    { 590, "590", "Brain abscess", false },
                    { 591, "591", "Brain aneurysm", false },
                    { 592, "592", "Aneurysm (brain)", false },
                    { 593, "593", "Brain death", false },
                    { 594, "594", "Breast abscess", false },
                    { 595, "595", "Breast cancer in women", false },
                    { 596, "596", "Breast cancer in men", false },
                    { 597, "597", "Breast lumps", false },
                    { 598, "598", "Bronchiectasis", false },
                    { 599, "599", "Bronchiolitis", false },
                    { 600, "600", "Bronchodilators", false },
                    { 601, "601", "Exophthalmos (bulging eyes)", false },
                    { 602, "602", "Bulimia", false },
                    { 603, "603", "Burns and scalds", false },
                    { 604, "604", "Bursitis", false },
                    { 605, "605", "Caesarean section", false },
                    { 606, "606", "Cancer", false },
                    { 607, "607", "Carotid endarterectomy", false },
                    { 608, "608", "Carpal tunnel syndrome", false },
                    { 609, "609", "Cartilage damage", false },
                    { 610, "610", "Cataract surgery", false },
                    { 611, "611", "Cavernous sinus thrombosis", false },
                    { 612, "612", "Cellulitis", false },
                    { 613, "613", "Cerebral palsy", false },
                    { 614, "614", "Cervical cancer", false },
                    { 615, "615", "Cervical screening", false },
                    { 616, "616", "Smear test", false },
                    { 617, "617", "Cervical spondylosis", false },
                    { 618, "618", "Charcot-Marie-Tooth disease", false },
                    { 619, "619", "Chemotherapy", false },
                    { 620, "620", "Chickenpox", false },
                    { 621, "621", "Childhood cataracts", false },
                    { 622, "622", "Cataracts (children)", false },
                    { 623, "623", "Chlamydia", false },
                    { 624, "624", "Cholera", false },
                    { 625, "625", "Chorionic villus sampling", false },
                    { 626, "626", "Myalgic encephalomyelitis or chronic fatigue syndrome (ME/CFS)", false },
                    { 627, "627", "Chronic fatigue syndrome (ME/CFS)", false },
                    { 628, "628", "Chronic lymphocytic leukaemia", false },
                    { 629, "629", "Leukaemia (chronic lymphocytic)", false },
                    { 630, "630", "Chronic myeloid leukaemia", false },
                    { 631, "631", "Leukaemia (chronic myeloid)", false },
                    { 632, "632", "Chronic obstructive pulmonary disease (COPD)", false },
                    { 633, "633", "Chronic pancreatitis", false },
                    { 634, "634", "Pancreatitis (chronic)", false },
                    { 635, "635", "Cirrhosis", false },
                    { 636, "636", "Cleft lip and palate", false },
                    { 637, "637", "Clinical depression", false },
                    { 638, "638", "Depression", false },
                    { 639, "639", "Clinical trials", false },
                    { 640, "640", "Club foot", false },
                    { 641, "641", "Coeliac disease", false },
                    { 642, "642", "Cognitive behavioural therapy (CBT)", false },
                    { 643, "643", "Colic", false },
                    { 644, "644", "Colostomy", false },
                    { 645, "645", "Colposcopy", false },
                    { 646, "646", "Common cold", false },
                    { 647, "647", "Complex regional pain syndrome", false },
                    { 648, "648", "Congenital heart disease", false },
                    { 649, "649", "Conjunctivitis", false },
                    { 650, "650", "Consent to treatment", false },
                    { 651, "651", "Constipation", false },
                    { 652, "652", "Contact dermatitis", false },
                    { 653, "653", "Eczema (contact dermatitis)", false },
                    { 654, "654", "Cornea transplant", false },
                    { 655, "655", "Corns and calluses", false },
                    { 656, "656", "Cardiac catheterisation and coronary angiography", false },
                    { 657, "657", "Coronary angioplasty and stent insertion", false },
                    { 658, "658", "Angioplasty", false },
                    { 659, "659", "Stent insertion", false },
                    { 660, "660", "Coronary artery bypass graft", false },
                    { 661, "661", "Heart bypass", false },
                    { 662, "662", "CABG", false },
                    { 663, "663", "Coronary heart disease", false },
                    { 664, "664", "Heart disease (coronary)", false },
                    { 665, "665", "Corticobasal degeneration", false },
                    { 666, "666", "Counselling", false },
                    { 667, "667", "Craniosynostosis", false },
                    { 668, "668", "Creutzfeldt-Jakob disease", false },
                    { 669, "669", "CJD", false },
                    { 670, "670", "Crohn's disease", false },
                    { 671, "671", "Croup", false },
                    { 672, "672", "Cushing's syndrome", false },
                    { 673, "673", "Cystic fibrosis", false },
                    { 674, "674", "Cystitis", false },
                    { 675, "675", "Cystoscopy", false },
                    { 676, "676", "Cytomegalovirus (CMV)", false },
                    { 677, "677", "Deafblindness", false },
                    { 678, "678", "DVT (deep vein thrombosis)", false },
                    { 679, "679", "DVT", false },
                    { 680, "680", "Dehydration", false },
                    { 681, "681", "Dementia with Lewy bodies", false },
                    { 682, "682", "Dengue", false },
                    { 683, "683", "Developmental co-ordination disorder (dyspraxia) in children", false },
                    { 684, "684", "Dyspraxia in children", false },
                    { 685, "685", "Bone density scan (DEXA scan)", false },
                    { 686, "686", "DEXA scan", false },
                    { 687, "687", "Diabetes insipidus", false },
                    { 688, "688", "Diabetic retinopathy", false },
                    { 689, "689", "Dialysis", false },
                    { 690, "690", "Diphtheria", false },
                    { 691, "691", "Discoid eczema", false },
                    { 692, "692", "Eczema (discoid)", false },
                    { 693, "693", "Disorders of consciousness", false },
                    { 694, "694", "Vegetative state", false },
                    { 695, "695", "Double vision", false },
                    { 696, "696", "Down's syndrome", false },
                    { 697, "697", "Dry eyes", false },
                    { 698, "698", "Dupuytren's contracture", false },
                    { 699, "699", "Dyslexia", false },
                    { 700, "700", "Dystonia", false },
                    { 701, "701", "Ectopic pregnancy", false },
                    { 702, "702", "Encephalitis", false },
                    { 703, "703", "Endocarditis", false },
                    { 704, "704", "Endometriosis", false },
                    { 705, "705", "Epidermolysis bullosa", false },
                    { 706, "706", "Epidural", false },
                    { 707, "707", "Epilepsy", false },
                    { 708, "708", "Erectile dysfunction (impotence)", false },
                    { 709, "709", "Impotence", false },
                    { 710, "710", "Excessive sweating (hyperhidrosis)", false },
                    { 711, "711", "Hyperhidrosis", false },
                    { 712, "712", "Sweating (excessive)", false },
                    { 713, "713", "Fabricated or induced illness", false },
                    { 714, "714", "Fainting", false },
                    { 715, "715", "Falls", false },
                    { 716, "716", "Femoral hernia repair", false },
                    { 717, "717", "Hernia (femoral)", false },
                    { 718, "718", "Fibroids", false },
                    { 719, "719", "Fibromyalgia", false },
                    { 720, "720", "First aid", false },
                    { 721, "721", "Farting (flatulence)", false },
                    { 722, "722", "Flatulence", false },
                    { 723, "723", "Wind", false },
                    { 724, "724", "Flu", false },
                    { 725, "725", "Influenza", false },
                    { 726, "726", "Food allergy", false },
                    { 727, "727", "Food poisoning", false },
                    { 728, "728", "Frontotemporal dementia", false },
                    { 729, "729", "Dementia (frontotemporal)", false },
                    { 730, "730", "Frostbite", false },
                    { 731, "731", "Frozen shoulder", false },
                    { 732, "732", "Fungal nail infection", false },
                    { 733, "733", "Nail fungal infection", false },
                    { 734, "734", "Gallbladder removal", false },
                    { 735, "735", "Gallstones", false },
                    { 736, "736", "Gangrene", false },
                    { 737, "737", "Gastrectomy", false },
                    { 738, "738", "Gastroscopy", false },
                    { 739, "739", "Gender dysphoria", false },
                    { 740, "740", "Generalised anxiety disorder in adults", false },
                    { 741, "741", "Anxiety disorder in adults", false },
                    { 742, "742", "Genital herpes", false },
                    { 743, "743", "Herpes (genital)", false },
                    { 744, "744", "Genital warts", false },
                    { 745, "745", "Gestational diabetes", false },
                    { 746, "746", "Diabetes in pregnancy", false },
                    { 747, "747", "Giardiasis", false },
                    { 748, "748", "Glandular fever", false },
                    { 749, "749", "Glaucoma", false },
                    { 750, "750", "Glomerulonephritis", false },
                    { 751, "751", "Glue ear", false },
                    { 752, "752", "Goitre", false },
                    { 753, "753", "Gonorrhoea", false },
                    { 754, "754", "Gout", false },
                    { 755, "755", "Guillain-Barré syndrome", false },
                    { 756, "756", "Gum disease", false },
                    { 757, "757", "Haemochromatosis", false },
                    { 758, "758", "Haemophilia", false },
                    { 759, "759", "Hair loss", false },
                    { 760, "760", "Hand tendon repair", false },
                    { 761, "761", "Having an operation (surgery)", false },
                    { 762, "762", "Surgery (having an operation)", false },
                    { 763, "763", "Hay fever", false },
                    { 764, "764", "Head lice and nits", false },
                    { 765, "765", "Hearing loss", false },
                    { 766, "766", "Deafness", false },
                    { 767, "767", "Hearing tests", false },
                    { 768, "768", "Heart attack", false },
                    { 769, "769", "Heart block", false },
                    { 770, "770", "Heartburn and acid reflux", false },
                    { 771, "771", "Gastro-oesophageal reflux disease (GORD)", false },
                    { 772, "772", "Heart failure", false },
                    { 773, "773", "Heart transplant", false },
                    { 774, "774", "Heavy periods", false },
                    { 775, "775", "Periods (heavy)", false },
                    { 776, "776", "Hepatitis A", false },
                    { 777, "777", "Hepatitis B", false },
                    { 778, "778", "Hepatitis C", false },
                    { 779, "779", "Hiatus hernia", false },
                    { 780, "780", "Hernia (hiatus)", false },
                    { 781, "781", "Hiccups", false },
                    { 782, "782", "High blood pressure (hypertension)", false },
                    { 783, "783", "Hypertension", false },
                    { 784, "784", "Blood pressure (high)", false },
                    { 785, "785", "High cholesterol", false },
                    { 786, "786", "Cholesterol (high)", false },
                    { 787, "787", "Hip fracture", false },
                    { 788, "788", "Hip replacement", false },
                    { 789, "789", "Excessive hair growth (hirsutism)", false },
                    { 790, "790", "hirsutism", false },
                    { 791, "791", "HIV and AIDS", false },
                    { 792, "792", "Hives", false },
                    { 793, "793", "Hodgkin lymphoma", false },
                    { 794, "794", "Hormone replacement therapy (HRT)", false },
                    { 795, "795", "HRT", false },
                    { 796, "796", "Huntington's disease", false },
                    { 797, "797", "Hydrocephalus", false },
                    { 798, "798", "Hydronephrosis", false },
                    { 799, "799", "Hysterectomy", false },
                    { 800, "800", "Hysteroscopy", false },
                    { 801, "801", "Idiopathic pulmonary fibrosis", false },
                    { 802, "802", "Pulmonary fibrosis", false },
                    { 803, "803", "Ileostomy", false },
                    { 804, "804", "Impetigo", false },
                    { 805, "805", "Infertility", false },
                    { 806, "806", "Inguinal hernia repair", false },
                    { 807, "807", "Hernia (inguinal)", false },
                    { 808, "808", "Insect bites and stings", false },
                    { 809, "809", "Sting or bite (insect)", false },
                    { 810, "810", "Insomnia", false },
                    { 811, "811", "Iron deficiency anaemia", false },
                    { 812, "812", "Anaemia (iron deficiency)", false },
                    { 813, "813", "Irregular periods", false },
                    { 814, "814", "Periods (irregular)", false },
                    { 815, "815", "Irritable bowel syndrome (IBS)", false },
                    { 816, "816", "IBS", false },
                    { 817, "817", "Itchy bottom", false },
                    { 818, "818", "Anus (itchy)", false },
                    { 819, "819", "Itchy skin", false },
                    { 820, "820", "IVF", false },
                    { 821, "821", "Japanese encephalitis", false },
                    { 822, "822", "Jaundice", false },
                    { 823, "823", "Newborn jaundice", false },
                    { 824, "824", "Jaundice in newborns", false },
                    { 825, "825", "Jellyfish and other sea creature stings", false },
                    { 826, "826", "Jet lag", false },
                    { 827, "827", "Joint hypermobility syndrome", false },
                    { 828, "828", "Kawasaki disease", false },
                    { 829, "829", "Kidney cancer", false },
                    { 830, "830", "Chronic kidney disease", false },
                    { 831, "831", "Kidney failure", false },
                    { 832, "832", "Kidney infection", false },
                    { 833, "833", "Kidney stones", false },
                    { 834, "834", "Kidney transplant", false },
                    { 835, "835", "Knee ligament surgery", false },
                    { 836, "836", "Knee replacement", false },
                    { 837, "837", "Kyphosis", false },
                    { 838, "838", "Labyrinthitis and vestibular neuritis", false },
                    { 839, "839", "Vestibular neuritis", false },
                    { 840, "840", "Lactose intolerance", false },
                    { 841, "841", "Laparoscopy (keyhole surgery)", false },
                    { 842, "842", "Laryngeal (larynx) cancer", false },
                    { 843, "843", "Laryngitis", false },
                    { 844, "844", "Laxatives", false },
                    { 845, "845", "Lazy eye", false },
                    { 846, "846", "Amblyopia", false },
                    { 847, "847", "Leg cramps", false },
                    { 848, "848", "Venous leg ulcer", false },
                    { 849, "849", "Leg ulcer", false },
                    { 850, "850", "Leptospirosis (Weil's disease)", false },
                    { 851, "851", "Weil's disease", false },
                    { 852, "852", "Leukoplakia", false },
                    { 853, "853", "Lichen planus", false },
                    { 854, "854", "Listeriosis", false },
                    { 855, "855", "Liver cancer", false },
                    { 856, "856", "Liver transplant", false },
                    { 857, "857", "Long-sightedness", false },
                    { 858, "858", "Low blood pressure (hypotension)", false },
                    { 859, "859", "Blood pressure (low)", false },
                    { 860, "860", "Hypotension", false },
                    { 861, "861", "Lumbar decompression surgery", false },
                    { 862, "862", "Lumbar puncture", false },
                    { 863, "863", "Lung cancer", false },
                    { 864, "864", "Lung transplant", false },
                    { 865, "865", "Lupus", false },
                    { 866, "866", "Lymphoedema", false },
                    { 867, "867", "Age-related macular degeneration (AMD)", false },
                    { 868, "868", "Macular degeneration (age-related)", false },
                    { 869, "869", "Malaria", false },
                    { 870, "870", "Malignant brain tumour (brain cancer)", false },
                    { 871, "871", "Brain tumour (malignant)", false },
                    { 872, "872", "Malnutrition", false },
                    { 873, "873", "Marfan syndrome", false },
                    { 874, "874", "Mastectomy", false },
                    { 875, "875", "Mastitis", false },
                    { 876, "876", "Mastocytosis", false },
                    { 877, "877", "Measles", false },
                    { 878, "878", "Medicines information", false },
                    { 879, "879", "Skin cancer (melanoma)", false },
                    { 880, "880", "Meningitis", false },
                    { 881, "881", "Menopause", false },
                    { 882, "882", "Migraine", false },
                    { 883, "883", "Head injury and concussion", false },
                    { 884, "884", "Miscarriage", false },
                    { 885, "885", "Moles", false },
                    { 886, "886", "Molluscum contagiosum", false },
                    { 887, "887", "Motor neurone disease", false },
                    { 888, "888", "Mouth cancer", false },
                    { 889, "889", "Tongue cancer", false },
                    { 890, "890", "MRI scan", false },
                    { 891, "891", "Mucositis", false },
                    { 892, "892", "Multiple myeloma", false },
                    { 893, "893", "Myeloma", false },
                    { 894, "894", "Multiple sclerosis", false },
                    { 895, "895", "Mumps", false },
                    { 896, "896", "Munchausen's syndrome", false },
                    { 897, "897", "Muscular dystrophy", false },
                    { 898, "898", "Myasthenia gravis", false },
                    { 899, "899", "Narcolepsy", false },
                    { 900, "900", "Nasal polyps", false },
                    { 901, "901", "Newborn respiratory distress syndrome", false },
                    { 902, "902", "Neurofibromatosis type 1", false },
                    { 903, "903", "Neurofibromatosis type 2", false },
                    { 904, "904", "Non-allergic rhinitis", false },
                    { 905, "905", "Non-gonococcal urethritis", false },
                    { 906, "906", "Urethritis (NGU)", false },
                    { 907, "907", "Non-Hodgkin lymphoma", false },
                    { 908, "908", "Skin cancer (non-melanoma)", false },
                    { 909, "909", "Squamous cell carcinoma", false },
                    { 910, "910", "Basal cell carcinoma", false },
                    { 911, "911", "Noonan syndrome", false },
                    { 912, "912", "Nosebleed", false },
                    { 913, "913", "Obesity", false },
                    { 914, "914", "Obsessive compulsive disorder (OCD)", false },
                    { 915, "915", "Occupational therapy", false },
                    { 916, "916", "Oesophageal cancer", false },
                    { 917, "917", "Orthodontics", false },
                    { 918, "918", "Osteoarthritis", false },
                    { 919, "919", "Osteomyelitis", false },
                    { 920, "920", "Osteopathy", false },
                    { 921, "921", "Osteoporosis", false },
                    { 922, "922", "Ovarian cancer", false },
                    { 923, "923", "Ovarian cyst", false },
                    { 924, "924", "Overactive thyroid (hyperthyroidism)", false },
                    { 925, "925", "Hyperthyroidism", false },
                    { 926, "926", "Pacemaker implantation", false },
                    { 927, "927", "Paget's disease of bone", false },
                    { 928, "928", "Paget's disease of the nipple", false },
                    { 929, "929", "Pancreas transplant", false },
                    { 930, "930", "Pancreatic cancer", false },
                    { 931, "931", "Paralysis", false },
                    { 932, "932", "Parkinson's disease", false },
                    { 933, "933", "Pelvic inflammatory disease", false },
                    { 934, "934", "Pelvic organ prolapse", false },
                    { 935, "935", "Prolapse (pelvic organ)", false },
                    { 936, "936", "Pemphigus vulgaris", false },
                    { 937, "937", "Perforated eardrum", false },
                    { 938, "938", "Eardrum (burst)", false },
                    { 939, "939", "Pericarditis", false },
                    { 940, "940", "Peripheral arterial disease (PAD)", false },
                    { 941, "941", "Peripheral neuropathy", false },
                    { 942, "942", "Peritonitis", false },
                    { 943, "943", "Phobias", false },
                    { 944, "944", "Physiotherapy", false },
                    { 945, "945", "Piles (haemorrhoids)", false },
                    { 946, "946", "Piles", false },
                    { 947, "947", "Pilonidal sinus", false },
                    { 948, "948", "Plastic surgery", false },
                    { 949, "949", "Pneumonia", false },
                    { 950, "950", "Poisoning", false },
                    { 951, "951", "Polycystic ovary syndrome", false },
                    { 952, "952", "Polycythaemia", false },
                    { 953, "953", "Polymyalgia rheumatica", false },
                    { 954, "954", "Post-herpetic neuralgia", false },
                    { 955, "955", "Postnatal depression", false },
                    { 956, "956", "Post-polio syndrome", false },
                    { 957, "957", "Post-traumatic stress disorder (PTSD)", false },
                    { 958, "958", "Prader-Willi syndrome", false },
                    { 959, "959", "Pre-eclampsia", false },
                    { 960, "960", "PMS (premenstrual syndrome)", false },
                    { 961, "961", "Pressure ulcers (pressure sores)", false },
                    { 962, "962", "Priapism (painful erections)", false },
                    { 963, "963", "Primary biliary cholangitis (primary biliary cirrhosis)", false },
                    { 964, "964", "Progressive supranuclear palsy", false },
                    { 965, "965", "Prostate cancer", false },
                    { 966, "966", "Benign prostate enlargement", false },
                    { 967, "967", "Prostate enlargement", false },
                    { 968, "968", "Psoriasis", false },
                    { 969, "969", "Psychosis", false },
                    { 970, "970", "Pulmonary embolism", false },
                    { 971, "971", "Pulmonary hypertension", false },
                    { 972, "972", "Rabies", false },
                    { 973, "973", "Radiotherapy", false },
                    { 974, "974", "Raynaud's", false },
                    { 975, "975", "Reactive arthritis", false },
                    { 976, "976", "Rectal examination", false },
                    { 977, "977", "Repetitive strain injury (RSI)", false },
                    { 978, "978", "Restless legs syndrome", false },
                    { 979, "979", "Restricted growth (dwarfism)", false },
                    { 980, "980", "Dwarfism", false },
                    { 981, "981", "Detached retina (retinal detachment)", false },
                    { 982, "982", "Retinal detachment", false },
                    { 983, "983", "Rhesus disease", false },
                    { 984, "984", "Rheumatic fever", false },
                    { 985, "985", "Rheumatoid arthritis", false },
                    { 986, "986", "Rickets and osteomalacia", false },
                    { 987, "987", "Osteomalacia", false },
                    { 988, "988", "Root canal treatment", false },
                    { 989, "989", "Rosacea", false },
                    { 990, "990", "Rubella (german measles)", false },
                    { 991, "991", "Scabies", false },
                    { 992, "992", "Scars", false },
                    { 993, "993", "Schizophrenia", false },
                    { 994, "994", "Sciatica", false },
                    { 995, "995", "Buttock pain", false },
                    { 996, "996", "Scoliosis", false },
                    { 997, "997", "Scurvy", false },
                    { 998, "998", "Seasonal affective disorder (SAD)", false },
                    { 999, "999", "Self-harm", false },
                    { 1000, "1000", "Sepsis", false },
                    { 1001, "1001", "Severe head injury", false },
                    { 1002, "1002", "Shingles", false },
                    { 1003, "1003", "Short-sightedness (myopia)", false },
                    { 1004, "1004", "Myopia", false },
                    { 1005, "1005", "Shoulder pain", false },
                    { 1006, "1006", "Sickle cell disease", false },
                    { 1007, "1007", "Sjögren's syndrome", false },
                    { 1008, "1008", "Sleep paralysis", false },
                    { 1009, "1009", "Slipped disc", false },
                    { 1010, "1010", "Small bowel transplant", false },
                    { 1011, "1011", "Bowel transplant", false },
                    { 1012, "1012", "Snoring", false },
                    { 1013, "1013", "Spina bifida", false },
                    { 1014, "1014", "Spinal muscular atrophy", false },
                    { 1015, "1015", "Sports injuries", false },
                    { 1016, "1016", "Sprains and strains", false },
                    { 1017, "1017", "Squint", false },
                    { 1018, "1018", "Selective serotonin reuptake inhibitors (SSRIs)", false },
                    { 1019, "1019", "Stammering", false },
                    { 1020, "1020", "Stuttering", false },
                    { 1021, "1021", "Statins", false },
                    { 1022, "1022", "Stem cell and bone marrow transplants", false },
                    { 1023, "1023", "Stillbirth", false },
                    { 1024, "1024", "Stomach cancer", false },
                    { 1025, "1025", "Stomach ulcer", false },
                    { 1026, "1026", "Stroke", false },
                    { 1027, "1027", "Subarachnoid haemorrhage", false },
                    { 1028, "1028", "Brain haemorrhage", false },
                    { 1029, "1029", "Subdural haematoma", false },
                    { 1030, "1030", "Help for suicidal thoughts", false },
                    { 1031, "1031", "Suicidal thoughts", false },
                    { 1032, "1032", "Supraventricular tachycardia (SVT)", false },
                    { 1033, "1033", "Dysphagia (swallowing problems)", false },
                    { 1034, "1034", "Swallowing problems", false },
                    { 1035, "1035", "Syphilis", false },
                    { 1036, "1036", "Coccydynia (tailbone pain)", false },
                    { 1037, "1037", "Tay-Sachs disease", false },
                    { 1038, "1038", "Teeth grinding (bruxism)", false },
                    { 1039, "1039", "Bruxism", false }
                });

            migrationBuilder.InsertData(
                table: "SysRefHealthCondition",
                columns: new[] { "Id", "Code", "Description", "IsDeleted" },
                values: new object[,]
                {
                    { 1040, "1040", "Tendonitis", false },
                    { 1041, "1041", "Tennis elbow", false },
                    { 1042, "1042", "Testicle lumps and swellings", false },
                    { 1043, "1043", "Testicular cancer", false },
                    { 1044, "1044", "Thalassaemia", false },
                    { 1045, "1045", "Threadworms", false },
                    { 1046, "1046", "Thyroid cancer", false },
                    { 1047, "1047", "Tick-borne encephalitis (TBE)", false },
                    { 1048, "1048", "Tics", false },
                    { 1049, "1049", "Tinnitus", false },
                    { 1050, "1050", "Tonsillitis", false },
                    { 1051, "1051", "Quinsy", false },
                    { 1052, "1052", "Tourette's syndrome", false },
                    { 1053, "1053", "Toxocariasis", false },
                    { 1054, "1054", "Toxoplasmosis", false },
                    { 1055, "1055", "Tracheostomy", false },
                    { 1056, "1056", "Transient ischaemic attack (TIA)", false },
                    { 1057, "1057", "TIA", false },
                    { 1058, "1058", "Transurethral resection of the prostate (TURP)", false },
                    { 1059, "1059", "Travel vaccinations", false },
                    { 1060, "1060", "Trichomoniasis", false },
                    { 1061, "1061", "Trichotillomania (hair pulling disorder)", false },
                    { 1062, "1062", "Trigeminal neuralgia", false },
                    { 1063, "1063", "Trigger finger", false },
                    { 1064, "1064", "Tuberculosis (TB)", false },
                    { 1065, "1065", "Tuberous sclerosis", false },
                    { 1066, "1066", "Turner syndrome", false },
                    { 1067, "1067", "Type 2 diabetes", false },
                    { 1068, "1068", "Diabetes (type 2)", false },
                    { 1069, "1069", "Typhoid fever", false },
                    { 1070, "1070", "Ulcerative colitis", false },
                    { 1071, "1071", "Umbilical hernia repair", false },
                    { 1072, "1072", "Hernia (umbilical)", false },
                    { 1073, "1073", "Underactive thyroid (hypothyroidism)", false },
                    { 1074, "1074", "Hypothyroidism", false },
                    { 1075, "1075", "Undescended testicles", false },
                    { 1076, "1076", "Urinary catheter", false },
                    { 1077, "1077", "Urinary incontinence", false },
                    { 1078, "1078", "Incontinence (urinary)", false },
                    { 1079, "1079", "Uveitis", false },
                    { 1080, "1080", "Vaginal cancer", false },
                    { 1081, "1081", "Vaginismus", false },
                    { 1082, "1082", "Varicose eczema", false },
                    { 1083, "1083", "Eczema (varicose)", false },
                    { 1084, "1084", "Varicose veins", false },
                    { 1085, "1085", "Vascular dementia", false },
                    { 1086, "1086", "Dementia (vascular)", false },
                    { 1087, "1087", "Vertigo", false },
                    { 1088, "1088", "Vitamin B12 or folate deficiency anaemia", false },
                    { 1089, "1089", "Anaemia (vitamin B12 or folate deficiency)", false },
                    { 1090, "1090", "Vitamins and minerals", false },
                    { 1091, "1091", "Vitiligo", false },
                    { 1092, "1092", "Vulval cancer", false },
                    { 1093, "1093", "Watering eyes", false },
                    { 1094, "1094", "Weight loss surgery", false },
                    { 1095, "1095", "Whiplash", false },
                    { 1096, "1096", "Wisdom tooth removal", false },
                    { 1097, "1097", "Womb (uterus) cancer", false },
                    { 1098, "1098", "Uterine (womb) cancer", false },
                    { 1099, "1099", "Endometrial cancer", false },
                    { 1100, "1100", "Yellow fever", false },
                    { 1101, "1101", "Abortion", false },
                    { 1102, "1102", "Bird flu", false },
                    { 1103, "1103", "Avian flu", false },
                    { 1104, "1104", "Antifungal medicines", false },
                    { 1105, "1105", "Blood transfusion", false },
                    { 1106, "1106", "Ringworm", false },
                    { 1107, "1107", "Early menopause", false },
                    { 1108, "1108", "Menopause (early)", false },
                    { 1109, "1109", "Floaters and flashes in the eyes", false },
                    { 1110, "1110", "Eye floaters", false },
                    { 1111, "1111", "Your contraception guide", false },
                    { 1112, "1112", "Dementia guide", false },
                    { 1113, "1113", "Fitness Studio exercise videos", false },
                    { 1114, "1114", "NHS Health Check", false },
                    { 1115, "1115", "Worms in humans", false },
                    { 1116, "1116", "Hookworm", false },
                    { 1117, "1117", "Tapeworm", false },
                    { 1118, "1118", "Roundworm", false },
                    { 1119, "1119", "Tremor or shaking hands", false },
                    { 1120, "1120", "tremor", false },
                    { 1121, "1121", "essential tremor", false },
                    { 1122, "1122", "shaking", false },
                    { 1123, "1123", "Low white blood cell count", false },
                    { 1124, "1124", "White blood cell count (low)", false },
                    { 1125, "1125", "Bunions", false },
                    { 1126, "1126", "Lost or changed sense of smell", false },
                    { 1127, "1127", "Anosmia", false },
                    { 1128, "1128", "Sense of smell (lost/changed)", false },
                    { 1129, "1129", "Soiling (child pooing their pants)", false },
                    { 1130, "1130", "Oral thrush (mouth thrush)", false },
                    { 1131, "1131", "Mouth thrush", false },
                    { 1132, "1132", "Temporal arteritis", false },
                    { 1133, "1133", "Giant cell arteritis", false },
                    { 1134, "1134", "Memory loss (amnesia)", false },
                    { 1135, "1135", "Amnesia", false },
                    { 1136, "1136", "Headaches", false },
                    { 1137, "1137", "Sinusitis (sinus infection)", false },
                    { 1138, "1138", "Sinusitis", false },
                    { 1139, "1139", "Thrush in men and women", false },
                    { 1140, "1140", "Cold sores", false },
                    { 1141, "1141", "Group B strep", false },
                    { 1142, "1142", "Skin picking disorder", false },
                    { 1143, "1143", "Hyperparathyroidism", false },
                    { 1144, "1144", "Hypoparathyroidism", false },
                    { 1145, "1145", "Ear infections", false },
                    { 1146, "1146", "Twitching eyes and muscles", false },
                    { 1147, "1147", "Learning disabilities", false },
                    { 1148, "1148", "NHS screening", false },
                    { 1149, "1149", "Hormone headaches", false },
                    { 1150, "1150", "Headaches (hormone)", false },
                    { 1151, "1151", "What to do if someone has a seizure (fit)", false },
                    { 1152, "1152", "Seizures (fits)", false },
                    { 1153, "1153", "Fits (seizures)", false },
                    { 1154, "1154", "Complementary and alternative medicine", false },
                    { 1155, "1155", "End of life care", false },
                    { 1156, "1156", "Knocked-out tooth", false },
                    { 1157, "1157", "Tooth knocked out", false },
                    { 1158, "1158", "Chipped broken or cracked tooth", false },
                    { 1159, "1159", "Tooth (chipped or broken)", false },
                    { 1160, "1160", "Diarrhoea and vomiting", false },
                    { 1161, "1161", "Tummy bug", false },
                    { 1162, "1162", "Vomiting", false },
                    { 1163, "1163", "Stomach bug", false },
                    { 1164, "1164", "Gastroenteritis", false },
                    { 1165, "1165", "Diarrhoea", false },
                    { 1166, "1166", "Being sick", false },
                    { 1167, "1167", "Bullous pemphigoid", false },
                    { 1168, "1168", "Feeling sick (nausea)", false },
                    { 1169, "1169", "Nausea", false },
                    { 1170, "1170", "Type 1 diabetes", false },
                    { 1171, "1171", "Diabetes (type 1)", false },
                    { 1172, "1172", "Social care and support guide", false },
                    { 1173, "1173", "Middle East respiratory syndrome (MERS)", false },
                    { 1174, "1174", "Monkeypox", false },
                    { 1175, "1175", "Diabetic eye screening 2", false },
                    { 1176, "1176", "Medical cannabis (and cannabis oils)", false },
                    { 1177, "1177", "Cannabis oil (medical cannabis)", false },
                    { 1178, "1178", "Diabetic eye screening 1", false },
                    { 1179, "1179", "Body odour (BO)", false },
                    { 1180, "1180", "Human papillomavirus (HPV)", false },
                    { 1181, "1181", "Plantar fasciitis", false },
                    { 1182, "1182", "Foot pain", false },
                    { 1183, "1183", "heel pain", false },
                    { 1184, "1184", "Toe pain", false },
                    { 1185, "1185", "ankle pain", false },
                    { 1186, "1186", "Autism", false },
                    { 1187, "1187", "Asperger's", false },
                    { 1188, "1188", "Cosmetic procedures", false },
                    { 1189, "1189", "Hand pain", false },
                    { 1190, "1190", "Swollen arms and hands (oedema)", false },
                    { 1191, "1191", "Colonoscopy", false },
                    { 1192, "1192", "Genetic and genomic testing", false },
                    { 1193, "1193", "Sleep apnoea", false },
                    { 1194, "1194", "Vaccinations", false },
                    { 1195, "1195", "Mental health and wellbeing", false },
                    { 1196, "1196", "High temperature (fever) in adults", false },
                    { 1197, "1197", "Fever in adults", false },
                    { 1198, "1198", "Coronavirus (COVID-19)", false },
                    { 1199, "1199", "Baby", false },
                    { 1200, "1200", "Do not attempt cardiopulmonary resuscitation (DNACPR) decisions", false },
                    { 1201, "1201", "Testicle pain", false },
                    { 1202, "1202", "Pain in testicles", false },
                    { 1203, "1203", "Hearing aids and implants", false },
                    { 1204, "1204", "Breast screening (mammogram)", false },
                    { 1205, "1205", "Smelly feet", false },
                    { 1206, "1206", "Academic attainment", false },
                    { 1207, "1207", "Ageing", false },
                    { 1208, "1208", "Aggression", false },
                    { 1209, "1209", "Antenatal care", false },
                    { 1210, "1210", "Blood donation", false },
                    { 1211, "1211", "Body image", false },
                    { 1212, "1212", "Breastfeeding", false },
                    { 1213, "1213", "Care homes", false },
                    { 1214, "1214", "Carers", false },
                    { 1215, "1215", "Child development", false },
                    { 1216, "1216", "Complementary therapies", false },
                    { 1217, "1217", "Contraception", false },
                    { 1218, "1218", "Domestic violence", false },
                    { 1219, "1219", "Eating well", false },
                    { 1220, "1220", "Exercise and sports", false },
                    { 1221, "1221", "General wellbeing", false },
                    { 1222, "1222", "Genetic screening", false },
                    { 1223, "1223", "Healthy volunteers", false },
                    { 1224, "1224", "Improving care and services", false },
                    { 1225, "1225", "Healthy lifestyle", false },
                    { 1226, "1226", "Long COVID", false },
                    { 1227, "1227", "Obesity risk", false },
                    { 1228, "1228", "Occupational health", false },
                    { 1229, "1229", "Parenting", false },
                    { 1230, "1230", "Public health", false },
                    { 1231, "1231", "Sleeping well", false },
                    { 1232, "1232", "Smoking", false },
                    { 1233, "1233", "Supplements", false }
                });

            migrationBuilder.InsertData(
                table: "SysRefIdentifierType",
                columns: new[] { "Id", "Code", "Description", "IsDeleted" },
                values: new object[,]
                {
                    { 1, "1", "ParticipantId", false },
                    { 2, "2", "NhsId", false }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SysRefCommunicationLanguage",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SysRefCommunicationLanguage",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SysRefDailyLifeImpact",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SysRefDailyLifeImpact",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SysRefDailyLifeImpact",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "SysRefDailyLifeImpact",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "SysRefGender",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SysRefGender",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SysRefGender",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 79);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 80);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 81);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 82);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 83);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 84);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 85);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 86);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 87);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 88);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 89);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 90);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 91);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 92);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 93);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 94);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 95);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 96);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 97);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 98);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 99);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 106);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 107);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 108);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 109);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 110);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 111);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 112);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 113);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 114);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 115);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 116);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 117);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 118);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 119);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 120);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 121);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 122);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 123);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 124);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 125);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 126);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 127);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 128);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 129);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 130);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 131);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 132);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 133);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 134);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 135);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 136);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 137);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 138);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 139);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 140);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 141);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 142);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 143);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 144);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 145);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 146);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 147);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 148);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 149);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 150);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 151);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 152);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 153);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 154);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 155);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 156);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 157);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 158);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 159);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 160);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 161);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 162);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 163);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 164);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 165);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 166);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 167);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 168);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 169);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 170);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 171);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 172);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 173);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 174);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 175);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 176);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 177);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 178);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 179);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 180);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 181);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 182);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 183);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 184);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 185);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 186);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 187);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 188);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 189);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 190);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 191);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 192);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 193);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 194);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 195);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 196);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 197);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 198);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 199);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 200);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 201);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 202);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 203);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 204);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 205);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 206);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 207);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 208);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 209);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 210);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 211);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 212);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 213);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 214);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 215);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 216);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 217);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 218);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 219);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 220);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 221);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 222);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 223);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 224);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 225);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 226);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 227);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 228);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 229);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 230);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 231);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 232);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 233);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 234);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 235);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 236);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 237);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 238);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 239);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 240);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 241);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 242);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 243);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 244);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 245);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 246);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 247);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 248);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 249);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 250);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 251);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 252);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 253);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 254);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 255);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 256);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 257);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 258);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 259);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 260);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 261);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 262);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 263);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 264);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 265);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 266);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 267);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 268);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 269);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 270);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 271);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 272);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 273);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 274);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 275);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 276);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 277);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 278);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 279);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 280);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 281);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 282);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 283);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 284);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 285);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 286);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 287);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 288);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 289);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 290);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 291);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 292);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 293);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 294);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 295);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 296);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 297);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 298);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 299);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 300);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 301);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 302);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 303);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 304);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 305);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 306);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 307);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 308);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 309);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 310);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 311);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 312);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 313);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 314);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 315);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 316);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 317);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 318);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 319);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 320);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 321);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 322);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 323);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 324);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 325);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 326);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 327);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 328);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 329);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 330);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 331);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 332);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 333);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 334);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 335);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 336);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 337);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 338);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 339);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 340);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 341);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 342);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 343);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 344);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 345);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 346);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 347);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 348);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 349);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 350);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 351);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 352);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 353);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 354);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 355);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 356);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 357);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 358);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 359);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 360);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 361);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 362);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 363);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 364);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 365);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 366);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 367);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 368);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 369);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 370);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 371);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 372);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 373);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 374);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 375);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 376);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 377);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 378);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 379);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 380);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 381);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 382);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 383);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 384);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 385);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 386);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 387);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 388);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 389);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 390);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 391);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 392);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 393);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 394);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 395);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 396);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 397);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 398);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 399);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 400);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 401);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 402);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 403);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 404);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 405);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 406);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 407);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 408);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 409);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 410);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 411);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 412);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 413);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 414);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 415);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 416);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 417);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 418);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 419);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 420);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 421);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 422);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 423);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 424);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 425);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 426);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 427);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 428);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 429);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 430);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 431);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 432);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 433);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 434);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 435);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 436);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 437);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 438);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 439);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 440);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 441);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 442);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 443);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 444);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 445);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 446);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 447);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 448);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 449);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 450);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 451);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 452);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 453);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 454);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 455);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 456);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 457);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 458);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 459);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 460);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 461);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 462);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 463);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 464);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 465);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 466);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 467);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 468);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 469);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 470);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 471);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 472);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 473);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 474);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 475);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 476);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 477);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 478);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 479);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 480);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 481);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 482);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 483);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 484);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 485);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 486);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 487);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 488);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 489);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 490);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 491);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 492);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 493);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 494);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 495);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 496);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 497);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 498);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 499);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 500);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 501);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 502);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 503);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 504);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 505);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 506);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 507);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 508);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 509);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 510);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 511);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 512);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 513);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 514);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 515);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 516);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 517);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 518);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 519);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 520);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 521);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 522);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 523);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 524);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 525);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 526);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 527);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 528);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 529);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 530);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 531);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 532);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 533);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 534);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 535);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 536);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 537);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 538);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 539);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 540);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 541);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 542);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 543);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 544);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 545);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 546);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 547);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 548);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 549);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 550);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 551);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 552);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 553);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 554);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 555);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 556);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 557);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 558);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 559);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 560);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 561);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 562);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 563);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 564);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 565);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 566);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 567);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 568);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 569);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 570);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 571);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 572);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 573);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 574);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 575);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 576);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 577);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 578);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 579);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 580);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 581);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 582);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 583);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 584);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 585);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 586);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 587);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 588);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 589);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 590);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 591);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 592);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 593);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 594);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 595);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 596);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 597);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 598);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 599);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 600);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 601);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 602);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 603);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 604);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 605);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 606);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 607);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 608);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 609);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 610);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 611);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 612);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 613);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 614);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 615);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 616);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 617);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 618);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 619);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 620);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 621);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 622);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 623);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 624);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 625);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 626);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 627);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 628);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 629);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 630);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 631);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 632);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 633);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 634);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 635);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 636);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 637);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 638);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 639);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 640);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 641);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 642);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 643);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 644);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 645);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 646);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 647);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 648);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 649);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 650);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 651);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 652);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 653);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 654);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 655);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 656);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 657);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 658);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 659);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 660);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 661);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 662);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 663);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 664);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 665);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 666);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 667);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 668);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 669);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 670);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 671);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 672);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 673);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 674);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 675);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 676);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 677);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 678);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 679);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 680);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 681);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 682);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 683);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 684);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 685);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 686);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 687);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 688);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 689);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 690);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 691);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 692);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 693);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 694);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 695);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 696);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 697);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 698);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 699);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 700);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 701);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 702);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 703);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 704);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 705);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 706);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 707);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 708);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 709);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 710);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 711);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 712);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 713);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 714);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 715);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 716);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 717);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 718);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 719);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 720);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 721);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 722);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 723);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 724);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 725);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 726);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 727);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 728);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 729);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 730);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 731);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 732);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 733);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 734);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 735);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 736);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 737);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 738);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 739);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 740);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 741);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 742);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 743);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 744);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 745);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 746);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 747);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 748);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 749);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 750);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 751);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 752);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 753);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 754);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 755);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 756);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 757);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 758);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 759);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 760);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 761);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 762);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 763);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 764);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 765);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 766);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 767);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 768);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 769);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 770);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 771);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 772);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 773);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 774);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 775);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 776);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 777);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 778);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 779);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 780);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 781);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 782);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 783);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 784);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 785);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 786);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 787);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 788);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 789);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 790);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 791);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 792);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 793);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 794);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 795);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 796);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 797);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 798);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 799);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 800);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 801);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 802);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 803);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 804);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 805);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 806);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 807);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 808);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 809);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 810);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 811);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 812);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 813);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 814);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 815);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 816);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 817);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 818);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 819);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 820);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 821);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 822);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 823);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 824);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 825);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 826);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 827);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 828);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 829);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 830);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 831);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 832);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 833);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 834);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 835);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 836);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 837);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 838);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 839);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 840);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 841);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 842);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 843);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 844);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 845);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 846);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 847);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 848);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 849);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 850);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 851);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 852);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 853);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 854);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 855);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 856);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 857);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 858);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 859);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 860);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 861);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 862);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 863);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 864);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 865);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 866);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 867);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 868);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 869);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 870);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 871);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 872);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 873);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 874);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 875);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 876);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 877);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 878);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 879);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 880);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 881);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 882);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 883);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 884);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 885);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 886);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 887);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 888);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 889);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 890);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 891);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 892);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 893);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 894);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 895);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 896);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 897);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 898);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 899);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 900);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 901);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 902);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 903);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 904);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 905);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 906);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 907);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 908);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 909);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 910);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 911);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 912);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 913);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 914);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 915);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 916);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 917);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 918);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 919);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 920);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 921);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 922);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 923);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 924);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 925);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 926);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 927);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 928);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 929);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 930);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 931);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 932);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 933);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 934);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 935);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 936);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 937);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 938);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 939);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 940);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 941);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 942);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 943);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 944);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 945);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 946);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 947);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 948);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 949);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 950);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 951);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 952);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 953);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 954);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 955);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 956);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 957);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 958);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 959);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 960);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 961);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 962);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 963);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 964);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 965);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 966);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 967);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 968);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 969);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 970);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 971);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 972);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 973);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 974);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 975);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 976);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 977);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 978);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 979);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 980);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 981);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 982);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 983);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 984);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 985);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 986);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 987);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 988);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 989);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 990);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 991);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 992);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 993);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 994);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 995);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 996);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 997);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 998);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 999);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1000);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1001);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1002);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1003);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1004);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1005);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1006);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1007);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1008);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1009);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1010);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1011);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1012);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1013);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1014);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1015);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1016);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1017);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1018);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1019);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1020);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1021);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1022);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1023);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1024);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1025);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1026);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1027);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1028);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1029);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1030);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1031);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1032);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1033);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1034);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1035);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1036);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1037);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1038);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1039);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1040);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1041);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1042);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1043);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1044);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1045);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1046);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1047);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1048);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1049);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1050);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1051);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1052);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1053);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1054);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1055);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1056);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1057);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1058);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1059);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1060);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1061);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1062);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1063);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1064);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1065);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1066);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1067);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1068);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1069);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1070);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1071);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1072);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1073);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1074);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1075);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1076);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1077);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1078);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1079);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1080);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1081);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1082);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1083);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1084);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1085);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1086);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1087);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1088);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1089);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1090);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1091);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1092);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1093);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1094);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1095);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1096);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1097);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1098);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1099);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1100);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1101);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1102);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1103);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1104);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1105);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1106);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1107);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1108);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1109);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1110);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1111);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1112);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1113);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1114);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1115);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1116);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1117);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1118);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1119);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1120);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1121);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1122);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1123);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1124);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1125);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1126);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1127);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1128);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1129);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1130);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1131);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1132);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1133);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1134);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1135);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1136);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1137);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1138);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1139);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1140);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1141);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1142);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1143);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1144);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1145);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1146);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1147);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1148);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1149);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1150);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1151);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1152);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1153);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1154);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1155);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1156);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1157);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1158);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1159);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1160);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1161);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1162);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1163);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1164);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1165);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1166);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1167);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1168);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1169);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1170);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1171);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1172);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1173);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1174);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1175);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1176);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1177);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1178);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1179);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1180);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1181);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1182);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1183);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1184);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1185);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1186);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1187);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1188);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1189);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1190);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1191);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1192);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1193);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1194);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1195);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1196);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1197);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1198);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1199);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1200);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1201);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1202);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1203);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1204);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1205);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1206);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1207);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1208);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1209);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1210);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1211);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1212);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1213);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1214);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1215);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1216);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1217);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1218);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1219);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1220);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1221);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1222);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1223);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1224);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1225);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1226);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1227);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1228);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1229);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1230);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1231);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1232);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1233);

            migrationBuilder.DeleteData(
                table: "SysRefIdentifierType",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SysRefIdentifierType",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
