using Newtonsoft.Json;
using PokeApi.Model;
using System;
using System.IO;
using System.Net;
namespace PokeApi
{
    class Program
    {
        static void Main(string[] args)
        {
            PokeConnection pokeConnection = new PokeConnection();
            pokeConnection.Connection();
        }
    }

    public class PokeConnection
    {
        public void Connection()
        {
            string url = "https://pokeapi.co/api/v2/pokemon/squirtle/";
            string pokemonJson = GetJson(url);
            Console.WriteLine(pokemonJson);

            string pokemonName = GetPokemonName(pokemonJson);
            Console.WriteLine($"Name: {pokemonName}");
        }

        private string GetJson(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                WebResponse response = request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, System.Text.Encoding.UTF8);
                    return reader.ReadToEnd();
                }
            }
            catch (WebException exc)
            {
                WebResponse errorResponse = exc.Response;
                using (Stream responseStream = errorResponse.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, System.Text.Encoding.GetEncoding("utf-8"));
                    string errorText = reader.ReadToEnd();
                    Console.WriteLine($"There was an error: {Environment.NewLine}{errorText}");
                }
                throw;
            }
        }

        private string GetPokemonName(string pokemonJson)
        {
            Pokemon pokemon = JsonConvert.DeserializeObject<Pokemon>(pokemonJson);
            return pokemon.Name;
        }
    }
}
