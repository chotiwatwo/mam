using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MamApi.Helpers;
using MamApi.Models;
using MamApi.Models.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MamApi.Controllers
{
    [Produces("application/json")]
    [Route("api/messages")]
    public class MessagesController : Controller
    {
        [HttpPost("send")]
        public IActionResult SendFirebaseCloudMessage([FromBody] FCMMessageResource messageResource)
        {
            var httpHelper = new HttpHelper();

            string fcmPath = "https://fcm.googleapis.com";
            string fcmResource = "/fcm/send";

            /*
             {
                "message": {
                    "token": "abcde",
                    "data": {
                        "title": "NCB",
                        "appId": "0161000999",
                        "body": "Reject",
                        "senderDepartment": "Credit Checking",
                        "senderUsername": "กิตต์รวี",
                        "sentTime": "2018-06-07T13:05:07.1211272+07:00",
                        "actionName": "NCB"
                    }
                }
            }
            */


            string res = httpHelper
                .ConsumeWebApiViaPost(fcmPath, fcmResource, messageResource);
            
            return Ok(res);
        }

        #region Create FCM Message
        //var fcmMessage = new FCMMessageResource();

        //fcmMessage.message = new Message
        //{
        //    Token = "abcde",
        //    Data = new MessageInfo
        //    {
        //        Title = "NCB",
        //        AppId = "0161000999",
        //        Body = "Reject",
        //        SenderDepartment = "Credit Checking",
        //        SenderUsername = "กิตต์รวี",
        //        SentTime = DateTime.Now,
        //        ActionName = "NCB"

        //        /*
        //                             "title" : "NCB",
        //                            "appId": "0161000999"
        //              "body" : "Reject",
        //                            "SenderDepartment": "Credit Checking",
        //                            "SenderUsername" : "กิตต์รวี",
        //                            "SentTime": "2018-06-07T00:00:00",
        //                            "actionName" : "NCB"
        //                            */

        //    }
        //};
        #endregion

    }


}