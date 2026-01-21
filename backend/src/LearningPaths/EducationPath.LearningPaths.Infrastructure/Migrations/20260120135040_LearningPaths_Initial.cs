using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EducationPath.LearningPaths.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class LearningPaths_Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "learning_paths");

            migrationBuilder.CreateTable(
                name: "roadmaps",
                schema: "learning_paths",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_roadmaps", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "lessons",
                schema: "learning_paths",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    is_completed = table.Column<bool>(type: "boolean", nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false),
                    roadmap_id = table.Column<Guid>(type: "uuid", nullable: false),
                    links = table.Column<string>(type: "jsonb", nullable: true),
                    content = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_lessons", x => x.id);
                    table.ForeignKey(
                        name: "fk_lessons_roadmaps_roadmap_id",
                        column: x => x.roadmap_id,
                        principalSchema: "learning_paths",
                        principalTable: "roadmaps",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "lesson_dependencies",
                schema: "learning_paths",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    from_lesson_id = table.Column<Guid>(type: "uuid", nullable: false),
                    to_lesson_id = table.Column<Guid>(type: "uuid", nullable: false),
                    roadmap_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_lesson_dependencies", x => x.id);
                    table.ForeignKey(
                        name: "fk_lesson_dependencies_lessons_from_lesson_id",
                        column: x => x.from_lesson_id,
                        principalSchema: "learning_paths",
                        principalTable: "lessons",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_lesson_dependencies_lessons_to_lesson_id",
                        column: x => x.to_lesson_id,
                        principalSchema: "learning_paths",
                        principalTable: "lessons",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_lesson_dependencies_roadmaps_roadmap_id",
                        column: x => x.roadmap_id,
                        principalSchema: "learning_paths",
                        principalTable: "roadmaps",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_lesson_dependencies_from_lesson_id",
                schema: "learning_paths",
                table: "lesson_dependencies",
                column: "from_lesson_id");

            migrationBuilder.CreateIndex(
                name: "ix_lesson_dependencies_roadmap_id",
                schema: "learning_paths",
                table: "lesson_dependencies",
                column: "roadmap_id");

            migrationBuilder.CreateIndex(
                name: "ix_lesson_dependencies_to_lesson_id",
                schema: "learning_paths",
                table: "lesson_dependencies",
                column: "to_lesson_id");

            migrationBuilder.CreateIndex(
                name: "ix_lessons_roadmap_id",
                schema: "learning_paths",
                table: "lessons",
                column: "roadmap_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "lesson_dependencies",
                schema: "learning_paths");

            migrationBuilder.DropTable(
                name: "lessons",
                schema: "learning_paths");

            migrationBuilder.DropTable(
                name: "roadmaps",
                schema: "learning_paths");
        }
    }
}
