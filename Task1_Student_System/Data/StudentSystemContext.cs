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

        }
    }
}
