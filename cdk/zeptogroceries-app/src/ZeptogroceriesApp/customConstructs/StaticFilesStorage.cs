using System.Collections.Generic;
using Amazon.CDK;
using Amazon.CDK.AWS.IAM;
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
            RemovalPolicy= RemovalPolicy.DESTROY,
            AutoDeleteObjects=true
            

        });

        myBucket.AddToResourcePolicy(new PolicyStatement(new PolicyStatementProps
        {
            Effect = Effect.ALLOW,
            Actions = new [] { "s3:GetObject" },
            Resources = new [] { myBucket.ArnForObjects("*") },
            Principals = new []
            {
                new ServicePrincipal("cloudfront.amazonaws.com")
            },
            Conditions = new Dictionary<string, object>
            {
                {
                    "StringEquals", new Dictionary<string, object>
                    {
                        { "AWS:SourceArn", "arn:aws:cloudfront::749625536154:distribution/E1S96GKIRQGXTE" }
                    }
                }
            }
        }));


    }

    public Bucket getMyBucket(){return myBucket;}


}