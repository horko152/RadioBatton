using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class InitDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "genres",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    genrename = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_genres", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    created_at = table.Column<DateTime>(nullable: false),
                    email = table.Column<string>(nullable: false),
                    username = table.Column<string>(nullable: false),
                    password = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "songs",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    artist = table.Column<string>(nullable: false),
                    songname = table.Column<string>(nullable: false),
                    album = table.Column<string>(nullable: true),
                    releasedate = table.Column<DateTime>(nullable: false),
                    file = table.Column<string>(nullable: false),
                    userid = table.Column<int>(nullable: false),
                    genreid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_songs", x => x.id);
                    table.ForeignKey(
                        name: "FK_songs_genres_genreid",
                        column: x => x.genreid,
                        principalTable: "genres",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_songs_users_userid",
                        column: x => x.userid,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "likes",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    like = table.Column<bool>(nullable: false),
                    songId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_likes", x => x.id);
                    table.ForeignKey(
                        name: "FK_likes_songs_songId",
                        column: x => x.songId,
                        principalTable: "songs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_likes_songId",
                table: "likes",
                column: "songId");

            migrationBuilder.CreateIndex(
                name: "IX_songs_genreid",
                table: "songs",
                column: "genreid");

            migrationBuilder.CreateIndex(
                name: "IX_songs_userid",
                table: "songs",
                column: "userid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "likes");

            migrationBuilder.DropTable(
                name: "songs");

            migrationBuilder.DropTable(
                name: "genres");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
