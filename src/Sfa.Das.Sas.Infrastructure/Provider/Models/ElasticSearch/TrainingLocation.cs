using Nest;

namespace Sfa.Das.Sas.Indexer.Infrastructure.Provider.Models.ElasticSearch
{
    public class TrainingLocation
    {
        public int LocationId { get; set; }

        public string LocationName { get; set; }

        [GeoShape]
        public CircleGeoShape Location { get; set; }

        public Address Address { get; set; }

        [GeoPoint]
        public GeoCoordinate LocationPoint { get; set; }
    }
}