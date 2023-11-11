using MediatR;
using Oms.Base.Response;
using Oms.Schema;

namespace Oms.Operation;

public record CreateTokenCommand(TokenRequest Model) : IRequest<ApiResponse<TokenResponse>>;