using MicroSlate.Infrastructure.AppConct;
using MicroSlate.Infrastructure.Identity;
using MicroSlate.Service.Abstracts.Authorization.claims;
using MicroSlate.Service.DTOs.Authorization;
using MicroSlate.Service.Helper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MicroSlate.Service.Implementions.Authorization.claims
{
    public class claimService : IClaimService
    {
        #region Fields
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _dbContext;
        #endregion
        #region Constructors
        public claimService(RoleManager<Role> roleManager,
                                    UserManager<ApplicationUser> userManager,
                                    ApplicationDbContext dbContext)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _dbContext = dbContext;
        }
        #endregion

        public async Task<ManageUserRolesDTO> ManageUserRolesData(ApplicationUser user)
        {
            var response = new ManageUserRolesDTO();
            var rolesList = new List<UserRoles>();
            //Roles
            var roles = await _roleManager.Roles.ToListAsync();
            response.UserId = user.Id;
            foreach (var role in roles)
            {
                var userrole = new UserRoles();
                userrole.Id = role.Id;
                userrole.Name = role.Name;
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userrole.HasRole = true;
                }
                else
                {
                    userrole.HasRole = false;
                }
                rolesList.Add(userrole);
            }
            response.userRoles = rolesList;
            return response;
        }
        public async Task<string> UpdateUserRoles(UpdateUserRolesRequest request)
        {
            var transact = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                //Get User
                var user = await _userManager.FindByIdAsync(request.UserId.ToString());
                if (user == null)
                {
                    return "UserIsNull";
                }
                //get user Old Roles
                var userRoles = await _userManager.GetRolesAsync(user);
                //Delete OldRoles
                var removeResult = await _userManager.RemoveFromRolesAsync(user, userRoles);
                if (!removeResult.Succeeded)
                    return "FailedToRemoveOldRoles";
                var selectedRoles = request.userRoles.Where(x => x.HasRole == true).Select(x => x.Name);

                //Add the Roles HasRole=True
                var addRolesresult = await _userManager.AddToRolesAsync(user, selectedRoles);
                if (!addRolesresult.Succeeded)
                    return "FailedToAddNewRoles";
                await transact.CommitAsync();
                //return Result
                return "Success";
            }
            catch (Exception ex)
            {
                await transact.RollbackAsync();
                return "FailedToUpdateUserRoles";
            }
        }
        public async Task<ManageUserClaimsResult> ManageUserClaimData(ApplicationUser user)
        {
            var response = new ManageUserClaimsResult();
            var usercliamsList = new List<UserClaims>();
            response.UserId = user.Id;
            //Get USer Claims
            var userClaims = await _userManager.GetClaimsAsync(user); //edit
                                                                      //create edit get print
            foreach (var claim in ClaimsStore.claims)
            {
                var userclaim = new UserClaims();
                userclaim.Type = claim.Type;
                if (userClaims.Any(x => x.Type == claim.Type))
                {
                    userclaim.Value = true;
                }
                else
                {
                    userclaim.Value = false;
                }
                usercliamsList.Add(userclaim);
            }
            response.userClaims = usercliamsList;
            //return Result
            return response;
        }

        public async Task<string> UpdateUserClaims(UpdateUserClaimsRequest request)
        {
            var transact = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var user = await _userManager.FindByIdAsync(request.UserId.ToString());
                if (user == null)
                {
                    return "UserIsNull";
                }
                //remove old Claims
                var userClaims = await _userManager.GetClaimsAsync(user);
                var removeClaimsResult = await _userManager.RemoveClaimsAsync(user, userClaims);
                if (!removeClaimsResult.Succeeded)
                    return "FailedToRemoveOldClaims";
                var claims = request.userClaims.Where(x => x.Value == true).Select(x => new Claim(x.Type, x.Value.ToString()));

                var addUserClaimResult = await _userManager.AddClaimsAsync(user, claims);
                if (!addUserClaimResult.Succeeded)
                    return "FailedToAddNewClaims";

                await transact.CommitAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                await transact.RollbackAsync();
                return "FailedToUpdateClaims";
            }
        }

    }
}
