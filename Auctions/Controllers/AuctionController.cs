using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Auctions.Models;
using System.IO;
using Newtonsoft.Json;

namespace Auctions.Controllers
{
    public class AuctionController : ApiController
    {
        static HttpClient client = new HttpClient();
        private List<Auction> auctions = new List<Auction>();

        private void SetAsJSON(HttpClient client)
        {
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public IHttpActionResult Post(string token, Auction auction)
        {
            //SetAsJSON(client);

            if (!check_permission( token ))
                            return this.BadRequest("Brak uprawnień do dodania aukcji!");

            var currentauctions = read_file_json();

            if ( currentauctions != null)
                auctions = currentauctions;

            auction.Id = auctions.Count;
            auctions.Add(auction);

            write_file_json(auctions);

            return this.Ok("Aukcja dodana");
        }

        public IHttpActionResult Get(int id, string token)
        {
            //view your auctions

            if (!check_permission(token ))
                return this.BadRequest("Brak uprawnień do dodania aukcji!");

            var currentauctions = read_file_json();

            if (currentauctions != null)
                auctions = currentauctions;

            List<Auction> lauctions = new List<Auction>();
            foreach (Auction auction in auctions)
                if(auction.UserId ==id)
                lauctions.Add(auction);
            return this.Ok(lauctions);
        }

        public IHttpActionResult Get( string token)
        {
            //view all auctions

            if (!check_permission(token))
                return this.BadRequest("Brak uprawnień do dodania aukcji!");

            var currentauctions = read_file_json();

            if (currentauctions != null)
                auctions = currentauctions;
            
            return this.Ok(auctions);
        }

        public IHttpActionResult Put(string token, Auction auction)
        {
            if (!check_permission(token ))
                return this.BadRequest("Brak uprawnień do dodania aukcji!");

            var currentauctions = read_file_json();
            if ( currentauctions != null)
                auctions = currentauctions;

            auctions[auction.Id].Title = auction.Title;
            auctions[auction.Id].About = auction.About;
            auctions[auction.Id].Startprice = auction.Startprice;
            auctions[auction.Id].Minprice = auction.Minprice;
            auctions[auction.Id].Currentprice = auction.Currentprice;
            auctions[auction.Id].Begindate = auction.Begindate;
            auctions[auction.Id].Enddate = auction.Enddate;
            write_file_json(auctions);
            return this.Ok(auctions[auction.Id]);
        }

        public IHttpActionResult Delete(int id, string token)
        {
            if (!check_permission(token))
                return this.BadRequest("Nie prawidłowy token!");

            var currentauctions = read_file_json();
            if ( currentauctions != null)
            {
                auctions = currentauctions;
                auctions.Remove( auctions[id]);
            }
            if( auctions.Count != (id + 1))
                for (int i = id; i < auctions.Count; i++)
                    auctions[i].Id--;

            write_file_json(auctions);
            return this.Ok("Usuniento aukcję");
        }

        private bool check_permission(string token)
        {
            var responsString =
                client.GetStringAsync(string.Format("https://localhost:44384/api/Login?token={0}", token));

            var response = JsonConvert.DeserializeObject<bool>(responsString.Result);

            return response;
        }



        // GET: api/Aukcje
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Aukcje/5
        public string Get(int id)
        {
            return "value";
        }

        // PUT: api/Aukcje/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Aukcje/5
        public void Delete(int id)
        {
        }

        public static List<Auction> read_file_json()
        {
            if (!System.IO.File.Exists("C:/databases/data_auctions.txt"))
                return null;
            string fileString = File.ReadAllText("C:/databases/data_auctions.txt");
            return (List<Auction>)JsonConvert.DeserializeObject<IEnumerable<Auction>>(fileString);
        }

        public static void write_file_json(List<Auction> auctions)
        {
            string jsonString = JsonConvert.SerializeObject(auctions);
            File.WriteAllText("C:/databases/data_auctions.txt", jsonString);
        }


    }
}
