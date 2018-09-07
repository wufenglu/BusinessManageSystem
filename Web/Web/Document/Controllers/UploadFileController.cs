using Document.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Document.Controllers
{
    [Authorize]
    [RoutePrefix("api/UploadFile")]
    public class UploadFileController : ApiController
    {
        [HttpGet]
        public UserInfoViewModel Upload()
        {
            return new UserInfoViewModel
            {
                Email = "hello",
                HasRegistered = true
            };
        }        
    }
}
