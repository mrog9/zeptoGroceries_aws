using Amazon.CDK.AWS.EC2;
using Amazon.CDK.AWS.ECS;
using Amazon.CDK.AWS.ElasticLoadBalancingV2;
using Constructs;

public class Fargate: Construct
{
    
    private FargateService myService;
    private SecurityGroup mySG;

    public Fargate(Construct scope, string id, string serviceName, ALB alb, Cluster cluster): base(scope, "myFargateConstruct")
    {

        mySG = new SecurityGroup(this, "FargateSG", new SecurityGroupProps{

            Vpc = cluster.Vpc,
            AllowAllOutbound= true

        });
        

        mySG.AddIngressRule(Peer.SecurityGroupId(alb.GetALBuniqueSGid()), Port.Tcp(3000));

        myService = new FargateService(this, id, new FargateServiceProps
        {
            
            Cluster = cluster,
            ServiceName = serviceName,
            SecurityGroups = [mySG]

        });

    }

    public FargateService GetFargateService(){return myService;}
    public string GetFargateUniqueSGid(){return mySG.UniqueId;}

}