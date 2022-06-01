using RestSharp;
using System.Threading.Tasks;
using System;
using Newtonsoft.Json;
using System.Net;

namespace Cinema
{
    //singleton class
    public sealed class ApiClient
    {
        private static readonly ApiClient instance = new ApiClient();
        private readonly RestClient _client = new RestClient();

        static ApiClient()
        {
        }
        private ApiClient()
        {
            _client = new RestClient("http://localhost:3002/");
        }
        public static ApiClient Instance
        {
            get { return instance; }
        }

        public async Task<MoviesList> GetMovie()
        {
            //RestClient client = new RestClient("http://localhost:3002/");
            var request = new RestRequest("get-movies/", Method.Get);
            var queryResult =  await _client.ExecuteAsync(request);
            Console.WriteLine(queryResult.Content);
            MoviesList movies = JsonConvert.DeserializeObject<MoviesList>(queryResult.Content);
            return movies;
        }

        public async Task<ProjectionList> GetProjections()
        {
            var request = new RestRequest("get-projections/", Method.Get);
            var response = await _client.ExecuteAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                ProjectionList projections = JsonConvert.DeserializeObject<ProjectionList>(response.Content);
                return projections;
            }
            return null;
        }

        public async Task<Ticket> GetTicket(string uuid)
        {
            var request = new RestRequest($"get-ticket/{uuid}", Method.Get);
            var response = await _client.ExecuteAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                TicketRoot ticketRoot = JsonConvert.DeserializeObject<TicketRoot>(response.Content);
                return ticketRoot.Ticket;
            }
            return null;
        }

        public async Task<TicketRoot> CreateTicket(TicketWrapper ticket)
        {
            var request = new RestRequest("create-ticket/", Method.Post);
            //request.AddParameter("application/json", json, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(ticket);
            Console.WriteLine(ticket);
            var response = await _client.ExecuteAsync(request);
            Console.WriteLine(response.Content);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<TicketRoot>(response.Content);
            }
            return null;
        }
    }
}
