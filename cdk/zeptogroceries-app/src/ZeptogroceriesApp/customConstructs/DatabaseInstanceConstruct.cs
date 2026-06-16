using System;
using Amazon.CDK.AWS.EC2;
using Amazon.CDK.AWS.ECS;
using Amazon.CDK.AWS.RDS;
using Constructs;

public class DatabaseInstanceConstruct : Construct
{
    
    private DatabaseInstance dbInstance;

    public DatabaseInstanceConstruct(Construct scope, string id, Cluster cluster, Fargate fargateService): base(scope, "myDBinstanceConstruct")
    {
        var sg = new SecurityGroup(this, "myDbSG", new SecurityGroupProps
        {
           Vpc= cluster.Vpc,
           AllowAllOutbound=true

        });



        sg.AddIngressRule(Peer.SecurityGroupId(fargateService.GetFargateUniqueSGid()), Port.Tcp(5432));
        
        dbInstance = new DatabaseInstance(this, id, new DatabaseInstanceProps
        {
            Engine = DatabaseInstanceEngine.Postgres(new PostgresInstanceEngineProps{
                Version = PostgresEngineVersion.VER_18
            }),
            Vpc = cluster.Vpc,
            Credentials = Credentials.FromGeneratedSecret(username:  "mr1123"),
            SecurityGroups= [sg]
        });

    }


}