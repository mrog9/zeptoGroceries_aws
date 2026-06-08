using Amazon.CDK;
using Constructs;

namespace ZeptogroceriesApp
{
    public class ZeptogroceriesAppStack : Stack
    {
        internal ZeptogroceriesAppStack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
        {
            // The code that defines your stack goes here

            new GitHubRole(this, "myGitHubRole");
            var myStaticPages = new StaticFilesStorage(this, "myS3staticFilesStorage");
            var myFront = new WebsiteFront(this, "myCloudFront", myStaticPages.getMyBucket());
            
        }
    }
}
