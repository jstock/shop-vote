using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Web.WebPages.OAuth;
using ShopVote.Models;

namespace ShopVote
{
  public static class AuthConfig
  {
    public static void RegisterAuth()
    {
      // To let users of this site log in using their accounts from other sites such as Microsoft, Facebook, and Twitter,
      // you must update this site. For more information visit http://go.microsoft.com/fwlink/?LinkID=252166

      //OAuthWebSecurity.RegisterMicrosoftClient(
      //    clientId: "",
      //    clientSecret: "");

      OAuthWebSecurity.RegisterTwitterClient(
          consumerKey: "7op4AFOp5w1na6ZGvdOrxIuYZ",
         consumerSecret: "0Lg9BYjJykZ3XbMiXCAq9qkdICcNtsS5JIvigyHqJejiWqeBLf");

      OAuthWebSecurity.RegisterFacebookClient(
          appId: "1600290690205054",
          appSecret: "89322c6f46e905708d6f85a9db188a06");
        /*
              OAuthWebSecurity.RegisterGoogleClient(
                 clientId: "000-000.apps.googleusercontent.com",
                 clientSecret: "00000000000");*/
    }
  }
}
