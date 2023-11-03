namespace StudentBlogAPI.Model.Internal;

public record InternalUpdateUserInfoData(
    int CurrentUserId,
    int UserToUpdateId,
    string FirstName,
    string LastName
);

public record InternalUpdatePasswordReqData(
    int CurrentUserId,
    int UserToUpdateId,
    string OldPassword,
    string NewPassword
);

public record InternalProcessedUpdatePasswordData(
    string HashedNewPassword,
    string NewSalt
);

public record InternalDeleteUserData(
    int CurrentUserId,
    int UserToDeleteId
);

public record InternalGetPostsByUserIdData(
    int CurrentUserId,
    int UserId,
    int PageNumber,
    int PageSize
);