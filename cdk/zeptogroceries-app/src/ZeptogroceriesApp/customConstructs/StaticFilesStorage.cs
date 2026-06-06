using Amazon.CDK.AWS.KinesisFirehose;
using Amazon.CDK.AWS.S3;
using Constructs;

public class StaticFilesStorage: Construct
{

    private Bucket myBucket;
    
    public StaticFilesStorage(Construct con, string id): base(con, "myS3Construct")
    {
        
        myBucket = new Bucket(this, id, new BucketProps
        {
            
            BucketName= "zeptogroceries-frontend",
            

        });

    }

    public Bucket getMyBucket(){return myBucket;}


}