using Amazon.CDK.AWS.CloudFront;
using Amazon.CDK.AWS.CloudFront.Origins;
using Amazon.CDK.AWS.S3;
using Constructs;

public class WebsiteFront : Construct
{
    private CfnDistribution myDist;

    public WebsiteFront(Construct con, string id, Bucket myBucket) : base(con, "myCloudFrontConstruct")
    {
        var oac = new CfnOriginAccessControl(this, "OAC", new CfnOriginAccessControlProps
        {
            OriginAccessControlConfig = new CfnOriginAccessControl.OriginAccessControlConfigProperty
            {
                Name = "FrontendOAC",
                OriginAccessControlOriginType = "s3",
                SigningBehavior = "always",
                SigningProtocol = "sigv4"
            }
        });

        
       myDist = new CfnDistribution(this, id, new CfnDistributionProps
        {
            DistributionConfig = new CfnDistribution.DistributionConfigProperty
            {
                Enabled = true,
                DefaultRootObject = "index.html",
                Origins = new []
                {
                    new CfnDistribution.OriginProperty
                    {
                        Id = "S3Origin",
                        DomainName = myBucket.BucketRegionalDomainName,
                        S3OriginConfig = new CfnDistribution.S3OriginConfigProperty
                        {
                            OriginAccessIdentity = ""
                        },
                        OriginAccessControlId = oac.AttrId
                    }
                },
                DefaultCacheBehavior = new CfnDistribution.DefaultCacheBehaviorProperty
                {
                    TargetOriginId = "S3Origin",
                    ViewerProtocolPolicy = "redirect-to-https",
                    AllowedMethods = new [] { "GET", "HEAD" },
                    CachedMethods = new [] { "GET", "HEAD" },
                    CachePolicyId = "658327ea-f89d-4fab-a63d-7e88639e58f6"
                }
            }
        });


    }

    public CfnDistribution getDistr(){return myDist;}

}