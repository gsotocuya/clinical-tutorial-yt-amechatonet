using AutoMapper;
using CLINICAL.Application.Dtos.Response;
using CLINICAL.Application.Interfaces;
using CLINICAL.Application.Interfaces.Interfaces;
using CLINICAL.Application.UseCase.Commons.Bases;
using MediatR;

namespace CLINICAL.Application.UseCase.UseCases.Analysis.Queries.GetAllQuery;

public class GetAllAnalysisHandler : IRequestHandler<GetAllAnalysisQuery, BaseResponse<IEnumerable<GetAllAnalysisResponseDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllAnalysisHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<IEnumerable<GetAllAnalysisResponseDto>>> Handle(GetAllAnalysisQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<GetAllAnalysisResponseDto>>();
        try
        {
            var analysis = await _unitOfWork.Analysis.GetAllASync("uspAnalysisList");
            if (analysis is not null)
            {
                response.IsSuccess = true;
                response.Data = _mapper.Map<IEnumerable<GetAllAnalysisResponseDto>>(analysis);
                response.Message = "Consulta Exitosa!!!";
            }
        }
        catch (Exception e)
        {
            response.Message = e.Message;
        }

        return response;
    }
}