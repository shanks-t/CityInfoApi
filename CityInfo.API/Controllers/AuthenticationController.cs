using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers;

[Route("api/authentication")]
[ApiController]
public class AuthenticationController : Controller
{
    private readonly IConfiguration _configuration;

    public class AuthenticationRequestBody
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }

    private class CityInfoUser
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }

        public CityInfoUser(
            int userId,
            string username,
            string firstName,
            string lastName,
            string city)
        {
            UserId = userId;
            Username = username;
            FirstName = firstName;
            LastName = lastName;
            City = city;
        }
    }

    public AuthenticationController(IConfiguration configuration)
    {
        _configuration = configuration ?? 
            throw new ArgumentNullException(nameof(configuration));
    }
    
    [HttpPost("authenticate")]
    public ActionResult<string> Authenticate(
        AuthenticationRequestBody authenticationRequestBody)
    {
        // Step 1: validate the username/password
        var user = ValidateUserCredentials(
            authenticationRequestBody.Username, 
            authenticationRequestBody.Password);

        if (user == null)
        {
            return Unauthorized();
        }
        
        // Step 2: create a token
        var securityKey = new SymmetricSecurityKey(
            Encoding.ASCII.GetBytes(_configuration["Authentication:SecretForKey"]));
        var signingCredentials = new SigningCredentials(
            securityKey, SecurityAlgorithms.HmacSha256);
        
        // The claims that
        var claimsForToken = new List<Claim>();
        claimsForToken.Add(new Claim("sub", user.UserId.ToString()));
        claimsForToken.Add(new Claim("given_name", user.FirstName));
        claimsForToken.Add(new Claim("family_name", user.LastName));
        claimsForToken.Add(new Claim("city", user.City));

        var jwtSecurityToken = new JwtSecurityToken(
            _configuration["Authentication:Issuer"],
            _configuration["Authentication:Audience"],
            claimsForToken,
            DateTime.UtcNow,
            DateTime.UtcNow.AddHours(1),
            signingCredentials);

        var tokenToReturn = new JwtSecurityTokenHandler()
            .WriteToken(jwtSecurityToken);

        return Ok(tokenToReturn);
    }

    private CityInfoUser ValidateUserCredentials(string? username, string? password)
    {
       // we don't have a user DB or table. If you have, check the passed-through username/
       // password against what's stored in the database
       //
       // For demo purposes, we assume the credentials are valid
       
       // return a new CityInfoUser (values would normally come from your user DB/table
       return new CityInfoUser(
           1,
           username ?? "",
           "Trey",
           "Shanks",
           "Paris");
    }
}