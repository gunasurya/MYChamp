using MYChamp.DbContexts;
using MYChamp.Models.Authentication;

namespace MYChamp.Handlers
{
    public class SessionHandler
    {
        private readonly MYChampDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SessionHandler(MYChampDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
        }
        public bool SessionExists(string userId)
        {
            var result = _db.Session.FirstOrDefault(s => s.UserId == userId && s.IsActive);
            if (result != null)
            {
                _httpContextAccessor.HttpContext.Session.SetString("uniqeId", result.UniqueId);

                return true;
            }
            else
            {
                return false;
            }
        }
        public void AddSessionInformation(string sessionId, string userId, string ip, DateTime loggedTime)
        {
            var session = new Session
            {
                SessionId = sessionId,
                UniqueId = userId + ip + Guid.NewGuid(), 
                Forcefully_logout_by = string.Empty,
                UserId = userId,
                IpAddress = ip,
                LoginTime = loggedTime,
                IsActive = true,
                Status = 1,
                LogoutType = string.Empty
            };
            _httpContextAccessor.HttpContext.Session.SetString("current_uniqeid", session.UniqueId);
            Console.WriteLine(_httpContextAccessor.HttpContext.Session.GetString("current_uniqeid") + "current uniqueId");

            _db.Session.Add(session);
            _db.SaveChanges();
        }

        public void UpdateSessionInformation(string sessionId, string userName)
        {
            var session = _db.Session.FirstOrDefault(x => x.UserId == userName);
            if (session != null)
            {
                session.IsActive = false;
                session.Status = 0;
                session.LogoutType = "normal";

                _db.Session.Update(session);
                _db.SaveChanges();
            }
        }

        public bool check(string uniqueId)
        {
            var obj = _db.Session.FirstOrDefault(x => x.UniqueId == uniqueId && x.Forcefully_logout);
            if (obj != null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public void UpdateForceLogout(string username, string uniqueId)
        {
            var session = _db.Session.FirstOrDefault(x => x.UserId == username && x.IsActive);
            if (session != null)
            {
                session.Forcefully_logout = true;
                session.Forcefully_logout_by = uniqueId;
                session.IsActive = false;
                session.Status = 0;
                session.LogoutType = "abnormal";

                _db.Session.Update(session);
                _db.SaveChanges();
            }
        }

        public bool checkForcefulLogout()
        {
            string userid = _httpContextAccessor.HttpContext.Session.GetString("uniqeid");
            Console.WriteLine(userid + " userid is");
            var obj = _db.Session.FirstOrDefault(u => u.UniqueId == userid && u.Forcefully_logout);
            if (obj != null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
