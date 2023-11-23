using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

public class Test
{
    public static void Main()
    {

        Configuration config = new Configuration();
        config.BasePath = "https://ext-api.vasttrafik.se/pr/v4";
        // Configure OAuth2 access token for authorization: auth
        config.AccessToken = "YOUR_ACCESS_TOKEN";

        var apiInstance = new JourneysApi(config);
        var detailsReference = "detailsReference_example";  // string | The reference to the journey, received from the search journeys query. A detailsReference is only valid during the same day as it was generated.
        var includes = new List<VTApiPlaneraResaWebV4ModelsJourneyDetailsIncludeType>(); // List<VTApiPlaneraResaWebV4ModelsJourneyDetailsIncludeType>? | The additional information to include in the response. (optional) 
        var channelIds = new List<int>(); // List<int>? | List of channel ids to include if 'ticketsuggestions' is set in the 'includes' parameter. Optional parameter. (optional) 
        var productTypes = new List<int>(); // List<int>? | List of product type ids to include if 'ticketsuggestions' is set in the 'includes' parameter. Optional parameter. (optional) 
        var travellerCategories = new List<VTApiPlaneraResaCoreModelsTravellerCategory>(); // List<VTApiPlaneraResaCoreModelsTravellerCategory>? | List of traveller category ids to include if 'ticketsuggestions' is set in the 'includes' parameter. Optional parameter. (optional) 
        
        
        try
        {
            // Returns details about a journey.
            VTApiPlaneraResaWebV4ModelsJourneyDetailsJourneyDetailsApiModel result = apiInstance.JourneysDetailsReferenceDetailsGet(detailsReference, includes, channelIds, productTypes, travellerCategories);
            Debug.WriteLine(result);
        }
        catch (ApiException e)
        {
            Debug.Print("Exception when calling JourneysApi.JourneysDetailsReferenceDetailsGet: " + e.Message );
            Debug.Print("Status Code: "+ e.ErrorCode);
            Debug.Print(e.StackTrace);
        }

    }
}