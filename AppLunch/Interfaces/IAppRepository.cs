using AppLunch.DataAccess;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppLunch.Interfaces
{
    public interface IAppRepository
    {
        Task<List<Member>> GetMembersAsync();

        Task<Member> GetMemberByIdentityIdAsync(string identityID);

        Task<Invite> InsertInviteAsync(Invite invite);

        Task<Invite> GetInviteByIDAsync(Guid id);

        Task CreateMemberAsync(Member member);

        Task<List<Location>> GetLocationsAsync();

        Task DeleteLocationAsync(int id);

        Task UpdateLocationAsync(Location location);

        Task<Location> InsertLocationAsync(Location location);

        Task<List<Venue>> GetVenuesByLocationIdAsync(int locationID);

        Task<Venue> GetVenueByIdAsync(int id);

        Task CreateVenueAsync(Venue venue);

        Task UpdateVenueAsync(Venue venue);


    }
}