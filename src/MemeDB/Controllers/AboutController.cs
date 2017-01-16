using Microsoft.AspNetCore.Mvc;

namespace MemeDB.Controllers
{
    [Route("company/[controller]/[action]")]
    public class AboutController
    {
        public string Phone()
        {
            return "(206) 243-5251";
        }
             
        public string Address()
        {
            return "USA";
        }
    }
}
