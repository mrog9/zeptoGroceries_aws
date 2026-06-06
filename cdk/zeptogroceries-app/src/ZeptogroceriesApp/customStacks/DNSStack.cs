using Amazon.CDK;
using Constructs;

public class DNSStack : Stack
{
    
    private StaticFilesStorage myStaticPages;
    private WebsiteFront myFront;

    public DNSStack(Construct con, string id, StackProps props):base(con, id, props){
        myStaticPages = new StaticFilesStorage(this, "myS3staticFilesStorage");
        myFront = new WebsiteFront(this, "myCloudFront", myStaticPages);

    }


}