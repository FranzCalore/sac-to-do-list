using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoList.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    ClienteID = table.Column<int>(name: "Cliente_ID", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cognome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Indirizzo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumeroTelefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.ClienteID);
                });

            migrationBuilder.CreateTable(
                name: "Dipendente",
                columns: table => new
                {
                    Username = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cognome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumeroTelefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dipendente", x => x.Username);
                });

            migrationBuilder.CreateTable(
                name: "Compito",
                columns: table => new
                {
                    CompitoId = table.Column<int>(name: "Compito_Id", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Categoria = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descrizione = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Scadenza = table.Column<DateTime>(type: "Date", nullable: false),
                    Stato = table.Column<bool>(type: "bit", nullable: false),
                    ClienteID = table.Column<int>(name: "Cliente_ID", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compito", x => x.CompitoId);
                    table.ForeignKey(
                        name: "FK_Compito_Cliente_Cliente_ID",
                        column: x => x.ClienteID,
                        principalTable: "Cliente",
                        principalColumn: "Cliente_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompitoDipendente",
                columns: table => new
                {
                    ListaCompitiAssegnatiCompitoId = table.Column<int>(name: "ListaCompitiAssegnatiCompito_Id", type: "int", nullable: false),
                    ListaDipendentiUsername = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompitoDipendente", x => new { x.ListaCompitiAssegnatiCompitoId, x.ListaDipendentiUsername });
                    table.ForeignKey(
                        name: "FK_CompitoDipendente_Compito_ListaCompitiAssegnatiCompito_Id",
                        column: x => x.ListaCompitiAssegnatiCompitoId,
                        principalTable: "Compito",
                        principalColumn: "Compito_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompitoDipendente_Dipendente_ListaDipendentiUsername",
                        column: x => x.ListaDipendentiUsername,
                        principalTable: "Dipendente",
                        principalColumn: "Username",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_Cliente_ID",
                table: "Cliente",
                column: "Cliente_ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Compito_Cliente_ID",
                table: "Compito",
                column: "Cliente_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Compito_Compito_Id",
                table: "Compito",
                column: "Compito_Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompitoDipendente_ListaDipendentiUsername",
                table: "CompitoDipendente",
                column: "ListaDipendentiUsername");

            migrationBuilder.CreateIndex(
                name: "IX_Dipendente_Username",
                table: "Dipendente",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompitoDipendente");

            migrationBuilder.DropTable(
                name: "Compito");

            migrationBuilder.DropTable(
                name: "Dipendente");

            migrationBuilder.DropTable(
                name: "Cliente");
        }
    }
}
