using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using ConfApp.Domain;
using ConfApp.Domain.Exceptions;
using System.Linq;
using ConfApp.Domain.Data;
using ConfApp.Domain.Models;

namespace ConfApp.Data.Repositories
{
    public class ConferenceRepository : IConferenceRepository
    {
        private readonly IContext _context;

        public ConferenceRepository(IContext context)
        {
            _context = context;
        }

        public List<ConferenceSummary> FindAll(int top, int skip)
        {
            return _context
                .ConferenceSummaries
                .Skip(skip)
                .Take(top)
                .ToList();
        }

        public ConferenceDetails FindById(Guid id)
        {
            try
            {
                return _context
                .Conferences
                .First(x => x.Id == id);
            }
            catch (InvalidOperationException ex)
            {
                throw new EntityNotFoundException(nameof(Conference), id.ToString(), ex);
            }
        }

        public ConferenceDetails Save(ConferenceDetails conference)
        {
            conference.Id = Guid.NewGuid();
            _context.Conferences.Add(conference);
            _context.SaveChangesAsync().Wait();

            return conference;
        }
    }
}