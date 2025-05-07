using Amazon.DynamoDBv2;
using HandlebarsDotNet.Runtime;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations;
using static System.Runtime.InteropServices.JavaScript.JSType;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Dynamo.Stream.Handler.Migrations
{
    /// <inheritdoc />
    public partial class BPoRDataDashboarddatatransform2of2healthconditiondata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SupercededById",
                table: "SysRefHealthCondition",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "AAA", "AAA", 3 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "AAA screening", "AAA screening", 3 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Abdominal aortic aneurysm", "Abdominal aortic aneurysm", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Abdominal aortic aneurysm screening", "Abdominal aortic aneurysm screening", 3 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Abortion", "Abortion", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Abscess", "Abscess", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Academic attainment", "Academic attainment", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Acanthosis nigricans", "Acanthosis nigricans", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Achalasia", "Achalasia", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Acid and chemical burns", "Acid and chemical burns", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Acid reflux in babies", "Acid reflux in babies", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Acne", "Acne", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Acoustic neuroma (vestibular schwannoma)", "Acoustic neuroma (vestibular schwannoma)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Acromegaly", "Acromegaly", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Actinic keratoses (solar keratoses)", "Actinic keratoses (solar keratoses)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Actinomycosis", "Actinomycosis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Acupuncture", "Acupuncture", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Acute cholecystitis", "Acute cholecystitis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Acute kidney injury", "Acute kidney injury", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Acute lymphoblastic leukaemia", "Acute lymphoblastic leukaemia", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Acute myeloid leukaemia", "Acute myeloid leukaemia", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Acute pancreatitis", "Acute pancreatitis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Acute respiratory distress syndrome", "Acute respiratory distress syndrome", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Addison's disease", "Addison's disease", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Adenoidectomy", "Adenoidectomy", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Age-related cataracts", "Age-related cataracts", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Age-related macular degeneration (AMD)", "Age-related macular degeneration (AMD)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Ageing", "Ageing", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Aggression", "Aggression", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Agoraphobia", "Agoraphobia", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Air or gas embolism", "Air or gas embolism", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Albinism", "Albinism", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Alcohol misuse", "Alcohol misuse", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Alcohol poisoning", "Alcohol poisoning", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Alcohol-related liver disease", "Alcohol-related liver disease", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Alexander technique", "Alexander technique", 0 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Alkaptonuria", "Alkaptonuria", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Allergic rhinitis", "Allergic rhinitis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Allergies", "Allergies", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Altitude sickness", "Altitude sickness", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Alzheimer's disease", "Alzheimer's disease", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Amblyopia", "Amblyopia", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Amnesia", "Amnesia", 767 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 44,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Amniocentesis", "Amniocentesis", 0 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 45,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Amputation", "Amputation", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 46,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Amyloidosis", "Amyloidosis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 47,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Anabolic steroid misuse", "Anabolic steroid misuse", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 48,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Anaemia (iron deficiency)", "Anaemia (iron deficiency)", 663 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 49,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Anaemia (vitamin B12 or folate deficiency)", "Anaemia (vitamin B12 or folate deficiency)", 1237 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 50,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Anaesthesia", "Anaesthesia", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 51,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Anal cancer", "Anal cancer", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 52,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Anal fissure", "Anal fissure", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 53,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Anal fistula", "Anal fistula", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 54,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Anal pain", "Anal pain", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 55,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Anaphylaxis", "Anaphylaxis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 56,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Androgen insensitivity syndrome", "Androgen insensitivity syndrome", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 57,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Aneurysm (abdominal aortic)", "Aneurysm (abdominal aortic)", 3 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 58,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Aneurysm (brain)", "Aneurysm (brain)", 170 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 59,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Angelman syndrome", "Angelman syndrome", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 60,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Angina", "Angina", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 61,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Angioedema", "Angioedema", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 62,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Angiography", "Angiography", 1151 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 63,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Angioplasty", "Angioplasty", 299 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 64,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Animal and human bites", "Animal and human bites", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 65,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "ankle pain", "ankle pain", 478 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 66,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Ankylosing spondylitis", "Ankylosing spondylitis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 67,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Anorexia nervosa", "Anorexia nervosa", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 68,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Anosmia", "Anosmia", 734 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 69,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Antacids", "Antacids", 569 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 70,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Antenatal care", "Antenatal care", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 71,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Antibiotics", "Antibiotics", 0 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 72,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Anticoagulant medicines", "Anticoagulant medicines", 140 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 73,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Antidepressants", "Antidepressants", 267 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 74,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Antifungal medicines", "Antifungal medicines", 0 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 75,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Antihistamines", "Antihistamines", 39 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 76,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Antiphospholipid syndrome (APS)", "Antiphospholipid syndrome (APS)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 77,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Antisocial personality disorder", "Antisocial personality disorder", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 78,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Anus (itchy)", "Anus (itchy)", 668 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 79,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Anxiety disorder in adults", "Anxiety disorder in adults", 501 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 80,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Anxiety disorders in children", "Anxiety disorders in children", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 81,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Aortic valve replacement", "Aortic valve replacement", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 82,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Aphasia", "Aphasia", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 83,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Appendicitis", "Appendicitis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 84,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Arrhythmia", "Arrhythmia", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 85,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Arterial thrombosis", "Arterial thrombosis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 86,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Arthritis", "Arthritis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 87,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Arthroscopy", "Arthroscopy", 678 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 88,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Asbestosis", "Asbestosis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 89,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Asperger's", "Asperger's", 101 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 90,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Aspergillosis", "Aspergillosis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 91,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Aspirin", "Aspirin", 875 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 92,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Asthma", "Asthma", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 93,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Astigmatism", "Astigmatism", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 94,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Ataxia", "Ataxia", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 95,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Atherosclerosis (arteriosclerosis)", "Atherosclerosis (arteriosclerosis)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 96,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Athlete's foot", "Athlete's foot", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 97,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Atopic eczema", "Atopic eczema", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 98,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Atrial fibrillation", "Atrial fibrillation", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 99,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Attention deficit hyperactivity disorder (ADHD)", "Attention deficit hyperactivity disorder (ADHD)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 100,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Auditory processing disorder (APD)", "Auditory processing disorder (APD)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 101,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Autism", "Autism", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 102,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Autosomal dominant polycystic kidney disease", "Autosomal dominant polycystic kidney disease", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 103,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Autosomal recessive polycystic kidney disease", "Autosomal recessive polycystic kidney disease", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 104,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Avian flu", "Avian flu", 128 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 105,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Baby", "Baby", 883 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 106,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Back pain", "Back pain", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 107,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Bacterial vaginosis", "Bacterial vaginosis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 108,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Bad breath", "Bad breath", 532 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 109,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Baker's cyst", "Baker's cyst", 937 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 110,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Balanitis", "Balanitis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 111,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Barium enema", "Barium enema", 1151 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 112,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Bartholin's cyst", "Bartholin's cyst", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 113,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Basal cell carcinoma", "Basal cell carcinoma", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 114,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Bedbugs", "Bedbugs", 656 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 115,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Bedwetting in children", "Bedwetting in children", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 116,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Behçet's disease", "Behçet's disease", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 117,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Being sick", "Being sick", 360 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 118,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Bell's palsy", "Bell's palsy", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 119,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Benign brain tumour (non-cancerous)", "Benign brain tumour (non-cancerous)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 120,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Benign prostate enlargement", "Benign prostate enlargement", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 121,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Bereavement", "Bereavement", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 122,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Beta blockers", "Beta blockers", 216 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 123,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Bile duct cancer (cholangiocarcinoma)", "Bile duct cancer (cholangiocarcinoma)", 248 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 124,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Bilharzia", "Bilharzia", 1020 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 125,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Binge eating disorder", "Binge eating disorder", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 126,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Biopsy", "Biopsy", 1151 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 127,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Bipolar disorder", "Bipolar disorder", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 128,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Bird flu", "Bird flu", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 129,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Birthmarks", "Birthmarks", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 130,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Bite (animal or human)", "Bite (animal or human)", 64 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 131,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Black eye", "Black eye", 444 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 132,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Bladder cancer", "Bladder cancer", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 133,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Bladder stones", "Bladder stones", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 134,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Bleeding after the menopause", "Bleeding after the menopause", 942 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 135,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Bleeding from the bottom (rectal bleeding)", "Bleeding from the bottom (rectal bleeding)", 987 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 136,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Blepharitis", "Blepharitis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 137,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Blindness and vision loss", "Blindness and vision loss", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 138,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Blisters", "Blisters", 462 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 139,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Bloating", "Bloating", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 140,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Blood clots", "Blood clots", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 141,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Blood donation", "Blood donation", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 142,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Blood groups", "Blood groups", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 143,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Blood in semen", "Blood in semen", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 144,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Blood in semen (haematospermia)", "Blood in semen (haematospermia)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 145,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Blood in urine", "Blood in urine", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 146,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Blood pressure (high)", "Blood pressure (high)", 597 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 147,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Blood pressure (low)", "Blood pressure (low)", 735 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 148,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Blood pressure test", "Blood pressure test", 1151 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 149,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Blood tests", "Blood tests", 1151 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 150,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Blood transfusion", "Blood transfusion", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 151,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Blue skin or lips (cyanosis)", "Blue skin or lips (cyanosis)", 319 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 152,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Blushing", "Blushing", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 153,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Body dysmorphic disorder (BDD)", "Body dysmorphic disorder (BDD)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 154,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Body image", "Body image", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 155,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Body odour (BO)", "Body odour (BO)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 156,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Boils", "Boils", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 157,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Bone cancer", "Bone cancer", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 158,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Bone cyst", "Bone cyst", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 159,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Bone density scan (DEXA scan)", "Bone density scan (DEXA scan)", 1151 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 160,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Borderline personality disorder", "Borderline personality disorder", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 161,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Botulism", "Botulism", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 162,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Bowel cancer", "Bowel cancer", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 163,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Bowel cancer screening", "Bowel cancer screening", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 164,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Bowel incontinence", "Bowel incontinence", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 165,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Bowel polyps", "Bowel polyps", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 166,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Bowel transplant", "Bowel transplant", 1063 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 167,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Bowen's disease", "Bowen's disease", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 168,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Brachycephaly and plagiocephaly", "Brachycephaly and plagiocephaly", 921 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 169,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Brain abscess", "Brain abscess", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 170,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Brain aneurysm", "Brain aneurysm", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 171,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Brain death", "Brain death", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 172,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Brain haemorrhage", "Brain haemorrhage", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 173,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Brain tumour (benign)", "Brain tumour (benign)", 119 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 174,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Brain tumour (malignant)", "Brain tumour (malignant)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 175,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Brain tumours", "Brain tumours", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 176,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Breast abscess", "Breast abscess", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 177,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Breast cancer in men", "Breast cancer in men", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 178,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Breast cancer in women", "Breast cancer in women", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 179,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Breast Feeding", "Breast Feeding", 184 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 180,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Breast lumps", "Breast lumps", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 181,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Breast pain", "Breast pain", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 182,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Breast reduction on the NHS", "Breast reduction on the NHS", 306 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 183,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Breast screening (mammogram)", "Breast screening (mammogram)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 184,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Breastfeeding", "Breastfeeding", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 185,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Breath-holding in babies and children", "Breath-holding in babies and children", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 186,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Broken ankle", "Broken ankle", 480 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 187,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Broken arm or wrist", "Broken arm or wrist", 480 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 188,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Broken collarbone", "Broken collarbone", 480 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 189,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Broken finger or thumb", "Broken finger or thumb", 480 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 190,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Broken leg", "Broken leg", 480 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 191,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Broken nose", "Broken nose", 480 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 192,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Broken or bruised ribs", "Broken or bruised ribs", 480 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 193,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Broken toe", "Broken toe", 480 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 194,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Bronchiectasis", "Bronchiectasis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 195,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Bronchiolitis", "Bronchiolitis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 196,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Bronchitis", "Bronchitis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 197,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Bronchodilators", "Bronchodilators", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 198,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Brucellosis", "Brucellosis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 199,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Brugada syndrome", "Brugada syndrome", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 200,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Bruxism", "Bruxism", 1139 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 201,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Bulging eyes (exophthalmos)", "Bulging eyes (exophthalmos)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 202,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Bulimia", "Bulimia", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 203,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Bullous pemphigoid", "Bullous pemphigoid", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 204,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Bunions", "Bunions", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 205,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Burns and scalds", "Burns and scalds", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 206,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Bursitis", "Bursitis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 207,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Buttock pain", "Buttock pain", 1022 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 208,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "CABG", "CABG", 300 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 209,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Caesarean section", "Caesarean section", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 210,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Cancer", "Cancer", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 211,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Cannabis oil (medical cannabis)", "Cannabis oil (medical cannabis)", 764 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 212,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Carbon monoxide poisoning", "Carbon monoxide poisoning", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 213,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Carcinoembryonic antigen (CEA) test", "Carcinoembryonic antigen (CEA) test", 1151 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 214,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Cardiac catheterisation and coronary angiography", "Cardiac catheterisation and coronary angiography", 1151 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 215,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Cardiomyopathy", "Cardiomyopathy", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 216,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Cardiovascular disease", "Cardiovascular disease", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 217,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Care homes", "Care homes", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 218,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Carers", "Carers", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 219,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Carotid endarterectomy", "Carotid endarterectomy", 95 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 220,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Carpal tunnel syndrome", "Carpal tunnel syndrome", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 221,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Cartilage damage", "Cartilage damage", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 222,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Cataract surgery", "Cataract surgery", 704 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 223,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Cataracts (age-related)", "Cataracts (age-related)", 26 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 224,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Cataracts (children)", "Cataracts (children)", 243 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 225,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Catarrh", "Catarrh", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 226,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Cavernoma", "Cavernoma", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 227,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Cavernous sinus thrombosis", "Cavernous sinus thrombosis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 228,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Cellulitis", "Cellulitis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 229,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Cerebral palsy", "Cerebral palsy", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 230,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Cervical cancer", "Cervical cancer", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 231,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Cervical rib", "Cervical rib", 1156 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 232,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Cervical screening", "Cervical screening", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 233,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Cervical spondylosis", "Cervical spondylosis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 234,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Charcot-Marie-Tooth disease", "Charcot-Marie-Tooth disease", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 235,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Charles Bonnet syndrome", "Charles Bonnet syndrome", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 236,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Chemotherapy", "Chemotherapy", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 237,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Chest infection", "Chest infection", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 238,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Chest pain", "Chest pain", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 239,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Chiari malformation", "Chiari malformation", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 240,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Chickenpox", "Chickenpox", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 241,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Chilblains", "Chilblains", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 242,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Child development", "Child development", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 243,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Childhood cataracts", "Childhood cataracts", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 244,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Chipped broken or cracked tooth", "Chipped broken or cracked tooth", 245 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 245,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Chipped, broken or cracked tooth", "Chipped, broken or cracked tooth", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 246,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Chiropractic", "Chiropractic", 106 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 247,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Chlamydia", "Chlamydia", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 248,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Cholangiocarcinoma", "Cholangiocarcinoma", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 249,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Cholecystitis (acute)", "Cholecystitis (acute)", 18 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 250,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Cholera", "Cholera", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 251,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Cholesteatoma", "Cholesteatoma", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 252,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Cholesterol (high)", "Cholesterol (high)", 599 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 253,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Chorionic villus sampling", "Chorionic villus sampling", 0 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 254,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Chronic fatigue syndrome (ME/CFS)", "Chronic fatigue syndrome (ME/CFS)", 802 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 255,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Chronic kidney disease", "Chronic kidney disease", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 256,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Chronic lymphocytic leukaemia", "Chronic lymphocytic leukaemia", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 257,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Chronic myeloid leukaemia", "Chronic myeloid leukaemia", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 258,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Chronic obstructive pulmonary disease (COPD)", "Chronic obstructive pulmonary disease (COPD)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 259,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Chronic pancreatitis", "Chronic pancreatitis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 260,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Chronic traumatic encephalopathy", "Chronic traumatic encephalopathy", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 261,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Circumcision in boys", "Circumcision in boys", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 262,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Circumcision in men", "Circumcision in men", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 263,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Cirrhosis", "Cirrhosis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 264,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "CJD", "CJD", 313 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 265,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Claustrophobia", "Claustrophobia", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 266,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Cleft lip and palate", "Cleft lip and palate", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 267,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Clinical depression", "Clinical depression", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 268,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Clinical trials", "Clinical trials", 549 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 269,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Clostridium difficile (C. diff) infection", "Clostridium difficile (C. diff) infection", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 270,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Club foot", "Club foot", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 271,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Cluster headaches", "Cluster headaches", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 272,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Coccydynia (tailbone pain)", "Coccydynia (tailbone pain)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 273,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Coeliac disease", "Coeliac disease", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 274,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Cognitive behavioural therapy (CBT)", "Cognitive behavioural therapy (CBT)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 275,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Cold sores", "Cold sores", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 276,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Colic", "Colic", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 277,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Colon cancer", "Colon cancer", 162 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 278,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Colonoscopy", "Colonoscopy", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 279,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Colostomy", "Colostomy", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 280,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Colour vision deficiency (colour blindness)", "Colour vision deficiency (colour blindness)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 281,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Colposcopy", "Colposcopy", 232 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 282,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Coma", "Coma", 367 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 283,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Common cold", "Common cold", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 284,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Compartment syndrome", "Compartment syndrome", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 285,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Complementary and alternative medicine", "Complementary and alternative medicine", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 286,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Complementary therapies", "Complementary therapies", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 287,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Complex regional pain syndrome", "Complex regional pain syndrome", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 288,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Concussion", "Concussion", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 289,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Confusion (sudden)", "Confusion (sudden)", 1117 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 290,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Congenital heart disease", "Congenital heart disease", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 291,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Congenital hip dislocation", "Congenital hip dislocation", 346 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 292,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Conjunctivitis", "Conjunctivitis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 293,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Consent to treatment", "Consent to treatment", 549 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 294,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Constipation", "Constipation", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 295,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Contact dermatitis", "Contact dermatitis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 296,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Contraception", "Contraception", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 297,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Cornea transplant", "Cornea transplant", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 298,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Corns and calluses", "Corns and calluses", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 299,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Coronary angioplasty and stent insertion", "Coronary angioplasty and stent insertion", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 300,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Coronary artery bypass graft", "Coronary artery bypass graft", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 301,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Coronary heart disease", "Coronary heart disease", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 302,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Coronavirus (COVID-19)", "Coronavirus (COVID-19)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 303,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Corticobasal degeneration", "Corticobasal degeneration", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 304,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Corticosteroid cream", "Corticosteroid cream", 1179 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 305,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Corticosteroids", "Corticosteroids", 1101 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 306,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Cosmetic procedures", "Cosmetic procedures", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 307,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Costochondritis", "Costochondritis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 308,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Cough", "Cough", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 309,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Coughing up blood (blood in phlegm)", "Coughing up blood (blood in phlegm)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 310,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Counselling", "Counselling", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 311,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Cradle cap", "Cradle cap", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 312,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Craniosynostosis", "Craniosynostosis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 313,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Creutzfeldt-Jakob disease", "Creutzfeldt-Jakob disease", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 314,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Crohn's disease", "Crohn's disease", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 315,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Croup", "Croup", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 316,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "CT scan", "CT scan", 1151 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 317,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Cushing's syndrome", "Cushing's syndrome", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 318,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Cuts and grazes", "Cuts and grazes", 0 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 319,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Cyanosis", "Cyanosis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 320,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Cyclical vomiting syndrome", "Cyclical vomiting syndrome", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 321,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Cyclospora", "Cyclospora", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 322,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Cyclothymia", "Cyclothymia", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 323,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Cystic fibrosis", "Cystic fibrosis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 324,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Cystitis", "Cystitis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 325,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Cystoscopy", "Cystoscopy", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 326,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Cytomegalovirus (CMV)", "Cytomegalovirus (CMV)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 327,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Dandruff", "Dandruff", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 328,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Deafblindness", "Deafblindness", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 329,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Deafness", "Deafness", 552 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 330,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Decompression sickness", "Decompression sickness", 31 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 331,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Decongestants", "Decongestants", 283 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 332,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Dehydration", "Dehydration", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 333,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Delirium", "Delirium", 1117 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 334,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Dementia (all)", "Dementia (all)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 335,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Dementia (frontotemporal)", "Dementia (frontotemporal)", 481 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 336,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Dementia (vascular)", "Dementia (vascular)", 1230 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 337,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Dementia guide", "Dementia guide", 334 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 338,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Dementia with Lewy bodies", "Dementia with Lewy bodies", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 339,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Dengue", "Dengue", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 340,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Dental abscess", "Dental abscess", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 341,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Dental pain", "Dental pain", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 342,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Dentures (false teeth)", "Dentures (false teeth)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 343,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Depression", "Depression", 267 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 344,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Detached retina (retinal detachment)", "Detached retina (retinal detachment)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 345,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Developmental co-ordination disorder (dyspraxia) in children", "Developmental co-ordination disorder (dyspraxia) in children", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 346,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Developmental dysplasia of the hip", "Developmental dysplasia of the hip", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 347,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "DEXA scan", "DEXA scan", 1151 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 348,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Diabetes", "Diabetes", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 349,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Diabetes (type 1)", "Diabetes (type 1)", 1203 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 350,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Diabetes (type 2)", "Diabetes (type 2)", 1204 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 351,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Diabetes in pregnancy", "Diabetes in pregnancy", 507 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 352,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Diabetes insipidus", "Diabetes insipidus", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 353,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Diabetic eye screening", "Diabetic eye screening", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 354,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Diabetic eye screening 1", "Diabetic eye screening 1", 353 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 355,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Diabetic eye screening 2", "Diabetic eye screening 2", 353 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 356,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Diabetic ketoacidosis", "Diabetic ketoacidosis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 357,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Diabetic retinopathy", "Diabetic retinopathy", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 358,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Dialysis", "Dialysis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 359,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Diarrhoea", "Diarrhoea", 360 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 360,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Diarrhoea and vomiting", "Diarrhoea and vomiting", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 361,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Differences in sex development", "Differences in sex development", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 362,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "DiGeorge syndrome (22q11 deletion)", "DiGeorge syndrome (22q11 deletion)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 363,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Diphtheria", "Diphtheria", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 364,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Discoid eczema", "Discoid eczema", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 365,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Dislocated kneecap", "Dislocated kneecap", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 366,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Dislocated shoulder", "Dislocated shoulder", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 367,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Disorders of consciousness", "Disorders of consciousness", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 368,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Dissociative disorders", "Dissociative disorders", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 369,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Diverticular disease and diverticulitis", "Diverticular disease and diverticulitis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 370,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Dizziness", "Dizziness", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 371,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Do not attempt cardiopulmonary resuscitation (DNACPR) decisions", "Do not attempt cardiopulmonary resuscitation (DNACPR) decisions", 417 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 372,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Domestic violence", "Domestic violence", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 373,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Double vision", "Double vision", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 374,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Down's syndrome", "Down's syndrome", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 375,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Dry eyes", "Dry eyes", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 376,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Dry lips", "Dry lips", 1077 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 377,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Dry mouth", "Dry mouth", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 378,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Dupuytren's contracture", "Dupuytren's contracture", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 379,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "DVT", "DVT", 380 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 380,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "DVT (deep vein thrombosis)", "DVT (deep vein thrombosis)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 381,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Dwarfism", "Dwarfism", 997 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 382,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Dysarthria (difficulty speaking)", "Dysarthria (difficulty speaking)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 383,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Dysentery", "Dysentery", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 384,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Dyslexia", "Dyslexia", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 385,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Dysphagia (swallowing problems)", "Dysphagia (swallowing problems)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 386,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Dyspraxia (developmental co-ordination disorder) in adults", "Dyspraxia (developmental co-ordination disorder) in adults", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 387,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Dyspraxia in children", "Dyspraxia in children", 345 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 388,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Dystonia", "Dystonia", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 389,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Ear infections", "Ear infections", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 390,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Earache", "Earache", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 391,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Eardrum (burst)", "Eardrum (burst)", 892 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 392,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Early menopause", "Early menopause", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 393,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Early or delayed puberty", "Early or delayed puberty", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 394,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Earwax build-up", "Earwax build-up", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 395,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Eating disorders", "Eating disorders", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 396,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Eating well", "Eating well", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 397,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Ebola virus disease", "Ebola virus disease", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 398,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Echocardiogram", "Echocardiogram", 1151 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 399,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Ectopic beats", "Ectopic beats", 563 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 400,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Ectopic pregnancy", "Ectopic pregnancy", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 401,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Ectropion", "Ectropion", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 402,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Eczema (atopic)", "Eczema (atopic)", 97 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 403,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Eczema (contact dermatitis)", "Eczema (contact dermatitis)", 295 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 404,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Eczema (discoid)", "Eczema (discoid)", 364 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 405,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Eczema (varicose)", "Eczema (varicose)", 1228 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 406,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Edwards' syndrome (trisomy 18)", "Edwards' syndrome (trisomy 18)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 407,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Ehlers-Danlos syndromes", "Ehlers-Danlos syndromes", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 408,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Ejaculation problems", "Ejaculation problems", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 409,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Elbow and arm pain", "Elbow and arm pain", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 410,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Electrocardiogram (ECG)", "Electrocardiogram (ECG)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 411,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Electroencephalogram (EEG)", "Electroencephalogram (EEG)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 412,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Electrolyte test", "Electrolyte test", 1151 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 413,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Embolism", "Embolism", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 414,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Emollients", "Emollients", 669 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 415,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Empyema", "Empyema", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 416,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Encephalitis", "Encephalitis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 417,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "End of life care", "End of life care", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 418,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Endocarditis", "Endocarditis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 419,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Endometrial cancer", "Endometrial cancer", 1263 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 420,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Endometriosis", "Endometriosis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 421,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Endoscopy", "Endoscopy", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 422,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Enhanced recovery", "Enhanced recovery", 0 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 423,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Epidermolysis bullosa", "Epidermolysis bullosa", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 424,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Epididymitis", "Epididymitis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 425,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Epidural", "Epidural", 875 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 426,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Epiglottitis", "Epiglottitis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 427,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Epilepsy", "Epilepsy", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 428,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Erectile dysfunction (impotence)", "Erectile dysfunction (impotence)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 429,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Erythema multiforme", "Erythema multiforme", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 430,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Erythema nodosum", "Erythema nodosum", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 431,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Erythromelalgia", "Erythromelalgia", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 432,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "essential tremor", "essential tremor", 1191 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 433,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Euthanasia and assisted suicide", "Euthanasia and assisted suicide", 417 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 434,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Ewing sarcoma", "Ewing sarcoma", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 435,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Excessive daytime sleepiness (hypersomnia)", "Excessive daytime sleepiness (hypersomnia)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 436,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Excessive hair growth (hirsutism)", "Excessive hair growth (hirsutism)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 437,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Excessive sweating (hyperhidrosis)", "Excessive sweating (hyperhidrosis)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 438,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Excessive thirst", "Excessive thirst", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 439,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Exercise and sports", "Exercise and sports", 548 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 440,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Exophthalmos (bulging eyes)", "Exophthalmos (bulging eyes)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 441,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Eye cancer", "Eye cancer", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 442,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Eye floaters", "Eye floaters", 469 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 443,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Eye infection (herpes)", "Eye infection (herpes)", 592 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 444,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Eye injuries", "Eye injuries", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 445,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Eye tests for children", "Eye tests for children", 0 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 446,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Eyelid problems", "Eyelid problems", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 447,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Fabricated or induced illness", "Fabricated or induced illness", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 448,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Face blindness", "Face blindness", 962 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 449,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Fainting", "Fainting", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 450,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Falls", "Falls", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 451,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Farting (flatulence)", "Farting (flatulence)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 452,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Febrile seizures", "Febrile seizures", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 453,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Feeling sick (nausea)", "Feeling sick (nausea)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 454,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Female genital mutilation (FGM)", "Female genital mutilation (FGM)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 455,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Femoral hernia repair", "Femoral hernia repair", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 456,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Fertility issues", "Fertility issues", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 457,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Fever in adults", "Fever in adults", 600 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 458,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Fever in children", "Fever in children", 601 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 459,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Fibroids", "Fibroids", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 460,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Fibromyalgia", "Fibromyalgia", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 461,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Finger pain", "Finger pain", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 462,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "First aid", "First aid", 0 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 463,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Fitness Studio exercise videos", "Fitness Studio exercise videos", 548 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 464,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Fits (children with fever)", "Fits (children with fever)", 452 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 465,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Fits (seizures)", "Fits (seizures)", 1028 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 466,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Flat feet", "Flat feet", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 467,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Flat head syndrome", "Flat head syndrome", 921 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 468,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Flatulence", "Flatulence", 451 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 469,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Floaters and flashes in the eyes", "Floaters and flashes in the eyes", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 470,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Flu", "Flu", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 471,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Fluoride", "Fluoride", 0 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 472,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Foetal alcohol spectrum disorder", "Foetal alcohol spectrum disorder", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 473,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Food allergy", "Food allergy", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 474,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Food colours and hyperactivity", "Food colours and hyperactivity", 99 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 475,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Food intolerance", "Food intolerance", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 476,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Food poisoning", "Food poisoning", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 477,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Foot drop", "Foot drop", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 478,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Foot pain", "Foot pain", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 479,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Foreskin problems", "Foreskin problems", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 480,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Fractures, sprains and broken bones", "Fractures, sprains and broken bones", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 481,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Frontotemporal dementia", "Frontotemporal dementia", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 482,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Frostbite", "Frostbite", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 483,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Frozen shoulder", "Frozen shoulder", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 484,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Functional neurological disorder", "Functional neurological disorder", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 485,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Fungal nail infection", "Fungal nail infection", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 486,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Gallbladder cancer", "Gallbladder cancer", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 487,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Gallbladder pain", "Gallbladder pain", 18 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 488,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Gallbladder removal", "Gallbladder removal", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 489,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Gallstones", "Gallstones", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 490,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Ganglion cyst", "Ganglion cyst", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 491,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Gangrene", "Gangrene", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 492,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Gastrectomy", "Gastrectomy", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 493,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Gastritis", "Gastritis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 494,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Gastro-oesophageal reflux disease (GORD)", "Gastro-oesophageal reflux disease (GORD)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 495,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Gastroenteritis", "Gastroenteritis", 360 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 496,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Gastroparesis", "Gastroparesis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 497,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Gastroscopy", "Gastroscopy", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 498,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Gender dysphoria", "Gender dysphoria", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 499,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "General anaesthesia", "General anaesthesia", 50 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 500,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "General wellbeing", "General wellbeing", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 501,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Generalised anxiety disorder in adults", "Generalised anxiety disorder in adults", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 502,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Genetic and genomic testing", "Genetic and genomic testing", 1151 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 503,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Genetic screening", "Genetic screening", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 504,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Genetic test for cancer gene", "Genetic test for cancer gene", 1151 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 505,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Genital herpes", "Genital herpes", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 506,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Genital warts", "Genital warts", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 507,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Gestational diabetes", "Gestational diabetes", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 508,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Giant cell arteritis", "Giant cell arteritis", 1141 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 509,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Giardiasis", "Giardiasis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 510,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Gigantism", "Gigantism", 14 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 511,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Gilbert's syndrome", "Gilbert's syndrome", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 512,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Glandular fever", "Glandular fever", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 513,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Glaucoma", "Glaucoma", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 514,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Glomerulonephritis", "Glomerulonephritis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 515,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Glue ear", "Glue ear", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 516,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Glutaric aciduria type 1", "Glutaric aciduria type 1", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 517,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Goitre", "Goitre", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 518,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Gonorrhoea", "Gonorrhoea", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 519,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Gout", "Gout", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 520,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Granuloma annulare", "Granuloma annulare", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 521,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Granulomatosis with polyangiitis", "Granulomatosis with polyangiitis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 522,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Group B strep", "Group B strep", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 523,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Growing pains", "Growing pains", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 524,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Guillain-Barré syndrome", "Guillain-Barré syndrome", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 525,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Gum disease", "Gum disease", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 526,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Haemochromatosis", "Haemochromatosis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 527,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Haemophilia", "Haemophilia", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 528,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Haemophilus influenzae type b (Hib)", "Haemophilus influenzae type b (Hib)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 529,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Hair dye reactions", "Hair dye reactions", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 530,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Hair loss", "Hair loss", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 531,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Hairy cell leukaemia", "Hairy cell leukaemia", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 532,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Halitosis", "Halitosis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 533,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Hallucinations and hearing voices", "Hallucinations and hearing voices", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 534,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Hamstring injury", "Hamstring injury", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 535,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Hand foot and mouth disease", "Hand foot and mouth disease", 538 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 536,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Hand pain", "Hand pain", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 537,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Hand tendon repair", "Hand tendon repair", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 538,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Hand, foot and mouth disease", "Hand, foot and mouth disease", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 539,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Having an operation (surgery)", "Having an operation (surgery)", 0 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 540,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Hay fever", "Hay fever", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 541,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Head and neck cancer", "Head and neck cancer", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 542,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Head injury and concussion", "Head injury and concussion", 288 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 543,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Head lice and nits", "Head lice and nits", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 544,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Headaches", "Headaches", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 545,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Headaches (hormone)", "Headaches (hormone)", 617 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 546,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Headaches (tension-type)", "Headaches (tension-type)", 1147 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 547,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Health anxiety", "Health anxiety", 632 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 548,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Healthy lifestyle", "Healthy lifestyle", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 549,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Healthy volunteer", "Healthy volunteer", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 550,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Healthy volunteers", "Healthy volunteers", 549 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 551,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Hearing aids and implants", "Hearing aids and implants", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 552,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Hearing loss", "Hearing loss", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 553,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Hearing tests", "Hearing tests", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 554,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Hearing tests for children", "Hearing tests for children", 553 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 555,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Hearing voices", "Hearing voices", 533 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 556,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Heart attack", "Heart attack", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 557,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Heart block", "Heart block", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 558,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Heart bypass", "Heart bypass", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 559,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Heart disease (coronary)", "Heart disease (coronary)", 301 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 560,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Heart failure", "Heart failure", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 561,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Heart pain", "Heart pain", 238 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 562,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Heart palpitations", "Heart palpitations", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 563,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Heart palpitations and ectopic beats", "Heart palpitations and ectopic beats", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 564,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Heart rhythm problems", "Heart rhythm problems", 84 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 565,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Heart transplant", "Heart transplant", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 566,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Heart valve problems", "Heart valve problems", 780 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 567,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Heart valve replacement", "Heart valve replacement", 81 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 568,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Heart-lung transplant", "Heart-lung transplant", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 569,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Heartburn and acid reflux", "Heartburn and acid reflux", 494 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 570,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Heat exhaustion and heatstroke", "Heat exhaustion and heatstroke", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 571,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Heat rash (prickly heat)", "Heat rash (prickly heat)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 572,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Heavy periods", "Heavy periods", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 573,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "heel pain", "heel pain", 478 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 574,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Help for suicidal thoughts", "Help for suicidal thoughts", 1119 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 575,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Henoch-Schönlein purpura (HSP)", "Henoch-Schönlein purpura (HSP)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 576,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Hepatitis", "Hepatitis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 577,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Hepatitis A", "Hepatitis A", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 578,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Hepatitis B", "Hepatitis B", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 579,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Hepatitis C", "Hepatitis C", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 580,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Herbal medicines", "Herbal medicines", 285 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 581,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Herceptin (trastuzumab)", "Herceptin (trastuzumab)", 210 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 582,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Hereditary haemorrhagic telangiectasia (HHT)", "Hereditary haemorrhagic telangiectasia (HHT)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 583,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Hereditary neuropathy with pressure palsies (HNPP)", "Hereditary neuropathy with pressure palsies (HNPP)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 584,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Hereditary spastic paraplegia", "Hereditary spastic paraplegia", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 585,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Hernia", "Hernia", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 586,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Hernia (femoral)", "Hernia (femoral)", 455 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 587,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Hernia (hiatus)", "Hernia (hiatus)", 594 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 588,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Hernia (inguinal)", "Hernia (inguinal)", 655 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 589,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Hernia (umbilical)", "Hernia (umbilical)", 585 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 590,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Herpes (genital)", "Herpes (genital)", 505 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 591,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Herpes in babies", "Herpes in babies", 819 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 592,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Herpes simplex eye infections", "Herpes simplex eye infections", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 593,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Herpetic whitlow (whitlow finger)", "Herpetic whitlow (whitlow finger)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 594,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Hiatus hernia", "Hiatus hernia", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 595,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Hiccups", "Hiccups", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 596,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Hidradenitis suppurativa (HS)", "Hidradenitis suppurativa (HS)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 597,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "High blood pressure (hypertension)", "High blood pressure (hypertension)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 598,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "High blood sugar (hyperglycaemia)", "High blood sugar (hyperglycaemia)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 599,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "High cholesterol", "High cholesterol", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 600,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "High temperature (fever) in adults", "High temperature (fever) in adults", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 601,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "High temperature (fever) in children", "High temperature (fever) in children", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 602,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Hip dysplasia", "Hip dysplasia", 346 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 603,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Hip fracture", "Hip fracture", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 604,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Hip pain in adults", "Hip pain in adults", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 605,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Hip pain in children (irritable hip)", "Hip pain in children (irritable hip)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 606,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Hip replacement", "Hip replacement", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 607,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Hirschsprung's disease", "Hirschsprung's disease", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 608,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "hirsutism", "hirsutism", 436 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 609,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "HIV and AIDS", "HIV and AIDS", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 610,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Hives", "Hives", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 611,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Hoarding disorder", "Hoarding disorder", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 612,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Hodgkin lymphoma", "Hodgkin lymphoma", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 613,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Home oxygen therapy", "Home oxygen therapy", 1042 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 614,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Homeopathy", "Homeopathy", 285 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 615,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Homocystinuria", "Homocystinuria", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 616,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Hookworm", "Hookworm", 1264 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 617,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Hormone headaches", "Hormone headaches", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 618,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Hormone replacement therapy (HRT)", "Hormone replacement therapy (HRT)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 619,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "HRT", "HRT", 618 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 620,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Hughes syndrome", "Hughes syndrome", 76 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 621,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Human papillomavirus (HPV)", "Human papillomavirus (HPV)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 622,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Huntington's disease", "Huntington's disease", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 623,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Hydrocephalus", "Hydrocephalus", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 624,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Hydronephrosis", "Hydronephrosis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 625,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Hyperglycaemia (high blood sugar)", "Hyperglycaemia (high blood sugar)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 626,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Hyperhidrosis", "Hyperhidrosis", 437 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 627,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Hyperparathyroidism", "Hyperparathyroidism", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 628,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Hypersomnia", "Hypersomnia", 435 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 629,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Hypertension", "Hypertension", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 630,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Hyperthyroidism", "Hyperthyroidism", 863 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 631,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Hypnotherapy", "Hypnotherapy", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 632,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Hypochondria", "Hypochondria", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 633,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Hypoglycaemia (low blood sugar)", "Hypoglycaemia (low blood sugar)", 736 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 634,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Hypoparathyroidism", "Hypoparathyroidism", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 635,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Hypotension", "Hypotension", 735 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 636,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Hypothermia", "Hypothermia", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 637,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Hypothyroidism", "Hypothyroidism", 1210 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 638,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Hysterectomy", "Hysterectomy", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 639,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Hysteroscopy", "Hysteroscopy", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 640,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "IBS", "IBS", 665 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 641,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Ichthyosis", "Ichthyosis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 642,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Idiopathic pulmonary fibrosis", "Idiopathic pulmonary fibrosis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 643,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Ileostomy", "Ileostomy", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 644,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Impetigo", "Impetigo", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 645,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Impotence", "Impotence", 428 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 646,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Improving care and services", "Improving care and services", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 647,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Incontinence (urinary)", "Incontinence (urinary)", 1215 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 648,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Indigestion", "Indigestion", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 649,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Infected piercings", "Infected piercings", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 650,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Infertility", "Infertility", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 651,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Inflammatory bowel disease", "Inflammatory bowel disease", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 652,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Influenza", "Influenza", 470 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 653,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Ingrown hairs", "Ingrown hairs", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 654,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Ingrown toenail", "Ingrown toenail", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 655,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Inguinal hernia repair", "Inguinal hernia repair", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 656,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Insect bites and stings", "Insect bites and stings", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 657,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Insomnia", "Insomnia", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 658,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Intensive care", "Intensive care", 0 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 659,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "intersex", "intersex", 361 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 660,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Interstitial cystitis", "Interstitial cystitis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 661,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Intracranial hypertension", "Intracranial hypertension", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 662,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Intrauterine insemination (IUI)", "Intrauterine insemination (IUI)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 663,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Iron deficiency anaemia", "Iron deficiency anaemia", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 664,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Irregular periods", "Irregular periods", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 665,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Irritable bowel syndrome (IBS)", "Irritable bowel syndrome (IBS)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 666,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Irritable hip", "Irritable hip", 605 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 667,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Isovaleric acidaemia", "Isovaleric acidaemia", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 668,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Itchy bottom", "Itchy bottom", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 669,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Itchy skin", "Itchy skin", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 670,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "IVF", "IVF", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 671,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Japanese encephalitis", "Japanese encephalitis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 672,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Jaundice", "Jaundice", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 673,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Jaundice in newborns", "Jaundice in newborns", 827 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 674,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Jaw pain", "Jaw pain", 1142 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 675,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Jellyfish and other sea creature stings", "Jellyfish and other sea creature stings", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 676,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Jet lag", "Jet lag", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 677,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Joint hypermobility syndrome", "Joint hypermobility syndrome", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 678,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Joint pain", "Joint pain", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 679,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Kaposi's sarcoma", "Kaposi's sarcoma", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 680,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Kawasaki disease", "Kawasaki disease", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 681,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Keloid scars", "Keloid scars", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 682,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Keratosis pilaris", "Keratosis pilaris", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 683,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Kidney cancer", "Kidney cancer", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 684,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Kidney failure", "Kidney failure", 255 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 685,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Kidney infection", "Kidney infection", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 686,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Kidney stones", "Kidney stones", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 687,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Kidney transplant", "Kidney transplant", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 688,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Klinefelter syndrome", "Klinefelter syndrome", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 689,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Knee ligament surgery", "Knee ligament surgery", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 690,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Knee pain", "Knee pain", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 691,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Knee replacement", "Knee replacement", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 692,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Knock knees", "Knock knees", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 693,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Knocked-out tooth", "Knocked-out tooth", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 694,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Kwashiorkor", "Kwashiorkor", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 695,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Kyphosis", "Kyphosis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 696,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Labial fusion", "Labial fusion", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 697,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Labyrinthitis and vestibular neuritis", "Labyrinthitis and vestibular neuritis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 698,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Lactate dehydrogenase (LDH) test", "Lactate dehydrogenase (LDH) test", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 699,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Lactose intolerance", "Lactose intolerance", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 700,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Lambert-Eaton myasthenic syndrome", "Lambert-Eaton myasthenic syndrome", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 701,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Laparoscopy (keyhole surgery)", "Laparoscopy (keyhole surgery)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 702,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Laryngeal (larynx) cancer", "Laryngeal (larynx) cancer", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 703,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Laryngitis", "Laryngitis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 704,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Laser eye surgery and lens surgery", "Laser eye surgery and lens surgery", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 705,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Laxatives", "Laxatives", 294 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 706,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Lazy eye", "Lazy eye", 42 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 707,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Learning disabilities", "Learning disabilities", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 708,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Leg cramps", "Leg cramps", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 709,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Leg ulcer", "Leg ulcer", 1233 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 710,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Legionnaires' disease", "Legionnaires' disease", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 711,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Leptospirosis (Weil's disease)", "Leptospirosis (Weil's disease)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 712,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Leukaemia (acute lymphoblastic)", "Leukaemia (acute lymphoblastic)", 20 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 713,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Leukaemia (acute myeloid)", "Leukaemia (acute myeloid)", 21 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 714,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Leukaemia (chronic lymphocytic)", "Leukaemia (chronic lymphocytic)", 256 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 715,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Leukaemia (chronic myeloid)", "Leukaemia (chronic myeloid)", 257 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 716,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Leukaemia (hairy cell)", "Leukaemia (hairy cell)", 531 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 717,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Leukoplakia", "Leukoplakia", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 718,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Lichen planus", "Lichen planus", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 719,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Lichen sclerosus", "Lichen sclerosus", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 720,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Limping in children", "Limping in children", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 721,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Lipoedema", "Lipoedema", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 722,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Lipoma", "Lipoma", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 723,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Lips (sore or dry)", "Lips (sore or dry)", 1077 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 724,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Listeriosis", "Listeriosis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 725,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Liver cancer", "Liver cancer", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 726,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Liver disease", "Liver disease", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 727,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Liver disease (alcohol-related)", "Liver disease (alcohol-related)", 35 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 728,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Liver transplant", "Liver transplant", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 729,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Local anaesthesia", "Local anaesthesia", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 730,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Long COVID", "Long COVID", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 731,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Long QT syndrome", "Long QT syndrome", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 732,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Long-sightedness", "Long-sightedness", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 733,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Loss of libido (reduced sex drive)", "Loss of libido (reduced sex drive)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 734,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Lost or changed sense of smell", "Lost or changed sense of smell", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 735,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Low blood pressure (hypotension)", "Low blood pressure (hypotension)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 736,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Low blood sugar (hypoglycaemia)", "Low blood sugar (hypoglycaemia)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 737,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Low sex drive (loss of libido)", "Low sex drive (loss of libido)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 738,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Low sperm count", "Low sperm count", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 739,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Low white blood cell count", "Low white blood cell count", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 740,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Lumbar decompression surgery", "Lumbar decompression surgery", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 741,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Lumbar puncture", "Lumbar puncture", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 742,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Lumps", "Lumps", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 743,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Lung cancer", "Lung cancer", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 744,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Lung transplant", "Lung transplant", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 745,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Lupus", "Lupus", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 746,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Lyme disease", "Lyme disease", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 747,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Lymphoedema", "Lymphoedema", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 748,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Macular degeneration (age-related)", "Macular degeneration (age-related)", 27 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 749,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Macular hole", "Macular hole", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 750,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Magnesium test", "Magnesium test", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 751,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Malaria", "Malaria", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 752,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Male menopause", "Male menopause", 1154 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 753,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Malignant brain tumour (brain cancer)", "Malignant brain tumour (brain cancer)", 174 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 754,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Mallet finger", "Mallet finger", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 755,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Malnutrition", "Malnutrition", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 756,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Maple syrup urine disease", "Maple syrup urine disease", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 757,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Marfan syndrome", "Marfan syndrome", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 758,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Mastectomy", "Mastectomy", 0 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 759,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Mastitis", "Mastitis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 760,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Mastocytosis", "Mastocytosis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 761,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Mastoiditis", "Mastoiditis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 762,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "MCADD", "MCADD", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 763,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Measles", "Measles", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 764,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Medical cannabis (and cannabis oils)", "Medical cannabis (and cannabis oils)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 765,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Medically unexplained symptoms", "Medically unexplained symptoms", 484 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 766,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Medicines information", "Medicines information", 549 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 767,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Memory loss (amnesia)", "Memory loss (amnesia)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 768,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Ménière's disease", "Ménière's disease", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 769,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Meningitis", "Meningitis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 770,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Menopause", "Menopause", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 771,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Menopause (early)", "Menopause (early)", 392 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 772,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Menstrual pain", "Menstrual pain", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 773,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Mental health and wellbeing", "Mental health and wellbeing", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 774,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Mesothelioma", "Mesothelioma", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 775,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Metabolic syndrome", "Metabolic syndrome", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 776,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Metallic taste", "Metallic taste", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 777,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Middle East respiratory syndrome (MERS)", "Middle East respiratory syndrome (MERS)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 778,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Migraine", "Migraine", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 779,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Miscarriage", "Miscarriage", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 780,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Mitral valve problems", "Mitral valve problems", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 781,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "MND", "MND", 788 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 782,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Molar pregnancy", "Molar pregnancy", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 783,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Moles", "Moles", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 784,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Molluscum contagiosum", "Molluscum contagiosum", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 785,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Monkeypox", "Monkeypox", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 786,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Morton's neuroma", "Morton's neuroma", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 787,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Motion sickness", "Motion sickness", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 788,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Motor neurone disease", "Motor neurone disease", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 789,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Mouth cancer", "Mouth cancer", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 790,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Mouth thrush", "Mouth thrush", 851 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 791,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Mouth ulcers", "Mouth ulcers", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 792,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "MRI scan", "MRI scan", 1151 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 793,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "MRSA", "MRSA", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 794,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Mucositis", "Mucositis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 795,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Multiple myeloma", "Multiple myeloma", 806 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 796,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Multiple sclerosis", "Multiple sclerosis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 797,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Multiple system atrophy", "Multiple system atrophy", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 798,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Mumps", "Mumps", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 799,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Munchausen syndrome", "Munchausen syndrome", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 800,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Munchausen's syndrome", "Munchausen's syndrome", 799 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 801,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Muscular dystrophy", "Muscular dystrophy", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 802,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Myalgic encephalomyelitis or chronic fatigue syndrome (ME/CFS)", "Myalgic encephalomyelitis or chronic fatigue syndrome (ME/CFS)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 803,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Myasthenia gravis", "Myasthenia gravis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 804,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Mycobacterium chimaera infection", "Mycobacterium chimaera infection", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 805,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Myelodysplastic syndrome (myelodysplasia)", "Myelodysplastic syndrome (myelodysplasia)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 806,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Myeloma", "Myeloma", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 807,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Myopia", "Myopia", 1041 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 808,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Myositis (polymyositis and dermatomyositis)", "Myositis (polymyositis and dermatomyositis)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 809,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Nail fungal infection", "Nail fungal infection", 485 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 810,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Nail patella syndrome", "Nail patella syndrome", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 811,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Nail problems", "Nail problems", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 812,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Narcolepsy", "Narcolepsy", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 813,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Nasal and sinus cancer", "Nasal and sinus cancer", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 814,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Nasal polyps", "Nasal polyps", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 815,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Nasopharyngeal cancer", "Nasopharyngeal cancer", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 816,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Nausea", "Nausea", 453 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 817,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Neck pain", "Neck pain", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 818,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Necrotising fasciitis", "Necrotising fasciitis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 819,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Neonatal herpes (herpes in a baby)", "Neonatal herpes (herpes in a baby)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 820,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Nephrotic syndrome in children", "Nephrotic syndrome in children", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 821,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Neuroblastoma", "Neuroblastoma", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 822,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Neuroendocrine tumours", "Neuroendocrine tumours", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 823,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Neuroendocrine tumours and carcinoid syndrome", "Neuroendocrine tumours and carcinoid syndrome", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 824,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Neurofibromatosis type 1", "Neurofibromatosis type 1", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 825,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Neurofibromatosis type 2", "Neurofibromatosis type 2", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 826,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Neuromyelitis optica", "Neuromyelitis optica", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 827,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Newborn jaundice", "Newborn jaundice", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 828,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Newborn respiratory distress syndrome", "Newborn respiratory distress syndrome", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 829,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "NHS Health Check", "NHS Health Check", 1151 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 830,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "NHS screening", "NHS screening", 1151 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 831,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Night sweats", "Night sweats", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 832,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Night terrors and nightmares", "Night terrors and nightmares", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 833,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Nipple discharge", "Nipple discharge", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 834,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Noise sensitivity (hyperacusis)", "Noise sensitivity (hyperacusis)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 835,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Non-alcoholic fatty liver disease (NAFLD)", "Non-alcoholic fatty liver disease (NAFLD)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 836,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Non-allergic rhinitis", "Non-allergic rhinitis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 837,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Non-gonococcal urethritis", "Non-gonococcal urethritis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 838,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Non-Hodgkin lymphoma", "Non-Hodgkin lymphoma", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 839,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Noonan syndrome", "Noonan syndrome", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 840,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Norovirus (vomiting bug)", "Norovirus (vomiting bug)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 841,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Nose cancer", "Nose cancer", 813 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 842,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Nosebleed", "Nosebleed", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 843,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "NSAIDs", "NSAIDs", 875 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 844,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Obesity", "Obesity", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 845,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Obesity risk", "Obesity risk", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 846,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Obsessive compulsive disorder (OCD)", "Obsessive compulsive disorder (OCD)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 847,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Occupational health", "Occupational health", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 848,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Occupational therapy", "Occupational therapy", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 849,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Oesophageal atresia and tracheo-oesophageal fistula", "Oesophageal atresia and tracheo-oesophageal fistula", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 850,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Oesophageal cancer", "Oesophageal cancer", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 851,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Oral thrush (mouth thrush)", "Oral thrush (mouth thrush)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 852,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Orf", "Orf", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 853,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Orthodontics", "Orthodontics", 0 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 854,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Osteoarthritis", "Osteoarthritis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 855,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Osteomalacia", "Osteomalacia", 1007 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 856,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Osteomyelitis", "Osteomyelitis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 857,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Osteopathy", "Osteopathy", 0 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 858,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Osteophyte (bone spur)", "Osteophyte (bone spur)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 859,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Osteoporosis", "Osteoporosis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 860,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Otosclerosis", "Otosclerosis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 861,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Ovarian cancer", "Ovarian cancer", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 862,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Ovarian cyst", "Ovarian cyst", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 863,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Overactive thyroid (hyperthyroidism)", "Overactive thyroid (hyperthyroidism)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 864,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Ovulation pain", "Ovulation pain", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 865,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Oxygen therapy", "Oxygen therapy", 1042 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 866,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Pacemaker implantation", "Pacemaker implantation", 0 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 867,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Paget's disease of bone", "Paget's disease of bone", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 868,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Paget's disease of the nipple", "Paget's disease of the nipple", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 869,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Pain in testicles", "Pain in testicles", 1149 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 870,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Pain in the back of the hand", "Pain in the back of the hand", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 871,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Pain in the ball of the foot", "Pain in the ball of the foot", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 872,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Pain in the bottom of the foot", "Pain in the bottom of the foot", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 873,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Pain in the palm of the hand", "Pain in the palm of the hand", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 874,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Pain in the top of the foot", "Pain in the top of the foot", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 875,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Pain relief", "Pain relief", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 876,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Palpitations", "Palpitations", 563 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 877,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Pancreas transplant", "Pancreas transplant", 0 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 878,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Pancreatic cancer", "Pancreatic cancer", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 879,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Pancreatitis (acute)", "Pancreatitis (acute)", 22 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 880,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Pancreatitis (chronic)", "Pancreatitis (chronic)", 259 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 881,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Panic disorder", "Panic disorder", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 882,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Paralysis", "Paralysis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 883,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Parenting", "Parenting", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 884,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Parkinson's disease", "Parkinson's disease", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 885,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Patau's syndrome", "Patau's syndrome", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 886,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Peak flow test", "Peak flow test", 1151 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 887,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Pelvic inflammatory disease", "Pelvic inflammatory disease", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 888,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Pelvic organ prolapse", "Pelvic organ prolapse", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 889,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Pelvic pain", "Pelvic pain", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 890,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Pemphigus vulgaris", "Pemphigus vulgaris", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 891,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Penile cancer", "Penile cancer", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 892,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Perforated eardrum", "Perforated eardrum", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 893,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Pericarditis", "Pericarditis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 894,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Period pain", "Period pain", 772 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 895,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Periods", "Periods", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 896,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Periods (heavy)", "Periods (heavy)", 572 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 897,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Periods (irregular)", "Periods (irregular)", 664 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 898,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Periods (stopped or missed)", "Periods (stopped or missed)", 1110 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 899,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Peripheral arterial disease (PAD)", "Peripheral arterial disease (PAD)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 900,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Peripheral neuropathy", "Peripheral neuropathy", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 901,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Peritonitis", "Peritonitis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 902,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Persistent trophoblastic disease and choriocarcinoma", "Persistent trophoblastic disease and choriocarcinoma", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 903,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Personality disorder", "Personality disorder", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 904,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Personality disorders", "Personality disorders", 903 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 905,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "PET scan", "PET scan", 1151 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 906,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Phaeochromocytoma", "Phaeochromocytoma", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 907,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Phenylketonuria", "Phenylketonuria", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 908,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Phimosis", "Phimosis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 909,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Phlebitis (superficial thrombophlebitis)", "Phlebitis (superficial thrombophlebitis)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 910,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Phobias", "Phobias", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 911,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Phosphate test", "Phosphate test", 1151 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 912,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Photodynamic therapy (PDT)", "Photodynamic therapy (PDT)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 913,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Physiotherapy", "Physiotherapy", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 914,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Piles", "Piles", 915 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 915,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Piles (haemorrhoids)", "Piles (haemorrhoids)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 916,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Pilonidal sinus", "Pilonidal sinus", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 917,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Pins and needles", "Pins and needles", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 918,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "PIP breast implants", "PIP breast implants", 306 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 919,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Pityriasis rosea", "Pityriasis rosea", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 920,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Pityriasis versicolor", "Pityriasis versicolor", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 921,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Plagiocephaly and brachycephaly (flat head syndrome)", "Plagiocephaly and brachycephaly (flat head syndrome)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 922,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Plantar fasciitis", "Plantar fasciitis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 923,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Plastic surgery", "Plastic surgery", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 924,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Pleurisy", "Pleurisy", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 925,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "PMS (premenstrual syndrome)", "PMS (premenstrual syndrome)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 926,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Pneumonia", "Pneumonia", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 927,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Poisoning", "Poisoning", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 928,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Polio", "Polio", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 929,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Polycystic kidney disease (autosomal dominant)", "Polycystic kidney disease (autosomal dominant)", 102 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 930,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Polycystic kidney disease (autosomal recessive)", "Polycystic kidney disease (autosomal recessive)", 103 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 931,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Polycystic ovary syndrome", "Polycystic ovary syndrome", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 932,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Polycythaemia", "Polycythaemia", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 933,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Polyhydramnios (too much amniotic fluid)", "Polyhydramnios (too much amniotic fluid)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 934,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Polymorphic light eruption", "Polymorphic light eruption", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 935,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Polymyalgia rheumatica", "Polymyalgia rheumatica", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 936,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Pompholyx (dyshidrotic eczema)", "Pompholyx (dyshidrotic eczema)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 937,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Popliteal cyst", "Popliteal cyst", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 938,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Post-herpetic neuralgia", "Post-herpetic neuralgia", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 939,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Post-mortem", "Post-mortem", 0 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 940,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Post-polio syndrome", "Post-polio syndrome", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 941,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Post-traumatic stress disorder (PTSD)", "Post-traumatic stress disorder (PTSD)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 942,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Postmenopausal bleeding", "Postmenopausal bleeding", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 943,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Postnatal depression", "Postnatal depression", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 944,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Postpartum psychosis", "Postpartum psychosis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 945,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Postural tachycardia syndrome (PoTS)", "Postural tachycardia syndrome (PoTS)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 946,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Potassium test", "Potassium test", 1151 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 947,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Prader-Willi syndrome", "Prader-Willi syndrome", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 948,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Pre-eclampsia", "Pre-eclampsia", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 949,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Predictive genetic tests for cancer risk genes", "Predictive genetic tests for cancer risk genes", 1151 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 950,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Pregnancy", "Pregnancy", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 951,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Premature ejaculation", "Premature ejaculation", 408 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 952,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Pressure ulcers (pressure sores)", "Pressure ulcers (pressure sores)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 953,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Priapism (painful erections)", "Priapism (painful erections)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 954,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Prickly heat", "Prickly heat", 571 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 955,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Primary biliary cholangitis (primary biliary cirrhosis)", "Primary biliary cholangitis (primary biliary cirrhosis)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 956,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Probiotics", "Probiotics", 548 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 957,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Problems swallowing pills", "Problems swallowing pills", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 958,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Proctalgia", "Proctalgia", 54 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 959,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Progressive supranuclear palsy", "Progressive supranuclear palsy", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 960,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Prolapse (pelvic organ)", "Prolapse (pelvic organ)", 888 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 961,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Prolonged grief disorder", "Prolonged grief disorder", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 962,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Prosopagnosia (face blindness)", "Prosopagnosia (face blindness)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 963,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Prostate cancer", "Prostate cancer", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 964,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Prostate enlargement", "Prostate enlargement", 120 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 965,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Prostate problems", "Prostate problems", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 966,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Prostatitis", "Prostatitis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 967,
                column: "SupercededById",
                value: null);

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 968,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Psoriatic arthritis", "Psoriatic arthritis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 969,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Psychiatry", "Psychiatry", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 970,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Psychosis", "Psychosis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 971,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Psychotic depression", "Psychotic depression", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 972,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Puberty (early or delayed)", "Puberty (early or delayed)", 393 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 973,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Pubic lice", "Pubic lice", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 974,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Public health", "Public health", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 975,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Pudendal neuralgia", "Pudendal neuralgia", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 976,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Pulmonary embolism", "Pulmonary embolism", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 977,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Pulmonary fibrosis", "Pulmonary fibrosis", 642 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 978,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Pulmonary hypertension", "Pulmonary hypertension", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 979,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Pyoderma gangrenosum", "Pyoderma gangrenosum", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 980,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Q fever", "Q fever", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 981,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Quinsy", "Quinsy", 1174 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 982,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Rabies", "Rabies", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 983,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Radiotherapy", "Radiotherapy", 210 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 984,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Rashes in babies and children", "Rashes in babies and children", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 985,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Raynaud's", "Raynaud's", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 986,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Reactive arthritis", "Reactive arthritis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 987,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Rectal bleeding", "Rectal bleeding", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 988,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Rectal cancer", "Rectal cancer", 162 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 989,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Rectal examination", "Rectal examination", 1151 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 990,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Red blood cell count", "Red blood cell count", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 991,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Red eye", "Red eye", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 992,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Reflux in babies", "Reflux in babies", 11 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 993,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Repetitive strain injury (RSI)", "Repetitive strain injury (RSI)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 994,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Respiratory syncytial virus (RSV)", "Respiratory syncytial virus (RSV)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 995,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Respiratory tract infections (RTIs)", "Respiratory tract infections (RTIs)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 996,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Restless legs syndrome", "Restless legs syndrome", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 997,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Restricted growth (dwarfism)", "Restricted growth (dwarfism)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 998,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Retinal detachment", "Retinal detachment", 344 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 999,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Retinal migraine", "Retinal migraine", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1000,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Retinoblastoma (eye cancer in children)", "Retinoblastoma (eye cancer in children)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1001,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Rett syndrome", "Rett syndrome", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1002,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Reye's syndrome", "Reye's syndrome", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1003,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Rhesus disease", "Rhesus disease", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1004,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Rheumatic fever", "Rheumatic fever", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1005,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Rheumatoid arthritis", "Rheumatoid arthritis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1006,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Rhinitis (allergic)", "Rhinitis (allergic)", 38 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1007,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Rickets and osteomalacia", "Rickets and osteomalacia", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1008,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Ringworm", "Ringworm", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1009,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Root canal treatment", "Root canal treatment", 0 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1010,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Rosacea", "Rosacea", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1011,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Roseola", "Roseola", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1012,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Roundworm", "Roundworm", 1264 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1013,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Rubella (german measles)", "Rubella (german measles)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1014,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Salivary gland stones", "Salivary gland stones", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1015,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Sarcoidosis", "Sarcoidosis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1016,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "SARS (severe acute respiratory syndrome)", "SARS (severe acute respiratory syndrome)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1017,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Scabies", "Scabies", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1018,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Scarlet fever", "Scarlet fever", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1019,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Scars", "Scars", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1020,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Schistosomiasis (bilharzia)", "Schistosomiasis (bilharzia)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1021,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Schizophrenia", "Schizophrenia", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1022,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Sciatica", "Sciatica", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1023,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Scleroderma", "Scleroderma", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1024,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Scoliosis", "Scoliosis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1025,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Scurvy", "Scurvy", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1026,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Seasonal affective disorder (SAD)", "Seasonal affective disorder (SAD)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1027,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Seizures (children with fever)", "Seizures (children with fever)", 452 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1028,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Seizures (fits)", "Seizures (fits)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1029,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Selective mutism", "Selective mutism", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1030,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Selective serotonin reuptake inhibitors (SSRIs)", "Selective serotonin reuptake inhibitors (SSRIs)", 773 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1031,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Self-harm", "Self-harm", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1032,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Sense of smell (lost/changed)", "Sense of smell (lost/changed)", 734 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1033,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Sepsis", "Sepsis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1034,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Septic arthritis", "Septic arthritis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1035,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Severe head injury", "Severe head injury", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1036,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Sexually transmitted infections (STIs)", "Sexually transmitted infections (STIs)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1037,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "shaking", "shaking", 1191 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1038,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Shin pain (shin splints)", "Shin pain (shin splints)", 1039 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1039,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Shin splints", "Shin splints", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1040,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Shingles", "Shingles", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1041,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Short-sightedness (myopia)", "Short-sightedness (myopia)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1042,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Shortness of breath", "Shortness of breath", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1043,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Shoulder impingement", "Shoulder impingement", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1044,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Shoulder pain", "Shoulder pain", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1045,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Sick building syndrome", "Sick building syndrome", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1046,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Sickle cell disease", "Sickle cell disease", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1047,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Silicosis", "Silicosis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1048,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Sinus cancer", "Sinus cancer", 813 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1049,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Sinusitis", "Sinusitis", 1050 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1050,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Sinusitis (sinus infection)", "Sinusitis (sinus infection)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1051,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Sjögren's syndrome", "Sjögren's syndrome", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1052,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Skin cancer (melanoma)", "Skin cancer (melanoma)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1053,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Skin cancer (non-melanoma)", "Skin cancer (non-melanoma)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1054,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Skin cyst", "Skin cyst", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1055,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Skin picking disorder", "Skin picking disorder", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1056,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Skin tags", "Skin tags", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1057,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Slapped cheek syndrome", "Slapped cheek syndrome", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1058,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Sleep apnoea", "Sleep apnoea", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1059,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Sleep paralysis", "Sleep paralysis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1060,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Sleeping well", "Sleeping well", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1061,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Sleepwalking", "Sleepwalking", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1062,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Slipped disc", "Slipped disc", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1063,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Small bowel transplant", "Small bowel transplant", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1064,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Smear test", "Smear test", 232 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1065,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Smelly feet", "Smelly feet", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1066,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Smelly urine", "Smelly urine", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1067,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Smoking", "Smoking", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1068,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Smoking (treatments to stop)", "Smoking (treatments to stop)", 1067 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1069,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Snake bites", "Snake bites", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1070,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Snoring", "Snoring", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1071,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Social anxiety (social phobia)", "Social anxiety (social phobia)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1072,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Social care and support guide", "Social care and support guide", 646 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1073,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Soft tissue sarcomas", "Soft tissue sarcomas", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1074,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Soiling (child pooing their pants)", "Soiling (child pooing their pants)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1075,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Solar keratoses", "Solar keratoses", 15 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1076,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Sore lips", "Sore lips", 1077 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1077,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Sore or dry lips", "Sore or dry lips", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1078,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Sore or white tongue", "Sore or white tongue", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1079,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Sore throat", "Sore throat", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1080,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Sperm count (low)", "Sperm count (low)", 738 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1081,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Spina bifida", "Spina bifida", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1082,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Spinal muscular atrophy", "Spinal muscular atrophy", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1083,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Spirometry", "Spirometry", 1151 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1084,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Spleen problems and spleen removal", "Spleen problems and spleen removal", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1085,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Spondylolisthesis", "Spondylolisthesis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1086,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Sports injuries", "Sports injuries", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1087,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Sprains and strains", "Sprains and strains", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1088,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Squamous cell carcinoma", "Squamous cell carcinoma", 1053 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1089,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Squint", "Squint", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1090,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Stammering", "Stammering", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1091,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Staph infection", "Staph infection", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1092,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Statins", "Statins", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1093,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Stem cell and bone marrow transplants", "Stem cell and bone marrow transplants", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1094,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Stent insertion", "Stent insertion", 299 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1095,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Steroid cream", "Steroid cream", 1179 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1096,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Steroid inhalers", "Steroid inhalers", 92 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1097,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Steroid injections", "Steroid injections", 1101 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1098,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Steroid misuse", "Steroid misuse", 47 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1099,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Steroid nasal sprays", "Steroid nasal sprays", 1101 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1100,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Steroid tablets", "Steroid tablets", 1101 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1101,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Steroids", "Steroids", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1102,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Stevens-Johnson syndrome", "Stevens-Johnson syndrome", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1103,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Stillbirth", "Stillbirth", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1104,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Sting or bite (insect)", "Sting or bite (insect)", 656 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1105,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Stomach ache", "Stomach ache", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1106,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Stomach bug", "Stomach bug", 360 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1107,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Stomach cancer", "Stomach cancer", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1108,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Stomach ulcer", "Stomach ulcer", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1109,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Stop smoking treatments", "Stop smoking treatments", 1067 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1110,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Stopped or missed periods", "Stopped or missed periods", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1111,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Stretch marks", "Stretch marks", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1112,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Stroke", "Stroke", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1113,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Stuttering", "Stuttering", 1090 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1114,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Stye", "Stye", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1115,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Subarachnoid haemorrhage", "Subarachnoid haemorrhage", 172 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1116,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Subdural haematoma", "Subdural haematoma", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1117,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Sudden confusion (delirium)", "Sudden confusion (delirium)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1118,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Sudden infant death syndrome (SIDS)", "Sudden infant death syndrome (SIDS)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1119,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Suicidal thoughts", "Suicidal thoughts", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1120,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Sunburn", "Sunburn", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1121,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Superficial thrombophlebitis", "Superficial thrombophlebitis", 909 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1122,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Supplements", "Supplements", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1123,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Supraventricular tachycardia (SVT)", "Supraventricular tachycardia (SVT)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1124,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Surgery (having an operation)", "Surgery (having an operation)", 0 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1125,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Swallowing pills", "Swallowing pills", 957 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1126,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Swallowing problems", "Swallowing problems", 385 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1127,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "sweat rash", "sweat rash", 571 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1128,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Sweating (excessive)", "Sweating (excessive)", 437 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1129,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Sweating at night", "Sweating at night", 831 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1130,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Swine flu (H1N1)", "Swine flu (H1N1)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1131,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Swollen ankles feet and legs (oedema)", "Swollen ankles feet and legs (oedema)", 1132 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1132,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Swollen ankles, feet and legs (oedema)", "Swollen ankles, feet and legs (oedema)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1133,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Swollen arms and hands (oedema)", "Swollen arms and hands (oedema)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1134,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Swollen glands", "Swollen glands", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1135,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Syphilis", "Syphilis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1136,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Tailbone (coccyx) pain", "Tailbone (coccyx) pain", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1137,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Tapeworm", "Tapeworm", 1264 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1138,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Tay-Sachs disease", "Tay-Sachs disease", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1139,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Teeth grinding (bruxism)", "Teeth grinding (bruxism)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1140,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Teeth whitening", "Teeth whitening", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1141,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Temporal arteritis", "Temporal arteritis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1142,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Temporomandibular disorder (TMD)", "Temporomandibular disorder (TMD)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1143,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Tendonitis", "Tendonitis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1144,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Tennis elbow", "Tennis elbow", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1145,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "TENS (transcutaneous electrical nerve stimulation)", "TENS (transcutaneous electrical nerve stimulation)", 875 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1146,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Tension headaches", "Tension headaches", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1147,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Tension-type headaches", "Tension-type headaches", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1148,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Testicle lumps and swellings", "Testicle lumps and swellings", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1149,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Testicle pain", "Testicle pain", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1150,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Testicular cancer", "Testicular cancer", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1151,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Tests, scans and screening", "Tests, scans and screening", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1152,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Tetanus", "Tetanus", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1153,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Thalassaemia", "Thalassaemia", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1154,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "The 'male menopause'", "The 'male menopause'", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1155,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Thirst (excessive)", "Thirst (excessive)", 438 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1156,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Thoracic outlet syndrome", "Thoracic outlet syndrome", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1157,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Threadworms", "Threadworms", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1158,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Throat (sore)", "Throat (sore)", 1079 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1159,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Thrombophilia", "Thrombophilia", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1160,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Thrush in men and women", "Thrush in men and women", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1161,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Thumb pain", "Thumb pain", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1162,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Thyroid cancer", "Thyroid cancer", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1163,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Thyroiditis", "Thyroiditis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1164,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "TIA", "TIA", 481 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1165,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Tick-borne encephalitis (TBE)", "Tick-borne encephalitis (TBE)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1166,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Tics", "Tics", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1167,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Tight foreskin (phimosis and paraphimosis)", "Tight foreskin (phimosis and paraphimosis)", 479 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1168,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Tight foreskin (phimosis)", "Tight foreskin (phimosis)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1169,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Tinnitus", "Tinnitus", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1170,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Toe pain", "Toe pain", 478 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1171,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Tongue (sore or white)", "Tongue (sore or white)", 1078 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1172,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Tongue cancer", "Tongue cancer", 789 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1173,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Tongue-tie", "Tongue-tie", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1174,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Tonsillitis", "Tonsillitis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1175,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Tooth (chipped or broken)", "Tooth (chipped or broken)", 245 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1176,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Tooth decay", "Tooth decay", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1177,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Tooth knocked out", "Tooth knocked out", 693 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1178,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Toothache", "Toothache", 341 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1179,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Topical corticosteroids", "Topical corticosteroids", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1180,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Total iron-binding capacity (TIBC) and transferrin test", "Total iron-binding capacity (TIBC) and transferrin test", 1151 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1181,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Total protein test", "Total protein test", 1151 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1182,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Tourette's syndrome", "Tourette's syndrome", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1183,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Toxic shock syndrome", "Toxic shock syndrome", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1184,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Toxocariasis", "Toxocariasis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1185,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Toxoplasmosis", "Toxoplasmosis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1186,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Tracheostomy", "Tracheostomy", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1187,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Transient ischaemic attack (TIA)", "Transient ischaemic attack (TIA)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1188,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Transurethral resection of the prostate (TURP)", "Transurethral resection of the prostate (TURP)", 964 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1189,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Travel vaccinations", "Travel vaccinations", 1221 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1190,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "tremor", "tremor", 1191 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1191,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Tremor or shaking hands", "Tremor or shaking hands", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1192,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Trichomoniasis", "Trichomoniasis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1193,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Trichotillomania (hair pulling disorder)", "Trichotillomania (hair pulling disorder)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1194,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Trigeminal neuralgia", "Trigeminal neuralgia", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1195,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Trigger finger", "Trigger finger", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1196,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Trimethylaminuria ('fish odour syndrome')", "Trimethylaminuria ('fish odour syndrome')", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1197,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Tuberculosis (TB)", "Tuberculosis (TB)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1198,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Tuberous sclerosis", "Tuberous sclerosis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1199,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Tummy ache", "Tummy ache", 1105 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1200,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Tummy bug", "Tummy bug", 360 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1201,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Turner syndrome", "Turner syndrome", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1202,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Twitching eyes and muscles", "Twitching eyes and muscles", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1203,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Type 1 diabetes", "Type 1 diabetes", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1204,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Type 2 diabetes", "Type 2 diabetes", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1205,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Typhoid fever", "Typhoid fever", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1206,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Typhus", "Typhus", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1207,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Ulcerative colitis", "Ulcerative colitis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1208,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Ultrasound scan", "Ultrasound scan", 1151 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1209,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Umbilical hernia repair", "Umbilical hernia repair", 585 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1210,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Underactive thyroid (hypothyroidism)", "Underactive thyroid (hypothyroidism)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1211,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Undescended testicles", "Undescended testicles", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1212,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Unintentional weight loss", "Unintentional weight loss", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1213,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Urethritis (NGU)", "Urethritis (NGU)", 837 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1214,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Urinary catheter", "Urinary catheter", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1215,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Urinary incontinence", "Urinary incontinence", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1216,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Urinary tract infections (UTIs)", "Urinary tract infections (UTIs)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1217,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Urine (smelly)", "Urine (smelly)", 1066 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1218,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Urine albumin to creatinine ratio (ACR)", "Urine albumin to creatinine ratio (ACR)", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1219,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Uterine (womb) cancer", "Uterine (womb) cancer", 1263 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1220,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Uveitis", "Uveitis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1221,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Vaccinations", "Vaccinations", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1222,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Vaginal cancer", "Vaginal cancer", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1223,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Vaginal discharge", "Vaginal discharge", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1224,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Vaginal dryness", "Vaginal dryness", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1225,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Vaginal pain", "Vaginal pain", 1246 });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1226,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Vaginismus", "Vaginismus", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1227,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Vaginitis", "Vaginitis", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1228,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Varicose eczema", "Varicose eczema", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1229,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Varicose veins", "Varicose veins", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1230,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Vascular dementia", "Vascular dementia", null });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1231,
                columns: new[] { "Code", "Description", "SupercededById" },
                values: new object[] { "Vasculitis", "Vasculitis", null });

            migrationBuilder.InsertData(
                table: "SysRefHealthCondition",
                columns: new[] { "Id", "Code", "Description", "IsDeleted", "SupercededById" },
                values: new object[,]
                {
                    { 1232, "Vegetative state", "Vegetative state", false, 367 },
                    { 1233, "Venous leg ulcer", "Venous leg ulcer", false, null },
                    { 1234, "Vertigo", "Vertigo", false, null },
                    { 1235, "Vestibular neuritis", "Vestibular neuritis", false, 697 },
                    { 1236, "Vestibular schwannoma", "Vestibular schwannoma", false, 13 },
                    { 1237, "Vitamin B12 or folate deficiency anaemia", "Vitamin B12 or folate deficiency anaemia", false, null },
                    { 1238, "Vitamins and minerals", "Vitamins and minerals", false, 548 },
                    { 1239, "Vitiligo", "Vitiligo", false, null },
                    { 1240, "Vomiting", "Vomiting", false, 840 },
                    { 1241, "Vomiting blood", "Vomiting blood", false, 1242 },
                    { 1242, "Vomiting blood (haematemesis)", "Vomiting blood (haematemesis)", false, null },
                    { 1243, "Vomiting bug", "Vomiting bug", false, 840 },
                    { 1244, "Von Willebrand disease", "Von Willebrand disease", false, null },
                    { 1245, "Vulval cancer", "Vulval cancer", false, null },
                    { 1246, "Vulvodynia (vulval pain)", "Vulvodynia (vulval pain)", false, null },
                    { 1247, "Warts and verrucas", "Warts and verrucas", false, null },
                    { 1248, "Watering eyes", "Watering eyes", false, null },
                    { 1249, "Weight loss (unexpected)", "Weight loss (unexpected)", false, 1212 },
                    { 1250, "Weight loss (unintentional)", "Weight loss (unintentional)", false, 1212 },
                    { 1251, "Weight loss surgery", "Weight loss surgery", false, 844 },
                    { 1252, "Weil's disease", "Weil's disease", false, 711 },
                    { 1253, "West Nile virus", "West Nile virus", false, null },
                    { 1254, "What to do if someone has a seizure (fit)", "What to do if someone has a seizure (fit)", false, 1028 },
                    { 1255, "Whiplash", "Whiplash", false, null },
                    { 1256, "White blood cell count (low)", "White blood cell count (low)", false, 739 },
                    { 1257, "Whitlow finger", "Whitlow finger", false, 593 },
                    { 1258, "Whooping cough", "Whooping cough", false, null },
                    { 1259, "Wind", "Wind", false, 451 },
                    { 1260, "Winter vomiting bug", "Winter vomiting bug", false, 840 },
                    { 1261, "Wisdom tooth removal", "Wisdom tooth removal", false, 0 },
                    { 1262, "Wolff-Parkinson-White syndrome", "Wolff-Parkinson-White syndrome", false, null },
                    { 1263, "Womb (uterus) cancer", "Womb (uterus) cancer", false, null },
                    { 1264, "Worms in humans", "Worms in humans", false, null },
                    { 1265, "Wrist pain", "Wrist pain", false, null },
                    { 1266, "X-ray", "X-ray", false, 1151 },
                    { 1267, "Yellow fever", "Yellow fever", false, null },
                    { 1268, "Your contraception guide", "Your contraception guide", false, 296 },
                    { 1269, "Zika virus", "Zika virus", false, null }
                });


            migrationBuilder.Sql(@"
            CREATE VIEW ParticipantActiveHealthCondition AS
                SELECT
                    PHC.ParticipantId,
                    ISNULL(RefA.SupercededById, RefA.Id) AS 'Active Health Condition'
                FROM
                    dte.ParticipantHealthCondition PHC
                        INNER JOIN
                    SysRefHealthCondition RefA ON PHC.HealthConditionId = RefA.Id
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.Sql(@"DROP VIEW ParticipantActiveHealthCondition");

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1232);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1233);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1234);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1235);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1236);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1237);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1238);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1239);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1240);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1241);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1242);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1243);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1244);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1245);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1246);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1247);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1248);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1249);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1250);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1251);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1252);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1253);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1254);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1255);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1256);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1257);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1258);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1259);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1260);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1261);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1262);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1263);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1264);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1265);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1266);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1267);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1268);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1269);

            migrationBuilder.DropColumn(
                name: "SupercededById",
                table: "SysRefHealthCondition");

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Acanthosis nigricans", "Acanthosis nigricans" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Achalasia", "Achalasia" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Acid and chemical burns", "Acid and chemical burns" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Acoustic neuroma (vestibular schwannoma)", "Acoustic neuroma (vestibular schwannoma)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Vestibular schwannoma", "Vestibular schwannoma" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Acromegaly", "Acromegaly" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Gigantism", "Gigantism" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Urine albumin to creatinine ratio (ACR)", "Urine albumin to creatinine ratio (ACR)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Actinic keratoses (solar keratoses)", "Actinic keratoses (solar keratoses)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Solar keratoses", "Solar keratoses" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Acupuncture", "Acupuncture" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Acute cholecystitis", "Acute cholecystitis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Gallbladder pain", "Gallbladder pain" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Cholecystitis (acute)", "Cholecystitis (acute)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "Code", "Description" },
                values: new object[] { "MND", "MND" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Acute kidney injury", "Acute kidney injury" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Acute respiratory distress syndrome", "Acute respiratory distress syndrome" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Adenoidectomy", "Adenoidectomy" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Air or gas embolism", "Air or gas embolism" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Decompression sickness", "Decompression sickness" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Alcohol poisoning", "Alcohol poisoning" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Alexander technique", "Alexander technique" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Alkaptonuria", "Alkaptonuria" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Amputation", "Amputation" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Amyloidosis", "Amyloidosis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Anabolic steroid misuse", "Anabolic steroid misuse" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Steroid misuse", "Steroid misuse" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Anaesthesia", "Anaesthesia" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Anal cancer", "Anal cancer" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Anal pain", "Anal pain" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Proctalgia", "Proctalgia" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Angelman syndrome", "Angelman syndrome" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Animal and human bites", "Animal and human bites" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Bite (animal or human)", "Bite (animal or human)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Anosmia", "Anosmia" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Antacids", "Antacids" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Antihistamines", "Antihistamines" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Antisocial personality disorder", "Antisocial personality disorder" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Anxiety disorders in children", "Anxiety disorders in children" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Arrhythmia", "Arrhythmia" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Heart rhythm problems", "Heart rhythm problems" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Arterial thrombosis", "Arterial thrombosis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Intrauterine insemination (IUI)", "Intrauterine insemination (IUI)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 44,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Asbestosis", "Asbestosis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 45,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Aspirin", "Aspirin" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 46,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Atherosclerosis (arteriosclerosis)", "Atherosclerosis (arteriosclerosis)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 47,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Athlete's foot", "Athlete's foot" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 48,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Auditory processing disorder (APD)", "Auditory processing disorder (APD)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 49,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Balanitis", "Balanitis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 50,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Barium enema", "Barium enema" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 51,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Bedbugs", "Bedbugs" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 52,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Beta blockers", "Beta blockers" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 53,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Black eye", "Black eye" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 54,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Blood clots", "Blood clots" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 55,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Blood groups", "Blood groups" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 56,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Blood in semen (haematospermia)", "Blood in semen (haematospermia)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 57,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Blood in urine", "Blood in urine" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 58,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Blood pressure test", "Blood pressure test" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 59,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Body dysmorphic disorder (BDD)", "Body dysmorphic disorder (BDD)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 60,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Infected piercings", "Infected piercings" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 61,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Boils", "Boils" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 62,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Botulism", "Botulism" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 63,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Bowel polyps", "Bowel polyps" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 64,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Bowen's disease", "Bowen's disease" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 65,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Brain tumours", "Brain tumours" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 66,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Breast pain", "Breast pain" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 67,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Breast reduction on the NHS", "Breast reduction on the NHS" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 68,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Breath-holding in babies and children", "Breath-holding in babies and children" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 69,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Broken ankle", "Broken ankle" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 70,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Broken arm or wrist", "Broken arm or wrist" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 71,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Broken collarbone", "Broken collarbone" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 72,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Broken finger or thumb", "Broken finger or thumb" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 73,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Broken leg", "Broken leg" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 74,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Broken nose", "Broken nose" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 75,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Broken or bruised ribs", "Broken or bruised ribs" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 76,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Broken toe", "Broken toe" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 77,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Bronchitis", "Bronchitis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 78,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Brucellosis", "Brucellosis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 79,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Brugada syndrome", "Brugada syndrome" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 80,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Carbon monoxide poisoning", "Carbon monoxide poisoning" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 81,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Neuroendocrine tumours and carcinoid syndrome", "Neuroendocrine tumours and carcinoid syndrome" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 82,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Cardiomyopathy", "Cardiomyopathy" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 83,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Cardiovascular disease", "Cardiovascular disease" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 84,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Age-related cataracts", "Age-related cataracts" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 85,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Cataracts (age-related)", "Cataracts (age-related)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 86,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Catarrh", "Catarrh" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 87,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Cavernoma", "Cavernoma" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 88,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Clostridium difficile (C. diff) infection", "Clostridium difficile (C. diff) infection" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 89,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Carcinoembryonic antigen (CEA) test", "Carcinoembryonic antigen (CEA) test" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 90,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Cervical rib", "Cervical rib" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 91,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Thoracic outlet syndrome", "Thoracic outlet syndrome" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 92,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Charles Bonnet syndrome", "Charles Bonnet syndrome" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 93,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Chest infection", "Chest infection" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 94,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Chest pain", "Chest pain" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 95,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Heart pain", "Heart pain" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 96,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Chiari malformation", "Chiari malformation" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 97,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Chilblains", "Chilblains" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 98,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Chiropractic", "Chiropractic" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 99,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Cholesteatoma", "Cholesteatoma" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 100,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Chronic traumatic encephalopathy", "Chronic traumatic encephalopathy" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 101,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Circumcision in boys", "Circumcision in boys" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 102,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Circumcision in men", "Circumcision in men" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 103,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Claustrophobia", "Claustrophobia" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 104,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Cluster headaches", "Cluster headaches" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 105,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Colour vision deficiency (colour blindness)", "Colour vision deficiency (colour blindness)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 106,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Coma", "Coma" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 107,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Compartment syndrome", "Compartment syndrome" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 108,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Concussion", "Concussion" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 109,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Sudden confusion (delirium)", "Sudden confusion (delirium)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 110,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Confusion (sudden)", "Confusion (sudden)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 111,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Delirium", "Delirium" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 112,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Costochondritis", "Costochondritis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 113,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Cough", "Cough" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 114,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Coughing up blood (blood in phlegm)", "Coughing up blood (blood in phlegm)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 115,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Cradle cap", "Cradle cap" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 116,
                columns: new[] { "Code", "Description" },
                values: new object[] { "CT scan", "CT scan" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 117,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Cuts and grazes", "Cuts and grazes" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 118,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Blue skin or lips (cyanosis)", "Blue skin or lips (cyanosis)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 119,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Cyanosis", "Cyanosis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 120,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Cyclical vomiting syndrome", "Cyclical vomiting syndrome" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 121,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Cyclospora", "Cyclospora" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 122,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Cyclothymia", "Cyclothymia" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 123,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Dandruff", "Dandruff" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 124,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Decongestants", "Decongestants" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 125,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Dental abscess", "Dental abscess" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 126,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Dentures (false teeth)", "Dentures (false teeth)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 127,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Dyspraxia (developmental co-ordination disorder) in adults", "Dyspraxia (developmental co-ordination disorder) in adults" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 128,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Developmental dysplasia of the hip", "Developmental dysplasia of the hip" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 129,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Congenital hip dislocation", "Congenital hip dislocation" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 130,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hip dysplasia", "Hip dysplasia" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 131,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Diabetes", "Diabetes" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 132,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Diabetic eye screening", "Diabetic eye screening" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 133,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Diabetic ketoacidosis", "Diabetic ketoacidosis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 134,
                columns: new[] { "Code", "Description" },
                values: new object[] { "DiGeorge syndrome (22q11 deletion)", "DiGeorge syndrome (22q11 deletion)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 135,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Dislocated kneecap", "Dislocated kneecap" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 136,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Dislocated shoulder", "Dislocated shoulder" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 137,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Differences in sex development", "Differences in sex development" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 138,
                columns: new[] { "Code", "Description" },
                values: new object[] { "intersex", "intersex" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 139,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Dissociative disorders", "Dissociative disorders" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 140,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Diverticular disease and diverticulitis", "Diverticular disease and diverticulitis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 141,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Dizziness", "Dizziness" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 142,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Dry mouth", "Dry mouth" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 143,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Dysarthria (difficulty speaking)", "Dysarthria (difficulty speaking)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 144,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Dysentery", "Dysentery" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 145,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Earache", "Earache" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 146,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Early or delayed puberty", "Early or delayed puberty" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 147,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Puberty (early or delayed)", "Puberty (early or delayed)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 148,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Earwax build-up", "Earwax build-up" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 149,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Eating disorders", "Eating disorders" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 150,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Ebola virus disease", "Ebola virus disease" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 151,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Echocardiogram", "Echocardiogram" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 152,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Ectropion", "Ectropion" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 153,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Edwards' syndrome (trisomy 18)", "Edwards' syndrome (trisomy 18)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 154,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Ehlers-Danlos syndromes", "Ehlers-Danlos syndromes" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 155,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Ejaculation problems", "Ejaculation problems" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 156,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Premature ejaculation", "Premature ejaculation" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 157,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Elbow and arm pain", "Elbow and arm pain" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 158,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Electrocardiogram (ECG)", "Electrocardiogram (ECG)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 159,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Electroencephalogram (EEG)", "Electroencephalogram (EEG)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 160,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Electrolyte test", "Electrolyte test" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 161,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Embolism", "Embolism" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 162,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Emollients", "Emollients" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 163,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Empyema", "Empyema" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 164,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Endoscopy", "Endoscopy" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 165,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Enhanced recovery", "Enhanced recovery" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 166,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Epididymitis", "Epididymitis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 167,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Epiglottitis", "Epiglottitis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 168,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Erythema multiforme", "Erythema multiforme" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 169,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Erythema nodosum", "Erythema nodosum" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 170,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Erythromelalgia", "Erythromelalgia" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 171,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Euthanasia and assisted suicide", "Euthanasia and assisted suicide" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 172,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Ewing sarcoma", "Ewing sarcoma" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 173,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Excessive daytime sleepiness (hypersomnia)", "Excessive daytime sleepiness (hypersomnia)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 174,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hypersomnia", "Hypersomnia" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 175,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Eye cancer", "Eye cancer" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 176,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Eye injuries", "Eye injuries" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 177,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Eyelid problems", "Eyelid problems" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 178,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Eye tests for children", "Eye tests for children" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 179,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Prosopagnosia (face blindness)", "Prosopagnosia (face blindness)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 180,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Face blindness", "Face blindness" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 181,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Febrile seizures", "Febrile seizures" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 182,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Fits (children with fever)", "Fits (children with fever)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 183,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Seizures (children with fever)", "Seizures (children with fever)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 184,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Female genital mutilation (FGM)", "Female genital mutilation (FGM)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 185,
                columns: new[] { "Code", "Description" },
                values: new object[] { "High temperature (fever) in children", "High temperature (fever) in children" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 186,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Fever in children", "Fever in children" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 187,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Flat feet", "Flat feet" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 188,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Fluoride", "Fluoride" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 189,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Foetal alcohol spectrum disorder", "Foetal alcohol spectrum disorder" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 190,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Food colours and hyperactivity", "Food colours and hyperactivity" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 191,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Food intolerance", "Food intolerance" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 192,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Foot drop", "Foot drop" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 193,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Gallbladder cancer", "Gallbladder cancer" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 194,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Ganglion cyst", "Ganglion cyst" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 195,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Gastritis", "Gastritis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 196,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Gastroparesis", "Gastroparesis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 197,
                columns: new[] { "Code", "Description" },
                values: new object[] { "General anaesthesia", "General anaesthesia" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 198,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Gilbert's syndrome", "Gilbert's syndrome" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 199,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Glutaric aciduria type 1", "Glutaric aciduria type 1" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 200,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Granuloma annulare", "Granuloma annulare" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 201,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Granulomatosis with polyangiitis", "Granulomatosis with polyangiitis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 202,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Growing pains", "Growing pains" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 203,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hair dye reactions", "Hair dye reactions" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 204,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hairy cell leukaemia", "Hairy cell leukaemia" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 205,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Leukaemia (hairy cell)", "Leukaemia (hairy cell)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 206,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hallucinations and hearing voices", "Hallucinations and hearing voices" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 207,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hearing voices", "Hearing voices" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 208,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hamstring injury", "Hamstring injury" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 209,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hand foot and mouth disease", "Hand foot and mouth disease" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 210,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Head and neck cancer", "Head and neck cancer" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 211,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Health anxiety", "Health anxiety" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 212,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hypochondria", "Hypochondria" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 213,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hearing tests for children", "Hearing tests for children" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 214,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Heart-lung transplant", "Heart-lung transplant" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 215,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Heart palpitations and ectopic beats", "Heart palpitations and ectopic beats" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 216,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Palpitations", "Palpitations" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 217,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Ectopic beats", "Ectopic beats" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 218,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Heat exhaustion and heatstroke", "Heat exhaustion and heatstroke" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 219,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Heat rash (prickly heat)", "Heat rash (prickly heat)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 220,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Prickly heat", "Prickly heat" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 221,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Sweating (excessive)", "Sweating (excessive)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 222,
                columns: new[] { "Code", "Description" },
                values: new object[] { "sweat rash", "sweat rash" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 223,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Henoch-Schönlein purpura (HSP)", "Henoch-Schönlein purpura (HSP)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 224,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hepatitis", "Hepatitis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 225,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Herbal medicines", "Herbal medicines" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 226,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Herceptin (trastuzumab)", "Herceptin (trastuzumab)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 227,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hereditary haemorrhagic telangiectasia (HHT)", "Hereditary haemorrhagic telangiectasia (HHT)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 228,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hereditary neuropathy with pressure palsies (HNPP)", "Hereditary neuropathy with pressure palsies (HNPP)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 229,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hereditary spastic paraplegia", "Hereditary spastic paraplegia" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 230,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hernia", "Hernia" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 231,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Herpes simplex eye infections", "Herpes simplex eye infections" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 232,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Eye infection (herpes)", "Eye infection (herpes)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 233,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Herpetic whitlow (whitlow finger)", "Herpetic whitlow (whitlow finger)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 234,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Whitlow finger", "Whitlow finger" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 235,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Haemophilus influenzae type b (Hib)", "Haemophilus influenzae type b (Hib)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 236,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hidradenitis suppurativa (HS)", "Hidradenitis suppurativa (HS)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 237,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hyperglycaemia (high blood sugar)", "Hyperglycaemia (high blood sugar)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 238,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hip pain in adults", "Hip pain in adults" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 239,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hirschsprung's disease", "Hirschsprung's disease" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 240,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hoarding disorder", "Hoarding disorder" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 241,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Homeopathy", "Homeopathy" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 242,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Home oxygen therapy", "Home oxygen therapy" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 243,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Oxygen therapy", "Oxygen therapy" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 244,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Homocystinuria", "Homocystinuria" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 245,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Noise sensitivity (hyperacusis)", "Noise sensitivity (hyperacusis)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 246,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hypnotherapy", "Hypnotherapy" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 247,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hypothermia", "Hypothermia" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 248,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Ichthyosis", "Ichthyosis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 249,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Indigestion", "Indigestion" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 250,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Inflammatory bowel disease", "Inflammatory bowel disease" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 251,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Ingrown hairs", "Ingrown hairs" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 252,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Ingrown toenail", "Ingrown toenail" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 253,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Intensive care", "Intensive care" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 254,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Interstitial cystitis", "Interstitial cystitis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 255,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Intracranial hypertension", "Intracranial hypertension" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 256,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hip pain in children (irritable hip)", "Hip pain in children (irritable hip)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 257,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Irritable hip", "Irritable hip" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 258,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Isovaleric acidaemia", "Isovaleric acidaemia" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 259,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Joint pain", "Joint pain" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 260,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Kaposi's sarcoma", "Kaposi's sarcoma" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 261,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Keratosis pilaris", "Keratosis pilaris" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 262,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Klinefelter syndrome", "Klinefelter syndrome" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 263,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Knee pain", "Knee pain" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 264,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Knock knees", "Knock knees" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 265,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Kwashiorkor", "Kwashiorkor" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 266,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Labial fusion", "Labial fusion" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 267,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Lambert-Eaton myasthenic syndrome", "Lambert-Eaton myasthenic syndrome" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 268,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Lactate dehydrogenase (LDH) test", "Lactate dehydrogenase (LDH) test" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 269,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Legionnaires' disease", "Legionnaires' disease" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 270,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Lichen sclerosus", "Lichen sclerosus" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 271,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Limping in children", "Limping in children" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 272,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Lipoedema", "Lipoedema" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 273,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Lipoma", "Lipoma" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 274,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Liver disease", "Liver disease" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 275,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Local anaesthesia", "Local anaesthesia" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 276,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Long QT syndrome", "Long QT syndrome" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 277,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Loss of libido (reduced sex drive)", "Loss of libido (reduced sex drive)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 278,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Low blood sugar (hypoglycaemia)", "Low blood sugar (hypoglycaemia)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 279,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hypoglycaemia (low blood sugar)", "Hypoglycaemia (low blood sugar)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 280,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Low sperm count", "Low sperm count" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 281,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Sperm count (low)", "Sperm count (low)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 282,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Lumps", "Lumps" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 283,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Lyme disease", "Lyme disease" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 284,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Macular hole", "Macular hole" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 285,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Magnesium test", "Magnesium test" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 286,
                columns: new[] { "Code", "Description" },
                values: new object[] { "The 'male menopause'", "The 'male menopause'" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 287,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Male menopause", "Male menopause" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 288,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Mallet finger", "Mallet finger" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 289,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Maple syrup urine disease", "Maple syrup urine disease" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 290,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Mastoiditis", "Mastoiditis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 291,
                columns: new[] { "Code", "Description" },
                values: new object[] { "MCADD", "MCADD" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 292,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Medically unexplained symptoms", "Medically unexplained symptoms" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 293,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Functional neurological disorder", "Functional neurological disorder" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 294,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Ménière's disease", "Ménière's disease" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 295,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Mesothelioma", "Mesothelioma" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 296,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Metabolic syndrome", "Metabolic syndrome" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 297,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Metallic taste", "Metallic taste" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 298,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Mitral valve problems", "Mitral valve problems" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 299,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Heart valve problems", "Heart valve problems" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 300,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Molar pregnancy", "Molar pregnancy" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 301,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Morton's neuroma", "Morton's neuroma" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 302,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Motion sickness", "Motion sickness" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 303,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Mouth ulcers", "Mouth ulcers" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 304,
                columns: new[] { "Code", "Description" },
                values: new object[] { "MRSA", "MRSA" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 305,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Multiple system atrophy", "Multiple system atrophy" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 306,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Mycobacterium chimaera infection", "Mycobacterium chimaera infection" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 307,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Myelodysplastic syndrome (myelodysplasia)", "Myelodysplastic syndrome (myelodysplasia)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 308,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Myositis (polymyositis and dermatomyositis)", "Myositis (polymyositis and dermatomyositis)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 309,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Nail patella syndrome", "Nail patella syndrome" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 310,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Nail problems", "Nail problems" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 311,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Nasal and sinus cancer", "Nasal and sinus cancer" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 312,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Nose cancer", "Nose cancer" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 313,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Sinus cancer", "Sinus cancer" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 314,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Nasopharyngeal cancer", "Nasopharyngeal cancer" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 315,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Neck pain", "Neck pain" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 316,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Necrotising fasciitis", "Necrotising fasciitis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 317,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Neonatal herpes (herpes in a baby)", "Neonatal herpes (herpes in a baby)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 318,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Herpes in babies", "Herpes in babies" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 319,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Nephrotic syndrome in children", "Nephrotic syndrome in children" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 320,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Neuroblastoma", "Neuroblastoma" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 321,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Neuroendocrine tumours", "Neuroendocrine tumours" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 322,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Neuromyelitis optica", "Neuromyelitis optica" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 323,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Night sweats", "Night sweats" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 324,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Sweating at night", "Sweating at night" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 325,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Night terrors and nightmares", "Night terrors and nightmares" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 326,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Nipple discharge", "Nipple discharge" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 327,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Non-alcoholic fatty liver disease (NAFLD)", "Non-alcoholic fatty liver disease (NAFLD)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 328,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Norovirus (vomiting bug)", "Norovirus (vomiting bug)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 329,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Vomiting bug", "Vomiting bug" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 330,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Winter vomiting bug", "Winter vomiting bug" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 331,
                columns: new[] { "Code", "Description" },
                values: new object[] { "NSAIDs", "NSAIDs" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 332,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Swollen ankles feet and legs (oedema)", "Swollen ankles feet and legs (oedema)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 333,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Oesophageal atresia and tracheo-oesophageal fistula", "Oesophageal atresia and tracheo-oesophageal fistula" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 334,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Orf", "Orf" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 335,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Osteophyte (bone spur)", "Osteophyte (bone spur)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 336,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Otosclerosis", "Otosclerosis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 337,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Ovulation pain", "Ovulation pain" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 338,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Panic disorder", "Panic disorder" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 339,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Patau's syndrome", "Patau's syndrome" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 340,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Peak flow test", "Peak flow test" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 341,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Pelvic pain", "Pelvic pain" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 342,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Penile cancer", "Penile cancer" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 343,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Period pain", "Period pain" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 344,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Menstrual pain", "Menstrual pain" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 345,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Periods", "Periods" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 346,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Persistent trophoblastic disease and choriocarcinoma", "Persistent trophoblastic disease and choriocarcinoma" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 347,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Personality disorder", "Personality disorder" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 348,
                columns: new[] { "Code", "Description" },
                values: new object[] { "PET scan", "PET scan" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 349,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Phaeochromocytoma", "Phaeochromocytoma" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 350,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Phenylketonuria", "Phenylketonuria" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 351,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Tight foreskin (phimosis and paraphimosis)", "Tight foreskin (phimosis and paraphimosis)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 352,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Foreskin problems", "Foreskin problems" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 353,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Phimosis", "Phimosis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 354,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Phlebitis (superficial thrombophlebitis)", "Phlebitis (superficial thrombophlebitis)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 355,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Superficial thrombophlebitis", "Superficial thrombophlebitis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 356,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Phosphate test", "Phosphate test" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 357,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Photodynamic therapy (PDT)", "Photodynamic therapy (PDT)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 358,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Pins and needles", "Pins and needles" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 359,
                columns: new[] { "Code", "Description" },
                values: new object[] { "PIP breast implants", "PIP breast implants" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 360,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Pityriasis rosea", "Pityriasis rosea" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 361,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Pityriasis versicolor", "Pityriasis versicolor" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 362,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Plagiocephaly and brachycephaly (flat head syndrome)", "Plagiocephaly and brachycephaly (flat head syndrome)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 363,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Brachycephaly and plagiocephaly", "Brachycephaly and plagiocephaly" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 364,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Flat head syndrome", "Flat head syndrome" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 365,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Pleurisy", "Pleurisy" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 366,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Polio", "Polio" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 367,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Polyhydramnios (too much amniotic fluid)", "Polyhydramnios (too much amniotic fluid)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 368,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Polymorphic light eruption", "Polymorphic light eruption" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 369,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Pompholyx (dyshidrotic eczema)", "Pompholyx (dyshidrotic eczema)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 370,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Postmenopausal bleeding", "Postmenopausal bleeding" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 371,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Bleeding after the menopause", "Bleeding after the menopause" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 372,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Post-mortem", "Post-mortem" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 373,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Postpartum psychosis", "Postpartum psychosis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 374,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Postural tachycardia syndrome (PoTS)", "Postural tachycardia syndrome (PoTS)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 375,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Potassium test", "Potassium test" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 376,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Predictive genetic tests for cancer risk genes", "Predictive genetic tests for cancer risk genes" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 377,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Genetic test for cancer gene", "Genetic test for cancer gene" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 378,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Probiotics", "Probiotics" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 379,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Problems swallowing pills", "Problems swallowing pills" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 380,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Swallowing pills", "Swallowing pills" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 381,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Prostate problems", "Prostate problems" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 382,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Prostatitis", "Prostatitis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 383,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Psoriatic arthritis", "Psoriatic arthritis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 384,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Psychiatry", "Psychiatry" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 385,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Pubic lice", "Pubic lice" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 386,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Pudendal neuralgia", "Pudendal neuralgia" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 387,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Pyoderma gangrenosum", "Pyoderma gangrenosum" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 388,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Q fever", "Q fever" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 389,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Rashes in babies and children", "Rashes in babies and children" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 390,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Red blood cell count", "Red blood cell count" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 391,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Red eye", "Red eye" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 392,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Reflux in babies", "Reflux in babies" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 393,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Acid reflux in babies", "Acid reflux in babies" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 394,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Respiratory tract infections (RTIs)", "Respiratory tract infections (RTIs)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 395,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Retinal migraine", "Retinal migraine" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 396,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Retinoblastoma (eye cancer in children)", "Retinoblastoma (eye cancer in children)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 397,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Rett syndrome", "Rett syndrome" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 398,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Reye's syndrome", "Reye's syndrome" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 399,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Roseola", "Roseola" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 400,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Salivary gland stones", "Salivary gland stones" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 401,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Sarcoidosis", "Sarcoidosis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 402,
                columns: new[] { "Code", "Description" },
                values: new object[] { "SARS (severe acute respiratory syndrome)", "SARS (severe acute respiratory syndrome)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 403,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Scarlet fever", "Scarlet fever" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 404,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Schistosomiasis (bilharzia)", "Schistosomiasis (bilharzia)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 405,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Bilharzia", "Bilharzia" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 406,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Scleroderma", "Scleroderma" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 407,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Selective mutism", "Selective mutism" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 408,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Septic arthritis", "Septic arthritis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 409,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Sexually transmitted infections (STIs)", "Sexually transmitted infections (STIs)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 410,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Shin splints", "Shin splints" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 411,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Shin pain (shin splints)", "Shin pain (shin splints)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 412,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Shortness of breath", "Shortness of breath" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 413,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Shoulder impingement", "Shoulder impingement" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 414,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Sick building syndrome", "Sick building syndrome" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 415,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Silicosis", "Silicosis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 416,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Skin cyst", "Skin cyst" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 417,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Skin tags", "Skin tags" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 418,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Slapped cheek syndrome", "Slapped cheek syndrome" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 419,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Sleepwalking", "Sleepwalking" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 420,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Smelly urine", "Smelly urine" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 421,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Urine (smelly)", "Urine (smelly)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 422,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Snake bites", "Snake bites" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 423,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Social anxiety (social phobia)", "Social anxiety (social phobia)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 424,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Soft tissue sarcomas", "Soft tissue sarcomas" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 425,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Sore or dry lips", "Sore or dry lips" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 426,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Sore lips", "Sore lips" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 427,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Dry lips", "Dry lips" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 428,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Lips (sore or dry)", "Lips (sore or dry)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 429,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Sore or white tongue", "Sore or white tongue" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 430,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Tongue (sore or white)", "Tongue (sore or white)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 431,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Sore throat", "Sore throat" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 432,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Throat (sore)", "Throat (sore)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 433,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Spirometry", "Spirometry" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 434,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Spleen problems and spleen removal", "Spleen problems and spleen removal" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 435,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Spondylolisthesis", "Spondylolisthesis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 436,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Staph infection", "Staph infection" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 437,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Steroid inhalers", "Steroid inhalers" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 438,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Steroid injections", "Steroid injections" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 439,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Steroid nasal sprays", "Steroid nasal sprays" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 440,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Steroids", "Steroids" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 441,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Corticosteroids", "Corticosteroids" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 442,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Steroid tablets", "Steroid tablets" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 443,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Stevens-Johnson syndrome", "Stevens-Johnson syndrome" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 444,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Stomach ache", "Stomach ache" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 445,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Tummy ache", "Tummy ache" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 446,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Stopped or missed periods", "Stopped or missed periods" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 447,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Periods (stopped or missed)", "Periods (stopped or missed)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 448,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Stop smoking treatments", "Stop smoking treatments" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 449,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Smoking (treatments to stop)", "Smoking (treatments to stop)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 450,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Stretch marks", "Stretch marks" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 451,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Stye", "Stye" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 452,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Sudden infant death syndrome (SIDS)", "Sudden infant death syndrome (SIDS)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 453,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Sunburn", "Sunburn" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 454,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Swine flu (H1N1)", "Swine flu (H1N1)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 455,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Swollen glands", "Swollen glands" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 456,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Temporomandibular disorder (TMD)", "Temporomandibular disorder (TMD)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 457,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Jaw pain", "Jaw pain" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 458,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Tension-type headaches", "Tension-type headaches" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 459,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Headaches (tension-type)", "Headaches (tension-type)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 460,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Tetanus", "Tetanus" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 461,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Excessive thirst", "Excessive thirst" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 462,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Thirst (excessive)", "Thirst (excessive)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 463,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Thrombophilia", "Thrombophilia" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 464,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Thyroiditis", "Thyroiditis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 465,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Total iron-binding capacity (TIBC) and transferrin test", "Total iron-binding capacity (TIBC) and transferrin test" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 466,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Tongue-tie", "Tongue-tie" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 467,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Toothache", "Toothache" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 468,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Dental pain", "Dental pain" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 469,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Tooth decay", "Tooth decay" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 470,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Topical corticosteroids", "Topical corticosteroids" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 471,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Steroid cream", "Steroid cream" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 472,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Corticosteroid cream", "Corticosteroid cream" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 473,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Total protein test", "Total protein test" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 474,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Toxic shock syndrome", "Toxic shock syndrome" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 475,
                columns: new[] { "Code", "Description" },
                values: new object[] { "TENS (transcutaneous electrical nerve stimulation)", "TENS (transcutaneous electrical nerve stimulation)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 476,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Trimethylaminuria ('fish odour syndrome')", "Trimethylaminuria ('fish odour syndrome')" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 477,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Typhus", "Typhus" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 478,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Ultrasound scan", "Ultrasound scan" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 479,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Unintentional weight loss", "Unintentional weight loss" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 480,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Weight loss (unintentional)", "Weight loss (unintentional)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 481,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Weight loss (unexpected)", "Weight loss (unexpected)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 482,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Urinary tract infections (UTIs)", "Urinary tract infections (UTIs)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 483,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Vaginal discharge", "Vaginal discharge" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 484,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Vaginal dryness", "Vaginal dryness" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 485,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Vaginitis", "Vaginitis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 486,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Vasculitis", "Vasculitis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 487,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Blindness and vision loss", "Blindness and vision loss" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 488,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Vomiting blood (haematemesis)", "Vomiting blood (haematemesis)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 489,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Von Willebrand disease", "Von Willebrand disease" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 490,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Vulvodynia (vulval pain)", "Vulvodynia (vulval pain)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 491,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Vaginal pain", "Vaginal pain" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 492,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Warts and verrucas", "Warts and verrucas" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 493,
                columns: new[] { "Code", "Description" },
                values: new object[] { "West Nile virus", "West Nile virus" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 494,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Whooping cough", "Whooping cough" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 495,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Wolff-Parkinson-White syndrome", "Wolff-Parkinson-White syndrome" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 496,
                columns: new[] { "Code", "Description" },
                values: new object[] { "X-ray", "X-ray" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 497,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Zika virus", "Zika virus" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 498,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Abdominal aortic aneurysm", "Abdominal aortic aneurysm" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 499,
                columns: new[] { "Code", "Description" },
                values: new object[] { "AAA", "AAA" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 500,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Aneurysm (abdominal aortic)", "Aneurysm (abdominal aortic)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 501,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Abdominal aortic aneurysm screening", "Abdominal aortic aneurysm screening" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 502,
                columns: new[] { "Code", "Description" },
                values: new object[] { "AAA screening", "AAA screening" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 503,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Abscess", "Abscess" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 504,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Acne", "Acne" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 505,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Actinomycosis", "Actinomycosis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 506,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Acute lymphoblastic leukaemia", "Acute lymphoblastic leukaemia" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 507,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Leukaemia (acute lymphoblastic)", "Leukaemia (acute lymphoblastic)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 508,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Acute myeloid leukaemia", "Acute myeloid leukaemia" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 509,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Leukaemia (acute myeloid)", "Leukaemia (acute myeloid)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 510,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Acute pancreatitis", "Acute pancreatitis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 511,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Pancreatitis (acute)", "Pancreatitis (acute)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 512,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Addison's disease", "Addison's disease" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 513,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Agoraphobia", "Agoraphobia" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 514,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Albinism", "Albinism" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 515,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Alcohol misuse", "Alcohol misuse" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 516,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Alcohol-related liver disease", "Alcohol-related liver disease" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 517,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Liver disease (alcohol-related)", "Liver disease (alcohol-related)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 518,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Allergic rhinitis", "Allergic rhinitis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 519,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Rhinitis (allergic)", "Rhinitis (allergic)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 520,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Allergies", "Allergies" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 521,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Altitude sickness", "Altitude sickness" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 522,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Alzheimer's disease", "Alzheimer's disease" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 523,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Amniocentesis", "Amniocentesis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 524,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Anal fissure", "Anal fissure" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 525,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Anal fistula", "Anal fistula" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 526,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Anaphylaxis", "Anaphylaxis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 527,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Androgen insensitivity syndrome", "Androgen insensitivity syndrome" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 528,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Angina", "Angina" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 529,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Angioedema", "Angioedema" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 530,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Angiography", "Angiography" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 531,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Ankylosing spondylitis", "Ankylosing spondylitis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 532,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Anorexia nervosa", "Anorexia nervosa" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 533,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Antibiotics", "Antibiotics" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 534,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Anticoagulant medicines", "Anticoagulant medicines" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 535,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Antidepressants", "Antidepressants" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 536,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Antiphospholipid syndrome (APS)", "Antiphospholipid syndrome (APS)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 537,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hughes syndrome", "Hughes syndrome" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 538,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Aortic valve replacement", "Aortic valve replacement" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 539,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Heart valve replacement", "Heart valve replacement" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 540,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Aphasia", "Aphasia" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 541,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Appendicitis", "Appendicitis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 542,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Arthritis", "Arthritis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 543,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Arthroscopy", "Arthroscopy" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 544,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Aspergillosis", "Aspergillosis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 545,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Asthma", "Asthma" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 546,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Astigmatism", "Astigmatism" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 547,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Ataxia", "Ataxia" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 548,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Atopic eczema", "Atopic eczema" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 549,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Eczema (atopic)", "Eczema (atopic)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 550,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Atrial fibrillation", "Atrial fibrillation" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 551,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Attention deficit hyperactivity disorder (ADHD)", "Attention deficit hyperactivity disorder (ADHD)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 552,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Autosomal dominant polycystic kidney disease", "Autosomal dominant polycystic kidney disease" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 553,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Polycystic kidney disease (autosomal dominant)", "Polycystic kidney disease (autosomal dominant)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 554,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Autosomal recessive polycystic kidney disease", "Autosomal recessive polycystic kidney disease" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 555,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Polycystic kidney disease (autosomal recessive)", "Polycystic kidney disease (autosomal recessive)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 556,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Back pain", "Back pain" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 557,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Bacterial vaginosis", "Bacterial vaginosis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 558,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Bad breath", "Bad breath" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 559,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Halitosis", "Halitosis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 560,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Baker's cyst", "Baker's cyst" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 561,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Popliteal cyst", "Popliteal cyst" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 562,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Bartholin's cyst", "Bartholin's cyst" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 563,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Bedwetting in children", "Bedwetting in children" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 564,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Behçet's disease", "Behçet's disease" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 565,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Bell's palsy", "Bell's palsy" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 566,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Benign brain tumour (non-cancerous)", "Benign brain tumour (non-cancerous)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 567,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Brain tumour (benign)", "Brain tumour (benign)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 568,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Bile duct cancer (cholangiocarcinoma)", "Bile duct cancer (cholangiocarcinoma)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 569,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Cholangiocarcinoma", "Cholangiocarcinoma" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 570,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Binge eating disorder", "Binge eating disorder" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 571,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Biopsy", "Biopsy" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 572,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Bipolar disorder", "Bipolar disorder" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 573,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Birthmarks", "Birthmarks" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 574,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Bladder cancer", "Bladder cancer" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 575,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Bladder stones", "Bladder stones" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 576,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Bleeding from the bottom (rectal bleeding)", "Bleeding from the bottom (rectal bleeding)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 577,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Rectal bleeding", "Rectal bleeding" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 578,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Blepharitis", "Blepharitis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 579,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Blisters", "Blisters" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 580,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Blood tests", "Blood tests" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 581,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Blushing", "Blushing" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 582,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Bone cancer", "Bone cancer" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 583,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Bone cyst", "Bone cyst" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 584,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Borderline personality disorder", "Borderline personality disorder" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 585,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Bowel cancer", "Bowel cancer" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 586,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Colon cancer", "Colon cancer" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 587,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Rectal cancer", "Rectal cancer" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 588,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Bowel cancer screening", "Bowel cancer screening" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 589,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Bowel incontinence", "Bowel incontinence" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 590,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Brain abscess", "Brain abscess" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 591,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Brain aneurysm", "Brain aneurysm" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 592,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Aneurysm (brain)", "Aneurysm (brain)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 593,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Brain death", "Brain death" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 594,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Breast abscess", "Breast abscess" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 595,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Breast cancer in women", "Breast cancer in women" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 596,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Breast cancer in men", "Breast cancer in men" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 597,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Breast lumps", "Breast lumps" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 598,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Bronchiectasis", "Bronchiectasis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 599,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Bronchiolitis", "Bronchiolitis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 600,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Bronchodilators", "Bronchodilators" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 601,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Exophthalmos (bulging eyes)", "Exophthalmos (bulging eyes)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 602,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Bulimia", "Bulimia" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 603,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Burns and scalds", "Burns and scalds" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 604,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Bursitis", "Bursitis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 605,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Caesarean section", "Caesarean section" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 606,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Cancer", "Cancer" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 607,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Carotid endarterectomy", "Carotid endarterectomy" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 608,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Carpal tunnel syndrome", "Carpal tunnel syndrome" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 609,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Cartilage damage", "Cartilage damage" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 610,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Cataract surgery", "Cataract surgery" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 611,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Cavernous sinus thrombosis", "Cavernous sinus thrombosis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 612,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Cellulitis", "Cellulitis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 613,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Cerebral palsy", "Cerebral palsy" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 614,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Cervical cancer", "Cervical cancer" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 615,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Cervical screening", "Cervical screening" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 616,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Smear test", "Smear test" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 617,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Cervical spondylosis", "Cervical spondylosis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 618,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Charcot-Marie-Tooth disease", "Charcot-Marie-Tooth disease" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 619,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Chemotherapy", "Chemotherapy" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 620,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Chickenpox", "Chickenpox" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 621,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Childhood cataracts", "Childhood cataracts" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 622,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Cataracts (children)", "Cataracts (children)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 623,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Chlamydia", "Chlamydia" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 624,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Cholera", "Cholera" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 625,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Chorionic villus sampling", "Chorionic villus sampling" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 626,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Myalgic encephalomyelitis or chronic fatigue syndrome (ME/CFS)", "Myalgic encephalomyelitis or chronic fatigue syndrome (ME/CFS)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 627,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Chronic fatigue syndrome (ME/CFS)", "Chronic fatigue syndrome (ME/CFS)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 628,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Chronic lymphocytic leukaemia", "Chronic lymphocytic leukaemia" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 629,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Leukaemia (chronic lymphocytic)", "Leukaemia (chronic lymphocytic)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 630,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Chronic myeloid leukaemia", "Chronic myeloid leukaemia" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 631,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Leukaemia (chronic myeloid)", "Leukaemia (chronic myeloid)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 632,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Chronic obstructive pulmonary disease (COPD)", "Chronic obstructive pulmonary disease (COPD)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 633,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Chronic pancreatitis", "Chronic pancreatitis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 634,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Pancreatitis (chronic)", "Pancreatitis (chronic)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 635,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Cirrhosis", "Cirrhosis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 636,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Cleft lip and palate", "Cleft lip and palate" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 637,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Clinical depression", "Clinical depression" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 638,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Depression", "Depression" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 639,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Clinical trials", "Clinical trials" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 640,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Club foot", "Club foot" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 641,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Coeliac disease", "Coeliac disease" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 642,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Cognitive behavioural therapy (CBT)", "Cognitive behavioural therapy (CBT)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 643,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Colic", "Colic" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 644,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Colostomy", "Colostomy" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 645,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Colposcopy", "Colposcopy" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 646,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Common cold", "Common cold" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 647,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Complex regional pain syndrome", "Complex regional pain syndrome" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 648,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Congenital heart disease", "Congenital heart disease" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 649,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Conjunctivitis", "Conjunctivitis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 650,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Consent to treatment", "Consent to treatment" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 651,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Constipation", "Constipation" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 652,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Contact dermatitis", "Contact dermatitis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 653,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Eczema (contact dermatitis)", "Eczema (contact dermatitis)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 654,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Cornea transplant", "Cornea transplant" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 655,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Corns and calluses", "Corns and calluses" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 656,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Cardiac catheterisation and coronary angiography", "Cardiac catheterisation and coronary angiography" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 657,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Coronary angioplasty and stent insertion", "Coronary angioplasty and stent insertion" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 658,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Angioplasty", "Angioplasty" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 659,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Stent insertion", "Stent insertion" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 660,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Coronary artery bypass graft", "Coronary artery bypass graft" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 661,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Heart bypass", "Heart bypass" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 662,
                columns: new[] { "Code", "Description" },
                values: new object[] { "CABG", "CABG" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 663,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Coronary heart disease", "Coronary heart disease" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 664,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Heart disease (coronary)", "Heart disease (coronary)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 665,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Corticobasal degeneration", "Corticobasal degeneration" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 666,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Counselling", "Counselling" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 667,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Craniosynostosis", "Craniosynostosis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 668,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Creutzfeldt-Jakob disease", "Creutzfeldt-Jakob disease" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 669,
                columns: new[] { "Code", "Description" },
                values: new object[] { "CJD", "CJD" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 670,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Crohn's disease", "Crohn's disease" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 671,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Croup", "Croup" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 672,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Cushing's syndrome", "Cushing's syndrome" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 673,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Cystic fibrosis", "Cystic fibrosis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 674,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Cystitis", "Cystitis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 675,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Cystoscopy", "Cystoscopy" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 676,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Cytomegalovirus (CMV)", "Cytomegalovirus (CMV)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 677,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Deafblindness", "Deafblindness" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 678,
                columns: new[] { "Code", "Description" },
                values: new object[] { "DVT (deep vein thrombosis)", "DVT (deep vein thrombosis)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 679,
                columns: new[] { "Code", "Description" },
                values: new object[] { "DVT", "DVT" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 680,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Dehydration", "Dehydration" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 681,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Dementia with Lewy bodies", "Dementia with Lewy bodies" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 682,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Dengue", "Dengue" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 683,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Developmental co-ordination disorder (dyspraxia) in children", "Developmental co-ordination disorder (dyspraxia) in children" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 684,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Dyspraxia in children", "Dyspraxia in children" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 685,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Bone density scan (DEXA scan)", "Bone density scan (DEXA scan)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 686,
                columns: new[] { "Code", "Description" },
                values: new object[] { "DEXA scan", "DEXA scan" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 687,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Diabetes insipidus", "Diabetes insipidus" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 688,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Diabetic retinopathy", "Diabetic retinopathy" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 689,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Dialysis", "Dialysis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 690,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Diphtheria", "Diphtheria" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 691,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Discoid eczema", "Discoid eczema" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 692,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Eczema (discoid)", "Eczema (discoid)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 693,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Disorders of consciousness", "Disorders of consciousness" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 694,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Vegetative state", "Vegetative state" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 695,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Double vision", "Double vision" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 696,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Down's syndrome", "Down's syndrome" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 697,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Dry eyes", "Dry eyes" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 698,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Dupuytren's contracture", "Dupuytren's contracture" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 699,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Dyslexia", "Dyslexia" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 700,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Dystonia", "Dystonia" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 701,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Ectopic pregnancy", "Ectopic pregnancy" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 702,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Encephalitis", "Encephalitis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 703,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Endocarditis", "Endocarditis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 704,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Endometriosis", "Endometriosis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 705,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Epidermolysis bullosa", "Epidermolysis bullosa" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 706,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Epidural", "Epidural" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 707,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Epilepsy", "Epilepsy" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 708,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Erectile dysfunction (impotence)", "Erectile dysfunction (impotence)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 709,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Impotence", "Impotence" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 710,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Excessive sweating (hyperhidrosis)", "Excessive sweating (hyperhidrosis)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 711,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hyperhidrosis", "Hyperhidrosis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 712,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Fabricated or induced illness", "Fabricated or induced illness" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 713,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Fainting", "Fainting" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 714,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Falls", "Falls" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 715,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Femoral hernia repair", "Femoral hernia repair" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 716,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hernia (femoral)", "Hernia (femoral)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 717,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Fibroids", "Fibroids" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 718,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Fibromyalgia", "Fibromyalgia" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 719,
                columns: new[] { "Code", "Description" },
                values: new object[] { "First aid", "First aid" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 720,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Farting (flatulence)", "Farting (flatulence)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 721,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Flatulence", "Flatulence" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 722,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Wind", "Wind" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 723,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Flu", "Flu" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 724,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Influenza", "Influenza" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 725,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Food allergy", "Food allergy" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 726,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Food poisoning", "Food poisoning" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 727,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Frontotemporal dementia", "Frontotemporal dementia" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 728,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Dementia (frontotemporal)", "Dementia (frontotemporal)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 729,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Frostbite", "Frostbite" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 730,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Frozen shoulder", "Frozen shoulder" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 731,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Fungal nail infection", "Fungal nail infection" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 732,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Nail fungal infection", "Nail fungal infection" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 733,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Gallbladder removal", "Gallbladder removal" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 734,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Gallstones", "Gallstones" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 735,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Gangrene", "Gangrene" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 736,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Gastrectomy", "Gastrectomy" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 737,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Gastroscopy", "Gastroscopy" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 738,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Gender dysphoria", "Gender dysphoria" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 739,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Generalised anxiety disorder in adults", "Generalised anxiety disorder in adults" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 740,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Anxiety disorder in adults", "Anxiety disorder in adults" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 741,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Genital herpes", "Genital herpes" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 742,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Herpes (genital)", "Herpes (genital)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 743,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Genital warts", "Genital warts" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 744,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Gestational diabetes", "Gestational diabetes" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 745,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Diabetes in pregnancy", "Diabetes in pregnancy" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 746,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Giardiasis", "Giardiasis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 747,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Glandular fever", "Glandular fever" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 748,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Glaucoma", "Glaucoma" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 749,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Glomerulonephritis", "Glomerulonephritis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 750,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Glue ear", "Glue ear" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 751,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Goitre", "Goitre" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 752,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Gonorrhoea", "Gonorrhoea" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 753,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Gout", "Gout" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 754,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Guillain-Barré syndrome", "Guillain-Barré syndrome" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 755,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Gum disease", "Gum disease" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 756,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Haemochromatosis", "Haemochromatosis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 757,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Haemophilia", "Haemophilia" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 758,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hair loss", "Hair loss" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 759,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hand tendon repair", "Hand tendon repair" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 760,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Having an operation (surgery)", "Having an operation (surgery)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 761,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Surgery (having an operation)", "Surgery (having an operation)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 762,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hay fever", "Hay fever" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 763,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Head lice and nits", "Head lice and nits" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 764,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hearing loss", "Hearing loss" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 765,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Deafness", "Deafness" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 766,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hearing tests", "Hearing tests" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 767,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Heart attack", "Heart attack" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 768,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Heart block", "Heart block" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 769,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Heartburn and acid reflux", "Heartburn and acid reflux" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 770,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Gastro-oesophageal reflux disease (GORD)", "Gastro-oesophageal reflux disease (GORD)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 771,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Heart failure", "Heart failure" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 772,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Heart transplant", "Heart transplant" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 773,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Heavy periods", "Heavy periods" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 774,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Periods (heavy)", "Periods (heavy)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 775,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hepatitis A", "Hepatitis A" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 776,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hepatitis B", "Hepatitis B" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 777,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hepatitis C", "Hepatitis C" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 778,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hiatus hernia", "Hiatus hernia" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 779,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hernia (hiatus)", "Hernia (hiatus)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 780,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hiccups", "Hiccups" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 781,
                columns: new[] { "Code", "Description" },
                values: new object[] { "High blood pressure (hypertension)", "High blood pressure (hypertension)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 782,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hypertension", "Hypertension" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 783,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Blood pressure (high)", "Blood pressure (high)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 784,
                columns: new[] { "Code", "Description" },
                values: new object[] { "High cholesterol", "High cholesterol" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 785,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Cholesterol (high)", "Cholesterol (high)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 786,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hip fracture", "Hip fracture" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 787,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hip replacement", "Hip replacement" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 788,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Excessive hair growth (hirsutism)", "Excessive hair growth (hirsutism)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 789,
                columns: new[] { "Code", "Description" },
                values: new object[] { "hirsutism", "hirsutism" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 790,
                columns: new[] { "Code", "Description" },
                values: new object[] { "HIV and AIDS", "HIV and AIDS" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 791,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hives", "Hives" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 792,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hodgkin lymphoma", "Hodgkin lymphoma" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 793,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hormone replacement therapy (HRT)", "Hormone replacement therapy (HRT)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 794,
                columns: new[] { "Code", "Description" },
                values: new object[] { "HRT", "HRT" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 795,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Huntington's disease", "Huntington's disease" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 796,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hydrocephalus", "Hydrocephalus" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 797,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hydronephrosis", "Hydronephrosis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 798,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hysterectomy", "Hysterectomy" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 799,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hysteroscopy", "Hysteroscopy" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 800,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Idiopathic pulmonary fibrosis", "Idiopathic pulmonary fibrosis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 801,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Pulmonary fibrosis", "Pulmonary fibrosis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 802,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Ileostomy", "Ileostomy" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 803,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Impetigo", "Impetigo" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 804,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Infertility", "Infertility" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 805,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Inguinal hernia repair", "Inguinal hernia repair" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 806,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hernia (inguinal)", "Hernia (inguinal)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 807,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Insect bites and stings", "Insect bites and stings" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 808,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Sting or bite (insect)", "Sting or bite (insect)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 809,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Insomnia", "Insomnia" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 810,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Iron deficiency anaemia", "Iron deficiency anaemia" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 811,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Anaemia (iron deficiency)", "Anaemia (iron deficiency)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 812,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Irregular periods", "Irregular periods" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 813,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Periods (irregular)", "Periods (irregular)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 814,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Irritable bowel syndrome (IBS)", "Irritable bowel syndrome (IBS)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 815,
                columns: new[] { "Code", "Description" },
                values: new object[] { "IBS", "IBS" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 816,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Itchy bottom", "Itchy bottom" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 817,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Anus (itchy)", "Anus (itchy)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 818,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Itchy skin", "Itchy skin" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 819,
                columns: new[] { "Code", "Description" },
                values: new object[] { "IVF", "IVF" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 820,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Japanese encephalitis", "Japanese encephalitis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 821,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Jaundice", "Jaundice" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 822,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Newborn jaundice", "Newborn jaundice" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 823,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Jaundice in newborns", "Jaundice in newborns" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 824,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Jellyfish and other sea creature stings", "Jellyfish and other sea creature stings" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 825,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Jet lag", "Jet lag" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 826,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Joint hypermobility syndrome", "Joint hypermobility syndrome" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 827,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Kawasaki disease", "Kawasaki disease" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 828,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Kidney cancer", "Kidney cancer" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 829,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Chronic kidney disease", "Chronic kidney disease" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 830,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Kidney failure", "Kidney failure" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 831,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Kidney infection", "Kidney infection" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 832,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Kidney stones", "Kidney stones" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 833,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Kidney transplant", "Kidney transplant" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 834,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Knee ligament surgery", "Knee ligament surgery" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 835,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Knee replacement", "Knee replacement" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 836,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Kyphosis", "Kyphosis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 837,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Labyrinthitis and vestibular neuritis", "Labyrinthitis and vestibular neuritis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 838,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Vestibular neuritis", "Vestibular neuritis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 839,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Lactose intolerance", "Lactose intolerance" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 840,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Laparoscopy (keyhole surgery)", "Laparoscopy (keyhole surgery)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 841,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Laryngeal (larynx) cancer", "Laryngeal (larynx) cancer" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 842,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Laryngitis", "Laryngitis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 843,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Laxatives", "Laxatives" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 844,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Lazy eye", "Lazy eye" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 845,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Amblyopia", "Amblyopia" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 846,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Leg cramps", "Leg cramps" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 847,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Venous leg ulcer", "Venous leg ulcer" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 848,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Leg ulcer", "Leg ulcer" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 849,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Leptospirosis (Weil's disease)", "Leptospirosis (Weil's disease)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 850,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Weil's disease", "Weil's disease" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 851,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Leukoplakia", "Leukoplakia" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 852,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Lichen planus", "Lichen planus" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 853,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Listeriosis", "Listeriosis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 854,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Liver cancer", "Liver cancer" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 855,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Liver transplant", "Liver transplant" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 856,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Long-sightedness", "Long-sightedness" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 857,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Low blood pressure (hypotension)", "Low blood pressure (hypotension)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 858,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Blood pressure (low)", "Blood pressure (low)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 859,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hypotension", "Hypotension" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 860,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Lumbar decompression surgery", "Lumbar decompression surgery" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 861,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Lumbar puncture", "Lumbar puncture" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 862,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Lung cancer", "Lung cancer" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 863,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Lung transplant", "Lung transplant" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 864,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Lupus", "Lupus" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 865,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Lymphoedema", "Lymphoedema" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 866,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Age-related macular degeneration (AMD)", "Age-related macular degeneration (AMD)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 867,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Macular degeneration (age-related)", "Macular degeneration (age-related)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 868,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Malaria", "Malaria" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 869,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Malignant brain tumour (brain cancer)", "Malignant brain tumour (brain cancer)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 870,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Brain tumour (malignant)", "Brain tumour (malignant)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 871,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Malnutrition", "Malnutrition" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 872,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Marfan syndrome", "Marfan syndrome" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 873,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Mastectomy", "Mastectomy" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 874,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Mastitis", "Mastitis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 875,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Mastocytosis", "Mastocytosis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 876,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Measles", "Measles" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 877,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Medicines information", "Medicines information" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 878,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Skin cancer (melanoma)", "Skin cancer (melanoma)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 879,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Meningitis", "Meningitis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 880,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Menopause", "Menopause" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 881,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Migraine", "Migraine" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 882,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Head injury and concussion", "Head injury and concussion" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 883,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Miscarriage", "Miscarriage" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 884,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Moles", "Moles" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 885,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Molluscum contagiosum", "Molluscum contagiosum" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 886,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Motor neurone disease", "Motor neurone disease" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 887,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Mouth cancer", "Mouth cancer" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 888,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Tongue cancer", "Tongue cancer" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 889,
                columns: new[] { "Code", "Description" },
                values: new object[] { "MRI scan", "MRI scan" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 890,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Mucositis", "Mucositis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 891,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Multiple myeloma", "Multiple myeloma" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 892,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Myeloma", "Myeloma" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 893,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Multiple sclerosis", "Multiple sclerosis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 894,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Mumps", "Mumps" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 895,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Munchausen's syndrome", "Munchausen's syndrome" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 896,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Muscular dystrophy", "Muscular dystrophy" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 897,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Myasthenia gravis", "Myasthenia gravis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 898,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Narcolepsy", "Narcolepsy" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 899,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Nasal polyps", "Nasal polyps" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 900,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Newborn respiratory distress syndrome", "Newborn respiratory distress syndrome" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 901,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Neurofibromatosis type 1", "Neurofibromatosis type 1" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 902,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Neurofibromatosis type 2", "Neurofibromatosis type 2" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 903,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Non-allergic rhinitis", "Non-allergic rhinitis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 904,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Non-gonococcal urethritis", "Non-gonococcal urethritis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 905,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Urethritis (NGU)", "Urethritis (NGU)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 906,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Non-Hodgkin lymphoma", "Non-Hodgkin lymphoma" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 907,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Skin cancer (non-melanoma)", "Skin cancer (non-melanoma)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 908,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Squamous cell carcinoma", "Squamous cell carcinoma" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 909,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Basal cell carcinoma", "Basal cell carcinoma" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 910,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Noonan syndrome", "Noonan syndrome" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 911,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Nosebleed", "Nosebleed" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 912,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Obesity", "Obesity" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 913,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Obsessive compulsive disorder (OCD)", "Obsessive compulsive disorder (OCD)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 914,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Occupational therapy", "Occupational therapy" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 915,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Oesophageal cancer", "Oesophageal cancer" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 916,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Orthodontics", "Orthodontics" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 917,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Osteoarthritis", "Osteoarthritis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 918,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Osteomyelitis", "Osteomyelitis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 919,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Osteopathy", "Osteopathy" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 920,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Osteoporosis", "Osteoporosis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 921,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Ovarian cancer", "Ovarian cancer" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 922,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Ovarian cyst", "Ovarian cyst" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 923,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Overactive thyroid (hyperthyroidism)", "Overactive thyroid (hyperthyroidism)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 924,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hyperthyroidism", "Hyperthyroidism" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 925,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Pacemaker implantation", "Pacemaker implantation" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 926,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Paget's disease of bone", "Paget's disease of bone" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 927,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Paget's disease of the nipple", "Paget's disease of the nipple" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 928,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Pancreas transplant", "Pancreas transplant" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 929,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Pancreatic cancer", "Pancreatic cancer" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 930,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Paralysis", "Paralysis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 931,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Parkinson's disease", "Parkinson's disease" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 932,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Pelvic inflammatory disease", "Pelvic inflammatory disease" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 933,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Pelvic organ prolapse", "Pelvic organ prolapse" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 934,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Prolapse (pelvic organ)", "Prolapse (pelvic organ)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 935,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Pemphigus vulgaris", "Pemphigus vulgaris" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 936,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Perforated eardrum", "Perforated eardrum" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 937,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Eardrum (burst)", "Eardrum (burst)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 938,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Pericarditis", "Pericarditis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 939,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Peripheral arterial disease (PAD)", "Peripheral arterial disease (PAD)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 940,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Peripheral neuropathy", "Peripheral neuropathy" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 941,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Peritonitis", "Peritonitis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 942,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Phobias", "Phobias" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 943,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Physiotherapy", "Physiotherapy" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 944,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Piles (haemorrhoids)", "Piles (haemorrhoids)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 945,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Piles", "Piles" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 946,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Pilonidal sinus", "Pilonidal sinus" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 947,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Plastic surgery", "Plastic surgery" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 948,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Pneumonia", "Pneumonia" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 949,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Poisoning", "Poisoning" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 950,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Polycystic ovary syndrome", "Polycystic ovary syndrome" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 951,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Polycythaemia", "Polycythaemia" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 952,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Polymyalgia rheumatica", "Polymyalgia rheumatica" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 953,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Post-herpetic neuralgia", "Post-herpetic neuralgia" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 954,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Postnatal depression", "Postnatal depression" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 955,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Post-polio syndrome", "Post-polio syndrome" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 956,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Post-traumatic stress disorder (PTSD)", "Post-traumatic stress disorder (PTSD)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 957,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Prader-Willi syndrome", "Prader-Willi syndrome" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 958,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Pre-eclampsia", "Pre-eclampsia" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 959,
                columns: new[] { "Code", "Description" },
                values: new object[] { "PMS (premenstrual syndrome)", "PMS (premenstrual syndrome)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 960,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Pressure ulcers (pressure sores)", "Pressure ulcers (pressure sores)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 961,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Priapism (painful erections)", "Priapism (painful erections)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 962,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Primary biliary cholangitis (primary biliary cirrhosis)", "Primary biliary cholangitis (primary biliary cirrhosis)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 963,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Progressive supranuclear palsy", "Progressive supranuclear palsy" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 964,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Prostate cancer", "Prostate cancer" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 965,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Benign prostate enlargement", "Benign prostate enlargement" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 966,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Prostate enlargement", "Prostate enlargement" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 968,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Psychosis", "Psychosis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 969,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Pulmonary embolism", "Pulmonary embolism" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 970,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Pulmonary hypertension", "Pulmonary hypertension" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 971,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Rabies", "Rabies" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 972,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Radiotherapy", "Radiotherapy" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 973,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Raynaud's", "Raynaud's" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 974,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Reactive arthritis", "Reactive arthritis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 975,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Rectal examination", "Rectal examination" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 976,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Repetitive strain injury (RSI)", "Repetitive strain injury (RSI)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 977,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Restless legs syndrome", "Restless legs syndrome" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 978,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Restricted growth (dwarfism)", "Restricted growth (dwarfism)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 979,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Dwarfism", "Dwarfism" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 980,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Detached retina (retinal detachment)", "Detached retina (retinal detachment)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 981,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Retinal detachment", "Retinal detachment" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 982,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Rhesus disease", "Rhesus disease" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 983,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Rheumatic fever", "Rheumatic fever" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 984,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Rheumatoid arthritis", "Rheumatoid arthritis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 985,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Rickets and osteomalacia", "Rickets and osteomalacia" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 986,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Osteomalacia", "Osteomalacia" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 987,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Root canal treatment", "Root canal treatment" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 988,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Rosacea", "Rosacea" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 989,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Rubella (german measles)", "Rubella (german measles)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 990,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Scabies", "Scabies" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 991,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Scars", "Scars" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 992,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Schizophrenia", "Schizophrenia" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 993,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Sciatica", "Sciatica" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 994,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Buttock pain", "Buttock pain" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 995,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Scoliosis", "Scoliosis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 996,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Scurvy", "Scurvy" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 997,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Seasonal affective disorder (SAD)", "Seasonal affective disorder (SAD)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 998,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Self-harm", "Self-harm" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 999,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Sepsis", "Sepsis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1000,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Severe head injury", "Severe head injury" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1001,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Shingles", "Shingles" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1002,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Short-sightedness (myopia)", "Short-sightedness (myopia)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1003,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Myopia", "Myopia" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1004,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Shoulder pain", "Shoulder pain" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1005,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Sickle cell disease", "Sickle cell disease" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1006,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Sjögren's syndrome", "Sjögren's syndrome" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1007,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Sleep paralysis", "Sleep paralysis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1008,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Slipped disc", "Slipped disc" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1009,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Small bowel transplant", "Small bowel transplant" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1010,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Bowel transplant", "Bowel transplant" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1011,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Snoring", "Snoring" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1012,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Spina bifida", "Spina bifida" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1013,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Spinal muscular atrophy", "Spinal muscular atrophy" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1014,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Sports injuries", "Sports injuries" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1015,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Sprains and strains", "Sprains and strains" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1016,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Squint", "Squint" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1017,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Selective serotonin reuptake inhibitors (SSRIs)", "Selective serotonin reuptake inhibitors (SSRIs)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1018,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Stammering", "Stammering" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1019,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Stuttering", "Stuttering" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1020,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Statins", "Statins" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1021,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Stem cell and bone marrow transplants", "Stem cell and bone marrow transplants" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1022,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Stillbirth", "Stillbirth" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1023,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Stomach cancer", "Stomach cancer" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1024,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Stomach ulcer", "Stomach ulcer" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1025,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Stroke", "Stroke" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1026,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Subarachnoid haemorrhage", "Subarachnoid haemorrhage" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1027,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Brain haemorrhage", "Brain haemorrhage" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1028,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Subdural haematoma", "Subdural haematoma" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1029,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Help for suicidal thoughts", "Help for suicidal thoughts" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1030,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Suicidal thoughts", "Suicidal thoughts" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1031,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Supraventricular tachycardia (SVT)", "Supraventricular tachycardia (SVT)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1032,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Dysphagia (swallowing problems)", "Dysphagia (swallowing problems)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1033,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Swallowing problems", "Swallowing problems" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1034,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Syphilis", "Syphilis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1035,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Coccydynia (tailbone pain)", "Coccydynia (tailbone pain)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1036,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Tay-Sachs disease", "Tay-Sachs disease" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1037,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Teeth grinding (bruxism)", "Teeth grinding (bruxism)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1038,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Bruxism", "Bruxism" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1039,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Tendonitis", "Tendonitis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1040,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Tennis elbow", "Tennis elbow" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1041,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Testicle lumps and swellings", "Testicle lumps and swellings" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1042,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Testicular cancer", "Testicular cancer" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1043,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Thalassaemia", "Thalassaemia" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1044,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Threadworms", "Threadworms" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1045,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Thyroid cancer", "Thyroid cancer" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1046,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Tick-borne encephalitis (TBE)", "Tick-borne encephalitis (TBE)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1047,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Tics", "Tics" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1048,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Tinnitus", "Tinnitus" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1049,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Tonsillitis", "Tonsillitis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1050,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Quinsy", "Quinsy" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1051,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Tourette's syndrome", "Tourette's syndrome" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1052,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Toxocariasis", "Toxocariasis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1053,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Toxoplasmosis", "Toxoplasmosis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1054,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Tracheostomy", "Tracheostomy" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1055,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Transient ischaemic attack (TIA)", "Transient ischaemic attack (TIA)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1056,
                columns: new[] { "Code", "Description" },
                values: new object[] { "TIA", "TIA" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1057,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Transurethral resection of the prostate (TURP)", "Transurethral resection of the prostate (TURP)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1058,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Travel vaccinations", "Travel vaccinations" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1059,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Trichomoniasis", "Trichomoniasis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1060,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Trichotillomania (hair pulling disorder)", "Trichotillomania (hair pulling disorder)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1061,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Trigeminal neuralgia", "Trigeminal neuralgia" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1062,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Trigger finger", "Trigger finger" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1063,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Tuberculosis (TB)", "Tuberculosis (TB)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1064,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Tuberous sclerosis", "Tuberous sclerosis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1065,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Turner syndrome", "Turner syndrome" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1066,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Type 2 diabetes", "Type 2 diabetes" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1067,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Diabetes (type 2)", "Diabetes (type 2)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1068,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Typhoid fever", "Typhoid fever" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1069,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Ulcerative colitis", "Ulcerative colitis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1070,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Umbilical hernia repair", "Umbilical hernia repair" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1071,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hernia (umbilical)", "Hernia (umbilical)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1072,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Underactive thyroid (hypothyroidism)", "Underactive thyroid (hypothyroidism)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1073,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hypothyroidism", "Hypothyroidism" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1074,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Undescended testicles", "Undescended testicles" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1075,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Urinary catheter", "Urinary catheter" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1076,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Urinary incontinence", "Urinary incontinence" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1077,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Incontinence (urinary)", "Incontinence (urinary)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1078,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Uveitis", "Uveitis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1079,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Vaginal cancer", "Vaginal cancer" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1080,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Vaginismus", "Vaginismus" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1081,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Varicose eczema", "Varicose eczema" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1082,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Eczema (varicose)", "Eczema (varicose)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1083,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Varicose veins", "Varicose veins" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1084,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Vascular dementia", "Vascular dementia" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1085,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Dementia (vascular)", "Dementia (vascular)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1086,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Vertigo", "Vertigo" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1087,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Vitamin B12 or folate deficiency anaemia", "Vitamin B12 or folate deficiency anaemia" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1088,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Anaemia (vitamin B12 or folate deficiency)", "Anaemia (vitamin B12 or folate deficiency)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1089,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Vitamins and minerals", "Vitamins and minerals" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1090,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Vitiligo", "Vitiligo" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1091,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Vulval cancer", "Vulval cancer" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1092,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Watering eyes", "Watering eyes" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1093,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Weight loss surgery", "Weight loss surgery" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1094,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Whiplash", "Whiplash" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1095,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Wisdom tooth removal", "Wisdom tooth removal" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1096,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Womb (uterus) cancer", "Womb (uterus) cancer" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1097,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Uterine (womb) cancer", "Uterine (womb) cancer" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1098,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Endometrial cancer", "Endometrial cancer" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1099,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Yellow fever", "Yellow fever" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1100,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Abortion", "Abortion" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1101,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Bird flu", "Bird flu" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1102,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Avian flu", "Avian flu" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1103,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Antifungal medicines", "Antifungal medicines" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1104,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Blood transfusion", "Blood transfusion" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1105,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Ringworm", "Ringworm" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1106,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Early menopause", "Early menopause" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1107,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Menopause (early)", "Menopause (early)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1108,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Floaters and flashes in the eyes", "Floaters and flashes in the eyes" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1109,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Eye floaters", "Eye floaters" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1110,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Your contraception guide", "Your contraception guide" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1111,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Dementia guide", "Dementia guide" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1112,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Fitness Studio exercise videos", "Fitness Studio exercise videos" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1113,
                columns: new[] { "Code", "Description" },
                values: new object[] { "NHS Health Check", "NHS Health Check" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1114,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Worms in humans", "Worms in humans" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1115,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hookworm", "Hookworm" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1116,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Tapeworm", "Tapeworm" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1117,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Roundworm", "Roundworm" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1118,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Tremor or shaking hands", "Tremor or shaking hands" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1119,
                columns: new[] { "Code", "Description" },
                values: new object[] { "tremor", "tremor" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1120,
                columns: new[] { "Code", "Description" },
                values: new object[] { "essential tremor", "essential tremor" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1121,
                columns: new[] { "Code", "Description" },
                values: new object[] { "shaking", "shaking" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1122,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Low white blood cell count", "Low white blood cell count" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1123,
                columns: new[] { "Code", "Description" },
                values: new object[] { "White blood cell count (low)", "White blood cell count (low)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1124,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Bunions", "Bunions" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1125,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Lost or changed sense of smell", "Lost or changed sense of smell" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1126,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Sense of smell (lost/changed)", "Sense of smell (lost/changed)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1127,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Soiling (child pooing their pants)", "Soiling (child pooing their pants)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1128,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Oral thrush (mouth thrush)", "Oral thrush (mouth thrush)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1129,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Mouth thrush", "Mouth thrush" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1130,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Temporal arteritis", "Temporal arteritis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1131,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Giant cell arteritis", "Giant cell arteritis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1132,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Memory loss (amnesia)", "Memory loss (amnesia)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1133,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Amnesia", "Amnesia" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1134,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Headaches", "Headaches" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1135,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Sinusitis (sinus infection)", "Sinusitis (sinus infection)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1136,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Sinusitis", "Sinusitis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1137,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Thrush in men and women", "Thrush in men and women" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1138,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Cold sores", "Cold sores" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1139,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Group B strep", "Group B strep" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1140,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Skin picking disorder", "Skin picking disorder" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1141,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hyperparathyroidism", "Hyperparathyroidism" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1142,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hypoparathyroidism", "Hypoparathyroidism" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1143,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Ear infections", "Ear infections" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1144,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Twitching eyes and muscles", "Twitching eyes and muscles" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1145,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Learning disabilities", "Learning disabilities" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1146,
                columns: new[] { "Code", "Description" },
                values: new object[] { "NHS screening", "NHS screening" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1147,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hormone headaches", "Hormone headaches" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1148,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Headaches (hormone)", "Headaches (hormone)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1149,
                columns: new[] { "Code", "Description" },
                values: new object[] { "What to do if someone has a seizure (fit)", "What to do if someone has a seizure (fit)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1150,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Seizures (fits)", "Seizures (fits)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1151,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Fits (seizures)", "Fits (seizures)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1152,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Complementary and alternative medicine", "Complementary and alternative medicine" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1153,
                columns: new[] { "Code", "Description" },
                values: new object[] { "End of life care", "End of life care" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1154,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Knocked-out tooth", "Knocked-out tooth" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1155,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Tooth knocked out", "Tooth knocked out" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1156,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Chipped broken or cracked tooth", "Chipped broken or cracked tooth" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1157,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Tooth (chipped or broken)", "Tooth (chipped or broken)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1158,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Diarrhoea and vomiting", "Diarrhoea and vomiting" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1159,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Tummy bug", "Tummy bug" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1160,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Vomiting", "Vomiting" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1161,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Stomach bug", "Stomach bug" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1162,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Gastroenteritis", "Gastroenteritis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1163,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Diarrhoea", "Diarrhoea" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1164,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Being sick", "Being sick" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1165,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Bullous pemphigoid", "Bullous pemphigoid" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1166,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Feeling sick (nausea)", "Feeling sick (nausea)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1167,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Nausea", "Nausea" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1168,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Type 1 diabetes", "Type 1 diabetes" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1169,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Diabetes (type 1)", "Diabetes (type 1)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1170,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Social care and support guide", "Social care and support guide" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1171,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Middle East respiratory syndrome (MERS)", "Middle East respiratory syndrome (MERS)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1172,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Monkeypox", "Monkeypox" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1173,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Diabetic eye screening 2", "Diabetic eye screening 2" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1174,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Medical cannabis (and cannabis oils)", "Medical cannabis (and cannabis oils)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1175,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Cannabis oil (medical cannabis)", "Cannabis oil (medical cannabis)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1176,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Diabetic eye screening 1", "Diabetic eye screening 1" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1177,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Body odour (BO)", "Body odour (BO)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1178,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Human papillomavirus (HPV)", "Human papillomavirus (HPV)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1179,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Plantar fasciitis", "Plantar fasciitis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1180,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Foot pain", "Foot pain" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1181,
                columns: new[] { "Code", "Description" },
                values: new object[] { "heel pain", "heel pain" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1182,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Toe pain", "Toe pain" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1183,
                columns: new[] { "Code", "Description" },
                values: new object[] { "ankle pain", "ankle pain" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1184,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Autism", "Autism" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1185,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Asperger's", "Asperger's" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1186,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Cosmetic procedures", "Cosmetic procedures" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1187,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hand pain", "Hand pain" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1188,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Swollen arms and hands (oedema)", "Swollen arms and hands (oedema)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1189,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Colonoscopy", "Colonoscopy" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1190,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Genetic and genomic testing", "Genetic and genomic testing" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1191,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Sleep apnoea", "Sleep apnoea" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1192,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Vaccinations", "Vaccinations" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1193,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Mental health and wellbeing", "Mental health and wellbeing" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1194,
                columns: new[] { "Code", "Description" },
                values: new object[] { "High temperature (fever) in adults", "High temperature (fever) in adults" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1195,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Fever in adults", "Fever in adults" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1196,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Coronavirus (COVID-19)", "Coronavirus (COVID-19)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1197,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Baby", "Baby" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1198,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Do not attempt cardiopulmonary resuscitation (DNACPR) decisions", "Do not attempt cardiopulmonary resuscitation (DNACPR) decisions" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1199,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Testicle pain", "Testicle pain" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1200,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Pain in testicles", "Pain in testicles" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1201,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hearing aids and implants", "Hearing aids and implants" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1202,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Breast screening (mammogram)", "Breast screening (mammogram)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1203,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Smelly feet", "Smelly feet" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1204,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Academic attainment", "Academic attainment" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1205,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Ageing", "Ageing" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1206,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Aggression", "Aggression" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1207,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Antenatal care", "Antenatal care" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1208,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Blood donation", "Blood donation" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1209,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Body image", "Body image" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1210,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Breastfeeding", "Breastfeeding" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1211,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Care homes", "Care homes" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1212,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Carers", "Carers" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1213,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Child development", "Child development" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1214,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Complementary therapies", "Complementary therapies" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1215,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Contraception", "Contraception" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1216,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Domestic violence", "Domestic violence" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1217,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Eating well", "Eating well" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1218,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Exercise and sports", "Exercise and sports" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1219,
                columns: new[] { "Code", "Description" },
                values: new object[] { "General wellbeing", "General wellbeing" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1220,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Genetic screening", "Genetic screening" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1221,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Healthy volunteers", "Healthy volunteers" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1222,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Improving care and services", "Improving care and services" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1223,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Healthy lifestyle", "Healthy lifestyle" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1224,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Long COVID", "Long COVID" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1225,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Obesity risk", "Obesity risk" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1226,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Occupational health", "Occupational health" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1227,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Parenting", "Parenting" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1228,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Public health", "Public health" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1229,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Sleeping well", "Sleeping well" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1230,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Smoking", "Smoking" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1231,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Supplements", "Supplements" });
        }
    }
}
