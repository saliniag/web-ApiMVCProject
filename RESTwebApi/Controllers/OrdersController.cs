using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Cors;

namespace RESTwebApi.Controllers
{
   //
    public class OrdersController : ApiController
    {
         [EnableCors(origins: "*", headers: "*", methods: "*")]
        /*
        public IEnumerable<Supplier> Get()
        {
            using (OrdersDBEntities entity = new OrdersDBEntities())
            {
               return  entity.Suppliers.ToList();

            } 
        }
         * */
/*
        //with query string
        public HttpResponseMessage Get(string group="a")
        {
            using (OrdersDBEntities entity = new OrdersDBEntities())
            {
                //switch the cases
                switch (group.ToLower())
                {

                    case "a":
                        return Request.CreateResponse(HttpStatusCode.OK,entity.Suppliers.Where((s)=>s.SupplierID <10 ).ToList());
                       
                    case "b":
                        return Request.CreateResponse(HttpStatusCode.OK, entity.Suppliers.Where((s)=>s.SupplierID >10 ).ToList());
                       
                    default:
                        return Request.CreateResponse(HttpStatusCode.BadRequest, "only a and b are accepted");

                }
                //return entity.Suppliers.ToList();

            }
        }
 * */
        /*
        public Supplier Get(int id)
        {
            using (OrdersDBEntities entity = new OrdersDBEntities())
            {
                return entity.Suppliers.FirstOrDefault((s) => s.SupplierID == id);
            } 
        }
         */  //for login authentication
             [BasicAuthorization]
        public HttpResponseMessage Get(string group = "all")
        {
            using (OrdersDBEntities entity = new OrdersDBEntities())
            {
                //get the identity
                string username = Thread.CurrentPrincipal.Identity.Name;
                //switch the cases
                switch (username)
                {

                    case "user1":
                        return Request.CreateResponse(HttpStatusCode.OK, entity.Suppliers.Where((s) => s.SupplierID < 10).ToList());

                    case "user2":
                        return Request.CreateResponse(HttpStatusCode.OK, entity.Suppliers.Where((s) => s.SupplierID > 10).ToList());

                    default:
                        return Request.CreateResponse(HttpStatusCode.BadRequest);

                }
                //return entity.Suppliers.ToList();

            }
        }
         
        public HttpResponseMessage Get(int id)
        {
            using (OrdersDBEntities entity = new OrdersDBEntities())
            {
                var entityValue= entity.Suppliers.FirstOrDefault((s) => s.SupplierID == id);
                 if (entityValue != null)
                 {
                     return Request.CreateResponse(HttpStatusCode.OK, entityValue);
                 }
                 else
                 {
                     return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Supplier with id" +id.ToString() + "not found");
                 }
            } 
        }
/* without any response
        public void Post([FromBody] Supplier supp)
        {
            using (OrdersDBEntities entity = new OrdersDBEntities())
            {
                entity.Suppliers.Add(supp);
                entity.SaveChanges();
            }
        }
 * */
        //[HttpDelete]
      
        public HttpResponseMessage Delete(int suppId)
         {
             try
             {
                 using (OrdersDBEntities entities = new OrdersDBEntities())
                 {

                     var entity = entities.Suppliers.FirstOrDefault((supp) => supp.SupplierID == suppId);
                     if (entity == null)
                     {
                         //the error message
                         return Request.CreateErrorResponse(HttpStatusCode.NotFound, "the id is not present");
                     }
                     else
                     {
                         entities.Suppliers.Remove(entity);
                         entities.SaveChanges();
                         return Request.CreateResponse(HttpStatusCode.OK);
                     }
                 }
             }
             catch (Exception ex)
             {
                 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
             }
         }

        public HttpResponseMessage Post([FromBody] Supplier supp)
        {
            try
            {
                using (OrdersDBEntities entity = new OrdersDBEntities())
                {
                    entity.Suppliers.Add(supp);
                    entity.SaveChanges();
                    //create the message
                    var message = Request.CreateResponse(HttpStatusCode.Created, supp);
                    message.Headers.Location = new Uri(Request.RequestUri + supp.SupplierID.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // implementing delete method


       

        /*
        public void Delete(int suppId)
        {
           using ( OrdersDBEntities entities = new OrdersDBEntities()){
               entities.Suppliers.Remove(entities.Suppliers.FirstOrDefault((supp)=> supp.SupplierID==suppId));
               entities.SaveChanges();
            }
        }
        */


        //put method
        public HttpResponseMessage Put(int id, [FromBody]Supplier supp)
        {
            try
            {
                //get the supplier
                using (OrdersDBEntities entities = new OrdersDBEntities())
                {
                    var supplier = entities.Suppliers.FirstOrDefault((sup) => sup.SupplierID == id);
                    //check if provided supplier is not null
                    if (supplier == null)
                    {
                       return Request.CreateErrorResponse(HttpStatusCode.NotFound, "no supplier ith this id is found");
                    }
                    else
                    {
                        supplier.CompanyName = supp.CompanyName;
                        supplier.PhoneNumber = supp.PhoneNumber;
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }

                }
            }
            catch (Exception ex)
            {
               return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
            
        }

    }
}
