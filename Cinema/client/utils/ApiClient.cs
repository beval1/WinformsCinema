using RestSharp;
using System.Threading.Tasks;
using System;
using Newtonsoft.Json;

namespace Cinema
{
    class ApiClient
    {
        private RestClient _client;

        public ApiClient()
        {
            _client = new RestClient("http://localhost:3002/");
        }

        public async Task<MoviesList> GetMovie()
        {
            //RestClient client = new RestClient("http://localhost:3002/");
            var request = new RestRequest("get-movies/", Method.Get);
            var queryResult =  await _client.ExecuteAsync(request);
            Console.WriteLine(queryResult.Content);
            MoviesList movies = JsonConvert.DeserializeObject<MoviesList>(queryResult.Content);
            /*
            Console.WriteLine(movies.data.Count);
            foreach (var movie in movies.data)
            {
                Console.WriteLine("print");
                Console.WriteLine(movie.id);
                Console.WriteLine(movie.movieName);
                Console.WriteLine(movie.coverImage);
                Console.WriteLine(movie.imdbLink);
                Console.WriteLine(movie.premierYear);
            }
            */
            return movies;
        }

        public async Task<ProjectionList> GetProjections()
        {
            var request = new RestRequest("get-projections/", Method.Get);
            var queryResult = await _client.ExecuteAsync(request);
            Console.WriteLine(queryResult.Content);
            ProjectionList projections = JsonConvert.DeserializeObject<ProjectionList>(queryResult.Content);
            
            return projections;
        }
    }
}
