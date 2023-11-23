using System.Collections.Generic;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;
using UnityEngine;

public class VasttrafikTest : MonoBehaviour
{
    private void Start()
    {

        var detailsReference =
            "eyJUIjpbeyJSIjoiMXwyMDExMHw0MHw4MHwyMzExMjAyMyIsIk8iOjEzLCJEIjoxNSwiSSI6MH0seyJSIjoiMXw4OTAzfDN8ODB8MjMxMTIwMjMiLCJPIjoxNSwiRCI6MTcsIkkiOjJ9XSwiQyI6W3siUiI6Ikh8MXxXJEE9MUBPPUhqYWxtYXIgQnJhbnRpbmdzcGxhdHNlbiwgR8O2dGVib3JnQEw9MzE4MDAwOEBhPTEyOEAkQT0xQE89SGphbG1hciBCcmFudGluZ3NwbGF0c2VuLCBHw7Z0ZWJvcmdATD0zMTgwMDA1QGE9MTI4QCQyMDIzMTEyMzE0NDUkMjAyMzExMjMxNDQ4JCQkMSQkJCQkJHwjVkUjMiNDRiMxMDAjQ0EjMCNDTSMwI1NJQ1QjMCNBTSM5NyNBTTIjMCNSVCMxNSMiLCJJIjoxfV19";
        var includes = new List<VTApiPlaneraResaWebV4ModelsJourneyDetailsIncludeType>(); // List<VTApiPlaneraResaWebV4ModelsJourneyDetailsIncludeType>? | The additional information to include in the response. (optional) 
        var channelIds = new List<int>(); // List<int>? | List of channel ids to include if 'ticketsuggestions' is set in the 'includes' parameter. Optional parameter. (optional) 
        var productTypes = new List<int>(); // List<int>? | List of product type ids to include if 'ticketsuggestions' is set in the 'includes' parameter. Optional parameter. (optional) 
        var travellerCategories = new List<VTApiPlaneraResaCoreModelsTravellerCategory>(); // List<VTApiPlaneraResaCoreModelsTravellerCategory>? | List of traveller category ids to include if 'ticketsuggestions' is set in the 'includes' parameter. Optional parameter. (optional) 
        
        
        try
        {
            // Returns details about a journey.
            VTApiPlaneraResaWebV4ModelsJourneyDetailsJourneyDetailsApiModel result = VasttrafikAuth.APIInstance.JourneysDetailsReferenceDetailsGet(detailsReference, includes, channelIds, productTypes, travellerCategories);
            Debug.Log(result);
           // var res = VasttrafikAuth.APIInstance.JourneysGet()
        }
        catch (ApiException e)
        {
            Debug.Log("Exception when calling JourneysApi.JourneysDetailsReferenceDetailsGet: " + e.Message );
            Debug.Log("Status Code: "+ e.ErrorCode);
            Debug.Log(e.StackTrace);
        }

    }
    
}