namespace Claims.Infrastructure.Persistence.EntityConfigurations;

public static class EntityConfiguration
{
    public const int HttpRequestMethodTypeLength = 64;

    public const string DefaultSchema = "dbo";

    public const string CoverAuditTableName = "CoverAudits";
    public const string ClaimAuditTableName = "ClaimAudits";

    public const string ClaimCollectionName = "claims";
    public const string CoverCollectionName = "covers";
}
