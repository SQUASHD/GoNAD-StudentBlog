namespace StudentBlogAPI.Model.DTOs;

public record AccessTokenResDto(
    string Token
    );
    
public record RefreshTokenResDto(
    string RefreshToken
    );