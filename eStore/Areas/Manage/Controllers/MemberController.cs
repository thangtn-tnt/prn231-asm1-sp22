using BusinessObject;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace eStore.Areas.Manage.Controllers
{

    [Area("Manage")]
    public class MemberController : Controller
    {
        private readonly HttpClient client = null;
        private string MemberUrlApi = string.Empty;
        private readonly JsonSerializerOptions options;

        public MemberController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            MemberUrlApi = "https://localhost:44318/api/Member";

            options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
        }
        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await client.GetAsync(MemberUrlApi);

            string strData = await response.Content.ReadAsStringAsync();

            List<Member> listMembers = !string.IsNullOrEmpty(strData) ? JsonSerializer.Deserialize<List<Member>>(strData, options) : null;

            return View(listMembers);
        }

        public async Task<IActionResult> Create()
        {
            HttpResponseMessage response = await client.GetAsync(MemberUrlApi);

            return View();
        }
    }
}
