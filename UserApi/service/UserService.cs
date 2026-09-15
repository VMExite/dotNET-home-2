using Microsoft.AspNetCore.Identity;
using UserApi.dto;
using UserApi.entity;
using UserApi.repository;
using UserApi.repository.impl;

namespace UserApi.service;

public class UserService(IRepository<User, string> userRepository)
{
    private readonly PasswordHasher<string> _passwordHasher = new();

    public User AddUser(UserRegisterForm form)
    {
        var hashed = _passwordHasher.HashPassword(null, form.Password);
        var user = new User
        {
            Username = form.Username,
            Password = hashed,
            Age = form.Age
        };
        return userRepository.Add(user);
    }
    public User? UpdateUser(UserUpdateForm form)
    {
        User user;
        try
        {
            user = userRepository.Get(form.Username);
        }
        catch (KeyNotFoundException e)
        {
            Console.WriteLine(e);
            return null;
        }

        var result = _passwordHasher.VerifyHashedPassword(null, user.Password, form.Credential);

        switch (result)
        {
            case PasswordVerificationResult.Failed:
                throw new UnauthorizedAccessException();
            case PasswordVerificationResult.Success:
                if (form.NewPassword != null)
                    user.Password = _passwordHasher.HashPassword(null, form.NewPassword);
                if (form.Age.HasValue)
                    user.Age = form.Age.Value;
                return userRepository.Update(user);
            default:
                return null;
        }
    }
    public void DeleteUser(UserDeleteForm form)
    {
        User user;
        try
        {
            user = userRepository.Get(form.Username);
        }
        catch (KeyNotFoundException e)
        {
            Console.WriteLine(e);
            throw new KeyNotFoundException();
        }

        var result = _passwordHasher.VerifyHashedPassword(null, user.Password, form.Credential);
        switch (result)
        {
            case PasswordVerificationResult.Success:
                userRepository.Delete(user.Username);
                break;
            default:
                throw new UnauthorizedAccessException();
        }
        
    }

    public User? Login(UserLoginForm form)
    {
        User user;
        try
        {
            user = userRepository.Get(form.Username);
        }
        catch (KeyNotFoundException e)
        {
            Console.WriteLine(e);
            return null;
        }
        
        var result = _passwordHasher.VerifyHashedPassword(null, user.Password, form.Password);
        switch (result)
        {
            case PasswordVerificationResult.Success:
                return user;
            default:
                return null;
        }
    }
}