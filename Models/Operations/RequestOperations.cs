using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DatingSite
{
    public class RequestOperations : Operations<Requests, int>
    {
        public RequestOperations(DataContext context) : base(context)
        {
        }

        public List<Requests> GetUsers(string id)
        {
            return Items.Include(x => x.RequestedBy)
                    .Where(request => request.RequestedTo_Id == id)
                    .OrderByDescending(x => x.RequestDate)
                    .ToList();
        }

        public List<Requests> GetSendedRequests(string userId)
        {
            return Items.Include(x => x.RequestedTo)
                    .Where(request => request.RequestedBy_Id == userId)
                    .OrderByDescending(x => x.RequestDate)
                    .ToList();
        }

        public List<Requests> GetRecievedRequests(string userId)
        {
            return Items.Include(x => x.RequestedBy)
                    .Where(request => request.RequestedTo_Id == userId)
                    .OrderByDescending(x => x.RequestDate)
                    .ToList();
        }

        public bool AlreadySent(string userId, string identityId)
        {
            var query = Items.Where(x => x.RequestedBy_Id == identityId
                    && x.RequestedTo_Id == userId).ToList();
            if (query.Count > 0) return true;
            else return false;
        }

        public bool Recieved(string userId, string identityId)
        {
            var query = Items.Where(x => x.RequestedBy_Id == userId
                    && x.RequestedTo_Id == identityId).ToList();
            if (query.Count > 0) return true;
            else return false;
        }
    }
}
