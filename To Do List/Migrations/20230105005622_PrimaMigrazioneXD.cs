using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoList.Migrations
{
    /// <inheritdoc />
    public partial class PrimaMigrazioneXD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    ClienteID = table.Column<int>(type: "int", nullable: false)
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
                    CompitoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Categoria = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descrizione = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Scadenza = table.Column<DateTime>(type: "Date", nullable: false),
                    Stato = table.Column<bool>(type: "bit", nullable: false),
                    ClienteID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compito", x => x.CompitoID);
                    table.ForeignKey(
                        name: "FK_Compito_Cliente_ClienteID",
                        column: x => x.ClienteID,
                        principalTable: "Cliente",
                        principalColumn: "ClienteID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompitoDipendente",
                columns: table => new
                {
                    ListaCompitiAssegnatiCompitoID = table.Column<int>(type: "int", nullable: false),
                    ListaDipendentiUsername = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompitoDipendente", x => new { x.ListaCompitiAssegnatiCompitoID, x.ListaDipendentiUsername });
                    table.ForeignKey(
                        name: "FK_CompitoDipendente_Compito_ListaCompitiAssegnatiCompitoID",
                        column: x => x.ListaCompitiAssegnatiCompitoID,
                        principalTable: "Compito",
                        principalColumn: "CompitoID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompitoDipendente_Dipendente_ListaDipendentiUsername",
                        column: x => x.ListaDipendentiUsername,
                        principalTable: "Dipendente",
                        principalColumn: "Username",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_ClienteID",
                table: "Cliente",
                column: "ClienteID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Compito_ClienteID",
                table: "Compito",
                column: "ClienteID");

            migrationBuilder.CreateIndex(
                name: "IX_Compito_CompitoID",
                table: "Compito",
                column: "CompitoID",
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
