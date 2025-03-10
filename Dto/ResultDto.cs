﻿namespace HRRS.Dto;
public record ResultDto(bool IsSuccess, string? ErrorMessage)
{
    public static ResultDto Success() => new(true, null);
    public static ResultDto Failure(string? errorMessage) => new(false, errorMessage);
}