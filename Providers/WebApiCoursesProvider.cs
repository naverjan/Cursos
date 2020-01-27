using ListaCursos.Interfaces;
using ListaCursos.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.IO;
using System.Text;

namespace ListaCursos.Providers
{
    public class WebApiCoursesProvider : ICoursesProvider
    {
        private readonly IHttpClientFactory httpClientFactory;

        public WebApiCoursesProvider(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<(bool IsSuccess, int? Id)> AddAsync(Course course)
        {
            course.Id = 0;
            var client = httpClientFactory.CreateClient("coursesServive");
            var options = new JsonSerializerOptions
            {
                WriteIndented = true                
            };
            var body = new StringContent(JsonSerializer.Serialize(course, options), Encoding.UTF8, "application/json");    
            var response = await client.PostAsync("/api/courses", body);
            
            if (response.IsSuccessStatusCode) {
                return (true, 1);
            }
            return (false, 1); ;
            
        }

        public async Task<ICollection<Course>> GetAllAsync()
        {
            var client = httpClientFactory.CreateClient("coursesServive");
            var response = await client.GetAsync("/api/courses");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                MemoryStream contentEnd = new MemoryStream(Encoding.UTF8.GetBytes(content));
                var results = await JsonSerializer.DeserializeAsync<ICollection<Course>>(contentEnd, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                return results;
            }
            return null;
        }

        public async Task<Course> GetAllAsync(int id)
        {
            var client = httpClientFactory.CreateClient("coursesServive");
            var response = await client.GetAsync($"/api/courses/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                MemoryStream contentEnd = new MemoryStream(Encoding.UTF8.GetBytes(content));
                var results = await JsonSerializer.DeserializeAsync<Course>(contentEnd, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                return results;
            }
            return null;
        }
    
        public Task<ICollection<Course>> SearchAsync(string search)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(int id, Course course)
        {
            var client = httpClientFactory.CreateClient("coursesServive");
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            var body = new StringContent(JsonSerializer.Serialize(course, options), Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"/api/courses/{id}", body);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
    }
}
