using Amazon.CDK.AWS.ECR;
using Constructs;

public class ECR : Construct
{
    
    private Repository repo;

    public ECR(Construct scope, string id) : base(scope, "myECRConstruct")
    {
        
        repo = new Repository(this, id, new RepositoryProps
        {
            
            RepositoryName = "ZeptoImageRepo",
            EmptyOnDelete = true,
            RemovalPolicy = Amazon.CDK.RemovalPolicy.DESTROY

        });

    }

}