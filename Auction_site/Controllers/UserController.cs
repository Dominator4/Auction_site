using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Newtonsoft.Json;
using Auction_site.Models;


namespace Auction_site.Controllers
{
    public class UserController : ApiController
    {
        private List<User> users = new List<User>();
        

        public IHttpActionResult Get(int id, string token)
        {
            if (!LoginController.CheckToken(token))
                return this.BadRequest("Nie prawidłowy token!");

            var currentusers = read_file_json();

            if (currentusers == null)
                return this.BadRequest("NO DATA!");

            return this.Ok(currentusers[id]);
        }


        public IHttpActionResult Post(User user)
        {

            var currentusers = read_file_json();

            if (currentusers != null)
                users = currentusers;


            if (user == null)
                return this.BadRequest("NO DATA!");
            else
            {
                user.Id = users.Count;
                users.Add(user);
            }

           
            write_file_json(users);

            return this.Ok(users[users.Count - 1]);
        }
        public IHttpActionResult Put( string token, User user)
        {
            if (!LoginController.CheckToken(token))
                return this.BadRequest("Nie prawidłowy token!");

            var currentusers = read_file_json();
            if (currentusers != null)
                users = currentusers;

            users[user.Id].Firstname = user.Firstname;
            users[user.Id].Lastname = user.Lastname;
            write_file_json(users);
            return this.Ok(users[user.Id]);
        }

        public IHttpActionResult Delete(int id, string token)
 {
            if (!LoginController.CheckToken(token))
                return this.BadRequest("Nie prawidłowy token!");

            var currentusers = read_file_json();
              if (currentusers != null)
{                users = currentusers;
users.Remove(users[id]);
}if (users.Count != (id + 1))
                for (int i = id; i < users.Count; i++)
                    users[i].Id--;

            write_file_json(users);
            return this.Ok("Konto usuniento.");
 }

        public static List<User> read_file_json()
        {
            if (!System.IO.File.Exists( "C:/databases/data_users.txt" ))
                return null;
                string fileString = File.ReadAllText("C:/databases/data_users.txt");
            return (List<User>)JsonConvert.DeserializeObject<IEnumerable<User>>(fileString);
        }

        public static void write_file_json(List<User> users)
        {
             var foldername =System.IO.Path.Combine( @"C:\\databases" );
            System.IO.Directory.CreateDirectory( foldername );
            string jsonString = JsonConvert.SerializeObject(users);
            File.WriteAllText("C:/databases/data_users.txt", jsonString);
        }


    }
}