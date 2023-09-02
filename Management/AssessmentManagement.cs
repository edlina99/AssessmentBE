using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Serialization;
using Assessment.Context;
using Assessment.DataAccess;
using Assessment.DtoModels;
using Azure.Core;
using Newtonsoft.Json;
namespace Assessment.Management
{
    public class AssessmentManagement
    {
        string accessToken="";
        HttpClient client = new HttpClient();
        private DataContext _dataContext;
        private readonly IConfiguration config;

        public AssessmentManagement(DataContext dataContext, IConfiguration configuration) 
        {
            _dataContext = dataContext;
            config = configuration;
        }

        public async Task<string> GetPlatformWellActual()
        {
            try
            {

                string websiteGetData = "http://test-demo.aemenersol.com/api/PlatformWell/GetPlatformWellActual";

               


                using (HttpClient client = new HttpClient())
                {
                    // Set the Bearer token in the Authorization header
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                    // Create and send an HTTP GET request (you can change the HTTP method as needed)
                    HttpResponseMessage response = await client.GetAsync(websiteGetData);

                    if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)

                    {

                        //login to the website
                        string LoginUrl = "http://test-demo.aemenersol.com/api/Account/Login";

                        var user = config["UsernameUser"];
                        var pass = config["Password"];

                        var request = new HttpRequestMessage(HttpMethod.Post, LoginUrl);
                        var content = new StringContent(JsonConvert.SerializeObject(new { username = user, password = pass}), Encoding.UTF8);
                        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                        request.Content = content;

                        var tokenResponse = await client.SendAsync(request);

                        if (tokenResponse.IsSuccessStatusCode)
                        {
                            var tokenContent = await tokenResponse.Content.ReadAsStringAsync();

                            // Deserialize the token JSON and extract the access token
                            accessToken = JsonConvert.DeserializeObject<string>(tokenContent);

                        }

                        await GetPlatformWellActual();
                    }

                    List<PlatformDto> platformDtos = new List<PlatformDto>();
                    platformDtos = JsonConvert.DeserializeObject<List<PlatformDto>>(await response.Content.ReadAsStringAsync());

                    PlatformWellDal platformWellDal = new PlatformWellDal(_dataContext);
                    platformWellDal.DbExecuteNonResult(platformDtos);

                }
                return "Data fetched";
            }
            catch (Exception ex) 
            {
                throw ex;
            }
            finally
            {
                client.Dispose();
            }
        }

        public async Task<string> GetDataPlatformWellDummy()
        {
            string websiteGetData = "http://test-demo.aemenersol.com/api/PlatformWell/GetPlatformWellDummy";

            using (HttpClient client = new HttpClient())
            {
                // Set the Bearer token in the Authorization header
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                // Create and send an HTTP GET request (you can change the HTTP method as needed)
                HttpResponseMessage response = await client.GetAsync(websiteGetData);

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)

                {

                    //login to the website
                    string LoginUrl = "http://test-demo.aemenersol.com/api/Account/Login";

                    var user = config["UsernameUser"];
                    var pass = config["Password"];

                    var request = new HttpRequestMessage(HttpMethod.Post, LoginUrl);
                    var content = new StringContent(JsonConvert.SerializeObject(new { username = user, password = pass }), Encoding.UTF8);
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    request.Content = content;

                    var tokenResponse = await client.SendAsync(request);

                    if (tokenResponse.IsSuccessStatusCode)
                    {
                        var tokenContent = await tokenResponse.Content.ReadAsStringAsync();

                        // Deserialize the token JSON and extract the access token
                        accessToken = JsonConvert.DeserializeObject<string>(tokenContent);

                    }

                    await GetDataPlatformWellDummy();
                }

                List<PlatformDto> platformDtos = new List<PlatformDto>();
                platformDtos = JsonConvert.DeserializeObject<List<PlatformDto>>(await response.Content.ReadAsStringAsync());

                PlatformWellDal platformWellDal = new PlatformWellDal(_dataContext);
                platformWellDal.DbExecuteNonResult(platformDtos);

            }
            return "Data fetched";

        }
    }
}
