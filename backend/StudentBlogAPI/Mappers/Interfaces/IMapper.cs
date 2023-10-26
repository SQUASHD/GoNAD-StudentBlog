namespace StudentBlogAPI.Mappers.Interfaces;

public interface IMapper<TModel, TReqDto, TResDto>
{
    TResDto MapToResDto(TModel model);

    TModel MapToModel(TReqDto dto);

    ICollection<TResDto> MapCollection(ICollection<TModel> models);
}