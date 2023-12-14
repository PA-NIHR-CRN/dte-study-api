using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dynamo.Stream.Handler.Migrations
{
    public partial class removehealthconditionsduplicates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1232);

            migrationBuilder.DeleteData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1233);

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
                keyValue: 967,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Psoriasis", "Psoriasis" });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 712,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Sweating (excessive)", "Sweating (excessive)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 713,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Fabricated or induced illness", "Fabricated or induced illness" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 714,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Fainting", "Fainting" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 715,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Falls", "Falls" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 716,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Femoral hernia repair", "Femoral hernia repair" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 717,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hernia (femoral)", "Hernia (femoral)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 718,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Fibroids", "Fibroids" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 719,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Fibromyalgia", "Fibromyalgia" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 720,
                columns: new[] { "Code", "Description" },
                values: new object[] { "First aid", "First aid" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 721,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Farting (flatulence)", "Farting (flatulence)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 722,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Flatulence", "Flatulence" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 723,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Wind", "Wind" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 724,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Flu", "Flu" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 725,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Influenza", "Influenza" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 726,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Food allergy", "Food allergy" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 727,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Food poisoning", "Food poisoning" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 728,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Frontotemporal dementia", "Frontotemporal dementia" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 729,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Dementia (frontotemporal)", "Dementia (frontotemporal)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 730,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Frostbite", "Frostbite" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 731,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Frozen shoulder", "Frozen shoulder" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 732,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Fungal nail infection", "Fungal nail infection" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 733,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Nail fungal infection", "Nail fungal infection" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 734,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Gallbladder removal", "Gallbladder removal" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 735,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Gallstones", "Gallstones" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 736,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Gangrene", "Gangrene" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 737,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Gastrectomy", "Gastrectomy" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 738,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Gastroscopy", "Gastroscopy" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 739,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Gender dysphoria", "Gender dysphoria" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 740,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Generalised anxiety disorder in adults", "Generalised anxiety disorder in adults" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 741,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Anxiety disorder in adults", "Anxiety disorder in adults" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 742,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Genital herpes", "Genital herpes" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 743,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Herpes (genital)", "Herpes (genital)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 744,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Genital warts", "Genital warts" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 745,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Gestational diabetes", "Gestational diabetes" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 746,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Diabetes in pregnancy", "Diabetes in pregnancy" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 747,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Giardiasis", "Giardiasis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 748,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Glandular fever", "Glandular fever" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 749,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Glaucoma", "Glaucoma" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 750,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Glomerulonephritis", "Glomerulonephritis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 751,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Glue ear", "Glue ear" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 752,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Goitre", "Goitre" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 753,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Gonorrhoea", "Gonorrhoea" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 754,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Gout", "Gout" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 755,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Guillain-Barré syndrome", "Guillain-Barré syndrome" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 756,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Gum disease", "Gum disease" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 757,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Haemochromatosis", "Haemochromatosis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 758,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Haemophilia", "Haemophilia" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 759,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hair loss", "Hair loss" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 760,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hand tendon repair", "Hand tendon repair" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 761,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Having an operation (surgery)", "Having an operation (surgery)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 762,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Surgery (having an operation)", "Surgery (having an operation)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 763,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hay fever", "Hay fever" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 764,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Head lice and nits", "Head lice and nits" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 765,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hearing loss", "Hearing loss" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 766,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Deafness", "Deafness" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 767,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hearing tests", "Hearing tests" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 768,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Heart attack", "Heart attack" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 769,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Heart block", "Heart block" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 770,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Heartburn and acid reflux", "Heartburn and acid reflux" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 771,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Gastro-oesophageal reflux disease (GORD)", "Gastro-oesophageal reflux disease (GORD)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 772,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Heart failure", "Heart failure" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 773,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Heart transplant", "Heart transplant" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 774,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Heavy periods", "Heavy periods" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 775,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Periods (heavy)", "Periods (heavy)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 776,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hepatitis A", "Hepatitis A" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 777,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hepatitis B", "Hepatitis B" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 778,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hepatitis C", "Hepatitis C" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 779,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hiatus hernia", "Hiatus hernia" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 780,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hernia (hiatus)", "Hernia (hiatus)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 781,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hiccups", "Hiccups" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 782,
                columns: new[] { "Code", "Description" },
                values: new object[] { "High blood pressure (hypertension)", "High blood pressure (hypertension)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 783,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hypertension", "Hypertension" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 784,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Blood pressure (high)", "Blood pressure (high)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 785,
                columns: new[] { "Code", "Description" },
                values: new object[] { "High cholesterol", "High cholesterol" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 786,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Cholesterol (high)", "Cholesterol (high)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 787,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hip fracture", "Hip fracture" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 788,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hip replacement", "Hip replacement" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 789,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Excessive hair growth (hirsutism)", "Excessive hair growth (hirsutism)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 790,
                columns: new[] { "Code", "Description" },
                values: new object[] { "hirsutism", "hirsutism" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 791,
                columns: new[] { "Code", "Description" },
                values: new object[] { "HIV and AIDS", "HIV and AIDS" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 792,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hives", "Hives" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 793,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hodgkin lymphoma", "Hodgkin lymphoma" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 794,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hormone replacement therapy (HRT)", "Hormone replacement therapy (HRT)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 795,
                columns: new[] { "Code", "Description" },
                values: new object[] { "HRT", "HRT" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 796,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Huntington's disease", "Huntington's disease" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 797,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hydrocephalus", "Hydrocephalus" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 798,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hydronephrosis", "Hydronephrosis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 799,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hysterectomy", "Hysterectomy" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 800,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hysteroscopy", "Hysteroscopy" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 801,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Idiopathic pulmonary fibrosis", "Idiopathic pulmonary fibrosis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 802,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Pulmonary fibrosis", "Pulmonary fibrosis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 803,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Ileostomy", "Ileostomy" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 804,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Impetigo", "Impetigo" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 805,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Infertility", "Infertility" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 806,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Inguinal hernia repair", "Inguinal hernia repair" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 807,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hernia (inguinal)", "Hernia (inguinal)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 808,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Insect bites and stings", "Insect bites and stings" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 809,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Sting or bite (insect)", "Sting or bite (insect)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 810,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Insomnia", "Insomnia" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 811,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Iron deficiency anaemia", "Iron deficiency anaemia" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 812,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Anaemia (iron deficiency)", "Anaemia (iron deficiency)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 813,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Irregular periods", "Irregular periods" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 814,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Periods (irregular)", "Periods (irregular)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 815,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Irritable bowel syndrome (IBS)", "Irritable bowel syndrome (IBS)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 816,
                columns: new[] { "Code", "Description" },
                values: new object[] { "IBS", "IBS" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 817,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Itchy bottom", "Itchy bottom" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 818,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Anus (itchy)", "Anus (itchy)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 819,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Itchy skin", "Itchy skin" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 820,
                columns: new[] { "Code", "Description" },
                values: new object[] { "IVF", "IVF" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 821,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Japanese encephalitis", "Japanese encephalitis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 822,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Jaundice", "Jaundice" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 823,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Newborn jaundice", "Newborn jaundice" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 824,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Jaundice in newborns", "Jaundice in newborns" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 825,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Jellyfish and other sea creature stings", "Jellyfish and other sea creature stings" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 826,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Jet lag", "Jet lag" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 827,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Joint hypermobility syndrome", "Joint hypermobility syndrome" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 828,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Kawasaki disease", "Kawasaki disease" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 829,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Kidney cancer", "Kidney cancer" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 830,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Chronic kidney disease", "Chronic kidney disease" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 831,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Kidney failure", "Kidney failure" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 832,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Kidney infection", "Kidney infection" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 833,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Kidney stones", "Kidney stones" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 834,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Kidney transplant", "Kidney transplant" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 835,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Knee ligament surgery", "Knee ligament surgery" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 836,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Knee replacement", "Knee replacement" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 837,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Kyphosis", "Kyphosis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 838,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Labyrinthitis and vestibular neuritis", "Labyrinthitis and vestibular neuritis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 839,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Vestibular neuritis", "Vestibular neuritis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 840,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Lactose intolerance", "Lactose intolerance" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 841,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Laparoscopy (keyhole surgery)", "Laparoscopy (keyhole surgery)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 842,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Laryngeal (larynx) cancer", "Laryngeal (larynx) cancer" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 843,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Laryngitis", "Laryngitis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 844,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Laxatives", "Laxatives" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 845,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Lazy eye", "Lazy eye" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 846,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Amblyopia", "Amblyopia" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 847,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Leg cramps", "Leg cramps" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 848,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Venous leg ulcer", "Venous leg ulcer" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 849,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Leg ulcer", "Leg ulcer" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 850,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Leptospirosis (Weil's disease)", "Leptospirosis (Weil's disease)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 851,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Weil's disease", "Weil's disease" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 852,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Leukoplakia", "Leukoplakia" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 853,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Lichen planus", "Lichen planus" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 854,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Listeriosis", "Listeriosis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 855,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Liver cancer", "Liver cancer" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 856,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Liver transplant", "Liver transplant" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 857,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Long-sightedness", "Long-sightedness" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 858,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Low blood pressure (hypotension)", "Low blood pressure (hypotension)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 859,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Blood pressure (low)", "Blood pressure (low)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 860,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hypotension", "Hypotension" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 861,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Lumbar decompression surgery", "Lumbar decompression surgery" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 862,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Lumbar puncture", "Lumbar puncture" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 863,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Lung cancer", "Lung cancer" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 864,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Lung transplant", "Lung transplant" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 865,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Lupus", "Lupus" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 866,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Lymphoedema", "Lymphoedema" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 867,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Age-related macular degeneration (AMD)", "Age-related macular degeneration (AMD)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 868,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Macular degeneration (age-related)", "Macular degeneration (age-related)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 869,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Malaria", "Malaria" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 870,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Malignant brain tumour (brain cancer)", "Malignant brain tumour (brain cancer)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 871,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Brain tumour (malignant)", "Brain tumour (malignant)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 872,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Malnutrition", "Malnutrition" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 873,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Marfan syndrome", "Marfan syndrome" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 874,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Mastectomy", "Mastectomy" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 875,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Mastitis", "Mastitis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 876,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Mastocytosis", "Mastocytosis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 877,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Measles", "Measles" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 878,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Medicines information", "Medicines information" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 879,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Skin cancer (melanoma)", "Skin cancer (melanoma)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 880,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Meningitis", "Meningitis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 881,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Menopause", "Menopause" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 882,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Migraine", "Migraine" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 883,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Head injury and concussion", "Head injury and concussion" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 884,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Miscarriage", "Miscarriage" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 885,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Moles", "Moles" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 886,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Molluscum contagiosum", "Molluscum contagiosum" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 887,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Motor neurone disease", "Motor neurone disease" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 888,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Mouth cancer", "Mouth cancer" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 889,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Tongue cancer", "Tongue cancer" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 890,
                columns: new[] { "Code", "Description" },
                values: new object[] { "MRI scan", "MRI scan" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 891,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Mucositis", "Mucositis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 892,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Multiple myeloma", "Multiple myeloma" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 893,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Myeloma", "Myeloma" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 894,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Multiple sclerosis", "Multiple sclerosis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 895,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Mumps", "Mumps" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 896,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Munchausen's syndrome", "Munchausen's syndrome" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 897,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Muscular dystrophy", "Muscular dystrophy" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 898,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Myasthenia gravis", "Myasthenia gravis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 899,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Narcolepsy", "Narcolepsy" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 900,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Nasal polyps", "Nasal polyps" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 901,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Newborn respiratory distress syndrome", "Newborn respiratory distress syndrome" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 902,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Neurofibromatosis type 1", "Neurofibromatosis type 1" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 903,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Neurofibromatosis type 2", "Neurofibromatosis type 2" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 904,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Non-allergic rhinitis", "Non-allergic rhinitis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 905,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Non-gonococcal urethritis", "Non-gonococcal urethritis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 906,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Urethritis (NGU)", "Urethritis (NGU)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 907,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Non-Hodgkin lymphoma", "Non-Hodgkin lymphoma" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 908,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Skin cancer (non-melanoma)", "Skin cancer (non-melanoma)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 909,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Squamous cell carcinoma", "Squamous cell carcinoma" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 910,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Basal cell carcinoma", "Basal cell carcinoma" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 911,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Noonan syndrome", "Noonan syndrome" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 912,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Nosebleed", "Nosebleed" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 913,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Obesity", "Obesity" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 914,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Obsessive compulsive disorder (OCD)", "Obsessive compulsive disorder (OCD)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 915,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Occupational therapy", "Occupational therapy" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 916,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Oesophageal cancer", "Oesophageal cancer" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 917,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Orthodontics", "Orthodontics" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 918,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Osteoarthritis", "Osteoarthritis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 919,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Osteomyelitis", "Osteomyelitis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 920,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Osteopathy", "Osteopathy" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 921,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Osteoporosis", "Osteoporosis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 922,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Ovarian cancer", "Ovarian cancer" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 923,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Ovarian cyst", "Ovarian cyst" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 924,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Overactive thyroid (hyperthyroidism)", "Overactive thyroid (hyperthyroidism)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 925,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hyperthyroidism", "Hyperthyroidism" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 926,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Pacemaker implantation", "Pacemaker implantation" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 927,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Paget's disease of bone", "Paget's disease of bone" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 928,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Paget's disease of the nipple", "Paget's disease of the nipple" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 929,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Pancreas transplant", "Pancreas transplant" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 930,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Pancreatic cancer", "Pancreatic cancer" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 931,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Paralysis", "Paralysis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 932,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Parkinson's disease", "Parkinson's disease" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 933,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Pelvic inflammatory disease", "Pelvic inflammatory disease" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 934,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Pelvic organ prolapse", "Pelvic organ prolapse" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 935,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Prolapse (pelvic organ)", "Prolapse (pelvic organ)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 936,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Pemphigus vulgaris", "Pemphigus vulgaris" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 937,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Perforated eardrum", "Perforated eardrum" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 938,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Eardrum (burst)", "Eardrum (burst)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 939,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Pericarditis", "Pericarditis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 940,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Peripheral arterial disease (PAD)", "Peripheral arterial disease (PAD)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 941,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Peripheral neuropathy", "Peripheral neuropathy" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 942,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Peritonitis", "Peritonitis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 943,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Phobias", "Phobias" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 944,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Physiotherapy", "Physiotherapy" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 945,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Piles (haemorrhoids)", "Piles (haemorrhoids)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 946,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Piles", "Piles" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 947,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Pilonidal sinus", "Pilonidal sinus" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 948,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Plastic surgery", "Plastic surgery" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 949,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Pneumonia", "Pneumonia" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 950,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Poisoning", "Poisoning" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 951,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Polycystic ovary syndrome", "Polycystic ovary syndrome" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 952,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Polycythaemia", "Polycythaemia" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 953,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Polymyalgia rheumatica", "Polymyalgia rheumatica" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 954,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Post-herpetic neuralgia", "Post-herpetic neuralgia" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 955,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Postnatal depression", "Postnatal depression" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 956,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Post-polio syndrome", "Post-polio syndrome" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 957,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Post-traumatic stress disorder (PTSD)", "Post-traumatic stress disorder (PTSD)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 958,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Prader-Willi syndrome", "Prader-Willi syndrome" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 959,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Pre-eclampsia", "Pre-eclampsia" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 960,
                columns: new[] { "Code", "Description" },
                values: new object[] { "PMS (premenstrual syndrome)", "PMS (premenstrual syndrome)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 961,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Pressure ulcers (pressure sores)", "Pressure ulcers (pressure sores)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 962,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Priapism (painful erections)", "Priapism (painful erections)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 963,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Primary biliary cholangitis (primary biliary cirrhosis)", "Primary biliary cholangitis (primary biliary cirrhosis)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 964,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Progressive supranuclear palsy", "Progressive supranuclear palsy" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 965,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Prostate cancer", "Prostate cancer" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 966,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Benign prostate enlargement", "Benign prostate enlargement" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 967,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Prostate enlargement", "Prostate enlargement" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 968,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Psoriasis", "Psoriasis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 969,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Psychosis", "Psychosis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 970,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Pulmonary embolism", "Pulmonary embolism" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 971,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Pulmonary hypertension", "Pulmonary hypertension" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 972,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Rabies", "Rabies" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 973,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Radiotherapy", "Radiotherapy" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 974,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Raynaud's", "Raynaud's" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 975,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Reactive arthritis", "Reactive arthritis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 976,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Rectal examination", "Rectal examination" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 977,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Repetitive strain injury (RSI)", "Repetitive strain injury (RSI)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 978,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Restless legs syndrome", "Restless legs syndrome" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 979,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Restricted growth (dwarfism)", "Restricted growth (dwarfism)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 980,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Dwarfism", "Dwarfism" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 981,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Detached retina (retinal detachment)", "Detached retina (retinal detachment)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 982,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Retinal detachment", "Retinal detachment" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 983,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Rhesus disease", "Rhesus disease" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 984,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Rheumatic fever", "Rheumatic fever" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 985,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Rheumatoid arthritis", "Rheumatoid arthritis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 986,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Rickets and osteomalacia", "Rickets and osteomalacia" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 987,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Osteomalacia", "Osteomalacia" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 988,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Root canal treatment", "Root canal treatment" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 989,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Rosacea", "Rosacea" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 990,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Rubella (german measles)", "Rubella (german measles)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 991,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Scabies", "Scabies" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 992,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Scars", "Scars" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 993,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Schizophrenia", "Schizophrenia" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 994,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Sciatica", "Sciatica" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 995,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Buttock pain", "Buttock pain" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 996,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Scoliosis", "Scoliosis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 997,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Scurvy", "Scurvy" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 998,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Seasonal affective disorder (SAD)", "Seasonal affective disorder (SAD)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 999,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Self-harm", "Self-harm" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1000,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Sepsis", "Sepsis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1001,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Severe head injury", "Severe head injury" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1002,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Shingles", "Shingles" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1003,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Short-sightedness (myopia)", "Short-sightedness (myopia)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1004,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Myopia", "Myopia" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1005,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Shoulder pain", "Shoulder pain" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1006,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Sickle cell disease", "Sickle cell disease" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1007,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Sjögren's syndrome", "Sjögren's syndrome" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1008,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Sleep paralysis", "Sleep paralysis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1009,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Slipped disc", "Slipped disc" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1010,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Small bowel transplant", "Small bowel transplant" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1011,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Bowel transplant", "Bowel transplant" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1012,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Snoring", "Snoring" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1013,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Spina bifida", "Spina bifida" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1014,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Spinal muscular atrophy", "Spinal muscular atrophy" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1015,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Sports injuries", "Sports injuries" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1016,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Sprains and strains", "Sprains and strains" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1017,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Squint", "Squint" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1018,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Selective serotonin reuptake inhibitors (SSRIs)", "Selective serotonin reuptake inhibitors (SSRIs)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1019,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Stammering", "Stammering" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1020,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Stuttering", "Stuttering" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1021,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Statins", "Statins" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1022,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Stem cell and bone marrow transplants", "Stem cell and bone marrow transplants" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1023,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Stillbirth", "Stillbirth" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1024,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Stomach cancer", "Stomach cancer" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1025,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Stomach ulcer", "Stomach ulcer" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1026,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Stroke", "Stroke" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1027,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Subarachnoid haemorrhage", "Subarachnoid haemorrhage" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1028,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Brain haemorrhage", "Brain haemorrhage" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1029,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Subdural haematoma", "Subdural haematoma" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1030,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Help for suicidal thoughts", "Help for suicidal thoughts" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1031,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Suicidal thoughts", "Suicidal thoughts" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1032,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Supraventricular tachycardia (SVT)", "Supraventricular tachycardia (SVT)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1033,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Dysphagia (swallowing problems)", "Dysphagia (swallowing problems)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1034,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Swallowing problems", "Swallowing problems" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1035,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Syphilis", "Syphilis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1036,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Coccydynia (tailbone pain)", "Coccydynia (tailbone pain)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1037,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Tay-Sachs disease", "Tay-Sachs disease" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1038,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Teeth grinding (bruxism)", "Teeth grinding (bruxism)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1039,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Bruxism", "Bruxism" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1040,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Tendonitis", "Tendonitis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1041,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Tennis elbow", "Tennis elbow" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1042,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Testicle lumps and swellings", "Testicle lumps and swellings" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1043,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Testicular cancer", "Testicular cancer" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1044,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Thalassaemia", "Thalassaemia" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1045,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Threadworms", "Threadworms" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1046,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Thyroid cancer", "Thyroid cancer" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1047,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Tick-borne encephalitis (TBE)", "Tick-borne encephalitis (TBE)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1048,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Tics", "Tics" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1049,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Tinnitus", "Tinnitus" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1050,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Tonsillitis", "Tonsillitis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1051,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Quinsy", "Quinsy" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1052,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Tourette's syndrome", "Tourette's syndrome" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1053,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Toxocariasis", "Toxocariasis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1054,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Toxoplasmosis", "Toxoplasmosis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1055,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Tracheostomy", "Tracheostomy" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1056,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Transient ischaemic attack (TIA)", "Transient ischaemic attack (TIA)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1057,
                columns: new[] { "Code", "Description" },
                values: new object[] { "TIA", "TIA" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1058,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Transurethral resection of the prostate (TURP)", "Transurethral resection of the prostate (TURP)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1059,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Travel vaccinations", "Travel vaccinations" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1060,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Trichomoniasis", "Trichomoniasis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1061,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Trichotillomania (hair pulling disorder)", "Trichotillomania (hair pulling disorder)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1062,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Trigeminal neuralgia", "Trigeminal neuralgia" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1063,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Trigger finger", "Trigger finger" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1064,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Tuberculosis (TB)", "Tuberculosis (TB)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1065,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Tuberous sclerosis", "Tuberous sclerosis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1066,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Turner syndrome", "Turner syndrome" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1067,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Type 2 diabetes", "Type 2 diabetes" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1068,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Diabetes (type 2)", "Diabetes (type 2)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1069,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Typhoid fever", "Typhoid fever" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1070,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Ulcerative colitis", "Ulcerative colitis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1071,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Umbilical hernia repair", "Umbilical hernia repair" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1072,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hernia (umbilical)", "Hernia (umbilical)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1073,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Underactive thyroid (hypothyroidism)", "Underactive thyroid (hypothyroidism)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1074,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hypothyroidism", "Hypothyroidism" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1075,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Undescended testicles", "Undescended testicles" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1076,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Urinary catheter", "Urinary catheter" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1077,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Urinary incontinence", "Urinary incontinence" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1078,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Incontinence (urinary)", "Incontinence (urinary)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1079,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Uveitis", "Uveitis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1080,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Vaginal cancer", "Vaginal cancer" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1081,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Vaginismus", "Vaginismus" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1082,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Varicose eczema", "Varicose eczema" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1083,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Eczema (varicose)", "Eczema (varicose)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1084,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Varicose veins", "Varicose veins" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1085,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Vascular dementia", "Vascular dementia" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1086,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Dementia (vascular)", "Dementia (vascular)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1087,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Vertigo", "Vertigo" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1088,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Vitamin B12 or folate deficiency anaemia", "Vitamin B12 or folate deficiency anaemia" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1089,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Anaemia (vitamin B12 or folate deficiency)", "Anaemia (vitamin B12 or folate deficiency)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1090,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Vitamins and minerals", "Vitamins and minerals" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1091,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Vitiligo", "Vitiligo" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1092,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Vulval cancer", "Vulval cancer" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1093,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Watering eyes", "Watering eyes" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1094,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Weight loss surgery", "Weight loss surgery" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1095,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Whiplash", "Whiplash" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1096,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Wisdom tooth removal", "Wisdom tooth removal" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1097,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Womb (uterus) cancer", "Womb (uterus) cancer" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1098,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Uterine (womb) cancer", "Uterine (womb) cancer" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1099,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Endometrial cancer", "Endometrial cancer" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1100,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Yellow fever", "Yellow fever" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1101,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Abortion", "Abortion" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1102,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Bird flu", "Bird flu" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1103,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Avian flu", "Avian flu" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1104,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Antifungal medicines", "Antifungal medicines" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1105,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Blood transfusion", "Blood transfusion" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1106,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Ringworm", "Ringworm" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1107,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Early menopause", "Early menopause" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1108,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Menopause (early)", "Menopause (early)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1109,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Floaters and flashes in the eyes", "Floaters and flashes in the eyes" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1110,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Eye floaters", "Eye floaters" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1111,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Your contraception guide", "Your contraception guide" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1112,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Dementia guide", "Dementia guide" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1113,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Fitness Studio exercise videos", "Fitness Studio exercise videos" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1114,
                columns: new[] { "Code", "Description" },
                values: new object[] { "NHS Health Check", "NHS Health Check" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1115,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Worms in humans", "Worms in humans" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1116,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hookworm", "Hookworm" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1117,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Tapeworm", "Tapeworm" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1118,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Roundworm", "Roundworm" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1119,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Tremor or shaking hands", "Tremor or shaking hands" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1120,
                columns: new[] { "Code", "Description" },
                values: new object[] { "tremor", "tremor" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1121,
                columns: new[] { "Code", "Description" },
                values: new object[] { "essential tremor", "essential tremor" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1122,
                columns: new[] { "Code", "Description" },
                values: new object[] { "shaking", "shaking" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1123,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Low white blood cell count", "Low white blood cell count" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1124,
                columns: new[] { "Code", "Description" },
                values: new object[] { "White blood cell count (low)", "White blood cell count (low)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1125,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Bunions", "Bunions" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1126,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Lost or changed sense of smell", "Lost or changed sense of smell" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1127,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Anosmia", "Anosmia" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1128,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Sense of smell (lost/changed)", "Sense of smell (lost/changed)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1129,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Soiling (child pooing their pants)", "Soiling (child pooing their pants)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1130,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Oral thrush (mouth thrush)", "Oral thrush (mouth thrush)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1131,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Mouth thrush", "Mouth thrush" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1132,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Temporal arteritis", "Temporal arteritis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1133,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Giant cell arteritis", "Giant cell arteritis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1134,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Memory loss (amnesia)", "Memory loss (amnesia)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1135,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Amnesia", "Amnesia" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1136,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Headaches", "Headaches" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1137,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Sinusitis (sinus infection)", "Sinusitis (sinus infection)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1138,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Sinusitis", "Sinusitis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1139,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Thrush in men and women", "Thrush in men and women" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1140,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Cold sores", "Cold sores" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1141,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Group B strep", "Group B strep" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1142,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Skin picking disorder", "Skin picking disorder" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1143,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hyperparathyroidism", "Hyperparathyroidism" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1144,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hypoparathyroidism", "Hypoparathyroidism" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1145,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Ear infections", "Ear infections" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1146,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Twitching eyes and muscles", "Twitching eyes and muscles" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1147,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Learning disabilities", "Learning disabilities" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1148,
                columns: new[] { "Code", "Description" },
                values: new object[] { "NHS screening", "NHS screening" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1149,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hormone headaches", "Hormone headaches" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1150,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Headaches (hormone)", "Headaches (hormone)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1151,
                columns: new[] { "Code", "Description" },
                values: new object[] { "What to do if someone has a seizure (fit)", "What to do if someone has a seizure (fit)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1152,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Seizures (fits)", "Seizures (fits)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1153,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Fits (seizures)", "Fits (seizures)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1154,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Complementary and alternative medicine", "Complementary and alternative medicine" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1155,
                columns: new[] { "Code", "Description" },
                values: new object[] { "End of life care", "End of life care" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1156,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Knocked-out tooth", "Knocked-out tooth" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1157,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Tooth knocked out", "Tooth knocked out" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1158,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Chipped broken or cracked tooth", "Chipped broken or cracked tooth" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1159,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Tooth (chipped or broken)", "Tooth (chipped or broken)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1160,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Diarrhoea and vomiting", "Diarrhoea and vomiting" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1161,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Tummy bug", "Tummy bug" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1162,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Vomiting", "Vomiting" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1163,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Stomach bug", "Stomach bug" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1164,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Gastroenteritis", "Gastroenteritis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1165,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Diarrhoea", "Diarrhoea" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1166,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Being sick", "Being sick" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1167,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Bullous pemphigoid", "Bullous pemphigoid" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1168,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Feeling sick (nausea)", "Feeling sick (nausea)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1169,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Nausea", "Nausea" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1170,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Type 1 diabetes", "Type 1 diabetes" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1171,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Diabetes (type 1)", "Diabetes (type 1)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1172,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Social care and support guide", "Social care and support guide" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1173,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Middle East respiratory syndrome (MERS)", "Middle East respiratory syndrome (MERS)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1174,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Monkeypox", "Monkeypox" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1175,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Diabetic eye screening 2", "Diabetic eye screening 2" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1176,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Medical cannabis (and cannabis oils)", "Medical cannabis (and cannabis oils)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1177,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Cannabis oil (medical cannabis)", "Cannabis oil (medical cannabis)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1178,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Diabetic eye screening 1", "Diabetic eye screening 1" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1179,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Body odour (BO)", "Body odour (BO)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1180,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Human papillomavirus (HPV)", "Human papillomavirus (HPV)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1181,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Plantar fasciitis", "Plantar fasciitis" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1182,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Foot pain", "Foot pain" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1183,
                columns: new[] { "Code", "Description" },
                values: new object[] { "heel pain", "heel pain" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1184,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Toe pain", "Toe pain" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1185,
                columns: new[] { "Code", "Description" },
                values: new object[] { "ankle pain", "ankle pain" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1186,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Autism", "Autism" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1187,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Asperger's", "Asperger's" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1188,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Cosmetic procedures", "Cosmetic procedures" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1189,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hand pain", "Hand pain" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1190,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Swollen arms and hands (oedema)", "Swollen arms and hands (oedema)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1191,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Colonoscopy", "Colonoscopy" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1192,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Genetic and genomic testing", "Genetic and genomic testing" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1193,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Sleep apnoea", "Sleep apnoea" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1194,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Vaccinations", "Vaccinations" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1195,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Mental health and wellbeing", "Mental health and wellbeing" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1196,
                columns: new[] { "Code", "Description" },
                values: new object[] { "High temperature (fever) in adults", "High temperature (fever) in adults" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1197,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Fever in adults", "Fever in adults" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1198,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Coronavirus (COVID-19)", "Coronavirus (COVID-19)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1199,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Baby", "Baby" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1200,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Do not attempt cardiopulmonary resuscitation (DNACPR) decisions", "Do not attempt cardiopulmonary resuscitation (DNACPR) decisions" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1201,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Testicle pain", "Testicle pain" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1202,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Pain in testicles", "Pain in testicles" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1203,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Hearing aids and implants", "Hearing aids and implants" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1204,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Breast screening (mammogram)", "Breast screening (mammogram)" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1205,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Smelly feet", "Smelly feet" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1206,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Academic attainment", "Academic attainment" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1207,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Ageing", "Ageing" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1208,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Aggression", "Aggression" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1209,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Antenatal care", "Antenatal care" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1210,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Blood donation", "Blood donation" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1211,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Body image", "Body image" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1212,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Breastfeeding", "Breastfeeding" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1213,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Care homes", "Care homes" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1214,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Carers", "Carers" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1215,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Child development", "Child development" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1216,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Complementary therapies", "Complementary therapies" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1217,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Contraception", "Contraception" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1218,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Domestic violence", "Domestic violence" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1219,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Eating well", "Eating well" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1220,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Exercise and sports", "Exercise and sports" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1221,
                columns: new[] { "Code", "Description" },
                values: new object[] { "General wellbeing", "General wellbeing" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1222,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Genetic screening", "Genetic screening" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1223,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Healthy volunteers", "Healthy volunteers" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1224,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Improving care and services", "Improving care and services" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1225,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Healthy lifestyle", "Healthy lifestyle" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1226,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Long COVID", "Long COVID" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1227,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Obesity risk", "Obesity risk" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1228,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Occupational health", "Occupational health" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1229,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Parenting", "Parenting" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1230,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Public health", "Public health" });

            migrationBuilder.UpdateData(
                table: "SysRefHealthCondition",
                keyColumn: "Id",
                keyValue: 1231,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Sleeping well", "Sleeping well" });

            migrationBuilder.InsertData(
                table: "SysRefHealthCondition",
                columns: new[] { "Id", "Code", "Description", "IsDeleted" },
                values: new object[,]
                {
                    { 1232, "Smoking", "Smoking", false },
                    { 1233, "Supplements", "Supplements", false }
                });
        }
    }
}
