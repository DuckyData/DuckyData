using DuckyData1._0._0Alpha.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DuckyData1._0._0Alpha.Controllers
{
    public class Manager
    {
        private DataContext ds = new DataContext();
        private ApplicationDbContext db = new ApplicationDbContext();

        public IEnumerable<ApplicationUser> AllUsers()
        {
            var fetchedObject = db.Users.OrderBy(x => x.UserName);
            return (fetchedObject == null) ? null :  fetchedObject;
        }

        /*
        public IEnumerable<string> AllCoursesString()
        {
            return ds.Courses.OrderBy(g => g.Name).Select(g => g.Name).ToList();
        }
        
        public IEnumerable<ProgramBase> AllPrograms()
        {
            return Mapper.Map<IEnumerable<ProgramBase>>(ds.Programs.OrderBy(nm => nm.Name));
        }
        /*
        public IEnumerable<Program> AllProgramsDs()
        {
            return ds.Programs.OrderBy(nm => nm.Name);
        }

        public IEnumerable<ProgramList> AllProgramsList()
        {
            return Mapper.Map<IEnumerable<ProgramList>>(ds.Programs.OrderBy(nm => nm.Name));
        }

        public ProgramBase AddProgram(ProgramAdd newItem)
        {
            var addedItem = ds.Programs.Add(Mapper.Map<Program>(newItem));
            ds.SaveChanges();

            return (addedItem == null) ? null : Mapper.Map<ProgramBase>(addedItem);
        }

        public ProgramBase GetProgramById(int id)
        {
            var fetchedObject = ds.Programs
                .SingleOrDefault(a => a.Id == id);

            return (fetchedObject == null) ? null
                : Mapper.Map<ProgramBase>(fetchedObject);

        }

        public ProgramBaseWithCourses GetProgramByIdWithCourses(int id)
        {
            var fetchedObject = ds.Programs
                .Include("Courses")
                .SingleOrDefault(a => a.Id == id);

            return (fetchedObject == null) ? null
                : Mapper.Map<ProgramBaseWithCourses>(fetchedObject);
        }

        //edit
        public ProgramBase EditProgram(ProgramEdit newItem)
        {
            var fetchedObject = ds.Programs
                .Include("Courses")
                .SingleOrDefault(a => a.Id == newItem.Id);

            if (fetchedObject == null)
            {
                return null;
            }
            else
            {
                ds.Entry(fetchedObject).CurrentValues.SetValues(newItem);
                ds.SaveChanges();

                return Mapper.Map<ProgramBase>(fetchedObject);
            }
        }

        //delete

        public bool DeleteProgramById(int id)
        {
            var itemToDelete = ds.Programs.Include("Courses")
                .SingleOrDefault(i => i.Id == id);

            if (itemToDelete == null)
            {
                return false;
            }
            else
            {
                ds.Programs.Remove(itemToDelete);
                ds.SaveChanges();
                return true;
            }

        }
        */



        



    }
}