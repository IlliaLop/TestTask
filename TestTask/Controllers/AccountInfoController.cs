using Microsoft.AspNetCore.Mvc;
using System.Collections.Specialized;
using System.Net;
using System;
using Newtonsoft.Json;
using TestTask.DataModel;

namespace TestTask.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountInfoController : ControllerBase
    {
        private readonly ILogger<AccountInfoController> _logger;
        private readonly TestDbContext dataContext; 
        public AccountInfoController(ILogger<AccountInfoController> logger, TestDbContext dataContext)
        {
            _logger = logger;
            this.dataContext = dataContext;
        }

        //private bool CheckSimilar(string Email)
        //{
        //    AccountInfo accountInfo;
        //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://localhost:7234/AccountInfo");
        //    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        //    Stream stream = response.GetResponseStream();

        //    using (StreamReader reader = new StreamReader(stream))
        //    {
        //        string sReadData = reader.ReadToEnd();
        //        accountInfo = JsonConvert.DeserializeObject<AccountInfo>(sReadData);
        //    }
        //    response.Close();
        //    stream.Dispose();
        //    response.Dispose();
        //    if (accountInfo.ContactEmail == Email)
        //    {
        //        return false;
        //    }
        //}
        [HttpGet("{AccountName}")]
        public async Task<IActionResult> Get(string AccountName)
        {
            var data = await dataContext.Accounts.FindAsync(AccountName);
            if(data is not null)
            {
                return Ok(data);
            }
            else
            {
                return BadRequest($"Account {AccountName} not found");
            }
        }

        [HttpGet(Name = "GetAccountInfo")]
        public IEnumerable<AccountInfo> Get()
        {
            AccountInfoList accountInfoList;
            return new List<AccountInfo>();
        }

        [HttpPost(Name = "PostAccountInfo")]
        public void Post(AccountInfo accountInfo)
        {
            //accountInfo.ContactEmail
        }
    }
}

