namespace UserApi.dto;

public record UserUpdateForm(string Username, string? NewPassword, int? Age, string Credential);