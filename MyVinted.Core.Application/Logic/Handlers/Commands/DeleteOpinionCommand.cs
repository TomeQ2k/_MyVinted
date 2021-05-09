using MediatR;
using MyVinted.Core.Application.Exceptions;
using MyVinted.Core.Application.Logic.Requests.Commands;
using MyVinted.Core.Application.Logic.Responses.Commands;
using MyVinted.Core.Application.Services;
using System.Threading;
using System.Threading.Tasks;

namespace MyVinted.Core.Application.Logic.Handlers.Commands
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