using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Auction_site.Models;

namespace Auction_site.Controllers
{
    public class LoginController : ApiController
    {
        public IHttpActionResult Get(string login, string pass)
        {
            var currentusers =UserController.read_file_json();
            if(currentusers!=null)
            foreach (User user in currentusers)
                if ( user.Login!=null && user.Login.Equals(login) && user.Pass.Equals(pass))
                {
                    var l = new Models.Login();
                   
                    l.Token = AesOperation.encry(AesOperation.mykey , login +pass);
                        l.Id = user.Id;
                        return this.Ok(l);
                }
                
                    return this.BadRequest("Nie prawidłowe login lub hasło");
        }

        public IHttpActionResult Get(string token)
        {
            return this.Ok(CheckToken(token));
        }

        public static bool CheckToken(string token)
        {
            var currentusers = UserController.read_file_json();
            string inscription = AesOperation.dencry(AesOperation.mykey, token);
            if (currentusers != null)
                foreach (var user in currentusers)
                if (user.Login != null && inscription != null && inscription.Contains(user.Login) == true && inscription.Contains(user.Pass) == true)
                    return true;

            return false;
        }
}
    }