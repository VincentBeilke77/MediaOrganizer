using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using MediaCatalog.Models;
using MediaOrganizer.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MediaOrganizer.Controllers
{
    public class MoviesController : Controller
    {
        private static string BASEURL = "https://localhost:5001/";

        public async Task<ActionResult> List()
        {
            var moviesViewModel = new MoviesViewModel();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASEURL);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync("api/Movies");

                if (response.IsSuccessStatusCode)
                {
                    var movieResponse = response.Content.ReadAsStringAsync().Result;

                    moviesViewModel.Movies = JsonConvert.DeserializeObject<List<MovieModel>>(movieResponse);
                }
            }

            return View(moviesViewModel);
        }

        public async Task<ActionResult> Details(int id)
        {
            var movie = new MovieModel();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASEURL);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync($"api/movies/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var movieResponse = response.Content.ReadAsStringAsync().Result;

                    movie = JsonConvert.DeserializeObject<MovieModel>(movieResponse);
                }
            }

            return View(movie);
        }

        [HttpPost]
        public async Task<ActionResult> AddMovie(MovieModel movie)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri($"{BASEURL}/api/movie");

                var result = await client.PostAsJsonAsync<MovieModel>("movie", movie);

                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("List");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(movie);
        }
    }
}