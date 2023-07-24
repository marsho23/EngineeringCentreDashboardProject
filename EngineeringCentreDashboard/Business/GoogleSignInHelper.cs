//using EngineeringCentreDashboard.Interfaces;
//using Google.Apis.Auth.OAuth2;
//using Google.Apis.Auth.OAuth2.Flows;
//using Google.Apis.Oauth2.v2;
//using Google.Apis.Oauth2.v2.Data;
//using System.Threading.Tasks;

//namespace EngineeringCentreDashboard.Business
//{
//    public class GoogleSignInHelper : IGoogleSignInHelper
//    {
//        private readonly GoogleClientSecrets _clientSecrets;
//        private readonly string _redirectUri;

//        public GoogleSignInHelper(string clientSecretsPath, string redirectUri)
//        {
//            _clientSecrets = GoogleClientSecrets.FromFile(clientSecretsPath);
//            _redirectUri = redirectUri;
//        }

//        public string GetAuthorizationUrl()
//        {
//            var initializer = new GoogleAuthorizationCodeFlow.Initializer
//            {
//                ClientSecrets = _clientSecrets.Secrets,
//                Scopes = new[] { Oauth2Service.Scope.UserinfoEmail, Oauth2Service.Scope.UserinfoProfile },
//            };

//            var flow = new GoogleAuthorizationCodeFlow(initializer);
//            var authUrl = flow.CreateAuthorizationCodeRequest(_redirectUri).Build().ToString();
//            return authUrl;
//        }

//        public async Task<UserCredential> GetUserCredential(string authorizationCode)
//        {
//            var initializer = new GoogleAuthorizationCodeFlow.Initializer
//            {
//                ClientSecrets = _clientSecrets.Secrets,
//                Scopes = new[] { Oauth2Service.Scope.UserinfoEmail, Oauth2Service.Scope.UserinfoProfile },
//            };

//            var flow = new GoogleAuthorizationCodeFlow(initializer);
//            var tokenResponse = await flow.ExchangeCodeForTokenAsync(authorizationCode, _redirectUri);

//            return new UserCredential(flow, "user", tokenResponse);
//        }

//        public async Task<Userinfo> GetUserInfo(UserCredential userCredential)
//        {
//            var oauth2Service = new Oauth2Service(new Google.Apis.Services.BaseClientService.Initializer
//            {
//                HttpClientInitializer = userCredential,
//            });

//            return await oauth2Service.Userinfo.Get().ExecuteAsync();
//        }
//    }
//}
