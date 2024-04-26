using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Dynamo.Stream.Handler.Migrations
{
    /// <inheritdoc />
    public partial class asd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ParticipantIdentifiers_Value",
                table: "ParticipantIdentifiers");

            migrationBuilder.DropIndex(
                name: "IX_ParticipantAddress_Postcode",
                table: "ParticipantAddress");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SysConfiguration",
                table: "SysConfiguration");

            migrationBuilder.DeleteData(
                table: "SysConfiguration",
                keyColumn: "Id",
                keyValue: 1);

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
                table: "SysRefEmailDeliveryStatus",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SysRefEmailDeliveryStatus",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SysRefEmailDeliveryStatus",
                keyColumn: "Id",
                keyValue: 3);

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
                table: "SysRefIdentifierType",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SysRefIdentifierType",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SysRefIdentifierType",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "SysRefRole",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SysRefRole",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.RenameTable(
                name: "SysConfiguration",
                newName: "SysConfigurations");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SysConfigurations",
                table: "SysConfigurations",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SysConfigurations",
                table: "SysConfigurations");

            migrationBuilder.RenameTable(
                name: "SysConfigurations",
                newName: "SysConfiguration");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SysConfiguration",
                table: "SysConfiguration",
                column: "Id");

            migrationBuilder.InsertData(
                table: "SysConfiguration",
                columns: new[] { "Id", "Name", "Value" },
                values: new object[] { 1, "IsInMaintenanceMode", "False" });

            migrationBuilder.InsertData(
                table: "SysRefCommunicationLanguage",
                columns: new[] { "Id", "Code", "Description", "IsDeleted" },
                values: new object[,]
                {
                    { 1, "en-GB", "English", false },
                    { 2, "cy-GB", "Welsh", false }
                });

            migrationBuilder.InsertData(
                table: "SysRefDailyLifeImpact",
                columns: new[] { "Id", "Code", "Description", "IsDeleted" },
                values: new object[,]
                {
                    { 1, "Yes, a lot", "Yes, a lot", false },
                    { 2, "Yes, a little", "Yes, a little", false },
                    { 3, "Not at all", "Not at all", false },
                    { 4, "Prefer not to say", "Prefer not to say", false }
                });

            migrationBuilder.InsertData(
                table: "SysRefEmailDeliveryStatus",
                columns: new[] { "Id", "Code", "Description", "IsDeleted" },
                values: new object[,]
                {
                    { 1, "Sent", "Sent", false },
                    { 2, "Delivered", "Delivered", false },
                    { 3, "RegisteredInterest", "RegisteredInterest", false }
                });

            migrationBuilder.InsertData(
                table: "SysRefGender",
                columns: new[] { "Id", "Code", "Description", "IsDeleted" },
                values: new object[,]
                {
                    { 1, "Male", "Male", false },
                    { 2, "Female", "Female", false },
                    { 3, "Prefer Not to Say", "Prefer Not to Say", false }
                });

            migrationBuilder.InsertData(
                table: "SysRefHealthCondition",
                columns: new[] { "Id", "Code", "Description", "IsDeleted" },
                values: new object[,]
                {
                    { 1, "Acanthosis nigricans", "Acanthosis nigricans", false },
                    { 2, "Achalasia", "Achalasia", false },
                    { 3, "Acid and chemical burns", "Acid and chemical burns", false },
                    { 4, "Acoustic neuroma (vestibular schwannoma)", "Acoustic neuroma (vestibular schwannoma)", false },
                    { 5, "Vestibular schwannoma", "Vestibular schwannoma", false },
                    { 6, "Acromegaly", "Acromegaly", false },
                    { 7, "Gigantism", "Gigantism", false },
                    { 8, "Urine albumin to creatinine ratio (ACR)", "Urine albumin to creatinine ratio (ACR)", false },
                    { 9, "Actinic keratoses (solar keratoses)", "Actinic keratoses (solar keratoses)", false },
                    { 10, "Solar keratoses", "Solar keratoses", false },
                    { 11, "Acupuncture", "Acupuncture", false },
                    { 12, "Acute cholecystitis", "Acute cholecystitis", false },
                    { 13, "Gallbladder pain", "Gallbladder pain", false },
                    { 14, "Cholecystitis (acute)", "Cholecystitis (acute)", false },
                    { 15, "MND", "MND", false },
                    { 16, "Acute kidney injury", "Acute kidney injury", false },
                    { 17, "Acute respiratory distress syndrome", "Acute respiratory distress syndrome", false },
                    { 18, "Adenoidectomy", "Adenoidectomy", false },
                    { 19, "Air or gas embolism", "Air or gas embolism", false },
                    { 20, "Decompression sickness", "Decompression sickness", false },
                    { 21, "Alcohol poisoning", "Alcohol poisoning", false },
                    { 22, "Alexander technique", "Alexander technique", false },
                    { 23, "Alkaptonuria", "Alkaptonuria", false },
                    { 24, "Amputation", "Amputation", false },
                    { 25, "Amyloidosis", "Amyloidosis", false },
                    { 26, "Anabolic steroid misuse", "Anabolic steroid misuse", false },
                    { 27, "Steroid misuse", "Steroid misuse", false },
                    { 28, "Anaesthesia", "Anaesthesia", false },
                    { 29, "Anal cancer", "Anal cancer", false },
                    { 30, "Anal pain", "Anal pain", false },
                    { 31, "Proctalgia", "Proctalgia", false },
                    { 32, "Angelman syndrome", "Angelman syndrome", false },
                    { 33, "Animal and human bites", "Animal and human bites", false },
                    { 34, "Bite (animal or human)", "Bite (animal or human)", false },
                    { 35, "Anosmia", "Anosmia", false },
                    { 36, "Antacids", "Antacids", false },
                    { 37, "Antihistamines", "Antihistamines", false },
                    { 38, "Antisocial personality disorder", "Antisocial personality disorder", false },
                    { 39, "Anxiety disorders in children", "Anxiety disorders in children", false },
                    { 40, "Arrhythmia", "Arrhythmia", false },
                    { 41, "Heart rhythm problems", "Heart rhythm problems", false },
                    { 42, "Arterial thrombosis", "Arterial thrombosis", false },
                    { 43, "Intrauterine insemination (IUI)", "Intrauterine insemination (IUI)", false },
                    { 44, "Asbestosis", "Asbestosis", false },
                    { 45, "Aspirin", "Aspirin", false },
                    { 46, "Atherosclerosis (arteriosclerosis)", "Atherosclerosis (arteriosclerosis)", false },
                    { 47, "Athlete's foot", "Athlete's foot", false },
                    { 48, "Auditory processing disorder (APD)", "Auditory processing disorder (APD)", false },
                    { 49, "Balanitis", "Balanitis", false },
                    { 50, "Barium enema", "Barium enema", false },
                    { 51, "Bedbugs", "Bedbugs", false },
                    { 52, "Beta blockers", "Beta blockers", false },
                    { 53, "Black eye", "Black eye", false },
                    { 54, "Blood clots", "Blood clots", false },
                    { 55, "Blood groups", "Blood groups", false },
                    { 56, "Blood in semen (haematospermia)", "Blood in semen (haematospermia)", false },
                    { 57, "Blood in urine", "Blood in urine", false },
                    { 58, "Blood pressure test", "Blood pressure test", false },
                    { 59, "Body dysmorphic disorder (BDD)", "Body dysmorphic disorder (BDD)", false },
                    { 60, "Infected piercings", "Infected piercings", false },
                    { 61, "Boils", "Boils", false },
                    { 62, "Botulism", "Botulism", false },
                    { 63, "Bowel polyps", "Bowel polyps", false },
                    { 64, "Bowen's disease", "Bowen's disease", false },
                    { 65, "Brain tumours", "Brain tumours", false },
                    { 66, "Breast pain", "Breast pain", false },
                    { 67, "Breast reduction on the NHS", "Breast reduction on the NHS", false },
                    { 68, "Breath-holding in babies and children", "Breath-holding in babies and children", false },
                    { 69, "Broken ankle", "Broken ankle", false },
                    { 70, "Broken arm or wrist", "Broken arm or wrist", false },
                    { 71, "Broken collarbone", "Broken collarbone", false },
                    { 72, "Broken finger or thumb", "Broken finger or thumb", false },
                    { 73, "Broken leg", "Broken leg", false },
                    { 74, "Broken nose", "Broken nose", false },
                    { 75, "Broken or bruised ribs", "Broken or bruised ribs", false },
                    { 76, "Broken toe", "Broken toe", false },
                    { 77, "Bronchitis", "Bronchitis", false },
                    { 78, "Brucellosis", "Brucellosis", false },
                    { 79, "Brugada syndrome", "Brugada syndrome", false },
                    { 80, "Carbon monoxide poisoning", "Carbon monoxide poisoning", false },
                    { 81, "Neuroendocrine tumours and carcinoid syndrome", "Neuroendocrine tumours and carcinoid syndrome", false },
                    { 82, "Cardiomyopathy", "Cardiomyopathy", false },
                    { 83, "Cardiovascular disease", "Cardiovascular disease", false },
                    { 84, "Age-related cataracts", "Age-related cataracts", false },
                    { 85, "Cataracts (age-related)", "Cataracts (age-related)", false },
                    { 86, "Catarrh", "Catarrh", false },
                    { 87, "Cavernoma", "Cavernoma", false },
                    { 88, "Clostridium difficile (C. diff) infection", "Clostridium difficile (C. diff) infection", false },
                    { 89, "Carcinoembryonic antigen (CEA) test", "Carcinoembryonic antigen (CEA) test", false },
                    { 90, "Cervical rib", "Cervical rib", false },
                    { 91, "Thoracic outlet syndrome", "Thoracic outlet syndrome", false },
                    { 92, "Charles Bonnet syndrome", "Charles Bonnet syndrome", false },
                    { 93, "Chest infection", "Chest infection", false },
                    { 94, "Chest pain", "Chest pain", false },
                    { 95, "Heart pain", "Heart pain", false },
                    { 96, "Chiari malformation", "Chiari malformation", false },
                    { 97, "Chilblains", "Chilblains", false },
                    { 98, "Chiropractic", "Chiropractic", false },
                    { 99, "Cholesteatoma", "Cholesteatoma", false },
                    { 100, "Chronic traumatic encephalopathy", "Chronic traumatic encephalopathy", false },
                    { 101, "Circumcision in boys", "Circumcision in boys", false },
                    { 102, "Circumcision in men", "Circumcision in men", false },
                    { 103, "Claustrophobia", "Claustrophobia", false },
                    { 104, "Cluster headaches", "Cluster headaches", false },
                    { 105, "Colour vision deficiency (colour blindness)", "Colour vision deficiency (colour blindness)", false },
                    { 106, "Coma", "Coma", false },
                    { 107, "Compartment syndrome", "Compartment syndrome", false },
                    { 108, "Concussion", "Concussion", false },
                    { 109, "Sudden confusion (delirium)", "Sudden confusion (delirium)", false },
                    { 110, "Confusion (sudden)", "Confusion (sudden)", false },
                    { 111, "Delirium", "Delirium", false },
                    { 112, "Costochondritis", "Costochondritis", false },
                    { 113, "Cough", "Cough", false },
                    { 114, "Coughing up blood (blood in phlegm)", "Coughing up blood (blood in phlegm)", false },
                    { 115, "Cradle cap", "Cradle cap", false },
                    { 116, "CT scan", "CT scan", false },
                    { 117, "Cuts and grazes", "Cuts and grazes", false },
                    { 118, "Blue skin or lips (cyanosis)", "Blue skin or lips (cyanosis)", false },
                    { 119, "Cyanosis", "Cyanosis", false },
                    { 120, "Cyclical vomiting syndrome", "Cyclical vomiting syndrome", false },
                    { 121, "Cyclospora", "Cyclospora", false },
                    { 122, "Cyclothymia", "Cyclothymia", false },
                    { 123, "Dandruff", "Dandruff", false },
                    { 124, "Decongestants", "Decongestants", false },
                    { 125, "Dental abscess", "Dental abscess", false },
                    { 126, "Dentures (false teeth)", "Dentures (false teeth)", false },
                    { 127, "Dyspraxia (developmental co-ordination disorder) in adults", "Dyspraxia (developmental co-ordination disorder) in adults", false },
                    { 128, "Developmental dysplasia of the hip", "Developmental dysplasia of the hip", false },
                    { 129, "Congenital hip dislocation", "Congenital hip dislocation", false },
                    { 130, "Hip dysplasia", "Hip dysplasia", false },
                    { 131, "Diabetes", "Diabetes", false },
                    { 132, "Diabetic eye screening", "Diabetic eye screening", false },
                    { 133, "Diabetic ketoacidosis", "Diabetic ketoacidosis", false },
                    { 134, "DiGeorge syndrome (22q11 deletion)", "DiGeorge syndrome (22q11 deletion)", false },
                    { 135, "Dislocated kneecap", "Dislocated kneecap", false },
                    { 136, "Dislocated shoulder", "Dislocated shoulder", false },
                    { 137, "Differences in sex development", "Differences in sex development", false },
                    { 138, "intersex", "intersex", false },
                    { 139, "Dissociative disorders", "Dissociative disorders", false },
                    { 140, "Diverticular disease and diverticulitis", "Diverticular disease and diverticulitis", false },
                    { 141, "Dizziness", "Dizziness", false },
                    { 142, "Dry mouth", "Dry mouth", false },
                    { 143, "Dysarthria (difficulty speaking)", "Dysarthria (difficulty speaking)", false },
                    { 144, "Dysentery", "Dysentery", false },
                    { 145, "Earache", "Earache", false },
                    { 146, "Early or delayed puberty", "Early or delayed puberty", false },
                    { 147, "Puberty (early or delayed)", "Puberty (early or delayed)", false },
                    { 148, "Earwax build-up", "Earwax build-up", false },
                    { 149, "Eating disorders", "Eating disorders", false },
                    { 150, "Ebola virus disease", "Ebola virus disease", false },
                    { 151, "Echocardiogram", "Echocardiogram", false },
                    { 152, "Ectropion", "Ectropion", false },
                    { 153, "Edwards' syndrome (trisomy 18)", "Edwards' syndrome (trisomy 18)", false },
                    { 154, "Ehlers-Danlos syndromes", "Ehlers-Danlos syndromes", false },
                    { 155, "Ejaculation problems", "Ejaculation problems", false },
                    { 156, "Premature ejaculation", "Premature ejaculation", false },
                    { 157, "Elbow and arm pain", "Elbow and arm pain", false },
                    { 158, "Electrocardiogram (ECG)", "Electrocardiogram (ECG)", false },
                    { 159, "Electroencephalogram (EEG)", "Electroencephalogram (EEG)", false },
                    { 160, "Electrolyte test", "Electrolyte test", false },
                    { 161, "Embolism", "Embolism", false },
                    { 162, "Emollients", "Emollients", false },
                    { 163, "Empyema", "Empyema", false },
                    { 164, "Endoscopy", "Endoscopy", false },
                    { 165, "Enhanced recovery", "Enhanced recovery", false },
                    { 166, "Epididymitis", "Epididymitis", false },
                    { 167, "Epiglottitis", "Epiglottitis", false },
                    { 168, "Erythema multiforme", "Erythema multiforme", false },
                    { 169, "Erythema nodosum", "Erythema nodosum", false },
                    { 170, "Erythromelalgia", "Erythromelalgia", false },
                    { 171, "Euthanasia and assisted suicide", "Euthanasia and assisted suicide", false },
                    { 172, "Ewing sarcoma", "Ewing sarcoma", false },
                    { 173, "Excessive daytime sleepiness (hypersomnia)", "Excessive daytime sleepiness (hypersomnia)", false },
                    { 174, "Hypersomnia", "Hypersomnia", false },
                    { 175, "Eye cancer", "Eye cancer", false },
                    { 176, "Eye injuries", "Eye injuries", false },
                    { 177, "Eyelid problems", "Eyelid problems", false },
                    { 178, "Eye tests for children", "Eye tests for children", false },
                    { 179, "Prosopagnosia (face blindness)", "Prosopagnosia (face blindness)", false },
                    { 180, "Face blindness", "Face blindness", false },
                    { 181, "Febrile seizures", "Febrile seizures", false },
                    { 182, "Fits (children with fever)", "Fits (children with fever)", false },
                    { 183, "Seizures (children with fever)", "Seizures (children with fever)", false },
                    { 184, "Female genital mutilation (FGM)", "Female genital mutilation (FGM)", false },
                    { 185, "High temperature (fever) in children", "High temperature (fever) in children", false },
                    { 186, "Fever in children", "Fever in children", false },
                    { 187, "Flat feet", "Flat feet", false },
                    { 188, "Fluoride", "Fluoride", false },
                    { 189, "Foetal alcohol spectrum disorder", "Foetal alcohol spectrum disorder", false },
                    { 190, "Food colours and hyperactivity", "Food colours and hyperactivity", false },
                    { 191, "Food intolerance", "Food intolerance", false },
                    { 192, "Foot drop", "Foot drop", false },
                    { 193, "Gallbladder cancer", "Gallbladder cancer", false },
                    { 194, "Ganglion cyst", "Ganglion cyst", false },
                    { 195, "Gastritis", "Gastritis", false },
                    { 196, "Gastroparesis", "Gastroparesis", false },
                    { 197, "General anaesthesia", "General anaesthesia", false },
                    { 198, "Gilbert's syndrome", "Gilbert's syndrome", false },
                    { 199, "Glutaric aciduria type 1", "Glutaric aciduria type 1", false },
                    { 200, "Granuloma annulare", "Granuloma annulare", false },
                    { 201, "Granulomatosis with polyangiitis", "Granulomatosis with polyangiitis", false },
                    { 202, "Growing pains", "Growing pains", false },
                    { 203, "Hair dye reactions", "Hair dye reactions", false },
                    { 204, "Hairy cell leukaemia", "Hairy cell leukaemia", false },
                    { 205, "Leukaemia (hairy cell)", "Leukaemia (hairy cell)", false },
                    { 206, "Hallucinations and hearing voices", "Hallucinations and hearing voices", false },
                    { 207, "Hearing voices", "Hearing voices", false },
                    { 208, "Hamstring injury", "Hamstring injury", false },
                    { 209, "Hand foot and mouth disease", "Hand foot and mouth disease", false },
                    { 210, "Head and neck cancer", "Head and neck cancer", false },
                    { 211, "Health anxiety", "Health anxiety", false },
                    { 212, "Hypochondria", "Hypochondria", false },
                    { 213, "Hearing tests for children", "Hearing tests for children", false },
                    { 214, "Heart-lung transplant", "Heart-lung transplant", false },
                    { 215, "Heart palpitations and ectopic beats", "Heart palpitations and ectopic beats", false },
                    { 216, "Palpitations", "Palpitations", false },
                    { 217, "Ectopic beats", "Ectopic beats", false },
                    { 218, "Heat exhaustion and heatstroke", "Heat exhaustion and heatstroke", false },
                    { 219, "Heat rash (prickly heat)", "Heat rash (prickly heat)", false },
                    { 220, "Prickly heat", "Prickly heat", false },
                    { 221, "Sweating (excessive)", "Sweating (excessive)", false },
                    { 222, "sweat rash", "sweat rash", false },
                    { 223, "Henoch-Schönlein purpura (HSP)", "Henoch-Schönlein purpura (HSP)", false },
                    { 224, "Hepatitis", "Hepatitis", false },
                    { 225, "Herbal medicines", "Herbal medicines", false },
                    { 226, "Herceptin (trastuzumab)", "Herceptin (trastuzumab)", false },
                    { 227, "Hereditary haemorrhagic telangiectasia (HHT)", "Hereditary haemorrhagic telangiectasia (HHT)", false },
                    { 228, "Hereditary neuropathy with pressure palsies (HNPP)", "Hereditary neuropathy with pressure palsies (HNPP)", false },
                    { 229, "Hereditary spastic paraplegia", "Hereditary spastic paraplegia", false },
                    { 230, "Hernia", "Hernia", false },
                    { 231, "Herpes simplex eye infections", "Herpes simplex eye infections", false },
                    { 232, "Eye infection (herpes)", "Eye infection (herpes)", false },
                    { 233, "Herpetic whitlow (whitlow finger)", "Herpetic whitlow (whitlow finger)", false },
                    { 234, "Whitlow finger", "Whitlow finger", false },
                    { 235, "Haemophilus influenzae type b (Hib)", "Haemophilus influenzae type b (Hib)", false },
                    { 236, "Hidradenitis suppurativa (HS)", "Hidradenitis suppurativa (HS)", false },
                    { 237, "Hyperglycaemia (high blood sugar)", "Hyperglycaemia (high blood sugar)", false },
                    { 238, "Hip pain in adults", "Hip pain in adults", false },
                    { 239, "Hirschsprung's disease", "Hirschsprung's disease", false },
                    { 240, "Hoarding disorder", "Hoarding disorder", false },
                    { 241, "Homeopathy", "Homeopathy", false },
                    { 242, "Home oxygen therapy", "Home oxygen therapy", false },
                    { 243, "Oxygen therapy", "Oxygen therapy", false },
                    { 244, "Homocystinuria", "Homocystinuria", false },
                    { 245, "Noise sensitivity (hyperacusis)", "Noise sensitivity (hyperacusis)", false },
                    { 246, "Hypnotherapy", "Hypnotherapy", false },
                    { 247, "Hypothermia", "Hypothermia", false },
                    { 248, "Ichthyosis", "Ichthyosis", false },
                    { 249, "Indigestion", "Indigestion", false },
                    { 250, "Inflammatory bowel disease", "Inflammatory bowel disease", false },
                    { 251, "Ingrown hairs", "Ingrown hairs", false },
                    { 252, "Ingrown toenail", "Ingrown toenail", false },
                    { 253, "Intensive care", "Intensive care", false },
                    { 254, "Interstitial cystitis", "Interstitial cystitis", false },
                    { 255, "Intracranial hypertension", "Intracranial hypertension", false },
                    { 256, "Hip pain in children (irritable hip)", "Hip pain in children (irritable hip)", false },
                    { 257, "Irritable hip", "Irritable hip", false },
                    { 258, "Isovaleric acidaemia", "Isovaleric acidaemia", false },
                    { 259, "Joint pain", "Joint pain", false },
                    { 260, "Kaposi's sarcoma", "Kaposi's sarcoma", false },
                    { 261, "Keratosis pilaris", "Keratosis pilaris", false },
                    { 262, "Klinefelter syndrome", "Klinefelter syndrome", false },
                    { 263, "Knee pain", "Knee pain", false },
                    { 264, "Knock knees", "Knock knees", false },
                    { 265, "Kwashiorkor", "Kwashiorkor", false },
                    { 266, "Labial fusion", "Labial fusion", false },
                    { 267, "Lambert-Eaton myasthenic syndrome", "Lambert-Eaton myasthenic syndrome", false },
                    { 268, "Lactate dehydrogenase (LDH) test", "Lactate dehydrogenase (LDH) test", false },
                    { 269, "Legionnaires' disease", "Legionnaires' disease", false },
                    { 270, "Lichen sclerosus", "Lichen sclerosus", false },
                    { 271, "Limping in children", "Limping in children", false },
                    { 272, "Lipoedema", "Lipoedema", false },
                    { 273, "Lipoma", "Lipoma", false },
                    { 274, "Liver disease", "Liver disease", false },
                    { 275, "Local anaesthesia", "Local anaesthesia", false },
                    { 276, "Long QT syndrome", "Long QT syndrome", false },
                    { 277, "Loss of libido (reduced sex drive)", "Loss of libido (reduced sex drive)", false },
                    { 278, "Low blood sugar (hypoglycaemia)", "Low blood sugar (hypoglycaemia)", false },
                    { 279, "Hypoglycaemia (low blood sugar)", "Hypoglycaemia (low blood sugar)", false },
                    { 280, "Low sperm count", "Low sperm count", false },
                    { 281, "Sperm count (low)", "Sperm count (low)", false },
                    { 282, "Lumps", "Lumps", false },
                    { 283, "Lyme disease", "Lyme disease", false },
                    { 284, "Macular hole", "Macular hole", false },
                    { 285, "Magnesium test", "Magnesium test", false },
                    { 286, "The 'male menopause'", "The 'male menopause'", false },
                    { 287, "Male menopause", "Male menopause", false },
                    { 288, "Mallet finger", "Mallet finger", false },
                    { 289, "Maple syrup urine disease", "Maple syrup urine disease", false },
                    { 290, "Mastoiditis", "Mastoiditis", false },
                    { 291, "MCADD", "MCADD", false },
                    { 292, "Medically unexplained symptoms", "Medically unexplained symptoms", false },
                    { 293, "Functional neurological disorder", "Functional neurological disorder", false },
                    { 294, "Ménière's disease", "Ménière's disease", false },
                    { 295, "Mesothelioma", "Mesothelioma", false },
                    { 296, "Metabolic syndrome", "Metabolic syndrome", false },
                    { 297, "Metallic taste", "Metallic taste", false },
                    { 298, "Mitral valve problems", "Mitral valve problems", false },
                    { 299, "Heart valve problems", "Heart valve problems", false },
                    { 300, "Molar pregnancy", "Molar pregnancy", false },
                    { 301, "Morton's neuroma", "Morton's neuroma", false },
                    { 302, "Motion sickness", "Motion sickness", false },
                    { 303, "Mouth ulcers", "Mouth ulcers", false },
                    { 304, "MRSA", "MRSA", false },
                    { 305, "Multiple system atrophy", "Multiple system atrophy", false },
                    { 306, "Mycobacterium chimaera infection", "Mycobacterium chimaera infection", false },
                    { 307, "Myelodysplastic syndrome (myelodysplasia)", "Myelodysplastic syndrome (myelodysplasia)", false },
                    { 308, "Myositis (polymyositis and dermatomyositis)", "Myositis (polymyositis and dermatomyositis)", false },
                    { 309, "Nail patella syndrome", "Nail patella syndrome", false },
                    { 310, "Nail problems", "Nail problems", false },
                    { 311, "Nasal and sinus cancer", "Nasal and sinus cancer", false },
                    { 312, "Nose cancer", "Nose cancer", false },
                    { 313, "Sinus cancer", "Sinus cancer", false },
                    { 314, "Nasopharyngeal cancer", "Nasopharyngeal cancer", false },
                    { 315, "Neck pain", "Neck pain", false },
                    { 316, "Necrotising fasciitis", "Necrotising fasciitis", false },
                    { 317, "Neonatal herpes (herpes in a baby)", "Neonatal herpes (herpes in a baby)", false },
                    { 318, "Herpes in babies", "Herpes in babies", false },
                    { 319, "Nephrotic syndrome in children", "Nephrotic syndrome in children", false },
                    { 320, "Neuroblastoma", "Neuroblastoma", false },
                    { 321, "Neuroendocrine tumours", "Neuroendocrine tumours", false },
                    { 322, "Neuromyelitis optica", "Neuromyelitis optica", false },
                    { 323, "Night sweats", "Night sweats", false },
                    { 324, "Sweating at night", "Sweating at night", false },
                    { 325, "Night terrors and nightmares", "Night terrors and nightmares", false },
                    { 326, "Nipple discharge", "Nipple discharge", false },
                    { 327, "Non-alcoholic fatty liver disease (NAFLD)", "Non-alcoholic fatty liver disease (NAFLD)", false },
                    { 328, "Norovirus (vomiting bug)", "Norovirus (vomiting bug)", false },
                    { 329, "Vomiting bug", "Vomiting bug", false },
                    { 330, "Winter vomiting bug", "Winter vomiting bug", false },
                    { 331, "NSAIDs", "NSAIDs", false },
                    { 332, "Swollen ankles feet and legs (oedema)", "Swollen ankles feet and legs (oedema)", false },
                    { 333, "Oesophageal atresia and tracheo-oesophageal fistula", "Oesophageal atresia and tracheo-oesophageal fistula", false },
                    { 334, "Orf", "Orf", false },
                    { 335, "Osteophyte (bone spur)", "Osteophyte (bone spur)", false },
                    { 336, "Otosclerosis", "Otosclerosis", false },
                    { 337, "Ovulation pain", "Ovulation pain", false },
                    { 338, "Panic disorder", "Panic disorder", false },
                    { 339, "Patau's syndrome", "Patau's syndrome", false },
                    { 340, "Peak flow test", "Peak flow test", false },
                    { 341, "Pelvic pain", "Pelvic pain", false },
                    { 342, "Penile cancer", "Penile cancer", false },
                    { 343, "Period pain", "Period pain", false },
                    { 344, "Menstrual pain", "Menstrual pain", false },
                    { 345, "Periods", "Periods", false },
                    { 346, "Persistent trophoblastic disease and choriocarcinoma", "Persistent trophoblastic disease and choriocarcinoma", false },
                    { 347, "Personality disorder", "Personality disorder", false },
                    { 348, "PET scan", "PET scan", false },
                    { 349, "Phaeochromocytoma", "Phaeochromocytoma", false },
                    { 350, "Phenylketonuria", "Phenylketonuria", false },
                    { 351, "Tight foreskin (phimosis and paraphimosis)", "Tight foreskin (phimosis and paraphimosis)", false },
                    { 352, "Foreskin problems", "Foreskin problems", false },
                    { 353, "Phimosis", "Phimosis", false },
                    { 354, "Phlebitis (superficial thrombophlebitis)", "Phlebitis (superficial thrombophlebitis)", false },
                    { 355, "Superficial thrombophlebitis", "Superficial thrombophlebitis", false },
                    { 356, "Phosphate test", "Phosphate test", false },
                    { 357, "Photodynamic therapy (PDT)", "Photodynamic therapy (PDT)", false },
                    { 358, "Pins and needles", "Pins and needles", false },
                    { 359, "PIP breast implants", "PIP breast implants", false },
                    { 360, "Pityriasis rosea", "Pityriasis rosea", false },
                    { 361, "Pityriasis versicolor", "Pityriasis versicolor", false },
                    { 362, "Plagiocephaly and brachycephaly (flat head syndrome)", "Plagiocephaly and brachycephaly (flat head syndrome)", false },
                    { 363, "Brachycephaly and plagiocephaly", "Brachycephaly and plagiocephaly", false },
                    { 364, "Flat head syndrome", "Flat head syndrome", false },
                    { 365, "Pleurisy", "Pleurisy", false },
                    { 366, "Polio", "Polio", false },
                    { 367, "Polyhydramnios (too much amniotic fluid)", "Polyhydramnios (too much amniotic fluid)", false },
                    { 368, "Polymorphic light eruption", "Polymorphic light eruption", false },
                    { 369, "Pompholyx (dyshidrotic eczema)", "Pompholyx (dyshidrotic eczema)", false },
                    { 370, "Postmenopausal bleeding", "Postmenopausal bleeding", false },
                    { 371, "Bleeding after the menopause", "Bleeding after the menopause", false },
                    { 372, "Post-mortem", "Post-mortem", false },
                    { 373, "Postpartum psychosis", "Postpartum psychosis", false },
                    { 374, "Postural tachycardia syndrome (PoTS)", "Postural tachycardia syndrome (PoTS)", false },
                    { 375, "Potassium test", "Potassium test", false },
                    { 376, "Predictive genetic tests for cancer risk genes", "Predictive genetic tests for cancer risk genes", false },
                    { 377, "Genetic test for cancer gene", "Genetic test for cancer gene", false },
                    { 378, "Probiotics", "Probiotics", false },
                    { 379, "Problems swallowing pills", "Problems swallowing pills", false },
                    { 380, "Swallowing pills", "Swallowing pills", false },
                    { 381, "Prostate problems", "Prostate problems", false },
                    { 382, "Prostatitis", "Prostatitis", false },
                    { 383, "Psoriatic arthritis", "Psoriatic arthritis", false },
                    { 384, "Psychiatry", "Psychiatry", false },
                    { 385, "Pubic lice", "Pubic lice", false },
                    { 386, "Pudendal neuralgia", "Pudendal neuralgia", false },
                    { 387, "Pyoderma gangrenosum", "Pyoderma gangrenosum", false },
                    { 388, "Q fever", "Q fever", false },
                    { 389, "Rashes in babies and children", "Rashes in babies and children", false },
                    { 390, "Red blood cell count", "Red blood cell count", false },
                    { 391, "Red eye", "Red eye", false },
                    { 392, "Reflux in babies", "Reflux in babies", false },
                    { 393, "Acid reflux in babies", "Acid reflux in babies", false },
                    { 394, "Respiratory tract infections (RTIs)", "Respiratory tract infections (RTIs)", false },
                    { 395, "Retinal migraine", "Retinal migraine", false },
                    { 396, "Retinoblastoma (eye cancer in children)", "Retinoblastoma (eye cancer in children)", false },
                    { 397, "Rett syndrome", "Rett syndrome", false },
                    { 398, "Reye's syndrome", "Reye's syndrome", false },
                    { 399, "Roseola", "Roseola", false },
                    { 400, "Salivary gland stones", "Salivary gland stones", false },
                    { 401, "Sarcoidosis", "Sarcoidosis", false },
                    { 402, "SARS (severe acute respiratory syndrome)", "SARS (severe acute respiratory syndrome)", false },
                    { 403, "Scarlet fever", "Scarlet fever", false },
                    { 404, "Schistosomiasis (bilharzia)", "Schistosomiasis (bilharzia)", false },
                    { 405, "Bilharzia", "Bilharzia", false },
                    { 406, "Scleroderma", "Scleroderma", false },
                    { 407, "Selective mutism", "Selective mutism", false },
                    { 408, "Septic arthritis", "Septic arthritis", false },
                    { 409, "Sexually transmitted infections (STIs)", "Sexually transmitted infections (STIs)", false },
                    { 410, "Shin splints", "Shin splints", false },
                    { 411, "Shin pain (shin splints)", "Shin pain (shin splints)", false },
                    { 412, "Shortness of breath", "Shortness of breath", false },
                    { 413, "Shoulder impingement", "Shoulder impingement", false },
                    { 414, "Sick building syndrome", "Sick building syndrome", false },
                    { 415, "Silicosis", "Silicosis", false },
                    { 416, "Skin cyst", "Skin cyst", false },
                    { 417, "Skin tags", "Skin tags", false },
                    { 418, "Slapped cheek syndrome", "Slapped cheek syndrome", false },
                    { 419, "Sleepwalking", "Sleepwalking", false },
                    { 420, "Smelly urine", "Smelly urine", false },
                    { 421, "Urine (smelly)", "Urine (smelly)", false },
                    { 422, "Snake bites", "Snake bites", false },
                    { 423, "Social anxiety (social phobia)", "Social anxiety (social phobia)", false },
                    { 424, "Soft tissue sarcomas", "Soft tissue sarcomas", false },
                    { 425, "Sore or dry lips", "Sore or dry lips", false },
                    { 426, "Sore lips", "Sore lips", false },
                    { 427, "Dry lips", "Dry lips", false },
                    { 428, "Lips (sore or dry)", "Lips (sore or dry)", false },
                    { 429, "Sore or white tongue", "Sore or white tongue", false },
                    { 430, "Tongue (sore or white)", "Tongue (sore or white)", false },
                    { 431, "Sore throat", "Sore throat", false },
                    { 432, "Throat (sore)", "Throat (sore)", false },
                    { 433, "Spirometry", "Spirometry", false },
                    { 434, "Spleen problems and spleen removal", "Spleen problems and spleen removal", false },
                    { 435, "Spondylolisthesis", "Spondylolisthesis", false },
                    { 436, "Staph infection", "Staph infection", false },
                    { 437, "Steroid inhalers", "Steroid inhalers", false },
                    { 438, "Steroid injections", "Steroid injections", false },
                    { 439, "Steroid nasal sprays", "Steroid nasal sprays", false },
                    { 440, "Steroids", "Steroids", false },
                    { 441, "Corticosteroids", "Corticosteroids", false },
                    { 442, "Steroid tablets", "Steroid tablets", false },
                    { 443, "Stevens-Johnson syndrome", "Stevens-Johnson syndrome", false },
                    { 444, "Stomach ache", "Stomach ache", false },
                    { 445, "Tummy ache", "Tummy ache", false },
                    { 446, "Stopped or missed periods", "Stopped or missed periods", false },
                    { 447, "Periods (stopped or missed)", "Periods (stopped or missed)", false },
                    { 448, "Stop smoking treatments", "Stop smoking treatments", false },
                    { 449, "Smoking (treatments to stop)", "Smoking (treatments to stop)", false },
                    { 450, "Stretch marks", "Stretch marks", false },
                    { 451, "Stye", "Stye", false },
                    { 452, "Sudden infant death syndrome (SIDS)", "Sudden infant death syndrome (SIDS)", false },
                    { 453, "Sunburn", "Sunburn", false },
                    { 454, "Swine flu (H1N1)", "Swine flu (H1N1)", false },
                    { 455, "Swollen glands", "Swollen glands", false },
                    { 456, "Temporomandibular disorder (TMD)", "Temporomandibular disorder (TMD)", false },
                    { 457, "Jaw pain", "Jaw pain", false },
                    { 458, "Tension-type headaches", "Tension-type headaches", false },
                    { 459, "Headaches (tension-type)", "Headaches (tension-type)", false },
                    { 460, "Tetanus", "Tetanus", false },
                    { 461, "Excessive thirst", "Excessive thirst", false },
                    { 462, "Thirst (excessive)", "Thirst (excessive)", false },
                    { 463, "Thrombophilia", "Thrombophilia", false },
                    { 464, "Thyroiditis", "Thyroiditis", false },
                    { 465, "Total iron-binding capacity (TIBC) and transferrin test", "Total iron-binding capacity (TIBC) and transferrin test", false },
                    { 466, "Tongue-tie", "Tongue-tie", false },
                    { 467, "Toothache", "Toothache", false },
                    { 468, "Dental pain", "Dental pain", false },
                    { 469, "Tooth decay", "Tooth decay", false },
                    { 470, "Topical corticosteroids", "Topical corticosteroids", false },
                    { 471, "Steroid cream", "Steroid cream", false },
                    { 472, "Corticosteroid cream", "Corticosteroid cream", false },
                    { 473, "Total protein test", "Total protein test", false },
                    { 474, "Toxic shock syndrome", "Toxic shock syndrome", false },
                    { 475, "TENS (transcutaneous electrical nerve stimulation)", "TENS (transcutaneous electrical nerve stimulation)", false },
                    { 476, "Trimethylaminuria ('fish odour syndrome')", "Trimethylaminuria ('fish odour syndrome')", false },
                    { 477, "Typhus", "Typhus", false },
                    { 478, "Ultrasound scan", "Ultrasound scan", false },
                    { 479, "Unintentional weight loss", "Unintentional weight loss", false },
                    { 480, "Weight loss (unintentional)", "Weight loss (unintentional)", false },
                    { 481, "Weight loss (unexpected)", "Weight loss (unexpected)", false },
                    { 482, "Urinary tract infections (UTIs)", "Urinary tract infections (UTIs)", false },
                    { 483, "Vaginal discharge", "Vaginal discharge", false },
                    { 484, "Vaginal dryness", "Vaginal dryness", false },
                    { 485, "Vaginitis", "Vaginitis", false },
                    { 486, "Vasculitis", "Vasculitis", false },
                    { 487, "Blindness and vision loss", "Blindness and vision loss", false },
                    { 488, "Vomiting blood (haematemesis)", "Vomiting blood (haematemesis)", false },
                    { 489, "Von Willebrand disease", "Von Willebrand disease", false },
                    { 490, "Vulvodynia (vulval pain)", "Vulvodynia (vulval pain)", false },
                    { 491, "Vaginal pain", "Vaginal pain", false },
                    { 492, "Warts and verrucas", "Warts and verrucas", false },
                    { 493, "West Nile virus", "West Nile virus", false },
                    { 494, "Whooping cough", "Whooping cough", false },
                    { 495, "Wolff-Parkinson-White syndrome", "Wolff-Parkinson-White syndrome", false },
                    { 496, "X-ray", "X-ray", false },
                    { 497, "Zika virus", "Zika virus", false },
                    { 498, "Abdominal aortic aneurysm", "Abdominal aortic aneurysm", false },
                    { 499, "AAA", "AAA", false },
                    { 500, "Aneurysm (abdominal aortic)", "Aneurysm (abdominal aortic)", false },
                    { 501, "Abdominal aortic aneurysm screening", "Abdominal aortic aneurysm screening", false },
                    { 502, "AAA screening", "AAA screening", false },
                    { 503, "Abscess", "Abscess", false },
                    { 504, "Acne", "Acne", false },
                    { 505, "Actinomycosis", "Actinomycosis", false },
                    { 506, "Acute lymphoblastic leukaemia", "Acute lymphoblastic leukaemia", false },
                    { 507, "Leukaemia (acute lymphoblastic)", "Leukaemia (acute lymphoblastic)", false },
                    { 508, "Acute myeloid leukaemia", "Acute myeloid leukaemia", false },
                    { 509, "Leukaemia (acute myeloid)", "Leukaemia (acute myeloid)", false },
                    { 510, "Acute pancreatitis", "Acute pancreatitis", false },
                    { 511, "Pancreatitis (acute)", "Pancreatitis (acute)", false },
                    { 512, "Addison's disease", "Addison's disease", false },
                    { 513, "Agoraphobia", "Agoraphobia", false },
                    { 514, "Albinism", "Albinism", false },
                    { 515, "Alcohol misuse", "Alcohol misuse", false },
                    { 516, "Alcohol-related liver disease", "Alcohol-related liver disease", false },
                    { 517, "Liver disease (alcohol-related)", "Liver disease (alcohol-related)", false },
                    { 518, "Allergic rhinitis", "Allergic rhinitis", false },
                    { 519, "Rhinitis (allergic)", "Rhinitis (allergic)", false },
                    { 520, "Allergies", "Allergies", false },
                    { 521, "Altitude sickness", "Altitude sickness", false },
                    { 522, "Alzheimer's disease", "Alzheimer's disease", false },
                    { 523, "Amniocentesis", "Amniocentesis", false },
                    { 524, "Anal fissure", "Anal fissure", false },
                    { 525, "Anal fistula", "Anal fistula", false },
                    { 526, "Anaphylaxis", "Anaphylaxis", false },
                    { 527, "Androgen insensitivity syndrome", "Androgen insensitivity syndrome", false },
                    { 528, "Angina", "Angina", false },
                    { 529, "Angioedema", "Angioedema", false },
                    { 530, "Angiography", "Angiography", false },
                    { 531, "Ankylosing spondylitis", "Ankylosing spondylitis", false },
                    { 532, "Anorexia nervosa", "Anorexia nervosa", false },
                    { 533, "Antibiotics", "Antibiotics", false },
                    { 534, "Anticoagulant medicines", "Anticoagulant medicines", false },
                    { 535, "Antidepressants", "Antidepressants", false },
                    { 536, "Antiphospholipid syndrome (APS)", "Antiphospholipid syndrome (APS)", false },
                    { 537, "Hughes syndrome", "Hughes syndrome", false },
                    { 538, "Aortic valve replacement", "Aortic valve replacement", false },
                    { 539, "Heart valve replacement", "Heart valve replacement", false },
                    { 540, "Aphasia", "Aphasia", false },
                    { 541, "Appendicitis", "Appendicitis", false },
                    { 542, "Arthritis", "Arthritis", false },
                    { 543, "Arthroscopy", "Arthroscopy", false },
                    { 544, "Aspergillosis", "Aspergillosis", false },
                    { 545, "Asthma", "Asthma", false },
                    { 546, "Astigmatism", "Astigmatism", false },
                    { 547, "Ataxia", "Ataxia", false },
                    { 548, "Atopic eczema", "Atopic eczema", false },
                    { 549, "Eczema (atopic)", "Eczema (atopic)", false },
                    { 550, "Atrial fibrillation", "Atrial fibrillation", false },
                    { 551, "Attention deficit hyperactivity disorder (ADHD)", "Attention deficit hyperactivity disorder (ADHD)", false },
                    { 552, "Autosomal dominant polycystic kidney disease", "Autosomal dominant polycystic kidney disease", false },
                    { 553, "Polycystic kidney disease (autosomal dominant)", "Polycystic kidney disease (autosomal dominant)", false },
                    { 554, "Autosomal recessive polycystic kidney disease", "Autosomal recessive polycystic kidney disease", false },
                    { 555, "Polycystic kidney disease (autosomal recessive)", "Polycystic kidney disease (autosomal recessive)", false },
                    { 556, "Back pain", "Back pain", false },
                    { 557, "Bacterial vaginosis", "Bacterial vaginosis", false },
                    { 558, "Bad breath", "Bad breath", false },
                    { 559, "Halitosis", "Halitosis", false },
                    { 560, "Baker's cyst", "Baker's cyst", false },
                    { 561, "Popliteal cyst", "Popliteal cyst", false },
                    { 562, "Bartholin's cyst", "Bartholin's cyst", false },
                    { 563, "Bedwetting in children", "Bedwetting in children", false },
                    { 564, "Behçet's disease", "Behçet's disease", false },
                    { 565, "Bell's palsy", "Bell's palsy", false },
                    { 566, "Benign brain tumour (non-cancerous)", "Benign brain tumour (non-cancerous)", false },
                    { 567, "Brain tumour (benign)", "Brain tumour (benign)", false },
                    { 568, "Bile duct cancer (cholangiocarcinoma)", "Bile duct cancer (cholangiocarcinoma)", false },
                    { 569, "Cholangiocarcinoma", "Cholangiocarcinoma", false },
                    { 570, "Binge eating disorder", "Binge eating disorder", false },
                    { 571, "Biopsy", "Biopsy", false },
                    { 572, "Bipolar disorder", "Bipolar disorder", false },
                    { 573, "Birthmarks", "Birthmarks", false },
                    { 574, "Bladder cancer", "Bladder cancer", false },
                    { 575, "Bladder stones", "Bladder stones", false },
                    { 576, "Bleeding from the bottom (rectal bleeding)", "Bleeding from the bottom (rectal bleeding)", false },
                    { 577, "Rectal bleeding", "Rectal bleeding", false },
                    { 578, "Blepharitis", "Blepharitis", false },
                    { 579, "Blisters", "Blisters", false },
                    { 580, "Blood tests", "Blood tests", false },
                    { 581, "Blushing", "Blushing", false },
                    { 582, "Bone cancer", "Bone cancer", false },
                    { 583, "Bone cyst", "Bone cyst", false },
                    { 584, "Borderline personality disorder", "Borderline personality disorder", false },
                    { 585, "Bowel cancer", "Bowel cancer", false },
                    { 586, "Colon cancer", "Colon cancer", false },
                    { 587, "Rectal cancer", "Rectal cancer", false },
                    { 588, "Bowel cancer screening", "Bowel cancer screening", false },
                    { 589, "Bowel incontinence", "Bowel incontinence", false },
                    { 590, "Brain abscess", "Brain abscess", false },
                    { 591, "Brain aneurysm", "Brain aneurysm", false },
                    { 592, "Aneurysm (brain)", "Aneurysm (brain)", false },
                    { 593, "Brain death", "Brain death", false },
                    { 594, "Breast abscess", "Breast abscess", false },
                    { 595, "Breast cancer in women", "Breast cancer in women", false },
                    { 596, "Breast cancer in men", "Breast cancer in men", false },
                    { 597, "Breast lumps", "Breast lumps", false },
                    { 598, "Bronchiectasis", "Bronchiectasis", false },
                    { 599, "Bronchiolitis", "Bronchiolitis", false },
                    { 600, "Bronchodilators", "Bronchodilators", false },
                    { 601, "Exophthalmos (bulging eyes)", "Exophthalmos (bulging eyes)", false },
                    { 602, "Bulimia", "Bulimia", false },
                    { 603, "Burns and scalds", "Burns and scalds", false },
                    { 604, "Bursitis", "Bursitis", false },
                    { 605, "Caesarean section", "Caesarean section", false },
                    { 606, "Cancer", "Cancer", false },
                    { 607, "Carotid endarterectomy", "Carotid endarterectomy", false },
                    { 608, "Carpal tunnel syndrome", "Carpal tunnel syndrome", false },
                    { 609, "Cartilage damage", "Cartilage damage", false },
                    { 610, "Cataract surgery", "Cataract surgery", false },
                    { 611, "Cavernous sinus thrombosis", "Cavernous sinus thrombosis", false },
                    { 612, "Cellulitis", "Cellulitis", false },
                    { 613, "Cerebral palsy", "Cerebral palsy", false },
                    { 614, "Cervical cancer", "Cervical cancer", false },
                    { 615, "Cervical screening", "Cervical screening", false },
                    { 616, "Smear test", "Smear test", false },
                    { 617, "Cervical spondylosis", "Cervical spondylosis", false },
                    { 618, "Charcot-Marie-Tooth disease", "Charcot-Marie-Tooth disease", false },
                    { 619, "Chemotherapy", "Chemotherapy", false },
                    { 620, "Chickenpox", "Chickenpox", false },
                    { 621, "Childhood cataracts", "Childhood cataracts", false },
                    { 622, "Cataracts (children)", "Cataracts (children)", false },
                    { 623, "Chlamydia", "Chlamydia", false },
                    { 624, "Cholera", "Cholera", false },
                    { 625, "Chorionic villus sampling", "Chorionic villus sampling", false },
                    { 626, "Myalgic encephalomyelitis or chronic fatigue syndrome (ME/CFS)", "Myalgic encephalomyelitis or chronic fatigue syndrome (ME/CFS)", false },
                    { 627, "Chronic fatigue syndrome (ME/CFS)", "Chronic fatigue syndrome (ME/CFS)", false },
                    { 628, "Chronic lymphocytic leukaemia", "Chronic lymphocytic leukaemia", false },
                    { 629, "Leukaemia (chronic lymphocytic)", "Leukaemia (chronic lymphocytic)", false },
                    { 630, "Chronic myeloid leukaemia", "Chronic myeloid leukaemia", false },
                    { 631, "Leukaemia (chronic myeloid)", "Leukaemia (chronic myeloid)", false },
                    { 632, "Chronic obstructive pulmonary disease (COPD)", "Chronic obstructive pulmonary disease (COPD)", false },
                    { 633, "Chronic pancreatitis", "Chronic pancreatitis", false },
                    { 634, "Pancreatitis (chronic)", "Pancreatitis (chronic)", false },
                    { 635, "Cirrhosis", "Cirrhosis", false },
                    { 636, "Cleft lip and palate", "Cleft lip and palate", false },
                    { 637, "Clinical depression", "Clinical depression", false },
                    { 638, "Depression", "Depression", false },
                    { 639, "Clinical trials", "Clinical trials", false },
                    { 640, "Club foot", "Club foot", false },
                    { 641, "Coeliac disease", "Coeliac disease", false },
                    { 642, "Cognitive behavioural therapy (CBT)", "Cognitive behavioural therapy (CBT)", false },
                    { 643, "Colic", "Colic", false },
                    { 644, "Colostomy", "Colostomy", false },
                    { 645, "Colposcopy", "Colposcopy", false },
                    { 646, "Common cold", "Common cold", false },
                    { 647, "Complex regional pain syndrome", "Complex regional pain syndrome", false },
                    { 648, "Congenital heart disease", "Congenital heart disease", false },
                    { 649, "Conjunctivitis", "Conjunctivitis", false },
                    { 650, "Consent to treatment", "Consent to treatment", false },
                    { 651, "Constipation", "Constipation", false },
                    { 652, "Contact dermatitis", "Contact dermatitis", false },
                    { 653, "Eczema (contact dermatitis)", "Eczema (contact dermatitis)", false },
                    { 654, "Cornea transplant", "Cornea transplant", false },
                    { 655, "Corns and calluses", "Corns and calluses", false },
                    { 656, "Cardiac catheterisation and coronary angiography", "Cardiac catheterisation and coronary angiography", false },
                    { 657, "Coronary angioplasty and stent insertion", "Coronary angioplasty and stent insertion", false },
                    { 658, "Angioplasty", "Angioplasty", false },
                    { 659, "Stent insertion", "Stent insertion", false },
                    { 660, "Coronary artery bypass graft", "Coronary artery bypass graft", false },
                    { 661, "Heart bypass", "Heart bypass", false },
                    { 662, "CABG", "CABG", false },
                    { 663, "Coronary heart disease", "Coronary heart disease", false },
                    { 664, "Heart disease (coronary)", "Heart disease (coronary)", false },
                    { 665, "Corticobasal degeneration", "Corticobasal degeneration", false },
                    { 666, "Counselling", "Counselling", false },
                    { 667, "Craniosynostosis", "Craniosynostosis", false },
                    { 668, "Creutzfeldt-Jakob disease", "Creutzfeldt-Jakob disease", false },
                    { 669, "CJD", "CJD", false },
                    { 670, "Crohn's disease", "Crohn's disease", false },
                    { 671, "Croup", "Croup", false },
                    { 672, "Cushing's syndrome", "Cushing's syndrome", false },
                    { 673, "Cystic fibrosis", "Cystic fibrosis", false },
                    { 674, "Cystitis", "Cystitis", false },
                    { 675, "Cystoscopy", "Cystoscopy", false },
                    { 676, "Cytomegalovirus (CMV)", "Cytomegalovirus (CMV)", false },
                    { 677, "Deafblindness", "Deafblindness", false },
                    { 678, "DVT (deep vein thrombosis)", "DVT (deep vein thrombosis)", false },
                    { 679, "DVT", "DVT", false },
                    { 680, "Dehydration", "Dehydration", false },
                    { 681, "Dementia with Lewy bodies", "Dementia with Lewy bodies", false },
                    { 682, "Dengue", "Dengue", false },
                    { 683, "Developmental co-ordination disorder (dyspraxia) in children", "Developmental co-ordination disorder (dyspraxia) in children", false },
                    { 684, "Dyspraxia in children", "Dyspraxia in children", false },
                    { 685, "Bone density scan (DEXA scan)", "Bone density scan (DEXA scan)", false },
                    { 686, "DEXA scan", "DEXA scan", false },
                    { 687, "Diabetes insipidus", "Diabetes insipidus", false },
                    { 688, "Diabetic retinopathy", "Diabetic retinopathy", false },
                    { 689, "Dialysis", "Dialysis", false },
                    { 690, "Diphtheria", "Diphtheria", false },
                    { 691, "Discoid eczema", "Discoid eczema", false },
                    { 692, "Eczema (discoid)", "Eczema (discoid)", false },
                    { 693, "Disorders of consciousness", "Disorders of consciousness", false },
                    { 694, "Vegetative state", "Vegetative state", false },
                    { 695, "Double vision", "Double vision", false },
                    { 696, "Down's syndrome", "Down's syndrome", false },
                    { 697, "Dry eyes", "Dry eyes", false },
                    { 698, "Dupuytren's contracture", "Dupuytren's contracture", false },
                    { 699, "Dyslexia", "Dyslexia", false },
                    { 700, "Dystonia", "Dystonia", false },
                    { 701, "Ectopic pregnancy", "Ectopic pregnancy", false },
                    { 702, "Encephalitis", "Encephalitis", false },
                    { 703, "Endocarditis", "Endocarditis", false },
                    { 704, "Endometriosis", "Endometriosis", false },
                    { 705, "Epidermolysis bullosa", "Epidermolysis bullosa", false },
                    { 706, "Epidural", "Epidural", false },
                    { 707, "Epilepsy", "Epilepsy", false },
                    { 708, "Erectile dysfunction (impotence)", "Erectile dysfunction (impotence)", false },
                    { 709, "Impotence", "Impotence", false },
                    { 710, "Excessive sweating (hyperhidrosis)", "Excessive sweating (hyperhidrosis)", false },
                    { 711, "Hyperhidrosis", "Hyperhidrosis", false },
                    { 712, "Fabricated or induced illness", "Fabricated or induced illness", false },
                    { 713, "Fainting", "Fainting", false },
                    { 714, "Falls", "Falls", false },
                    { 715, "Femoral hernia repair", "Femoral hernia repair", false },
                    { 716, "Hernia (femoral)", "Hernia (femoral)", false },
                    { 717, "Fibroids", "Fibroids", false },
                    { 718, "Fibromyalgia", "Fibromyalgia", false },
                    { 719, "First aid", "First aid", false },
                    { 720, "Farting (flatulence)", "Farting (flatulence)", false },
                    { 721, "Flatulence", "Flatulence", false },
                    { 722, "Wind", "Wind", false },
                    { 723, "Flu", "Flu", false },
                    { 724, "Influenza", "Influenza", false },
                    { 725, "Food allergy", "Food allergy", false },
                    { 726, "Food poisoning", "Food poisoning", false },
                    { 727, "Frontotemporal dementia", "Frontotemporal dementia", false },
                    { 728, "Dementia (frontotemporal)", "Dementia (frontotemporal)", false },
                    { 729, "Frostbite", "Frostbite", false },
                    { 730, "Frozen shoulder", "Frozen shoulder", false },
                    { 731, "Fungal nail infection", "Fungal nail infection", false },
                    { 732, "Nail fungal infection", "Nail fungal infection", false },
                    { 733, "Gallbladder removal", "Gallbladder removal", false },
                    { 734, "Gallstones", "Gallstones", false },
                    { 735, "Gangrene", "Gangrene", false },
                    { 736, "Gastrectomy", "Gastrectomy", false },
                    { 737, "Gastroscopy", "Gastroscopy", false },
                    { 738, "Gender dysphoria", "Gender dysphoria", false },
                    { 739, "Generalised anxiety disorder in adults", "Generalised anxiety disorder in adults", false },
                    { 740, "Anxiety disorder in adults", "Anxiety disorder in adults", false },
                    { 741, "Genital herpes", "Genital herpes", false },
                    { 742, "Herpes (genital)", "Herpes (genital)", false },
                    { 743, "Genital warts", "Genital warts", false },
                    { 744, "Gestational diabetes", "Gestational diabetes", false },
                    { 745, "Diabetes in pregnancy", "Diabetes in pregnancy", false },
                    { 746, "Giardiasis", "Giardiasis", false },
                    { 747, "Glandular fever", "Glandular fever", false },
                    { 748, "Glaucoma", "Glaucoma", false },
                    { 749, "Glomerulonephritis", "Glomerulonephritis", false },
                    { 750, "Glue ear", "Glue ear", false },
                    { 751, "Goitre", "Goitre", false },
                    { 752, "Gonorrhoea", "Gonorrhoea", false },
                    { 753, "Gout", "Gout", false },
                    { 754, "Guillain-Barré syndrome", "Guillain-Barré syndrome", false },
                    { 755, "Gum disease", "Gum disease", false },
                    { 756, "Haemochromatosis", "Haemochromatosis", false },
                    { 757, "Haemophilia", "Haemophilia", false },
                    { 758, "Hair loss", "Hair loss", false },
                    { 759, "Hand tendon repair", "Hand tendon repair", false },
                    { 760, "Having an operation (surgery)", "Having an operation (surgery)", false },
                    { 761, "Surgery (having an operation)", "Surgery (having an operation)", false },
                    { 762, "Hay fever", "Hay fever", false },
                    { 763, "Head lice and nits", "Head lice and nits", false },
                    { 764, "Hearing loss", "Hearing loss", false },
                    { 765, "Deafness", "Deafness", false },
                    { 766, "Hearing tests", "Hearing tests", false },
                    { 767, "Heart attack", "Heart attack", false },
                    { 768, "Heart block", "Heart block", false },
                    { 769, "Heartburn and acid reflux", "Heartburn and acid reflux", false },
                    { 770, "Gastro-oesophageal reflux disease (GORD)", "Gastro-oesophageal reflux disease (GORD)", false },
                    { 771, "Heart failure", "Heart failure", false },
                    { 772, "Heart transplant", "Heart transplant", false },
                    { 773, "Heavy periods", "Heavy periods", false },
                    { 774, "Periods (heavy)", "Periods (heavy)", false },
                    { 775, "Hepatitis A", "Hepatitis A", false },
                    { 776, "Hepatitis B", "Hepatitis B", false },
                    { 777, "Hepatitis C", "Hepatitis C", false },
                    { 778, "Hiatus hernia", "Hiatus hernia", false },
                    { 779, "Hernia (hiatus)", "Hernia (hiatus)", false },
                    { 780, "Hiccups", "Hiccups", false },
                    { 781, "High blood pressure (hypertension)", "High blood pressure (hypertension)", false },
                    { 782, "Hypertension", "Hypertension", false },
                    { 783, "Blood pressure (high)", "Blood pressure (high)", false },
                    { 784, "High cholesterol", "High cholesterol", false },
                    { 785, "Cholesterol (high)", "Cholesterol (high)", false },
                    { 786, "Hip fracture", "Hip fracture", false },
                    { 787, "Hip replacement", "Hip replacement", false },
                    { 788, "Excessive hair growth (hirsutism)", "Excessive hair growth (hirsutism)", false },
                    { 789, "hirsutism", "hirsutism", false },
                    { 790, "HIV and AIDS", "HIV and AIDS", false },
                    { 791, "Hives", "Hives", false },
                    { 792, "Hodgkin lymphoma", "Hodgkin lymphoma", false },
                    { 793, "Hormone replacement therapy (HRT)", "Hormone replacement therapy (HRT)", false },
                    { 794, "HRT", "HRT", false },
                    { 795, "Huntington's disease", "Huntington's disease", false },
                    { 796, "Hydrocephalus", "Hydrocephalus", false },
                    { 797, "Hydronephrosis", "Hydronephrosis", false },
                    { 798, "Hysterectomy", "Hysterectomy", false },
                    { 799, "Hysteroscopy", "Hysteroscopy", false },
                    { 800, "Idiopathic pulmonary fibrosis", "Idiopathic pulmonary fibrosis", false },
                    { 801, "Pulmonary fibrosis", "Pulmonary fibrosis", false },
                    { 802, "Ileostomy", "Ileostomy", false },
                    { 803, "Impetigo", "Impetigo", false },
                    { 804, "Infertility", "Infertility", false },
                    { 805, "Inguinal hernia repair", "Inguinal hernia repair", false },
                    { 806, "Hernia (inguinal)", "Hernia (inguinal)", false },
                    { 807, "Insect bites and stings", "Insect bites and stings", false },
                    { 808, "Sting or bite (insect)", "Sting or bite (insect)", false },
                    { 809, "Insomnia", "Insomnia", false },
                    { 810, "Iron deficiency anaemia", "Iron deficiency anaemia", false },
                    { 811, "Anaemia (iron deficiency)", "Anaemia (iron deficiency)", false },
                    { 812, "Irregular periods", "Irregular periods", false },
                    { 813, "Periods (irregular)", "Periods (irregular)", false },
                    { 814, "Irritable bowel syndrome (IBS)", "Irritable bowel syndrome (IBS)", false },
                    { 815, "IBS", "IBS", false },
                    { 816, "Itchy bottom", "Itchy bottom", false },
                    { 817, "Anus (itchy)", "Anus (itchy)", false },
                    { 818, "Itchy skin", "Itchy skin", false },
                    { 819, "IVF", "IVF", false },
                    { 820, "Japanese encephalitis", "Japanese encephalitis", false },
                    { 821, "Jaundice", "Jaundice", false },
                    { 822, "Newborn jaundice", "Newborn jaundice", false },
                    { 823, "Jaundice in newborns", "Jaundice in newborns", false },
                    { 824, "Jellyfish and other sea creature stings", "Jellyfish and other sea creature stings", false },
                    { 825, "Jet lag", "Jet lag", false },
                    { 826, "Joint hypermobility syndrome", "Joint hypermobility syndrome", false },
                    { 827, "Kawasaki disease", "Kawasaki disease", false },
                    { 828, "Kidney cancer", "Kidney cancer", false },
                    { 829, "Chronic kidney disease", "Chronic kidney disease", false },
                    { 830, "Kidney failure", "Kidney failure", false },
                    { 831, "Kidney infection", "Kidney infection", false },
                    { 832, "Kidney stones", "Kidney stones", false },
                    { 833, "Kidney transplant", "Kidney transplant", false },
                    { 834, "Knee ligament surgery", "Knee ligament surgery", false },
                    { 835, "Knee replacement", "Knee replacement", false },
                    { 836, "Kyphosis", "Kyphosis", false },
                    { 837, "Labyrinthitis and vestibular neuritis", "Labyrinthitis and vestibular neuritis", false },
                    { 838, "Vestibular neuritis", "Vestibular neuritis", false },
                    { 839, "Lactose intolerance", "Lactose intolerance", false },
                    { 840, "Laparoscopy (keyhole surgery)", "Laparoscopy (keyhole surgery)", false },
                    { 841, "Laryngeal (larynx) cancer", "Laryngeal (larynx) cancer", false },
                    { 842, "Laryngitis", "Laryngitis", false },
                    { 843, "Laxatives", "Laxatives", false },
                    { 844, "Lazy eye", "Lazy eye", false },
                    { 845, "Amblyopia", "Amblyopia", false },
                    { 846, "Leg cramps", "Leg cramps", false },
                    { 847, "Venous leg ulcer", "Venous leg ulcer", false },
                    { 848, "Leg ulcer", "Leg ulcer", false },
                    { 849, "Leptospirosis (Weil's disease)", "Leptospirosis (Weil's disease)", false },
                    { 850, "Weil's disease", "Weil's disease", false },
                    { 851, "Leukoplakia", "Leukoplakia", false },
                    { 852, "Lichen planus", "Lichen planus", false },
                    { 853, "Listeriosis", "Listeriosis", false },
                    { 854, "Liver cancer", "Liver cancer", false },
                    { 855, "Liver transplant", "Liver transplant", false },
                    { 856, "Long-sightedness", "Long-sightedness", false },
                    { 857, "Low blood pressure (hypotension)", "Low blood pressure (hypotension)", false },
                    { 858, "Blood pressure (low)", "Blood pressure (low)", false },
                    { 859, "Hypotension", "Hypotension", false },
                    { 860, "Lumbar decompression surgery", "Lumbar decompression surgery", false },
                    { 861, "Lumbar puncture", "Lumbar puncture", false },
                    { 862, "Lung cancer", "Lung cancer", false },
                    { 863, "Lung transplant", "Lung transplant", false },
                    { 864, "Lupus", "Lupus", false },
                    { 865, "Lymphoedema", "Lymphoedema", false },
                    { 866, "Age-related macular degeneration (AMD)", "Age-related macular degeneration (AMD)", false },
                    { 867, "Macular degeneration (age-related)", "Macular degeneration (age-related)", false },
                    { 868, "Malaria", "Malaria", false },
                    { 869, "Malignant brain tumour (brain cancer)", "Malignant brain tumour (brain cancer)", false },
                    { 870, "Brain tumour (malignant)", "Brain tumour (malignant)", false },
                    { 871, "Malnutrition", "Malnutrition", false },
                    { 872, "Marfan syndrome", "Marfan syndrome", false },
                    { 873, "Mastectomy", "Mastectomy", false },
                    { 874, "Mastitis", "Mastitis", false },
                    { 875, "Mastocytosis", "Mastocytosis", false },
                    { 876, "Measles", "Measles", false },
                    { 877, "Medicines information", "Medicines information", false },
                    { 878, "Skin cancer (melanoma)", "Skin cancer (melanoma)", false },
                    { 879, "Meningitis", "Meningitis", false },
                    { 880, "Menopause", "Menopause", false },
                    { 881, "Migraine", "Migraine", false },
                    { 882, "Head injury and concussion", "Head injury and concussion", false },
                    { 883, "Miscarriage", "Miscarriage", false },
                    { 884, "Moles", "Moles", false },
                    { 885, "Molluscum contagiosum", "Molluscum contagiosum", false },
                    { 886, "Motor neurone disease", "Motor neurone disease", false },
                    { 887, "Mouth cancer", "Mouth cancer", false },
                    { 888, "Tongue cancer", "Tongue cancer", false },
                    { 889, "MRI scan", "MRI scan", false },
                    { 890, "Mucositis", "Mucositis", false },
                    { 891, "Multiple myeloma", "Multiple myeloma", false },
                    { 892, "Myeloma", "Myeloma", false },
                    { 893, "Multiple sclerosis", "Multiple sclerosis", false },
                    { 894, "Mumps", "Mumps", false },
                    { 895, "Munchausen's syndrome", "Munchausen's syndrome", false },
                    { 896, "Muscular dystrophy", "Muscular dystrophy", false },
                    { 897, "Myasthenia gravis", "Myasthenia gravis", false },
                    { 898, "Narcolepsy", "Narcolepsy", false },
                    { 899, "Nasal polyps", "Nasal polyps", false },
                    { 900, "Newborn respiratory distress syndrome", "Newborn respiratory distress syndrome", false },
                    { 901, "Neurofibromatosis type 1", "Neurofibromatosis type 1", false },
                    { 902, "Neurofibromatosis type 2", "Neurofibromatosis type 2", false },
                    { 903, "Non-allergic rhinitis", "Non-allergic rhinitis", false },
                    { 904, "Non-gonococcal urethritis", "Non-gonococcal urethritis", false },
                    { 905, "Urethritis (NGU)", "Urethritis (NGU)", false },
                    { 906, "Non-Hodgkin lymphoma", "Non-Hodgkin lymphoma", false },
                    { 907, "Skin cancer (non-melanoma)", "Skin cancer (non-melanoma)", false },
                    { 908, "Squamous cell carcinoma", "Squamous cell carcinoma", false },
                    { 909, "Basal cell carcinoma", "Basal cell carcinoma", false },
                    { 910, "Noonan syndrome", "Noonan syndrome", false },
                    { 911, "Nosebleed", "Nosebleed", false },
                    { 912, "Obesity", "Obesity", false },
                    { 913, "Obsessive compulsive disorder (OCD)", "Obsessive compulsive disorder (OCD)", false },
                    { 914, "Occupational therapy", "Occupational therapy", false },
                    { 915, "Oesophageal cancer", "Oesophageal cancer", false },
                    { 916, "Orthodontics", "Orthodontics", false },
                    { 917, "Osteoarthritis", "Osteoarthritis", false },
                    { 918, "Osteomyelitis", "Osteomyelitis", false },
                    { 919, "Osteopathy", "Osteopathy", false },
                    { 920, "Osteoporosis", "Osteoporosis", false },
                    { 921, "Ovarian cancer", "Ovarian cancer", false },
                    { 922, "Ovarian cyst", "Ovarian cyst", false },
                    { 923, "Overactive thyroid (hyperthyroidism)", "Overactive thyroid (hyperthyroidism)", false },
                    { 924, "Hyperthyroidism", "Hyperthyroidism", false },
                    { 925, "Pacemaker implantation", "Pacemaker implantation", false },
                    { 926, "Paget's disease of bone", "Paget's disease of bone", false },
                    { 927, "Paget's disease of the nipple", "Paget's disease of the nipple", false },
                    { 928, "Pancreas transplant", "Pancreas transplant", false },
                    { 929, "Pancreatic cancer", "Pancreatic cancer", false },
                    { 930, "Paralysis", "Paralysis", false },
                    { 931, "Parkinson's disease", "Parkinson's disease", false },
                    { 932, "Pelvic inflammatory disease", "Pelvic inflammatory disease", false },
                    { 933, "Pelvic organ prolapse", "Pelvic organ prolapse", false },
                    { 934, "Prolapse (pelvic organ)", "Prolapse (pelvic organ)", false },
                    { 935, "Pemphigus vulgaris", "Pemphigus vulgaris", false },
                    { 936, "Perforated eardrum", "Perforated eardrum", false },
                    { 937, "Eardrum (burst)", "Eardrum (burst)", false },
                    { 938, "Pericarditis", "Pericarditis", false },
                    { 939, "Peripheral arterial disease (PAD)", "Peripheral arterial disease (PAD)", false },
                    { 940, "Peripheral neuropathy", "Peripheral neuropathy", false },
                    { 941, "Peritonitis", "Peritonitis", false },
                    { 942, "Phobias", "Phobias", false },
                    { 943, "Physiotherapy", "Physiotherapy", false },
                    { 944, "Piles (haemorrhoids)", "Piles (haemorrhoids)", false },
                    { 945, "Piles", "Piles", false },
                    { 946, "Pilonidal sinus", "Pilonidal sinus", false },
                    { 947, "Plastic surgery", "Plastic surgery", false },
                    { 948, "Pneumonia", "Pneumonia", false },
                    { 949, "Poisoning", "Poisoning", false },
                    { 950, "Polycystic ovary syndrome", "Polycystic ovary syndrome", false },
                    { 951, "Polycythaemia", "Polycythaemia", false },
                    { 952, "Polymyalgia rheumatica", "Polymyalgia rheumatica", false },
                    { 953, "Post-herpetic neuralgia", "Post-herpetic neuralgia", false },
                    { 954, "Postnatal depression", "Postnatal depression", false },
                    { 955, "Post-polio syndrome", "Post-polio syndrome", false },
                    { 956, "Post-traumatic stress disorder (PTSD)", "Post-traumatic stress disorder (PTSD)", false },
                    { 957, "Prader-Willi syndrome", "Prader-Willi syndrome", false },
                    { 958, "Pre-eclampsia", "Pre-eclampsia", false },
                    { 959, "PMS (premenstrual syndrome)", "PMS (premenstrual syndrome)", false },
                    { 960, "Pressure ulcers (pressure sores)", "Pressure ulcers (pressure sores)", false },
                    { 961, "Priapism (painful erections)", "Priapism (painful erections)", false },
                    { 962, "Primary biliary cholangitis (primary biliary cirrhosis)", "Primary biliary cholangitis (primary biliary cirrhosis)", false },
                    { 963, "Progressive supranuclear palsy", "Progressive supranuclear palsy", false },
                    { 964, "Prostate cancer", "Prostate cancer", false },
                    { 965, "Benign prostate enlargement", "Benign prostate enlargement", false },
                    { 966, "Prostate enlargement", "Prostate enlargement", false },
                    { 967, "Psoriasis", "Psoriasis", false },
                    { 968, "Psychosis", "Psychosis", false },
                    { 969, "Pulmonary embolism", "Pulmonary embolism", false },
                    { 970, "Pulmonary hypertension", "Pulmonary hypertension", false },
                    { 971, "Rabies", "Rabies", false },
                    { 972, "Radiotherapy", "Radiotherapy", false },
                    { 973, "Raynaud's", "Raynaud's", false },
                    { 974, "Reactive arthritis", "Reactive arthritis", false },
                    { 975, "Rectal examination", "Rectal examination", false },
                    { 976, "Repetitive strain injury (RSI)", "Repetitive strain injury (RSI)", false },
                    { 977, "Restless legs syndrome", "Restless legs syndrome", false },
                    { 978, "Restricted growth (dwarfism)", "Restricted growth (dwarfism)", false },
                    { 979, "Dwarfism", "Dwarfism", false },
                    { 980, "Detached retina (retinal detachment)", "Detached retina (retinal detachment)", false },
                    { 981, "Retinal detachment", "Retinal detachment", false },
                    { 982, "Rhesus disease", "Rhesus disease", false },
                    { 983, "Rheumatic fever", "Rheumatic fever", false },
                    { 984, "Rheumatoid arthritis", "Rheumatoid arthritis", false },
                    { 985, "Rickets and osteomalacia", "Rickets and osteomalacia", false },
                    { 986, "Osteomalacia", "Osteomalacia", false },
                    { 987, "Root canal treatment", "Root canal treatment", false },
                    { 988, "Rosacea", "Rosacea", false },
                    { 989, "Rubella (german measles)", "Rubella (german measles)", false },
                    { 990, "Scabies", "Scabies", false },
                    { 991, "Scars", "Scars", false },
                    { 992, "Schizophrenia", "Schizophrenia", false },
                    { 993, "Sciatica", "Sciatica", false },
                    { 994, "Buttock pain", "Buttock pain", false },
                    { 995, "Scoliosis", "Scoliosis", false },
                    { 996, "Scurvy", "Scurvy", false },
                    { 997, "Seasonal affective disorder (SAD)", "Seasonal affective disorder (SAD)", false },
                    { 998, "Self-harm", "Self-harm", false },
                    { 999, "Sepsis", "Sepsis", false },
                    { 1000, "Severe head injury", "Severe head injury", false },
                    { 1001, "Shingles", "Shingles", false },
                    { 1002, "Short-sightedness (myopia)", "Short-sightedness (myopia)", false },
                    { 1003, "Myopia", "Myopia", false },
                    { 1004, "Shoulder pain", "Shoulder pain", false },
                    { 1005, "Sickle cell disease", "Sickle cell disease", false },
                    { 1006, "Sjögren's syndrome", "Sjögren's syndrome", false },
                    { 1007, "Sleep paralysis", "Sleep paralysis", false },
                    { 1008, "Slipped disc", "Slipped disc", false },
                    { 1009, "Small bowel transplant", "Small bowel transplant", false },
                    { 1010, "Bowel transplant", "Bowel transplant", false },
                    { 1011, "Snoring", "Snoring", false },
                    { 1012, "Spina bifida", "Spina bifida", false },
                    { 1013, "Spinal muscular atrophy", "Spinal muscular atrophy", false },
                    { 1014, "Sports injuries", "Sports injuries", false },
                    { 1015, "Sprains and strains", "Sprains and strains", false },
                    { 1016, "Squint", "Squint", false },
                    { 1017, "Selective serotonin reuptake inhibitors (SSRIs)", "Selective serotonin reuptake inhibitors (SSRIs)", false },
                    { 1018, "Stammering", "Stammering", false },
                    { 1019, "Stuttering", "Stuttering", false },
                    { 1020, "Statins", "Statins", false },
                    { 1021, "Stem cell and bone marrow transplants", "Stem cell and bone marrow transplants", false },
                    { 1022, "Stillbirth", "Stillbirth", false },
                    { 1023, "Stomach cancer", "Stomach cancer", false },
                    { 1024, "Stomach ulcer", "Stomach ulcer", false },
                    { 1025, "Stroke", "Stroke", false },
                    { 1026, "Subarachnoid haemorrhage", "Subarachnoid haemorrhage", false },
                    { 1027, "Brain haemorrhage", "Brain haemorrhage", false },
                    { 1028, "Subdural haematoma", "Subdural haematoma", false },
                    { 1029, "Help for suicidal thoughts", "Help for suicidal thoughts", false },
                    { 1030, "Suicidal thoughts", "Suicidal thoughts", false },
                    { 1031, "Supraventricular tachycardia (SVT)", "Supraventricular tachycardia (SVT)", false },
                    { 1032, "Dysphagia (swallowing problems)", "Dysphagia (swallowing problems)", false },
                    { 1033, "Swallowing problems", "Swallowing problems", false },
                    { 1034, "Syphilis", "Syphilis", false },
                    { 1035, "Coccydynia (tailbone pain)", "Coccydynia (tailbone pain)", false },
                    { 1036, "Tay-Sachs disease", "Tay-Sachs disease", false },
                    { 1037, "Teeth grinding (bruxism)", "Teeth grinding (bruxism)", false },
                    { 1038, "Bruxism", "Bruxism", false },
                    { 1039, "Tendonitis", "Tendonitis", false },
                    { 1040, "Tennis elbow", "Tennis elbow", false },
                    { 1041, "Testicle lumps and swellings", "Testicle lumps and swellings", false },
                    { 1042, "Testicular cancer", "Testicular cancer", false },
                    { 1043, "Thalassaemia", "Thalassaemia", false },
                    { 1044, "Threadworms", "Threadworms", false },
                    { 1045, "Thyroid cancer", "Thyroid cancer", false },
                    { 1046, "Tick-borne encephalitis (TBE)", "Tick-borne encephalitis (TBE)", false },
                    { 1047, "Tics", "Tics", false },
                    { 1048, "Tinnitus", "Tinnitus", false },
                    { 1049, "Tonsillitis", "Tonsillitis", false },
                    { 1050, "Quinsy", "Quinsy", false },
                    { 1051, "Tourette's syndrome", "Tourette's syndrome", false },
                    { 1052, "Toxocariasis", "Toxocariasis", false },
                    { 1053, "Toxoplasmosis", "Toxoplasmosis", false },
                    { 1054, "Tracheostomy", "Tracheostomy", false },
                    { 1055, "Transient ischaemic attack (TIA)", "Transient ischaemic attack (TIA)", false },
                    { 1056, "TIA", "TIA", false },
                    { 1057, "Transurethral resection of the prostate (TURP)", "Transurethral resection of the prostate (TURP)", false },
                    { 1058, "Travel vaccinations", "Travel vaccinations", false },
                    { 1059, "Trichomoniasis", "Trichomoniasis", false },
                    { 1060, "Trichotillomania (hair pulling disorder)", "Trichotillomania (hair pulling disorder)", false },
                    { 1061, "Trigeminal neuralgia", "Trigeminal neuralgia", false },
                    { 1062, "Trigger finger", "Trigger finger", false },
                    { 1063, "Tuberculosis (TB)", "Tuberculosis (TB)", false },
                    { 1064, "Tuberous sclerosis", "Tuberous sclerosis", false },
                    { 1065, "Turner syndrome", "Turner syndrome", false },
                    { 1066, "Type 2 diabetes", "Type 2 diabetes", false },
                    { 1067, "Diabetes (type 2)", "Diabetes (type 2)", false },
                    { 1068, "Typhoid fever", "Typhoid fever", false },
                    { 1069, "Ulcerative colitis", "Ulcerative colitis", false },
                    { 1070, "Umbilical hernia repair", "Umbilical hernia repair", false },
                    { 1071, "Hernia (umbilical)", "Hernia (umbilical)", false },
                    { 1072, "Underactive thyroid (hypothyroidism)", "Underactive thyroid (hypothyroidism)", false },
                    { 1073, "Hypothyroidism", "Hypothyroidism", false },
                    { 1074, "Undescended testicles", "Undescended testicles", false },
                    { 1075, "Urinary catheter", "Urinary catheter", false },
                    { 1076, "Urinary incontinence", "Urinary incontinence", false },
                    { 1077, "Incontinence (urinary)", "Incontinence (urinary)", false },
                    { 1078, "Uveitis", "Uveitis", false },
                    { 1079, "Vaginal cancer", "Vaginal cancer", false },
                    { 1080, "Vaginismus", "Vaginismus", false },
                    { 1081, "Varicose eczema", "Varicose eczema", false },
                    { 1082, "Eczema (varicose)", "Eczema (varicose)", false },
                    { 1083, "Varicose veins", "Varicose veins", false },
                    { 1084, "Vascular dementia", "Vascular dementia", false },
                    { 1085, "Dementia (vascular)", "Dementia (vascular)", false },
                    { 1086, "Vertigo", "Vertigo", false },
                    { 1087, "Vitamin B12 or folate deficiency anaemia", "Vitamin B12 or folate deficiency anaemia", false },
                    { 1088, "Anaemia (vitamin B12 or folate deficiency)", "Anaemia (vitamin B12 or folate deficiency)", false },
                    { 1089, "Vitamins and minerals", "Vitamins and minerals", false },
                    { 1090, "Vitiligo", "Vitiligo", false },
                    { 1091, "Vulval cancer", "Vulval cancer", false },
                    { 1092, "Watering eyes", "Watering eyes", false },
                    { 1093, "Weight loss surgery", "Weight loss surgery", false },
                    { 1094, "Whiplash", "Whiplash", false },
                    { 1095, "Wisdom tooth removal", "Wisdom tooth removal", false },
                    { 1096, "Womb (uterus) cancer", "Womb (uterus) cancer", false },
                    { 1097, "Uterine (womb) cancer", "Uterine (womb) cancer", false },
                    { 1098, "Endometrial cancer", "Endometrial cancer", false },
                    { 1099, "Yellow fever", "Yellow fever", false },
                    { 1100, "Abortion", "Abortion", false },
                    { 1101, "Bird flu", "Bird flu", false },
                    { 1102, "Avian flu", "Avian flu", false },
                    { 1103, "Antifungal medicines", "Antifungal medicines", false },
                    { 1104, "Blood transfusion", "Blood transfusion", false },
                    { 1105, "Ringworm", "Ringworm", false },
                    { 1106, "Early menopause", "Early menopause", false },
                    { 1107, "Menopause (early)", "Menopause (early)", false },
                    { 1108, "Floaters and flashes in the eyes", "Floaters and flashes in the eyes", false },
                    { 1109, "Eye floaters", "Eye floaters", false },
                    { 1110, "Your contraception guide", "Your contraception guide", false },
                    { 1111, "Dementia guide", "Dementia guide", false },
                    { 1112, "Fitness Studio exercise videos", "Fitness Studio exercise videos", false },
                    { 1113, "NHS Health Check", "NHS Health Check", false },
                    { 1114, "Worms in humans", "Worms in humans", false },
                    { 1115, "Hookworm", "Hookworm", false },
                    { 1116, "Tapeworm", "Tapeworm", false },
                    { 1117, "Roundworm", "Roundworm", false },
                    { 1118, "Tremor or shaking hands", "Tremor or shaking hands", false },
                    { 1119, "tremor", "tremor", false },
                    { 1120, "essential tremor", "essential tremor", false },
                    { 1121, "shaking", "shaking", false },
                    { 1122, "Low white blood cell count", "Low white blood cell count", false },
                    { 1123, "White blood cell count (low)", "White blood cell count (low)", false },
                    { 1124, "Bunions", "Bunions", false },
                    { 1125, "Lost or changed sense of smell", "Lost or changed sense of smell", false },
                    { 1126, "Sense of smell (lost/changed)", "Sense of smell (lost/changed)", false },
                    { 1127, "Soiling (child pooing their pants)", "Soiling (child pooing their pants)", false },
                    { 1128, "Oral thrush (mouth thrush)", "Oral thrush (mouth thrush)", false },
                    { 1129, "Mouth thrush", "Mouth thrush", false },
                    { 1130, "Temporal arteritis", "Temporal arteritis", false },
                    { 1131, "Giant cell arteritis", "Giant cell arteritis", false },
                    { 1132, "Memory loss (amnesia)", "Memory loss (amnesia)", false },
                    { 1133, "Amnesia", "Amnesia", false },
                    { 1134, "Headaches", "Headaches", false },
                    { 1135, "Sinusitis (sinus infection)", "Sinusitis (sinus infection)", false },
                    { 1136, "Sinusitis", "Sinusitis", false },
                    { 1137, "Thrush in men and women", "Thrush in men and women", false },
                    { 1138, "Cold sores", "Cold sores", false },
                    { 1139, "Group B strep", "Group B strep", false },
                    { 1140, "Skin picking disorder", "Skin picking disorder", false },
                    { 1141, "Hyperparathyroidism", "Hyperparathyroidism", false },
                    { 1142, "Hypoparathyroidism", "Hypoparathyroidism", false },
                    { 1143, "Ear infections", "Ear infections", false },
                    { 1144, "Twitching eyes and muscles", "Twitching eyes and muscles", false },
                    { 1145, "Learning disabilities", "Learning disabilities", false },
                    { 1146, "NHS screening", "NHS screening", false },
                    { 1147, "Hormone headaches", "Hormone headaches", false },
                    { 1148, "Headaches (hormone)", "Headaches (hormone)", false },
                    { 1149, "What to do if someone has a seizure (fit)", "What to do if someone has a seizure (fit)", false },
                    { 1150, "Seizures (fits)", "Seizures (fits)", false },
                    { 1151, "Fits (seizures)", "Fits (seizures)", false },
                    { 1152, "Complementary and alternative medicine", "Complementary and alternative medicine", false },
                    { 1153, "End of life care", "End of life care", false },
                    { 1154, "Knocked-out tooth", "Knocked-out tooth", false },
                    { 1155, "Tooth knocked out", "Tooth knocked out", false },
                    { 1156, "Chipped broken or cracked tooth", "Chipped broken or cracked tooth", false },
                    { 1157, "Tooth (chipped or broken)", "Tooth (chipped or broken)", false },
                    { 1158, "Diarrhoea and vomiting", "Diarrhoea and vomiting", false },
                    { 1159, "Tummy bug", "Tummy bug", false },
                    { 1160, "Vomiting", "Vomiting", false },
                    { 1161, "Stomach bug", "Stomach bug", false },
                    { 1162, "Gastroenteritis", "Gastroenteritis", false },
                    { 1163, "Diarrhoea", "Diarrhoea", false },
                    { 1164, "Being sick", "Being sick", false },
                    { 1165, "Bullous pemphigoid", "Bullous pemphigoid", false },
                    { 1166, "Feeling sick (nausea)", "Feeling sick (nausea)", false },
                    { 1167, "Nausea", "Nausea", false },
                    { 1168, "Type 1 diabetes", "Type 1 diabetes", false },
                    { 1169, "Diabetes (type 1)", "Diabetes (type 1)", false },
                    { 1170, "Social care and support guide", "Social care and support guide", false },
                    { 1171, "Middle East respiratory syndrome (MERS)", "Middle East respiratory syndrome (MERS)", false },
                    { 1172, "Monkeypox", "Monkeypox", false },
                    { 1173, "Diabetic eye screening 2", "Diabetic eye screening 2", false },
                    { 1174, "Medical cannabis (and cannabis oils)", "Medical cannabis (and cannabis oils)", false },
                    { 1175, "Cannabis oil (medical cannabis)", "Cannabis oil (medical cannabis)", false },
                    { 1176, "Diabetic eye screening 1", "Diabetic eye screening 1", false },
                    { 1177, "Body odour (BO)", "Body odour (BO)", false },
                    { 1178, "Human papillomavirus (HPV)", "Human papillomavirus (HPV)", false },
                    { 1179, "Plantar fasciitis", "Plantar fasciitis", false },
                    { 1180, "Foot pain", "Foot pain", false },
                    { 1181, "heel pain", "heel pain", false },
                    { 1182, "Toe pain", "Toe pain", false },
                    { 1183, "ankle pain", "ankle pain", false },
                    { 1184, "Autism", "Autism", false },
                    { 1185, "Asperger's", "Asperger's", false },
                    { 1186, "Cosmetic procedures", "Cosmetic procedures", false },
                    { 1187, "Hand pain", "Hand pain", false },
                    { 1188, "Swollen arms and hands (oedema)", "Swollen arms and hands (oedema)", false },
                    { 1189, "Colonoscopy", "Colonoscopy", false },
                    { 1190, "Genetic and genomic testing", "Genetic and genomic testing", false },
                    { 1191, "Sleep apnoea", "Sleep apnoea", false },
                    { 1192, "Vaccinations", "Vaccinations", false },
                    { 1193, "Mental health and wellbeing", "Mental health and wellbeing", false },
                    { 1194, "High temperature (fever) in adults", "High temperature (fever) in adults", false },
                    { 1195, "Fever in adults", "Fever in adults", false },
                    { 1196, "Coronavirus (COVID-19)", "Coronavirus (COVID-19)", false },
                    { 1197, "Baby", "Baby", false },
                    { 1198, "Do not attempt cardiopulmonary resuscitation (DNACPR) decisions", "Do not attempt cardiopulmonary resuscitation (DNACPR) decisions", false },
                    { 1199, "Testicle pain", "Testicle pain", false },
                    { 1200, "Pain in testicles", "Pain in testicles", false },
                    { 1201, "Hearing aids and implants", "Hearing aids and implants", false },
                    { 1202, "Breast screening (mammogram)", "Breast screening (mammogram)", false },
                    { 1203, "Smelly feet", "Smelly feet", false },
                    { 1204, "Academic attainment", "Academic attainment", false },
                    { 1205, "Ageing", "Ageing", false },
                    { 1206, "Aggression", "Aggression", false },
                    { 1207, "Antenatal care", "Antenatal care", false },
                    { 1208, "Blood donation", "Blood donation", false },
                    { 1209, "Body image", "Body image", false },
                    { 1210, "Breastfeeding", "Breastfeeding", false },
                    { 1211, "Care homes", "Care homes", false },
                    { 1212, "Carers", "Carers", false },
                    { 1213, "Child development", "Child development", false },
                    { 1214, "Complementary therapies", "Complementary therapies", false },
                    { 1215, "Contraception", "Contraception", false },
                    { 1216, "Domestic violence", "Domestic violence", false },
                    { 1217, "Eating well", "Eating well", false },
                    { 1218, "Exercise and sports", "Exercise and sports", false },
                    { 1219, "General wellbeing", "General wellbeing", false },
                    { 1220, "Genetic screening", "Genetic screening", false },
                    { 1221, "Healthy volunteers", "Healthy volunteers", false },
                    { 1222, "Improving care and services", "Improving care and services", false },
                    { 1223, "Healthy lifestyle", "Healthy lifestyle", false },
                    { 1224, "Long COVID", "Long COVID", false },
                    { 1225, "Obesity risk", "Obesity risk", false },
                    { 1226, "Occupational health", "Occupational health", false },
                    { 1227, "Parenting", "Parenting", false },
                    { 1228, "Public health", "Public health", false },
                    { 1229, "Sleeping well", "Sleeping well", false },
                    { 1230, "Smoking", "Smoking", false },
                    { 1231, "Supplements", "Supplements", false }
                });

            migrationBuilder.InsertData(
                table: "SysRefIdentifierType",
                columns: new[] { "Id", "Code", "Description", "IsDeleted" },
                values: new object[,]
                {
                    { 1, "ParticipantId", "ParticipantId", false },
                    { 2, "NhsId", "NhsId", false },
                    { 3, "Deleted", "Deleted", false }
                });

            migrationBuilder.InsertData(
                table: "SysRefRole",
                columns: new[] { "Id", "Code", "Description", "IsDeleted" },
                values: new object[,]
                {
                    { 1, "Admin", "Admin", false },
                    { 2, "Researcher", "Researcher", false }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantIdentifiers_Value",
                table: "ParticipantIdentifiers",
                column: "Value");

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantAddress_Postcode",
                table: "ParticipantAddress",
                column: "Postcode");
        }
    }
}
