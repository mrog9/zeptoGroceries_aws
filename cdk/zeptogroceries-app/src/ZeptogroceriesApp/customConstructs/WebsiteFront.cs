using Amazon.CDK.AWS.CloudFront;
using Amazon.CDK.AWS.CloudFront.Origins;
using Amazon.CDK.AWS.ElasticLoadBalancingV2;
using Amazon.CDK.AWS.S3;
using Constructs;

public class WebsiteFront : Construct
{
    private CfnDistribution myDist;

    public WebsiteFront(Construct con, string id, StaticFilesStorage myStorage, ALB myALB) : base(con, "myCloudFrontConstruct")
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
                        DomainName = myStorage.GetMyBucket().BucketRegionalDomainName,
                        S3OriginConfig = new CfnDistribution.S3OriginConfigProperty
                        {
                            OriginAccessIdentity = ""
                        },
                        OriginAccessControlId = oac.AttrId
                    },
                    new CfnDistribution.OriginProperty
                    {
                        
                        Id= "ALBOrigin",
                        DomainName = myALB.GetALB().LoadBalancerDnsName,
                        CustomOriginConfig = new CfnDistribution.CustomOriginConfigProperty
                        {
                            OriginProtocolPolicy = "https-only",
                            HttpsPort=443,
                            OriginSslProtocols = new [] {"TLSv1.2"}
                        }

                    }
                },
                DefaultCacheBehavior = new CfnDistribution.DefaultCacheBehaviorProperty
                {
                    TargetOriginId = "S3Origin",
                    ViewerProtocolPolicy = "redirect-to-https",
                    AllowedMethods = new [] { "GET", "HEAD" },
                    CachedMethods = new [] { "GET", "HEAD" },
                    CachePolicyId = "658327ea-f89d-4fab-a63d-7e88639e58f6"
                },
                CacheBehaviors = new []
                {
                    new CfnDistribution.CacheBehaviorProperty
                    {
                        PathPattern = "/users/*",
                        TargetOriginId = "ALBOrigin",
                        ViewerProtocolPolicy = "redirect-to-https",
                        AllowedMethods = new [] { "GET", "POST", "HEAD"},
                        CachedMethods = new [] { "GET", "HEAD" },
                        CachePolicyId = "4135ea2d-6df8-4f9a-9e6a-2f0c0f1d3f3f" // Managed policy: CachingDisabled
                    }
                }

            }
        });


    }

    public CfnDistribution GetDistr(){return myDist;}

}