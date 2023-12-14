using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dynamo.Stream.Handler.Migrations
{
    public partial class updatetousecode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "SysRefCommunicationLanguage",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Code", "Description" },
                values: new object[] { "en-GB", "English" });

            migrationBuilder.UpdateData(
                table: "SysRefCommunicationLanguage",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Code", "Description" },
                values: new object[] { "cy-GB", "Welsh" });

            migrationBuilder.UpdateData(
                table: "SysRefDailyLifeImpact",
                keyColumn: "Id",
                keyValue: 1,
                column: "Code",
                value: "Yes, a lot");

            migrationBuilder.UpdateData(
                table: "SysRefDailyLifeImpact",
                keyColumn: "Id",
                keyValue: 2,
                column: "Code",
                value: "Yes, a little");

            migrationBuilder.UpdateData(
                table: "SysRefDailyLifeImpact",
                keyColumn: "Id",
                keyValue: 3,
                column: "Code",
                value: "Not at all");

            migrationBuilder.UpdateData(
                table: "SysRefDailyLifeImpact",
                keyColumn: "Id",
                keyValue: 4,
                column: "Code",
                value: "Prefer not to say");

            migrationBuilder.UpdateData(
                table: "SysRefGender",
                keyColumn: "Id",
                keyValue: 1,
                column: "Code",
                value: "Male");

            migrationBuilder.UpdateData(
                table: "SysRefGender",
                keyColumn: "Id",
                keyValue: 2,
                column: "Code",
                value: "Female");

            migrationBuilder.UpdateData(
                table: "SysRefGender",
                keyColumn: "Id",
                keyValue: 3,
                column: "Code",
                value: "Prefer Not to Say");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1,
                column: "Code",
                value: "Acanthosis nigricans");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 2,
                column: "Code",
                value: "Achalasia");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 3,
                column: "Code",
                value: "Acid and chemical burns");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 4,
                column: "Code",
                value: "Acoustic neuroma (vestibular schwannoma)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 5,
                column: "Code",
                value: "Vestibular schwannoma");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 6,
                column: "Code",
                value: "Acromegaly");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 7,
                column: "Code",
                value: "Gigantism");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 8,
                column: "Code",
                value: "Urine albumin to creatinine ratio (ACR)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 9,
                column: "Code",
                value: "Actinic keratoses (solar keratoses)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 10,
                column: "Code",
                value: "Solar keratoses");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 11,
                column: "Code",
                value: "Acupuncture");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 12,
                column: "Code",
                value: "Acute cholecystitis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 13,
                column: "Code",
                value: "Gallbladder pain");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 14,
                column: "Code",
                value: "Cholecystitis (acute)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 15,
                column: "Code",
                value: "MND");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 16,
                column: "Code",
                value: "Acute kidney injury");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 17,
                column: "Code",
                value: "Acute respiratory distress syndrome");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 18,
                column: "Code",
                value: "Adenoidectomy");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 19,
                column: "Code",
                value: "Air or gas embolism");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 20,
                column: "Code",
                value: "Decompression sickness");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 21,
                column: "Code",
                value: "Alcohol poisoning");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 22,
                column: "Code",
                value: "Alexander technique");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 23,
                column: "Code",
                value: "Alkaptonuria");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 24,
                column: "Code",
                value: "Amputation");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 25,
                column: "Code",
                value: "Amyloidosis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 26,
                column: "Code",
                value: "Anabolic steroid misuse");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 27,
                column: "Code",
                value: "Steroid misuse");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 28,
                column: "Code",
                value: "Anaesthesia");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 29,
                column: "Code",
                value: "Anal cancer");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 30,
                column: "Code",
                value: "Anal pain");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 31,
                column: "Code",
                value: "Proctalgia");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 32,
                column: "Code",
                value: "Angelman syndrome");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 33,
                column: "Code",
                value: "Animal and human bites");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 34,
                column: "Code",
                value: "Bite (animal or human)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 35,
                column: "Code",
                value: "Anosmia");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 36,
                column: "Code",
                value: "Antacids");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 37,
                column: "Code",
                value: "Antihistamines");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 38,
                column: "Code",
                value: "Antisocial personality disorder");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 39,
                column: "Code",
                value: "Anxiety disorders in children");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 40,
                column: "Code",
                value: "Arrhythmia");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 41,
                column: "Code",
                value: "Heart rhythm problems");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 42,
                column: "Code",
                value: "Arterial thrombosis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 43,
                column: "Code",
                value: "Intrauterine insemination (IUI)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 44,
                column: "Code",
                value: "Asbestosis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 45,
                column: "Code",
                value: "Aspirin");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 46,
                column: "Code",
                value: "Atherosclerosis (arteriosclerosis)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 47,
                column: "Code",
                value: "Athlete's foot");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 48,
                column: "Code",
                value: "Auditory processing disorder (APD)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 49,
                column: "Code",
                value: "Balanitis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 50,
                column: "Code",
                value: "Barium enema");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 51,
                column: "Code",
                value: "Bedbugs");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 52,
                column: "Code",
                value: "Beta blockers");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 53,
                column: "Code",
                value: "Black eye");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 54,
                column: "Code",
                value: "Blood clots");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 55,
                column: "Code",
                value: "Blood groups");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 56,
                column: "Code",
                value: "Blood in semen (haematospermia)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 57,
                column: "Code",
                value: "Blood in urine");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 58,
                column: "Code",
                value: "Blood pressure test");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 59,
                column: "Code",
                value: "Body dysmorphic disorder (BDD)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 60,
                column: "Code",
                value: "Infected piercings");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 61,
                column: "Code",
                value: "Boils");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 62,
                column: "Code",
                value: "Botulism");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 63,
                column: "Code",
                value: "Bowel polyps");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 64,
                column: "Code",
                value: "Bowen's disease");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 65,
                column: "Code",
                value: "Brain tumours");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 66,
                column: "Code",
                value: "Breast pain");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 67,
                column: "Code",
                value: "Breast reduction on the NHS");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 68,
                column: "Code",
                value: "Breath-holding in babies and children");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 69,
                column: "Code",
                value: "Broken ankle");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 70,
                column: "Code",
                value: "Broken arm or wrist");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 71,
                column: "Code",
                value: "Broken collarbone");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 72,
                column: "Code",
                value: "Broken finger or thumb");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 73,
                column: "Code",
                value: "Broken leg");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 74,
                column: "Code",
                value: "Broken nose");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 75,
                column: "Code",
                value: "Broken or bruised ribs");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 76,
                column: "Code",
                value: "Broken toe");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 77,
                column: "Code",
                value: "Bronchitis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 78,
                column: "Code",
                value: "Brucellosis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 79,
                column: "Code",
                value: "Brugada syndrome");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 80,
                column: "Code",
                value: "Carbon monoxide poisoning");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 81,
                column: "Code",
                value: "Neuroendocrine tumours and carcinoid syndrome");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 82,
                column: "Code",
                value: "Cardiomyopathy");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 83,
                column: "Code",
                value: "Cardiovascular disease");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 84,
                column: "Code",
                value: "Age-related cataracts");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 85,
                column: "Code",
                value: "Cataracts (age-related)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 86,
                column: "Code",
                value: "Catarrh");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 87,
                column: "Code",
                value: "Cavernoma");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 88,
                column: "Code",
                value: "Clostridium difficile (C. diff) infection");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 89,
                column: "Code",
                value: "Carcinoembryonic antigen (CEA) test");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 90,
                column: "Code",
                value: "Cervical rib");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 91,
                column: "Code",
                value: "Thoracic outlet syndrome");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 92,
                column: "Code",
                value: "Charles Bonnet syndrome");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 93,
                column: "Code",
                value: "Chest infection");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 94,
                column: "Code",
                value: "Chest pain");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 95,
                column: "Code",
                value: "Heart pain");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 96,
                column: "Code",
                value: "Chiari malformation");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 97,
                column: "Code",
                value: "Chilblains");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 98,
                column: "Code",
                value: "Chiropractic");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 99,
                column: "Code",
                value: "Cholesteatoma");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 100,
                column: "Code",
                value: "Chronic traumatic encephalopathy");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 101,
                column: "Code",
                value: "Circumcision in boys");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 102,
                column: "Code",
                value: "Circumcision in men");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 103,
                column: "Code",
                value: "Claustrophobia");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 104,
                column: "Code",
                value: "Cluster headaches");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 105,
                column: "Code",
                value: "Colour vision deficiency (colour blindness)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 106,
                column: "Code",
                value: "Coma");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 107,
                column: "Code",
                value: "Compartment syndrome");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 108,
                column: "Code",
                value: "Concussion");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 109,
                column: "Code",
                value: "Sudden confusion (delirium)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 110,
                column: "Code",
                value: "Confusion (sudden)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 111,
                column: "Code",
                value: "Delirium");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 112,
                column: "Code",
                value: "Costochondritis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 113,
                column: "Code",
                value: "Cough");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 114,
                column: "Code",
                value: "Coughing up blood (blood in phlegm)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 115,
                column: "Code",
                value: "Cradle cap");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 116,
                column: "Code",
                value: "CT scan");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 117,
                column: "Code",
                value: "Cuts and grazes");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 118,
                column: "Code",
                value: "Blue skin or lips (cyanosis)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 119,
                column: "Code",
                value: "Cyanosis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 120,
                column: "Code",
                value: "Cyclical vomiting syndrome");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 121,
                column: "Code",
                value: "Cyclospora");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 122,
                column: "Code",
                value: "Cyclothymia");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 123,
                column: "Code",
                value: "Dandruff");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 124,
                column: "Code",
                value: "Decongestants");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 125,
                column: "Code",
                value: "Dental abscess");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 126,
                column: "Code",
                value: "Dentures (false teeth)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 127,
                column: "Code",
                value: "Dyspraxia (developmental co-ordination disorder) in adults");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 128,
                column: "Code",
                value: "Developmental dysplasia of the hip");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 129,
                column: "Code",
                value: "Congenital hip dislocation");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 130,
                column: "Code",
                value: "Hip dysplasia");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 131,
                column: "Code",
                value: "Diabetes");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 132,
                column: "Code",
                value: "Diabetic eye screening");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 133,
                column: "Code",
                value: "Diabetic ketoacidosis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 134,
                column: "Code",
                value: "DiGeorge syndrome (22q11 deletion)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 135,
                column: "Code",
                value: "Dislocated kneecap");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 136,
                column: "Code",
                value: "Dislocated shoulder");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 137,
                column: "Code",
                value: "Differences in sex development");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 138,
                column: "Code",
                value: "intersex");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 139,
                column: "Code",
                value: "Dissociative disorders");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 140,
                column: "Code",
                value: "Diverticular disease and diverticulitis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 141,
                column: "Code",
                value: "Dizziness");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 142,
                column: "Code",
                value: "Dry mouth");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 143,
                column: "Code",
                value: "Dysarthria (difficulty speaking)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 144,
                column: "Code",
                value: "Dysentery");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 145,
                column: "Code",
                value: "Earache");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 146,
                column: "Code",
                value: "Early or delayed puberty");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 147,
                column: "Code",
                value: "Puberty (early or delayed)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 148,
                column: "Code",
                value: "Earwax build-up");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 149,
                column: "Code",
                value: "Eating disorders");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 150,
                column: "Code",
                value: "Ebola virus disease");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 151,
                column: "Code",
                value: "Echocardiogram");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 152,
                column: "Code",
                value: "Ectropion");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 153,
                column: "Code",
                value: "Edwards' syndrome (trisomy 18)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 154,
                column: "Code",
                value: "Ehlers-Danlos syndromes");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 155,
                column: "Code",
                value: "Ejaculation problems");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 156,
                column: "Code",
                value: "Premature ejaculation");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 157,
                column: "Code",
                value: "Elbow and arm pain");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 158,
                column: "Code",
                value: "Electrocardiogram (ECG)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 159,
                column: "Code",
                value: "Electroencephalogram (EEG)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 160,
                column: "Code",
                value: "Electrolyte test");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 161,
                column: "Code",
                value: "Embolism");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 162,
                column: "Code",
                value: "Emollients");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 163,
                column: "Code",
                value: "Empyema");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 164,
                column: "Code",
                value: "Endoscopy");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 165,
                column: "Code",
                value: "Enhanced recovery");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 166,
                column: "Code",
                value: "Epididymitis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 167,
                column: "Code",
                value: "Epiglottitis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 168,
                column: "Code",
                value: "Erythema multiforme");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 169,
                column: "Code",
                value: "Erythema nodosum");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 170,
                column: "Code",
                value: "Erythromelalgia");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 171,
                column: "Code",
                value: "Euthanasia and assisted suicide");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 172,
                column: "Code",
                value: "Ewing sarcoma");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 173,
                column: "Code",
                value: "Excessive daytime sleepiness (hypersomnia)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 174,
                column: "Code",
                value: "Hypersomnia");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 175,
                column: "Code",
                value: "Eye cancer");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 176,
                column: "Code",
                value: "Eye injuries");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 177,
                column: "Code",
                value: "Eyelid problems");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 178,
                column: "Code",
                value: "Eye tests for children");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 179,
                column: "Code",
                value: "Prosopagnosia (face blindness)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 180,
                column: "Code",
                value: "Face blindness");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 181,
                column: "Code",
                value: "Febrile seizures");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 182,
                column: "Code",
                value: "Fits (children with fever)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 183,
                column: "Code",
                value: "Seizures (children with fever)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 184,
                column: "Code",
                value: "Female genital mutilation (FGM)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 185,
                column: "Code",
                value: "High temperature (fever) in children");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 186,
                column: "Code",
                value: "Fever in children");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 187,
                column: "Code",
                value: "Flat feet");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 188,
                column: "Code",
                value: "Fluoride");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 189,
                column: "Code",
                value: "Foetal alcohol spectrum disorder");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 190,
                column: "Code",
                value: "Food colours and hyperactivity");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 191,
                column: "Code",
                value: "Food intolerance");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 192,
                column: "Code",
                value: "Foot drop");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 193,
                column: "Code",
                value: "Gallbladder cancer");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 194,
                column: "Code",
                value: "Ganglion cyst");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 195,
                column: "Code",
                value: "Gastritis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 196,
                column: "Code",
                value: "Gastroparesis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 197,
                column: "Code",
                value: "General anaesthesia");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 198,
                column: "Code",
                value: "Gilbert's syndrome");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 199,
                column: "Code",
                value: "Glutaric aciduria type 1");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 200,
                column: "Code",
                value: "Granuloma annulare");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 201,
                column: "Code",
                value: "Granulomatosis with polyangiitis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 202,
                column: "Code",
                value: "Growing pains");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 203,
                column: "Code",
                value: "Hair dye reactions");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 204,
                column: "Code",
                value: "Hairy cell leukaemia");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 205,
                column: "Code",
                value: "Leukaemia (hairy cell)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 206,
                column: "Code",
                value: "Hallucinations and hearing voices");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 207,
                column: "Code",
                value: "Hearing voices");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 208,
                column: "Code",
                value: "Hamstring injury");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 209,
                column: "Code",
                value: "Hand foot and mouth disease");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 210,
                column: "Code",
                value: "Head and neck cancer");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 211,
                column: "Code",
                value: "Health anxiety");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 212,
                column: "Code",
                value: "Hypochondria");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 213,
                column: "Code",
                value: "Hearing tests for children");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 214,
                column: "Code",
                value: "Heart-lung transplant");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 215,
                column: "Code",
                value: "Heart palpitations and ectopic beats");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 216,
                column: "Code",
                value: "Palpitations");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 217,
                column: "Code",
                value: "Ectopic beats");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 218,
                column: "Code",
                value: "Heat exhaustion and heatstroke");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 219,
                column: "Code",
                value: "Heat rash (prickly heat)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 220,
                column: "Code",
                value: "Prickly heat");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 221,
                column: "Code",
                value: "Sweating (excessive)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 222,
                column: "Code",
                value: "sweat rash");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 223,
                column: "Code",
                value: "Henoch-Schönlein purpura (HSP)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 224,
                column: "Code",
                value: "Hepatitis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 225,
                column: "Code",
                value: "Herbal medicines");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 226,
                column: "Code",
                value: "Herceptin (trastuzumab)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 227,
                column: "Code",
                value: "Hereditary haemorrhagic telangiectasia (HHT)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 228,
                column: "Code",
                value: "Hereditary neuropathy with pressure palsies (HNPP)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 229,
                column: "Code",
                value: "Hereditary spastic paraplegia");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 230,
                column: "Code",
                value: "Hernia");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 231,
                column: "Code",
                value: "Herpes simplex eye infections");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 232,
                column: "Code",
                value: "Eye infection (herpes)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 233,
                column: "Code",
                value: "Herpetic whitlow (whitlow finger)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 234,
                column: "Code",
                value: "Whitlow finger");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 235,
                column: "Code",
                value: "Haemophilus influenzae type b (Hib)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 236,
                column: "Code",
                value: "Hidradenitis suppurativa (HS)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 237,
                column: "Code",
                value: "Hyperglycaemia (high blood sugar)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 238,
                column: "Code",
                value: "Hip pain in adults");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 239,
                column: "Code",
                value: "Hirschsprung's disease");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 240,
                column: "Code",
                value: "Hoarding disorder");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 241,
                column: "Code",
                value: "Homeopathy");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 242,
                column: "Code",
                value: "Home oxygen therapy");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 243,
                column: "Code",
                value: "Oxygen therapy");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 244,
                column: "Code",
                value: "Homocystinuria");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 245,
                column: "Code",
                value: "Noise sensitivity (hyperacusis)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 246,
                column: "Code",
                value: "Hypnotherapy");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 247,
                column: "Code",
                value: "Hypothermia");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 248,
                column: "Code",
                value: "Ichthyosis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 249,
                column: "Code",
                value: "Indigestion");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 250,
                column: "Code",
                value: "Inflammatory bowel disease");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 251,
                column: "Code",
                value: "Ingrown hairs");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 252,
                column: "Code",
                value: "Ingrown toenail");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 253,
                column: "Code",
                value: "Intensive care");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 254,
                column: "Code",
                value: "Interstitial cystitis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 255,
                column: "Code",
                value: "Intracranial hypertension");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 256,
                column: "Code",
                value: "Hip pain in children (irritable hip)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 257,
                column: "Code",
                value: "Irritable hip");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 258,
                column: "Code",
                value: "Isovaleric acidaemia");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 259,
                column: "Code",
                value: "Joint pain");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 260,
                column: "Code",
                value: "Kaposi's sarcoma");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 261,
                column: "Code",
                value: "Keratosis pilaris");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 262,
                column: "Code",
                value: "Klinefelter syndrome");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 263,
                column: "Code",
                value: "Knee pain");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 264,
                column: "Code",
                value: "Knock knees");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 265,
                column: "Code",
                value: "Kwashiorkor");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 266,
                column: "Code",
                value: "Labial fusion");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 267,
                column: "Code",
                value: "Lambert-Eaton myasthenic syndrome");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 268,
                column: "Code",
                value: "Lactate dehydrogenase (LDH) test");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 269,
                column: "Code",
                value: "Legionnaires' disease");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 270,
                column: "Code",
                value: "Lichen sclerosus");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 271,
                column: "Code",
                value: "Limping in children");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 272,
                column: "Code",
                value: "Lipoedema");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 273,
                column: "Code",
                value: "Lipoma");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 274,
                column: "Code",
                value: "Liver disease");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 275,
                column: "Code",
                value: "Local anaesthesia");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 276,
                column: "Code",
                value: "Long QT syndrome");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 277,
                column: "Code",
                value: "Loss of libido (reduced sex drive)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 278,
                column: "Code",
                value: "Low blood sugar (hypoglycaemia)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 279,
                column: "Code",
                value: "Hypoglycaemia (low blood sugar)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 280,
                column: "Code",
                value: "Low sperm count");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 281,
                column: "Code",
                value: "Sperm count (low)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 282,
                column: "Code",
                value: "Lumps");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 283,
                column: "Code",
                value: "Lyme disease");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 284,
                column: "Code",
                value: "Macular hole");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 285,
                column: "Code",
                value: "Magnesium test");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 286,
                column: "Code",
                value: "The 'male menopause'");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 287,
                column: "Code",
                value: "Male menopause");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 288,
                column: "Code",
                value: "Mallet finger");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 289,
                column: "Code",
                value: "Maple syrup urine disease");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 290,
                column: "Code",
                value: "Mastoiditis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 291,
                column: "Code",
                value: "MCADD");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 292,
                column: "Code",
                value: "Medically unexplained symptoms");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 293,
                column: "Code",
                value: "Functional neurological disorder");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 294,
                column: "Code",
                value: "Ménière's disease");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 295,
                column: "Code",
                value: "Mesothelioma");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 296,
                column: "Code",
                value: "Metabolic syndrome");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 297,
                column: "Code",
                value: "Metallic taste");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 298,
                column: "Code",
                value: "Mitral valve problems");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 299,
                column: "Code",
                value: "Heart valve problems");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 300,
                column: "Code",
                value: "Molar pregnancy");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 301,
                column: "Code",
                value: "Morton's neuroma");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 302,
                column: "Code",
                value: "Motion sickness");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 303,
                column: "Code",
                value: "Mouth ulcers");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 304,
                column: "Code",
                value: "MRSA");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 305,
                column: "Code",
                value: "Multiple system atrophy");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 306,
                column: "Code",
                value: "Mycobacterium chimaera infection");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 307,
                column: "Code",
                value: "Myelodysplastic syndrome (myelodysplasia)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 308,
                column: "Code",
                value: "Myositis (polymyositis and dermatomyositis)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 309,
                column: "Code",
                value: "Nail patella syndrome");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 310,
                column: "Code",
                value: "Nail problems");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 311,
                column: "Code",
                value: "Nasal and sinus cancer");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 312,
                column: "Code",
                value: "Nose cancer");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 313,
                column: "Code",
                value: "Sinus cancer");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 314,
                column: "Code",
                value: "Nasopharyngeal cancer");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 315,
                column: "Code",
                value: "Neck pain");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 316,
                column: "Code",
                value: "Necrotising fasciitis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 317,
                column: "Code",
                value: "Neonatal herpes (herpes in a baby)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 318,
                column: "Code",
                value: "Herpes in babies");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 319,
                column: "Code",
                value: "Nephrotic syndrome in children");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 320,
                column: "Code",
                value: "Neuroblastoma");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 321,
                column: "Code",
                value: "Neuroendocrine tumours");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 322,
                column: "Code",
                value: "Neuromyelitis optica");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 323,
                column: "Code",
                value: "Night sweats");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 324,
                column: "Code",
                value: "Sweating at night");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 325,
                column: "Code",
                value: "Night terrors and nightmares");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 326,
                column: "Code",
                value: "Nipple discharge");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 327,
                column: "Code",
                value: "Non-alcoholic fatty liver disease (NAFLD)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 328,
                column: "Code",
                value: "Norovirus (vomiting bug)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 329,
                column: "Code",
                value: "Vomiting bug");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 330,
                column: "Code",
                value: "Winter vomiting bug");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 331,
                column: "Code",
                value: "NSAIDs");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 332,
                column: "Code",
                value: "Swollen ankles feet and legs (oedema)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 333,
                column: "Code",
                value: "Oesophageal atresia and tracheo-oesophageal fistula");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 334,
                column: "Code",
                value: "Orf");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 335,
                column: "Code",
                value: "Osteophyte (bone spur)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 336,
                column: "Code",
                value: "Otosclerosis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 337,
                column: "Code",
                value: "Ovulation pain");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 338,
                column: "Code",
                value: "Panic disorder");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 339,
                column: "Code",
                value: "Patau's syndrome");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 340,
                column: "Code",
                value: "Peak flow test");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 341,
                column: "Code",
                value: "Pelvic pain");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 342,
                column: "Code",
                value: "Penile cancer");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 343,
                column: "Code",
                value: "Period pain");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 344,
                column: "Code",
                value: "Menstrual pain");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 345,
                column: "Code",
                value: "Periods");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 346,
                column: "Code",
                value: "Persistent trophoblastic disease and choriocarcinoma");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 347,
                column: "Code",
                value: "Personality disorder");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 348,
                column: "Code",
                value: "PET scan");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 349,
                column: "Code",
                value: "Phaeochromocytoma");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 350,
                column: "Code",
                value: "Phenylketonuria");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 351,
                column: "Code",
                value: "Tight foreskin (phimosis and paraphimosis)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 352,
                column: "Code",
                value: "Foreskin problems");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 353,
                column: "Code",
                value: "Phimosis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 354,
                column: "Code",
                value: "Phlebitis (superficial thrombophlebitis)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 355,
                column: "Code",
                value: "Superficial thrombophlebitis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 356,
                column: "Code",
                value: "Phosphate test");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 357,
                column: "Code",
                value: "Photodynamic therapy (PDT)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 358,
                column: "Code",
                value: "Pins and needles");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 359,
                column: "Code",
                value: "PIP breast implants");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 360,
                column: "Code",
                value: "Pityriasis rosea");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 361,
                column: "Code",
                value: "Pityriasis versicolor");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 362,
                column: "Code",
                value: "Plagiocephaly and brachycephaly (flat head syndrome)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 363,
                column: "Code",
                value: "Brachycephaly and plagiocephaly");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 364,
                column: "Code",
                value: "Flat head syndrome");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 365,
                column: "Code",
                value: "Pleurisy");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 366,
                column: "Code",
                value: "Polio");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 367,
                column: "Code",
                value: "Polyhydramnios (too much amniotic fluid)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 368,
                column: "Code",
                value: "Polymorphic light eruption");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 369,
                column: "Code",
                value: "Pompholyx (dyshidrotic eczema)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 370,
                column: "Code",
                value: "Postmenopausal bleeding");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 371,
                column: "Code",
                value: "Bleeding after the menopause");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 372,
                column: "Code",
                value: "Post-mortem");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 373,
                column: "Code",
                value: "Postpartum psychosis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 374,
                column: "Code",
                value: "Postural tachycardia syndrome (PoTS)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 375,
                column: "Code",
                value: "Potassium test");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 376,
                column: "Code",
                value: "Predictive genetic tests for cancer risk genes");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 377,
                column: "Code",
                value: "Genetic test for cancer gene");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 378,
                column: "Code",
                value: "Probiotics");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 379,
                column: "Code",
                value: "Problems swallowing pills");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 380,
                column: "Code",
                value: "Swallowing pills");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 381,
                column: "Code",
                value: "Prostate problems");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 382,
                column: "Code",
                value: "Prostatitis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 383,
                column: "Code",
                value: "Psoriatic arthritis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 384,
                column: "Code",
                value: "Psychiatry");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 385,
                column: "Code",
                value: "Pubic lice");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 386,
                column: "Code",
                value: "Pudendal neuralgia");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 387,
                column: "Code",
                value: "Pyoderma gangrenosum");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 388,
                column: "Code",
                value: "Q fever");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 389,
                column: "Code",
                value: "Rashes in babies and children");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 390,
                column: "Code",
                value: "Red blood cell count");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 391,
                column: "Code",
                value: "Red eye");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 392,
                column: "Code",
                value: "Reflux in babies");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 393,
                column: "Code",
                value: "Acid reflux in babies");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 394,
                column: "Code",
                value: "Respiratory tract infections (RTIs)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 395,
                column: "Code",
                value: "Retinal migraine");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 396,
                column: "Code",
                value: "Retinoblastoma (eye cancer in children)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 397,
                column: "Code",
                value: "Rett syndrome");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 398,
                column: "Code",
                value: "Reye's syndrome");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 399,
                column: "Code",
                value: "Roseola");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 400,
                column: "Code",
                value: "Salivary gland stones");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 401,
                column: "Code",
                value: "Sarcoidosis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 402,
                column: "Code",
                value: "SARS (severe acute respiratory syndrome)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 403,
                column: "Code",
                value: "Scarlet fever");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 404,
                column: "Code",
                value: "Schistosomiasis (bilharzia)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 405,
                column: "Code",
                value: "Bilharzia");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 406,
                column: "Code",
                value: "Scleroderma");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 407,
                column: "Code",
                value: "Selective mutism");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 408,
                column: "Code",
                value: "Septic arthritis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 409,
                column: "Code",
                value: "Sexually transmitted infections (STIs)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 410,
                column: "Code",
                value: "Shin splints");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 411,
                column: "Code",
                value: "Shin pain (shin splints)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 412,
                column: "Code",
                value: "Shortness of breath");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 413,
                column: "Code",
                value: "Shoulder impingement");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 414,
                column: "Code",
                value: "Sick building syndrome");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 415,
                column: "Code",
                value: "Silicosis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 416,
                column: "Code",
                value: "Skin cyst");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 417,
                column: "Code",
                value: "Skin tags");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 418,
                column: "Code",
                value: "Slapped cheek syndrome");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 419,
                column: "Code",
                value: "Sleepwalking");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 420,
                column: "Code",
                value: "Smelly urine");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 421,
                column: "Code",
                value: "Urine (smelly)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 422,
                column: "Code",
                value: "Snake bites");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 423,
                column: "Code",
                value: "Social anxiety (social phobia)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 424,
                column: "Code",
                value: "Soft tissue sarcomas");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 425,
                column: "Code",
                value: "Sore or dry lips");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 426,
                column: "Code",
                value: "Sore lips");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 427,
                column: "Code",
                value: "Dry lips");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 428,
                column: "Code",
                value: "Lips (sore or dry)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 429,
                column: "Code",
                value: "Sore or white tongue");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 430,
                column: "Code",
                value: "Tongue (sore or white)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 431,
                column: "Code",
                value: "Sore throat");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 432,
                column: "Code",
                value: "Throat (sore)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 433,
                column: "Code",
                value: "Spirometry");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 434,
                column: "Code",
                value: "Spleen problems and spleen removal");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 435,
                column: "Code",
                value: "Spondylolisthesis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 436,
                column: "Code",
                value: "Staph infection");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 437,
                column: "Code",
                value: "Steroid inhalers");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 438,
                column: "Code",
                value: "Steroid injections");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 439,
                column: "Code",
                value: "Steroid nasal sprays");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 440,
                column: "Code",
                value: "Steroids");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 441,
                column: "Code",
                value: "Corticosteroids");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 442,
                column: "Code",
                value: "Steroid tablets");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 443,
                column: "Code",
                value: "Stevens-Johnson syndrome");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 444,
                column: "Code",
                value: "Stomach ache");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 445,
                column: "Code",
                value: "Tummy ache");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 446,
                column: "Code",
                value: "Stopped or missed periods");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 447,
                column: "Code",
                value: "Periods (stopped or missed)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 448,
                column: "Code",
                value: "Stop smoking treatments");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 449,
                column: "Code",
                value: "Smoking (treatments to stop)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 450,
                column: "Code",
                value: "Stretch marks");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 451,
                column: "Code",
                value: "Stye");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 452,
                column: "Code",
                value: "Sudden infant death syndrome (SIDS)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 453,
                column: "Code",
                value: "Sunburn");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 454,
                column: "Code",
                value: "Swine flu (H1N1)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 455,
                column: "Code",
                value: "Swollen glands");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 456,
                column: "Code",
                value: "Temporomandibular disorder (TMD)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 457,
                column: "Code",
                value: "Jaw pain");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 458,
                column: "Code",
                value: "Tension-type headaches");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 459,
                column: "Code",
                value: "Headaches (tension-type)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 460,
                column: "Code",
                value: "Tetanus");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 461,
                column: "Code",
                value: "Excessive thirst");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 462,
                column: "Code",
                value: "Thirst (excessive)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 463,
                column: "Code",
                value: "Thrombophilia");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 464,
                column: "Code",
                value: "Thyroiditis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 465,
                column: "Code",
                value: "Total iron-binding capacity (TIBC) and transferrin test");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 466,
                column: "Code",
                value: "Tongue-tie");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 467,
                column: "Code",
                value: "Toothache");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 468,
                column: "Code",
                value: "Dental pain");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 469,
                column: "Code",
                value: "Tooth decay");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 470,
                column: "Code",
                value: "Topical corticosteroids");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 471,
                column: "Code",
                value: "Steroid cream");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 472,
                column: "Code",
                value: "Corticosteroid cream");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 473,
                column: "Code",
                value: "Total protein test");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 474,
                column: "Code",
                value: "Toxic shock syndrome");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 475,
                column: "Code",
                value: "TENS (transcutaneous electrical nerve stimulation)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 476,
                column: "Code",
                value: "Trimethylaminuria ('fish odour syndrome')");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 477,
                column: "Code",
                value: "Typhus");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 478,
                column: "Code",
                value: "Ultrasound scan");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 479,
                column: "Code",
                value: "Unintentional weight loss");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 480,
                column: "Code",
                value: "Weight loss (unintentional)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 481,
                column: "Code",
                value: "Weight loss (unexpected)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 482,
                column: "Code",
                value: "Urinary tract infections (UTIs)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 483,
                column: "Code",
                value: "Vaginal discharge");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 484,
                column: "Code",
                value: "Vaginal dryness");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 485,
                column: "Code",
                value: "Vaginitis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 486,
                column: "Code",
                value: "Vasculitis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 487,
                column: "Code",
                value: "Blindness and vision loss");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 488,
                column: "Code",
                value: "Vomiting blood (haematemesis)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 489,
                column: "Code",
                value: "Von Willebrand disease");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 490,
                column: "Code",
                value: "Vulvodynia (vulval pain)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 491,
                column: "Code",
                value: "Vaginal pain");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 492,
                column: "Code",
                value: "Warts and verrucas");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 493,
                column: "Code",
                value: "West Nile virus");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 494,
                column: "Code",
                value: "Whooping cough");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 495,
                column: "Code",
                value: "Wolff-Parkinson-White syndrome");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 496,
                column: "Code",
                value: "X-ray");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 497,
                column: "Code",
                value: "Zika virus");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 498,
                column: "Code",
                value: "Abdominal aortic aneurysm");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 499,
                column: "Code",
                value: "AAA");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 500,
                column: "Code",
                value: "Aneurysm (abdominal aortic)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 501,
                column: "Code",
                value: "Abdominal aortic aneurysm screening");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 502,
                column: "Code",
                value: "AAA screening");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 503,
                column: "Code",
                value: "Abscess");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 504,
                column: "Code",
                value: "Acne");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 505,
                column: "Code",
                value: "Actinomycosis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 506,
                column: "Code",
                value: "Acute lymphoblastic leukaemia");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 507,
                column: "Code",
                value: "Leukaemia (acute lymphoblastic)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 508,
                column: "Code",
                value: "Acute myeloid leukaemia");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 509,
                column: "Code",
                value: "Leukaemia (acute myeloid)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 510,
                column: "Code",
                value: "Acute pancreatitis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 511,
                column: "Code",
                value: "Pancreatitis (acute)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 512,
                column: "Code",
                value: "Addison's disease");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 513,
                column: "Code",
                value: "Agoraphobia");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 514,
                column: "Code",
                value: "Albinism");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 515,
                column: "Code",
                value: "Alcohol misuse");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 516,
                column: "Code",
                value: "Alcohol-related liver disease");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 517,
                column: "Code",
                value: "Liver disease (alcohol-related)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 518,
                column: "Code",
                value: "Allergic rhinitis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 519,
                column: "Code",
                value: "Rhinitis (allergic)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 520,
                column: "Code",
                value: "Allergies");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 521,
                column: "Code",
                value: "Altitude sickness");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 522,
                column: "Code",
                value: "Alzheimer's disease");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 523,
                column: "Code",
                value: "Amniocentesis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 524,
                column: "Code",
                value: "Anal fissure");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 525,
                column: "Code",
                value: "Anal fistula");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 526,
                column: "Code",
                value: "Anaphylaxis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 527,
                column: "Code",
                value: "Androgen insensitivity syndrome");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 528,
                column: "Code",
                value: "Angina");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 529,
                column: "Code",
                value: "Angioedema");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 530,
                column: "Code",
                value: "Angiography");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 531,
                column: "Code",
                value: "Ankylosing spondylitis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 532,
                column: "Code",
                value: "Anorexia nervosa");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 533,
                column: "Code",
                value: "Antibiotics");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 534,
                column: "Code",
                value: "Anticoagulant medicines");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 535,
                column: "Code",
                value: "Antidepressants");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 536,
                column: "Code",
                value: "Antiphospholipid syndrome (APS)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 537,
                column: "Code",
                value: "Hughes syndrome");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 538,
                column: "Code",
                value: "Aortic valve replacement");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 539,
                column: "Code",
                value: "Heart valve replacement");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 540,
                column: "Code",
                value: "Aphasia");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 541,
                column: "Code",
                value: "Appendicitis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 542,
                column: "Code",
                value: "Arthritis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 543,
                column: "Code",
                value: "Arthroscopy");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 544,
                column: "Code",
                value: "Aspergillosis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 545,
                column: "Code",
                value: "Asthma");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 546,
                column: "Code",
                value: "Astigmatism");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 547,
                column: "Code",
                value: "Ataxia");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 548,
                column: "Code",
                value: "Atopic eczema");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 549,
                column: "Code",
                value: "Eczema (atopic)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 550,
                column: "Code",
                value: "Atrial fibrillation");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 551,
                column: "Code",
                value: "Attention deficit hyperactivity disorder (ADHD)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 552,
                column: "Code",
                value: "Autosomal dominant polycystic kidney disease");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 553,
                column: "Code",
                value: "Polycystic kidney disease (autosomal dominant)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 554,
                column: "Code",
                value: "Autosomal recessive polycystic kidney disease");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 555,
                column: "Code",
                value: "Polycystic kidney disease (autosomal recessive)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 556,
                column: "Code",
                value: "Back pain");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 557,
                column: "Code",
                value: "Bacterial vaginosis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 558,
                column: "Code",
                value: "Bad breath");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 559,
                column: "Code",
                value: "Halitosis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 560,
                column: "Code",
                value: "Baker's cyst");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 561,
                column: "Code",
                value: "Popliteal cyst");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 562,
                column: "Code",
                value: "Bartholin's cyst");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 563,
                column: "Code",
                value: "Bedwetting in children");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 564,
                column: "Code",
                value: "Behçet's disease");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 565,
                column: "Code",
                value: "Bell's palsy");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 566,
                column: "Code",
                value: "Benign brain tumour (non-cancerous)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 567,
                column: "Code",
                value: "Brain tumour (benign)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 568,
                column: "Code",
                value: "Bile duct cancer (cholangiocarcinoma)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 569,
                column: "Code",
                value: "Cholangiocarcinoma");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 570,
                column: "Code",
                value: "Binge eating disorder");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 571,
                column: "Code",
                value: "Biopsy");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 572,
                column: "Code",
                value: "Bipolar disorder");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 573,
                column: "Code",
                value: "Birthmarks");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 574,
                column: "Code",
                value: "Bladder cancer");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 575,
                column: "Code",
                value: "Bladder stones");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 576,
                column: "Code",
                value: "Bleeding from the bottom (rectal bleeding)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 577,
                column: "Code",
                value: "Rectal bleeding");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 578,
                column: "Code",
                value: "Blepharitis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 579,
                column: "Code",
                value: "Blisters");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 580,
                column: "Code",
                value: "Blood tests");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 581,
                column: "Code",
                value: "Blushing");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 582,
                column: "Code",
                value: "Bone cancer");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 583,
                column: "Code",
                value: "Bone cyst");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 584,
                column: "Code",
                value: "Borderline personality disorder");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 585,
                column: "Code",
                value: "Bowel cancer");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 586,
                column: "Code",
                value: "Colon cancer");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 587,
                column: "Code",
                value: "Rectal cancer");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 588,
                column: "Code",
                value: "Bowel cancer screening");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 589,
                column: "Code",
                value: "Bowel incontinence");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 590,
                column: "Code",
                value: "Brain abscess");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 591,
                column: "Code",
                value: "Brain aneurysm");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 592,
                column: "Code",
                value: "Aneurysm (brain)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 593,
                column: "Code",
                value: "Brain death");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 594,
                column: "Code",
                value: "Breast abscess");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 595,
                column: "Code",
                value: "Breast cancer in women");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 596,
                column: "Code",
                value: "Breast cancer in men");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 597,
                column: "Code",
                value: "Breast lumps");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 598,
                column: "Code",
                value: "Bronchiectasis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 599,
                column: "Code",
                value: "Bronchiolitis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 600,
                column: "Code",
                value: "Bronchodilators");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 601,
                column: "Code",
                value: "Exophthalmos (bulging eyes)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 602,
                column: "Code",
                value: "Bulimia");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 603,
                column: "Code",
                value: "Burns and scalds");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 604,
                column: "Code",
                value: "Bursitis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 605,
                column: "Code",
                value: "Caesarean section");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 606,
                column: "Code",
                value: "Cancer");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 607,
                column: "Code",
                value: "Carotid endarterectomy");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 608,
                column: "Code",
                value: "Carpal tunnel syndrome");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 609,
                column: "Code",
                value: "Cartilage damage");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 610,
                column: "Code",
                value: "Cataract surgery");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 611,
                column: "Code",
                value: "Cavernous sinus thrombosis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 612,
                column: "Code",
                value: "Cellulitis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 613,
                column: "Code",
                value: "Cerebral palsy");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 614,
                column: "Code",
                value: "Cervical cancer");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 615,
                column: "Code",
                value: "Cervical screening");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 616,
                column: "Code",
                value: "Smear test");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 617,
                column: "Code",
                value: "Cervical spondylosis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 618,
                column: "Code",
                value: "Charcot-Marie-Tooth disease");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 619,
                column: "Code",
                value: "Chemotherapy");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 620,
                column: "Code",
                value: "Chickenpox");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 621,
                column: "Code",
                value: "Childhood cataracts");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 622,
                column: "Code",
                value: "Cataracts (children)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 623,
                column: "Code",
                value: "Chlamydia");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 624,
                column: "Code",
                value: "Cholera");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 625,
                column: "Code",
                value: "Chorionic villus sampling");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 626,
                column: "Code",
                value: "Myalgic encephalomyelitis or chronic fatigue syndrome (ME/CFS)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 627,
                column: "Code",
                value: "Chronic fatigue syndrome (ME/CFS)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 628,
                column: "Code",
                value: "Chronic lymphocytic leukaemia");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 629,
                column: "Code",
                value: "Leukaemia (chronic lymphocytic)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 630,
                column: "Code",
                value: "Chronic myeloid leukaemia");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 631,
                column: "Code",
                value: "Leukaemia (chronic myeloid)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 632,
                column: "Code",
                value: "Chronic obstructive pulmonary disease (COPD)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 633,
                column: "Code",
                value: "Chronic pancreatitis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 634,
                column: "Code",
                value: "Pancreatitis (chronic)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 635,
                column: "Code",
                value: "Cirrhosis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 636,
                column: "Code",
                value: "Cleft lip and palate");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 637,
                column: "Code",
                value: "Clinical depression");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 638,
                column: "Code",
                value: "Depression");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 639,
                column: "Code",
                value: "Clinical trials");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 640,
                column: "Code",
                value: "Club foot");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 641,
                column: "Code",
                value: "Coeliac disease");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 642,
                column: "Code",
                value: "Cognitive behavioural therapy (CBT)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 643,
                column: "Code",
                value: "Colic");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 644,
                column: "Code",
                value: "Colostomy");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 645,
                column: "Code",
                value: "Colposcopy");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 646,
                column: "Code",
                value: "Common cold");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 647,
                column: "Code",
                value: "Complex regional pain syndrome");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 648,
                column: "Code",
                value: "Congenital heart disease");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 649,
                column: "Code",
                value: "Conjunctivitis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 650,
                column: "Code",
                value: "Consent to treatment");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 651,
                column: "Code",
                value: "Constipation");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 652,
                column: "Code",
                value: "Contact dermatitis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 653,
                column: "Code",
                value: "Eczema (contact dermatitis)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 654,
                column: "Code",
                value: "Cornea transplant");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 655,
                column: "Code",
                value: "Corns and calluses");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 656,
                column: "Code",
                value: "Cardiac catheterisation and coronary angiography");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 657,
                column: "Code",
                value: "Coronary angioplasty and stent insertion");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 658,
                column: "Code",
                value: "Angioplasty");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 659,
                column: "Code",
                value: "Stent insertion");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 660,
                column: "Code",
                value: "Coronary artery bypass graft");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 661,
                column: "Code",
                value: "Heart bypass");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 662,
                column: "Code",
                value: "CABG");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 663,
                column: "Code",
                value: "Coronary heart disease");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 664,
                column: "Code",
                value: "Heart disease (coronary)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 665,
                column: "Code",
                value: "Corticobasal degeneration");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 666,
                column: "Code",
                value: "Counselling");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 667,
                column: "Code",
                value: "Craniosynostosis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 668,
                column: "Code",
                value: "Creutzfeldt-Jakob disease");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 669,
                column: "Code",
                value: "CJD");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 670,
                column: "Code",
                value: "Crohn's disease");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 671,
                column: "Code",
                value: "Croup");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 672,
                column: "Code",
                value: "Cushing's syndrome");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 673,
                column: "Code",
                value: "Cystic fibrosis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 674,
                column: "Code",
                value: "Cystitis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 675,
                column: "Code",
                value: "Cystoscopy");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 676,
                column: "Code",
                value: "Cytomegalovirus (CMV)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 677,
                column: "Code",
                value: "Deafblindness");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 678,
                column: "Code",
                value: "DVT (deep vein thrombosis)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 679,
                column: "Code",
                value: "DVT");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 680,
                column: "Code",
                value: "Dehydration");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 681,
                column: "Code",
                value: "Dementia with Lewy bodies");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 682,
                column: "Code",
                value: "Dengue");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 683,
                column: "Code",
                value: "Developmental co-ordination disorder (dyspraxia) in children");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 684,
                column: "Code",
                value: "Dyspraxia in children");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 685,
                column: "Code",
                value: "Bone density scan (DEXA scan)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 686,
                column: "Code",
                value: "DEXA scan");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 687,
                column: "Code",
                value: "Diabetes insipidus");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 688,
                column: "Code",
                value: "Diabetic retinopathy");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 689,
                column: "Code",
                value: "Dialysis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 690,
                column: "Code",
                value: "Diphtheria");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 691,
                column: "Code",
                value: "Discoid eczema");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 692,
                column: "Code",
                value: "Eczema (discoid)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 693,
                column: "Code",
                value: "Disorders of consciousness");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 694,
                column: "Code",
                value: "Vegetative state");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 695,
                column: "Code",
                value: "Double vision");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 696,
                column: "Code",
                value: "Down's syndrome");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 697,
                column: "Code",
                value: "Dry eyes");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 698,
                column: "Code",
                value: "Dupuytren's contracture");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 699,
                column: "Code",
                value: "Dyslexia");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 700,
                column: "Code",
                value: "Dystonia");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 701,
                column: "Code",
                value: "Ectopic pregnancy");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 702,
                column: "Code",
                value: "Encephalitis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 703,
                column: "Code",
                value: "Endocarditis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 704,
                column: "Code",
                value: "Endometriosis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 705,
                column: "Code",
                value: "Epidermolysis bullosa");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 706,
                column: "Code",
                value: "Epidural");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 707,
                column: "Code",
                value: "Epilepsy");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 708,
                column: "Code",
                value: "Erectile dysfunction (impotence)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 709,
                column: "Code",
                value: "Impotence");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 710,
                column: "Code",
                value: "Excessive sweating (hyperhidrosis)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 711,
                column: "Code",
                value: "Hyperhidrosis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 712,
                column: "Code",
                value: "Sweating (excessive)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 713,
                column: "Code",
                value: "Fabricated or induced illness");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 714,
                column: "Code",
                value: "Fainting");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 715,
                column: "Code",
                value: "Falls");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 716,
                column: "Code",
                value: "Femoral hernia repair");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 717,
                column: "Code",
                value: "Hernia (femoral)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 718,
                column: "Code",
                value: "Fibroids");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 719,
                column: "Code",
                value: "Fibromyalgia");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 720,
                column: "Code",
                value: "First aid");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 721,
                column: "Code",
                value: "Farting (flatulence)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 722,
                column: "Code",
                value: "Flatulence");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 723,
                column: "Code",
                value: "Wind");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 724,
                column: "Code",
                value: "Flu");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 725,
                column: "Code",
                value: "Influenza");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 726,
                column: "Code",
                value: "Food allergy");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 727,
                column: "Code",
                value: "Food poisoning");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 728,
                column: "Code",
                value: "Frontotemporal dementia");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 729,
                column: "Code",
                value: "Dementia (frontotemporal)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 730,
                column: "Code",
                value: "Frostbite");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 731,
                column: "Code",
                value: "Frozen shoulder");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 732,
                column: "Code",
                value: "Fungal nail infection");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 733,
                column: "Code",
                value: "Nail fungal infection");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 734,
                column: "Code",
                value: "Gallbladder removal");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 735,
                column: "Code",
                value: "Gallstones");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 736,
                column: "Code",
                value: "Gangrene");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 737,
                column: "Code",
                value: "Gastrectomy");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 738,
                column: "Code",
                value: "Gastroscopy");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 739,
                column: "Code",
                value: "Gender dysphoria");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 740,
                column: "Code",
                value: "Generalised anxiety disorder in adults");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 741,
                column: "Code",
                value: "Anxiety disorder in adults");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 742,
                column: "Code",
                value: "Genital herpes");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 743,
                column: "Code",
                value: "Herpes (genital)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 744,
                column: "Code",
                value: "Genital warts");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 745,
                column: "Code",
                value: "Gestational diabetes");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 746,
                column: "Code",
                value: "Diabetes in pregnancy");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 747,
                column: "Code",
                value: "Giardiasis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 748,
                column: "Code",
                value: "Glandular fever");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 749,
                column: "Code",
                value: "Glaucoma");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 750,
                column: "Code",
                value: "Glomerulonephritis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 751,
                column: "Code",
                value: "Glue ear");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 752,
                column: "Code",
                value: "Goitre");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 753,
                column: "Code",
                value: "Gonorrhoea");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 754,
                column: "Code",
                value: "Gout");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 755,
                column: "Code",
                value: "Guillain-Barré syndrome");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 756,
                column: "Code",
                value: "Gum disease");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 757,
                column: "Code",
                value: "Haemochromatosis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 758,
                column: "Code",
                value: "Haemophilia");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 759,
                column: "Code",
                value: "Hair loss");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 760,
                column: "Code",
                value: "Hand tendon repair");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 761,
                column: "Code",
                value: "Having an operation (surgery)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 762,
                column: "Code",
                value: "Surgery (having an operation)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 763,
                column: "Code",
                value: "Hay fever");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 764,
                column: "Code",
                value: "Head lice and nits");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 765,
                column: "Code",
                value: "Hearing loss");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 766,
                column: "Code",
                value: "Deafness");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 767,
                column: "Code",
                value: "Hearing tests");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 768,
                column: "Code",
                value: "Heart attack");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 769,
                column: "Code",
                value: "Heart block");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 770,
                column: "Code",
                value: "Heartburn and acid reflux");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 771,
                column: "Code",
                value: "Gastro-oesophageal reflux disease (GORD)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 772,
                column: "Code",
                value: "Heart failure");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 773,
                column: "Code",
                value: "Heart transplant");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 774,
                column: "Code",
                value: "Heavy periods");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 775,
                column: "Code",
                value: "Periods (heavy)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 776,
                column: "Code",
                value: "Hepatitis A");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 777,
                column: "Code",
                value: "Hepatitis B");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 778,
                column: "Code",
                value: "Hepatitis C");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 779,
                column: "Code",
                value: "Hiatus hernia");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 780,
                column: "Code",
                value: "Hernia (hiatus)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 781,
                column: "Code",
                value: "Hiccups");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 782,
                column: "Code",
                value: "High blood pressure (hypertension)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 783,
                column: "Code",
                value: "Hypertension");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 784,
                column: "Code",
                value: "Blood pressure (high)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 785,
                column: "Code",
                value: "High cholesterol");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 786,
                column: "Code",
                value: "Cholesterol (high)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 787,
                column: "Code",
                value: "Hip fracture");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 788,
                column: "Code",
                value: "Hip replacement");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 789,
                column: "Code",
                value: "Excessive hair growth (hirsutism)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 790,
                column: "Code",
                value: "hirsutism");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 791,
                column: "Code",
                value: "HIV and AIDS");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 792,
                column: "Code",
                value: "Hives");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 793,
                column: "Code",
                value: "Hodgkin lymphoma");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 794,
                column: "Code",
                value: "Hormone replacement therapy (HRT)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 795,
                column: "Code",
                value: "HRT");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 796,
                column: "Code",
                value: "Huntington's disease");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 797,
                column: "Code",
                value: "Hydrocephalus");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 798,
                column: "Code",
                value: "Hydronephrosis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 799,
                column: "Code",
                value: "Hysterectomy");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 800,
                column: "Code",
                value: "Hysteroscopy");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 801,
                column: "Code",
                value: "Idiopathic pulmonary fibrosis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 802,
                column: "Code",
                value: "Pulmonary fibrosis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 803,
                column: "Code",
                value: "Ileostomy");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 804,
                column: "Code",
                value: "Impetigo");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 805,
                column: "Code",
                value: "Infertility");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 806,
                column: "Code",
                value: "Inguinal hernia repair");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 807,
                column: "Code",
                value: "Hernia (inguinal)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 808,
                column: "Code",
                value: "Insect bites and stings");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 809,
                column: "Code",
                value: "Sting or bite (insect)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 810,
                column: "Code",
                value: "Insomnia");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 811,
                column: "Code",
                value: "Iron deficiency anaemia");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 812,
                column: "Code",
                value: "Anaemia (iron deficiency)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 813,
                column: "Code",
                value: "Irregular periods");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 814,
                column: "Code",
                value: "Periods (irregular)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 815,
                column: "Code",
                value: "Irritable bowel syndrome (IBS)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 816,
                column: "Code",
                value: "IBS");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 817,
                column: "Code",
                value: "Itchy bottom");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 818,
                column: "Code",
                value: "Anus (itchy)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 819,
                column: "Code",
                value: "Itchy skin");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 820,
                column: "Code",
                value: "IVF");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 821,
                column: "Code",
                value: "Japanese encephalitis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 822,
                column: "Code",
                value: "Jaundice");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 823,
                column: "Code",
                value: "Newborn jaundice");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 824,
                column: "Code",
                value: "Jaundice in newborns");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 825,
                column: "Code",
                value: "Jellyfish and other sea creature stings");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 826,
                column: "Code",
                value: "Jet lag");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 827,
                column: "Code",
                value: "Joint hypermobility syndrome");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 828,
                column: "Code",
                value: "Kawasaki disease");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 829,
                column: "Code",
                value: "Kidney cancer");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 830,
                column: "Code",
                value: "Chronic kidney disease");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 831,
                column: "Code",
                value: "Kidney failure");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 832,
                column: "Code",
                value: "Kidney infection");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 833,
                column: "Code",
                value: "Kidney stones");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 834,
                column: "Code",
                value: "Kidney transplant");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 835,
                column: "Code",
                value: "Knee ligament surgery");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 836,
                column: "Code",
                value: "Knee replacement");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 837,
                column: "Code",
                value: "Kyphosis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 838,
                column: "Code",
                value: "Labyrinthitis and vestibular neuritis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 839,
                column: "Code",
                value: "Vestibular neuritis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 840,
                column: "Code",
                value: "Lactose intolerance");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 841,
                column: "Code",
                value: "Laparoscopy (keyhole surgery)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 842,
                column: "Code",
                value: "Laryngeal (larynx) cancer");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 843,
                column: "Code",
                value: "Laryngitis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 844,
                column: "Code",
                value: "Laxatives");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 845,
                column: "Code",
                value: "Lazy eye");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 846,
                column: "Code",
                value: "Amblyopia");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 847,
                column: "Code",
                value: "Leg cramps");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 848,
                column: "Code",
                value: "Venous leg ulcer");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 849,
                column: "Code",
                value: "Leg ulcer");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 850,
                column: "Code",
                value: "Leptospirosis (Weil's disease)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 851,
                column: "Code",
                value: "Weil's disease");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 852,
                column: "Code",
                value: "Leukoplakia");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 853,
                column: "Code",
                value: "Lichen planus");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 854,
                column: "Code",
                value: "Listeriosis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 855,
                column: "Code",
                value: "Liver cancer");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 856,
                column: "Code",
                value: "Liver transplant");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 857,
                column: "Code",
                value: "Long-sightedness");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 858,
                column: "Code",
                value: "Low blood pressure (hypotension)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 859,
                column: "Code",
                value: "Blood pressure (low)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 860,
                column: "Code",
                value: "Hypotension");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 861,
                column: "Code",
                value: "Lumbar decompression surgery");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 862,
                column: "Code",
                value: "Lumbar puncture");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 863,
                column: "Code",
                value: "Lung cancer");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 864,
                column: "Code",
                value: "Lung transplant");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 865,
                column: "Code",
                value: "Lupus");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 866,
                column: "Code",
                value: "Lymphoedema");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 867,
                column: "Code",
                value: "Age-related macular degeneration (AMD)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 868,
                column: "Code",
                value: "Macular degeneration (age-related)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 869,
                column: "Code",
                value: "Malaria");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 870,
                column: "Code",
                value: "Malignant brain tumour (brain cancer)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 871,
                column: "Code",
                value: "Brain tumour (malignant)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 872,
                column: "Code",
                value: "Malnutrition");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 873,
                column: "Code",
                value: "Marfan syndrome");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 874,
                column: "Code",
                value: "Mastectomy");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 875,
                column: "Code",
                value: "Mastitis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 876,
                column: "Code",
                value: "Mastocytosis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 877,
                column: "Code",
                value: "Measles");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 878,
                column: "Code",
                value: "Medicines information");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 879,
                column: "Code",
                value: "Skin cancer (melanoma)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 880,
                column: "Code",
                value: "Meningitis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 881,
                column: "Code",
                value: "Menopause");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 882,
                column: "Code",
                value: "Migraine");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 883,
                column: "Code",
                value: "Head injury and concussion");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 884,
                column: "Code",
                value: "Miscarriage");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 885,
                column: "Code",
                value: "Moles");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 886,
                column: "Code",
                value: "Molluscum contagiosum");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 887,
                column: "Code",
                value: "Motor neurone disease");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 888,
                column: "Code",
                value: "Mouth cancer");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 889,
                column: "Code",
                value: "Tongue cancer");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 890,
                column: "Code",
                value: "MRI scan");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 891,
                column: "Code",
                value: "Mucositis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 892,
                column: "Code",
                value: "Multiple myeloma");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 893,
                column: "Code",
                value: "Myeloma");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 894,
                column: "Code",
                value: "Multiple sclerosis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 895,
                column: "Code",
                value: "Mumps");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 896,
                column: "Code",
                value: "Munchausen's syndrome");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 897,
                column: "Code",
                value: "Muscular dystrophy");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 898,
                column: "Code",
                value: "Myasthenia gravis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 899,
                column: "Code",
                value: "Narcolepsy");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 900,
                column: "Code",
                value: "Nasal polyps");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 901,
                column: "Code",
                value: "Newborn respiratory distress syndrome");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 902,
                column: "Code",
                value: "Neurofibromatosis type 1");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 903,
                column: "Code",
                value: "Neurofibromatosis type 2");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 904,
                column: "Code",
                value: "Non-allergic rhinitis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 905,
                column: "Code",
                value: "Non-gonococcal urethritis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 906,
                column: "Code",
                value: "Urethritis (NGU)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 907,
                column: "Code",
                value: "Non-Hodgkin lymphoma");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 908,
                column: "Code",
                value: "Skin cancer (non-melanoma)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 909,
                column: "Code",
                value: "Squamous cell carcinoma");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 910,
                column: "Code",
                value: "Basal cell carcinoma");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 911,
                column: "Code",
                value: "Noonan syndrome");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 912,
                column: "Code",
                value: "Nosebleed");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 913,
                column: "Code",
                value: "Obesity");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 914,
                column: "Code",
                value: "Obsessive compulsive disorder (OCD)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 915,
                column: "Code",
                value: "Occupational therapy");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 916,
                column: "Code",
                value: "Oesophageal cancer");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 917,
                column: "Code",
                value: "Orthodontics");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 918,
                column: "Code",
                value: "Osteoarthritis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 919,
                column: "Code",
                value: "Osteomyelitis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 920,
                column: "Code",
                value: "Osteopathy");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 921,
                column: "Code",
                value: "Osteoporosis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 922,
                column: "Code",
                value: "Ovarian cancer");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 923,
                column: "Code",
                value: "Ovarian cyst");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 924,
                column: "Code",
                value: "Overactive thyroid (hyperthyroidism)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 925,
                column: "Code",
                value: "Hyperthyroidism");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 926,
                column: "Code",
                value: "Pacemaker implantation");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 927,
                column: "Code",
                value: "Paget's disease of bone");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 928,
                column: "Code",
                value: "Paget's disease of the nipple");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 929,
                column: "Code",
                value: "Pancreas transplant");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 930,
                column: "Code",
                value: "Pancreatic cancer");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 931,
                column: "Code",
                value: "Paralysis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 932,
                column: "Code",
                value: "Parkinson's disease");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 933,
                column: "Code",
                value: "Pelvic inflammatory disease");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 934,
                column: "Code",
                value: "Pelvic organ prolapse");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 935,
                column: "Code",
                value: "Prolapse (pelvic organ)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 936,
                column: "Code",
                value: "Pemphigus vulgaris");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 937,
                column: "Code",
                value: "Perforated eardrum");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 938,
                column: "Code",
                value: "Eardrum (burst)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 939,
                column: "Code",
                value: "Pericarditis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 940,
                column: "Code",
                value: "Peripheral arterial disease (PAD)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 941,
                column: "Code",
                value: "Peripheral neuropathy");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 942,
                column: "Code",
                value: "Peritonitis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 943,
                column: "Code",
                value: "Phobias");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 944,
                column: "Code",
                value: "Physiotherapy");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 945,
                column: "Code",
                value: "Piles (haemorrhoids)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 946,
                column: "Code",
                value: "Piles");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 947,
                column: "Code",
                value: "Pilonidal sinus");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 948,
                column: "Code",
                value: "Plastic surgery");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 949,
                column: "Code",
                value: "Pneumonia");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 950,
                column: "Code",
                value: "Poisoning");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 951,
                column: "Code",
                value: "Polycystic ovary syndrome");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 952,
                column: "Code",
                value: "Polycythaemia");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 953,
                column: "Code",
                value: "Polymyalgia rheumatica");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 954,
                column: "Code",
                value: "Post-herpetic neuralgia");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 955,
                column: "Code",
                value: "Postnatal depression");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 956,
                column: "Code",
                value: "Post-polio syndrome");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 957,
                column: "Code",
                value: "Post-traumatic stress disorder (PTSD)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 958,
                column: "Code",
                value: "Prader-Willi syndrome");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 959,
                column: "Code",
                value: "Pre-eclampsia");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 960,
                column: "Code",
                value: "PMS (premenstrual syndrome)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 961,
                column: "Code",
                value: "Pressure ulcers (pressure sores)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 962,
                column: "Code",
                value: "Priapism (painful erections)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 963,
                column: "Code",
                value: "Primary biliary cholangitis (primary biliary cirrhosis)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 964,
                column: "Code",
                value: "Progressive supranuclear palsy");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 965,
                column: "Code",
                value: "Prostate cancer");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 966,
                column: "Code",
                value: "Benign prostate enlargement");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 967,
                column: "Code",
                value: "Prostate enlargement");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 968,
                column: "Code",
                value: "Psoriasis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 969,
                column: "Code",
                value: "Psychosis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 970,
                column: "Code",
                value: "Pulmonary embolism");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 971,
                column: "Code",
                value: "Pulmonary hypertension");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 972,
                column: "Code",
                value: "Rabies");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 973,
                column: "Code",
                value: "Radiotherapy");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 974,
                column: "Code",
                value: "Raynaud's");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 975,
                column: "Code",
                value: "Reactive arthritis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 976,
                column: "Code",
                value: "Rectal examination");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 977,
                column: "Code",
                value: "Repetitive strain injury (RSI)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 978,
                column: "Code",
                value: "Restless legs syndrome");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 979,
                column: "Code",
                value: "Restricted growth (dwarfism)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 980,
                column: "Code",
                value: "Dwarfism");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 981,
                column: "Code",
                value: "Detached retina (retinal detachment)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 982,
                column: "Code",
                value: "Retinal detachment");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 983,
                column: "Code",
                value: "Rhesus disease");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 984,
                column: "Code",
                value: "Rheumatic fever");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 985,
                column: "Code",
                value: "Rheumatoid arthritis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 986,
                column: "Code",
                value: "Rickets and osteomalacia");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 987,
                column: "Code",
                value: "Osteomalacia");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 988,
                column: "Code",
                value: "Root canal treatment");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 989,
                column: "Code",
                value: "Rosacea");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 990,
                column: "Code",
                value: "Rubella (german measles)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 991,
                column: "Code",
                value: "Scabies");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 992,
                column: "Code",
                value: "Scars");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 993,
                column: "Code",
                value: "Schizophrenia");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 994,
                column: "Code",
                value: "Sciatica");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 995,
                column: "Code",
                value: "Buttock pain");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 996,
                column: "Code",
                value: "Scoliosis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 997,
                column: "Code",
                value: "Scurvy");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 998,
                column: "Code",
                value: "Seasonal affective disorder (SAD)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 999,
                column: "Code",
                value: "Self-harm");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1000,
                column: "Code",
                value: "Sepsis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1001,
                column: "Code",
                value: "Severe head injury");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1002,
                column: "Code",
                value: "Shingles");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1003,
                column: "Code",
                value: "Short-sightedness (myopia)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1004,
                column: "Code",
                value: "Myopia");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1005,
                column: "Code",
                value: "Shoulder pain");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1006,
                column: "Code",
                value: "Sickle cell disease");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1007,
                column: "Code",
                value: "Sjögren's syndrome");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1008,
                column: "Code",
                value: "Sleep paralysis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1009,
                column: "Code",
                value: "Slipped disc");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1010,
                column: "Code",
                value: "Small bowel transplant");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1011,
                column: "Code",
                value: "Bowel transplant");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1012,
                column: "Code",
                value: "Snoring");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1013,
                column: "Code",
                value: "Spina bifida");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1014,
                column: "Code",
                value: "Spinal muscular atrophy");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1015,
                column: "Code",
                value: "Sports injuries");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1016,
                column: "Code",
                value: "Sprains and strains");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1017,
                column: "Code",
                value: "Squint");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1018,
                column: "Code",
                value: "Selective serotonin reuptake inhibitors (SSRIs)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1019,
                column: "Code",
                value: "Stammering");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1020,
                column: "Code",
                value: "Stuttering");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1021,
                column: "Code",
                value: "Statins");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1022,
                column: "Code",
                value: "Stem cell and bone marrow transplants");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1023,
                column: "Code",
                value: "Stillbirth");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1024,
                column: "Code",
                value: "Stomach cancer");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1025,
                column: "Code",
                value: "Stomach ulcer");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1026,
                column: "Code",
                value: "Stroke");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1027,
                column: "Code",
                value: "Subarachnoid haemorrhage");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1028,
                column: "Code",
                value: "Brain haemorrhage");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1029,
                column: "Code",
                value: "Subdural haematoma");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1030,
                column: "Code",
                value: "Help for suicidal thoughts");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1031,
                column: "Code",
                value: "Suicidal thoughts");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1032,
                column: "Code",
                value: "Supraventricular tachycardia (SVT)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1033,
                column: "Code",
                value: "Dysphagia (swallowing problems)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1034,
                column: "Code",
                value: "Swallowing problems");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1035,
                column: "Code",
                value: "Syphilis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1036,
                column: "Code",
                value: "Coccydynia (tailbone pain)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1037,
                column: "Code",
                value: "Tay-Sachs disease");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1038,
                column: "Code",
                value: "Teeth grinding (bruxism)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1039,
                column: "Code",
                value: "Bruxism");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1040,
                column: "Code",
                value: "Tendonitis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1041,
                column: "Code",
                value: "Tennis elbow");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1042,
                column: "Code",
                value: "Testicle lumps and swellings");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1043,
                column: "Code",
                value: "Testicular cancer");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1044,
                column: "Code",
                value: "Thalassaemia");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1045,
                column: "Code",
                value: "Threadworms");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1046,
                column: "Code",
                value: "Thyroid cancer");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1047,
                column: "Code",
                value: "Tick-borne encephalitis (TBE)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1048,
                column: "Code",
                value: "Tics");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1049,
                column: "Code",
                value: "Tinnitus");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1050,
                column: "Code",
                value: "Tonsillitis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1051,
                column: "Code",
                value: "Quinsy");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1052,
                column: "Code",
                value: "Tourette's syndrome");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1053,
                column: "Code",
                value: "Toxocariasis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1054,
                column: "Code",
                value: "Toxoplasmosis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1055,
                column: "Code",
                value: "Tracheostomy");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1056,
                column: "Code",
                value: "Transient ischaemic attack (TIA)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1057,
                column: "Code",
                value: "TIA");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1058,
                column: "Code",
                value: "Transurethral resection of the prostate (TURP)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1059,
                column: "Code",
                value: "Travel vaccinations");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1060,
                column: "Code",
                value: "Trichomoniasis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1061,
                column: "Code",
                value: "Trichotillomania (hair pulling disorder)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1062,
                column: "Code",
                value: "Trigeminal neuralgia");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1063,
                column: "Code",
                value: "Trigger finger");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1064,
                column: "Code",
                value: "Tuberculosis (TB)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1065,
                column: "Code",
                value: "Tuberous sclerosis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1066,
                column: "Code",
                value: "Turner syndrome");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1067,
                column: "Code",
                value: "Type 2 diabetes");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1068,
                column: "Code",
                value: "Diabetes (type 2)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1069,
                column: "Code",
                value: "Typhoid fever");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1070,
                column: "Code",
                value: "Ulcerative colitis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1071,
                column: "Code",
                value: "Umbilical hernia repair");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1072,
                column: "Code",
                value: "Hernia (umbilical)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1073,
                column: "Code",
                value: "Underactive thyroid (hypothyroidism)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1074,
                column: "Code",
                value: "Hypothyroidism");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1075,
                column: "Code",
                value: "Undescended testicles");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1076,
                column: "Code",
                value: "Urinary catheter");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1077,
                column: "Code",
                value: "Urinary incontinence");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1078,
                column: "Code",
                value: "Incontinence (urinary)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1079,
                column: "Code",
                value: "Uveitis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1080,
                column: "Code",
                value: "Vaginal cancer");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1081,
                column: "Code",
                value: "Vaginismus");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1082,
                column: "Code",
                value: "Varicose eczema");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1083,
                column: "Code",
                value: "Eczema (varicose)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1084,
                column: "Code",
                value: "Varicose veins");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1085,
                column: "Code",
                value: "Vascular dementia");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1086,
                column: "Code",
                value: "Dementia (vascular)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1087,
                column: "Code",
                value: "Vertigo");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1088,
                column: "Code",
                value: "Vitamin B12 or folate deficiency anaemia");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1089,
                column: "Code",
                value: "Anaemia (vitamin B12 or folate deficiency)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1090,
                column: "Code",
                value: "Vitamins and minerals");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1091,
                column: "Code",
                value: "Vitiligo");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1092,
                column: "Code",
                value: "Vulval cancer");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1093,
                column: "Code",
                value: "Watering eyes");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1094,
                column: "Code",
                value: "Weight loss surgery");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1095,
                column: "Code",
                value: "Whiplash");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1096,
                column: "Code",
                value: "Wisdom tooth removal");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1097,
                column: "Code",
                value: "Womb (uterus) cancer");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1098,
                column: "Code",
                value: "Uterine (womb) cancer");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1099,
                column: "Code",
                value: "Endometrial cancer");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1100,
                column: "Code",
                value: "Yellow fever");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1101,
                column: "Code",
                value: "Abortion");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1102,
                column: "Code",
                value: "Bird flu");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1103,
                column: "Code",
                value: "Avian flu");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1104,
                column: "Code",
                value: "Antifungal medicines");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1105,
                column: "Code",
                value: "Blood transfusion");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1106,
                column: "Code",
                value: "Ringworm");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1107,
                column: "Code",
                value: "Early menopause");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1108,
                column: "Code",
                value: "Menopause (early)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1109,
                column: "Code",
                value: "Floaters and flashes in the eyes");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1110,
                column: "Code",
                value: "Eye floaters");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1111,
                column: "Code",
                value: "Your contraception guide");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1112,
                column: "Code",
                value: "Dementia guide");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1113,
                column: "Code",
                value: "Fitness Studio exercise videos");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1114,
                column: "Code",
                value: "NHS Health Check");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1115,
                column: "Code",
                value: "Worms in humans");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1116,
                column: "Code",
                value: "Hookworm");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1117,
                column: "Code",
                value: "Tapeworm");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1118,
                column: "Code",
                value: "Roundworm");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1119,
                column: "Code",
                value: "Tremor or shaking hands");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1120,
                column: "Code",
                value: "tremor");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1121,
                column: "Code",
                value: "essential tremor");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1122,
                column: "Code",
                value: "shaking");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1123,
                column: "Code",
                value: "Low white blood cell count");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1124,
                column: "Code",
                value: "White blood cell count (low)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1125,
                column: "Code",
                value: "Bunions");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1126,
                column: "Code",
                value: "Lost or changed sense of smell");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1127,
                column: "Code",
                value: "Anosmia");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1128,
                column: "Code",
                value: "Sense of smell (lost/changed)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1129,
                column: "Code",
                value: "Soiling (child pooing their pants)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1130,
                column: "Code",
                value: "Oral thrush (mouth thrush)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1131,
                column: "Code",
                value: "Mouth thrush");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1132,
                column: "Code",
                value: "Temporal arteritis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1133,
                column: "Code",
                value: "Giant cell arteritis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1134,
                column: "Code",
                value: "Memory loss (amnesia)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1135,
                column: "Code",
                value: "Amnesia");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1136,
                column: "Code",
                value: "Headaches");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1137,
                column: "Code",
                value: "Sinusitis (sinus infection)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1138,
                column: "Code",
                value: "Sinusitis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1139,
                column: "Code",
                value: "Thrush in men and women");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1140,
                column: "Code",
                value: "Cold sores");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1141,
                column: "Code",
                value: "Group B strep");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1142,
                column: "Code",
                value: "Skin picking disorder");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1143,
                column: "Code",
                value: "Hyperparathyroidism");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1144,
                column: "Code",
                value: "Hypoparathyroidism");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1145,
                column: "Code",
                value: "Ear infections");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1146,
                column: "Code",
                value: "Twitching eyes and muscles");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1147,
                column: "Code",
                value: "Learning disabilities");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1148,
                column: "Code",
                value: "NHS screening");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1149,
                column: "Code",
                value: "Hormone headaches");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1150,
                column: "Code",
                value: "Headaches (hormone)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1151,
                column: "Code",
                value: "What to do if someone has a seizure (fit)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1152,
                column: "Code",
                value: "Seizures (fits)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1153,
                column: "Code",
                value: "Fits (seizures)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1154,
                column: "Code",
                value: "Complementary and alternative medicine");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1155,
                column: "Code",
                value: "End of life care");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1156,
                column: "Code",
                value: "Knocked-out tooth");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1157,
                column: "Code",
                value: "Tooth knocked out");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1158,
                column: "Code",
                value: "Chipped broken or cracked tooth");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1159,
                column: "Code",
                value: "Tooth (chipped or broken)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1160,
                column: "Code",
                value: "Diarrhoea and vomiting");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1161,
                column: "Code",
                value: "Tummy bug");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1162,
                column: "Code",
                value: "Vomiting");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1163,
                column: "Code",
                value: "Stomach bug");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1164,
                column: "Code",
                value: "Gastroenteritis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1165,
                column: "Code",
                value: "Diarrhoea");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1166,
                column: "Code",
                value: "Being sick");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1167,
                column: "Code",
                value: "Bullous pemphigoid");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1168,
                column: "Code",
                value: "Feeling sick (nausea)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1169,
                column: "Code",
                value: "Nausea");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1170,
                column: "Code",
                value: "Type 1 diabetes");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1171,
                column: "Code",
                value: "Diabetes (type 1)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1172,
                column: "Code",
                value: "Social care and support guide");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1173,
                column: "Code",
                value: "Middle East respiratory syndrome (MERS)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1174,
                column: "Code",
                value: "Monkeypox");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1175,
                column: "Code",
                value: "Diabetic eye screening 2");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1176,
                column: "Code",
                value: "Medical cannabis (and cannabis oils)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1177,
                column: "Code",
                value: "Cannabis oil (medical cannabis)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1178,
                column: "Code",
                value: "Diabetic eye screening 1");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1179,
                column: "Code",
                value: "Body odour (BO)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1180,
                column: "Code",
                value: "Human papillomavirus (HPV)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1181,
                column: "Code",
                value: "Plantar fasciitis");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1182,
                column: "Code",
                value: "Foot pain");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1183,
                column: "Code",
                value: "heel pain");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1184,
                column: "Code",
                value: "Toe pain");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1185,
                column: "Code",
                value: "ankle pain");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1186,
                column: "Code",
                value: "Autism");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1187,
                column: "Code",
                value: "Asperger's");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1188,
                column: "Code",
                value: "Cosmetic procedures");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1189,
                column: "Code",
                value: "Hand pain");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1190,
                column: "Code",
                value: "Swollen arms and hands (oedema)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1191,
                column: "Code",
                value: "Colonoscopy");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1192,
                column: "Code",
                value: "Genetic and genomic testing");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1193,
                column: "Code",
                value: "Sleep apnoea");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1194,
                column: "Code",
                value: "Vaccinations");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1195,
                column: "Code",
                value: "Mental health and wellbeing");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1196,
                column: "Code",
                value: "High temperature (fever) in adults");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1197,
                column: "Code",
                value: "Fever in adults");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1198,
                column: "Code",
                value: "Coronavirus (COVID-19)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1199,
                column: "Code",
                value: "Baby");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1200,
                column: "Code",
                value: "Do not attempt cardiopulmonary resuscitation (DNACPR) decisions");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1201,
                column: "Code",
                value: "Testicle pain");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1202,
                column: "Code",
                value: "Pain in testicles");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1203,
                column: "Code",
                value: "Hearing aids and implants");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1204,
                column: "Code",
                value: "Breast screening (mammogram)");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1205,
                column: "Code",
                value: "Smelly feet");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1206,
                column: "Code",
                value: "Academic attainment");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1207,
                column: "Code",
                value: "Ageing");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1208,
                column: "Code",
                value: "Aggression");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1209,
                column: "Code",
                value: "Antenatal care");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1210,
                column: "Code",
                value: "Blood donation");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1211,
                column: "Code",
                value: "Body image");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1212,
                column: "Code",
                value: "Breastfeeding");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1213,
                column: "Code",
                value: "Care homes");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1214,
                column: "Code",
                value: "Carers");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1215,
                column: "Code",
                value: "Child development");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1216,
                column: "Code",
                value: "Complementary therapies");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1217,
                column: "Code",
                value: "Contraception");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1218,
                column: "Code",
                value: "Domestic violence");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1219,
                column: "Code",
                value: "Eating well");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1220,
                column: "Code",
                value: "Exercise and sports");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1221,
                column: "Code",
                value: "General wellbeing");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1222,
                column: "Code",
                value: "Genetic screening");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1223,
                column: "Code",
                value: "Healthy volunteers");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1224,
                column: "Code",
                value: "Improving care and services");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1225,
                column: "Code",
                value: "Healthy lifestyle");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1226,
                column: "Code",
                value: "Long COVID");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1227,
                column: "Code",
                value: "Obesity risk");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1228,
                column: "Code",
                value: "Occupational health");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1229,
                column: "Code",
                value: "Parenting");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1230,
                column: "Code",
                value: "Public health");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1231,
                column: "Code",
                value: "Sleeping well");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1232,
                column: "Code",
                value: "Smoking");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1233,
                column: "Code",
                value: "Supplements");

            migrationBuilder.UpdateData(
                table: "SysRefIdentifierType",
                keyColumn: "Id",
                keyValue: 1,
                column: "Code",
                value: "ParticipantId");

            migrationBuilder.UpdateData(
                table: "SysRefIdentifierType",
                keyColumn: "Id",
                keyValue: 2,
                column: "Code",
                value: "NhsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "SysRefCommunicationLanguage",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Code", "Description" },
                values: new object[] { "1", "en-GB" });

            migrationBuilder.UpdateData(
                table: "SysRefCommunicationLanguage",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Code", "Description" },
                values: new object[] { "2", "cy-GB" });

            migrationBuilder.UpdateData(
                table: "SysRefDailyLifeImpact",
                keyColumn: "Id",
                keyValue: 1,
                column: "Code",
                value: "1");

            migrationBuilder.UpdateData(
                table: "SysRefDailyLifeImpact",
                keyColumn: "Id",
                keyValue: 2,
                column: "Code",
                value: "2");

            migrationBuilder.UpdateData(
                table: "SysRefDailyLifeImpact",
                keyColumn: "Id",
                keyValue: 3,
                column: "Code",
                value: "3");

            migrationBuilder.UpdateData(
                table: "SysRefDailyLifeImpact",
                keyColumn: "Id",
                keyValue: 4,
                column: "Code",
                value: "4");

            migrationBuilder.UpdateData(
                table: "SysRefGender",
                keyColumn: "Id",
                keyValue: 1,
                column: "Code",
                value: "1");

            migrationBuilder.UpdateData(
                table: "SysRefGender",
                keyColumn: "Id",
                keyValue: 2,
                column: "Code",
                value: "2");

            migrationBuilder.UpdateData(
                table: "SysRefGender",
                keyColumn: "Id",
                keyValue: 3,
                column: "Code",
                value: "3");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1,
                column: "Code",
                value: "1");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 2,
                column: "Code",
                value: "2");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 3,
                column: "Code",
                value: "3");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 4,
                column: "Code",
                value: "4");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 5,
                column: "Code",
                value: "5");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 6,
                column: "Code",
                value: "6");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 7,
                column: "Code",
                value: "7");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 8,
                column: "Code",
                value: "8");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 9,
                column: "Code",
                value: "9");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 10,
                column: "Code",
                value: "10");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 11,
                column: "Code",
                value: "11");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 12,
                column: "Code",
                value: "12");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 13,
                column: "Code",
                value: "13");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 14,
                column: "Code",
                value: "14");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 15,
                column: "Code",
                value: "15");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 16,
                column: "Code",
                value: "16");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 17,
                column: "Code",
                value: "17");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 18,
                column: "Code",
                value: "18");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 19,
                column: "Code",
                value: "19");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 20,
                column: "Code",
                value: "20");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 21,
                column: "Code",
                value: "21");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 22,
                column: "Code",
                value: "22");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 23,
                column: "Code",
                value: "23");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 24,
                column: "Code",
                value: "24");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 25,
                column: "Code",
                value: "25");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 26,
                column: "Code",
                value: "26");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 27,
                column: "Code",
                value: "27");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 28,
                column: "Code",
                value: "28");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 29,
                column: "Code",
                value: "29");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 30,
                column: "Code",
                value: "30");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 31,
                column: "Code",
                value: "31");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 32,
                column: "Code",
                value: "32");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 33,
                column: "Code",
                value: "33");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 34,
                column: "Code",
                value: "34");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 35,
                column: "Code",
                value: "35");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 36,
                column: "Code",
                value: "36");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 37,
                column: "Code",
                value: "37");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 38,
                column: "Code",
                value: "38");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 39,
                column: "Code",
                value: "39");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 40,
                column: "Code",
                value: "40");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 41,
                column: "Code",
                value: "41");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 42,
                column: "Code",
                value: "42");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 43,
                column: "Code",
                value: "43");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 44,
                column: "Code",
                value: "44");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 45,
                column: "Code",
                value: "45");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 46,
                column: "Code",
                value: "46");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 47,
                column: "Code",
                value: "47");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 48,
                column: "Code",
                value: "48");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 49,
                column: "Code",
                value: "49");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 50,
                column: "Code",
                value: "50");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 51,
                column: "Code",
                value: "51");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 52,
                column: "Code",
                value: "52");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 53,
                column: "Code",
                value: "53");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 54,
                column: "Code",
                value: "54");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 55,
                column: "Code",
                value: "55");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 56,
                column: "Code",
                value: "56");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 57,
                column: "Code",
                value: "57");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 58,
                column: "Code",
                value: "58");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 59,
                column: "Code",
                value: "59");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 60,
                column: "Code",
                value: "60");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 61,
                column: "Code",
                value: "61");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 62,
                column: "Code",
                value: "62");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 63,
                column: "Code",
                value: "63");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 64,
                column: "Code",
                value: "64");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 65,
                column: "Code",
                value: "65");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 66,
                column: "Code",
                value: "66");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 67,
                column: "Code",
                value: "67");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 68,
                column: "Code",
                value: "68");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 69,
                column: "Code",
                value: "69");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 70,
                column: "Code",
                value: "70");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 71,
                column: "Code",
                value: "71");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 72,
                column: "Code",
                value: "72");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 73,
                column: "Code",
                value: "73");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 74,
                column: "Code",
                value: "74");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 75,
                column: "Code",
                value: "75");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 76,
                column: "Code",
                value: "76");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 77,
                column: "Code",
                value: "77");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 78,
                column: "Code",
                value: "78");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 79,
                column: "Code",
                value: "79");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 80,
                column: "Code",
                value: "80");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 81,
                column: "Code",
                value: "81");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 82,
                column: "Code",
                value: "82");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 83,
                column: "Code",
                value: "83");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 84,
                column: "Code",
                value: "84");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 85,
                column: "Code",
                value: "85");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 86,
                column: "Code",
                value: "86");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 87,
                column: "Code",
                value: "87");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 88,
                column: "Code",
                value: "88");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 89,
                column: "Code",
                value: "89");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 90,
                column: "Code",
                value: "90");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 91,
                column: "Code",
                value: "91");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 92,
                column: "Code",
                value: "92");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 93,
                column: "Code",
                value: "93");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 94,
                column: "Code",
                value: "94");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 95,
                column: "Code",
                value: "95");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 96,
                column: "Code",
                value: "96");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 97,
                column: "Code",
                value: "97");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 98,
                column: "Code",
                value: "98");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 99,
                column: "Code",
                value: "99");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 100,
                column: "Code",
                value: "100");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 101,
                column: "Code",
                value: "101");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 102,
                column: "Code",
                value: "102");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 103,
                column: "Code",
                value: "103");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 104,
                column: "Code",
                value: "104");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 105,
                column: "Code",
                value: "105");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 106,
                column: "Code",
                value: "106");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 107,
                column: "Code",
                value: "107");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 108,
                column: "Code",
                value: "108");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 109,
                column: "Code",
                value: "109");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 110,
                column: "Code",
                value: "110");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 111,
                column: "Code",
                value: "111");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 112,
                column: "Code",
                value: "112");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 113,
                column: "Code",
                value: "113");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 114,
                column: "Code",
                value: "114");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 115,
                column: "Code",
                value: "115");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 116,
                column: "Code",
                value: "116");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 117,
                column: "Code",
                value: "117");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 118,
                column: "Code",
                value: "118");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 119,
                column: "Code",
                value: "119");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 120,
                column: "Code",
                value: "120");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 121,
                column: "Code",
                value: "121");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 122,
                column: "Code",
                value: "122");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 123,
                column: "Code",
                value: "123");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 124,
                column: "Code",
                value: "124");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 125,
                column: "Code",
                value: "125");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 126,
                column: "Code",
                value: "126");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 127,
                column: "Code",
                value: "127");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 128,
                column: "Code",
                value: "128");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 129,
                column: "Code",
                value: "129");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 130,
                column: "Code",
                value: "130");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 131,
                column: "Code",
                value: "131");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 132,
                column: "Code",
                value: "132");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 133,
                column: "Code",
                value: "133");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 134,
                column: "Code",
                value: "134");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 135,
                column: "Code",
                value: "135");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 136,
                column: "Code",
                value: "136");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 137,
                column: "Code",
                value: "137");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 138,
                column: "Code",
                value: "138");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 139,
                column: "Code",
                value: "139");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 140,
                column: "Code",
                value: "140");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 141,
                column: "Code",
                value: "141");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 142,
                column: "Code",
                value: "142");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 143,
                column: "Code",
                value: "143");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 144,
                column: "Code",
                value: "144");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 145,
                column: "Code",
                value: "145");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 146,
                column: "Code",
                value: "146");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 147,
                column: "Code",
                value: "147");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 148,
                column: "Code",
                value: "148");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 149,
                column: "Code",
                value: "149");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 150,
                column: "Code",
                value: "150");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 151,
                column: "Code",
                value: "151");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 152,
                column: "Code",
                value: "152");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 153,
                column: "Code",
                value: "153");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 154,
                column: "Code",
                value: "154");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 155,
                column: "Code",
                value: "155");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 156,
                column: "Code",
                value: "156");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 157,
                column: "Code",
                value: "157");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 158,
                column: "Code",
                value: "158");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 159,
                column: "Code",
                value: "159");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 160,
                column: "Code",
                value: "160");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 161,
                column: "Code",
                value: "161");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 162,
                column: "Code",
                value: "162");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 163,
                column: "Code",
                value: "163");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 164,
                column: "Code",
                value: "164");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 165,
                column: "Code",
                value: "165");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 166,
                column: "Code",
                value: "166");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 167,
                column: "Code",
                value: "167");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 168,
                column: "Code",
                value: "168");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 169,
                column: "Code",
                value: "169");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 170,
                column: "Code",
                value: "170");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 171,
                column: "Code",
                value: "171");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 172,
                column: "Code",
                value: "172");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 173,
                column: "Code",
                value: "173");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 174,
                column: "Code",
                value: "174");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 175,
                column: "Code",
                value: "175");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 176,
                column: "Code",
                value: "176");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 177,
                column: "Code",
                value: "177");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 178,
                column: "Code",
                value: "178");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 179,
                column: "Code",
                value: "179");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 180,
                column: "Code",
                value: "180");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 181,
                column: "Code",
                value: "181");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 182,
                column: "Code",
                value: "182");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 183,
                column: "Code",
                value: "183");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 184,
                column: "Code",
                value: "184");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 185,
                column: "Code",
                value: "185");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 186,
                column: "Code",
                value: "186");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 187,
                column: "Code",
                value: "187");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 188,
                column: "Code",
                value: "188");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 189,
                column: "Code",
                value: "189");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 190,
                column: "Code",
                value: "190");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 191,
                column: "Code",
                value: "191");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 192,
                column: "Code",
                value: "192");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 193,
                column: "Code",
                value: "193");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 194,
                column: "Code",
                value: "194");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 195,
                column: "Code",
                value: "195");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 196,
                column: "Code",
                value: "196");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 197,
                column: "Code",
                value: "197");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 198,
                column: "Code",
                value: "198");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 199,
                column: "Code",
                value: "199");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 200,
                column: "Code",
                value: "200");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 201,
                column: "Code",
                value: "201");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 202,
                column: "Code",
                value: "202");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 203,
                column: "Code",
                value: "203");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 204,
                column: "Code",
                value: "204");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 205,
                column: "Code",
                value: "205");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 206,
                column: "Code",
                value: "206");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 207,
                column: "Code",
                value: "207");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 208,
                column: "Code",
                value: "208");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 209,
                column: "Code",
                value: "209");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 210,
                column: "Code",
                value: "210");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 211,
                column: "Code",
                value: "211");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 212,
                column: "Code",
                value: "212");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 213,
                column: "Code",
                value: "213");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 214,
                column: "Code",
                value: "214");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 215,
                column: "Code",
                value: "215");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 216,
                column: "Code",
                value: "216");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 217,
                column: "Code",
                value: "217");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 218,
                column: "Code",
                value: "218");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 219,
                column: "Code",
                value: "219");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 220,
                column: "Code",
                value: "220");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 221,
                column: "Code",
                value: "221");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 222,
                column: "Code",
                value: "222");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 223,
                column: "Code",
                value: "223");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 224,
                column: "Code",
                value: "224");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 225,
                column: "Code",
                value: "225");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 226,
                column: "Code",
                value: "226");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 227,
                column: "Code",
                value: "227");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 228,
                column: "Code",
                value: "228");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 229,
                column: "Code",
                value: "229");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 230,
                column: "Code",
                value: "230");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 231,
                column: "Code",
                value: "231");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 232,
                column: "Code",
                value: "232");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 233,
                column: "Code",
                value: "233");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 234,
                column: "Code",
                value: "234");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 235,
                column: "Code",
                value: "235");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 236,
                column: "Code",
                value: "236");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 237,
                column: "Code",
                value: "237");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 238,
                column: "Code",
                value: "238");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 239,
                column: "Code",
                value: "239");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 240,
                column: "Code",
                value: "240");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 241,
                column: "Code",
                value: "241");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 242,
                column: "Code",
                value: "242");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 243,
                column: "Code",
                value: "243");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 244,
                column: "Code",
                value: "244");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 245,
                column: "Code",
                value: "245");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 246,
                column: "Code",
                value: "246");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 247,
                column: "Code",
                value: "247");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 248,
                column: "Code",
                value: "248");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 249,
                column: "Code",
                value: "249");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 250,
                column: "Code",
                value: "250");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 251,
                column: "Code",
                value: "251");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 252,
                column: "Code",
                value: "252");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 253,
                column: "Code",
                value: "253");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 254,
                column: "Code",
                value: "254");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 255,
                column: "Code",
                value: "255");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 256,
                column: "Code",
                value: "256");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 257,
                column: "Code",
                value: "257");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 258,
                column: "Code",
                value: "258");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 259,
                column: "Code",
                value: "259");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 260,
                column: "Code",
                value: "260");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 261,
                column: "Code",
                value: "261");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 262,
                column: "Code",
                value: "262");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 263,
                column: "Code",
                value: "263");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 264,
                column: "Code",
                value: "264");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 265,
                column: "Code",
                value: "265");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 266,
                column: "Code",
                value: "266");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 267,
                column: "Code",
                value: "267");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 268,
                column: "Code",
                value: "268");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 269,
                column: "Code",
                value: "269");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 270,
                column: "Code",
                value: "270");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 271,
                column: "Code",
                value: "271");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 272,
                column: "Code",
                value: "272");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 273,
                column: "Code",
                value: "273");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 274,
                column: "Code",
                value: "274");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 275,
                column: "Code",
                value: "275");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 276,
                column: "Code",
                value: "276");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 277,
                column: "Code",
                value: "277");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 278,
                column: "Code",
                value: "278");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 279,
                column: "Code",
                value: "279");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 280,
                column: "Code",
                value: "280");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 281,
                column: "Code",
                value: "281");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 282,
                column: "Code",
                value: "282");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 283,
                column: "Code",
                value: "283");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 284,
                column: "Code",
                value: "284");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 285,
                column: "Code",
                value: "285");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 286,
                column: "Code",
                value: "286");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 287,
                column: "Code",
                value: "287");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 288,
                column: "Code",
                value: "288");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 289,
                column: "Code",
                value: "289");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 290,
                column: "Code",
                value: "290");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 291,
                column: "Code",
                value: "291");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 292,
                column: "Code",
                value: "292");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 293,
                column: "Code",
                value: "293");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 294,
                column: "Code",
                value: "294");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 295,
                column: "Code",
                value: "295");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 296,
                column: "Code",
                value: "296");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 297,
                column: "Code",
                value: "297");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 298,
                column: "Code",
                value: "298");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 299,
                column: "Code",
                value: "299");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 300,
                column: "Code",
                value: "300");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 301,
                column: "Code",
                value: "301");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 302,
                column: "Code",
                value: "302");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 303,
                column: "Code",
                value: "303");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 304,
                column: "Code",
                value: "304");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 305,
                column: "Code",
                value: "305");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 306,
                column: "Code",
                value: "306");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 307,
                column: "Code",
                value: "307");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 308,
                column: "Code",
                value: "308");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 309,
                column: "Code",
                value: "309");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 310,
                column: "Code",
                value: "310");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 311,
                column: "Code",
                value: "311");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 312,
                column: "Code",
                value: "312");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 313,
                column: "Code",
                value: "313");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 314,
                column: "Code",
                value: "314");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 315,
                column: "Code",
                value: "315");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 316,
                column: "Code",
                value: "316");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 317,
                column: "Code",
                value: "317");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 318,
                column: "Code",
                value: "318");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 319,
                column: "Code",
                value: "319");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 320,
                column: "Code",
                value: "320");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 321,
                column: "Code",
                value: "321");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 322,
                column: "Code",
                value: "322");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 323,
                column: "Code",
                value: "323");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 324,
                column: "Code",
                value: "324");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 325,
                column: "Code",
                value: "325");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 326,
                column: "Code",
                value: "326");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 327,
                column: "Code",
                value: "327");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 328,
                column: "Code",
                value: "328");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 329,
                column: "Code",
                value: "329");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 330,
                column: "Code",
                value: "330");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 331,
                column: "Code",
                value: "331");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 332,
                column: "Code",
                value: "332");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 333,
                column: "Code",
                value: "333");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 334,
                column: "Code",
                value: "334");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 335,
                column: "Code",
                value: "335");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 336,
                column: "Code",
                value: "336");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 337,
                column: "Code",
                value: "337");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 338,
                column: "Code",
                value: "338");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 339,
                column: "Code",
                value: "339");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 340,
                column: "Code",
                value: "340");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 341,
                column: "Code",
                value: "341");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 342,
                column: "Code",
                value: "342");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 343,
                column: "Code",
                value: "343");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 344,
                column: "Code",
                value: "344");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 345,
                column: "Code",
                value: "345");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 346,
                column: "Code",
                value: "346");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 347,
                column: "Code",
                value: "347");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 348,
                column: "Code",
                value: "348");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 349,
                column: "Code",
                value: "349");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 350,
                column: "Code",
                value: "350");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 351,
                column: "Code",
                value: "351");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 352,
                column: "Code",
                value: "352");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 353,
                column: "Code",
                value: "353");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 354,
                column: "Code",
                value: "354");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 355,
                column: "Code",
                value: "355");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 356,
                column: "Code",
                value: "356");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 357,
                column: "Code",
                value: "357");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 358,
                column: "Code",
                value: "358");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 359,
                column: "Code",
                value: "359");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 360,
                column: "Code",
                value: "360");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 361,
                column: "Code",
                value: "361");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 362,
                column: "Code",
                value: "362");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 363,
                column: "Code",
                value: "363");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 364,
                column: "Code",
                value: "364");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 365,
                column: "Code",
                value: "365");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 366,
                column: "Code",
                value: "366");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 367,
                column: "Code",
                value: "367");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 368,
                column: "Code",
                value: "368");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 369,
                column: "Code",
                value: "369");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 370,
                column: "Code",
                value: "370");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 371,
                column: "Code",
                value: "371");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 372,
                column: "Code",
                value: "372");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 373,
                column: "Code",
                value: "373");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 374,
                column: "Code",
                value: "374");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 375,
                column: "Code",
                value: "375");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 376,
                column: "Code",
                value: "376");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 377,
                column: "Code",
                value: "377");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 378,
                column: "Code",
                value: "378");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 379,
                column: "Code",
                value: "379");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 380,
                column: "Code",
                value: "380");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 381,
                column: "Code",
                value: "381");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 382,
                column: "Code",
                value: "382");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 383,
                column: "Code",
                value: "383");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 384,
                column: "Code",
                value: "384");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 385,
                column: "Code",
                value: "385");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 386,
                column: "Code",
                value: "386");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 387,
                column: "Code",
                value: "387");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 388,
                column: "Code",
                value: "388");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 389,
                column: "Code",
                value: "389");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 390,
                column: "Code",
                value: "390");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 391,
                column: "Code",
                value: "391");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 392,
                column: "Code",
                value: "392");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 393,
                column: "Code",
                value: "393");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 394,
                column: "Code",
                value: "394");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 395,
                column: "Code",
                value: "395");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 396,
                column: "Code",
                value: "396");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 397,
                column: "Code",
                value: "397");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 398,
                column: "Code",
                value: "398");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 399,
                column: "Code",
                value: "399");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 400,
                column: "Code",
                value: "400");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 401,
                column: "Code",
                value: "401");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 402,
                column: "Code",
                value: "402");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 403,
                column: "Code",
                value: "403");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 404,
                column: "Code",
                value: "404");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 405,
                column: "Code",
                value: "405");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 406,
                column: "Code",
                value: "406");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 407,
                column: "Code",
                value: "407");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 408,
                column: "Code",
                value: "408");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 409,
                column: "Code",
                value: "409");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 410,
                column: "Code",
                value: "410");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 411,
                column: "Code",
                value: "411");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 412,
                column: "Code",
                value: "412");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 413,
                column: "Code",
                value: "413");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 414,
                column: "Code",
                value: "414");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 415,
                column: "Code",
                value: "415");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 416,
                column: "Code",
                value: "416");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 417,
                column: "Code",
                value: "417");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 418,
                column: "Code",
                value: "418");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 419,
                column: "Code",
                value: "419");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 420,
                column: "Code",
                value: "420");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 421,
                column: "Code",
                value: "421");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 422,
                column: "Code",
                value: "422");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 423,
                column: "Code",
                value: "423");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 424,
                column: "Code",
                value: "424");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 425,
                column: "Code",
                value: "425");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 426,
                column: "Code",
                value: "426");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 427,
                column: "Code",
                value: "427");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 428,
                column: "Code",
                value: "428");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 429,
                column: "Code",
                value: "429");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 430,
                column: "Code",
                value: "430");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 431,
                column: "Code",
                value: "431");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 432,
                column: "Code",
                value: "432");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 433,
                column: "Code",
                value: "433");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 434,
                column: "Code",
                value: "434");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 435,
                column: "Code",
                value: "435");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 436,
                column: "Code",
                value: "436");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 437,
                column: "Code",
                value: "437");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 438,
                column: "Code",
                value: "438");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 439,
                column: "Code",
                value: "439");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 440,
                column: "Code",
                value: "440");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 441,
                column: "Code",
                value: "441");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 442,
                column: "Code",
                value: "442");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 443,
                column: "Code",
                value: "443");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 444,
                column: "Code",
                value: "444");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 445,
                column: "Code",
                value: "445");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 446,
                column: "Code",
                value: "446");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 447,
                column: "Code",
                value: "447");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 448,
                column: "Code",
                value: "448");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 449,
                column: "Code",
                value: "449");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 450,
                column: "Code",
                value: "450");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 451,
                column: "Code",
                value: "451");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 452,
                column: "Code",
                value: "452");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 453,
                column: "Code",
                value: "453");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 454,
                column: "Code",
                value: "454");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 455,
                column: "Code",
                value: "455");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 456,
                column: "Code",
                value: "456");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 457,
                column: "Code",
                value: "457");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 458,
                column: "Code",
                value: "458");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 459,
                column: "Code",
                value: "459");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 460,
                column: "Code",
                value: "460");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 461,
                column: "Code",
                value: "461");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 462,
                column: "Code",
                value: "462");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 463,
                column: "Code",
                value: "463");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 464,
                column: "Code",
                value: "464");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 465,
                column: "Code",
                value: "465");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 466,
                column: "Code",
                value: "466");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 467,
                column: "Code",
                value: "467");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 468,
                column: "Code",
                value: "468");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 469,
                column: "Code",
                value: "469");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 470,
                column: "Code",
                value: "470");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 471,
                column: "Code",
                value: "471");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 472,
                column: "Code",
                value: "472");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 473,
                column: "Code",
                value: "473");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 474,
                column: "Code",
                value: "474");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 475,
                column: "Code",
                value: "475");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 476,
                column: "Code",
                value: "476");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 477,
                column: "Code",
                value: "477");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 478,
                column: "Code",
                value: "478");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 479,
                column: "Code",
                value: "479");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 480,
                column: "Code",
                value: "480");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 481,
                column: "Code",
                value: "481");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 482,
                column: "Code",
                value: "482");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 483,
                column: "Code",
                value: "483");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 484,
                column: "Code",
                value: "484");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 485,
                column: "Code",
                value: "485");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 486,
                column: "Code",
                value: "486");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 487,
                column: "Code",
                value: "487");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 488,
                column: "Code",
                value: "488");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 489,
                column: "Code",
                value: "489");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 490,
                column: "Code",
                value: "490");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 491,
                column: "Code",
                value: "491");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 492,
                column: "Code",
                value: "492");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 493,
                column: "Code",
                value: "493");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 494,
                column: "Code",
                value: "494");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 495,
                column: "Code",
                value: "495");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 496,
                column: "Code",
                value: "496");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 497,
                column: "Code",
                value: "497");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 498,
                column: "Code",
                value: "498");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 499,
                column: "Code",
                value: "499");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 500,
                column: "Code",
                value: "500");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 501,
                column: "Code",
                value: "501");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 502,
                column: "Code",
                value: "502");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 503,
                column: "Code",
                value: "503");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 504,
                column: "Code",
                value: "504");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 505,
                column: "Code",
                value: "505");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 506,
                column: "Code",
                value: "506");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 507,
                column: "Code",
                value: "507");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 508,
                column: "Code",
                value: "508");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 509,
                column: "Code",
                value: "509");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 510,
                column: "Code",
                value: "510");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 511,
                column: "Code",
                value: "511");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 512,
                column: "Code",
                value: "512");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 513,
                column: "Code",
                value: "513");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 514,
                column: "Code",
                value: "514");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 515,
                column: "Code",
                value: "515");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 516,
                column: "Code",
                value: "516");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 517,
                column: "Code",
                value: "517");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 518,
                column: "Code",
                value: "518");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 519,
                column: "Code",
                value: "519");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 520,
                column: "Code",
                value: "520");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 521,
                column: "Code",
                value: "521");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 522,
                column: "Code",
                value: "522");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 523,
                column: "Code",
                value: "523");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 524,
                column: "Code",
                value: "524");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 525,
                column: "Code",
                value: "525");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 526,
                column: "Code",
                value: "526");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 527,
                column: "Code",
                value: "527");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 528,
                column: "Code",
                value: "528");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 529,
                column: "Code",
                value: "529");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 530,
                column: "Code",
                value: "530");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 531,
                column: "Code",
                value: "531");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 532,
                column: "Code",
                value: "532");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 533,
                column: "Code",
                value: "533");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 534,
                column: "Code",
                value: "534");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 535,
                column: "Code",
                value: "535");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 536,
                column: "Code",
                value: "536");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 537,
                column: "Code",
                value: "537");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 538,
                column: "Code",
                value: "538");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 539,
                column: "Code",
                value: "539");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 540,
                column: "Code",
                value: "540");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 541,
                column: "Code",
                value: "541");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 542,
                column: "Code",
                value: "542");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 543,
                column: "Code",
                value: "543");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 544,
                column: "Code",
                value: "544");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 545,
                column: "Code",
                value: "545");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 546,
                column: "Code",
                value: "546");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 547,
                column: "Code",
                value: "547");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 548,
                column: "Code",
                value: "548");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 549,
                column: "Code",
                value: "549");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 550,
                column: "Code",
                value: "550");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 551,
                column: "Code",
                value: "551");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 552,
                column: "Code",
                value: "552");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 553,
                column: "Code",
                value: "553");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 554,
                column: "Code",
                value: "554");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 555,
                column: "Code",
                value: "555");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 556,
                column: "Code",
                value: "556");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 557,
                column: "Code",
                value: "557");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 558,
                column: "Code",
                value: "558");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 559,
                column: "Code",
                value: "559");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 560,
                column: "Code",
                value: "560");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 561,
                column: "Code",
                value: "561");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 562,
                column: "Code",
                value: "562");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 563,
                column: "Code",
                value: "563");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 564,
                column: "Code",
                value: "564");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 565,
                column: "Code",
                value: "565");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 566,
                column: "Code",
                value: "566");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 567,
                column: "Code",
                value: "567");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 568,
                column: "Code",
                value: "568");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 569,
                column: "Code",
                value: "569");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 570,
                column: "Code",
                value: "570");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 571,
                column: "Code",
                value: "571");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 572,
                column: "Code",
                value: "572");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 573,
                column: "Code",
                value: "573");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 574,
                column: "Code",
                value: "574");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 575,
                column: "Code",
                value: "575");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 576,
                column: "Code",
                value: "576");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 577,
                column: "Code",
                value: "577");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 578,
                column: "Code",
                value: "578");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 579,
                column: "Code",
                value: "579");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 580,
                column: "Code",
                value: "580");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 581,
                column: "Code",
                value: "581");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 582,
                column: "Code",
                value: "582");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 583,
                column: "Code",
                value: "583");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 584,
                column: "Code",
                value: "584");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 585,
                column: "Code",
                value: "585");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 586,
                column: "Code",
                value: "586");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 587,
                column: "Code",
                value: "587");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 588,
                column: "Code",
                value: "588");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 589,
                column: "Code",
                value: "589");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 590,
                column: "Code",
                value: "590");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 591,
                column: "Code",
                value: "591");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 592,
                column: "Code",
                value: "592");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 593,
                column: "Code",
                value: "593");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 594,
                column: "Code",
                value: "594");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 595,
                column: "Code",
                value: "595");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 596,
                column: "Code",
                value: "596");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 597,
                column: "Code",
                value: "597");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 598,
                column: "Code",
                value: "598");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 599,
                column: "Code",
                value: "599");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 600,
                column: "Code",
                value: "600");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 601,
                column: "Code",
                value: "601");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 602,
                column: "Code",
                value: "602");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 603,
                column: "Code",
                value: "603");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 604,
                column: "Code",
                value: "604");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 605,
                column: "Code",
                value: "605");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 606,
                column: "Code",
                value: "606");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 607,
                column: "Code",
                value: "607");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 608,
                column: "Code",
                value: "608");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 609,
                column: "Code",
                value: "609");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 610,
                column: "Code",
                value: "610");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 611,
                column: "Code",
                value: "611");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 612,
                column: "Code",
                value: "612");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 613,
                column: "Code",
                value: "613");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 614,
                column: "Code",
                value: "614");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 615,
                column: "Code",
                value: "615");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 616,
                column: "Code",
                value: "616");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 617,
                column: "Code",
                value: "617");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 618,
                column: "Code",
                value: "618");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 619,
                column: "Code",
                value: "619");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 620,
                column: "Code",
                value: "620");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 621,
                column: "Code",
                value: "621");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 622,
                column: "Code",
                value: "622");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 623,
                column: "Code",
                value: "623");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 624,
                column: "Code",
                value: "624");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 625,
                column: "Code",
                value: "625");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 626,
                column: "Code",
                value: "626");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 627,
                column: "Code",
                value: "627");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 628,
                column: "Code",
                value: "628");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 629,
                column: "Code",
                value: "629");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 630,
                column: "Code",
                value: "630");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 631,
                column: "Code",
                value: "631");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 632,
                column: "Code",
                value: "632");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 633,
                column: "Code",
                value: "633");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 634,
                column: "Code",
                value: "634");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 635,
                column: "Code",
                value: "635");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 636,
                column: "Code",
                value: "636");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 637,
                column: "Code",
                value: "637");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 638,
                column: "Code",
                value: "638");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 639,
                column: "Code",
                value: "639");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 640,
                column: "Code",
                value: "640");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 641,
                column: "Code",
                value: "641");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 642,
                column: "Code",
                value: "642");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 643,
                column: "Code",
                value: "643");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 644,
                column: "Code",
                value: "644");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 645,
                column: "Code",
                value: "645");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 646,
                column: "Code",
                value: "646");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 647,
                column: "Code",
                value: "647");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 648,
                column: "Code",
                value: "648");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 649,
                column: "Code",
                value: "649");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 650,
                column: "Code",
                value: "650");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 651,
                column: "Code",
                value: "651");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 652,
                column: "Code",
                value: "652");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 653,
                column: "Code",
                value: "653");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 654,
                column: "Code",
                value: "654");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 655,
                column: "Code",
                value: "655");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 656,
                column: "Code",
                value: "656");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 657,
                column: "Code",
                value: "657");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 658,
                column: "Code",
                value: "658");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 659,
                column: "Code",
                value: "659");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 660,
                column: "Code",
                value: "660");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 661,
                column: "Code",
                value: "661");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 662,
                column: "Code",
                value: "662");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 663,
                column: "Code",
                value: "663");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 664,
                column: "Code",
                value: "664");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 665,
                column: "Code",
                value: "665");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 666,
                column: "Code",
                value: "666");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 667,
                column: "Code",
                value: "667");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 668,
                column: "Code",
                value: "668");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 669,
                column: "Code",
                value: "669");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 670,
                column: "Code",
                value: "670");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 671,
                column: "Code",
                value: "671");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 672,
                column: "Code",
                value: "672");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 673,
                column: "Code",
                value: "673");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 674,
                column: "Code",
                value: "674");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 675,
                column: "Code",
                value: "675");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 676,
                column: "Code",
                value: "676");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 677,
                column: "Code",
                value: "677");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 678,
                column: "Code",
                value: "678");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 679,
                column: "Code",
                value: "679");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 680,
                column: "Code",
                value: "680");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 681,
                column: "Code",
                value: "681");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 682,
                column: "Code",
                value: "682");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 683,
                column: "Code",
                value: "683");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 684,
                column: "Code",
                value: "684");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 685,
                column: "Code",
                value: "685");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 686,
                column: "Code",
                value: "686");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 687,
                column: "Code",
                value: "687");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 688,
                column: "Code",
                value: "688");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 689,
                column: "Code",
                value: "689");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 690,
                column: "Code",
                value: "690");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 691,
                column: "Code",
                value: "691");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 692,
                column: "Code",
                value: "692");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 693,
                column: "Code",
                value: "693");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 694,
                column: "Code",
                value: "694");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 695,
                column: "Code",
                value: "695");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 696,
                column: "Code",
                value: "696");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 697,
                column: "Code",
                value: "697");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 698,
                column: "Code",
                value: "698");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 699,
                column: "Code",
                value: "699");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 700,
                column: "Code",
                value: "700");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 701,
                column: "Code",
                value: "701");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 702,
                column: "Code",
                value: "702");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 703,
                column: "Code",
                value: "703");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 704,
                column: "Code",
                value: "704");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 705,
                column: "Code",
                value: "705");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 706,
                column: "Code",
                value: "706");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 707,
                column: "Code",
                value: "707");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 708,
                column: "Code",
                value: "708");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 709,
                column: "Code",
                value: "709");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 710,
                column: "Code",
                value: "710");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 711,
                column: "Code",
                value: "711");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 712,
                column: "Code",
                value: "712");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 713,
                column: "Code",
                value: "713");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 714,
                column: "Code",
                value: "714");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 715,
                column: "Code",
                value: "715");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 716,
                column: "Code",
                value: "716");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 717,
                column: "Code",
                value: "717");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 718,
                column: "Code",
                value: "718");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 719,
                column: "Code",
                value: "719");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 720,
                column: "Code",
                value: "720");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 721,
                column: "Code",
                value: "721");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 722,
                column: "Code",
                value: "722");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 723,
                column: "Code",
                value: "723");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 724,
                column: "Code",
                value: "724");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 725,
                column: "Code",
                value: "725");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 726,
                column: "Code",
                value: "726");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 727,
                column: "Code",
                value: "727");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 728,
                column: "Code",
                value: "728");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 729,
                column: "Code",
                value: "729");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 730,
                column: "Code",
                value: "730");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 731,
                column: "Code",
                value: "731");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 732,
                column: "Code",
                value: "732");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 733,
                column: "Code",
                value: "733");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 734,
                column: "Code",
                value: "734");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 735,
                column: "Code",
                value: "735");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 736,
                column: "Code",
                value: "736");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 737,
                column: "Code",
                value: "737");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 738,
                column: "Code",
                value: "738");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 739,
                column: "Code",
                value: "739");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 740,
                column: "Code",
                value: "740");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 741,
                column: "Code",
                value: "741");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 742,
                column: "Code",
                value: "742");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 743,
                column: "Code",
                value: "743");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 744,
                column: "Code",
                value: "744");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 745,
                column: "Code",
                value: "745");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 746,
                column: "Code",
                value: "746");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 747,
                column: "Code",
                value: "747");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 748,
                column: "Code",
                value: "748");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 749,
                column: "Code",
                value: "749");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 750,
                column: "Code",
                value: "750");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 751,
                column: "Code",
                value: "751");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 752,
                column: "Code",
                value: "752");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 753,
                column: "Code",
                value: "753");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 754,
                column: "Code",
                value: "754");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 755,
                column: "Code",
                value: "755");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 756,
                column: "Code",
                value: "756");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 757,
                column: "Code",
                value: "757");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 758,
                column: "Code",
                value: "758");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 759,
                column: "Code",
                value: "759");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 760,
                column: "Code",
                value: "760");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 761,
                column: "Code",
                value: "761");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 762,
                column: "Code",
                value: "762");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 763,
                column: "Code",
                value: "763");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 764,
                column: "Code",
                value: "764");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 765,
                column: "Code",
                value: "765");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 766,
                column: "Code",
                value: "766");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 767,
                column: "Code",
                value: "767");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 768,
                column: "Code",
                value: "768");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 769,
                column: "Code",
                value: "769");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 770,
                column: "Code",
                value: "770");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 771,
                column: "Code",
                value: "771");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 772,
                column: "Code",
                value: "772");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 773,
                column: "Code",
                value: "773");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 774,
                column: "Code",
                value: "774");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 775,
                column: "Code",
                value: "775");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 776,
                column: "Code",
                value: "776");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 777,
                column: "Code",
                value: "777");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 778,
                column: "Code",
                value: "778");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 779,
                column: "Code",
                value: "779");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 780,
                column: "Code",
                value: "780");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 781,
                column: "Code",
                value: "781");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 782,
                column: "Code",
                value: "782");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 783,
                column: "Code",
                value: "783");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 784,
                column: "Code",
                value: "784");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 785,
                column: "Code",
                value: "785");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 786,
                column: "Code",
                value: "786");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 787,
                column: "Code",
                value: "787");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 788,
                column: "Code",
                value: "788");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 789,
                column: "Code",
                value: "789");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 790,
                column: "Code",
                value: "790");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 791,
                column: "Code",
                value: "791");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 792,
                column: "Code",
                value: "792");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 793,
                column: "Code",
                value: "793");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 794,
                column: "Code",
                value: "794");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 795,
                column: "Code",
                value: "795");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 796,
                column: "Code",
                value: "796");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 797,
                column: "Code",
                value: "797");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 798,
                column: "Code",
                value: "798");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 799,
                column: "Code",
                value: "799");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 800,
                column: "Code",
                value: "800");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 801,
                column: "Code",
                value: "801");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 802,
                column: "Code",
                value: "802");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 803,
                column: "Code",
                value: "803");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 804,
                column: "Code",
                value: "804");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 805,
                column: "Code",
                value: "805");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 806,
                column: "Code",
                value: "806");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 807,
                column: "Code",
                value: "807");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 808,
                column: "Code",
                value: "808");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 809,
                column: "Code",
                value: "809");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 810,
                column: "Code",
                value: "810");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 811,
                column: "Code",
                value: "811");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 812,
                column: "Code",
                value: "812");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 813,
                column: "Code",
                value: "813");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 814,
                column: "Code",
                value: "814");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 815,
                column: "Code",
                value: "815");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 816,
                column: "Code",
                value: "816");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 817,
                column: "Code",
                value: "817");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 818,
                column: "Code",
                value: "818");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 819,
                column: "Code",
                value: "819");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 820,
                column: "Code",
                value: "820");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 821,
                column: "Code",
                value: "821");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 822,
                column: "Code",
                value: "822");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 823,
                column: "Code",
                value: "823");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 824,
                column: "Code",
                value: "824");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 825,
                column: "Code",
                value: "825");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 826,
                column: "Code",
                value: "826");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 827,
                column: "Code",
                value: "827");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 828,
                column: "Code",
                value: "828");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 829,
                column: "Code",
                value: "829");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 830,
                column: "Code",
                value: "830");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 831,
                column: "Code",
                value: "831");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 832,
                column: "Code",
                value: "832");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 833,
                column: "Code",
                value: "833");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 834,
                column: "Code",
                value: "834");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 835,
                column: "Code",
                value: "835");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 836,
                column: "Code",
                value: "836");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 837,
                column: "Code",
                value: "837");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 838,
                column: "Code",
                value: "838");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 839,
                column: "Code",
                value: "839");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 840,
                column: "Code",
                value: "840");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 841,
                column: "Code",
                value: "841");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 842,
                column: "Code",
                value: "842");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 843,
                column: "Code",
                value: "843");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 844,
                column: "Code",
                value: "844");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 845,
                column: "Code",
                value: "845");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 846,
                column: "Code",
                value: "846");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 847,
                column: "Code",
                value: "847");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 848,
                column: "Code",
                value: "848");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 849,
                column: "Code",
                value: "849");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 850,
                column: "Code",
                value: "850");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 851,
                column: "Code",
                value: "851");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 852,
                column: "Code",
                value: "852");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 853,
                column: "Code",
                value: "853");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 854,
                column: "Code",
                value: "854");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 855,
                column: "Code",
                value: "855");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 856,
                column: "Code",
                value: "856");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 857,
                column: "Code",
                value: "857");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 858,
                column: "Code",
                value: "858");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 859,
                column: "Code",
                value: "859");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 860,
                column: "Code",
                value: "860");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 861,
                column: "Code",
                value: "861");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 862,
                column: "Code",
                value: "862");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 863,
                column: "Code",
                value: "863");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 864,
                column: "Code",
                value: "864");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 865,
                column: "Code",
                value: "865");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 866,
                column: "Code",
                value: "866");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 867,
                column: "Code",
                value: "867");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 868,
                column: "Code",
                value: "868");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 869,
                column: "Code",
                value: "869");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 870,
                column: "Code",
                value: "870");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 871,
                column: "Code",
                value: "871");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 872,
                column: "Code",
                value: "872");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 873,
                column: "Code",
                value: "873");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 874,
                column: "Code",
                value: "874");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 875,
                column: "Code",
                value: "875");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 876,
                column: "Code",
                value: "876");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 877,
                column: "Code",
                value: "877");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 878,
                column: "Code",
                value: "878");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 879,
                column: "Code",
                value: "879");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 880,
                column: "Code",
                value: "880");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 881,
                column: "Code",
                value: "881");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 882,
                column: "Code",
                value: "882");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 883,
                column: "Code",
                value: "883");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 884,
                column: "Code",
                value: "884");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 885,
                column: "Code",
                value: "885");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 886,
                column: "Code",
                value: "886");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 887,
                column: "Code",
                value: "887");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 888,
                column: "Code",
                value: "888");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 889,
                column: "Code",
                value: "889");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 890,
                column: "Code",
                value: "890");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 891,
                column: "Code",
                value: "891");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 892,
                column: "Code",
                value: "892");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 893,
                column: "Code",
                value: "893");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 894,
                column: "Code",
                value: "894");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 895,
                column: "Code",
                value: "895");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 896,
                column: "Code",
                value: "896");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 897,
                column: "Code",
                value: "897");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 898,
                column: "Code",
                value: "898");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 899,
                column: "Code",
                value: "899");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 900,
                column: "Code",
                value: "900");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 901,
                column: "Code",
                value: "901");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 902,
                column: "Code",
                value: "902");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 903,
                column: "Code",
                value: "903");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 904,
                column: "Code",
                value: "904");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 905,
                column: "Code",
                value: "905");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 906,
                column: "Code",
                value: "906");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 907,
                column: "Code",
                value: "907");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 908,
                column: "Code",
                value: "908");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 909,
                column: "Code",
                value: "909");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 910,
                column: "Code",
                value: "910");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 911,
                column: "Code",
                value: "911");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 912,
                column: "Code",
                value: "912");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 913,
                column: "Code",
                value: "913");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 914,
                column: "Code",
                value: "914");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 915,
                column: "Code",
                value: "915");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 916,
                column: "Code",
                value: "916");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 917,
                column: "Code",
                value: "917");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 918,
                column: "Code",
                value: "918");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 919,
                column: "Code",
                value: "919");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 920,
                column: "Code",
                value: "920");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 921,
                column: "Code",
                value: "921");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 922,
                column: "Code",
                value: "922");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 923,
                column: "Code",
                value: "923");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 924,
                column: "Code",
                value: "924");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 925,
                column: "Code",
                value: "925");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 926,
                column: "Code",
                value: "926");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 927,
                column: "Code",
                value: "927");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 928,
                column: "Code",
                value: "928");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 929,
                column: "Code",
                value: "929");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 930,
                column: "Code",
                value: "930");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 931,
                column: "Code",
                value: "931");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 932,
                column: "Code",
                value: "932");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 933,
                column: "Code",
                value: "933");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 934,
                column: "Code",
                value: "934");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 935,
                column: "Code",
                value: "935");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 936,
                column: "Code",
                value: "936");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 937,
                column: "Code",
                value: "937");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 938,
                column: "Code",
                value: "938");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 939,
                column: "Code",
                value: "939");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 940,
                column: "Code",
                value: "940");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 941,
                column: "Code",
                value: "941");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 942,
                column: "Code",
                value: "942");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 943,
                column: "Code",
                value: "943");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 944,
                column: "Code",
                value: "944");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 945,
                column: "Code",
                value: "945");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 946,
                column: "Code",
                value: "946");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 947,
                column: "Code",
                value: "947");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 948,
                column: "Code",
                value: "948");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 949,
                column: "Code",
                value: "949");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 950,
                column: "Code",
                value: "950");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 951,
                column: "Code",
                value: "951");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 952,
                column: "Code",
                value: "952");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 953,
                column: "Code",
                value: "953");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 954,
                column: "Code",
                value: "954");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 955,
                column: "Code",
                value: "955");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 956,
                column: "Code",
                value: "956");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 957,
                column: "Code",
                value: "957");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 958,
                column: "Code",
                value: "958");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 959,
                column: "Code",
                value: "959");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 960,
                column: "Code",
                value: "960");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 961,
                column: "Code",
                value: "961");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 962,
                column: "Code",
                value: "962");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 963,
                column: "Code",
                value: "963");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 964,
                column: "Code",
                value: "964");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 965,
                column: "Code",
                value: "965");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 966,
                column: "Code",
                value: "966");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 967,
                column: "Code",
                value: "967");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 968,
                column: "Code",
                value: "968");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 969,
                column: "Code",
                value: "969");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 970,
                column: "Code",
                value: "970");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 971,
                column: "Code",
                value: "971");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 972,
                column: "Code",
                value: "972");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 973,
                column: "Code",
                value: "973");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 974,
                column: "Code",
                value: "974");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 975,
                column: "Code",
                value: "975");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 976,
                column: "Code",
                value: "976");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 977,
                column: "Code",
                value: "977");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 978,
                column: "Code",
                value: "978");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 979,
                column: "Code",
                value: "979");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 980,
                column: "Code",
                value: "980");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 981,
                column: "Code",
                value: "981");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 982,
                column: "Code",
                value: "982");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 983,
                column: "Code",
                value: "983");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 984,
                column: "Code",
                value: "984");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 985,
                column: "Code",
                value: "985");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 986,
                column: "Code",
                value: "986");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 987,
                column: "Code",
                value: "987");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 988,
                column: "Code",
                value: "988");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 989,
                column: "Code",
                value: "989");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 990,
                column: "Code",
                value: "990");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 991,
                column: "Code",
                value: "991");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 992,
                column: "Code",
                value: "992");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 993,
                column: "Code",
                value: "993");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 994,
                column: "Code",
                value: "994");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 995,
                column: "Code",
                value: "995");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 996,
                column: "Code",
                value: "996");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 997,
                column: "Code",
                value: "997");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 998,
                column: "Code",
                value: "998");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 999,
                column: "Code",
                value: "999");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1000,
                column: "Code",
                value: "1000");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1001,
                column: "Code",
                value: "1001");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1002,
                column: "Code",
                value: "1002");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1003,
                column: "Code",
                value: "1003");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1004,
                column: "Code",
                value: "1004");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1005,
                column: "Code",
                value: "1005");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1006,
                column: "Code",
                value: "1006");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1007,
                column: "Code",
                value: "1007");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1008,
                column: "Code",
                value: "1008");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1009,
                column: "Code",
                value: "1009");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1010,
                column: "Code",
                value: "1010");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1011,
                column: "Code",
                value: "1011");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1012,
                column: "Code",
                value: "1012");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1013,
                column: "Code",
                value: "1013");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1014,
                column: "Code",
                value: "1014");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1015,
                column: "Code",
                value: "1015");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1016,
                column: "Code",
                value: "1016");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1017,
                column: "Code",
                value: "1017");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1018,
                column: "Code",
                value: "1018");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1019,
                column: "Code",
                value: "1019");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1020,
                column: "Code",
                value: "1020");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1021,
                column: "Code",
                value: "1021");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1022,
                column: "Code",
                value: "1022");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1023,
                column: "Code",
                value: "1023");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1024,
                column: "Code",
                value: "1024");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1025,
                column: "Code",
                value: "1025");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1026,
                column: "Code",
                value: "1026");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1027,
                column: "Code",
                value: "1027");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1028,
                column: "Code",
                value: "1028");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1029,
                column: "Code",
                value: "1029");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1030,
                column: "Code",
                value: "1030");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1031,
                column: "Code",
                value: "1031");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1032,
                column: "Code",
                value: "1032");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1033,
                column: "Code",
                value: "1033");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1034,
                column: "Code",
                value: "1034");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1035,
                column: "Code",
                value: "1035");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1036,
                column: "Code",
                value: "1036");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1037,
                column: "Code",
                value: "1037");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1038,
                column: "Code",
                value: "1038");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1039,
                column: "Code",
                value: "1039");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1040,
                column: "Code",
                value: "1040");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1041,
                column: "Code",
                value: "1041");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1042,
                column: "Code",
                value: "1042");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1043,
                column: "Code",
                value: "1043");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1044,
                column: "Code",
                value: "1044");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1045,
                column: "Code",
                value: "1045");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1046,
                column: "Code",
                value: "1046");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1047,
                column: "Code",
                value: "1047");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1048,
                column: "Code",
                value: "1048");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1049,
                column: "Code",
                value: "1049");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1050,
                column: "Code",
                value: "1050");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1051,
                column: "Code",
                value: "1051");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1052,
                column: "Code",
                value: "1052");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1053,
                column: "Code",
                value: "1053");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1054,
                column: "Code",
                value: "1054");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1055,
                column: "Code",
                value: "1055");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1056,
                column: "Code",
                value: "1056");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1057,
                column: "Code",
                value: "1057");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1058,
                column: "Code",
                value: "1058");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1059,
                column: "Code",
                value: "1059");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1060,
                column: "Code",
                value: "1060");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1061,
                column: "Code",
                value: "1061");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1062,
                column: "Code",
                value: "1062");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1063,
                column: "Code",
                value: "1063");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1064,
                column: "Code",
                value: "1064");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1065,
                column: "Code",
                value: "1065");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1066,
                column: "Code",
                value: "1066");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1067,
                column: "Code",
                value: "1067");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1068,
                column: "Code",
                value: "1068");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1069,
                column: "Code",
                value: "1069");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1070,
                column: "Code",
                value: "1070");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1071,
                column: "Code",
                value: "1071");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1072,
                column: "Code",
                value: "1072");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1073,
                column: "Code",
                value: "1073");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1074,
                column: "Code",
                value: "1074");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1075,
                column: "Code",
                value: "1075");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1076,
                column: "Code",
                value: "1076");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1077,
                column: "Code",
                value: "1077");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1078,
                column: "Code",
                value: "1078");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1079,
                column: "Code",
                value: "1079");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1080,
                column: "Code",
                value: "1080");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1081,
                column: "Code",
                value: "1081");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1082,
                column: "Code",
                value: "1082");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1083,
                column: "Code",
                value: "1083");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1084,
                column: "Code",
                value: "1084");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1085,
                column: "Code",
                value: "1085");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1086,
                column: "Code",
                value: "1086");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1087,
                column: "Code",
                value: "1087");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1088,
                column: "Code",
                value: "1088");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1089,
                column: "Code",
                value: "1089");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1090,
                column: "Code",
                value: "1090");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1091,
                column: "Code",
                value: "1091");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1092,
                column: "Code",
                value: "1092");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1093,
                column: "Code",
                value: "1093");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1094,
                column: "Code",
                value: "1094");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1095,
                column: "Code",
                value: "1095");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1096,
                column: "Code",
                value: "1096");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1097,
                column: "Code",
                value: "1097");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1098,
                column: "Code",
                value: "1098");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1099,
                column: "Code",
                value: "1099");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1100,
                column: "Code",
                value: "1100");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1101,
                column: "Code",
                value: "1101");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1102,
                column: "Code",
                value: "1102");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1103,
                column: "Code",
                value: "1103");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1104,
                column: "Code",
                value: "1104");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1105,
                column: "Code",
                value: "1105");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1106,
                column: "Code",
                value: "1106");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1107,
                column: "Code",
                value: "1107");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1108,
                column: "Code",
                value: "1108");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1109,
                column: "Code",
                value: "1109");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1110,
                column: "Code",
                value: "1110");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1111,
                column: "Code",
                value: "1111");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1112,
                column: "Code",
                value: "1112");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1113,
                column: "Code",
                value: "1113");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1114,
                column: "Code",
                value: "1114");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1115,
                column: "Code",
                value: "1115");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1116,
                column: "Code",
                value: "1116");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1117,
                column: "Code",
                value: "1117");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1118,
                column: "Code",
                value: "1118");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1119,
                column: "Code",
                value: "1119");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1120,
                column: "Code",
                value: "1120");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1121,
                column: "Code",
                value: "1121");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1122,
                column: "Code",
                value: "1122");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1123,
                column: "Code",
                value: "1123");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1124,
                column: "Code",
                value: "1124");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1125,
                column: "Code",
                value: "1125");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1126,
                column: "Code",
                value: "1126");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1127,
                column: "Code",
                value: "1127");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1128,
                column: "Code",
                value: "1128");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1129,
                column: "Code",
                value: "1129");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1130,
                column: "Code",
                value: "1130");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1131,
                column: "Code",
                value: "1131");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1132,
                column: "Code",
                value: "1132");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1133,
                column: "Code",
                value: "1133");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1134,
                column: "Code",
                value: "1134");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1135,
                column: "Code",
                value: "1135");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1136,
                column: "Code",
                value: "1136");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1137,
                column: "Code",
                value: "1137");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1138,
                column: "Code",
                value: "1138");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1139,
                column: "Code",
                value: "1139");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1140,
                column: "Code",
                value: "1140");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1141,
                column: "Code",
                value: "1141");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1142,
                column: "Code",
                value: "1142");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1143,
                column: "Code",
                value: "1143");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1144,
                column: "Code",
                value: "1144");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1145,
                column: "Code",
                value: "1145");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1146,
                column: "Code",
                value: "1146");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1147,
                column: "Code",
                value: "1147");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1148,
                column: "Code",
                value: "1148");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1149,
                column: "Code",
                value: "1149");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1150,
                column: "Code",
                value: "1150");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1151,
                column: "Code",
                value: "1151");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1152,
                column: "Code",
                value: "1152");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1153,
                column: "Code",
                value: "1153");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1154,
                column: "Code",
                value: "1154");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1155,
                column: "Code",
                value: "1155");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1156,
                column: "Code",
                value: "1156");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1157,
                column: "Code",
                value: "1157");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1158,
                column: "Code",
                value: "1158");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1159,
                column: "Code",
                value: "1159");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1160,
                column: "Code",
                value: "1160");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1161,
                column: "Code",
                value: "1161");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1162,
                column: "Code",
                value: "1162");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1163,
                column: "Code",
                value: "1163");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1164,
                column: "Code",
                value: "1164");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1165,
                column: "Code",
                value: "1165");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1166,
                column: "Code",
                value: "1166");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1167,
                column: "Code",
                value: "1167");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1168,
                column: "Code",
                value: "1168");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1169,
                column: "Code",
                value: "1169");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1170,
                column: "Code",
                value: "1170");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1171,
                column: "Code",
                value: "1171");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1172,
                column: "Code",
                value: "1172");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1173,
                column: "Code",
                value: "1173");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1174,
                column: "Code",
                value: "1174");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1175,
                column: "Code",
                value: "1175");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1176,
                column: "Code",
                value: "1176");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1177,
                column: "Code",
                value: "1177");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1178,
                column: "Code",
                value: "1178");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1179,
                column: "Code",
                value: "1179");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1180,
                column: "Code",
                value: "1180");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1181,
                column: "Code",
                value: "1181");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1182,
                column: "Code",
                value: "1182");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1183,
                column: "Code",
                value: "1183");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1184,
                column: "Code",
                value: "1184");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1185,
                column: "Code",
                value: "1185");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1186,
                column: "Code",
                value: "1186");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1187,
                column: "Code",
                value: "1187");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1188,
                column: "Code",
                value: "1188");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1189,
                column: "Code",
                value: "1189");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1190,
                column: "Code",
                value: "1190");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1191,
                column: "Code",
                value: "1191");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1192,
                column: "Code",
                value: "1192");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1193,
                column: "Code",
                value: "1193");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1194,
                column: "Code",
                value: "1194");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1195,
                column: "Code",
                value: "1195");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1196,
                column: "Code",
                value: "1196");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1197,
                column: "Code",
                value: "1197");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1198,
                column: "Code",
                value: "1198");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1199,
                column: "Code",
                value: "1199");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1200,
                column: "Code",
                value: "1200");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1201,
                column: "Code",
                value: "1201");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1202,
                column: "Code",
                value: "1202");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1203,
                column: "Code",
                value: "1203");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1204,
                column: "Code",
                value: "1204");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1205,
                column: "Code",
                value: "1205");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1206,
                column: "Code",
                value: "1206");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1207,
                column: "Code",
                value: "1207");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1208,
                column: "Code",
                value: "1208");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1209,
                column: "Code",
                value: "1209");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1210,
                column: "Code",
                value: "1210");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1211,
                column: "Code",
                value: "1211");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1212,
                column: "Code",
                value: "1212");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1213,
                column: "Code",
                value: "1213");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1214,
                column: "Code",
                value: "1214");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1215,
                column: "Code",
                value: "1215");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1216,
                column: "Code",
                value: "1216");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1217,
                column: "Code",
                value: "1217");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1218,
                column: "Code",
                value: "1218");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1219,
                column: "Code",
                value: "1219");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1220,
                column: "Code",
                value: "1220");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1221,
                column: "Code",
                value: "1221");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1222,
                column: "Code",
                value: "1222");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1223,
                column: "Code",
                value: "1223");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1224,
                column: "Code",
                value: "1224");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1225,
                column: "Code",
                value: "1225");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1226,
                column: "Code",
                value: "1226");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1227,
                column: "Code",
                value: "1227");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1228,
                column: "Code",
                value: "1228");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1229,
                column: "Code",
                value: "1229");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1230,
                column: "Code",
                value: "1230");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1231,
                column: "Code",
                value: "1231");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1232,
                column: "Code",
                value: "1232");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1233,
                column: "Code",
                value: "1233");

            migrationBuilder.UpdateData(
                table: "SysRefIdentifierType",
                keyColumn: "Id",
                keyValue: 1,
                column: "Code",
                value: "1");

            migrationBuilder.UpdateData(
                table: "SysRefIdentifierType",
                keyColumn: "Id",
                keyValue: 2,
                column: "Code",
                value: "2");
        }
    }
}
