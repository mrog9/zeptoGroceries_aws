using Amazon.CDK.AWS.CloudFront;
using Amazon.CDK.AWS.CloudFront.Origins;
using Amazon.CDK.AWS.S3;
using Constructs;

public class WebsiteFront : Construct
{
    private Distribution myDist;

    public WebsiteFront(Construct con, string id, StaticFilesStorage myStorage) : base(con, "myCloudFrontConstruct")
    {
        
        myDist = new Distribution(this, id, new DistributionProps{
            DefaultBehavior= new BehaviorOptions(){
                
                AllowedMethods=AllowedMethods.ALLOW_GET_HEAD_OPTIONS,
                Origin=S3BucketOrigin.WithOriginAccessControl(myStorage.getMyBucket())

            }
        

        });

    }

}