﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutomatedEducationProgram.Migrations.AutomatedEducationProgram
{
    public partial class addingdocumenttextFKs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RelevantDocId",
                table: "VocabularyWord",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RelevantDocId",
                table: "ExamQuestions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VocabularyWord_RelevantDocId",
                table: "VocabularyWord",
                column: "RelevantDocId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamQuestions_RelevantDocId",
                table: "ExamQuestions",
                column: "RelevantDocId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamQuestions_DocumentTexts_RelevantDocId",
                table: "ExamQuestions",
                column: "RelevantDocId",
                principalTable: "DocumentTexts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VocabularyWord_DocumentTexts_RelevantDocId",
                table: "VocabularyWord",
                column: "RelevantDocId",
                principalTable: "DocumentTexts",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamQuestions_DocumentTexts_RelevantDocId",
                table: "ExamQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_VocabularyWord_DocumentTexts_RelevantDocId",
                table: "VocabularyWord");

            migrationBuilder.DropIndex(
                name: "IX_VocabularyWord_RelevantDocId",
                table: "VocabularyWord");

            migrationBuilder.DropIndex(
                name: "IX_ExamQuestions_RelevantDocId",
                table: "ExamQuestions");

            migrationBuilder.DropColumn(
                name: "RelevantDocId",
                table: "VocabularyWord");

            migrationBuilder.DropColumn(
                name: "RelevantDocId",
                table: "ExamQuestions");
        }
    }
}
