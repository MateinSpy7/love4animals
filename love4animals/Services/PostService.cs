using love4animals.DTOs;
using love4animals.Repositories;
using love4animals.Models;

namespace love4animals.Services;

public class PostService : IPostService
{
    private readonly IPostRepository _repo;

    public PostService(IPostRepository repo) => _repo = repo;

    public IEnumerable<GetPostDto> GetAll() => 
        _repo.GetAll().Select(p => new GetPostDto(p.Id, p.Content, p.ImageUrl, p.CreatedAt, p.MissionaryId, p.CampaignId));

    public GetPostDto? GetById(Guid id)
    {
        var post = _repo.GetById(id);
        return post == null ? null : new GetPostDto(post.Id, post.Content, post.ImageUrl, post.CreatedAt, post.MissionaryId, post.CampaignId);
    }

    public GetPostDto Create(CreatePostDto dto)
    {
        var post = new Post 
        { 
            Content = dto.Content, 
            ImageUrl = dto.ImageUrl, 
            MissionaryId = dto.MissionaryId, 
            CampaignId = dto.CampaignId 
        };
        _repo.Add(post);
        return new GetPostDto(post.Id, post.Content, post.ImageUrl, post.CreatedAt, post.MissionaryId, post.CampaignId);
    }

    public bool Update(Guid id, UpdatePostDto dto)
    {
        var existing = _repo.GetById(id);
        if (existing == null) return false;

        existing.Content = dto.Content;
        existing.ImageUrl = dto.ImageUrl;
        existing.CampaignId = dto.CampaignId;
        _repo.Update(existing);
        return true;
    }

    public bool Delete(Guid id)
    {
        if (_repo.GetById(id) == null) return false;
        _repo.Delete(id);
        return true;
    }
}