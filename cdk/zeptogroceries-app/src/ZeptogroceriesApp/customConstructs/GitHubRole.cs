using System.Collections.Generic;
using Amazon.CDK.AWS.IAM;
using Constructs;

public class GitHubRole : Construct
{
    
    private Role role;

    public GitHubRole(Construct con, string id) : base(con, "myRoleConstruct")
    {
        
        OpenIdConnectProvider provider = new OpenIdConnectProvider(this, "GitHubOIDC", new OpenIdConnectProviderProps
        {
            Url= "https://token.actions.githubusercontent.com",
            ClientIds = new [] { "sts.amazonaws.com" }

        });

        role = new Role(this, "GitHubActionsRole", new RoleProps
        {
            
            AssumedBy= new FederatedPrincipal(
                provider.OpenIdConnectProviderArn,
                new Dictionary<string, object>
                {
                    {
                        "StringEquals", new Dictionary<string, object> 
                        {
                            { "token.actions.githubusercontent.com:aud", "sts.amazonaws.com" }
                        }
                    },
                    {
                        "StringLike", new Dictionary<string, object>
                        {
                            { "token.actions.githubusercontent.com:sub", "repo:mrog9/zeptoGroceries_aws:ref:refs/heads/*" }   
                        }
                    }
                },
                "sts:AssumeRoleWithWebIdentity"
             )

        });

        role.AddManagedPolicy(ManagedPolicy.FromAwsManagedPolicyName("AdministratorAccess"));

        var ecrStatement = new PolicyStatement(new PolicyStatementProps
        {
            Effect = Effect.ALLOW
        });
        ecrStatement.AddActions([
                "ecr:GetAuthorizationToken",
                "ecr:BatchCheckLayerAvailability",
                "ecr:InitiateLayerUpload",
                "ecr:UploadLayerPart",
                "ecr:CompleteLayerUpload",
                "ecr:PutImage"
            ]);

        ecrStatement.AddResources(["arn:aws:ecr:us-east-1:749625536154:repository/zepto-image-repo"]);

        role.AddToPolicy(ecrStatement);


    }


}