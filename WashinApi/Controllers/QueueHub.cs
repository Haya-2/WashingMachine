using Microsoft.AspNetCore.SignalR;

namespace WashinApi.Hub
{
    public class QueueHub : Microsoft.AspNetCore.SignalR.Hub
    {
        // Join a specific building's group
        public async Task JoinBuildingGroup(int buildingId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"Building_{buildingId}");
        }

        // Leave a specific building's group
        public async Task LeaveBuildingGroup(int buildingId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"Building_{buildingId}");
        }
    }
}