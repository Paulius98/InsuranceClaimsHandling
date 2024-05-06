namespace Claims.Domain.Entities.Audtiting;

public class ClaimAudit : Audit
{
    public Guid ClaimId { get; set; }

    public override string ToString()
    {
        return $"ClaimId: {ClaimId}, HttpRequestType: {HttpRequestType}";
    }
}
