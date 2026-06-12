using Amazon.CDK.AWS.EC2;
using Constructs;

public class VPC : Construct
{
    
    private Vpc myVPC;

    public VPC(Construct scope, string id):base(scope, "myVPCconstruct")
    {
        
        myVPC = new Vpc(this, id, new VpcProps{});

    }

    public Vpc getVPC(){return myVPC;}

}