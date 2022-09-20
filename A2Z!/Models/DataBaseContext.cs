using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2Z_.Models
{
    class DataBaseContext: DbContext
    { 
        public DbSet<User> Users { get; set; }

        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Section> Sections { get; set; }

        public DbSet<Faculty> Faculties { get; set; }

        public DbSet<Year> Years { get; set; }

        public DbSet<Material_Study> Material_Studies { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<Student_course> Student_Courses { get; set; }

        public DbSet<Payment> Payments { get; set; }

        public DbSet<Outlay> Outlays { get; set; }

        public DbSet<CafeSales> CafeSales { get; set; }

        public DbSet<Caffe> Caffes { get; set; }

        public DbSet<TypyOfOutlayPayment> TypyOfOutlayPayments { get; set; }

        public DbSet<CafeSalesForShow> CafeSalesForShows { get; set; }

        public DbSet<ProgramInstituteC> programInstitutes { get; set; }

        public DbSet<ProgramInstituteOneAndHalfHour> programInstituteOneAndHalfHours { get; set; }

        public DbSet<SetTimeForCourse> setTimeForCourses  { get; set; }

        public DbSet<Term> terms { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            optionsBuilder.UseSqlite($@"Data Source= E:\Special\SecretSanta\LastOne\A2Z.sqlite");
            base.OnConfiguring(optionsBuilder);

        }
    }
}
