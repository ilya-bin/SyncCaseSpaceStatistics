using Acumatica.Support.Programs.SyncCaseSpaceStatistics.DbEntities;
using Simple.OData.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Acumatica.Support.Integration.Internal.OData
{
    // TODO Free from tight coupling with DbEntities
    public class InternalOdataClient
    {
        private ODataClientSettings _oDataClientSettings;
        private ODataClient _odataClient;

        public InternalOdataClient(ODataClientSettings oDataClientSettings = null)
        {
            //initsettings
            if (oDataClientSettings == null)
            {
                _oDataClientSettings = new ODataClientSettings();
                _oDataClientSettings.IgnoreUnmappedProperties = false;
                _oDataClientSettings.NameMatchResolver = new InternalCasesMatchResolver();
                _oDataClientSettings.PayloadFormat = ODataPayloadFormat.Json;
                var config = ConfigurationManager.AppSettings;
                _oDataClientSettings.BaseUri = new Uri(config["InternalUrl"] + config["oDataUrl"]);
                var usernName = config["InternalLogin"];
                var password = config["InternalPassword"];
                if (Debugger.IsAttached)
                {
                    usernName = Environment.GetEnvironmentVariable(
                        "internal_username", EnvironmentVariableTarget.User);
                    password = Environment.GetEnvironmentVariable(
                        "internal_password", EnvironmentVariableTarget.User);
                }
                _oDataClientSettings.Credentials = new NetworkCredential(usernName, password);
            }

            _odataClient = new ODataClient(_oDataClientSettings);
            
        }

        public async Task<List<Case>> GetAllCasesFromInternalGIAsync()
        {
            var client = new ODataClient(_oDataClientSettings);
            var result = await client
                .For<Case>()
                //.Filter(c => c.CaseID == "077777" || c.CaseID == "077776")
                //.Top(100000)
                .Select(c => c.CaseID)
                .Select(c => c.Status)
                .Select(c => c.ClosingDate)
                .Select(c => c.LastModifiedOn)
                .Select(c => c.Owner)
                .Select(c => c.OwnerName)
                .Select(c => c.SecondaryOwner)
                .Select(c => c.Subject)
                .OrderBy(c => c.CaseID)
                .FindEntriesAsync();
            return result.ToList();
        }

        public async Task<List<CaseDTO>> GetAllNewCasesAsync()
        {
            var client = new ODataClient(_oDataClientSettings);
            var result = await client
                .For<CaseDTO>()
                .Filter(c => c.Status == "New")
                .Select(c => c.CaseID)
                .Select(c => c.Status)
                .Select(c => c.ClosingDate)
                .Select(c => c.LastModifiedOn)
                .Select(c => c.Owner)
                .Select(c => c.OwnerName)
                .Select(c => c.SecondaryOwner)
                .Select(c => c.Subject)
                .Select(c => c.Severity)
                .Select(c => c.Priority)
                .Select(c => c.DateReported)
                .FindEntriesAsync();
            return result.ToList();
        }

        public async Task<List<CaseDTO>> GetCasesTodayAsync()
        {
            var client = new ODataClient(_oDataClientSettings);
            var result = await client
                .For<CaseDTO>()
                .Filter(c => (c.DateReported >= DateTime.Today 
                    || c.LastIncomingActivity >= DateTime.Today)
                    && c.ClassID == "PARTNERREQ")
                .Select(c => c.CaseID)
                .Select(c => c.Status)
                .Select(c => c.Reason)
                .Select(c => c.ClosingDate)
                .Select(c => c.LastModifiedOn)
                .Select(c => c.Owner)
                .Select(c => c.OwnerName)
                .Select(c => c.SecondaryOwner)
                .Select(c => c.Subject)
                .Select(c => c.Severity)
                .Select(c => c.Priority)
                .Select(c => c.DateReported)
                .Select(c => c.LastIncomingActivity)
                .Select(c => c.ClassID)
                .FindEntriesAsync();
            return result.ToList();
        }

        private class InternalCasesMatchResolver : INameMatchResolver
        {

            public bool IsMatch(string actualName, string requestedName)
            {
                actualName = actualName.Split('.').Last();
                requestedName = requestedName.Split('.').Last();

                return actualName == requestedName ||
                       (actualName == "CR-Cases" && requestedName == "Case")
                       || (actualName == "CR-Cases" && requestedName == "CaseDTO");
            }
        }
    }
}
