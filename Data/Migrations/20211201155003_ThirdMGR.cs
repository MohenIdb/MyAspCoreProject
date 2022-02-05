using Microsoft.EntityFrameworkCore.Migrations;

namespace MyAspCoreFinalProject.Data.Migrations
{
    public partial class ThirdMGR : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BloodDonors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 150, nullable: false),
                    Gender = table.Column<string>(maxLength: 10, nullable: false),
                    Age = table.Column<int>(nullable: false),
                    PhotoUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodDonors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DonorDetails",
                columns: table => new
                {
                    DonorDetailsId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BloodDonorId = table.Column<int>(nullable: false),
                    OrganizationName = table.Column<string>(nullable: true),
                    DonateTime = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonorDetails", x => x.DonorDetailsId);
                    table.ForeignKey(
                        name: "FK_DonorDetails_BloodDonors_BloodDonorId",
                        column: x => x.BloodDonorId,
                        principalTable: "BloodDonors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DonorDetails_BloodDonorId",
                table: "DonorDetails",
                column: "BloodDonorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DonorDetails");

            migrationBuilder.DropTable(
                name: "BloodDonors");
        }
    }
}
