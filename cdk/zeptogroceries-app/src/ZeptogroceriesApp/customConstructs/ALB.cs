using Amazon.CDK.AWS.EC2;
using Amazon.CDK.AWS.ECS;
using Amazon.CDK.AWS.ElasticLoadBalancingV2;
using Constructs;

public class ALB : Construct
{
    
    private ApplicationLoadBalancer myALB;
    private SecurityGroup mySG;

    public ApplicationListener albListener {get; private set;}

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
    public string GetALBuniqueSGid(){return mySG.UniqueId;}

    public void AddListener(string targetID, Fargate fgService)
    {
        
        albListener.AddTargets(targetID, new AddApplicationTargetsProps
        {
            Port = 3000,
            Priority = 10,
            Conditions = [ListenerCondition.PathPatterns(["/users/postUser", "/users/getUser" ])],
            Targets=[fgService.GetFargateService()]


        });

    }

}
