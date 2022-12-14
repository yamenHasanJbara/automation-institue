// <auto-generated />
using System;
using A2Z_.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace A2Z_.Migrations
{
    [DbContext(typeof(DataBaseContext))]
    [Migration("20220316184536_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.10");

            modelBuilder.Entity("A2Z_.Models.CafeSales", b =>
                {
                    b.Property<int>("CafeSales_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AmountOfSaleDrink")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CaffeDrink_Id")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("date")
                        .HasColumnType("TEXT");

                    b.HasKey("CafeSales_Id");

                    b.HasIndex("CaffeDrink_Id");

                    b.ToTable("CafeSales");
                });

            modelBuilder.Entity("A2Z_.Models.CafeSalesForShow", b =>
                {
                    b.Property<int>("CafeSales_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AmountOfSaleDrink")
                        .HasColumnType("INTEGER");

                    b.Property<string>("DrinkName")
                        .HasColumnType("TEXT");

                    b.Property<int>("DrinkPrice")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("date")
                        .HasColumnType("TEXT");

                    b.HasKey("CafeSales_Id");

                    b.ToTable("CafeSalesForShows");
                });

            modelBuilder.Entity("A2Z_.Models.Caffe", b =>
                {
                    b.Property<int>("Drink_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("DrinkName")
                        .HasColumnType("TEXT");

                    b.Property<int>("DrinkPrice")
                        .HasColumnType("INTEGER");

                    b.HasKey("Drink_Id");

                    b.ToTable("Caffes");
                });

            modelBuilder.Entity("A2Z_.Models.Course", b =>
                {
                    b.Property<int>("Course_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("End_Date")
                        .HasColumnType("date");

                    b.Property<int?>("Faculty_Id")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Group")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsFinished")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Material_Study_Id")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Price")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Section_Id")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Start_Date")
                        .HasColumnType("date");

                    b.Property<int?>("Teacher_Id")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Terms_id")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Year_Id")
                        .HasColumnType("INTEGER");

                    b.Property<int>("percent")
                        .HasColumnType("INTEGER");

                    b.HasKey("Course_Id");

                    b.HasIndex("Faculty_Id");

                    b.HasIndex("Material_Study_Id");

                    b.HasIndex("Section_Id");

                    b.HasIndex("Teacher_Id");

                    b.HasIndex("Terms_id");

                    b.HasIndex("Year_Id");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("A2Z_.Models.Faculty", b =>
                {
                    b.Property<int>("Faculty_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int?>("Section_Id")
                        .HasColumnType("INTEGER");

                    b.HasKey("Faculty_Id");

                    b.HasIndex("Section_Id");

                    b.ToTable("Faculties");
                });

            modelBuilder.Entity("A2Z_.Models.Material_Study", b =>
                {
                    b.Property<int>("Material_Study_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Faculty_Id")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int?>("Section_Id")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Semester")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Year_Id")
                        .HasColumnType("INTEGER");

                    b.HasKey("Material_Study_Id");

                    b.HasIndex("Faculty_Id");

                    b.HasIndex("Section_Id");

                    b.HasIndex("Year_Id");

                    b.ToTable("Material_Studies");
                });

            modelBuilder.Entity("A2Z_.Models.Outlay", b =>
                {
                    b.Property<int>("Outlay_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Amount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Note")
                        .HasColumnType("TEXT");

                    b.Property<int>("Outlay_Type")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("TypyOfOutlayPayment_Id")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("date")
                        .HasColumnType("TEXT");

                    b.HasKey("Outlay_Id");

                    b.HasIndex("TypyOfOutlayPayment_Id");

                    b.ToTable("Outlays");
                });

            modelBuilder.Entity("A2Z_.Models.Payment", b =>
                {
                    b.Property<int>("Payment_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Amount")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Course_Id")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Payment_Type")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PillNumber")
                        .HasColumnType("TEXT");

                    b.Property<int>("Stayed")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Student_Id")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Teacher_Id")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("date")
                        .HasColumnType("date");

                    b.HasKey("Payment_Id");

                    b.HasIndex("Course_Id");

                    b.HasIndex("Student_Id");

                    b.HasIndex("Teacher_Id");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("A2Z_.Models.ProgramInstituteC", b =>
                {
                    b.Property<int>("ProgramInstitute_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Course_Id")
                        .HasColumnType("INTEGER");

                    b.Property<string>("day")
                        .HasColumnType("TEXT");

                    b.Property<string>("hall")
                        .HasColumnType("TEXT");

                    b.Property<string>("hour")
                        .HasColumnType("TEXT");

                    b.HasKey("ProgramInstitute_Id");

                    b.HasIndex("Course_Id");

                    b.ToTable("programInstitutes");
                });

            modelBuilder.Entity("A2Z_.Models.ProgramInstituteOneAndHalfHour", b =>
                {
                    b.Property<int>("ProgramInstitute_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Course_Id")
                        .HasColumnType("INTEGER");

                    b.Property<string>("day")
                        .HasColumnType("TEXT");

                    b.Property<string>("hall")
                        .HasColumnType("TEXT");

                    b.Property<string>("hour")
                        .HasColumnType("TEXT");

                    b.HasKey("ProgramInstitute_Id");

                    b.HasIndex("Course_Id");

                    b.ToTable("programInstituteOneAndHalfHours");
                });

            modelBuilder.Entity("A2Z_.Models.Section", b =>
                {
                    b.Property<int>("Section_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Section_Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Section_Id");

                    b.ToTable("Sections");
                });

            modelBuilder.Entity("A2Z_.Models.SetTimeForCourse", b =>
                {
                    b.Property<int>("SetTimeForCourse_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Courselength")
                        .HasColumnType("TEXT");

                    b.Property<string>("StartTime")
                        .HasColumnType("TEXT");

                    b.HasKey("SetTimeForCourse_Id");

                    b.ToTable("setTimeForCourses");
                });

            modelBuilder.Entity("A2Z_.Models.Student", b =>
                {
                    b.Property<int>("Student_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Faculty_Id")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Number_Phone")
                        .HasColumnType("TEXT");

                    b.HasKey("Student_Id");

                    b.HasIndex("Faculty_Id");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("A2Z_.Models.Student_course", b =>
                {
                    b.Property<int>("Student_Course_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Course_Id")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Student_Id")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Withdrawn")
                        .HasColumnType("INTEGER");

                    b.HasKey("Student_Course_Id");

                    b.HasIndex("Course_Id");

                    b.HasIndex("Student_Id");

                    b.ToTable("Student_Courses");
                });

            modelBuilder.Entity("A2Z_.Models.Teacher", b =>
                {
                    b.Property<int>("Teacher_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Number_Phone")
                        .HasColumnType("TEXT");

                    b.HasKey("Teacher_Id");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("A2Z_.Models.Term", b =>
                {
                    b.Property<int>("Terms_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("end_date")
                        .HasColumnType("date");

                    b.Property<string>("name")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("start_date")
                        .HasColumnType("date");

                    b.HasKey("Terms_id");

                    b.ToTable("terms");
                });

            modelBuilder.Entity("A2Z_.Models.TypyOfOutlayPayment", b =>
                {
                    b.Property<int>("TypyOfOutlayPayment_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("type")
                        .HasColumnType("TEXT");

                    b.HasKey("TypyOfOutlayPayment_Id");

                    b.ToTable("TypyOfOutlayPayments");
                });

            modelBuilder.Entity("A2Z_.Models.User", b =>
                {
                    b.Property<int>("User_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .HasColumnType("TEXT");

                    b.Property<int>("permission")
                        .HasColumnType("INTEGER");

                    b.HasKey("User_Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("A2Z_.Models.Year", b =>
                {
                    b.Property<int>("Year_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Faculty_Id")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Year_Number")
                        .HasColumnType("INTEGER");

                    b.HasKey("Year_Id");

                    b.HasIndex("Faculty_Id");

                    b.ToTable("Years");
                });

            modelBuilder.Entity("A2Z_.Models.CafeSales", b =>
                {
                    b.HasOne("A2Z_.Models.Caffe", "Caffe")
                        .WithMany("CafeSales")
                        .HasForeignKey("CaffeDrink_Id");
                });

            modelBuilder.Entity("A2Z_.Models.Course", b =>
                {
                    b.HasOne("A2Z_.Models.Faculty", "faculty")
                        .WithMany()
                        .HasForeignKey("Faculty_Id");

                    b.HasOne("A2Z_.Models.Material_Study", "material_Study")
                        .WithMany()
                        .HasForeignKey("Material_Study_Id");

                    b.HasOne("A2Z_.Models.Section", "section")
                        .WithMany()
                        .HasForeignKey("Section_Id");

                    b.HasOne("A2Z_.Models.Teacher", "teacher")
                        .WithMany("courses")
                        .HasForeignKey("Teacher_Id");

                    b.HasOne("A2Z_.Models.Term", "term")
                        .WithMany("courses")
                        .HasForeignKey("Terms_id");

                    b.HasOne("A2Z_.Models.Year", "Year")
                        .WithMany()
                        .HasForeignKey("Year_Id");
                });

            modelBuilder.Entity("A2Z_.Models.Faculty", b =>
                {
                    b.HasOne("A2Z_.Models.Section", "Section")
                        .WithMany("Faculties")
                        .HasForeignKey("Section_Id");
                });

            modelBuilder.Entity("A2Z_.Models.Material_Study", b =>
                {
                    b.HasOne("A2Z_.Models.Faculty", "Faculty")
                        .WithMany()
                        .HasForeignKey("Faculty_Id");

                    b.HasOne("A2Z_.Models.Section", "Section")
                        .WithMany()
                        .HasForeignKey("Section_Id");

                    b.HasOne("A2Z_.Models.Year", "Year")
                        .WithMany("Material_Studies")
                        .HasForeignKey("Year_Id");
                });

            modelBuilder.Entity("A2Z_.Models.Outlay", b =>
                {
                    b.HasOne("A2Z_.Models.TypyOfOutlayPayment", "TypyOfOutlayPayment")
                        .WithMany("Outlays")
                        .HasForeignKey("TypyOfOutlayPayment_Id");
                });

            modelBuilder.Entity("A2Z_.Models.Payment", b =>
                {
                    b.HasOne("A2Z_.Models.Course", "course")
                        .WithMany("payments")
                        .HasForeignKey("Course_Id");

                    b.HasOne("A2Z_.Models.Student", "student")
                        .WithMany("payments")
                        .HasForeignKey("Student_Id");

                    b.HasOne("A2Z_.Models.Teacher", "teacher")
                        .WithMany("payments")
                        .HasForeignKey("Teacher_Id");
                });

            modelBuilder.Entity("A2Z_.Models.ProgramInstituteC", b =>
                {
                    b.HasOne("A2Z_.Models.Course", "course")
                        .WithMany()
                        .HasForeignKey("Course_Id");
                });

            modelBuilder.Entity("A2Z_.Models.ProgramInstituteOneAndHalfHour", b =>
                {
                    b.HasOne("A2Z_.Models.Course", "course")
                        .WithMany()
                        .HasForeignKey("Course_Id");
                });

            modelBuilder.Entity("A2Z_.Models.Student", b =>
                {
                    b.HasOne("A2Z_.Models.Faculty", null)
                        .WithMany("students")
                        .HasForeignKey("Faculty_Id");
                });

            modelBuilder.Entity("A2Z_.Models.Student_course", b =>
                {
                    b.HasOne("A2Z_.Models.Course", "Course_Id_FK")
                        .WithMany("Student_Courses")
                        .HasForeignKey("Course_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("A2Z_.Models.Student", "Student_Id_FK")
                        .WithMany("Student_Courses")
                        .HasForeignKey("Student_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("A2Z_.Models.Year", b =>
                {
                    b.HasOne("A2Z_.Models.Faculty", "Faculty")
                        .WithMany("Years")
                        .HasForeignKey("Faculty_Id");
                });
#pragma warning restore 612, 618
        }
    }
}
