using UserApi.Models;

namespace UserApi.Repositories.Impl;

public class DictionaryUserRepository : IRepository<User, string>
{
    private readonly Dictionary<string, User> _storage = new Dictionary<string, User>();
    
    public User Add(User entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        return _storage.TryAdd(entity.Username, entity) ?  _storage[entity.Username] : throw new ArgumentException();
    }

    public User Delete(string username)
    {   
        var entity = _storage[username];
        _storage.Remove(username);
        return entity;
    }

    public User Get(string username)
    {
        return _storage[username];
    }

    public List<User> GetAll()
    {
        return [.. _storage.Values];
    }

    public User Update(User entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        
        _storage[entity.Username] = entity;
        return _storage[entity.Username];
    }
}