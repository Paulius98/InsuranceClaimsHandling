namespace Claims.Domain.Entities.Audtiting;

public class ClaimAudit : Audit
{
    public Guid ClaimId { get; set; }

}
