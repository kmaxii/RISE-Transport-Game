using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace vasttrafik
{
    
    [Serializable]
    public class JourneyDetails
    {
        public List<TripLeg> tripLegs;
        public List<ConnectionLink> connectionLinks;
        public ArrivalAccessLink arrivalAccessLink;
        public List<TariffZone> tariffZones;
    }
    
    [Serializable]
    public class JourneyResult
    {
        public List<Result> results;
        public Pagination pagination;
        public Links links;
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    [Serializable]
    public class ArrivalAccessLink
    {
        public string transportMode;
        public string transportSubMode;
        public Origin origin;
        public Destination destination;
        public List<Note> notes;
        public int distanceInMeters;
        public string plannedDepartureTime;
        public string plannedArrivalTime;
        public int plannedDurationInMinutes;
        public string estimatedDepartureTime;
        public string estimatedArrivalTime;
        public int estimatedDurationInMinutes;
        public int estimatedNumberOfSteps;
        public List<LinkCoordinate> linkCoordinates;
        public List<Segment> segments;
    }
    
    [Serializable]
    public class CallsOnTripLeg
    {
        public StopPoint stopPoint ;
        public DateTime plannedArrivalTime ;
        public DateTime plannedDepartureTime ;
        public DateTime estimatedDepartureTime ;
        public DateTime estimatedOtherwisePlannedArrivalTime ;
        public DateTime estimatedOtherwisePlannedDepartureTime ;
        public string plannedPlatform ;
        public double latitude ;
        public double longitude ;
        public string index ;
        public bool isOnTripLeg ;
        public bool isTripLegStart ;
        public List<TariffZone> tariffZones ;
        public bool isCancelled ;
        public DateTime? estimatedArrivalTime ;
        public bool? isTripLegStop ;
    }

    [Serializable]
    public class ConnectionLink
    {
        public string transportMode;
        public string transportSubMode;
        public Origin origin;
        public Destination destination;
        public List<Note> notes;
        public int distanceInMeters;
        public string plannedDepartureTime;
        public string plannedArrivalTime;
        public int plannedDurationInMinutes;
        public string estimatedDepartureTime;
        public string estimatedArrivalTime;
        public int estimatedDurationInMinutes;
        public int estimatedNumberOfSteps;
        public List<LinkCoordinate> linkCoordinates;
        public List<Segment> segments;
        public int journeyLegIndex;
    }

    [Serializable]
    public class DepartureAccessLink
    {
        public string transportMode;
        public string transportSubMode;
        public Origin origin;
        public Destination destination;
        public List<Note> notes;
        public int distanceInMeters;
        public string plannedDepartureTime;
        public string plannedArrivalTime;
        public int plannedDurationInMinutes;
        public string estimatedDepartureTime;
        public string estimatedArrivalTime;
        public int estimatedDurationInMinutes;
        public int estimatedNumberOfSteps;
        public List<LinkCoordinate> linkCoordinates;
        public List<Segment> segments;
    }

    [Serializable]
    public class Destination
    {
        public StopPoint stopPoint;
        public string plannedTime;
        public string estimatedTime;
        public string estimatedOtherwisePlannedTime;
        public List<Note> notes;
        public string gid;
        public string name;
        public string locationType;
        public int latitude;
        public int longitude;
    }

    [Serializable]
    public class DestinationLink
    {
        public string transportMode;
        public string transportSubMode;
        public Origin origin;
        public Destination destination;
        public List<Note> notes;
        public int distanceInMeters;
        public string plannedDepartureTime;
        public string plannedArrivalTime;
        public int plannedDurationInMinutes;
        public string estimatedDepartureTime;
        public string estimatedArrivalTime;
        public int estimatedDurationInMinutes;
        public int estimatedNumberOfSteps;
        public List<LinkCoordinate> linkCoordinates;
        public List<Segment> segments;
    }



    [Serializable]
    public class LinkCoordinate
    {
        public int latitude;
        public int longitude;
        public int elevation;
        public bool isOnTripLeg;
        public bool isTripLegStart;
        public bool isTripLegStop;
    }

    [Serializable]
    public class Links
    {
        public string previous;
        public string next;
        public string current;
    }

    [Serializable]
    public class Note
    {
        public string type;
        public string severity;
        public string text;
    }

    [Serializable]
    public class Occupancy
    {
        public string level;
        public string source;
    }

    [Serializable]
    public class Origin
    {
        public string gid;
        public string name;
        public string locationType;
        public int latitude;
        public int longitude;
        public string plannedTime;
        public string estimatedTime;
        public string estimatedOtherwisePlannedTime;
        public List<Note> notes;
        public StopPoint stopPoint;
    }

    [Serializable]
    public class Pagination
    {
        public int limit;
        public int offset;
        public int size;
    }

    [Serializable]
    public class Result
    {

        
        public string reconstructionReference;
        public string detailsReference;
        public DepartureAccessLink departureAccessLink;
        public List<TripLeg> tripLegs;
        public List<ConnectionLink> connectionLinks;
        public ArrivalAccessLink arrivalAccessLink;
        public DestinationLink destinationLink;
        public bool isDeparted;
        public Occupancy occupancy;
        public string LeaveTime
        {
            get
            {
                
                if (tripLegs.Count != 0)
                    return tripLegs[0]?.origin?.plannedTime;
                
                return destinationLink.origin.plannedTime;
            }
        }

        public string DestinationTime
        {
            get
            {
                if (tripLegs.Count != 0)
                    return tripLegs[^1]?.destination?.plannedTime;
                return destinationLink.destination.plannedTime;
            }
        }

        public int SwitchesAmount => tripLegs.Count - 1;
        
    }


    [Serializable]
    public class Segment
    {
        public string name;
        public string maneuver;
        public string orientation;
        public string maneuverDescription;
        public int distanceInMeters;
    }

    [Serializable]
    public class ServiceJourney
    {
        public string gid;
        public string direction;
        public Line line;
        public string number;
        public List<ServiceJourneyCoordinate> serviceJourneyCoordinates;
    }
    
    [Serializable]
    public class Line
    {
        public string name;
        public string backgroundColor;
        public string foregroundColor;
        public string borderColor;
        public string transportMode;
        public string transportSubMode;
        public string shortName;
        public string designation;
        public bool isWheelchairAccessible;
    }

    [Serializable]
    public class ServiceJourneyCoordinate
    {
        public double latitude;
        public double longitude;
        public bool isOnTripLeg;
        public bool isTripLegStart;
    }
    
    [Serializable]
    public class StopArea
    {
        public string gid;
        public string name;
        public int latitude;
        public int longitude;
        public TariffZone tariffZone1;
        public TariffZone tariffZone2;
    }

    [Serializable]
    public class StopPoint
    {
        public string gid;
        public string name;
        public string platform;
        public double latitude;
        public double longitude;
        public StopArea stopArea;
    }

    [Serializable]
    public class TariffZone
    {
        public string gid;
        public string name;
        public int number;
        public string shortName;
    }
    

    [Serializable]
    public class TripLeg
    {
        public List<ServiceJourney> serviceJourneys;
        public List<CallsOnTripLeg> callsOnTripLeg;
        public List<TripLegCoordinate> tripLegCoordinates;
        public List<TariffZone> tariffZones;
        public Origin origin;
        public Destination destination;
        public bool isCancelled;
        public bool isPartCancelled;
        public ServiceJourney serviceJourney;
        public List<Note> notes;
        public int estimatedDistanceInMeters;
        public int plannedConnectingTimeInMinutes;
        public int estimatedConnectingTimeInMinutes;
        public bool isRiskOfMissingConnection;
        public string plannedDepartureTime;
        public string plannedArrivalTime;
        public int plannedDurationInMinutes;
        public string estimatedDepartureTime;
        public string estimatedArrivalTime;
        public int estimatedDurationInMinutes;
        public string estimatedOtherwisePlannedArrivalTime;
        public string estimatedOtherwisePlannedDepartureTime;
        public Occupancy occupancy;
        public int journeyLegIndex;
    }
    
    [Serializable]
    public class TripLegCoordinate
    {
        public double latitude;
        public double longitude;
        public bool isOnTripLeg;
        public bool isTripLegStart;
        public bool isTripLegStop;
    }
}