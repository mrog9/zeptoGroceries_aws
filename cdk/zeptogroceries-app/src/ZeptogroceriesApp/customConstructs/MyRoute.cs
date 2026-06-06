using Amazon.CDK;
using Amazon.CDK.AWS.Route53;
using Constructs;

public class MyRoute : Construct
{

    private PublicHostedZone hostedZone;
    
    public MyRoute(Construct con, string id): base(con, "myRouteConstruct")
    {
        
        hostedZone = new PublicHostedZone(this, id, new PublicHostedZoneProps{
            
            ZoneName= "zeptogroceries-notreal.com"

        });

    }

}