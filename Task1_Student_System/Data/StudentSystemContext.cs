using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using P01_StudentSystem.Models;


namespace P01_StudentSystem.Data
{
    internal class StudentSystemContext : DbContext
    {
        public DbSet<Student> Students {  get; set; }
        public DbSet<Course> Courses {  get; set; }
        public DbSet<Resource> Resources {  get; set; }
        public DbSet<Homework> Homeworks {  get; set; }
        public DbSet<StudentCourse> StudentCourses {  get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=StudentSystemDB;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>()
                .Property(S => S.PhoneNumber)
                .IsUnicode(false);


            modelBuilder.Entity<Resource>()
                .Property(R => R.Url)
                .IsUnicode(false);

            modelBuilder.Entity<Homework>()
                .Property(H => H.Content)
                .IsUnicode(false);

            modelBuilder.Entity<StudentCourse>()
                .HasKey(SC => new { SC.StudentId, SC.CourseId });


            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Student)
                .WithMany(s => s.StudentCourses)
                .HasForeignKey(sc => sc.StudentId);


            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Course)
                .WithMany(c => c.StudentCourses)
                .HasForeignKey(sc => sc.CourseId);

            modelBuilder.Entity<Resource>()
                .HasOne(R => R.Course)
                .WithMany(C => C.Resources)
                .HasForeignKey(R => R.CourseId);

            modelBuilder.Entity<Homework>()
                .HasOne(H => H.Course)
                .WithMany(C => C.Homeworks)
                .HasForeignKey(H => H.CourseId);


            modelBuilder.Entity<Homework>()
                .HasOne(H => H.Student)
                .WithMany(S => S.Homeworks)
                .HasForeignKey(H => H.StudentId);


            modelBuilder.Entity<Student>().HasData(

                new Student
                {
                    StudentId = 1,
                    Name = "Youssef",
                    PhoneNumber = "1012750565",
                    RegisteredOn = new DateTime(2026, 7, 18),
                    Birthday = new DateTime(2002, 4, 13)
                },

                new Student
                {
                    StudentId = 2,
                    Name = "Mohamed",
                    PhoneNumber = "1281736381",
                    RegisteredOn = new DateTime(2026, 7, 19),
                    Birthday = new DateTime(2002, 4, 13)
                }

            );


            modelBuilder.Entity<Course>().HasData(

                new Course
                {
                    CourseId = 1,
                    Name = "C#",
                    Description = "Introduction to C#",
                    StartDate = new DateTime(2026, 7, 20),
                    EndDate = new DateTime(2026, 10, 13),
                    Price = 1500
                },

                new Course
                {
                    CourseId = 2,
                    Name = "C++",
                    Description = "Introduction to C++",
                    StartDate = new DateTime(2026, 7, 20),
                    EndDate = new DateTime(2026, 10, 13),
                    Price = 1500
                }
            );

            modelBuilder.Entity<StudentCourse>().HasData(

                  new StudentCourse
                  {
                      StudentId = 1,
                      CourseId = 1
                  },

                  new StudentCourse
                  {
                      StudentId = 1,
                      CourseId = 2
                  },

                  new StudentCourse
                  {
                      StudentId = 2,
                      CourseId = 1
                  }

            );

            modelBuilder.Entity<Resource>().HasData(

               new Resource
               {
                   ResourceId = 1,
                   Name = "Introduction",
                   Url = "https://example.com/csharp",
                   ResourceType = ResourceType.Video,
                   CourseId = 1
               },

               new Resource
               {
                   ResourceId = 2,
                   Name = "SQL Notes",
                   Url = "https://example.com/sql",
                   ResourceType = ResourceType.Document,
                   CourseId = 2
               }
            );


            modelBuilder.Entity<Homework>().HasData(

              new Homework
              {
                  HomeworkId = 1,
                  Content = "Homework 1",
                  ContentType = ContentType.Pdf,
                  SubmissionTime = new DateTime(2026, 7, 20),
                  StudentId = 1,
                  CourseId = 1
              },

                 new Homework
                 {
                     HomeworkId = 2,
                     Content = "Homework 2",
                     ContentType = ContentType.Zip,
                     SubmissionTime = new DateTime(2026, 7, 25),
                     StudentId = 2,
                     CourseId = 1
                 }
            );




        }
    }
}
