using Amazon.CDK.AWS.EC2;
using Amazon.CDK.AWS.ECS;
using Amazon.CDK.AWS.ElasticLoadBalancingV2;
using Constructs;

public class Fargate: Construct
{
    
    private FargateService myService;
    private SecurityGroup mySG;

    public Fargate(Construct scope, string id, string serviceName,ECR serviceRepo, ALB alb, Cluster cluster): base(scope, "myFargateConstruct")
    {

        mySG = new SecurityGroup(this, "FargateSG", new SecurityGroupProps{

            Vpc = cluster.Vpc,
            AllowAllOutbound= true

        });
        

        mySG.AddIngressRule(Peer.SecurityGroupId(alb.GetALBSGid()), Port.Tcp(3000));

        var taskDef = new FargateTaskDefinition(this, serviceName + "TaskDefinition", new FargateTaskDefinitionProps
        {
            Cpu = 256,
            MemoryLimitMiB = 512
        });

        var container = taskDef.AddContainer(serviceName + "Container", new ContainerDefinitionOptions
        {
            Image = ContainerImage.FromEcrRepository(serviceRepo.getRepo()),
            Logging = LogDriver.AwsLogs(new AwsLogDriverProps
            {
                StreamPrefix = serviceName
            })

        });

        container.AddPortMappings(new PortMapping
        {
            ContainerPort = 3000,
            Protocol = Amazon.CDK.AWS.ECS.Protocol.TCP
        });

        myService = new FargateService(this, id, new FargateServiceProps
        {
            
            Cluster = cluster,
            TaskDefinition = taskDef,
            ServiceName = serviceName,
            SecurityGroups = [mySG]

        });

    }

    public FargateService GetFargateService(){return myService;}
    public string GetFargateUniqueSGid(){return mySG.UniqueId;}

    public void AddListener(string id, string[] pathPatterns, double portNum, ALB alb)
    {
        
        alb.GetALBListener().AddTargets(id, new AddApplicationTargetsProps
        {
            Port = portNum,
            Protocol = ApplicationProtocol.HTTPS,
            Priority = 10,
            Conditions = [ListenerCondition.PathPatterns(pathPatterns)],
            Targets=[myService]


        });

    }
}