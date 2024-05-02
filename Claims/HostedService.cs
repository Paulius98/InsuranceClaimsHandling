using Claims.Domain.Interfaces.Queues;

namespace Claims
{
    public class HostedService : IHostedService
    {
        private readonly IClaimAuditMessageReceiver _claimMessageReceiver;
        private readonly ICoverAuditMessageReceiver _coverMessageReceiver;

        public HostedService(
            IClaimAuditMessageReceiver claimMessageReceiver, 
            ICoverAuditMessageReceiver coverMessageReceiver)
        {
            _claimMessageReceiver = claimMessageReceiver;
            _coverMessageReceiver = coverMessageReceiver;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await Task.WhenAll(
                _claimMessageReceiver.RegisterMessageReceiverAsync(cancellationToken),
                _coverMessageReceiver.RegisterMessageReceiverAsync(cancellationToken)
            );
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Task.WhenAll(
                _claimMessageReceiver.UnregisterMessageReceiverAsync(cancellationToken),
                _coverMessageReceiver.UnregisterMessageReceiverAsync(cancellationToken)
            );
        }
    }
}
