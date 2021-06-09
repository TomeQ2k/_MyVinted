using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyVinted.Core.Application.Exceptions;
using MyVinted.Core.Application.Features.Requests.Commands;
using MyVinted.Core.Application.Features.Responses.Commands;
using MyVinted.Core.Application.Services;

namespace MyVinted.Core.Application.Features.Handlers.Commands
{
    public class DeleteOpinionCommand : IRequestHandler<DeleteOpinionRequest, DeleteOpinionResponse>
    {
        private readonly IOpinionService opinionService;

        public DeleteOpinionCommand(IOpinionService opinionService)
        {
            this.opinionService = opinionService;
        }

        public async Task<DeleteOpinionResponse> Handle(DeleteOpinionRequest request, CancellationToken cancellationToken)
            => await opinionService.DeleteOpinion(request.OpinionId, request.UserId)
                ? new DeleteOpinionResponse()
                : throw new CrudException("Deleting opinion failed");
    }
}