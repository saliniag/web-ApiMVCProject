using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RESTwebApi.Controllers
{
    public class ValuesController : ApiController
    {
        //create a static variable of my 
        public static List<string> myString = new List<string>(){
           "value0","value1", "value2"
        };
        // GET api/values
        public IEnumerable<string> Get()
        {
            return myString;
        }

        // GET api/values/5
        public string Get(int id)
        {
            return myString[id];
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
            myString.Add(value);
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
            myString[id] = value;
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
            myString.Remove(myString[id]);
        }
    }
}
