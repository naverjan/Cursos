using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ListaCursos.Interfaces;
using ListaCursos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ListaCursos
{
    public class coursesModel : PageModel
    {        
        private readonly ICoursesProvider coursesProvider;

        public List<Course> Courses { get; set; }

        [BindProperty(SupportsGet = true)]//Permite el mecanismo de enlace bidirecional
        public string Search { get; set; }

        public coursesModel(ICoursesProvider coursesProvider) {
            this.coursesProvider = coursesProvider;            
        }       

        //Este metodo es utilizado para cuando haces una petición get
        public async Task<IActionResult> OnGet()
        {
            if (!string.IsNullOrWhiteSpace(Search))
            {                
                //Búsqueda
                var results = await coursesProvider.SearchAsync(Search);
                if(results != null)
                {
                    Courses = new List<Course>(results);
                }
            }
            else
            {
                var results = await coursesProvider.GetAllAsync();
                //Si el resultado es diferente null lo asignamos a a courses
                if (results != null)
                {
                    Courses = new List<Course>(results);
                }
            }

            

            //Page es un metodo que retorna un obejto de tipo pageResult  que implemente a IActionResult
            return Page();
        }

        public async Task<IActionResult> OnPostDelete(int id) {
            var course = await coursesProvider.GetAllAsync(id);
            if (course == null) {
                return NotFound();
            }
            return RedirectToPage("Details");
        }
    }
    
}