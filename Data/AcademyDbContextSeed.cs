using ControlRisksAcademy.Data.Constants;
using ControlRisksAcademy.DataAccess;
using ControlRisksAcademy.Models;
using ControlRisksAcademy.Models.Authentication;
using ControlRisksAcademy.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlRisksAcademy.Data
{
    public class AcademyDbContextSeed
    {
        public static async Task SeedEssentialsAsync(UserManager<ApplicationUser> userManager,
                                                     RoleManager<IdentityRole> roleManager,
                                                     AcademyDbContext academyDbContext)
        {
            await SeedRoles(roleManager);
            await SeedUsers(userManager);
            await SeedClassrooms(academyDbContext);
            await SeedCourses(academyDbContext);
            await SeedStudents(academyDbContext);
        }

        private static async Task SeedStudents(AcademyDbContext academyDbContext)
        {
            List<Students> students = new()
            {
                new()
                {
                    Name = "Jacob",
                    Surname = "Muchengeti",
                    DoB = DateTime.Parse("1 January 2000"),
                    EmailAddress = "jacob@mobileapp.co.za",
                    Gender = 'M',
                    PhoneNumber = "0718592919",
                    StudentNumber = "ST0001"
                },
                new()
                {
                    Name = "Mark",
                    Surname = "Robins",
                    DoB = DateTime.Parse("1 September 1999"),
                    EmailAddress = "mark@mobileapp.co.za",
                    Gender = 'M',
                    PhoneNumber = "0718002900",
                    StudentNumber = "ST0002"
                },
                new()
                {
                    Name = "Karen",
                    Surname = "Dube",
                    DoB = DateTime.Parse("1 December 2001"),
                    EmailAddress = "karen@mobileapp.co.za",
                    Gender = 'F',
                    PhoneNumber = "0718592919",
                    StudentNumber = "ST0003"
                }
            };

            try
            {
                foreach (var item in students)
                {
                    if (!academyDbContext.Students.Any(x => x.StudentNumber == item.StudentNumber))
                        academyDbContext.Students.Add(item);
                }

                await academyDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static async Task SeedCourses(AcademyDbContext academyDbContext)
        {
            List<Courses> courses = new()
            {
                new()
                {
                    Name = "Biology",
                    Description = "Biology is a branch of science that deals with living organisms and their vital processes. Biology encompasses diverse fields, including botany, conservation, ecology, evolution, genetics, marine biology, medicine, microbiology, molecular biology, physiology, and zoology,",
                    Classroom = academyDbContext.Classrooms.SingleOrDefault(x => x.Id == 2)
                },

                new()
                {
                    Name = "Maths",
                    Description = "Mathematics is the science and study of quality, structure, space, and change. Mathematicians seek out patterns, formulate new conjectures, and establish truth by rigorous deduction from appropriately chosen axioms and definitions.",
                    Classroom = academyDbContext.Classrooms.SingleOrDefault(x => x.Id == 3)
                },

                new()
                {
                    Name = "English",
                    Description = "This course emphasizes the fundamental language skills of reading, writing, speaking, listening, thinking, viewing and presenting.",
                    Classroom = academyDbContext.Classrooms.SingleOrDefault(x => x.Id == 1)
                },

                new()
                {
                    Name = "History",
                    Description = "History is the study of life in society in the past, in all its aspect, in relation to present developments and future hopes. It is the story of man in time, an inquiry into the past based on evidence. Indeed, evidence is the raw material of history teaching and learning.",
                    Classroom = academyDbContext.Classrooms.SingleOrDefault(x => x.Id == 4)
                }
            };
            try
            {
                foreach (var item in courses)
                {
                    if (!academyDbContext.Courses.Any(x => x.Name == item.Name))
                        academyDbContext.Courses.Add(item);
                }
                await academyDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static async Task SeedClassrooms(AcademyDbContext academyDbContext)
        {
            List<Classrooms> classrooms = new()
            {
                new() { Name = "A01", Active = true },
                new() { Name = "A02", Active = true },
                new() { Name = "A03", Active = true },
                new() { Name = "A04", Active = true },
                new() { Name = "A05", Active = true },
                new() { Name = "A06", Active = true },
                new() { Name = "A07", Active = true },
                new() { Name = "A08", Active = true },
                new() { Name = "B01", Active = true },
                new() { Name = "B02", Active = true },
                new() { Name = "B03", Active = true },
                new() { Name = "B04", Active = true },
                new() { Name = "B05", Active = true },
                new() { Name = "B06", Active = true },
                new() { Name = "B07", Active = true },
                new() { Name = "B08", Active = true },
                new() { Name = "C01", Active = true },
                new() { Name = "C02", Active = true },
                new() { Name = "C03", Active = true },
                new() { Name = "C04", Active = true },
                new() { Name = "C05", Active = true },
                new() { Name = "C06", Active = true },
                new() { Name = "C07", Active = true },
                new() { Name = "C08", Active = true }
            };

            try
            {
                foreach (var item in classrooms)
                {
                    if (!academyDbContext.Classrooms.Any(x => x.Name == item.Name))
                        academyDbContext.Classrooms.Add(item);
                }

                await academyDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static async Task SeedUsers(UserManager<ApplicationUser> userManager)
        {
            try
            {
                //Seed Default User
                var defaultUser = new ApplicationUser { UserName = Authorization.default_username, Email = Authorization.default_email, EmailConfirmed = true, PhoneNumberConfirmed = true };

                if (userManager.Users.All(u => u.UserName != defaultUser.UserName))
                {
                    await userManager.CreateAsync(defaultUser, Authorization.default_password);
                    await userManager.AddToRoleAsync(defaultUser, Authorization.default_role.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            try
            {
                //Seed Roles
                var isAdmin = await roleManager.RoleExistsAsync(Authorization.Roles.Administrator.ToString());
                if (!isAdmin)
                    await roleManager.CreateAsync(new IdentityRole(Authorization.Roles.Administrator.ToString()));

                var isUser = await roleManager.RoleExistsAsync(Authorization.Roles.User.ToString());
                if (!isUser)
                    await roleManager.CreateAsync(new IdentityRole(Authorization.Roles.User.ToString()));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}