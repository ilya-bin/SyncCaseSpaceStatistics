using Simple.OData.Client;
using System.Linq;


namespace Acumatica.Support.Programs.SyncCaseDbStatistics
{
    class InternalCasesMatchResolver : INameMatchResolver
    {
                
        public bool IsMatch(string actualName, string requestedName)
        {
            actualName = actualName.Split('.').Last();
            requestedName = requestedName.Split('.').Last();

            return actualName == requestedName ||
                   (actualName == "CR-Cases" && requestedName == "Case");
        }
    }
}
