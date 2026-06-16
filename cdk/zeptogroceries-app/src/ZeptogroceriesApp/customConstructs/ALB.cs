using Amazon.CDK.AWS.EC2;
using Amazon.CDK.AWS.ECS;
using Amazon.CDK.AWS.ElasticLoadBalancingV2;
using Constructs;

public class ALB : Construct
{
    
    private ApplicationLoadBalancer myALB;
    private SecurityGroup mySG;

    private ApplicationListener albListener;

    public ALB(Construct scope, string id, Cluster cluster):base(scope, "ALBConstruct")
    {

        mySG = new SecurityGroup(this, "albSG", new SecurityGroupProps
                {
                    Vpc = cluster.Vpc,
                    AllowAllOutbound=true
                });

        mySG.AddIngressRule(Peer.AnyIpv4(), Port.Tcp(443));
        
        myALB = new ApplicationLoadBalancer(this, id, new ApplicationLoadBalancerProps
        {
                Vpc = cluster.Vpc,
                SecurityGroup= mySG,
                InternetFacing=true

        });

        albListener = myALB.AddListener("myHTTPSListener", new ApplicationListenerProps
        {
            Port = 443,
            Protocol = ApplicationProtocol.HTTPS

        });

    }

    public ApplicationLoadBalancer GetALB() {return myALB;}
    public string GetALBSGid(){return mySG.SecurityGroupId;}
    public ApplicationListener GetALBListener(){return albListener;}

}
