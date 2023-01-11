using Microsoft.AspNetCore.Mvc;
using System.Collections.Specialized;
using System.Net;
using System;
using Newtonsoft.Json;
using TestTask.DataModel;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Web.Helpers;
using Microsoft.Extensions.Hosting;

namespace TestTask.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountInfoController : ControllerBase
    {
        private readonly TestDbContext dataContext;
        private async Task<string> PostInfo(AccountInfo accountInfo)
        {
            Contact contact = new Contact();
            Account account = new Account();
            Incident incident = new Incident();

            if (accountInfo.ContactEmail is not null)
            {
                contact.Email = accountInfo.ContactEmail;
                contact.FirstName = accountInfo.ContactFirstName;
                contact.LastName = accountInfo.ContactLastName;
                contact.Account = new List<Account>();
                account.Incident = new List<Incident>();
                if (accountInfo.IncidentName is null && accountInfo.AccountName is null)
                {
                    await dataContext.Contact.AddAsync(contact);
                    await dataContext.SaveChangesAsync();
                }

                if (accountInfo.AccountName is not null && accountInfo.IncidentName is not null)
                {
                    account.Name = accountInfo.AccountName;
                    account.Contact = contact;
                    contact.Account.Add(account);
                }
                else if (accountInfo.AccountName is not null && accountInfo.IncidentName is null)
                {
                    account.Name = accountInfo.AccountName;
                    account.Contact = contact;
                    contact.Account.Add(account);
                    await dataContext.Account.AddAsync(account);

                    await dataContext.Contact.AddAsync(contact);
                    await dataContext.SaveChangesAsync();
                }

                if (accountInfo.IncidentName is not null && accountInfo.AccountName is not null)
                {
                    incident.Account = account;
                    incident.IncidentName = accountInfo.IncidentName;
                    incident.Description = accountInfo.IncidentDescription;
                    account.Incident.Add(incident);
                    await dataContext.Incident.AddAsync(incident);

                    await dataContext.Account.AddAsync(account);

                    await dataContext.Contact.AddAsync(contact);
                    await dataContext.SaveChangesAsync();
                }
                else if (accountInfo.IncidentName is not null && accountInfo.AccountName is null)
                {
                    await dataContext.Contact.AddAsync(contact);
                    await dataContext.SaveChangesAsync();
                    return "Contact is created. If you want to create Incident, create first Account.";
                }
            }
            else
            {
                return "If you want to add info, create first Contact. If you have a contact, just update your info";
            }
            return "Data sent successfully";
        }
        private async Task<string> UpdateInfoByEmail(AccountInfo accountInfo, string Email, Contact dataByEmail)
        {
            Contact contact = new Contact();
            Account account = new Account();
            Incident incident = new Incident();
            if (accountInfo.ContactEmail is not null)
            {
                dataByEmail.Email = accountInfo.ContactEmail;
                dataByEmail.FirstName = accountInfo.ContactFirstName;
                dataByEmail.LastName = accountInfo.ContactLastName;
                contact.FirstName = dataByEmail.FirstName;
                contact.LastName = dataByEmail.LastName;
                contact.Email = dataByEmail.Email;
                contact.Account = dataByEmail.Account;
                dataByEmail.Account.Clear();
                dataByEmail.Account = new List<Account>();
                dataByEmail.Account.Add(account);
                foreach (var acc in dataByEmail.Account)
                {
                    acc.Incident = new List<Incident>();
                }

                if (accountInfo.IncidentName is null && accountInfo.AccountName is null)
                {
                    foreach (var acc in dataByEmail.Account)
                    {
                        acc.Incident.Add(incident);
                        acc.Contact = contact;
                        if (acc.Name is null)
                        {
                            acc.Name = "Empty";
                        }
                        else
                        {
                            acc.Name = null;
                        }
                        foreach (var inc in acc.Incident)
                        {
                            if (inc.IncidentName is null)
                            {
                                inc.IncidentName = "Empty";
                            }
                            else
                            {
                                inc.IncidentName = null;
                            }
                        }

                    }
                    await dataContext.SaveChangesAsync();
                }

                if (accountInfo.AccountName is not null && accountInfo.IncidentName is not null)
                {
                    foreach (var acc in dataByEmail.Account)
                    {
                        acc.Contact = contact;
                        acc.Name = accountInfo.AccountName;
                        acc.Incident.Add(incident);
                        foreach (var inc in acc.Incident)
                        {
                            inc.Account = account;
                            inc.IncidentName = accountInfo.IncidentName;
                            inc.Description = accountInfo.IncidentDescription;
                            incident.Account = account;
                            incident.IncidentName = accountInfo.IncidentName;
                            incident.Description = accountInfo.IncidentDescription;
                        }
                    }
                    await dataContext.SaveChangesAsync();
                }
                else if (accountInfo.AccountName is not null && accountInfo.IncidentName is null)
                {
                    foreach (var acc in dataByEmail.Account)
                    {
                        acc.Name = accountInfo.AccountName;
                        acc.Contact = contact;
                        acc.Incident.Add(incident);
                        foreach (var inc in acc.Incident)
                        {
                            if (inc.IncidentName is null)
                            {
                                inc.IncidentName = "Empty";
                            }
                            else
                            {
                                inc.IncidentName = null;
                            }
                        }
                    }
                    await dataContext.SaveChangesAsync();
                }
                else if (accountInfo.IncidentName is not null && accountInfo.AccountName is null)
                {

                    foreach (var acc in dataByEmail.Account)
                    {
                        if (acc.Name is null)
                        {
                            acc.Name = "Empty";
                        }
                        else
                        {
                            acc.Name = null;
                        }
                        acc.Contact = contact;
                        acc.Incident.Add(incident);
                        foreach (var inc in acc.Incident)
                        {
                            if (inc.IncidentName is null)
                            {
                                inc.IncidentName = "Empty";
                            }
                            else
                            {
                                inc.IncidentName = null;
                            }
                        }
                    }
                    await dataContext.SaveChangesAsync();
                    return "Contact is created. If you want to create Incident, create first Account.";
                }
            }
            else
            {
                return "If you want to update info, create first Contact. If you have a contact, just update your info";
            }

            return "Data sent successfully";
        }
        public AccountInfoController(TestDbContext dataContext)
        {
            this.dataContext = dataContext;
        }

        [HttpGet("getbyemail/{Email}", Name = "GetAccountInfoByEmail")]
        public async Task<IActionResult> GetByEmail(string Email) // Get that return data of account by AccountName or Email
        {
            var dataEmail = await dataContext.Contact
                .Include(x => x.Account)
                .FirstOrDefaultAsync(x =>
                    x.Email.Equals(Email));
            if (dataEmail is not null)
            {
                var count = 0;
                string result = $"First Name: {(dataEmail.FirstName is not null ? dataEmail.FirstName : "No info")}\nLast Name: {(dataEmail.LastName is not null ? dataEmail.LastName : "No info")}\nEmail: {dataEmail.Email}\n";
                foreach (var account in dataEmail.Account)
                {
                    count++;
                    result += $"Account Name: {account.Name}\n";
                    var dataIncident = await dataContext.Account
                        .Include(x => x.Incident)
                        .Include(x => x.Contact)
                        .FirstOrDefaultAsync(x =>
                            x.Name.Equals(account.Name));
                    foreach (var incident in dataIncident.Incident)
                    {
                        result += $"¹{count} incident:\nIncident Name: {(incident.IncidentName is not null ? incident.IncidentName : "No info")}\nIncident Description: {(incident.Description is not null ? incident.Description : "No info")}";
                    }
                }
                return Ok(result);
            }
            else
            {
                return BadRequest($"404 ERROR - Email {Email} not found");
            }
        }

        [HttpGet("getbyaccountName/{accountName}", Name = "GetAccountByAccountName")]
        public async Task<IActionResult> GetByAccountName(string accountName) // Get that return data of account by AccountName or Email
        {
            var dataAccountName = await dataContext.Account
                .Include(x => x.Incident)
                .Include(x => x.Contact)
                .FirstOrDefaultAsync(x =>
                    x.Name.Equals(accountName));
            if (dataAccountName is not null)
            {
                var count = 0;
                string result = $"First Name: {(dataAccountName.Contact.FirstName is not null ? dataAccountName.Contact.FirstName : "No info")}\nLast Name: {(dataAccountName.Contact.LastName is not null ? dataAccountName.Contact.LastName : "No info")}\nEmail: {dataAccountName.Contact.Email}\nAccount Name: {(dataAccountName.Name is not null ? dataAccountName.Name : "No info")}\n";
                foreach (var incident in dataAccountName.Incident)
                {
                    count++;
                    result += $"¹{count} incident:\nIncident Name: {(incident.IncidentName is not null ? incident.IncidentName : "No info")}\nIncident Description: {(incident.Description is not null ? incident.Description : "No info")}";
                }
                return Ok(result);
            }
            else
            {
                return BadRequest($"404 ERROR - Account name {accountName} not found");
            }
        }
        [HttpPost("{AccountInfo}")]
        public async Task<IActionResult> Post([FromBody] AccountInfo accountInfo)  // Necessary fields for Contact(Email), Account(Name), Incident(Name)
        {
            if (accountInfo.ContactEmail is null && accountInfo.IncidentName is null && accountInfo.AccountName is null)
            {
                return BadRequest("If you want to create contact, account or incdinet, enter necessary data");
            }

            var result = await PostInfo(accountInfo);
            return Ok(result);
        }
        [HttpPut("/putbyemail/{Email}")]   // You must update info by existing email
        public async Task<IActionResult> Put([FromBody] AccountInfo accountInfo, string Email)
        {
            if (accountInfo.ContactEmail is null)
            {
                return BadRequest("If you want to create contact, account or incdinet, enter necessary data");
            }

            var dataByEmail = await dataContext.Contact
                    .Include(x => x.Account)
                    .FirstOrDefaultAsync(x =>
                        x.Email.Equals(Email));
            if (dataByEmail is not null)
            {
                var result = await UpdateInfoByEmail(accountInfo, Email, dataByEmail);
                return Ok(result);
            }
            else
            {
                return BadRequest("Email is not found, please enter correct");
            }
        }
        [HttpDelete("{Email}", Name = "DeleteAccountInfoByEmail")]
        public async Task<IActionResult> Delete(string Email) // Delete account info by email
        {
            if (Email is null)
            {
                return BadRequest("404 ERROR Enter email for delete");
            }
            var dataByEmail = await dataContext.Contact
                .Include(x => x.Account)
                .FirstOrDefaultAsync(x =>
                    x.Email.Equals(Email));
            if (dataByEmail is not null)
            {
                dataContext.Contact.Remove(dataByEmail);
                await dataContext.SaveChangesAsync();
                return Ok("Delete is done");
            }
            else
            {
                return BadRequest("ERROR Enter email for delete");
            }
        }
        [HttpPut("/putbyAllInfo/{AccountInfo}")]   // You must enter from body Account Info
        public async Task<IActionResult> PutByAccountInfo([FromBody] AccountInfo accountInfo)
        {
            if (accountInfo.AccountName is not null)
            {
                var dataAccountName = await dataContext.Account
                    .Include(x => x.Incident)
                    .Include(x => x.Contact)
                    .FirstOrDefaultAsync(x =>
                        x.Name.Equals(accountInfo.AccountName));

                if (dataAccountName is null)
                {
                    BadRequest($"404 ERROR - Account name {accountInfo.AccountName} not found");
                }
            }

            if (accountInfo.ContactEmail is not null)
            {
                var dataEmail = await dataContext.Contact
                .Include(x => x.Account)
                .FirstOrDefaultAsync(x =>
                    x.Email.Equals(accountInfo.ContactEmail));

                if (accountInfo.AccountName is not null)
                {
                    var dataAccountName = await dataContext.Account
                        .Include(x => x.Incident)
                        .Include(x => x.Contact)
                        .FirstOrDefaultAsync(x =>
                            x.Name.Equals(accountInfo.AccountName));

                    if (dataEmail is not null && dataAccountName is not null)
                    {
                        dataEmail.FirstName = accountInfo.ContactFirstName;
                        dataEmail.LastName = accountInfo.ContactLastName;

                        if (dataEmail.Account.Contains(dataAccountName) is false)
                        {
                            dataEmail.Account.Add(dataAccountName);
                        }
                        await dataContext.SaveChangesAsync();
                        return Ok("Data was changed successfully");
                    }
                    else if (dataEmail is not null && dataAccountName is null)
                    {
                        UpdateInfoByEmail(accountInfo, accountInfo.ContactEmail, dataEmail);
                        return Ok("Data was changed successfully");
                    }
                    else if (dataEmail is null && dataAccountName is not null)
                    {
                        Contact contact = new Contact();
                        contact.FirstName = accountInfo.ContactFirstName;
                        contact.LastName = accountInfo.ContactLastName;
                        contact.Email = accountInfo.ContactEmail;

                        Incident incident = new Incident();
                        incident.IncidentName = accountInfo.IncidentName;
                        incident.Description = accountInfo.IncidentDescription;
                        incident.Account = dataAccountName;

                        Account account = new Account();
                        account.Contact = contact;
                        account.Incident = dataAccountName.Incident;
                        account.Incident.Add(incident);

                        contact.Account = new List<Account>();
                        contact.Account.Add(account);
                        account.Name = $"{accountInfo.AccountName}({contact.Account.Count})";

                        await dataContext.Incident.AddAsync(incident);
                        await dataContext.Account.AddAsync(account);
                        await dataContext.Contact.AddAsync(contact);
                        await dataContext.SaveChangesAsync();
                        return Ok("New contact was created successfully");
                    }
                    else if(dataEmail is null && dataAccountName is null)
                    {
                        await PostInfo(accountInfo);
                        return Ok("New contact was created successfully");
                    }
                }
                else
                {
                    if (dataEmail is not null)
                    {
                        dataEmail.FirstName = accountInfo.ContactFirstName;
                        dataEmail.LastName = accountInfo.ContactLastName;
                        await dataContext.SaveChangesAsync();
                        return Ok("Data was changed successfully");
                    }
                    else
                    {
                        await PostInfo(accountInfo);
                        return Ok("New contact was created successfully");
                    }
                }
            }
            else
            {
                return BadRequest("Email cant be null. Enter email");
            }
            return Ok();
        }
    }
}

