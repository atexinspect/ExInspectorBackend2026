using ExInspectorBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace ExInspectorBackend.Controllers
{
    public static class BackendEndpoints
    {

        public static void MapBackendEndpoints(this IEndpointRouteBuilder routes)
        {

            //----------------------------------------------------------------------------------------------------

            routes.MapDelete("/api/BackendDeleteAll", async (AppDbContext db) =>
            {
                // https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-7.0/whatsnew#executeupdate-and-executedelete-bulk-updates
                await db.Asset.ExecuteDeleteAsync();
                await db.AssetType.ExecuteDeleteAsync();
                await db.Location.ExecuteDeleteAsync();
                await db.LocationSystems.ExecuteDeleteAsync();
                await db.LotDetails.ExecuteDeleteAsync();
                await db.LotSchedules.ExecuteDeleteAsync();
                await db.Manufacturers.ExecuteDeleteAsync();
                await db.MembershipRoles.ExecuteDeleteAsync();
                await db.MembershipTasks.ExecuteDeleteAsync();
                await db.MembershipUsers.ExecuteDeleteAsync();
                await db.MembershipUsersInRoles.ExecuteDeleteAsync();
                await db.Schedules.ExecuteDeleteAsync();
                await db.WorkOrders.ExecuteDeleteAsync();
                return Results.Ok();
            });

            //----------------------------------------------------------------------------------------------------

            routes.MapDelete("/api/BackendDeleteDeleteMarkedOnly", async (AppDbContext db) =>
            {
                await db.Asset.Where(a => a.Deleted == true).ExecuteDeleteAsync();
                await db.AssetType.Where(a => a.Deleted == true).ExecuteDeleteAsync();
                await db.Location.Where(a => a.Deleted == true).ExecuteDeleteAsync();
                await db.LocationSystems.Where(a => a.Deleted == true).ExecuteDeleteAsync();
                await db.LotDetails.Where(a => a.Deleted == true).ExecuteDeleteAsync();
                await db.LotSchedules.Where(a => a.Deleted == true).ExecuteDeleteAsync();
                await db.Manufacturers.Where(a => a.Deleted == true).ExecuteDeleteAsync();
                await db.MembershipRoles.Where(a => a.Deleted == true).ExecuteDeleteAsync();
                await db.MembershipTasks.Where(a => a.Deleted == true).ExecuteDeleteAsync();
                await db.MembershipUsers.Where(a => a.Deleted == true).ExecuteDeleteAsync();
                await db.MembershipUsersInRoles.Where(a => a.Deleted == true).ExecuteDeleteAsync();
                await db.Schedules.Where(a => a.Deleted == true).ExecuteDeleteAsync();
                await db.WorkOrders.Where(a => a.Deleted == true).ExecuteDeleteAsync();
                return Results.Ok();
            });

            //----------------------------------------------------------------------------------------------------

        }

    }

}
