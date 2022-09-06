using Microsoft.EntityFrameworkCore.Migrations;

namespace Wortastik.Data.Migrations
{
    public partial class addedOwnerUsernameToJobPostingModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_JobPosting",
                table: "JobPosting");

            migrationBuilder.RenameTable(
                name: "JobPosting",
                newName: "JobPostings");

            migrationBuilder.RenameColumn(
                name: "Jobtitle",
                table: "JobPostings",
                newName: "JobTitle");

            migrationBuilder.AddColumn<string>(
                name: "OwnerUsername",
                table: "JobPostings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobPostings",
                table: "JobPostings",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_JobPostings",
                table: "JobPostings");

            migrationBuilder.DropColumn(
                name: "OwnerUsername",
                table: "JobPostings");

            migrationBuilder.RenameTable(
                name: "JobPostings",
                newName: "JobPosting");

            migrationBuilder.RenameColumn(
                name: "JobTitle",
                table: "JobPosting",
                newName: "Jobtitle");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobPosting",
                table: "JobPosting",
                column: "Id");
        }
    }
}
