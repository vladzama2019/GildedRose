using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace GildedRose.Controllers
{
    public class TokenController : Controller
    {
        private const string SECRET_KEY = "abcdef1234567890";
        public static readonly SymmetricSecurityKey SIGNING_KEY = 
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenController.SECRET_KEY));

        /// <summary>
        /// Generates a new token based on the user name
        /// </summary>
        /// <param name="userName">User name</param>
        /// <returns>A new token</returns>
        [HttpGet]
        [Route("api/Token/{userName}")]
        public IActionResult Get(string userName)
        {
            string errorMessage = string.Empty;
            string result = string.Empty;

            // validate that the user name is not blank
            if (!string.IsNullOrEmpty(userName))
            {
                // generate a new token
                result = GenerateToken(userName, ref errorMessage);
            }

            // check if the token was generated without any errors
            if(!string.IsNullOrEmpty(result) && string.IsNullOrEmpty(errorMessage))
            {
                // create result
                return new ObjectResult(result);
            }
            else
            {
                // return the error
                return BadRequest(errorMessage);
            }
        }

        /// <summary>
        /// Generates JWT security token
        /// </summary>
        /// <param name="userName">User name</param>
        /// <param name="errorMessage">Error message</param>
        /// <returns>JWT security token</returns>
        private string GenerateToken(string userName, ref string errorMessage)
        {
            string result = string.Empty;
            SecurityToken token = null;

            try
            {
                token = new JwtSecurityToken(
                    // create claims
                    claims: new Claim[]
                    {
                        new Claim(ClaimTypes.Name, userName)
                    },
                    // set the start date
                    notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                    // set the expiration time
                    expires: new DateTimeOffset(DateTime.Now.AddMinutes(60)).DateTime,
                    // create signing credentials
                    signingCredentials: new SigningCredentials(SIGNING_KEY, SecurityAlgorithms.HmacSha256)
                );

                result = new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                result = string.Empty;
                token = null;
            }

            return result;
        }

    }
}