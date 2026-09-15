using UserApi.dto;
using UserApi.entity;

namespace UserApi.repository.impl;

public class DictionaryUserRepository : IRepository<User, string>
{
    private readonly Dictionary<string, User> _storage = new Dictionary<string, User>();
    
    public User Add(User entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        if (_storage.ContainsKey(entity.Username))
        {
            throw new ArgumentException();
        }
        
        _storage.Add(entity.Username, entity);
        return _storage[entity.Username];
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

    public User Update(User entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        
        _storage[entity.Username] = entity;
        return _storage[entity.Username];
    }
}