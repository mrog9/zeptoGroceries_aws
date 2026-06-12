using System;
using Amazon.CDK.AWS.EC2;
using Amazon.CDK.AWS.RDS;
using Constructs;

public class DatabaseInstanceConstruct : Construct
{
    
    private DatabaseInstance dbInstance;

    public DatabaseInstanceConstruct(Construct scope, string id, Vpc vpc): base(scope, "myDBinstanceConstruct")
    {
        
        dbInstance = new DatabaseInstance(this, id, new DatabaseInstanceProps
        {
            Engine = DatabaseInstanceEngine.Postgres(new PostgresInstanceEngineProps{
                Version = PostgresEngineVersion.VER_18
            }),
            Vpc = vpc,
            Credentials = Credentials.FromGeneratedSecret(username:  "mr1123")
        });

    }


}