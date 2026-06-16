using Amazon.CDK;
using Amazon.CDK.AWS.ECS;
using Constructs;

namespace ZeptogroceriesApp
{
    public class ZeptogroceriesAppStack : Stack
    {
        internal ZeptogroceriesAppStack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
        {
            // The code that defines your stack goes here

            new GitHubRole(this, "myGitHubRole");
            var myECR = new ECR(this, "ZeptoImageRepo");
            var myStaticPages = new StaticFilesStorage(this, "myS3staticFilesStorage");
            var myVPC = new VPC(this, "myVPC");
            Cluster cluster = new Cluster(this, "myCluster", new ClusterProps
            {
                Vpc = myVPC.GetVPC()
            });

            var myALB = new ALB(this, "myALB", cluster);

            var myFGservice = new Fargate(this, "myUsersFargateService", "UsersService", myECR, myALB, cluster);

            myALB.AddListener("UsersServiceTarget", myFGservice);

            var myFront = new WebsiteFront(this, "myCloudFront", myStaticPages, myALB);
            myStaticPages.SetBucketPolicy(myFront.GetDistr());
            var dbInstanceObj = new DatabaseInstanceConstruct(this, "myDBinstance", cluster, myFGservice);
            
        }
    }
}
