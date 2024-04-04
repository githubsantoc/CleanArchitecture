
using Application.services;
using Application.Wrapper;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Command.HangfireCommand
{
    public class HangfireCommandHandler : IRequestHandler<HangFireCommand, string>
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IMailServices _iMailServices;
        private readonly IBackgroundJobClient _backgroundJobClient;

        public HangfireCommandHandler(IMailServices iMailServices, IServiceScopeFactory serviceScopeFactory, IBackgroundJobClient backgroundJobClient)
        {
            _iMailServices = iMailServices;
            _serviceScopeFactory = serviceScopeFactory;
            _backgroundJobClient = backgroundJobClient;
        }
        public async Task<string> Handle(HangFireCommand request, CancellationToken cancellationToken)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {

                var userManager = scope.ServiceProvider.GetRequiredService<IUserWrapper>();
                var admins = await userManager.GetUsersInRoleAsy("Admin");
                var customers = await userManager.GetUsersInRoleAsy("Customer");
                string? scheduleParentJobId = null;
                foreach (var customer in customers)
                {
                    //sending the email at specific time like 12 pm or 3 pm
                    //  RecurringJob.AddOrUpdate(() => YourMethod(), "*/2 * * * *")  it tells every two minutes
                    //  RecurringJob.AddOrUpdate(() => _iMailServices.SendEmailAsync(customer.Email, request.Subject, request.Body), "0 23 * * *");
                    scheduleParentJobId = _backgroundJobClient.Schedule(() => _iMailServices.SendEmailAsync(customer.Email, request.Subject, request.Body), TimeSpan.FromSeconds(10)/*new DateTimeOffset(2024, 2, 29, 12, 19, 0, TimeSpan.Zero)*/);
                }
                foreach (var admin in admins)
                {
                    if (scheduleParentJobId != null)
                        _backgroundJobClient.ContinueJobWith(scheduleParentJobId, () => _iMailServices.SendEmailAsync(admin.Email, "Confirmation", "The mail has been send to all customer"));

                }
            }
            return "Success sending mail";
        }
    }
}
