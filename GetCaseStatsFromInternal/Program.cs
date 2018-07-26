using Acumatica.Support.Programs.SyncCaseSpaceStatistics.DbEntities;
using Acumatica.Support.Programs.SyncCaseSpaceStatistics.InternalODataClient;
using Simple.OData.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Acumatica.Support.Programs.GetCaseStatsFromInternal
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var currentTime = DateTime.Now;
            // Get cases from internal
            var internalClient = new InternalOdataClient();
            var cases = await internalClient.GetAllCasesFromInternalGIAsync();

            // update database
            using (var db = new DbSpacesContext())
            {
                db.Database.ExecuteSqlCommand("DELETE [Cases]");

                
                cases.ForEach(c => c.SyncDateTime = currentTime);
                db.Cases.AddRange(cases);
                db.SaveChanges();
            }
        }       
    }
}
