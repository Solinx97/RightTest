namespace RightTest.UsersBL.DTOs;

public record UserDto(
    Guid Id,
    string Name,
    string PasswordHash,
    string Salt
    );
