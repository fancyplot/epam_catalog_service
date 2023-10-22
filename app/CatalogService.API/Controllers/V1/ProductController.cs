using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.API.Controllers.V1;

[Route("v1/[controller]")]
[ApiController]
public partial class ProductController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public ProductController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }
}