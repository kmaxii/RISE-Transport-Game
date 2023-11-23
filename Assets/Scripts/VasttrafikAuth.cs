using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using UnityEngine;

public class VasttrafikAuth : MonoBehaviour
{

    [SerializeField] private string accessToken;
    public static JourneysApi APIInstance { get; private set; }
    
    private void Awake()
    { 
        if (APIInstance == null)
        {
            Initalize();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Initalize()
    {
        Configuration config = new Configuration();
        config.BasePath = "https://ext-api.vasttrafik.se/pr/v4";
        // Configure OAuth2 access token for authorization: auth
        config.AccessToken = accessToken;

        
        APIInstance = new JourneysApi(config);
    }
}
