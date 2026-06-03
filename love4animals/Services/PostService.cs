using love4animals.DTOs;
using love4animals.Repositories;
using love4animals.Models;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace love4animals.Services;

public class PostService : IPostService
{
    private readonly IPostRepository _repo;
    private readonly IDistributedCache _cache; //Redis

    public PostService(IPostRepository repo, IDistributedCache cache)
    {
        _repo = repo;
        _cache = cache;
    }

    public IEnumerable<GetPostDto> GetAll()
    {
        // CACHE-ASIDE
        string cacheKey = "posts:all";
        var cachedData = _cache.GetString(cacheKey);

        if (cachedData != null)
        {
            return JsonSerializer.Deserialize<List<GetPostDto>>(cachedData)!;
        }

        var posts = _repo.GetAll().Select(p => new GetPostDto(p.Id, p.Content, p.ImageUrl, p.CreatedAt, p.MissionaryId, p.CampaignId)).ToList();
        
        _cache.SetString(cacheKey, JsonSerializer.Serialize(posts), new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
        });

        return posts;
    }

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
        
        _cache.Remove("posts:all"); // INVALIDACIÓN

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
        
        _cache.Remove("posts:all"); // INVALIDACIÓN

        return true;
    }

    public bool Delete(Guid id)
    {
        if (_repo.GetById(id) == null) return false;
        _repo.Delete(id);
        
        _cache.Remove("posts:all"); // INVALIDACIÓN

        return true;
    }
}