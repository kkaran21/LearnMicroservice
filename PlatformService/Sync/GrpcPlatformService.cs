using AutoMapper;
using Grpc.Core;
using PlatformService.Data;
using static PlatformService.grpcPlatform;

namespace PlatformService.SyncDataServices.Grpc
{
    public class GrpcPlatformService : grpcPlatformBase
    {
        private readonly IPlatformRepo _platformRepo;
        private readonly IMapper _mapper;

        public GrpcPlatformService(IPlatformRepo platformRepo, IMapper mapper)
        {
            _platformRepo = platformRepo;
            _mapper = mapper;
        }

        public override Task<platformResponse> getAllPlatforms(emptyRequest emptyRequest, ServerCallContext context)
        {
            platformResponse platformResponse = new platformResponse();
            var allplatforms = _platformRepo.GetAllPlatforms();
            foreach (var item in allplatforms)
            {
                platformResponse.Platforms.Add(_mapper.Map<grpcPlatformModel>(item));
            }
             
            return Task.FromResult(platformResponse);
        }
    }
}