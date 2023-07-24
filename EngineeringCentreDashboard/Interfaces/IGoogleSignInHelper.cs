using Google.Apis.Auth.OAuth2;
using Google.Apis.Oauth2.v2.Data;
using System.Threading.Tasks;
using Google.Apis.Oauth2.v2.Data;


namespace EngineeringCentreDashboard.Interfaces
{
    public interface IGoogleSignInHelper
    {
        string GetAuthorizationUrl();
        Task<UserCredential> GetUserCredential(string authorizationCode);
        Task<Userinfo> GetUserInfo(UserCredential userCredential);
    }
}
