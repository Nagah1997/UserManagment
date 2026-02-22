using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroSlate.Application.Features.Users.Query.Models.GetAll
{
    public class GetUserQuery: IRequest<Result<GetUserResponse>>
    {
    }
}
