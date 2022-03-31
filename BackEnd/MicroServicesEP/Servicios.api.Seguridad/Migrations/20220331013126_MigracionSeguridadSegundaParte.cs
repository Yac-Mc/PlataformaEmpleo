using Microsoft.EntityFrameworkCore.Migrations;

namespace Servicios.api.Seguridad.Migrations
{
    public partial class MigracionSeguridadSegundaParte : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "PathImage",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TypeUser",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "Applicant");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                nullable: false
                );

            migrationBuilder.AlterColumn<string>(
                name: "Surnames",
                table: "AspNetUsers",
                nullable: false
                );
            migrationBuilder.Sql("ALTER TABLE AspNetUsers ADD CONSTRAINT CHK_TypeUser CHECK (TypeUser = 'Bidder' OR TypeUser = 'Applicant' OR TypeUser = 'Administrator');");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PathImage",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TypeUser",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
