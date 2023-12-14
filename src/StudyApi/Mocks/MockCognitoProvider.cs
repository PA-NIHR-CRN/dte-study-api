using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Amazon.Runtime;
using Application.Contracts;
using Microsoft.IdentityModel.Tokens;

namespace StudyApi.Mocks;

public class MockCognitoProvider : IAmazonCognitoIdentityProvider
{
    private readonly Dictionary<string, MockCognitoUser> _users;
    private readonly IPrivateKeyProvider _privateKeyProvider;
    public IClientConfig Config { get; }

    public MockCognitoProvider(IPrivateKeyProvider privateKeyProvider)
    {
        _users = new Dictionary<string, MockCognitoUser>();
        _privateKeyProvider = privateKeyProvider;
    }

    public void Dispose()
    {
        // Do nothing
    }

    public Task<AddCustomAttributesResponse> AddCustomAttributesAsync(AddCustomAttributesRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<AdminAddUserToGroupResponse> AdminAddUserToGroupAsync(AdminAddUserToGroupRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<AdminConfirmSignUpResponse> AdminConfirmSignUpAsync(AdminConfirmSignUpRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<AdminCreateUserResponse> AdminCreateUserAsync(AdminCreateUserRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<AdminDeleteUserResponse> AdminDeleteUserAsync(AdminDeleteUserRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<AdminDeleteUserAttributesResponse> AdminDeleteUserAttributesAsync(
        AdminDeleteUserAttributesRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<AdminDisableProviderForUserResponse> AdminDisableProviderForUserAsync(
        AdminDisableProviderForUserRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<AdminDisableUserResponse> AdminDisableUserAsync(AdminDisableUserRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<AdminEnableUserResponse> AdminEnableUserAsync(AdminEnableUserRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<AdminForgetDeviceResponse> AdminForgetDeviceAsync(AdminForgetDeviceRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<AdminGetDeviceResponse> AdminGetDeviceAsync(AdminGetDeviceRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<AdminGetUserResponse> AdminGetUserAsync(AdminGetUserRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        if (!_users.ContainsKey(request.Username))
        {
            throw new UserNotFoundException($"User not found, Username: {request.Username}");
        }

        var user = _users[request.Username];

        return Task.FromResult(new AdminGetUserResponse
        {
            Username = user.Username,
            UserAttributes = new List<AttributeType>
            {
                new AttributeType
                {
                    Name = "email",
                    Value = user.Email
                }
            }
        });
    }

    public Task<AdminInitiateAuthResponse> AdminInitiateAuthAsync(AdminInitiateAuthRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        if (!_users.TryGetValue(request.AuthParameters["USERNAME"], out var user))
        {
            throw new NotAuthorizedException("Incorrect username or password.");
        }

        if (user.Password != request.AuthParameters["PASSWORD"])
        {
            throw new NotAuthorizedException("Incorrect username or password.");
        }

        var token = GenerateJwtToken(user.Username);
        
        return Task.FromResult(new AdminInitiateAuthResponse
        {
            AuthenticationResult = new AuthenticationResultType
            {
                IdToken = token,
                AccessToken = token,
                RefreshToken = token,
                ExpiresIn = 3600,
                TokenType = "Bearer"
            }
        });
    }
    
    private string GenerateJwtToken(string userId, CancellationToken cancellationToken = new CancellationToken())
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var task = _privateKeyProvider.GetPrivateKeyAsync(cancellationToken);
        RSA publicKey = task.Result;
        byte[] key = publicKey.ExportRSAPublicKey();

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("sub", userId)
            }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public Task<AdminLinkProviderForUserResponse> AdminLinkProviderForUserAsync(AdminLinkProviderForUserRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<AdminListDevicesResponse> AdminListDevicesAsync(AdminListDevicesRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<AdminListGroupsForUserResponse> AdminListGroupsForUserAsync(AdminListGroupsForUserRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<AdminListUserAuthEventsResponse> AdminListUserAuthEventsAsync(AdminListUserAuthEventsRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<AdminRemoveUserFromGroupResponse> AdminRemoveUserFromGroupAsync(AdminRemoveUserFromGroupRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<AdminResetUserPasswordResponse> AdminResetUserPasswordAsync(AdminResetUserPasswordRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<AdminRespondToAuthChallengeResponse> AdminRespondToAuthChallengeAsync(
        AdminRespondToAuthChallengeRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<AdminSetUserMFAPreferenceResponse> AdminSetUserMFAPreferenceAsync(
        AdminSetUserMFAPreferenceRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<AdminSetUserPasswordResponse> AdminSetUserPasswordAsync(AdminSetUserPasswordRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<AdminSetUserSettingsResponse> AdminSetUserSettingsAsync(AdminSetUserSettingsRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<AdminUpdateAuthEventFeedbackResponse> AdminUpdateAuthEventFeedbackAsync(
        AdminUpdateAuthEventFeedbackRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<AdminUpdateDeviceStatusResponse> AdminUpdateDeviceStatusAsync(AdminUpdateDeviceStatusRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<AdminUpdateUserAttributesResponse> AdminUpdateUserAttributesAsync(
        AdminUpdateUserAttributesRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<AdminUserGlobalSignOutResponse> AdminUserGlobalSignOutAsync(AdminUserGlobalSignOutRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<AssociateSoftwareTokenResponse> AssociateSoftwareTokenAsync(AssociateSoftwareTokenRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<ChangePasswordResponse> ChangePasswordAsync(ChangePasswordRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<ConfirmDeviceResponse> ConfirmDeviceAsync(ConfirmDeviceRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<ConfirmForgotPasswordResponse> ConfirmForgotPasswordAsync(ConfirmForgotPasswordRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<ConfirmSignUpResponse> ConfirmSignUpAsync(ConfirmSignUpRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<CreateGroupResponse> CreateGroupAsync(CreateGroupRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<CreateIdentityProviderResponse> CreateIdentityProviderAsync(CreateIdentityProviderRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<CreateResourceServerResponse> CreateResourceServerAsync(CreateResourceServerRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<CreateUserImportJobResponse> CreateUserImportJobAsync(CreateUserImportJobRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<CreateUserPoolResponse> CreateUserPoolAsync(CreateUserPoolRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<CreateUserPoolClientResponse> CreateUserPoolClientAsync(CreateUserPoolClientRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<CreateUserPoolDomainResponse> CreateUserPoolDomainAsync(CreateUserPoolDomainRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<DeleteGroupResponse> DeleteGroupAsync(DeleteGroupRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<DeleteIdentityProviderResponse> DeleteIdentityProviderAsync(DeleteIdentityProviderRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<DeleteResourceServerResponse> DeleteResourceServerAsync(DeleteResourceServerRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<DeleteUserResponse> DeleteUserAsync(DeleteUserRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<DeleteUserAttributesResponse> DeleteUserAttributesAsync(DeleteUserAttributesRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<DeleteUserPoolResponse> DeleteUserPoolAsync(DeleteUserPoolRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<DeleteUserPoolClientResponse> DeleteUserPoolClientAsync(DeleteUserPoolClientRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<DeleteUserPoolDomainResponse> DeleteUserPoolDomainAsync(DeleteUserPoolDomainRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<DescribeIdentityProviderResponse> DescribeIdentityProviderAsync(DescribeIdentityProviderRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<DescribeResourceServerResponse> DescribeResourceServerAsync(DescribeResourceServerRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<DescribeRiskConfigurationResponse> DescribeRiskConfigurationAsync(
        DescribeRiskConfigurationRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<DescribeUserImportJobResponse> DescribeUserImportJobAsync(DescribeUserImportJobRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<DescribeUserPoolResponse> DescribeUserPoolAsync(DescribeUserPoolRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        // Create a mock password policy object
        var passwordPolicy = new PasswordPolicyType()
        {
            MinimumLength = 8,
            RequireLowercase = true,
            RequireUppercase = true,
            RequireNumbers = true,
            RequireSymbols = true
        };

        // Create a mock user pool object with the password policy
        var userPool = new UserPoolType()
        {
            Id = "mockUserPoolId_123",
            Name = "mock-user-pool-name",
            LastModifiedDate = DateTime.UtcNow,
            CreationDate = DateTime.UtcNow,
            SchemaAttributes = new List<SchemaAttributeType>()
            {
                new SchemaAttributeType() { Name = "email", Required = true, Mutable = true }
            },
            AutoVerifiedAttributes = new List<string>() { "email" },
            Policies = new UserPoolPolicyType()
            {
                PasswordPolicy = passwordPolicy
            }
        };

        // Create a mock response object with the user pool
        var response = new DescribeUserPoolResponse()
        {
            UserPool = userPool
        };

        return Task.FromResult(response);
    }

    public Task<DescribeUserPoolClientResponse> DescribeUserPoolClientAsync(DescribeUserPoolClientRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<DescribeUserPoolDomainResponse> DescribeUserPoolDomainAsync(DescribeUserPoolDomainRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<ForgetDeviceResponse> ForgetDeviceAsync(ForgetDeviceRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<GetCSVHeaderResponse> GetCSVHeaderAsync(GetCSVHeaderRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<GetDeviceResponse> GetDeviceAsync(GetDeviceRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<GetGroupResponse> GetGroupAsync(GetGroupRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<GetIdentityProviderByIdentifierResponse> GetIdentityProviderByIdentifierAsync(
        GetIdentityProviderByIdentifierRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<GetSigningCertificateResponse> GetSigningCertificateAsync(GetSigningCertificateRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<GetUICustomizationResponse> GetUICustomizationAsync(GetUICustomizationRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<GetUserResponse> GetUserAsync(GetUserRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<GetUserAttributeVerificationCodeResponse> GetUserAttributeVerificationCodeAsync(
        GetUserAttributeVerificationCodeRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<GetUserPoolMfaConfigResponse> GetUserPoolMfaConfigAsync(GetUserPoolMfaConfigRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<GlobalSignOutResponse> GlobalSignOutAsync(GlobalSignOutRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<InitiateAuthResponse> InitiateAuthAsync(InitiateAuthRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<ListDevicesResponse> ListDevicesAsync(ListDevicesRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<ListGroupsResponse> ListGroupsAsync(ListGroupsRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<ListIdentityProvidersResponse> ListIdentityProvidersAsync(ListIdentityProvidersRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<ListResourceServersResponse> ListResourceServersAsync(ListResourceServersRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<ListTagsForResourceResponse> ListTagsForResourceAsync(ListTagsForResourceRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<ListUserImportJobsResponse> ListUserImportJobsAsync(ListUserImportJobsRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<ListUserPoolClientsResponse> ListUserPoolClientsAsync(ListUserPoolClientsRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<ListUserPoolsResponse> ListUserPoolsAsync(ListUserPoolsRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<ListUsersResponse> ListUsersAsync(ListUsersRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<ListUsersInGroupResponse> ListUsersInGroupAsync(ListUsersInGroupRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<ResendConfirmationCodeResponse> ResendConfirmationCodeAsync(ResendConfirmationCodeRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<RespondToAuthChallengeResponse> RespondToAuthChallengeAsync(RespondToAuthChallengeRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<RevokeTokenResponse> RevokeTokenAsync(RevokeTokenRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<SetRiskConfigurationResponse> SetRiskConfigurationAsync(SetRiskConfigurationRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<SetUICustomizationResponse> SetUICustomizationAsync(SetUICustomizationRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<SetUserMFAPreferenceResponse> SetUserMFAPreferenceAsync(SetUserMFAPreferenceRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<SetUserPoolMfaConfigResponse> SetUserPoolMfaConfigAsync(SetUserPoolMfaConfigRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<SetUserSettingsResponse> SetUserSettingsAsync(SetUserSettingsRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<SignUpResponse> SignUpAsync(SignUpRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        _users.Add(request.Username,
            new MockCognitoUser(request.Username, request.Password, request.Username, true, "Confirmed"));
        var response = new SignUpResponse
        {
            UserSub = request.Username,
            HttpStatusCode = HttpStatusCode.OK
        };
        return Task.FromResult(response);
    }

    public Task<StartUserImportJobResponse> StartUserImportJobAsync(StartUserImportJobRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<StopUserImportJobResponse> StopUserImportJobAsync(StopUserImportJobRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<TagResourceResponse> TagResourceAsync(TagResourceRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<UntagResourceResponse> UntagResourceAsync(UntagResourceRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<UpdateAuthEventFeedbackResponse> UpdateAuthEventFeedbackAsync(UpdateAuthEventFeedbackRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<UpdateDeviceStatusResponse> UpdateDeviceStatusAsync(UpdateDeviceStatusRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<UpdateGroupResponse> UpdateGroupAsync(UpdateGroupRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<UpdateIdentityProviderResponse> UpdateIdentityProviderAsync(UpdateIdentityProviderRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<UpdateResourceServerResponse> UpdateResourceServerAsync(UpdateResourceServerRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<UpdateUserAttributesResponse> UpdateUserAttributesAsync(UpdateUserAttributesRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<UpdateUserPoolResponse> UpdateUserPoolAsync(UpdateUserPoolRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<UpdateUserPoolClientResponse> UpdateUserPoolClientAsync(UpdateUserPoolClientRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<UpdateUserPoolDomainResponse> UpdateUserPoolDomainAsync(UpdateUserPoolDomainRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<VerifySoftwareTokenResponse> VerifySoftwareTokenAsync(VerifySoftwareTokenRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task<VerifyUserAttributeResponse> VerifyUserAttributeAsync(VerifyUserAttributeRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public ICognitoIdentityProviderPaginatorFactory Paginators { get; }
}