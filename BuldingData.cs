using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day2_2
{
    internal class BuldingData
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int ReportingYear { get; set; }
        [JsonProperty("global_id")]
        public int GlobalId { get; set; }
        public string ReportingPeriod { get; set; }
        public float PlannedIndicators { get; set; }
        public float ActualIndicators { get; set; }
        public string PlanImplementation { get; set; }
        public string ProjectedImplementation { get; set; }
        public string NOTE { get; set; }

        public TerritoryType TerritoryType
        {
            get
            {
                if (Name.ToLower().Contains("новой"))
                    return TerritoryType.New;
                else if (Name.ToLower().Contains("старой"))
                    return TerritoryType.Old;
                else
                    return TerritoryType.All;
            }
        }
    }
    public enum TerritoryType
    {
        Old,
        New,
        All
    }
}
