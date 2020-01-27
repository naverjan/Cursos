
using ListaCursos.Interfaces;
using ListaCursos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListaCursos.Providers
{
    public class FakeCoursesProvider : ICoursesProvider
    {
        private readonly List<Course> repo = new List<Course>();

        public FakeCoursesProvider()
        {
            repo.Add(new Course()
            {
                Id = 2,
                Name = "Calculo I",
                Author = "Andres Verjan",
                Description = "Aprene a implementar funciones en la vida diaria",
                Uri = "https://www.google.com"
            });
            repo.Add(new Course()
            {
                Id = 3,
                Name = "Integración continua",
                Author = "Andres Verjan",
                Description = "Aprende a realizar desarrollo de software de alta calidad",
                Uri = "https://www.google.com"
            });
            repo.Add(new Course()
            {
                Id = 4,
                Name = "Etica empresarial",
                Author = "Andres Verjan",
                Description = "El desarrollo de las empresas actualidad y futuro",
                Uri = "https://www.google.com"
            });
        }

        public Task<ICollection<Course>> GetAllAsync()
        {
            return Task.FromResult((ICollection<Course>)repo.ToList());
        }

        public Task<ICollection<Course>> SearchAsync(string search)
        {
            return Task.FromResult((ICollection<Course>)repo.Where(c=>c.Name.ToLowerInvariant().Contains(search.ToLowerInvariant())).ToList());
        }

        public Task<Course> GetAllAsync(int id)
        {
            return Task.FromResult(repo.FirstOrDefault(c => c.Id == id));
        }

        public Task<bool> UpdateAsync(int id, Course course)
        {
            var courseToUpdate = repo.FirstOrDefault(c => c.Id == id);
            if (courseToUpdate != null) 
            {
                courseToUpdate.Name = course.Name;
                courseToUpdate.Author = course.Author;
                courseToUpdate.Description = course.Description;
                courseToUpdate.Uri = course.Uri;

                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        /**
         * Creación de curso
         */
        public Task<(bool IsSuccess, int? Id)> AddAsync(Course course)
        {
            //Tomamos el id mayor y sumamos 1
            course.Id = repo.Max(c => c.Id) + 1;
            repo.Add(course);
            return Task.FromResult((true, (int?) course.Id));
        }
    }
}
