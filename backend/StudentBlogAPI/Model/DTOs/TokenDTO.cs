namespace StudentBlogAPI.Model.DTOs;

public record RefreshAccessTokenDto(
    string Token
    );
    
public record RefreshTokenResDto(
    string RefreshToken
    );