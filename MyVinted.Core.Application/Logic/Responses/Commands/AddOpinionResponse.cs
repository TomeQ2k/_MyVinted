using MyVinted.Core.Application.Dtos;
using MyVinted.Core.Application.Models;

namespace MyVinted.Core.Application.Logic.Responses.Commands
{
    public record AddOpinionResponse : BaseResponse
    {
        public OpinionDto Opinion { get; init; }
        public double NewRating { get; init; }

        public AddOpinionResponse(Error error = null) : base(error) { }
    }
}