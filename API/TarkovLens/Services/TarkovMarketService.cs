using Microsoft.Extensions.Options;
using Raven.Client.Documents.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TarkovLens.Services
{
    public interface ITarkovMarketService
    {

    }

    public class TarkovMarketService : ITarkovMarketService
    {
        private readonly IDocumentSession session;
        private readonly Secrets _secrets;

        public TarkovMarketService(IDocumentSession documentSession, IOptions<Secrets> secrets)
        {
            session = documentSession;
            _secrets = secrets.Value;
        }
    }
}
