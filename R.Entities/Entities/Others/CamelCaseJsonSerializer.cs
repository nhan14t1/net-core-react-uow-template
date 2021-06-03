using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace R.Entities.Entities.Others
{
    public sealed class CamelCaseJsonSerializer: JsonSerializer
    {
         public CamelCaseJsonSerializer()
         {
            ContractResolver = new CamelCasePropertyNamesContractResolver();
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            DateTimeZoneHandling = DateTimeZoneHandling.Local;
         }
    }
}
