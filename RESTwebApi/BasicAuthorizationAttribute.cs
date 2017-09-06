using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;// namespace required for AuthorizationFilterAttribute  class
using System.Web.Http.Filters; 
using System.Net.Http; //create response
using System.Net;      //httpstatus
using System.Text; //for encoding

using System.Threading;
using System.Security.Principal;   //generic principal

namespace RESTwebApi
{
    // this class will create 
    public class BasicAuthorizationAttribute: AuthorizationFilterAttribute 
    {//check the header for authorization parameters from client
        //we need HttpActionContext for this
        //also we need a mthod to override from AuthorizationFilterAttribute  class
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            //check if the request header is not having the values
            if (actionContext.Request.Headers.Authorization == null)
            {
                //create a http response
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            else
            {
                //retrive the username and pasword from headers
                string authenticationToken = actionContext.Request.Headers.Authorization.Parameter;
                //decode it
                string decodedToken = Encoding.UTF8.GetString(Convert.FromBase64String(authenticationToken));
                //split it in :
                string[] userPass = decodedToken.Split(':');
                string userName = userPass[0];
                string password = userPass[1];
                //check for valid 
                if(OrderSecurity.login(userName,password))
                {
                    Thread.CurrentPrincipal= new GenericPrincipal(new GenericIdentity(userName),null);
                }
                else{
                     actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                }
            }
        }

    }
}