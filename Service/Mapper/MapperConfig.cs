using Mapster;
using Repository.Models;
using Shared.DTO;

namespace Service.Mapper
{
    public static class MapperConfig
    {
        public static void RegisterPosteMapping()
        {
            TypeAdapterConfig<Poste, PostDto>.NewConfig()
                .Map(dest => dest.UserId,
                     src => src.User.Id,
                     src => src.User != null);
            TypeAdapterConfig<PostDto, Poste>.NewConfig();
        }

        public static void RegisterCommentMapping()
        {
            TypeAdapterConfig<Comment, CommentDto>.NewConfig()
                .Map(dest => dest.PosteId,
                     src => src.Poste.Id,
                     src => src.Poste != null)

               .Map(dest => dest.UserId,
                     src => src.User.Id,
                     src => src.User != null);
            TypeAdapterConfig<CommentDto, Comment>.NewConfig();
        }

        public static void RegisterLikeMapping()
        {
            TypeAdapterConfig<Like, LikeDto>.NewConfig()
                .Map(dest => dest.UserId,
                     src => src.User.Id,
                     src => src.User != null)

                .Map(dest => dest.PosteId,
                     src => src.Poste.Id,
                     src => src.Poste != null)

                .Map(dest => dest.CommentId,
                     src => src.Comment.Id,
                     src => src.Comment != null);
            TypeAdapterConfig<LikeDto, Like>.NewConfig();
        }
    }
}
