using Microsoft.AspNetCore.Identity;
using MyVinted.Core.Application.Exceptions;
using MyVinted.Core.Application.Extensions;
using MyVinted.Core.Application.Services;
using MyVinted.Core.Domain.Data;
using MyVinted.Core.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using MyVinted.Core.Application.Results;
using MyVinted.Core.Domain.Entities;
using IdentityResult = MyVinted.Core.Application.Results.IdentityResult;

namespace MyVinted.Infrastructure.Shared.Services
{
    public class FacebookIdentityService : BaseExternalIdentityService, IFacebookIdentityService
    {
        private readonly HttpClient httpClient = new HttpClient { BaseAddress = new Uri(Constants.FacebookUri) }.AddRequestHeaders();

        public FacebookIdentityService(UserManager<User> userManager, IJwtAuthorizationTokenGenerator jwtAuthorizationTokenGenerator, IRolesManager rolesManager, IUnitOfWork unitOfWork)
            : base(userManager, jwtAuthorizationTokenGenerator, rolesManager, unitOfWork) { }

        public override async Task<IdentityResult> SignInWithExternalProvider(string provider, string idToken)
        {
            var facebookUser = await GetUserFromFacebook(idToken) ?? throw new ExternalAuthException("Invalid external authentication");

            return await AddUserLogin(provider, facebookUser.Id, facebookUser.Email,
                username: $"{facebookUser.FirstName}_{facebookUser.LastName}_{facebookUser.Id}", pictureUrl: facebookUser.PictureUrl);
        }

        public async Task<FacebookUserResult> GetUserFromFacebook(string idToken)
        {
            var facebookResult = await GetFacebookResult(idToken, "me", "fields=email,first_name,last_name,picture.width(100).height(100)")
                ?? throw new ServiceException("Fetching user data from Facebook service failed");

            var facebookUser = new FacebookUserResult
            (
                facebookResult["id"].ToString(),
                facebookResult["email"].ToString(),
                facebookResult["first_name"].ToString(),
                facebookResult["last_name"].ToString(),
                facebookResult["picture"].GetProperty("data").GetProperty("url").ToString()
            );

            return facebookUser;
        }

        #region private

        private async Task<Dictionary<string, JsonElement>> GetFacebookResult(string accessToken, string endpoint, string args = null)
        {
            var facebookResponse = await httpClient.GetAsync($"{endpoint}?access_token={accessToken}&{args}");

            if (!facebookResponse.IsSuccessStatusCode)
                return default(Dictionary<string, JsonElement>);

            var facebookResult = await facebookResponse.Content.ReadAsStringAsync();

            return facebookResult.FromJSON<Dictionary<string, JsonElement>>();
        }

        #endregion
    }
}