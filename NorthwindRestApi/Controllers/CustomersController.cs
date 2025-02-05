using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NorthwindRestApi.Models;

namespace NorthwindRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        // Alustetaan tietokantayhteys
        NorthwindOriginalContext db = new NorthwindOriginalContext();

        // Hakee kaikki asiakkaat
        [HttpGet]
        public ActionResult GetAllCustomers()
        {
            try
            {
                var asiakkaat = db.Customers.ToList();
                return Ok(asiakkaat);
            }
            catch (Exception ex)
            {
                return BadRequest("Tapahtui virhe. Lue lisää: " + ex.InnerException); //InnerException on tarkempi kuin esim. Message
            }
        }


        // Hakee yhden asiakkaan pääavaimella
        [HttpGet("{id}")]
        public ActionResult GetOneCustomerById(string id)
        {
            try
            {
                var asiakas = db.Customers.Find(id);
                if (asiakas != null)
                {
                    return Ok(asiakas);
                }
                else
                {
                    return NotFound($"Asiakasta id:llä {id} ei löydy."); // string interpolation
                }
            }
            catch (Exception e)
            {
                return BadRequest("Tapahtui virhe. Lue lisää: " + e);
            }
        }

        // Uuden lisääminen
        [HttpPost]
        public ActionResult AddNew([FromBody] Customer cust) //Ottaa vastaan Http -body -osiosta Customer (vahva tyyppi) -luokkaa vastaavan olion
        {
            try
            {
                db.Customers.Add(cust);
                db.SaveChanges();
                return Ok("Lisättiin uusi asiakas " + cust.CompanyName);
            }
            catch (Exception e)
            {
                return BadRequest("Tapahtui virhe. Lue lisää: " + e.InnerException);
            }
        }

    }
}
