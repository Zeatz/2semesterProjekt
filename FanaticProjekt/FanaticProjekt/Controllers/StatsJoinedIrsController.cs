using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using FanaticProjekt;

namespace FanaticProjekt.Controllers
{
    public class StatsJoinedIrsController : ApiController
    {
        private DbModel db = new DbModel();

        // GET: api/StatsJoinedIrs
        public IQueryable<StatsJoinedIr> GetStatsJoinedIrs()
        {
            // selv sigende
            return db.StatsJoinedIrs;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}