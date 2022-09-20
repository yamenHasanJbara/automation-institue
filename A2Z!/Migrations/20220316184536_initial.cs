using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace A2Z_.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CafeSalesForShows",
                columns: table => new
                {
                    CafeSales_Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AmountOfSaleDrink = table.Column<int>(nullable: false),
                    date = table.Column<DateTime>(nullable: false),
                    DrinkName = table.Column<string>(nullable: true),
                    DrinkPrice = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CafeSalesForShows", x => x.CafeSales_Id);
                });

            migrationBuilder.CreateTable(
                name: "Caffes",
                columns: table => new
                {
                    Drink_Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DrinkName = table.Column<string>(nullable: true),
                    DrinkPrice = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Caffes", x => x.Drink_Id);
                });

            migrationBuilder.CreateTable(
                name: "Sections",
                columns: table => new
                {
                    Section_Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Section_Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sections", x => x.Section_Id);
                });

            migrationBuilder.CreateTable(
                name: "setTimeForCourses",
                columns: table => new
                {
                    SetTimeForCourse_Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Courselength = table.Column<string>(nullable: true),
                    StartTime = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_setTimeForCourses", x => x.SetTimeForCourse_Id);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    Teacher_Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Number_Phone = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.Teacher_Id);
                });

            migrationBuilder.CreateTable(
                name: "terms",
                columns: table => new
                {
                    Terms_id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(nullable: true),
                    start_date = table.Column<DateTime>(type: "date", nullable: false),
                    end_date = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_terms", x => x.Terms_id);
                });

            migrationBuilder.CreateTable(
                name: "TypyOfOutlayPayments",
                columns: table => new
                {
                    TypyOfOutlayPayment_Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypyOfOutlayPayments", x => x.TypyOfOutlayPayment_Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    User_Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserName = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    permission = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.User_Id);
                });

            migrationBuilder.CreateTable(
                name: "CafeSales",
                columns: table => new
                {
                    CafeSales_Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AmountOfSaleDrink = table.Column<int>(nullable: false),
                    date = table.Column<DateTime>(nullable: false),
                    CaffeDrink_Id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CafeSales", x => x.CafeSales_Id);
                    table.ForeignKey(
                        name: "FK_CafeSales_Caffes_CaffeDrink_Id",
                        column: x => x.CaffeDrink_Id,
                        principalTable: "Caffes",
                        principalColumn: "Drink_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Faculties",
                columns: table => new
                {
                    Faculty_Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Section_Id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faculties", x => x.Faculty_Id);
                    table.ForeignKey(
                        name: "FK_Faculties_Sections_Section_Id",
                        column: x => x.Section_Id,
                        principalTable: "Sections",
                        principalColumn: "Section_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Outlays",
                columns: table => new
                {
                    Outlay_Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Amount = table.Column<int>(nullable: false),
                    Outlay_Type = table.Column<int>(nullable: false),
                    date = table.Column<DateTime>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    TypyOfOutlayPayment_Id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Outlays", x => x.Outlay_Id);
                    table.ForeignKey(
                        name: "FK_Outlays_TypyOfOutlayPayments_TypyOfOutlayPayment_Id",
                        column: x => x.TypyOfOutlayPayment_Id,
                        principalTable: "TypyOfOutlayPayments",
                        principalColumn: "TypyOfOutlayPayment_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Student_Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Number_Phone = table.Column<string>(nullable: true),
                    Faculty_Id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Student_Id);
                    table.ForeignKey(
                        name: "FK_Students_Faculties_Faculty_Id",
                        column: x => x.Faculty_Id,
                        principalTable: "Faculties",
                        principalColumn: "Faculty_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Years",
                columns: table => new
                {
                    Year_Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Year_Number = table.Column<int>(nullable: false),
                    Faculty_Id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Years", x => x.Year_Id);
                    table.ForeignKey(
                        name: "FK_Years_Faculties_Faculty_Id",
                        column: x => x.Faculty_Id,
                        principalTable: "Faculties",
                        principalColumn: "Faculty_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Material_Studies",
                columns: table => new
                {
                    Material_Study_Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Semester = table.Column<int>(nullable: false),
                    Year_Id = table.Column<int>(nullable: true),
                    Faculty_Id = table.Column<int>(nullable: true),
                    Section_Id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Material_Studies", x => x.Material_Study_Id);
                    table.ForeignKey(
                        name: "FK_Material_Studies_Faculties_Faculty_Id",
                        column: x => x.Faculty_Id,
                        principalTable: "Faculties",
                        principalColumn: "Faculty_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Material_Studies_Sections_Section_Id",
                        column: x => x.Section_Id,
                        principalTable: "Sections",
                        principalColumn: "Section_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Material_Studies_Years_Year_Id",
                        column: x => x.Year_Id,
                        principalTable: "Years",
                        principalColumn: "Year_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Course_Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Section_Id = table.Column<int>(nullable: true),
                    Faculty_Id = table.Column<int>(nullable: true),
                    Year_Id = table.Column<int>(nullable: true),
                    Material_Study_Id = table.Column<int>(nullable: true),
                    Teacher_Id = table.Column<int>(nullable: true),
                    Terms_id = table.Column<int>(nullable: true),
                    Group = table.Column<string>(nullable: true),
                    Price = table.Column<int>(nullable: false),
                    Start_Date = table.Column<DateTime>(type: "date", nullable: false),
                    End_Date = table.Column<DateTime>(type: "date", nullable: false),
                    percent = table.Column<int>(nullable: false),
                    IsFinished = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Course_Id);
                    table.ForeignKey(
                        name: "FK_Courses_Faculties_Faculty_Id",
                        column: x => x.Faculty_Id,
                        principalTable: "Faculties",
                        principalColumn: "Faculty_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Courses_Material_Studies_Material_Study_Id",
                        column: x => x.Material_Study_Id,
                        principalTable: "Material_Studies",
                        principalColumn: "Material_Study_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Courses_Sections_Section_Id",
                        column: x => x.Section_Id,
                        principalTable: "Sections",
                        principalColumn: "Section_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Courses_Teachers_Teacher_Id",
                        column: x => x.Teacher_Id,
                        principalTable: "Teachers",
                        principalColumn: "Teacher_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Courses_terms_Terms_id",
                        column: x => x.Terms_id,
                        principalTable: "terms",
                        principalColumn: "Terms_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Courses_Years_Year_Id",
                        column: x => x.Year_Id,
                        principalTable: "Years",
                        principalColumn: "Year_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Payment_Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Amount = table.Column<int>(nullable: false),
                    date = table.Column<DateTime>(type: "date", nullable: false),
                    Student_Id = table.Column<int>(nullable: true),
                    Teacher_Id = table.Column<int>(nullable: true),
                    Course_Id = table.Column<int>(nullable: true),
                    Payment_Type = table.Column<int>(nullable: false),
                    Stayed = table.Column<int>(nullable: false),
                    PillNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Payment_Id);
                    table.ForeignKey(
                        name: "FK_Payments_Courses_Course_Id",
                        column: x => x.Course_Id,
                        principalTable: "Courses",
                        principalColumn: "Course_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Payments_Students_Student_Id",
                        column: x => x.Student_Id,
                        principalTable: "Students",
                        principalColumn: "Student_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Payments_Teachers_Teacher_Id",
                        column: x => x.Teacher_Id,
                        principalTable: "Teachers",
                        principalColumn: "Teacher_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "programInstituteOneAndHalfHours",
                columns: table => new
                {
                    ProgramInstitute_Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Course_Id = table.Column<int>(nullable: true),
                    day = table.Column<string>(nullable: true),
                    hour = table.Column<string>(nullable: true),
                    hall = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_programInstituteOneAndHalfHours", x => x.ProgramInstitute_Id);
                    table.ForeignKey(
                        name: "FK_programInstituteOneAndHalfHours_Courses_Course_Id",
                        column: x => x.Course_Id,
                        principalTable: "Courses",
                        principalColumn: "Course_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "programInstitutes",
                columns: table => new
                {
                    ProgramInstitute_Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Course_Id = table.Column<int>(nullable: true),
                    day = table.Column<string>(nullable: true),
                    hour = table.Column<string>(nullable: true),
                    hall = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_programInstitutes", x => x.ProgramInstitute_Id);
                    table.ForeignKey(
                        name: "FK_programInstitutes_Courses_Course_Id",
                        column: x => x.Course_Id,
                        principalTable: "Courses",
                        principalColumn: "Course_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Student_Courses",
                columns: table => new
                {
                    Student_Course_Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Course_Id = table.Column<int>(nullable: false),
                    Student_Id = table.Column<int>(nullable: false),
                    Withdrawn = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student_Courses", x => x.Student_Course_Id);
                    table.ForeignKey(
                        name: "FK_Student_Courses_Courses_Course_Id",
                        column: x => x.Course_Id,
                        principalTable: "Courses",
                        principalColumn: "Course_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Student_Courses_Students_Student_Id",
                        column: x => x.Student_Id,
                        principalTable: "Students",
                        principalColumn: "Student_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CafeSales_CaffeDrink_Id",
                table: "CafeSales",
                column: "CaffeDrink_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_Faculty_Id",
                table: "Courses",
                column: "Faculty_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_Material_Study_Id",
                table: "Courses",
                column: "Material_Study_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_Section_Id",
                table: "Courses",
                column: "Section_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_Teacher_Id",
                table: "Courses",
                column: "Teacher_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_Terms_id",
                table: "Courses",
                column: "Terms_id");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_Year_Id",
                table: "Courses",
                column: "Year_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Faculties_Section_Id",
                table: "Faculties",
                column: "Section_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Material_Studies_Faculty_Id",
                table: "Material_Studies",
                column: "Faculty_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Material_Studies_Section_Id",
                table: "Material_Studies",
                column: "Section_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Material_Studies_Year_Id",
                table: "Material_Studies",
                column: "Year_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Outlays_TypyOfOutlayPayment_Id",
                table: "Outlays",
                column: "TypyOfOutlayPayment_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_Course_Id",
                table: "Payments",
                column: "Course_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_Student_Id",
                table: "Payments",
                column: "Student_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_Teacher_Id",
                table: "Payments",
                column: "Teacher_Id");

            migrationBuilder.CreateIndex(
                name: "IX_programInstituteOneAndHalfHours_Course_Id",
                table: "programInstituteOneAndHalfHours",
                column: "Course_Id");

            migrationBuilder.CreateIndex(
                name: "IX_programInstitutes_Course_Id",
                table: "programInstitutes",
                column: "Course_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Student_Courses_Course_Id",
                table: "Student_Courses",
                column: "Course_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Student_Courses_Student_Id",
                table: "Student_Courses",
                column: "Student_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Students_Faculty_Id",
                table: "Students",
                column: "Faculty_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Years_Faculty_Id",
                table: "Years",
                column: "Faculty_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CafeSales");

            migrationBuilder.DropTable(
                name: "CafeSalesForShows");

            migrationBuilder.DropTable(
                name: "Outlays");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "programInstituteOneAndHalfHours");

            migrationBuilder.DropTable(
                name: "programInstitutes");

            migrationBuilder.DropTable(
                name: "setTimeForCourses");

            migrationBuilder.DropTable(
                name: "Student_Courses");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Caffes");

            migrationBuilder.DropTable(
                name: "TypyOfOutlayPayments");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Material_Studies");

            migrationBuilder.DropTable(
                name: "Teachers");

            migrationBuilder.DropTable(
                name: "terms");

            migrationBuilder.DropTable(
                name: "Years");

            migrationBuilder.DropTable(
                name: "Faculties");

            migrationBuilder.DropTable(
                name: "Sections");
        }
    }
}
