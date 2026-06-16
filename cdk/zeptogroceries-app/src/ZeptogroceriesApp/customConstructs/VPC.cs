using Amazon.CDK.AWS.EC2;
using Constructs;

public class VPC : Construct
{
    
    private Vpc myVPC;

    public VPC(Construct scope, string id):base(scope, "myVPCconstruct")
    {
        
        myVPC = new Vpc(this, id, new VpcProps
        {
            MaxAzs = 2,
            SubnetConfiguration= [
                new SubnetConfiguration{
                    Name="public",
                    SubnetType= SubnetType.PUBLIC
                },
                new SubnetConfiguration{
                    Name="private",
                    SubnetType = SubnetType.PRIVATE_WITH_EGRESS
                }
            ]
        });

    }

    public Vpc GetVPC(){return myVPC;}

}