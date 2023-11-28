using System;
using System.Collections.Generic;

namespace vasttrafik
{
    [Serializable]
    public class JourneyResult
    {
        public List<Result> results;
        public Links links;
    }

    [Serializable]

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class ConnectionLink
    {
        public int journeyLegIndex ;
        public string transportMode ;
        public string transportSubMode ;
        public Origin origin ;
        public Destination destination ;
        public List<object> notes ;
        public int distanceInMeters ;
        public string plannedDepartureTime ;
        public string plannedArrivalTime ;
        public int plannedDurationInMinutes ;
        public string estimatedDepartureTime ;
        public string estimatedArrivalTime ;
        public int estimatedDurationInMinutes ;
        public int estimatedNumberOfSteps ;
        public List<LinkCoordinate> linkCoordinates ;
    }
    [Serializable]

    public class DepartureAccessLink
    {
        public string transportMode ;
        public string transportSubMode ;
        public Origin origin ;
        public Destination destination ;
        public List<object> notes ;
        public int distanceInMeters ;
        public string plannedDepartureTime ;
        public string plannedArrivalTime ;
        public int plannedDurationInMinutes ;
        public string estimatedDepartureTime ;
        public string estimatedArrivalTime ;
        public int estimatedDurationInMinutes ;
        public int estimatedNumberOfSteps ;
        public List<LinkCoordinate> linkCoordinates ;
    }
    [Serializable]

    public class Destination
    {
        public StopPoint stopPoint ;
        public string plannedTime ;
        public string estimatedTime ;
        public string estimatedOtherwisePlannedTime ;
        public List<object> notes ;
    }
    [Serializable]

    public class Line
    {
        public string shortName ;
        public string designation ;
        public bool isWheelchairAccessible ;
        public string name ;
        public string backgroundColor ;
        public string foregroundColor ;
        public string borderColor ;
        public string transportMode ;
        public string transportSubMode ;
    }
    [Serializable]

    public class LinkCoordinate
    {
        public double latitude ;
        public double longitude ;
    }
    [Serializable]

    public class Links
    {
        public string previous ;
        public string next ;
        public string current ;
    }
    [Serializable]

    public class Note
    {
        public string type ;
        public string severity ;
        public string text ;
    }
    [Serializable]

    public class Origin
    {
        public string gid ;
        public string name ;
        public string locationType ;
        public double latitude ;
        public double longitude ;
        public string plannedTime ;
        public string estimatedTime ;
        public string estimatedOtherwisePlannedTime ;
        public List<object> notes ;
        public StopPoint stopPoint ;
    }

    [Serializable]
    public class Result
    {
        public string reconstructionReference ;
        public string detailsReference ;
        public DepartureAccessLink departureAccessLink ;
        public List<TripLeg> tripLegs ;
        public List<ConnectionLink> connectionLinks ;
        public bool isDeparted ;
        
       public string leaveTime => tripLegs[0].origin.plannedTime;
       public string destinationTime => tripLegs[^1].destination.plannedTime;
    }

    [Serializable]

    public class ServiceJourney
    {
        public string gid ;
        public string direction ;
        public string number ;
        public Line line ;
    }
    [Serializable]

    public class StopArea
    {
        public string gid ;
        public string name ;
        public double latitude ;
        public double longitude ;
        public TariffZone1 tariffZone1 ;
    }
    [Serializable]

    public class StopPoint
    {
        public string gid ;
        public string name ;
        public string platform ;
        public StopArea stopArea ;
    }
    [Serializable]

    public class TariffZone1
    {
        public string gid ;
        public string name ;
        public int number ;
        public string shortName ;
    }
    [Serializable]

    public class TripLeg
    {
        public Origin origin ;
        public Destination destination ;
        public bool isCancelled ;
        public bool isPartCancelled ;
        public ServiceJourney serviceJourney ;
        public List<Note> notes ;
        public string plannedDepartureTime ;
        public string plannedArrivalTime ;
        public int plannedDurationInMinutes ;
        public string estimatedDepartureTime ;
        public string estimatedArrivalTime ;
        public int estimatedDurationInMinutes ;
        public string estimatedOtherwisePlannedArrivalTime ;
        public string estimatedOtherwisePlannedDepartureTime ;
        public int journeyLegIndex ;
        public int? plannedConnectingTimeInMinutes ;
        public int? estimatedConnectingTimeInMinutes ;
        public bool? isRiskOfMissingConnection ;
    }


}